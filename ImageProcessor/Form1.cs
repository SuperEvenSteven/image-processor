using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace ImageProcessor {
    public partial class Form1 : Form {

        internal class ImageOperator {

            public ImageOperator() {
                TempX = new int[3, 3];
                TempY = new int[3, 3];
            }

            public int[,] OperatorX { get; set; }
            public int[,] OperatorY { get; set; }
            public string Name { get; set; }

            public int[,] TempX { get; set; }
            public int[,] TempY { get; set; }

            double x, y = 0;
            public double mathOperator = 0;

            public double Apply(int xx, int yy, Bitmap bmp, bool show, TextBox tb) {
                for (int y = 0; y < 3; y++) {
                    for (int x = 0; x < 3; x++) {
                        Color c = bmp.GetPixel(x + xx, y + yy);
                        TempX[x, y] = c.R;
                        TempY[x, y] = c.R;
                    }
                }

                if (show) {
                    tb.AppendText(ThreebythreeToString(TempX, "tempX"));
                    tb.AppendText(ThreebythreeToString(TempY, "tempY"));

                    tb.AppendText(ThreebythreeToString(OperatorX, Name));
                    tb.AppendText(ThreebythreeToString(OperatorY, Name));
                }
                ThreeBythreeMultiply(TempX, OperatorX);
                ThreeBythreeMultiply(TempY, OperatorY);


                x = ThreeBythreeSum(TempX);
                y = ThreeBythreeSum(TempY);
                mathOperator = Math.Abs(x) + Math.Abs(y);
                if (show) {
                    tb.AppendText(ThreebythreeToString(TempX, "tempX"));
                    tb.AppendText(ThreebythreeToString(TempY, "tempY"));
                    tb.AppendText("sX=" + x.ToString() + " " + "sY=" + x.ToString() + " operator=" + mathOperator.ToString());
                }
                return mathOperator;
            }

            /// <summary>
            /// multiply (not a matrix multiply) two 3x3 matrixes
            /// put result back in a
            /// </summary>
            /// <param name="a"></param>
            /// <param name="b"></param>
            private void ThreeBythreeMultiply(int[,] a, int[,] b) {
                for (int y = 0; y < 3; y++) {
                    for (int x = 0; x < 3; x++) {
                        a[x, y] = a[x, y] * b[x, y];
                    }
                }
            }

            private int ThreeBythreeSum(int[,] a) {
                int sum = 0;
                for (int y = 0; y < 3; y++) {
                    for (int x = 0; x < 3; x++) {
                        sum = sum + a[x, y];
                    }
                }
                return sum;
            }

            private string ThreebythreeToString(int[,] a, string heading) {
                string t = "";
                if (heading != "") t = t + heading + "\r\n";
                for (int y = 0; y < 3; y++) {
                    t = t + a[0, y].ToString() + " " + a[1, y].ToString() + " " + a[2, y].ToString() + "\r\n";
                }
                return t;
            }
        }

        #region Member variables
        private readonly int[] gaps = new int[7] { 0, 3, 6, 8, 10, 13, 16 };
        private string dirTestFaceBmp = @"C:\Temp\IPP\FaceData\TestFaceBmp\";
        private string dirTestNotFaceBmp = @"C:\Temp\IPP\FaceData\TestNotFaceBmp\";
        private string dirTrainFaceBmp = @"C:\Temp\IPP\FaceData\TrainFaceBmp\";
        private string dirTrainNotFaceBmp = @"C:\Temp\IPP\FaceData\TrainNotFaceBmp\";
        private string dirTrainNotFaceBmpOld = @"C:\Temp\IPP\FaceData\TrainNotFaceBmpOld\";

        // string prefix = "cmu_";
        // int testFace = 471;
        // int testNotFace = 23572;
        // int trainFace = 2429;
        // int trainNotFace = 4547;

        Bitmap bmp = null;

        // https://homepages.inf.ed.ac.uk/rbf/HIPR2/canny.htm
        private double[,,] selectedOperator = ConvolutionUtils.Matrix.Sobel3x3x8;
        #endregion

        public Form1() {
            InitializeComponent();

            //// initialise sobel operator
            //sobelOperator = new ImageOperator {
            //    OperatorX = new int[3, 3] { { -1, 0, 1 }, { -2, 0, 2 }, { -1, 0, 1 } },
            //    OperatorY = new int[3, 3] { { -1, -2, -1 }, { 0, 0, 0 }, { 1, 2, 1 } },
            //    Name = "sobel"
            //};

            //// initialise kirsch operator
            //kirschOperator = new ImageOperator {
            //    OperatorX = new int[3, 3] { { -5, 5, 3 }, { -5, 0, 3 }, { 3, 3, 3 } },
            //    OperatorY = new int[3, 3] { { -5, -5, 3 }, { -5, 0, 3 }, { 3, 3, 3 } },
            //    Name = "kirsch"
            //};
        }

        #region Debug Button Clicks
        private void DisplayAsRawRGBButton_Click(object sender, EventArgs e) {
            // test 1
            bmp = new Bitmap(dirTrainFaceBmp + "face00001.bmp", false);
            PreviewPictureBox.Image = bmp;
            textBox1.Clear();

            string s;

            for (int y = 0; y < 19; y++) {
                for (int x = 0; x < 19; x++) {
                    Color c = bmp.GetPixel(x, y);
                    s = "R" + c.R.ToString() + " " + "G" + c.G.ToString() + " " + "B" + c.B.ToString() + " ";
                    textBox1.AppendText(s);
                }
                textBox1.AppendText("\r\n");
            }

        }

        private void PreviewOperatorButton_Click(object sender, EventArgs e) {
            // test 2
            bmp = new Bitmap(dirTrainFaceBmp + "face00001.bmp", false);

            PreviewPictureBox.Image = bmp.ConvolutionFilter(selectedOperator, 1.0 / 4.0);
            textBox1.Clear();

            //string bb = ShowBitmap(bmp);
            //textBox1.AppendText(bb);

            // compute operator @ 0,0

            //selectedOperator.Apply(0, 0, bmp, true, textBox1);
            //textBox1.AppendText("\r\n  .........................................  \r\n");
            //selectedOperator.Apply(3, 3, bmp, true, textBox1);
            //textBox1.AppendText("\r\n  .........................................  \r\n");
            //selectedOperator.Apply(6, 3, bmp, true, textBox1);
        }

        #endregion

        #region Bulk Actions

        private void ProcessAllImageSetsButton_Click(object sender, EventArgs e) {
            // commented out as a safety catch 
            ProcessDir(dirTrainFaceBmp, "face", "D5", 1, 2429, "trainDataFace.txt", 1, true);
            ProcessDir(dirTrainNotFaceBmp, "cmu_", "D5", 0, 4547, "trainDataNotFace.txt", 0, true);
            ProcessDir(dirTestFaceBmp, "cmu_", "D4", 0, 471, "testDataFace.txt", 1, true);
            ProcessDir(dirTestNotFaceBmp, "cmu_", "D4", 0, 23572, "testDataNotFace.txt", 0, true);
        }
        private void RenameFilesButton_Click(object sender, EventArgs e) {
            // Rename Files in dirTrainNotFaceBmpOld 

            string[] filePaths = Directory.GetFiles(dirTrainNotFaceBmpOld);

            textBox1.Clear();
            for (int i = 0; i < filePaths.Length; i++) {

                //textBox1.Text = textBox1.Text + filePaths[i] + "\r\n";
                string ff = filePaths[i];
                string f = Path.GetFileName(ff);
                //textBox1.Text = textBox1.Text + f + "\r\n";


                string fromFile = dirTrainNotFaceBmpOld + f;
                string toFile = dirTrainNotFaceBmp + TrainFacePrefixTextBox.Text + i.ToString("D5") + ".bmp";
                // copyFile(fromFile, toFile);

                //if (i > 10) break;
                CopyFile(fromFile, toFile, i);

            }
        }

        private void ExitButton_Click_1(object sender, EventArgs e) {
            Close();
        }

        #endregion

        #region Train Image Set Actions

        private void TrainFaceBoth_Click(object sender, EventArgs e) {
            // test3
            textBox1.Clear();

            string fname = dirTrainFaceBmp + "face00001.bmp";
            string val = ProcessImage(fname, 0, true);

            string fname2 = dirTrainFaceBmp + "face00002.bmp";
            string val2 = ProcessImage(fname2, 0, true);

            //bmp = new Bitmap(dirTrainFaceBmp + "face00001.bmp", false);

            //pictureBox1.Image = bmp;

            //textBox1.Clear();

            //string bb = showBitmap(bmp);
            //textBox1.AppendText(bb);

            //textBox1.AppendText("\r\n  .........................................  \r\n");


            //for (int yy = 0; yy < 7; yy++)
            //{
            //    for (int xx = 0; xx < 7; xx++)
            //    {
            //        double s = computeSobel(gaps[xx], gaps[yy], bmp, false);
            //        textBox1.AppendText(s.ToString() + " ");
            //    }
            //}

        }

        private void ProcessTrainFacesButton_Click(object sender, EventArgs e) {
            ProcessDir(dirTrainFaceBmp, "face", "D5", 1, 2429, "trainDataFace.txt", 1, true);
        }

        private void ProcessTrainNotFaceButton_Click(object sender, EventArgs e) {
            ProcessDir(dirTrainNotFaceBmp, "face", "D5", 1, 2429, "trainDataFace.txt", 1, true);
        }

        #endregion

        #region Test Image Set Actions

        private void ProcessTestFaceButton_Click(object sender, EventArgs e) {
            ProcessDir(dirTestFaceBmp, "face", "D5", 1, 2429, "trainDataFace.txt", 1, true);
        }

        private void ProcessTestNotFaceButton_Click(object sender, EventArgs e) {
            ProcessDir(dirTestNotFaceBmp, "face", "D5", 1, 2429, "trainDataFace.txt", 1, true);
        }

        #endregion

        #region Util Methods

        private void ProcessDir(string inputDirectory, string prefix, string formatS, int lowVal, int hiVal, string outputFile, int classification, bool show) {
            // test 4 process a directory

            //string inputDirectory = dirTrainFaceBmp;
            //string prefix = "face";
            //string formatS = "D5";
            //int lowVal = 1;
            //int hiVal = 2429;
            //string outputFile = "trainDataFace.txt";

            textBox1.Clear();

            string outName = inputDirectory + outputFile;
            System.IO.StreamWriter file = new System.IO.StreamWriter(outName);
            //file.WriteLine(lines);


            for (int i = lowVal; i <= hiVal; i++) {
                string fname = inputDirectory + prefix + i.ToString(formatS) + ".bmp";
                textBox1.AppendText("Process> " + fname + " into " + outName + "\r\n");

                string val = ProcessImage(fname, classification, false);

                if (i < 10 && show) {
                    textBox1.AppendText(val + "\r\n");
                }
                //if (i == 10) break; // debug exit

                file.WriteLine(val);
            }

            file.Close();

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

            PreviewPictureBox.Image = bmp.ConvolutionFilter(selectedOperator, 1.0 / 4.0);

            if (show) {

                textBox1.AppendText("\r\n  .#.....................................#.  \r\n");
                textBox1.AppendText("Fname = " + fileName + "\r\n");
                string bb = ShowBitmap(bmp);
                textBox1.AppendText(bb);
                textBox1.AppendText("  .-.....................................-.  \r\n");
            }

            //for (int yy = 0; yy < 7; yy++) {
            //    for (int xx = 0; xx < 7; xx++) {
            //        double s = selectedOperator.Apply(gaps[xx], gaps[yy], bmp, false, textBox1);
            //        //if (show) { textBox1.AppendText(s.ToString() + " "); }
            //        retv = retv + s.ToString() + " ";
            //    }
            //}
            retv = retv + classification.ToString();
            if (show) { textBox1.AppendText(retv); }
            return retv;

        }

        private void CopyFile(string fromFile, string toFile, int i) {
            //textBox1.Text = textBox1.Text + "from >>" + fromFile + "\r\n";
            //textBox1.Text = textBox1.Text + "to   >>" + toFile + "\r\n\r\n";            
            string m1 = "from >>" + fromFile + "\r\n";
            string m2 = "to   >>" + toFile + "\r\n\r\n";
            textBox1.AppendText(m1);
            textBox1.AppendText(m2);

            //File.Copy(fromFile, toFile, true); // - basically commented out as a saftey catch
        }
        #endregion

        #region Operator Radio Button Clicks

        private void Prewitt3x3x4RadioButton_CheckedChanged(object sender, EventArgs e) {
            selectedOperator = ConvolutionUtils.Matrix.Prewitt3x3x4;
        }

        private void Prewitt3x3x8RadioButton_CheckedChanged(object sender, EventArgs e) {
            selectedOperator = ConvolutionUtils.Matrix.Prewitt3x3x8;
        }

        private void Prewitt5x5x4RadioButton_CheckedChanged(object sender, EventArgs e) {
            selectedOperator = ConvolutionUtils.Matrix.Prewitt5x5x4;
        }

        private void Kirsch3x3x1RadioButton_CheckedChanged(object sender, EventArgs e) {
            selectedOperator = ConvolutionUtils.Matrix.Kirsch3x3x1;
        }

        private void Kirsch3x3x4RadioButton_CheckedChanged(object sender, EventArgs e) {
            selectedOperator = ConvolutionUtils.Matrix.Kirsch3x3x4;
        }

        private void Kirsch3x3x8RadioButton_CheckedChanged(object sender, EventArgs e) {
            selectedOperator = ConvolutionUtils.Matrix.Kirsch3x3x8;
        }

        private void Sobel3x3x1RadioButton_CheckedChanged(object sender, EventArgs e) {
            selectedOperator = ConvolutionUtils.Matrix.Sobel3x3x1;
        }

        private void Sobel3x3x4RadioButton_CheckedChanged(object sender, EventArgs e) {
            selectedOperator = ConvolutionUtils.Matrix.Sobel3x3x4;
        }

        private void Sobel3x3x8RadioButton_CheckedChanged(object sender, EventArgs e) {
            selectedOperator = ConvolutionUtils.Matrix.Sobel3x3x8;
        }

        private void Sobel5x5x4RadioButton_CheckedChanged(object sender, EventArgs e) {
            selectedOperator = ConvolutionUtils.Matrix.Sobel5x5x4;
        }

        private void Scharr3x3x4RadioButton_CheckedChanged(object sender, EventArgs e) {
            selectedOperator = ConvolutionUtils.Matrix.Scharr3x3x4;
        }

        private void Scharr3x3x8RadioButton_CheckedChanged(object sender, EventArgs e) {
            selectedOperator = ConvolutionUtils.Matrix.Scharr3x3x8;
        }

        private void Scharr5x5x4RadioButton_CheckedChanged(object sender, EventArgs e) {
            selectedOperator = ConvolutionUtils.Matrix.Scharr5x5x4;
        }

        private void Isotropic3x3x4RadioButton_CheckedChanged(object sender, EventArgs e) {
            selectedOperator = ConvolutionUtils.Matrix.Isotropic3x3x4;
        }

        private void Isotropic3x3x8RadioButton_CheckedChanged(object sender, EventArgs e) {
            selectedOperator = ConvolutionUtils.Matrix.Isotropic3x3x8;
        }
        #endregion
    }
}
