using System;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;
using ImageProcessor;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.Generic;
using System.Linq;

namespace ImageProcessor {
    public partial class Form1 : Form {

        #region Singleton pattern
        private static Form1 instance;

        public static Form1 Instance {
            get {
                return instance;
            }
            set { instance = value; }
        }
        #endregion

        public enum ImageSetType {
            [Description("Train face image set")]
            TrainFace,
            [Description("Train not face image set")]
            TrainNotFace,
            [Description("Test face image set")]
            TestFace,
            [Description("Test not face image set")]
            TestNotFace,
            [Description("Labeled Faces in the Wild set")]
            LfwFace
        }

        public enum ImageType {
            NotFace,
            Face
        }

        #region Constants
        private const int trainFaceFileCount = 2429;
        private const int trainNotFaceFileCount = 4548;
        private const int testFaceFileCount = 472;
        private const int testNotFaceFileCount = 23573;
        private const int lfwFaceCount = 13223;
        private const int totalFileCount = 31020;
        #endregion

        #region Member variables
        private string dirTestFaceBmp = @"C:\Temp\IPP\FaceData\TestFaceBmp\";
        private string dirTestNotFaceBmp = @"C:\Temp\IPP\FaceData\TestNotFaceBmp\";
        private string dirTrainFaceBmp = @"C:\Temp\IPP\FaceData\TrainFaceBmp\";
        private string dirTrainNotFaceBmp = @"C:\Temp\IPP\FaceData\TrainNotFaceBmp\";
        private string dirLfwFaceBmp = @"C:\Temp\IPP\FaceData\LfwFacesBmp\";
        private string dirNew = @"C:\Temp\IPP\FaceData\Both\";

        //technically a filter since it's 2 or more operators
        int widthHeightSize = 7;
        bool verboseOutput = false;
        double[,,] selectedOperator;
        private long startTimeMs = 0;
        private int filesProcessed = 0;
        private bool showLivePreview = true;
        private int totalFilesToProcess = 0;
        private List<CancellationTokenSource> cancelTokenSources = new List<CancellationTokenSource>();

        #endregion

        public Form1() {
            InitializeComponent();
            Form1.Instance = this;
            //load the combobox
            OperatorComboBox.DataSource = Enum.GetValues(typeof(OperatorNames));
            //OperatorComboBox.SelectedValue = (int)OperatorNames.Sobel3x3x1;
        }

        #region Debug Button Clicks
        private void DisplayAsRawRGBButton_Click(object sender, EventArgs e) {
            var bmp = new Bitmap(dirTrainFaceBmp + "face00001.bmp", false);
            PreviewPictureBox.Image = bmp;
            outputTextBox.Clear();
            SetVerboseOutput(true);
            string s;

            for (int y = 0; y < 19; y++) {
                for (int x = 0; x < 19; x++) {
                    Color c = bmp.GetPixel(x, y);
                    s = "R" + c.R.ToString() + " " + "G" + c.G.ToString() + " " + "B" + c.B.ToString() + " ";
                    outputTextBox.AppendText(s);
                }
                outputTextBox.AppendText("\r\n");
            }
            SetVerboseOutput(false);
        }

        private void PreviewOperatorButton_Click(object sender, EventArgs e) {
            var filename = dirTrainFaceBmp + "face00001.bmp";
            outputTextBox.Clear();
            PreviewPictureBox.Image = new Bitmap(dirTrainFaceBmp + "face00001.bmp", false);
            SetVerboseOutput(true);
            ProcessImage(filename, 1);
            SetVerboseOutput(false);
        }

        private void VerboseLoggingChkBx_CheckedChanged(object sender, EventArgs e) {
            verboseOutput = ((CheckBox)sender).Checked;
        }

        private void SetVerboseOutput(bool v) {
            VerboseLoggingChkBx.Checked = v;
            verboseOutput = v;
        }

        #endregion

        #region Main Menu

