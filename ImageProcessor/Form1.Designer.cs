namespace ImageProcessor
{
    partial class Form1
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.PreviewPictureBox = new System.Windows.Forms.PictureBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.ProcessBothTrain = new System.Windows.Forms.Button();
            this.TrainNotFacePrefixTextBox = new System.Windows.Forms.TextBox();
            this.ProcessTrainFacesButton = new System.Windows.Forms.Button();
            this.TrainFacePrefixTextBox = new System.Windows.Forms.TextBox();
            this.ProcessTrainNotFacesButton = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.Kirsch3x3x1RadioButton = new System.Windows.Forms.RadioButton();
            this.Sobel3x3x1RadioButton = new System.Windows.Forms.RadioButton();
            this.TestingGroupBox = new System.Windows.Forms.GroupBox();
            this.DisplayAsRawRGBButton = new System.Windows.Forms.Button();
            this.PreviewOperatorButton = new System.Windows.Forms.Button();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.ProcessBothTestButton = new System.Windows.Forms.Button();
            this.TestNotFacePrefixTextBox = new System.Windows.Forms.TextBox();
            this.ProcessTestNotFacesButton = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.TestFacePrefixTextBox = new System.Windows.Forms.TextBox();
            this.ProcessTestFaceButton = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.ExitButton = new System.Windows.Forms.Button();
            this.ProcessAllButton = new System.Windows.Forms.Button();
            this.RenameFilesButton = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.Prewitt3x3x4RadioButton = new System.Windows.Forms.RadioButton();
            this.Prewitt3x3x8RadioButton = new System.Windows.Forms.RadioButton();
            this.Prewitt5x5x4RadioButton = new System.Windows.Forms.RadioButton();
            this.Kirsch3x3x4RadioButton = new System.Windows.Forms.RadioButton();
            this.Kirsch3x3x8RadioButton = new System.Windows.Forms.RadioButton();
            this.Sobel3x3x4RadioButton = new System.Windows.Forms.RadioButton();
            this.Sobel3x3x8RadioButton = new System.Windows.Forms.RadioButton();
            this.Sobel5x5x4RadioButton = new System.Windows.Forms.RadioButton();
            this.Scharr3x3x4RadioButton = new System.Windows.Forms.RadioButton();
            this.Scharr3x3x8RadioButton = new System.Windows.Forms.RadioButton();
            this.Scharr5x5x4RadioButton = new System.Windows.Forms.RadioButton();
            this.Isotropic3x3x4RadioButton = new System.Windows.Forms.RadioButton();
            this.Isotropic3x3x8RadioButton = new System.Windows.Forms.RadioButton();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PreviewPictureBox)).BeginInit();
            this.groupBox6.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.TestingGroupBox.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.PreviewPictureBox);
            this.panel1.Controls.Add(this.groupBox6);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.TestingGroupBox);
            this.panel1.Controls.Add(this.groupBox5);
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Location = new System.Drawing.Point(12, 8);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(380, 827);
            this.panel1.TabIndex = 1;
            // 
            // PreviewPictureBox
            // 
            this.PreviewPictureBox.Location = new System.Drawing.Point(3, 459);
            this.PreviewPictureBox.Name = "PreviewPictureBox";
            this.PreviewPictureBox.Size = new System.Drawing.Size(370, 363);
            this.PreviewPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PreviewPictureBox.TabIndex = 3;
            this.PreviewPictureBox.TabStop = false;
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.label1);
            this.groupBox6.Controls.Add(this.label2);
            this.groupBox6.Controls.Add(this.ProcessBothTrain);
            this.groupBox6.Controls.Add(this.TrainNotFacePrefixTextBox);
            this.groupBox6.Controls.Add(this.ProcessTrainFacesButton);
            this.groupBox6.Controls.Add(this.TrainFacePrefixTextBox);
            this.groupBox6.Controls.Add(this.ProcessTrainNotFacesButton);
            this.groupBox6.Location = new System.Drawing.Point(3, 123);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(223, 162);
            this.groupBox6.TabIndex = 14;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Train Image Set";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 111);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 13);
            this.label1.TabIndex = 13;
            this.label1.Text = "Train Face prefix:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 140);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(109, 13);
            this.label2.TabIndex = 15;
            this.label2.Text = "Train Not Face prefix:";
            // 
            // ProcessBothTrain
            // 
            this.ProcessBothTrain.Location = new System.Drawing.Point(6, 77);
            this.ProcessBothTrain.Name = "ProcessBothTrain";
            this.ProcessBothTrain.Size = new System.Drawing.Size(211, 23);
            this.ProcessBothTrain.TabIndex = 6;
            this.ProcessBothTrain.Text = "Process Both";
            this.ProcessBothTrain.UseVisualStyleBackColor = true;
            this.ProcessBothTrain.Click += new System.EventHandler(this.TrainFaceBoth_Click);
            // 
            // TrainNotFacePrefixTextBox
            // 
            this.TrainNotFacePrefixTextBox.Location = new System.Drawing.Point(123, 137);
            this.TrainNotFacePrefixTextBox.Name = "TrainNotFacePrefixTextBox";
            this.TrainNotFacePrefixTextBox.Size = new System.Drawing.Size(94, 20);
            this.TrainNotFacePrefixTextBox.TabIndex = 14;
            this.TrainNotFacePrefixTextBox.Text = "cmu_";
            // 
            // ProcessTrainFacesButton
            // 
            this.ProcessTrainFacesButton.Location = new System.Drawing.Point(6, 19);
            this.ProcessTrainFacesButton.Name = "ProcessTrainFacesButton";
            this.ProcessTrainFacesButton.Size = new System.Drawing.Size(211, 23);
            this.ProcessTrainFacesButton.TabIndex = 7;
            this.ProcessTrainFacesButton.Text = "Process - Train Faces";
            this.ProcessTrainFacesButton.UseVisualStyleBackColor = true;
            this.ProcessTrainFacesButton.Click += new System.EventHandler(this.ProcessTrainFacesButton_Click);
            // 
            // TrainFacePrefixTextBox
            // 
            this.TrainFacePrefixTextBox.Location = new System.Drawing.Point(123, 108);
            this.TrainFacePrefixTextBox.Name = "TrainFacePrefixTextBox";
            this.TrainFacePrefixTextBox.Size = new System.Drawing.Size(94, 20);
            this.TrainFacePrefixTextBox.TabIndex = 4;
            this.TrainFacePrefixTextBox.Text = "cmu_";
            // 
            // ProcessTrainNotFacesButton
            // 
            this.ProcessTrainNotFacesButton.Location = new System.Drawing.Point(6, 48);
            this.ProcessTrainNotFacesButton.Name = "ProcessTrainNotFacesButton";
            this.ProcessTrainNotFacesButton.Size = new System.Drawing.Size(211, 23);
            this.ProcessTrainNotFacesButton.TabIndex = 8;
            this.ProcessTrainNotFacesButton.Text = "Process - Train Not Faces";
            this.ProcessTrainNotFacesButton.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.Isotropic3x3x8RadioButton);
            this.groupBox1.Controls.Add(this.Isotropic3x3x4RadioButton);
            this.groupBox1.Controls.Add(this.Scharr5x5x4RadioButton);
            this.groupBox1.Controls.Add(this.Scharr3x3x8RadioButton);
            this.groupBox1.Controls.Add(this.Scharr3x3x4RadioButton);
            this.groupBox1.Controls.Add(this.Sobel5x5x4RadioButton);
            this.groupBox1.Controls.Add(this.Sobel3x3x8RadioButton);
            this.groupBox1.Controls.Add(this.Sobel3x3x4RadioButton);
            this.groupBox1.Controls.Add(this.Kirsch3x3x8RadioButton);
            this.groupBox1.Controls.Add(this.Kirsch3x3x4RadioButton);
            this.groupBox1.Controls.Add(this.Prewitt5x5x4RadioButton);
            this.groupBox1.Controls.Add(this.Prewitt3x3x8RadioButton);
            this.groupBox1.Controls.Add(this.Prewitt3x3x4RadioButton);
            this.groupBox1.Controls.Add(this.Kirsch3x3x1RadioButton);
            this.groupBox1.Controls.Add(this.Sobel3x3x1RadioButton);
            this.groupBox1.Location = new System.Drawing.Point(231, 90);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox1.Size = new System.Drawing.Size(142, 363);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Operators";
            // 
            // Kirsch3x3x1RadioButton
            // 
            this.Kirsch3x3x1RadioButton.AutoSize = true;
            this.Kirsch3x3x1RadioButton.Location = new System.Drawing.Point(10, 86);
            this.Kirsch3x3x1RadioButton.Margin = new System.Windows.Forms.Padding(2);
            this.Kirsch3x3x1RadioButton.Name = "Kirsch3x3x1RadioButton";
            this.Kirsch3x3x1RadioButton.Size = new System.Drawing.Size(118, 17);
            this.Kirsch3x3x1RadioButton.TabIndex = 1;
            this.Kirsch3x3x1RadioButton.TabStop = true;
            this.Kirsch3x3x1RadioButton.Text = "Kirsch 3x3 1 Rotate";
            this.Kirsch3x3x1RadioButton.UseVisualStyleBackColor = true;
            this.Kirsch3x3x1RadioButton.CheckedChanged += new System.EventHandler(this.Kirsch3x3x1RadioButton_CheckedChanged);
            // 
            // Sobel3x3x1RadioButton
            // 
            this.Sobel3x3x1RadioButton.AutoSize = true;
            this.Sobel3x3x1RadioButton.Location = new System.Drawing.Point(10, 155);
            this.Sobel3x3x1RadioButton.Margin = new System.Windows.Forms.Padding(2);
            this.Sobel3x3x1RadioButton.Name = "Sobel3x3x1RadioButton";
            this.Sobel3x3x1RadioButton.Size = new System.Drawing.Size(118, 17);
            this.Sobel3x3x1RadioButton.TabIndex = 0;
            this.Sobel3x3x1RadioButton.TabStop = true;
            this.Sobel3x3x1RadioButton.Text = "Sobel 3x3 1xRotate";
            this.Sobel3x3x1RadioButton.UseVisualStyleBackColor = true;
            this.Sobel3x3x1RadioButton.CheckedChanged += new System.EventHandler(this.Sobel3x3x1RadioButton_CheckedChanged);
            // 
            // TestingGroupBox
            // 
            this.TestingGroupBox.Controls.Add(this.DisplayAsRawRGBButton);
            this.TestingGroupBox.Controls.Add(this.PreviewOperatorButton);
            this.TestingGroupBox.Location = new System.Drawing.Point(232, 3);
            this.TestingGroupBox.Name = "TestingGroupBox";
            this.TestingGroupBox.Size = new System.Drawing.Size(142, 82);
            this.TestingGroupBox.TabIndex = 9;
            this.TestingGroupBox.TabStop = false;
            this.TestingGroupBox.Text = "Debug";
            // 
            // DisplayAsRawRGBButton
            // 
            this.DisplayAsRawRGBButton.Location = new System.Drawing.Point(9, 19);
            this.DisplayAsRawRGBButton.Name = "DisplayAsRawRGBButton";
            this.DisplayAsRawRGBButton.Size = new System.Drawing.Size(119, 23);
            this.DisplayAsRawRGBButton.TabIndex = 2;
            this.DisplayAsRawRGBButton.Text = "Display as Raw RGB";
            this.DisplayAsRawRGBButton.UseVisualStyleBackColor = true;
            this.DisplayAsRawRGBButton.Click += new System.EventHandler(this.DisplayAsRawRGBButton_Click);
            // 
            // PreviewOperatorButton
            // 
            this.PreviewOperatorButton.Location = new System.Drawing.Point(9, 48);
            this.PreviewOperatorButton.Name = "PreviewOperatorButton";
            this.PreviewOperatorButton.Size = new System.Drawing.Size(119, 24);
            this.PreviewOperatorButton.TabIndex = 5;
            this.PreviewOperatorButton.Text = "Preview Operator";
            this.PreviewOperatorButton.UseVisualStyleBackColor = true;
            this.PreviewOperatorButton.Click += new System.EventHandler(this.PreviewOperatorButton_Click);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.label4);
            this.groupBox5.Controls.Add(this.ProcessBothTestButton);
            this.groupBox5.Controls.Add(this.TestNotFacePrefixTextBox);
            this.groupBox5.Controls.Add(this.ProcessTestNotFacesButton);
            this.groupBox5.Controls.Add(this.label3);
            this.groupBox5.Controls.Add(this.TestFacePrefixTextBox);
            this.groupBox5.Controls.Add(this.ProcessTestFaceButton);
            this.groupBox5.Location = new System.Drawing.Point(3, 291);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(223, 162);
            this.groupBox5.TabIndex = 13;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Test Image Set";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 142);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(106, 13);
            this.label4.TabIndex = 19;
            this.label4.Text = "Test Not Face prefix:";
            // 
            // ProcessBothTestButton
            // 
            this.ProcessBothTestButton.Location = new System.Drawing.Point(6, 78);
            this.ProcessBothTestButton.Name = "ProcessBothTestButton";
            this.ProcessBothTestButton.Size = new System.Drawing.Size(209, 23);
            this.ProcessBothTestButton.TabIndex = 11;
            this.ProcessBothTestButton.Text = "Process Both";
            this.ProcessBothTestButton.UseVisualStyleBackColor = true;
            // 
            // TestNotFacePrefixTextBox
            // 
            this.TestNotFacePrefixTextBox.Location = new System.Drawing.Point(121, 139);
            this.TestNotFacePrefixTextBox.Name = "TestNotFacePrefixTextBox";
            this.TestNotFacePrefixTextBox.Size = new System.Drawing.Size(94, 20);
            this.TestNotFacePrefixTextBox.TabIndex = 18;
            this.TestNotFacePrefixTextBox.Text = "cmu_";
            // 
            // ProcessTestNotFacesButton
            // 
            this.ProcessTestNotFacesButton.Location = new System.Drawing.Point(6, 48);
            this.ProcessTestNotFacesButton.Name = "ProcessTestNotFacesButton";
            this.ProcessTestNotFacesButton.Size = new System.Drawing.Size(209, 23);
            this.ProcessTestNotFacesButton.TabIndex = 10;
            this.ProcessTestNotFacesButton.Text = "Process - Test Not Faces";
            this.ProcessTestNotFacesButton.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 114);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(86, 13);
            this.label3.TabIndex = 17;
            this.label3.Text = "Test Face prefix:";
            // 
            // TestFacePrefixTextBox
            // 
            this.TestFacePrefixTextBox.Location = new System.Drawing.Point(121, 111);
            this.TestFacePrefixTextBox.Name = "TestFacePrefixTextBox";
            this.TestFacePrefixTextBox.Size = new System.Drawing.Size(94, 20);
            this.TestFacePrefixTextBox.TabIndex = 16;
            this.TestFacePrefixTextBox.Text = "cmu_";
            // 
            // ProcessTestFaceButton
            // 
            this.ProcessTestFaceButton.Location = new System.Drawing.Point(6, 19);
            this.ProcessTestFaceButton.Name = "ProcessTestFaceButton";
            this.ProcessTestFaceButton.Size = new System.Drawing.Size(209, 23);
            this.ProcessTestFaceButton.TabIndex = 9;
            this.ProcessTestFaceButton.Text = "Process - Test Faces";
            this.ProcessTestFaceButton.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.ExitButton);
            this.groupBox2.Controls.Add(this.ProcessAllButton);
            this.groupBox2.Controls.Add(this.RenameFilesButton);
            this.groupBox2.Location = new System.Drawing.Point(3, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(223, 114);
            this.groupBox2.TabIndex = 10;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Bulk Processing Actions";
            // 
            // ExitButton
            // 
            this.ExitButton.Location = new System.Drawing.Point(6, 77);
            this.ExitButton.Name = "ExitButton";
            this.ExitButton.Size = new System.Drawing.Size(211, 27);
            this.ExitButton.TabIndex = 1;
            this.ExitButton.Text = "Exit";
            this.ExitButton.UseVisualStyleBackColor = true;
            this.ExitButton.Click += new System.EventHandler(this.ExitButton_Click_1);
            // 
            // ProcessAllButton
            // 
            this.ProcessAllButton.Location = new System.Drawing.Point(6, 19);
            this.ProcessAllButton.Name = "ProcessAllButton";
            this.ProcessAllButton.Size = new System.Drawing.Size(211, 23);
            this.ProcessAllButton.TabIndex = 8;
            this.ProcessAllButton.Text = "Process All";
            this.ProcessAllButton.UseVisualStyleBackColor = true;
            this.ProcessAllButton.Click += new System.EventHandler(this.ProcessAllImageSetsButton_Click);
            // 
            // RenameFilesButton
            // 
            this.RenameFilesButton.Location = new System.Drawing.Point(6, 48);
            this.RenameFilesButton.Name = "RenameFilesButton";
            this.RenameFilesButton.Size = new System.Drawing.Size(211, 23);
            this.RenameFilesButton.TabIndex = 3;
            this.RenameFilesButton.Text = "Rename Files";
            this.RenameFilesButton.UseVisualStyleBackColor = true;
            this.RenameFilesButton.Click += new System.EventHandler(this.RenameFilesButton_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(398, 8);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox1.Size = new System.Drawing.Size(1020, 827);
            this.textBox1.TabIndex = 2;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 836);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1430, 22);
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // Prewitt3x3x4RadioButton
            // 
            this.Prewitt3x3x4RadioButton.AutoSize = true;
            this.Prewitt3x3x4RadioButton.Location = new System.Drawing.Point(10, 17);
            this.Prewitt3x3x4RadioButton.Margin = new System.Windows.Forms.Padding(2);
            this.Prewitt3x3x4RadioButton.Name = "Prewitt3x3x4RadioButton";
            this.Prewitt3x3x4RadioButton.Size = new System.Drawing.Size(123, 17);
            this.Prewitt3x3x4RadioButton.TabIndex = 2;
            this.Prewitt3x3x4RadioButton.TabStop = true;
            this.Prewitt3x3x4RadioButton.Text = "Prewitt 3x3 4xRotate";
            this.Prewitt3x3x4RadioButton.UseVisualStyleBackColor = true;
            this.Prewitt3x3x4RadioButton.CheckedChanged += new System.EventHandler(this.Prewitt3x3x4RadioButton_CheckedChanged);
            // 
            // Prewitt3x3x8RadioButton
            // 
            this.Prewitt3x3x8RadioButton.AutoSize = true;
            this.Prewitt3x3x8RadioButton.Location = new System.Drawing.Point(10, 40);
            this.Prewitt3x3x8RadioButton.Margin = new System.Windows.Forms.Padding(2);
            this.Prewitt3x3x8RadioButton.Name = "Prewitt3x3x8RadioButton";
            this.Prewitt3x3x8RadioButton.Size = new System.Drawing.Size(123, 17);
            this.Prewitt3x3x8RadioButton.TabIndex = 3;
            this.Prewitt3x3x8RadioButton.TabStop = true;
            this.Prewitt3x3x8RadioButton.Text = "Prewitt 3x3 8xRotate";
            this.Prewitt3x3x8RadioButton.UseVisualStyleBackColor = true;
            this.Prewitt3x3x8RadioButton.CheckedChanged += new System.EventHandler(this.Prewitt3x3x8RadioButton_CheckedChanged);
            // 
            // Prewitt5x5x4RadioButton
            // 
            this.Prewitt5x5x4RadioButton.AutoSize = true;
            this.Prewitt5x5x4RadioButton.Location = new System.Drawing.Point(10, 63);
            this.Prewitt5x5x4RadioButton.Margin = new System.Windows.Forms.Padding(2);
            this.Prewitt5x5x4RadioButton.Name = "Prewitt5x5x4RadioButton";
            this.Prewitt5x5x4RadioButton.Size = new System.Drawing.Size(123, 17);
            this.Prewitt5x5x4RadioButton.TabIndex = 4;
            this.Prewitt5x5x4RadioButton.TabStop = true;
            this.Prewitt5x5x4RadioButton.Text = "Prewitt 5x5 4xRotate";
            this.Prewitt5x5x4RadioButton.UseVisualStyleBackColor = true;
            this.Prewitt5x5x4RadioButton.CheckedChanged += new System.EventHandler(this.Prewitt5x5x4RadioButton_CheckedChanged);
            // 
            // Kirsch3x3x4RadioButton
            // 
            this.Kirsch3x3x4RadioButton.AutoSize = true;
            this.Kirsch3x3x4RadioButton.Location = new System.Drawing.Point(10, 109);
            this.Kirsch3x3x4RadioButton.Margin = new System.Windows.Forms.Padding(2);
            this.Kirsch3x3x4RadioButton.Name = "Kirsch3x3x4RadioButton";
            this.Kirsch3x3x4RadioButton.Size = new System.Drawing.Size(120, 17);
            this.Kirsch3x3x4RadioButton.TabIndex = 5;
            this.Kirsch3x3x4RadioButton.TabStop = true;
            this.Kirsch3x3x4RadioButton.Text = "Kirsch 3x3 4xRotate";
            this.Kirsch3x3x4RadioButton.UseVisualStyleBackColor = true;
            this.Kirsch3x3x4RadioButton.CheckedChanged += new System.EventHandler(this.Kirsch3x3x4RadioButton_CheckedChanged);
            // 
            // Kirsch3x3x8RadioButton
            // 
            this.Kirsch3x3x8RadioButton.AutoSize = true;
            this.Kirsch3x3x8RadioButton.Location = new System.Drawing.Point(10, 132);
            this.Kirsch3x3x8RadioButton.Margin = new System.Windows.Forms.Padding(2);
            this.Kirsch3x3x8RadioButton.Name = "Kirsch3x3x8RadioButton";
            this.Kirsch3x3x8RadioButton.Size = new System.Drawing.Size(120, 17);
            this.Kirsch3x3x8RadioButton.TabIndex = 6;
            this.Kirsch3x3x8RadioButton.TabStop = true;
            this.Kirsch3x3x8RadioButton.Text = "Kirsch 3x3 8xRotate";
            this.Kirsch3x3x8RadioButton.UseVisualStyleBackColor = true;
            this.Kirsch3x3x8RadioButton.CheckedChanged += new System.EventHandler(this.Kirsch3x3x8RadioButton_CheckedChanged);
            // 
            // Sobel3x3x4RadioButton
            // 
            this.Sobel3x3x4RadioButton.AutoSize = true;
            this.Sobel3x3x4RadioButton.Location = new System.Drawing.Point(10, 178);
            this.Sobel3x3x4RadioButton.Margin = new System.Windows.Forms.Padding(2);
            this.Sobel3x3x4RadioButton.Name = "Sobel3x3x4RadioButton";
            this.Sobel3x3x4RadioButton.Size = new System.Drawing.Size(118, 17);
            this.Sobel3x3x4RadioButton.TabIndex = 7;
            this.Sobel3x3x4RadioButton.TabStop = true;
            this.Sobel3x3x4RadioButton.Text = "Sobel 3x3 4xRotate";
            this.Sobel3x3x4RadioButton.UseVisualStyleBackColor = true;
            this.Sobel3x3x4RadioButton.CheckedChanged += new System.EventHandler(this.Sobel3x3x4RadioButton_CheckedChanged);
            // 
            // Sobel3x3x8RadioButton
            // 
            this.Sobel3x3x8RadioButton.AutoSize = true;
            this.Sobel3x3x8RadioButton.Location = new System.Drawing.Point(10, 201);
            this.Sobel3x3x8RadioButton.Margin = new System.Windows.Forms.Padding(2);
            this.Sobel3x3x8RadioButton.Name = "Sobel3x3x8RadioButton";
            this.Sobel3x3x8RadioButton.Size = new System.Drawing.Size(118, 17);
            this.Sobel3x3x8RadioButton.TabIndex = 8;
            this.Sobel3x3x8RadioButton.TabStop = true;
            this.Sobel3x3x8RadioButton.Text = "Sobel 3x3 8xRotate";
            this.Sobel3x3x8RadioButton.UseVisualStyleBackColor = true;
            this.Sobel3x3x8RadioButton.CheckedChanged += new System.EventHandler(this.Sobel3x3x8RadioButton_CheckedChanged);
            // 
            // Sobel5x5x4RadioButton
            // 
            this.Sobel5x5x4RadioButton.AutoSize = true;
            this.Sobel5x5x4RadioButton.Location = new System.Drawing.Point(10, 224);
            this.Sobel5x5x4RadioButton.Margin = new System.Windows.Forms.Padding(2);
            this.Sobel5x5x4RadioButton.Name = "Sobel5x5x4RadioButton";
            this.Sobel5x5x4RadioButton.Size = new System.Drawing.Size(118, 17);
            this.Sobel5x5x4RadioButton.TabIndex = 9;
            this.Sobel5x5x4RadioButton.TabStop = true;
            this.Sobel5x5x4RadioButton.Text = "Sobel 5x5 4xRotate";
            this.Sobel5x5x4RadioButton.UseVisualStyleBackColor = true;
            this.Sobel5x5x4RadioButton.CheckedChanged += new System.EventHandler(this.Sobel5x5x4RadioButton_CheckedChanged);
            // 
            // Scharr3x3x4RadioButton
            // 
            this.Scharr3x3x4RadioButton.AutoSize = true;
            this.Scharr3x3x4RadioButton.Location = new System.Drawing.Point(10, 247);
            this.Scharr3x3x4RadioButton.Margin = new System.Windows.Forms.Padding(2);
            this.Scharr3x3x4RadioButton.Name = "Scharr3x3x4RadioButton";
            this.Scharr3x3x4RadioButton.Size = new System.Drawing.Size(122, 17);
            this.Scharr3x3x4RadioButton.TabIndex = 10;
            this.Scharr3x3x4RadioButton.TabStop = true;
            this.Scharr3x3x4RadioButton.Text = "Scharr 3x3 4xRotate";
            this.Scharr3x3x4RadioButton.UseVisualStyleBackColor = true;
            this.Scharr3x3x4RadioButton.CheckedChanged += new System.EventHandler(this.Scharr3x3x4RadioButton_CheckedChanged);
            // 
            // Scharr3x3x8RadioButton
            // 
            this.Scharr3x3x8RadioButton.AutoSize = true;
            this.Scharr3x3x8RadioButton.Location = new System.Drawing.Point(10, 270);
            this.Scharr3x3x8RadioButton.Margin = new System.Windows.Forms.Padding(2);
            this.Scharr3x3x8RadioButton.Name = "Scharr3x3x8RadioButton";
            this.Scharr3x3x8RadioButton.Size = new System.Drawing.Size(122, 17);
            this.Scharr3x3x8RadioButton.TabIndex = 11;
            this.Scharr3x3x8RadioButton.TabStop = true;
            this.Scharr3x3x8RadioButton.Text = "Scharr 3x3 8xRotate";
            this.Scharr3x3x8RadioButton.UseVisualStyleBackColor = true;
            this.Scharr3x3x8RadioButton.CheckedChanged += new System.EventHandler(this.Scharr3x3x8RadioButton_CheckedChanged);
            // 
            // Scharr5x5x4RadioButton
            // 
            this.Scharr5x5x4RadioButton.AutoSize = true;
            this.Scharr5x5x4RadioButton.Location = new System.Drawing.Point(10, 293);
            this.Scharr5x5x4RadioButton.Margin = new System.Windows.Forms.Padding(2);
            this.Scharr5x5x4RadioButton.Name = "Scharr5x5x4RadioButton";
            this.Scharr5x5x4RadioButton.Size = new System.Drawing.Size(122, 17);
            this.Scharr5x5x4RadioButton.TabIndex = 12;
            this.Scharr5x5x4RadioButton.TabStop = true;
            this.Scharr5x5x4RadioButton.Text = "Scharr 5x5 4xRotate";
            this.Scharr5x5x4RadioButton.UseVisualStyleBackColor = true;
            this.Scharr5x5x4RadioButton.CheckedChanged += new System.EventHandler(this.Scharr5x5x4RadioButton_CheckedChanged);
            // 
            // Isotropic3x3x4RadioButton
            // 
            this.Isotropic3x3x4RadioButton.AutoSize = true;
            this.Isotropic3x3x4RadioButton.Location = new System.Drawing.Point(10, 316);
            this.Isotropic3x3x4RadioButton.Margin = new System.Windows.Forms.Padding(2);
            this.Isotropic3x3x4RadioButton.Name = "Isotropic3x3x4RadioButton";
            this.Isotropic3x3x4RadioButton.Size = new System.Drawing.Size(131, 17);
            this.Isotropic3x3x4RadioButton.TabIndex = 13;
            this.Isotropic3x3x4RadioButton.TabStop = true;
            this.Isotropic3x3x4RadioButton.Text = "Isotropic 3x3 4xRotate";
            this.Isotropic3x3x4RadioButton.UseVisualStyleBackColor = true;
            this.Isotropic3x3x4RadioButton.CheckedChanged += new System.EventHandler(this.Isotropic3x3x4RadioButton_CheckedChanged);
            // 
            // Isotropic3x3x8RadioButton
            // 
            this.Isotropic3x3x8RadioButton.AutoSize = true;
            this.Isotropic3x3x8RadioButton.Location = new System.Drawing.Point(10, 339);
            this.Isotropic3x3x8RadioButton.Margin = new System.Windows.Forms.Padding(2);
            this.Isotropic3x3x8RadioButton.Name = "Isotropic3x3x8RadioButton";
            this.Isotropic3x3x8RadioButton.Size = new System.Drawing.Size(131, 17);
            this.Isotropic3x3x8RadioButton.TabIndex = 14;
            this.Isotropic3x3x8RadioButton.TabStop = true;
            this.Isotropic3x3x8RadioButton.Text = "Isotropic 3x3 8xRotate";
            this.Isotropic3x3x8RadioButton.UseVisualStyleBackColor = true;
            this.Isotropic3x3x8RadioButton.CheckedChanged += new System.EventHandler(this.Isotropic3x3x8RadioButton_CheckedChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1430, 858);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Image Processor Pro";
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PreviewPictureBox)).EndInit();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.TestingGroupBox.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button ExitButton;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button DisplayAsRawRGBButton;
        private System.Windows.Forms.PictureBox PreviewPictureBox;
        private System.Windows.Forms.Button RenameFilesButton;
        private System.Windows.Forms.TextBox TrainFacePrefixTextBox;
        private System.Windows.Forms.Button PreviewOperatorButton;
        private System.Windows.Forms.Button ProcessBothTrain;
        private System.Windows.Forms.Button ProcessTrainFacesButton;
        private System.Windows.Forms.Button ProcessAllButton;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton Kirsch3x3x1RadioButton;
        private System.Windows.Forms.RadioButton Sobel3x3x1RadioButton;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox TestingGroupBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox TestNotFacePrefixTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox TestFacePrefixTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox TrainNotFacePrefixTextBox;
        private System.Windows.Forms.Button ProcessTestNotFacesButton;
        private System.Windows.Forms.Button ProcessTestFaceButton;
        private System.Windows.Forms.Button ProcessTrainNotFacesButton;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Button ProcessBothTestButton;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.RadioButton Prewitt3x3x4RadioButton;
        private System.Windows.Forms.RadioButton Isotropic3x3x8RadioButton;
        private System.Windows.Forms.RadioButton Isotropic3x3x4RadioButton;
        private System.Windows.Forms.RadioButton Scharr5x5x4RadioButton;
        private System.Windows.Forms.RadioButton Scharr3x3x8RadioButton;
        private System.Windows.Forms.RadioButton Scharr3x3x4RadioButton;
        private System.Windows.Forms.RadioButton Sobel5x5x4RadioButton;
        private System.Windows.Forms.RadioButton Sobel3x3x8RadioButton;
        private System.Windows.Forms.RadioButton Sobel3x3x4RadioButton;
        private System.Windows.Forms.RadioButton Kirsch3x3x8RadioButton;
        private System.Windows.Forms.RadioButton Kirsch3x3x4RadioButton;
        private System.Windows.Forms.RadioButton Prewitt5x5x4RadioButton;
        private System.Windows.Forms.RadioButton Prewitt3x3x8RadioButton;
    }
}

