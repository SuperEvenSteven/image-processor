using System;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;
using ImageProcessor;
using System.Threading.Tasks;
using System.Threading;

namespace ImageProcessor {
    public partial class Form1 : Form {

        public enum ImageSetType {
            [Description("Train face image set")]
            TrainFace,
            [Description("Train not face image set")]
            TrainNotFace,
            [Description("Test face image set")]
            TestFace,
            [Description("Test not face image set")]
            TestNotFace
        }

        #region Member variables
        private string dirTestFaceBmp = @"C:\Temp\IPP\FaceData\TestFaceBmp\";
        private string dirTestNotFaceBmp = @"C:\Temp\IPP\FaceData\TestNotFaceBmp\";
        private string dirTrainFaceBmp = @"C:\Temp\IPP\FaceData\TrainFaceBmp\";
        private string dirTrainNotFaceBmp = @"C:\Temp\IPP\FaceData\TrainNotFaceBmp\";

        //technically a filter since it's 2 or more operators
        double[,,] selectedOperator;
        Bitmap bmp = null;
        private int filesProcessed = 0;
        private int totalFilesToProcess = 0;
        private CancellationTokenSource cancelTokenSource;
        private CancellationToken cancelToken;

        #endregion

        public Form1() {
            InitializeComponent();
        }

        #region Debug Button Clicks
        private void DisplayAsRawRGBButton_Click(object sender, EventArgs e) {
            bmp = new Bitmap(dirTrainFaceBmp + "face00001.bmp", false);
            PreviewPictureBox.Image = bmp;
            outputTextBox.Clear();

            string s;

            for (int y = 0; y < 19; y++) {
                for (int x = 0; x < 19; x++) {
                    Color c = bmp.GetPixel(x, y);
                    s = "R" + c.R.ToString() + " " + "G" + c.G.ToString() + " " + "B" + c.B.ToString() + " ";
                    outputTextBox.AppendText(s);
                }
                outputTextBox.AppendText("\r\n");
            }

        }

        private void PreviewOperatorButton_Click(object sender, EventArgs e) {
            bmp = new Bitmap(dirTrainFaceBmp + "face00001.bmp", false);
            var retv = "";
            outputTextBox.Clear();
            PreviewPictureBox.Image = bmp.ConvolutionFilter(selectedOperator, outputTextBox, true);

            string bb = ShowBitmap(bmp);
            outputTextBox.AppendText(bb);

            outputTextBox.AppendText("\r\n  .........................................  \r\n");
            var redBytes = Convolution.ConvolutionFilterAsString(bmp, selectedOperator, outputTextBox, true);
            for (int i = 0; i < redBytes.Length; i++) { retv += redBytes[i]; }
            outputTextBox.AppendText(retv + " 0");
            outputTextBox.AppendText("\r\n  .........................................  \r\n");
        }

        #endregion

        #region Main Menu

        private void ProcessAllImageSetsButton_Click(object sender, EventArgs e) {
            ResetProgress(31019);
            ProcessDirAsync(dirTrainFaceBmp, "face", "D5", 1, 2429, "trainDataFace.txt", 1, true);
            ProcessDirAsync(dirTrainNotFaceBmp, "cmu_", "D5", 0, 4547, "trainDataNotFace.txt", 0, true);
            ProcessDirAsync(dirTestFaceBmp, "cmu_", "D4", 0, 471, "testDataFace.txt", 1, true);
            ProcessDirAsync(dirTestNotFaceBmp, "cmu_", "D4", 0, 23572, "testDataNotFace.txt", 0, true);
        }
        private void RenameFilesButton_Click(object sender, EventArgs e) {

        }

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

        private void ExitButton_Click_1(object sender, EventArgs e) {
            Close();
        }

        #endregion

        #region Image Set Processing Actions

        private void ProcessTrainFacesButton_Click(object sender, EventArgs e) {
            if (!TrainFacePrefixTextBox.IsEmpty("Train face prefix,")) {
                ResetProgress(2429);
                ProcessDirAsync(dirTrainFaceBmp, "face", "D5", 1, 2429, "trainDataFace.txt", 1, false);
            }
        }

