using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImageProcessor {
    public partial class Form1 : Form {

        //string dirTestFaceBmp = Directory.GetCurrentDirectory() +  @"\data\TestFaceBmp\";
        //string dirTestNotFaceBmp = Directory.GetCurrentDirectory() + @"\data\TestNotFaceBmp\";
        string dirTrainFaceBmp = Directory.GetCurrentDirectory() + @"\data\TrainFaceBmp\";
        string dirTrainNotFaceBmp = Directory.GetCurrentDirectory() + @"\data\TrainNotFaceBmp\";
        string dirTrainNotFaceBmpOld = Directory.GetCurrentDirectory() + @"\data\TrainNotFaceBmpOld\";

        //string prefix = "cmu_";

        //int testFace = 471;
        // int testNotFace = 23572;
        // int trainFace = 2429;
        // int trainNotFace = 4547;

        Bitmap bmp = null;

        public int[,] sobelX = new int[3, 3] { { -1, 0, 1 }, { -2, 0, 2 }, { -1, 0, 1 } };
        public int[,] sobelY = new int[3, 3] { { -1, -2, -1 }, { 0, 0, 0 }, { 1, 2, 1 } };

        public int[,] kirschX = new int[3, 3] { { -5, 5, 3 }, { -5, 0, 3 }, { 3, 3, 3 } };
        public int[,] kirschY = new int[3, 3] { { -5, -5, 3 }, { -5, 0, 3 }, { 3, 3, 3 } };

        public int[,] operatorX;
        public int[,] operatorY;
        public string operatorXName;
        public string operatorYName;

        public int[,] sXtemp = new int[3, 3];
        public int[,] sYtemp = new int[3, 3];

        double sX = 0;
        double sY = 0;
        double mathOperator = 0;

        int[] gaps = new int[7] { 0, 3, 6, 8, 10, 13, 16 };

        public Form1() {
            InitializeComponent();
        }

        //private void button1_Click(object sender, EventArgs e)
        //{

        //}

        private void button1_Click_1(object sender, EventArgs e) {
            Close();
        }

        private void button2_Click(object sender, EventArgs e) {
            // test 1
            bmp = new Bitmap(dirTrainFaceBmp + "face00001.bmp", false);

            pictureBox1.Image = bmp;

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

        public void copyFile(string fromFile, string toFile, int i) {
            //textBox1.Text = textBox1.Text + "from >>" + fromFile + "\r\n";
            //textBox1.Text = textBox1.Text + "to   >>" + toFile + "\r\n\r\n";            
            string m1 = "from >>" + fromFile + "\r\n";
            string m2 = "to   >>" + toFile + "\r\n\r\n";
            textBox1.AppendText(m1);
            textBox1.AppendText(m2);

            //File.Copy(fromFile, toFile, true); // - basically commented out as a saftey catch
        }

        private void button3_Click(object sender, EventArgs e) {
            // Rename Files in dirTrainNotFaceBmpOld 

            string[] filePaths = Directory.GetFiles(dirTrainNotFaceBmpOld);

            textBox1.Clear();
            for (int i = 0; i < filePaths.Length; i++) {

                //textBox1.Text = textBox1.Text + filePaths[i] + "\r\n";
                string ff = filePaths[i];
                string f = Path.GetFileName(ff);
                //textBox1.Text = textBox1.Text + f + "\r\n";


                string fromFile = dirTrainNotFaceBmpOld + f;
                string toFile = dirTrainNotFaceBmp + textBox2.Text + i.ToString("D5") + ".bmp";
                // copyFile(fromFile, toFile);

                //if (i > 10) break;
                copyFile(fromFile, toFile, i);

            }
        }

        /// <summary>
        /// multiply (not a matrix multiply) two 3x3 matrixes
        /// put result back in a
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        public void threeBythreeMultiply(int[,] a, int[,] b) {
            for (int y = 0; y < 3; y++) {
                for (int x = 0; x < 3; x++) {
                    a[x, y] = a[x, y] * b[x, y];
                }
            }
        }

        public int threeBythreeSum(int[,] a) {
            int sum = 0;
            for (int y = 0; y < 3; y++) {
                for (int x = 0; x < 3; x++) {
                    sum = sum + a[x, y];
                }
            }
            return sum;
        }

        public string threebythreeToString(int[,] a, string heading) {
            string t = "";
            if (heading != "") t = t + heading + "\r\n";
            for (int y = 0; y < 3; y++) {
                t = t + a[0, y].ToString() + " " + a[1, y].ToString() + " " + a[2, y].ToString() + "\r\n";
            }
            return t;
        }

        double computeOperator(int xx, int yy, Bitmap bmp, bool show) {
            for (int y = 0; y < 3; y++) {
                for (int x = 0; x < 3; x++) {
                    Color c = bmp.GetPixel(x + xx, y + yy);
                    sXtemp[x, y] = c.R;
                    sYtemp[x, y] = c.R;
                }
            }

            if (show) {
                textBox1.AppendText(threebythreeToString(sXtemp, "sXtemp"));
                textBox1.AppendText(threebythreeToString(sYtemp, "sYtemp"));

                textBox1.AppendText(threebythreeToString(operatorX, operatorXName));
                textBox1.AppendText(threebythreeToString(operatorY, operatorYName));
            }
            threeBythreeMultiply(sXtemp, operatorX);
            threeBythreeMultiply(sYtemp, operatorY);


            sX = threeBythreeSum(sXtemp);
            sY = threeBythreeSum(sYtemp);
            mathOperator = Math.Abs(sX) + Math.Abs(sY);
            if (show) {
                textBox1.AppendText(threebythreeToString(sXtemp, "sXtemp"));
                textBox1.AppendText(threebythreeToString(sYtemp, "sYtemp"));
                textBox1.AppendText("sX=" + sX.ToString() + " " + "sY=" + sY.ToString() + " operator=" + mathOperator.ToString());
            }
            return mathOperator;
        }

        public string showBitmap(Bitmap bmp) {
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

        private void button4_Click(object sender, EventArgs e) {
            // test 2
            bmp = new Bitmap(dirTrainFaceBmp + "face00001.bmp", false);

            pictureBox1.Image = bmp;

            textBox1.Clear();

            string bb = showBitmap(bmp);
            textBox1.AppendText(bb);

            // compute sobel @ 0,0

            computeOperator(0, 0, bmp, true);

            textBox1.AppendText("\r\n  .........................................  \r\n");

            computeOperator(3, 3, bmp, true);

            textBox1.AppendText("\r\n  .........................................  \r\n");

            computeOperator(6, 3, bmp, true);

        }

        private void button8_Click(object sender, EventArgs e) {
            // test 2
            bmp = new Bitmap(dirTrainFaceBmp + "face00001.bmp", false);

            pictureBox1.Image = bmp;

            textBox1.Clear();

            string bb = showBitmap(bmp);
            textBox1.AppendText(bb);

            // compute kirsch @ 0,0

            computeOperator(0, 0, bmp, true);

            textBox1.AppendText("\r\n  .........................................  \r\n");

            computeOperator(3, 3, bmp, true);

            textBox1.AppendText("\r\n  .........................................  \r\n");

            computeOperator(6, 3, bmp, true);

        }

        public string processImage(string fileName, int classification, bool show) {
            string retv = "";

            bmp = new Bitmap(fileName, false);

            pictureBox1.Image = bmp;

            if (show) {

                textBox1.AppendText("\r\n  .#.....................................#.  \r\n");
                textBox1.AppendText("Fname = " + fileName + "\r\n");
                string bb = showBitmap(bmp);
                textBox1.AppendText(bb);
                textBox1.AppendText("  .-.....................................-.  \r\n");
            }

            for (int yy = 0; yy < 7; yy++) {
                for (int xx = 0; xx < 7; xx++) {
                    double s = computeOperator(gaps[xx], gaps[yy], bmp, false);
                    //if (show) { textBox1.AppendText(s.ToString() + " "); }
                    retv = retv + s.ToString() + " ";
                }
            }
            retv = retv + classification.ToString();
            if (show) { textBox1.AppendText(retv); }
            return retv;

        }

        private void button5_Click(object sender, EventArgs e) {
            // test3
            textBox1.Clear();

            string fname = dirTrainFaceBmp + "face00001.bmp";
            string val = processImage(fname, 0, true);

            string fname2 = dirTrainFaceBmp + "face00002.bmp";
            string val2 = processImage(fname2, 0, true);

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

        private void button6_Click(object sender, EventArgs e) {
            processDir(dirTrainFaceBmp, "face", "D5", 1, 2429, "trainDataFace.txt", 1, true);
        }

        public void processDir(string inputDirectory, string prefix, string formatS, int lowVal, int hiVal, string outputFile, int classification, bool show) {
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

                string val = processImage(fname, classification, false);

                if (i < 10 && show) {
                    textBox1.AppendText(val + "\r\n");
                }
                //if (i == 10) break; // debug exit

                file.WriteLine(val);
            }

            file.Close();

        }

        private void button7_Click(object sender, EventArgs e) {
            // commented out as a safety catch 
            // processDir(dirTrainFaceBmp, "face", "D5", 1, 2429, "trainDataFace.txt", 1, true);

            //  processDir(dirTrainNotFaceBmp, "cmu_", "D5", 0, 4547, "trainDataNotFace.txt", 0, true);

            //  processDir(dirTestFaceBmp, "cmu_", "D4", 0, 471, "testDataFace.txt", 1, true);

            //  processDir(dirTestNotFaceBmp, "cmu_", "D4", 0, 23572, "testDataNotFace.txt", 0, true);
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e) {
            // sobel operator radio button clicked
            operatorX = sobelX;
            operatorY = sobelY;
            operatorXName = "sobelX";
            operatorYName = "sobelY";
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e) {
            // kirsch operator radio button clicked
            operatorX = kirschX;
            operatorY = kirschY;
            operatorXName = "kirschX";
            operatorYName = "kirschY";
        }
    }
}