        private void RestoreDefaultsBtn_Click(object sender, EventArgs e) {
            // default rename file prefixes
            TrainFaceRenameTxtBx.Text = "cmu_";
            TrainNotFaceRenameTxtBx.Text = "cmu_";
            TestFaceRenameTxtBx.Text = "cmu_";
            TestNotFaceRenameTxtBx.Text = "cmu_";

            // default rename source dir
            TrainFaceSrcDir.Text = "";
            TrainNotFaceSrcDir.Text = "";
            TestFaceSrcDir.Text = "";
            TestNotFaceSrcDir.Text = "";

            // default rename dest dir
            TrainFaceDestDir.Text = "";
            TrainNotFaceDestDir.Text = "";
            TestFaceDestDir.Text = "";
            TestNotFaceDestDir.Text = "";
        }

        private void StopButton_Click(object sender, EventArgs e) {
            foreach (CancellationTokenSource token in cancelTokenSources)
                if (!token.IsCancellationRequested) {
                    CurrentStatusLabel.Text = "Stopping called!";
                    token.Cancel();
                }
        }

        private void ExitButton_Click_1(object sender, EventArgs e) {
            Close();
        }

        #endregion

        #region Image Set Processing Actions

        private void ProcessTrainFacesButton_Click(object sender, EventArgs e) {
            if (!TrainFacePrefixTextBox.IsEmpty("Train face prefix,")) {
                ResetProgress(trainFaceFileCount);
                var prefix = TrainFacePrefixTextBox.Text;
                ProcessDirAsync(dirTrainFaceBmp, "trainDataFace.csv", ImageType.Face, true, prefix, "D5", 1, trainFaceFileCount);
            }
        }

        private void ProcessTrainNotFacesButton_Click(object sender, EventArgs e) {
            if (!TrainNotFacePrefixTextBox.IsEmpty("Train not face prefix,")) {
                ResetProgress(trainNotFaceFileCount);
                var prefix = TrainNotFacePrefixTextBox.Text;
                ProcessDirAsync(dirTrainNotFaceBmp, "trainDataNotFace.csv", ImageType.NotFace, true, prefix, "D5", 1, trainNotFaceFileCount);
            }
        }

        private async void ProcessTrainFaceBoth_Click(object sender, EventArgs e) {
            if (!TrainNotFacePrefixTextBox.IsEmpty("Train not face prefix,") || !TrainFacePrefixTextBox.IsEmpty("Train face prefix,")) {
                ResetProgress(trainFaceFileCount + trainNotFaceFileCount);
                var prefix = TrainFacePrefixTextBox.Text;
                var prefix2 = TrainNotFacePrefixTextBox.Text;

                Form1.Instance.ClearOutput();
                Form1.Instance.SetCurrentStatus("Processing...");

                var cancelTokenSource = new CancellationTokenSource();
                cancelTokenSources.Add(cancelTokenSource);
                var token = cancelTokenSource.Token;

                Directory.CreateDirectory(dirNew);
                string outputFile = dirNew + "trainDataBoth.csv";
                using (File.Create(outputFile)) { };

                try {
                    Task taskTrainFace = Task.Factory.StartNew(() => Work(dirTrainFaceBmp, outputFile, ImageType.Face, true, prefix, "D5", 1, trainFaceFileCount), token);
                    await Task.WhenAll(taskTrainFace);
                    Task taskTrainNotFace = Task.Factory.StartNew(() => Work(dirTrainNotFaceBmp, outputFile, ImageType.NotFace, true, prefix2, "D5", 0, trainNotFaceFileCount - 1), token);
                    await Task.WhenAll(taskTrainNotFace);
                    Form1.Instance.SetCurrentStatus("Finished!");
                } catch (OperationCanceledException) {
                    Form1.Instance.SetCurrentStatus("Stopped!");
                } finally {
                    cancelTokenSources.Remove(cancelTokenSource);
                }
            }
        }

        private void ProcessTestFaceButton_Click(object sender, EventArgs e) {
            if (!TestFacePrefixTextBox.IsEmpty("Test face prefix,")) {
                ResetProgress(testFaceFileCount);
                var prefix = TestFacePrefixTextBox.Text;
                ProcessDirAsync(dirTestFaceBmp, "testDataFace.csv", ImageType.Face, true, prefix, "D4", 0, testFaceFileCount - 1);
            }
        }

        private void ProcessTestNotFacesButton_Click(object sender, EventArgs e) {
            if (!TestNotFacePrefixTextBox.IsEmpty("Test not face prefix,")) {
                ResetProgress(testNotFaceFileCount);
                var prefix = TestNotFacePrefixTextBox.Text;
                ProcessDirAsync(dirTestNotFaceBmp, "testDataNotFace.csv", ImageType.NotFace, true, prefix, "D6", 0, testNotFaceFileCount - 1);
            }
        }