        private void ProcessTrainNotFacesButton_Click(object sender, EventArgs e) {
            if (!TrainNotFacePrefixTextBox.IsEmpty("Train not face prefix,")) {
                ResetProgress(4547);
                ProcessDirAsync(dirTrainNotFaceBmp, "face", "D5", 1, 4547, "trainDataNotFace.txt", 1, false);
            }
        }

        private void ProcessTrainFaceBoth_Click(object sender, EventArgs e) {
            if (!TrainNotFacePrefixTextBox.IsEmpty("Train not face prefix,") || !TrainFacePrefixTextBox.IsEmpty("Train face prefix,")) {
                ResetProgress(2429 + 4547);
                ProcessDirAsync(dirTrainFaceBmp, "face", "D5", 1, 2429, "trainData.txt", 1, false);
                ProcessDirAsync(dirTrainNotFaceBmp, "face", "D5", 1, 4547, "trainData.txt", 1, false);
            }
        }

        private void ProcessTestFaceButton_Click(object sender, EventArgs e) {
            if (!TestFacePrefixTextBox.IsEmpty("Test face prefix,")) {
                ResetProgress(471);
                ProcessDirAsync(dirTestFaceBmp, "face", "D5", 1, 471, "testDataFace.txt", 1, false);
            }
        }

        private void ProcessTestNotFacesButton_Click(object sender, EventArgs e) {
            if (!TestNotFacePrefixTextBox.IsEmpty("Test not face prefix,")) {
                ResetProgress(23572);
                ProcessDirAsync(dirTestNotFaceBmp, "face", "D5", 1, 23572, "testDataNotFace.txt", 1, false);
            }
        }

        private void ProcessBothTestButton_Click(object sender, EventArgs e) {
            if (!TestNotFacePrefixTextBox.IsEmpty("Test not face prefix,") || !TestFacePrefixTextBox.IsEmpty("Test face prefix,")) {
                ResetProgress(471 + 23572);
                ProcessDirAsync(dirTestFaceBmp, "face", "D5", 1, 471, "testData.txt", 1, false);
                ProcessDirAsync(dirTestNotFaceBmp, "face", "D5", 1, 23572, "testData.txt", 1, false);
            }
        }

        private void ResetProgress(int numOfFiles) {
            ImgProgressBar.Value = 0;
            filesProcessed = 0;
            PercentageComplete.Text = "0%";
            totalFilesToProcess = numOfFiles;
        }

        #endregion

        #region Bulk Image Manipulation
        /// <summary>
        /// Processes images in a directory using the selected mathematical operator. An output
        /// of 49 int parameters followed by it's classification is appended to the given filename.
        /// </summary>
        /// <param name="inputDirectory"></param>
        /// <param name="prefix">"face"</param>
        /// <param name="formatS">"D5"</param>
        /// <param name="lowVal">1</param>
        /// <param name="hiVal">2429</param>
        /// <param name="outputFile">"trainDataFace.txt"</param>
        /// <param name="classification">0 or 1, i.e. face or not face</param>
        /// <param name="show">verbosity</param>
        private async void ProcessDirAsync(string inputDirectory, string prefix, string formatS, int lowVal, int hiVal, string outputFile, int classification, bool show) {
            outputTextBox.Clear();

            cancelTokenSource = new CancellationTokenSource();
            cancelToken = cancelTokenSource.Token;

            // create a Progress updater on the UI thread but without blocking it
            var progress = new Progress<int>(epochsProcessed => {
                filesProcessed++;
                ImgProgressBar.Value = (filesProcessed / totalFilesToProcess) * 100;
                PercentageComplete.Text = ImgProgressBar.Value + "%";
            });

            string outName = inputDirectory + outputFile;

            try {
                await Task.Factory.StartNew(() => Work(inputDirectory, prefix, formatS, lowVal, hiVal, outName, classification, show, progress), cancelToken);
                outputTextBox.AppendText("UI thread be free");
            } catch (OperationCanceledException e) {
                outputTextBox.AppendText("Stopped!");
            }
        }

