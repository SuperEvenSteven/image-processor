using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace ImageProcessor {
    /// <summary>
    /// Compass edge detection original author:  Dewald Esterhuizen
    ///  
    /// Heavily modified for use in comparison against Robert Cox's 3x3 Sobel Operator.
    /// 
    /// For research use in Soft Computing - University of Canberra.
    /// 
    /// Sourced from: https://softwarebydefault.com/2013/06/22/compass-edge-detection/
    /// 
    /// </summary>
    /// <param name="baseKernel"></param>
    /// <param name="degrees"></param>
    /// <returns></returns>
    public static class Convolution {

        /// <summary>
        /// Operator definitions.
        /// </summary>
        public static class Matrix {

            public static double[,,] Prewitt3x3x1 {
                get {
                    double[,,] baseKernel = new double[,,] { { {  -1,  0,  1,  },
                                                         {        -1,  0,  1,  },
                                                         {        -1,  0,  1,  }, } };




                    return baseKernel;
                }
            }

            public static double[,,] Prewitt3x3x4 {
                get {
                    double[,] baseKernel = new double[,] { {  -1,  0,  1,  },
                                                         {    -1,  0,  1,  },
                                                         {    -1,  0,  1,  }, };


                    double[,,] kernel = RotateMatrix(baseKernel, 90);


                    return kernel;
                }
            }

            public static double[,,] Prewitt3x3x8 {
                get {
                    double[,] baseKernel = new double[,] { {  -1,  0,  1,  },
                                                         {    -1,  0,  1,  },
                                                         {    -1,  0,  1,  }, };
                    double[,,] kernel = RotateMatrix(baseKernel, 45);
                    return kernel;
                }
            }

            public static double[,,] Kirsch3x3x1 {
                get {
                    double[,,] baseKernel = new double[,,] { { {  -3, -3,  5,  },
                                                           {      -3,  0,  5,  },
                                                           {      -3, -3,  5,  }, } };
                    return baseKernel;
                }
            }

            public static double[,,] Kirsch3x3x4 {
                get {
                    double[,] baseKernel = new double[,] { {  -3, -3,  5,  },
                                                           {  -3,  0,  5,  },
                                                           {  -3, -3,  5,  }, };
                    double[,,] kernel = RotateMatrix(baseKernel, 90);
                    return kernel;
                }
            }

            public static double[,,] Kirsch3x3x8 {
                get {
                    double[,] baseKernel = new double[,] { {  -3, -3,  5,  },
                                                           {  -3,  0,  5,  },
                                                           {  -3, -3,  5,  }, };
                    double[,,] kernel = RotateMatrix(baseKernel, 45);
                    return kernel;
                }
            }

            public static double[,,] Sobel3x3x1 {
                get {
                    double[,,] baseKernel = new double[,,] { { {  -1,  0,  1,  },
                                                           {      -2,  0,  2,  },
                                                           {      -1,  0,  1,  }, }};
                    return baseKernel;
                }
            }

            public static double[,,] Sobel3x3x4 {
                get {
                    double[,] baseKernel = new double[,] { {  -1,  0,  1,  },
                                                           {  -2,  0,  2,  },
                                                           {  -1,  0,  1,  }, };
                    double[,,] kernel = RotateMatrix(baseKernel, 90);
                    return kernel;
                }
            }

            public static double[,,] Sobel3x3x8 {
                get {
                    double[,] baseKernel = new double[,] { {  -1,  0,  1,  },
                                                           {  -2,  0,  2,  },
                                                           {  -1,  0,  1,  }, };
                    double[,,] kernel = RotateMatrix(baseKernel, 45);
                    return kernel;
                }
            }

            public static double[,,] Scharr3x3x1 {
                get {
                    double[,,] baseKernel = new double[,,] { { {  -1,  0,  1,  },
                                                           {      -3,  0,  3,  },
                                                           {      -1,  0,  1,  }, } };
                    return baseKernel;
                }
            }

            public static double[,,] Scharr3x3x4 {
                get {
                    double[,] baseKernel = new double[,] { {  -1,  0,  1,  },
                                                           {  -3,  0,  3,  },
                                                           {  -1,  0,  1,  }, };
                    double[,,] kernel = RotateMatrix(baseKernel, 90);
                    return kernel;
                }
            }

            public static double[,,] Scharr3x3x8 {
                get {
                    double[,] baseKernel = new double[,] { {  -1,  0,  1,  },
                                                           {  -3,  0,  3,  },
                                                           {  -1,  0,  1,  }, };
                    double[,,] kernel = RotateMatrix(baseKernel, 45);
                    return kernel;
                }
            }

            public static double[,,] Isotropic3x3x1 {
                get {
                    double[,,] baseKernel = new double[,,] { { {             -1,  0,             1,  },
                                                           {      -Math.Sqrt(2),  0,  Math.Sqrt(2),  },
                                                           {                 -1,  0,             1,  },  } };
                    return baseKernel;
                }
            }

            public static double[,,] Isotropic3x3x4 {
                get {
                    double[,] baseKernel = new double[,] { {             -1,  0,             1,  },
                                                           {  -Math.Sqrt(2),  0,  Math.Sqrt(2),  },
                                                           {             -1,  0,             1,  },  };
                    double[,,] kernel = RotateMatrix(baseKernel, 90);
                    return kernel;
                }
            }

            public static double[,,] Isotropic3x3x8 {
                get {
                    double[,] baseKernel = new double[,] { {             -1,  0,             1,  },
                                                           {  -Math.Sqrt(2),  0,  Math.Sqrt(2),  },
                                                           {             -1,  0,             1,  }, };
                    double[,,] kernel = RotateMatrix(baseKernel, 45);
                    return kernel;
                }
            }
        }

        private static readonly int[] gaps = new int[7] { 0, 3, 6, 8, 10, 13, 16 };

        /// <summary>
        /// Applies the given operator against the given image and returns a new bitmap.
        /// </summary>
        /// <param name="sourceBitmap"></param>
        /// <param name="filterMatrix"></param>
        /// <param name="factor"></param>
        /// <param name="bias"></param>
        /// <returns></returns>
        public static Bitmap ConvolutionFilter(this Bitmap sourceBitmap, double[,,] filterMatrix, TextBox tb, double factor = 1, int bias = 0) {
            byte[] resultBuffer = ConvolutionFilterAsBytes(sourceBitmap, filterMatrix, tb, factor, bias);
            Bitmap resultBitmap = new Bitmap(sourceBitmap.Width, sourceBitmap.Height);

            BitmapData resultData = resultBitmap.LockBits(new Rectangle(0, 0, resultBitmap.Width, resultBitmap.Height),
                ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);

            Marshal.Copy(resultBuffer, 0, resultData.Scan0, resultBuffer.Length);
            resultBitmap.UnlockBits(resultData);

            return resultBitmap;
        }

        /// <summary>
        /// This method originally applied the given operator against R,G and B colours in the given bitmap. I've
        /// re-worked this to adapt the same logging as Robert Cox's sample ThreeByThree textbox logging and simplified
        /// it to use only the R value as he did.
        /// </summary>
        /// <param name="sourceBitmap"></param>
        /// <param name="filterMatrix"></param>
        /// <param name="factor"></param>
        /// <param name="bias"></param>
        /// <returns></returns>
        public static byte[] ConvolutionFilterAsBytes(Bitmap sourceBitmap, double[,,] filterMatrix, TextBox tb, double factor = 1, int bias = 0) {
            BitmapData sourceData = sourceBitmap.LockBits(new Rectangle(0, 0, sourceBitmap.Width, sourceBitmap.Height), ImageLockMode.ReadOnly,
                                                 PixelFormat.Format32bppArgb);

            byte[] pixelBuffer = new byte[sourceData.Stride * sourceData.Height];
            byte[] resultBuffer = new byte[sourceData.Stride * sourceData.Height];

            Marshal.Copy(sourceData.Scan0, pixelBuffer, 0, pixelBuffer.Length);
            sourceBitmap.UnlockBits(sourceData);

            int filterWidth = filterMatrix.GetLength(1);
            //int filterHeight = filterMatrix.GetLength(0);
            int filterOffset = (filterWidth - 1) / 2;
            int byteOffset = 0;
            byte[] rgbByte = new byte[3];

            //for (int yy = 0; yy < 7; yy++) {
            //for (int xx = 0; xx < 7; xx++) {
            //double s = selectedOperator.Apply(gaps[xx], gaps[yy], bmp, false, textBox1);
            //if (show) { textBox1.AppendText(s.ToString() + " "); }
            //retv = retv + s.ToString() + " ";
            //}
            //}
            //retv = retv + classification.ToString();
            //if (show) { textBox1.AppendText(retv); }


            // Iterate width and height of the given image, assuming a 19x19 pixel image.
            // Check a grid of numbers and multiple the central value against the operator
            // as shown in the lecture slides. Move the grid in 3x3 increments like a stamp
            // until all the image has been covered.
            for (int offsetY = filterOffset; offsetY < sourceBitmap.Height - filterOffset; offsetY++) {
                for (int offsetX = filterOffset; offsetX < sourceBitmap.Width - filterOffset; offsetX++) {
                    byteOffset = offsetY *
                                 sourceData.Stride +
                                 offsetX * 4;
                    // perform operator multiply and save the rgba values as bytes to the buffer
                    rgbByte = ComputeOperator(filterMatrix, filterOffset, byteOffset, pixelBuffer, factor, bias, sourceData);

                    tb.AppendText(rgbByte[0] + " ");

                    resultBuffer[byteOffset] = rgbByte[0];
                    //resultBuffer[byteOffset + 1] = rgbByte[1];
                    //resultBuffer[byteOffset + 2] = rgbByte[2];
                    //resultBuffer[byteOffset + 3] = 255;
                }
            }
            return resultBuffer;
        }

        /// <summary>
        /// Multiplies the R,G and B values against the given operator for each direction. Highest value wins!!!
        /// Returns highest of R,G and B.
        /// </summary>
        /// <returns></returns>
        private static byte[] ComputeOperator(double[,,] filterMatrix, int filterOffset, int byteOffset, byte[] pixelBuffer, double factor, int bias,
            BitmapData sourceData) {

            var highestRGB = new byte[3];
            double blue = 0;
            double green = 0;
            double red = 0;
            double blueCompass = 0.0;
            double greenCompass = 0.0;
            double redCompass = 0.0;
            int calcOffset = 0;

            // Iterate through all the directional filters and take only the highest 
            // 3x3 multiply.
            for (int compass = 0; compass < filterMatrix.GetLength(0); compass++) {

                blueCompass = 0.0;
                greenCompass = 0.0;
                redCompass = 0.0;

                // go through each element in the filter and multiple the color values
                for (int filterY = -filterOffset; filterY <= filterOffset; filterY++) {
                    for (int filterX = -filterOffset; filterX <= filterOffset; filterX++) {
                        calcOffset = byteOffset +
                                     (filterX * 4) +
                                     (filterY * sourceData.Stride); // move ever 3rd pixel

                        blueCompass += (double)(pixelBuffer[calcOffset]) *
                                        filterMatrix[compass,
                                        filterY + filterOffset,
                                        filterX + filterOffset];

                        greenCompass += (double)(pixelBuffer[calcOffset + 1]) *
                                        filterMatrix[compass,
                                        filterY + filterOffset,
                                        filterX + filterOffset];

                        redCompass += (double)(pixelBuffer[calcOffset + 2]) *
                                        filterMatrix[compass,
                                        filterY + filterOffset,
                                        filterX + filterOffset];
                    }
                }

                // get the highest value of all, default to 0 if negative
                blue = (blueCompass > blue ? blueCompass : blue);
                green = (greenCompass > green ? greenCompass : green);
                red = (redCompass > red ? redCompass : red);
            }

            // apply bias if any
            blue = factor * blue + bias;
            green = factor * green + bias;
            red = factor * red + bias;

            // ensure max and min range are applied if the result is over or under
            if (blue > 255) { blue = 255; } else if (blue < 0) { blue = 0; }
            if (green > 255) { green = 255; } else if (green < 0) { green = 0; }
            if (red > 255) { red = 255; } else if (red < 0) { red = 0; }

            // return the highest rgb values
            highestRGB[0] = (byte)(blue);
            highestRGB[1] = (byte)(green);
            highestRGB[2] = (byte)(red);
            return highestRGB;
        }


        /// <summary>
        /// Rotates the given operator the given number of times and returns an array of all those
        /// rotated operators.
        /// </summary>
        /// <param name="baseKernel"></param>
        /// <param name="degrees"></param>
        /// <returns></returns>
        public static double[,,] RotateMatrix(double[,] baseKernel,
                                         double degrees) {
            double[,,] kernel = new double[(int)(360 / degrees),
                baseKernel.GetLength(0), baseKernel.GetLength(1)];

            int xOffset = baseKernel.GetLength(1) / 2;
            int yOffset = baseKernel.GetLength(0) / 2;

            for (int y = 0; y < baseKernel.GetLength(0); y++) {
                for (int x = 0; x < baseKernel.GetLength(1); x++) {
                    for (int compass = 0; compass <
                        kernel.GetLength(0); compass++) {
                        double radians = compass * degrees *
                                         Math.PI / 180.0;

                        int resultX = (int)(Math.Round((x - xOffset) *
                                   Math.Cos(radians) - (y - yOffset) *
                                   Math.Sin(radians)) + xOffset);

                        int resultY = (int)(Math.Round((x - xOffset) *
                                    Math.Sin(radians) + (y - yOffset) *
                                    Math.Cos(radians)) + yOffset);

                        kernel[compass, resultY, resultX] =
                                                    baseKernel[y, x];
                    }
                }
            }
            return kernel;
        }

        #region ThreeByThree Methods
        /// <summary>
        /// multiply (not a matrix multiply) two 3x3 matrixes
        /// put result back in a
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        private static void ThreeBythreeMultiply(int[,] a, int[,] b) {
            for (int y = 0; y < 3; y++) {
                for (int x = 0; x < 3; x++) {
                    a[x, y] = a[x, y] * b[x, y];
                }
            }
        }

        private static int ThreeBythreeSum(int[,] a) {
            int sum = 0;
            for (int y = 0; y < 3; y++) {
                for (int x = 0; x < 3; x++) {
                    sum = sum + a[x, y];
                }
            }
            return sum;
        }

        private static string ThreebythreeToString(int[,] a, string heading) {
            string t = "";
            if (heading != "") t = t + heading + "\r\n";
            for (int y = 0; y < 3; y++) {
                t = t + a[0, y].ToString() + " " + a[1, y].ToString() + " " + a[2, y].ToString() + "\r\n";
            }
            return t;
        }
        #endregion
    }
}
