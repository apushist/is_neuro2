namespace AForge.WindowsForms
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
			this.cmbVideoSource = new System.Windows.Forms.ComboBox();
			this.label1 = new System.Windows.Forms.Label();
			this.StartButton = new System.Windows.Forms.Button();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.originalImageBox = new System.Windows.Forms.PictureBox();
			this.panel1 = new System.Windows.Forms.Panel();
			this.processedImgBox = new System.Windows.Forms.PictureBox();
			this.panel2 = new System.Windows.Forms.Panel();
			this.label8 = new System.Windows.Forms.Label();
			this.checkBox1 = new System.Windows.Forms.CheckBox();
			this.label2 = new System.Windows.Forms.Label();
			this.tresholdTrackBar = new System.Windows.Forms.TrackBar();
			this.label4 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.marginTrackBar = new System.Windows.Forms.TrackBar();
			this.borderTrackBar = new System.Windows.Forms.TrackBar();
			this.statusLabel = new System.Windows.Forms.Label();
			this.ticksLabel = new System.Windows.Forms.Label();
			this.resolutionsBox = new System.Windows.Forms.ComboBox();
			this.elapsedTimeLabel = new System.Windows.Forms.Label();
			this.progressBar1 = new System.Windows.Forms.ProgressBar();
			this.label5 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.netBox = new System.Windows.Forms.ComboBox();
			this.button1 = new System.Windows.Forms.Button();
			this.label7 = new System.Windows.Forms.Label();
			this.checkBox2 = new System.Windows.Forms.CheckBox();
			this.groupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.originalImageBox)).BeginInit();
			this.panel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.processedImgBox)).BeginInit();
			this.panel2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.tresholdTrackBar)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.marginTrackBar)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.borderTrackBar)).BeginInit();
			this.SuspendLayout();
			// 
			// cmbVideoSource
			// 
			this.cmbVideoSource.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.cmbVideoSource.FormattingEnabled = true;
			this.cmbVideoSource.Location = new System.Drawing.Point(20, 889);
			this.cmbVideoSource.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.cmbVideoSource.Name = "cmbVideoSource";
			this.cmbVideoSource.Size = new System.Drawing.Size(326, 28);
			this.cmbVideoSource.TabIndex = 1;
			this.cmbVideoSource.SelectionChangeCommitted += new System.EventHandler(this.cmbVideoSource_SelectionChangeCommitted);
			// 
			// label1
			// 
			this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(15, 865);
			this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(119, 20);
			this.label1.TabIndex = 2;
			this.label1.Text = "Выбор камеры";
			// 
			// StartButton
			// 
			this.StartButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.StartButton.Location = new System.Drawing.Point(357, 917);
			this.StartButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.StartButton.Name = "StartButton";
			this.StartButton.Size = new System.Drawing.Size(188, 46);
			this.StartButton.TabIndex = 3;
			this.StartButton.Text = "Старт";
			this.StartButton.UseVisualStyleBackColor = true;
			this.StartButton.Click += new System.EventHandler(this.btnStart_Click);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.originalImageBox);
			this.groupBox1.Location = new System.Drawing.Point(2, 0);
			this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.groupBox1.Size = new System.Drawing.Size(768, 798);
			this.groupBox1.TabIndex = 11;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Камера";
			// 
			// originalImageBox
			// 
			this.originalImageBox.Location = new System.Drawing.Point(9, 18);
			this.originalImageBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.originalImageBox.Name = "originalImageBox";
			this.originalImageBox.Size = new System.Drawing.Size(750, 769);
			this.originalImageBox.TabIndex = 1;
			this.originalImageBox.TabStop = false;
			// 
			// panel1
			// 
			this.panel1.BackColor = System.Drawing.SystemColors.ControlLight;
			this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.panel1.Controls.Add(this.processedImgBox);
			this.panel1.Location = new System.Drawing.Point(778, 18);
			this.panel1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(666, 769);
			this.panel1.TabIndex = 12;
			// 
			// processedImgBox
			// 
			this.processedImgBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.processedImgBox.Location = new System.Drawing.Point(0, 0);
			this.processedImgBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.processedImgBox.Name = "processedImgBox";
			this.processedImgBox.Size = new System.Drawing.Size(662, 765);
			this.processedImgBox.TabIndex = 0;
			this.processedImgBox.TabStop = false;
			// 
			// panel2
			// 
			this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panel2.Controls.Add(this.label8);
			this.panel2.Controls.Add(this.checkBox1);
			this.panel2.Controls.Add(this.label2);
			this.panel2.Controls.Add(this.tresholdTrackBar);
			this.panel2.Controls.Add(this.label4);
			this.panel2.Controls.Add(this.label3);
			this.panel2.Controls.Add(this.marginTrackBar);
			this.panel2.Controls.Add(this.borderTrackBar);
			this.panel2.Location = new System.Drawing.Point(778, 812);
			this.panel2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(662, 310);
			this.panel2.TabIndex = 18;
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Location = new System.Drawing.Point(259, 219);
			this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(58, 20);
			this.label8.TabIndex = 41;
			this.label8.Text = "Класс:";
			this.label8.Visible = false;
			// 
			// checkBox1
			// 
			this.checkBox1.AutoSize = true;
			this.checkBox1.Location = new System.Drawing.Point(261, 140);
			this.checkBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.checkBox1.Name = "checkBox1";
			this.checkBox1.Size = new System.Drawing.Size(128, 24);
			this.checkBox1.TabIndex = 24;
			this.checkBox1.Text = "Обработать";
			this.checkBox1.UseVisualStyleBackColor = true;
			this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(84, 151);
			this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(55, 20);
			this.label2.TabIndex = 23;
			this.label2.Text = "Порог";
			// 
			// tresholdTrackBar
			// 
			this.tresholdTrackBar.LargeChange = 1;
			this.tresholdTrackBar.Location = new System.Drawing.Point(10, 205);
			this.tresholdTrackBar.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.tresholdTrackBar.Maximum = 255;
			this.tresholdTrackBar.Name = "tresholdTrackBar";
			this.tresholdTrackBar.Size = new System.Drawing.Size(210, 69);
			this.tresholdTrackBar.TabIndex = 22;
			this.tresholdTrackBar.TickFrequency = 25;
			this.tresholdTrackBar.Value = 120;
			this.tresholdTrackBar.ValueChanged += new System.EventHandler(this.tresholdTrackBar_ValueChanged);
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(320, 14);
			this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(55, 20);
			this.label4.TabIndex = 21;
			this.label4.Text = "Зазор";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(92, 14);
			this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(49, 20);
			this.label3.TabIndex = 20;
			this.label3.Text = "Поля";
			// 
			// marginTrackBar
			// 
			this.marginTrackBar.LargeChange = 10;
			this.marginTrackBar.Location = new System.Drawing.Point(243, 48);
			this.marginTrackBar.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.marginTrackBar.Maximum = 40;
			this.marginTrackBar.Name = "marginTrackBar";
			this.marginTrackBar.Size = new System.Drawing.Size(210, 69);
			this.marginTrackBar.TabIndex = 19;
			this.marginTrackBar.TickFrequency = 4;
			this.marginTrackBar.Value = 10;
			this.marginTrackBar.ValueChanged += new System.EventHandler(this.marginTrackBar_ValueChanged);
			// 
			// borderTrackBar
			// 
			this.borderTrackBar.LargeChange = 60;
			this.borderTrackBar.Location = new System.Drawing.Point(10, 48);
			this.borderTrackBar.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.borderTrackBar.Maximum = 160;
			this.borderTrackBar.Name = "borderTrackBar";
			this.borderTrackBar.Size = new System.Drawing.Size(210, 69);
			this.borderTrackBar.TabIndex = 18;
			this.borderTrackBar.TickFrequency = 10;
			this.borderTrackBar.Value = 40;
			this.borderTrackBar.ValueChanged += new System.EventHandler(this.borderTrackBar_ValueChanged);
			// 
			// statusLabel
			// 
			this.statusLabel.AutoSize = true;
			this.statusLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.statusLabel.Location = new System.Drawing.Point(15, 812);
			this.statusLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.statusLabel.Name = "statusLabel";
			this.statusLabel.Size = new System.Drawing.Size(112, 32);
			this.statusLabel.TabIndex = 24;
			this.statusLabel.Text = "Статус:";
			// 
			// ticksLabel
			// 
			this.ticksLabel.AutoSize = true;
			this.ticksLabel.Location = new System.Drawing.Point(564, 895);
			this.ticksLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.ticksLabel.Name = "ticksLabel";
			this.ticksLabel.Size = new System.Drawing.Size(194, 20);
			this.ticksLabel.TabIndex = 30;
			this.ticksLabel.Text = "Ticks for frame processing";
			// 
			// resolutionsBox
			// 
			this.resolutionsBox.AllowDrop = true;
			this.resolutionsBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.resolutionsBox.FormattingEnabled = true;
			this.resolutionsBox.Location = new System.Drawing.Point(21, 931);
			this.resolutionsBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.resolutionsBox.Name = "resolutionsBox";
			this.resolutionsBox.Size = new System.Drawing.Size(325, 28);
			this.resolutionsBox.TabIndex = 34;
			// 
			// elapsedTimeLabel
			// 
			this.elapsedTimeLabel.AutoSize = true;
			this.elapsedTimeLabel.Location = new System.Drawing.Point(20, 1055);
			this.elapsedTimeLabel.Name = "elapsedTimeLabel";
			this.elapsedTimeLabel.Size = new System.Drawing.Size(62, 20);
			this.elapsedTimeLabel.TabIndex = 35;
			this.elapsedTimeLabel.Text = "Время:";
			// 
			// progressBar1
			// 
			this.progressBar1.Location = new System.Drawing.Point(20, 1009);
			this.progressBar1.Name = "progressBar1";
			this.progressBar1.Size = new System.Drawing.Size(326, 43);
			this.progressBar1.TabIndex = 36;
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(20, 983);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(0, 20);
			this.label5.TabIndex = 37;
			// 
			// label6
			// 
			this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(18, 1095);
			this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(142, 20);
			this.label6.TabIndex = 39;
			this.label6.Text = "Выбор нейросети";
			// 
			// netBox
			// 
			this.netBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.netBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.netBox.FormattingEnabled = true;
			this.netBox.Location = new System.Drawing.Point(24, 1120);
			this.netBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.netBox.Name = "netBox";
			this.netBox.Size = new System.Drawing.Size(326, 28);
			this.netBox.TabIndex = 38;
			this.netBox.SelectedIndexChanged += new System.EventHandler(this.netBox_SelectedIndexChanged);
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(392, 1088);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(75, 43);
			this.button1.TabIndex = 25;
			this.button1.Text = "Тест";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(483, 1098);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(84, 20);
			this.label7.TabIndex = 40;
			this.label7.Text = "Точность:";
			// 
			// checkBox2
			// 
			this.checkBox2.AutoSize = true;
			this.checkBox2.Checked = true;
			this.checkBox2.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkBox2.Location = new System.Drawing.Point(392, 1009);
			this.checkBox2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.checkBox2.Name = "checkBox2";
			this.checkBox2.Size = new System.Drawing.Size(298, 24);
			this.checkBox2.TabIndex = 25;
			this.checkBox2.Text = "Загрузить предобученную модель";
			this.checkBox2.UseVisualStyleBackColor = true;
			this.checkBox2.CheckedChanged += new System.EventHandler(this.checkBox2_CheckedChanged);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoSize = true;
			this.ClientSize = new System.Drawing.Size(1460, 1171);
			this.Controls.Add(this.checkBox2);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.netBox);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.progressBar1);
			this.Controls.Add(this.elapsedTimeLabel);
			this.Controls.Add(this.resolutionsBox);
			this.Controls.Add(this.ticksLabel);
			this.Controls.Add(this.statusLabel);
			this.Controls.Add(this.panel2);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.StartButton);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.cmbVideoSource);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.KeyPreview = true;
			this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.Name = "MainForm";
			this.Text = "Распознавалка";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyDown);
			this.groupBox1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.originalImageBox)).EndInit();
			this.panel1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.processedImgBox)).EndInit();
			this.panel2.ResumeLayout(false);
			this.panel2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.tresholdTrackBar)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.marginTrackBar)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.borderTrackBar)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ComboBox cmbVideoSource;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button StartButton;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.PictureBox originalImageBox;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TrackBar marginTrackBar;
        private System.Windows.Forms.TrackBar borderTrackBar;
        private System.Windows.Forms.Label statusLabel;
        private System.Windows.Forms.Label ticksLabel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TrackBar tresholdTrackBar;
        private System.Windows.Forms.PictureBox processedImgBox;
        private System.Windows.Forms.ComboBox resolutionsBox;
        private System.Windows.Forms.CheckBox checkBox1;
		private System.Windows.Forms.Label elapsedTimeLabel;
		private System.Windows.Forms.ProgressBar progressBar1;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.ComboBox netBox;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.CheckBox checkBox2;
		private System.Windows.Forms.Label label8;
	}
}