        private async void ProcessBothTestButton_Click(object sender, EventArgs e) {
            if (!TestNotFacePrefixTextBox.IsEmpty("Test not face prefix,") || !TestFacePrefixTextBox.IsEmpty("Test face prefix,")) {
                ResetProgress(testFaceFileCount + testNotFaceFileCount);
                var prefix = TestFacePrefixTextBox.Text;
                var prefix2 = TestNotFacePrefixTextBox.Text;

                Form1.Instance.ClearOutput();
                Form1.Instance.SetCurrentStatus("Processing...");

                var cancelTokenSource = new CancellationTokenSource();
                cancelTokenSources.Add(cancelTokenSource);
                var token = cancelTokenSource.Token;

                Directory.CreateDirectory(dirNew);
                string outputFile = dirNew + "testDataBoth.csv";
                using (File.Create(outputFile)) { };

                try {
                    Task taskTestFace = Task.Factory.StartNew(() => Work(dirTestFaceBmp, outputFile, ImageType.Face, true, prefix, "D4", 0, testFaceFileCount - 1), token);
                    await Task.WhenAll(taskTestFace);
                    Task taskTestNotFace = Task.Factory.StartNew(() => Work(dirTestNotFaceBmp, outputFile, ImageType.NotFace, true, prefix2, "D6", 0, testNotFaceFileCount - 1), token);
                    await Task.WhenAll(taskTestNotFace);
                    Form1.Instance.SetCurrentStatus("Finished!");
                } catch (OperationCanceledException) {
                    Form1.Instance.SetCurrentStatus("Stopped!");
                } finally {
                    cancelTokenSources.Remove(cancelTokenSource);
                }
            }
        }


        private void LfwProcessFaces_Click(object sender, EventArgs e) {
            Form1.Instance.ClearOutput();
            ResetProgress(lfwFaceCount);
            Form1.Instance.SetCurrentStatus("Processing...");
            Directory.CreateDirectory(dirNew);
            string outputFile = "lfwFaceData.csv";
            ProcessDirAsync(dirLfwFaceBmp, outputFile, ImageType.Face);
        }

        private void ResetProgress(int numOfFiles) {
            startTimeMs = TimeUtils.GetCurrentTimeMillisSinceEpoch();
            ImgProgressBar.Value = 0;
            filesProcessed = 0;
            PercentageComplete.Text = "0%";
            totalFilesToProcess = numOfFiles;
        }
        #endregion

        #region Bulk Image Manipulation
        /// <summary>
        /// Asynchronously processes images in a directory using the selected mathematical operator. An output
        /// of 49 int parameters followed by it's classification is appended to the given filename.
        /// </summary>
        /// <param name="inputDirectory"></param>
        /// <param name="prefix">"face"</param>
        /// <param name="formatS">"D5"</param>
        /// <param name="lowVal">1</param>
        /// <param name="hiVal">2429</param>
        /// <param name="outputFile">"trainDataFace.csv"</param>
        /// <param name="classification">0 or 1, i.e. face or not face</param>
        /// <param name="show">verbosity</param>
        private async void ProcessDirAsync(string inputDirectory, string outputFile, ImageType classification, bool hasPrefixes = false, string prefix = null, string formatS = null, int lowVal = 0, int hiVal = 0) {
            Form1.Instance.ClearOutput();
            Form1.Instance.SetCurrentStatus("Processing...");

            var cancelTokenSource = new CancellationTokenSource();
            cancelTokenSources.Add(cancelTokenSource);
            var token = cancelTokenSource.Token;

            string outName = inputDirectory + outputFile;
            using (File.Create(outName)) { };

            try {
                await Task.Factory.StartNew(() => Work(inputDirectory, outName, classification, hasPrefixes, prefix, formatS, lowVal, hiVal), token);
                Form1.Instance.SetCurrentStatus("Finished!");
            } catch (OperationCanceledException) {
                Form1.Instance.SetCurrentStatus("Stopped!");
            } finally {
                cancelTokenSources.Remove(cancelTokenSource);
            }
        }

