using System.Diagnostics;
using System.IO;
using System.Linq;
using Accord.Neuro;
using Accord.Neuro.Learning;

namespace AForge.WindowsForms
{
    class AccordNet : BaseNetwork
    {
        /// <summary>
        /// Реализация нейронной сети из Accord.NET
        /// </summary>
        private ActivationNetwork network;

        //  Секундомер спортивный, завода «Агат», измеряет время пробегания стометровки, ну и время затраченное на обучение тоже умеет
        public Stopwatch stopWatch = new Stopwatch();

        /// <summary>
        /// Конструктор сети с указанием структуры (количество слоёв и нейронов в них)
        /// </summary>
        /// <param name="structure">Массив с указанием нейронов на каждом слое (включая сенсорный)</param>
        public AccordNet(SamplesSet data)
		{
            int[] structure = { 588, 256, 50, 10 };
			// Создаём сеть - вроде того
			network = new ActivationNetwork(new SigmoidFunction(2.0), structure[0], structure.Skip(1).ToArray());
			

			//  Встряска "мозгов" сети - рандомизируем веса связей
			new NguyenWidrow(network).Randomize();
        }

        //  Создаём "обучателя" - либо параллельного, либо последовательного  
        private ISupervisedLearning MakeTeacher()
        {
            return new ParallelResilientBackpropagationLearning(network);
        }

        public override double TrainOnDataSet(SamplesSet samplesSet, int epochsCount, double acceptableError)
        {
            //  Сначала надо сконструировать массивы входов и выходов
            double[][] inputs = new double[samplesSet.Count][];
            double[][] outputs = new double[samplesSet.Count][];

            //  Теперь массивы из samplesSet группируем в inputs и outputs
            for (int i = 0; i < samplesSet.Count; ++i)
            {
                inputs[i] = samplesSet[i].input;
                outputs[i] = samplesSet[i].Output;
            }

            //  Текущий счётчик эпох
            int epoch_to_run = 0;

            //  Создаём "обучателя" - либо параллельного, либо последовательного  
            var teacher = MakeTeacher();

            double error = double.PositiveInfinity;

#if DEBUG
            StreamWriter errorsFile = File.CreateText("errors.csv");
#endif

            stopWatch.Restart();

            while (epoch_to_run < epochsCount && error > acceptableError)
            {
                epoch_to_run++;
                error = teacher.RunEpoch(inputs, outputs);
#if DEBUG
                errorsFile.WriteLine(error);
#endif
                OnTrainProgress((epoch_to_run * 1.0) / epochsCount, error, stopWatch.Elapsed);
            }

#if DEBUG
            errorsFile.Close();
#endif

            OnTrainProgress(1.0, error, stopWatch.Elapsed);

            stopWatch.Stop();

            return error;
        }

        protected override double[] Compute(double[] input)
        {
            return network.Compute(input);
        }

		public override void SaveToBin()
		{
			using (FileStream fs = new FileStream(DataLoader.GetProjectRoot()+"\\networks\\accordNet.bin", FileMode.Create))
			{
				var formatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
				formatter.Serialize(fs, network);
			}
		}

		public override void LoadFromBin()
        {
			using (FileStream fs = new FileStream(DataLoader.GetProjectRoot() + "\\networks\\accordNet.bin", FileMode.Open))
			{
				var formatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
				network = (ActivationNetwork)formatter.Deserialize(fs);
			}
		}

	}
}