        private void Work(string inputDirectory, string prefix, string formatS, int lowVal, int hiVal, string outName, int classification, bool show, IProgress<int> progress) {
            {
                System.IO.StreamWriter file = new System.IO.StreamWriter(outName);
                for (int i = lowVal; i <= hiVal; i++) {
                    // handbrake
                    cancelToken.ThrowIfCancellationRequested();

                    string fname = inputDirectory + prefix + i.ToString(formatS) + ".bmp";
                    //outputTextBox.AppendText("Process> " + fname + " into " + outName + "\r\n");

                    string val = ProcessImage(fname, classification, false);

                    if (i < 10 && show) {
                        //outputTextBox.AppendText(val + "\r\n");
                    }
                    //if (i == 10) break; // debug exit

                    file.WriteLine(val);

                    // update progress bar
                    progress.Report(1);
                }
                file.Close();
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

        public string ProcessImage(string fileName, int classification, bool show) {
            string retv = "";

            bmp = new Bitmap(fileName, false);

            PreviewPictureBox.Image = bmp.ConvolutionFilter(selectedOperator, outputTextBox, show);

            if (show) {

                //outputTextBox.AppendText("\r\n  .#.....................................#.  \r\n");
                //outputTextBox.AppendText("Fname = " + fileName + "\r\n");
                string bb = ShowBitmap(bmp);
                //outputTextBox.AppendText(bb);
                //outputTextBox.AppendText("  .-.....................................-.  \r\n");
            }

            // convert the bitmap using the given compass operator
            var redBytes = Convolution.ConvolutionFilterAsBytes(bmp, selectedOperator, outputTextBox, show);
            // build a line of all Red values
            for (int i = 0; i < redBytes.Length; i++) { retv += redBytes[i]; }

            retv = retv + classification.ToString();
            //if (show) { outputTextBox.AppendText(retv); }
            return retv;

        }

        #endregion

        #region Operator Radio Button Clicks

        private void Prewitt3x3x1RadioButton_CheckedChanged(object sender, EventArgs e) {
            selectedOperator = Matrix.Prewitt3x3x1;
        }

        private void Prewitt3x3x4RadioButton_CheckedChanged(object sender, EventArgs e) {
            selectedOperator = Matrix.Prewitt3x3x4;
        }

        private void Prewitt3x3x8RadioButton_CheckedChanged(object sender, EventArgs e) {
            selectedOperator = Matrix.Prewitt3x3x8;
        }

        private void Kirsch3x3x1RadioButton_CheckedChanged(object sender, EventArgs e) {
            selectedOperator = Matrix.Kirsch3x3x1;
        }

        private void Kirsch3x3x4RadioButton_CheckedChanged(object sender, EventArgs e) {
            selectedOperator = Matrix.Kirsch3x3x4;
        }

        private void Kirsch3x3x8RadioButton_CheckedChanged(object sender, EventArgs e) {
            selectedOperator = Matrix.Kirsch3x3x8;
        }

        private void Sobel3x3x1RadioButton_CheckedChanged(object sender, EventArgs e) {
            selectedOperator = Matrix.Sobel3x3x1;
        }

        private void Sobel3x3x4RadioButton_CheckedChanged(object sender, EventArgs e) {
            selectedOperator = Matrix.Sobel3x3x4;
        }

        private void Sobel3x3x8RadioButton_CheckedChanged(object sender, EventArgs e) {
            selectedOperator = Matrix.Sobel3x3x8;
        }

        private void Scharr3x3x1RadioButton_CheckedChanged(object sender, EventArgs e) {
            selectedOperator = Matrix.Scharr3x3x1;
        }

        private void Scharr3x3x4RadioButton_CheckedChanged(object sender, EventArgs e) {
            selectedOperator = Matrix.Scharr3x3x4;
        }

        private void Scharr3x3x8RadioButton_CheckedChanged(object sender, EventArgs e) {
            selectedOperator = Matrix.Scharr3x3x8;
        }

        private void Isotropic3x3x1RadioButton_CheckedChanged(object sender, EventArgs e) {
            selectedOperator = Matrix.Isotropic3x3x1;
        }

        private void Isotropic3x3x4RadioButton_CheckedChanged(object sender, EventArgs e) {
            selectedOperator = Matrix.Isotropic3x3x4;
        }

        private void Isotropic3x3x8RadioButton_CheckedChanged(object sender, EventArgs e) {
            selectedOperator = Matrix.Isotropic3x3x8;
        }

        #endregion

        #region Rename File Buttons

        private void RenameTrainFaceBtn_Click(object sender, EventArgs e) {
            RenameFiles(TrainFaceSrcDir.Text, TrainFaceDestDir.Text, TrainFaceRenameTxtBx.Text, "Train face file prefix");
        }

        private void RenameTrainNotFaceBtn_Click(object sender, EventArgs e) {
            RenameFiles(TrainNotFaceRenameTxtBx.Text, TrainNotFaceSrcDir.Text, TrainNotFaceDestDir.Text, "Train not face file prefix");
        }

        private void RenameTestNotFaceBtn_Click(object sender, EventArgs e) {
            RenameFiles(TestFaceRenameTxtBx.Text, TestFaceSrcDir.Text, TestFaceDestDir.Text, "Test not face file prefix");
        }

        private void RenameTestFaceButton_Click(object sender, EventArgs e) {
            RenameFiles(TestNotFaceRenameTxtBx.Text, TestNotFaceSrcDir.Text, TestNotFaceDestDir.Text, "Test face file prefix");
        }

        private void RenameFiles(string dirFrom, string dirTo, string prefix, string imgSetName) {

            if (prefix.IsEmpty(imgSetName)) // shows a waning box
                return; // don't rename
            if (!dirFrom.DirExists() || !dirTo.DirExists()) // shows a create directory question msg box
                return; // don't rename

            string[] filePaths = Directory.GetFiles(dirFrom);

            outputTextBox.Clear();
            for (int i = 0; i < filePaths.Length; i++) {

                //textBox1.Text = textBox1.Text + filePaths[i] + "\r\n";
                string ff = filePaths[i];
                string f = Path.GetFileName(ff);
                outputTextBox.Text = outputTextBox.Text + f + "\r\n";


                string fromFile = dirFrom + f;
                string toFile = dirTo + prefix + i.ToString("D5") + ".bmp";
                CopyFile(fromFile, toFile);

                if (i > 10) break;
                CopyFile(fromFile, toFile);
            }
        }

        private void CopyFile(string fromFile, string toFile) {
            outputTextBox.Text = outputTextBox.Text + "from >>" + fromFile + "\r\n";
            outputTextBox.Text = outputTextBox.Text + "to   >>" + toFile + "\r\n\r\n";
            string m1 = "from >>" + fromFile + "\r\n";
            string m2 = "to   >>" + toFile + "\r\n\r\n";
            outputTextBox.AppendText(m1);
            outputTextBox.AppendText(m2);

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

        private void RenameAllImagesBtn_Click(object sender, EventArgs e) {

            TrainFaceRenameTxtBx.Text = "";
            TrainNotFaceRenameTxtBx.Text = "";
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

            RenameFiles(TrainFaceSrcDir.Text, TrainFaceDestDir.Text, TrainFaceRenameTxtBx.Text, "Train face file prefix");
            RenameFiles(TrainNotFaceRenameTxtBx.Text, TrainNotFaceSrcDir.Text, TrainNotFaceDestDir.Text, "Train not face file prefix");
            RenameFiles(TestFaceRenameTxtBx.Text, TestFaceSrcDir.Text, TestFaceDestDir.Text, "Test not face file prefix");
            RenameFiles(TestNotFaceRenameTxtBx.Text, TestNotFaceSrcDir.Text, TestNotFaceDestDir.Text, "Test face file prefix");
        }

        private void StopButton_Click(object sender, EventArgs e) {
            if (!cancelTokenSource.IsCancellationRequested) {
                outputTextBox.AppendText("Stopping called!");
                cancelTokenSource.Cancel();
            }
        }
    }
}