        private void Work(string inputDirectory, string outName, ImageType classification, bool hasPrefixes = false, string prefix = null, string formatS = null, int lowVal = 0, int hiVal = 0) {
            {
                if (!File.Exists(outName))
                    using (File.Create(outName)) { };

                using (StreamWriter file = File.AppendText(outName)) {
                    if (hasPrefixes) { // honour the defined naming prefixing and image indexing ranges
                        for (int i = lowVal; i <= hiVal; i++) {
                            string fname;

                            // handbrake
                            cancelTokenSources.ForEach(ts => ts.Token.ThrowIfCancellationRequested());

                            if (formatS == "D6" && i <= 9999) {
                                fname = inputDirectory + prefix + i.ToString("D4") + ".bmp";
                            } else if (formatS == "D6" && i > 9999) {
                                fname = inputDirectory + prefix + i.ToString("D5") + ".bmp";
                            } else
                                fname = inputDirectory + prefix + i.ToString(formatS) + ".bmp";

                            SingleJob(file, fname, classification, outName);
                        }
                    } else { // just iterate through all files in the given directory
                        string[] filePaths = Directory.GetFiles(inputDirectory);

                        for (int i = 0; i < filePaths.Length; i++) {
                            // handbrake
                            cancelTokenSources.ForEach(ts => ts.Token.ThrowIfCancellationRequested());
                            string ff = filePaths[i];
                            string fname = Path.GetFileName(ff);
                            SingleJob(file, ff, classification, outName);
                        }
                    }
                }
            }
        }

        private void SingleJob(StreamWriter file, string fname, ImageType classification, string outName) {
            if (!fname.ToUpper().EndsWith(".BMP")) {
                Form1.Instance.AppendVerboseText("Skipped non-bmp files, filename=" + fname);
                return;
            }

            if (!File.Exists(fname)) {
                Form1.Instance.AppendText("WARN - File not found, skipping filename=" + fname + "\r\n");
            } else {
                string val = ProcessImage(fname, (int)classification);
                if (val == "") {
                    Form1.Instance.AppendText("ERROR - A problem occurred processing " + fname + ", aborting." + "\r\n");
                    cancelTokenSources.ForEach(ts => ts.Token.ThrowIfCancellationRequested());
                }

                Form1.Instance.SetCurrentStatus("Processing > " + fname + " into " + outName + "\r\n");
                //if (i < 10) {
                //    Form1.Instance.AppendText(val + "\r\n");
                //}
                //if (i == 10) break; // debug exit

                file.WriteLine(val);

                // update progress bar
                Form1.Instance.ReportFileProcessed();
            }
        }

        public string ShowBitmap(Bitmap bmp) {
            string s = "";

            for (int y = 0; y < 19; y++) {
                for (int x = 0; x < 19; x++) {
                    Color c = bmp.GetPixel(x, y);
                    string ss = c.R.ToString() + " ";
                    s = s + ss;
                }
                s = s + "\r\n";
            }
            return s;
        }

        public string ProcessImage(string filename, int classification) {
            var bmp = new Bitmap(filename, false);

            if (showLivePreview) {
                Form1.Instance.SetSobelImage(bmp.ConvolutionFilter(widthHeightSize, selectedOperator));
                Form1.Instance.SetPreviewImage(new Bitmap(filename));
            }
            Form1.Instance.AppendVerboseText("\r\n  .#.....................................#.  \r\n");
            Form1.Instance.AppendVerboseText("Fname = " + filename + "\r\n");
            string bb = ShowBitmap(bmp);
            Form1.Instance.AppendVerboseText(bb);
            Form1.Instance.AppendVerboseText("  .-.....................................-.  \r\n");

            // convert the bitmap using the given compass operator
            var retv = Convolution.ConvolutionFilterAsString(bmp, widthHeightSize, selectedOperator);
            // build a line of all Red values

            retv += classification.ToString();
            Form1.Instance.AppendVerboseText(retv);
            return retv;
        }

        /// <summary>
        /// Used to allow outputBox text updates from async threads.
        /// </summary>
        /// <param name="s"></param>
        public void AppendText(string s) {
            if (this.InvokeRequired) {// delegate the UI instance update the output
                this.Invoke(new MethodInvoker(() => this.AppendText(s)));
            } else {// UI-only call to update the output instance
                outputTextBox.AppendText(s);
            }
        }

