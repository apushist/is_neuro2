using Accord.MachineLearning.Bayes;
using Accord.MachineLearning.Boosting;
using Accord.Neuro;
using AForge.Video;
using AForge.Video.DirectShow;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AForge.WindowsForms
{
    delegate void FormUpdateDelegate();

    public partial class MainForm : Form
    {
        /// <summary>
        /// Класс, реализующий всю логику работы
        /// </summary>
        private Controller controller = null;

        /// <summary>
        /// Событие для синхронизации таймера
        /// </summary>
        private AutoResetEvent evnt = new AutoResetEvent(false);
                
        /// <summary>
        /// Список устройств для снятия видео (веб-камер)
        /// </summary>
        private FilterInfoCollection videoDevicesList;
        
        /// <summary>
        /// Выбранное устройство для видео
        /// </summary>
        private IVideoSource videoSource;
        
        /// <summary>
        /// Таймер для измерения производительности (времени на обработку кадра)
        /// </summary>
        private Stopwatch sw = new Stopwatch();
        
        /// <summary>
        /// Таймер для обновления объектов интерфейса
        /// </summary>
        System.Threading.Timer updateTmr;

		private Dictionary<string, BaseNetwork> networksCache = new Dictionary<string, BaseNetwork>();
		private Dictionary<string, bool> networksTrained = new Dictionary<string, bool>();
        private BaseNetwork network;
        private SamplesSet data = DataLoader.CreateFromImageFiles();
        private SamplesSet train = new SamplesSet();
        private SamplesSet test = new SamplesSet();

        private bool loadNetwork = true;

		/// <summary>
		/// Функция обновления формы, тут же происходит анализ текущего этапа, и при необходимости переключение на следующий
		/// Вызывается автоматически - это плохо, надо по делегатам вообще-то
		/// </summary>
		private void UpdateFormFields()
        {
            //  Проверяем, вызвана ли функция из потока главной формы. Если нет - вызов через Invoke
            //  для синхронизации, и выход
            if (statusLabel.InvokeRequired)
            {
                this.Invoke(new FormUpdateDelegate(UpdateFormFields));
                return;
            }

            sw.Stop();
            ticksLabel.Text = "Тики : " + sw.Elapsed.ToString();
            originalImageBox.Image = controller.GetOriginalImage();
            processedImgBox.Image = controller.GetProcessedImage();
        }

        /// <summary>
        /// Обёртка для обновления формы - перерисовки картинок, изменения состояния и прочего
        /// </summary>
        /// <param name="StateInfo"></param>
        public void Tick(object StateInfo)
        {
            UpdateFormFields();
            return;
        }

        public MainForm()
        {
            InitializeComponent();
            // Список камер получаем
            videoDevicesList = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            foreach (FilterInfo videoDevice in videoDevicesList)
            {
                cmbVideoSource.Items.Add(videoDevice.Name);
            }
            if (cmbVideoSource.Items.Count > 0)
            {
                cmbVideoSource.SelectedIndex = 0;
            }
            else
            {
                MessageBox.Show("А нет у вас камеры!", "Ошибочка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            controller = new Controller(new FormUpdateDelegate(UpdateFormFields));
			//updateTmr = new System.Threading.Timer(Tick, evnt, 500, 100);


			networksCache = new Dictionary<string, BaseNetwork> {
                { "Accord.Net Perseptron", new AccordNet(data)},
                {"Студентческий персептрон", new StudentNetwork(data) }
			};
            networksTrained = new Dictionary<string, bool>{
				{ "Accord.Net Perseptron", false},
				{"Студентческий персептрон", false }
			};
			netBox.Items.AddRange(networksCache.Keys.Select(s => (object)s).ToArray());

            int trainCount = (int)(data.Count * 0.8);

            for (int i = 0; i < data.Count; i++)
            {
                if (i < trainCount)
                    train.AddSample(data[i]);
                else
                    test.AddSample(data[i]);
            }
		
		}

		private void video_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            //  Время засекаем
            sw.Restart();

            //  Отправляем изображение на обработку, и выводим оригинал (с раскраской) и разрезанные изображения
            if(controller.Ready)
                
                #pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
                controller.ProcessImage((Bitmap)eventArgs.Frame.Clone());
                #pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed

        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (videoSource == null)
            {
                var vcd = new VideoCaptureDevice(videoDevicesList[cmbVideoSource.SelectedIndex].MonikerString);
                vcd.VideoResolution = vcd.VideoCapabilities[resolutionsBox.SelectedIndex];
                Debug.WriteLine(vcd.VideoCapabilities[1].FrameSize.ToString());
                Debug.WriteLine(resolutionsBox.SelectedIndex);
                videoSource = vcd;
                videoSource.NewFrame += new NewFrameEventHandler(video_NewFrame);
                videoSource.Start();
                StartButton.Text = "Стоп";
                cmbVideoSource.Enabled = false;
            }
            else
            {
                videoSource.SignalToStop();
                if (videoSource != null && videoSource.IsRunning && originalImageBox.Image != null)
                {
                    originalImageBox.Image.Dispose();
                }
                videoSource = null;
                StartButton.Text = "Старт";
                cmbVideoSource.Enabled = true;
            }
        }

        private void tresholdTrackBar_ValueChanged(object sender, EventArgs e)
        {
            controller.settings.threshold = (byte)tresholdTrackBar.Value;
            controller.settings.differenceLim = (float)tresholdTrackBar.Value/tresholdTrackBar.Maximum;
        }

        private void borderTrackBar_ValueChanged(object sender, EventArgs e)
        {
            controller.settings.border = borderTrackBar.Value;
        }

        private void marginTrackBar_ValueChanged(object sender, EventArgs e)
        {
            controller.settings.margin = marginTrackBar.Value;
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (updateTmr != null)
                updateTmr.Dispose();
            

            if (videoSource != null && videoSource.IsRunning)
            {
                videoSource.SignalToStop();
            }
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            switch(e.KeyCode)
            {
                case Keys.W: controller.settings.decTop(); Debug.WriteLine("Up!"); break;
                case Keys.S: controller.settings.incTop(); Debug.WriteLine("Down!"); break;
                case Keys.A: controller.settings.decLeft(); Debug.WriteLine("Left!"); break;
                case Keys.D: controller.settings.incLeft(); Debug.WriteLine("Right!"); break;
                case Keys.Q: controller.settings.border++; Debug.WriteLine("Plus!"); break;
                case Keys.E: controller.settings.border--; Debug.WriteLine("Minus!"); break;
            }
        }

        private void cmbVideoSource_SelectionChangeCommitted(object sender, EventArgs e)
        {
            var vcd = new VideoCaptureDevice(videoDevicesList[cmbVideoSource.SelectedIndex].MonikerString);
            resolutionsBox.Items.Clear();
            for (int i = 0; i < vcd.VideoCapabilities.Length; i++)
                resolutionsBox.Items.Add(vcd.VideoCapabilities[i].FrameSize.ToString());
            resolutionsBox.SelectedIndex = 0;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            controller.settings.processImg = checkBox1.Checked;
        }

		public void UpdateLearningInfo(double progress, double error, TimeSpan elapsedTime)
		{
			if (progressBar1.InvokeRequired)
			{
				progressBar1.Invoke(new TrainProgressHandler(UpdateLearningInfo),
								   progress, error, elapsedTime);
				return;
			}

			label5.Text = "Ошибка: " + error;
			int progressPercent = (int)System.Math.Round(progress * 100);
			elapsedTimeLabel.Text = "Затраченное время: " + elapsedTime.ToString(@"hh\:mm\:ss\:ff");
			progressBar1.Value = progressPercent;
		}

		private void netBox_SelectedIndexChanged(object sender, EventArgs e)
		{
            string selected = netBox.SelectedItem.ToString();
			network = networksCache[selected];
            if (loadNetwork)
            {
                network.LoadFromBin();
            }
            else if (!networksTrained[selected])
            {
				network.TrainProgress += UpdateLearningInfo;
				network.TrainOnDataSet(train, 150, 0.01);
                networksTrained[selected] = true;
                network.SaveToBin();
			}
            else
            {
                label5.Text = "Модель обучена и готова";
			}
			controller.settings.Network = network;
		}

		private void button1_Click(object sender, EventArgs e)
		{
            double accuracy = test.TestNeuralNetwork(network);
            label7.Text = $"Точность на тестовой выборке : {accuracy * 100,5:F2}%";
        }


		private void checkBox2_CheckedChanged(object sender, EventArgs e)
		{
            loadNetwork = checkBox2.Checked;
		}
	}
}