        /// <summary>
        /// Used to allow outputBox text updates from async threads.
        /// </summary>
        /// <param name="s"></param>
        public void AppendVerboseText(string s) {
            if (verboseOutput) {
                if (this.InvokeRequired) {// delegate the UI instance update the output
                    this.Invoke(new MethodInvoker(() => this.AppendText(s)));
                } else {// UI-only call to update the output instance
                    outputTextBox.AppendText(s);
                }
            }
        }

        /// <summary>
        /// Used to allow current status text updates from async threads.
        /// </summary>
        /// <param name="s"></param>
        public void SetCurrentStatus(string s) {
            if (this.InvokeRequired) {// delegate the UI instance update the current status text
                this.Invoke(new MethodInvoker(() => this.SetCurrentStatus(s)));
            } else {// UI-only call to update the current status text
                CurrentStatusLabel.Text = s;
            }
        }

        /// <summary>
        /// Used to allow outputBox text to be cleared from async threads.
        /// </summary>
        /// <param name="s"></param>
        public void ClearOutput() {
            if (this.InvokeRequired) {// delegate the UI instance clear the output
                this.Invoke(new MethodInvoker(() => this.ClearOutput()));
            } else {// UI-only call to update the output instance
                outputTextBox.Clear();
            }
        }

        /// <summary>
        /// Used to set the sobel image from async threads.
        /// </summary>
        /// <param name="s"></param>
        public void SetSobelImage(Bitmap bmp) {
            if (this.InvokeRequired) {// delegate the UI instance set the sobel image
                this.Invoke(new MethodInvoker(() => this.SetSobelImage(bmp)));
            } else {// UI-only call to set the sobel image
                EdgedPictureBox.Image = bmp;
            }
        }

        /// <summary>
        /// Used to set the preview image from async threads.
        /// </summary>
        /// <param name="s"></param>
        public void SetPreviewImage(Bitmap bmp) {
            if (this.InvokeRequired) {// delegate the UI instance set the preview image
                this.Invoke(new MethodInvoker(() => this.SetPreviewImage(bmp)));
            } else {// UI-only call to set the preview image
                PreviewPictureBox.Image = bmp;
            }
        }

        /// <summary>
        /// Used to update the progress bar and completed percentage from async threads.
        /// </summary>
        /// <param name="s"></param>
        public void ReportFileProcessed() {
            if (this.InvokeRequired) {// let the UI instance update the progress bar and completed percentage
                this.Invoke(new MethodInvoker(() => this.ReportFileProcessed()));
            } else {// UI-only call to set the preview image
                filesProcessed++;
                ImgProgressBar.Value = filesProcessed * 100 / totalFilesToProcess;
                PercentageComplete.Text = ImgProgressBar.Value + "%";
                fileCountProgress.Text = "Processed files (" + filesProcessed + "/" + totalFilesToProcess + ")";

                var durationMillis = TimeUtils.GetTimeElapsedMillisSince(startTimeMs);
                JobDurationLabel.Text = TimeUtils.GetFormattedDurationFromMillis(durationMillis);

            }
        }
        #endregion

        #region Operator Radio Button Clicks

        private void Prewitt3x3x1RadioButton_CheckedChanged(object sender, EventArgs e) {
            selectedOperator = Operators.Prewitt3x3x1;
        }

        private void Prewitt3x3x4RadioButton_CheckedChanged(object sender, EventArgs e) {
            selectedOperator = Operators.Prewitt3x3x4;
        }

        private void Prewitt3x3x8RadioButton_CheckedChanged(object sender, EventArgs e) {
            selectedOperator = Operators.Prewitt3x3x8;
        }

        private void Kirsch3x3x1RadioButton_CheckedChanged(object sender, EventArgs e) {
            selectedOperator = Operators.Kirsch3x3x1;
        }

        private void Kirsch3x3x4RadioButton_CheckedChanged(object sender, EventArgs e) {
            selectedOperator = Operators.Kirsch3x3x4;
        }

        private void Kirsch3x3x8RadioButton_CheckedChanged(object sender, EventArgs e) {
            selectedOperator = Operators.Kirsch3x3x8;
        }

        private void Sobel3x3x1RadioButton_CheckedChanged(object sender, EventArgs e) {
            selectedOperator = Operators.Sobel3x3x1;
        }

        private void Sobel3x3x4RadioButton_CheckedChanged(object sender, EventArgs e) {
            selectedOperator = Operators.Sobel3x3x4;
        }

        private void Sobel3x3x8RadioButton_CheckedChanged(object sender, EventArgs e) {
            selectedOperator = Operators.Sobel3x3x8;
        }

        private void Scharr3x3x1RadioButton_CheckedChanged(object sender, EventArgs e) {
            selectedOperator = Operators.Scharr3x3x1;
        }

        private void Scharr3x3x4RadioButton_CheckedChanged(object sender, EventArgs e) {
            selectedOperator = Operators.Scharr3x3x4;
        }

        private void Scharr3x3x8RadioButton_CheckedChanged(object sender, EventArgs e) {
            selectedOperator = Operators.Scharr3x3x8;
        }

        private void Isotropic3x3x1RadioButton_CheckedChanged(object sender, EventArgs e) {
            selectedOperator = Operators.Isotropic3x3x1;
        }

        private void Isotropic3x3x4RadioButton_CheckedChanged(object sender, EventArgs e) {
            selectedOperator = Operators.Isotropic3x3x4;
        }

        private void Isotropic3x3x8RadioButton_CheckedChanged(object sender, EventArgs e) {
            selectedOperator = Operators.Isotropic3x3x8;
        }

        #endregion

        #region Rename File Buttons
        private async void RenameTrainFaceBtn_Click(object sender, EventArgs e) {
            var cancelTokenSource = new CancellationTokenSource();
            cancelTokenSources.Add(cancelTokenSource);
            var token = cancelTokenSource.Token;
            try {
                cancelTokenSources.Add(cancelTokenSource);
                Form1.Instance.AppendText("Renaming train face image files");
                ResetProgress(trainFaceFileCount);
                await Task.Factory.StartNew(() => RenameFiles(TrainFaceSrcDir.Text, TrainFaceDestDir.Text, TrainFaceRenameTxtBx.Text, "Train face file prefix"),
                    token);
            } catch (OperationCanceledException) {
                Form1.Instance.SetCurrentStatus("Stopped!");
            } finally {
                cancelTokenSources.Remove(cancelTokenSource);
            }
        }

        private async void RenameTrainNotFaceBtn_Click(object sender, EventArgs e) {
            var cancelTokenSource = new CancellationTokenSource();
            cancelTokenSources.Add(cancelTokenSource);
            var token = cancelTokenSource.Token;
            try {
                cancelTokenSources.Add(cancelTokenSource);
                ResetProgress(trainNotFaceFileCount);
                await Task.Factory.StartNew(() => RenameFiles(TrainNotFaceSrcDir.Text, TrainNotFaceDestDir.Text, TrainNotFaceRenameTxtBx.Text, "Train not face file prefix"),
                    token);
            } catch (OperationCanceledException) {
                Form1.Instance.SetCurrentStatus("Stopped!");
            } finally {
                cancelTokenSources.Remove(cancelTokenSource);
            }
        }

        private async void RenameTestFaceButton_Click(object sender, EventArgs e) {
            var cancelTokenSource = new CancellationTokenSource();
            cancelTokenSources.Add(cancelTokenSource);
            var token = cancelTokenSource.Token;
            try {
                cancelTokenSources.Add(cancelTokenSource);
                ResetProgress(testFaceFileCount);
                await Task.Factory.StartNew(() => RenameFiles(TestFaceSrcDir.Text, TestFaceDestDir.Text, TestFaceRenameTxtBx.Text, "Test face file prefix"),
                    token);
            } catch (OperationCanceledException) {
                Form1.Instance.SetCurrentStatus("Stopped!");
            } finally {
                cancelTokenSources.Remove(cancelTokenSource);
            }
        }

        private async void RenameTestNotFaceBtn_Click(object sender, EventArgs e) {
            var cancelTokenSource = new CancellationTokenSource();
            cancelTokenSources.Add(cancelTokenSource);
            var token = cancelTokenSource.Token;
            try {
                cancelTokenSource = new CancellationTokenSource();

                ResetProgress(testNotFaceFileCount);
                await Task.Factory.StartNew(() => RenameFiles(TestNotFaceSrcDir.Text, TestNotFaceDestDir.Text, TestNotFaceRenameTxtBx.Text, "Test not face file prefix"),
                    token);
            } catch (OperationCanceledException) {
                Form1.Instance.SetCurrentStatus("Stopped!");
            } finally {
                cancelTokenSources.Remove(cancelTokenSource);
            }
        }

        private void RenameFiles(string dirFrom, string dirTo, string prefix, string imgSetName) {
            if (prefix.IsEmpty(imgSetName)) // shows a waning box
                return; // don't rename
            if (!dirFrom.DirExists() || !dirTo.DirExists()) // shows a create directory question msg box
                return; // don't rename

            string[] filePaths = Directory.GetFiles(dirFrom);

            Form1.Instance.ClearOutput();
            for (int i = 0; i < filePaths.Length; i++) {
                // handbrake
                cancelTokenSources.ForEach(ts => ts.Token.ThrowIfCancellationRequested());

                string ff = filePaths[i];
                string f = Path.GetFileName(ff);

                string fromFile = dirFrom + f;
                if (fromFile.ToUpper().EndsWith(".BMP")) {
                    string toFile = dirTo + prefix + i.ToString("D5") + ".bmp";
                    // if (i > 10) break; // use for testing
                    CopyFile(fromFile, toFile);
                    Form1.Instance.ReportFileProcessed();
                } else
                    Form1.Instance.AppendVerboseText("Skipped non-bmp files, filename=" + fromFile);

            }
            Form1.Instance.SetCurrentStatus("Finished!");
        }

        private void CopyFile(string fromFile, string toFile) {
            Form1.Instance.SetCurrentStatus("Copying file from >>" + fromFile + " to   >>" + toFile + "\r\n\r\n");
            File.Copy(fromFile, toFile, true);
        }
        #endregion

        #region Source & Destination Button Clicks
        public void ChooseFolder(TextBox textBox) {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK) {
                textBox.Text = folderBrowserDialog1.SelectedPath;
            }
        }
        private void SrcTrainFaceBtn_Click(object sender, EventArgs e) {
            ChooseFolder(TrainFaceSrcDir);
        }

        private void SrcTestNotaceBtn_Click(object sender, EventArgs e) {
            ChooseFolder(TrainNotFaceSrcDir);
        }

        private void SrcTestFaceBtn_Click(object sender, EventArgs e) {
            ChooseFolder(TrainFaceSrcDir);
        }

        private void SrcTestNotFaceBtn_Click(object sender, EventArgs e) {
            ChooseFolder(TestFaceSrcDir);
        }

        private void DestTrainFaceBtn_Click(object sender, EventArgs e) {
            ChooseFolder(TrainFaceDestDir);
        }

        private void DestTrainNotFaceBtn_Click(object sender, EventArgs e) {
            ChooseFolder(TrainNotFaceDestDir);
        }

        private void DestTestFaceBtn_Click(object sender, EventArgs e) {
            ChooseFolder(TrainFaceSrcDir);
        }

        private void DestTestNotFaceBtn_Click(object sender, EventArgs e) {
            ChooseFolder(TestNotFaceDestDir);
        }
        #endregion

        #region Not finished
        // TODO - 
        //private async void RenameAllImagesBtn_Click(object sender, EventArgs e) {
        //    var cancelTokenSource = new CancellationTokenSource();
        //    cancelTokenSources.Add(cancelTokenSource);
        //    var token = cancelTokenSource.Token;
        //    try {
        //        cancelTokenSources.Add(cancelTokenSource);
        //        ResetProgress(totalFileCount);
        //        await Task.Factory.StartNew(() => {
        //            // Stop! check all prefixes and paths are present before renaming anything

        //            if (TrainFaceRenameTxtBx.IsEmpty("Renaming train face prefix,")) return;
        //            if (TrainNotFaceRenameTxtBx.IsEmpty("Renaming train not face prefix,")) return;
        //            if (TestFaceRenameTxtBx.IsEmpty("Renaming test face prefix,")) return;
        //            if (TestNotFaceRenameTxtBx.IsEmpty("Renaming test not face prefix,")) return;

        //            // default rename source dir
        //            if (TrainFaceSrcDir.IsEmpty("Renaming train face source directory path,")) return;
        //            if (TrainNotFaceSrcDir.IsEmpty("Renaming train not face source directory path,")) return;
        //            if (TestFaceSrcDir.IsEmpty("Renaming test face source directory path,")) return;
        //            if (TestNotFaceSrcDir.IsEmpty("Renaming test not face source directory path,")) return;

        //            // default rename dest dir
        //            if (TrainFaceDestDir.IsEmpty("Renaming train face destination directory path,")) return;
        //            if (TrainNotFaceDestDir.IsEmpty("Renaming train not face destination directory path,")) return;
        //            if (TestFaceDestDir.IsEmpty("Renaming test face destination directory path,")) return;
        //            if (TestNotFaceDestDir.IsEmpty("Renaming test not face destination directory path,")) return;

        //            RenameFiles(TrainFaceSrcDir.Text, TrainFaceDestDir.Text, TrainFaceRenameTxtBx.Text, "Train face file prefix");
        //            RenameFiles(TrainNotFaceRenameTxtBx.Text, TrainNotFaceSrcDir.Text, TrainNotFaceDestDir.Text, "Train not face file prefix");
        //            RenameFiles(TestFaceRenameTxtBx.Text, TestFaceSrcDir.Text, TestFaceDestDir.Text, "Test not face file prefix");
        //            RenameFiles(TestNotFaceRenameTxtBx.Text, TestNotFaceSrcDir.Text, TestNotFaceDestDir.Text, "Test face file prefix");
        //        }, token);
        //    } catch (OperationCanceledException) {
        //        Form1.Instance.SetCurrentStatus("Stopped!");
        //    } finally {
        //        cancelTokenSources.Remove(cancelTokenSource);
        //    }
        //}
        #endregion

        private void livePreviewCheckBox_CheckedChanged(object sender, EventArgs e) {
            showLivePreview = ((CheckBox)sender).Checked;
        }

        private void TrainStartIndexNumeric_ValueChanged(object sender, EventArgs e) {

        }

        private void TrainEndIndexNumeric_ValueChanged(object sender, EventArgs e) {

        }

        private void TestStartIndexNumeric_ValueChanged(object sender, EventArgs e) {

        }

        private void TestEndIndexNumeric_ValueChanged(object sender, EventArgs e) {

        }

        private void comboBox1_SelectedValueChanged(object sender, EventArgs e) {
            OperatorNames selection;
            Enum.TryParse<OperatorNames>(((ComboBox)sender).SelectedValue.ToString(), out selection);
            switch (selection) {
                case OperatorNames.Prewitt3x3x1: selectedOperator = Operators.Prewitt3x3x1; break;
                case OperatorNames.Prewitt3x3x4: selectedOperator = Operators.Prewitt3x3x4; break;
                case OperatorNames.Prewitt3x3x8: selectedOperator = Operators.Prewitt3x3x8; break;
                case OperatorNames.Kirsch3x3x1: selectedOperator = Operators.Kirsch3x3x1; break;
                case OperatorNames.Kirsch3x3x4: selectedOperator = Operators.Kirsch3x3x4; break;
                case OperatorNames.Kirsch3x3x8: selectedOperator = Operators.Kirsch3x3x8; break;
                case OperatorNames.Sobel3x3x1: selectedOperator = Operators.Sobel3x3x1; break;
                case OperatorNames.Sobel3x3x4: selectedOperator = Operators.Sobel3x3x4; break;
                case OperatorNames.Sobel3x3x8: selectedOperator = Operators.Sobel3x3x8; break;
                case OperatorNames.Scharr3x3x1: selectedOperator = Operators.Scharr3x3x1; break;
                case OperatorNames.Scharr3x3x4: selectedOperator = Operators.Scharr3x3x4; break;
                case OperatorNames.Scharr3x3x8: selectedOperator = Operators.Scharr3x3x8; break;
                case OperatorNames.Isotropic3x3x1: selectedOperator = Operators.Isotropic3x3x1; break;
                case OperatorNames.Isotropic3x3x4: selectedOperator = Operators.Isotropic3x3x4; break;
                case OperatorNames.Isotropic3x3x8: selectedOperator = Operators.Isotropic3x3x8; break;
            }
        }
    }
}
