using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace ImageProcessor {
    /// <summary>
    /// Compass edge detection original author:  Dewald Esterhuizen
    ///  
    /// For research use in Soft Computing - University of Canberra.
    /// 
    /// Sourced from: https://softwarebydefault.com/2013/06/22/compass-edge-detection/
    /// 
    /// </summary>
    /// <param name="baseKernel"></param>
    /// <param name="degrees"></param>
    /// <returns></returns>
    public static class ConvolutionUtils {

        public static class Matrix {

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

            public static double[,,] Prewitt5x5x4 {
                get {
                    double[,] baseKernel = new double[,] { {  -2, -1,  0,  1, 2,  },
                                                           {  -2, -1,  0,  1, 2,  },
                                                           {  -2, -1,  0,  1, 2,  },
                                                           {  -2, -1,  0,  1, 2,  },
                                                           {  -2, -1,  0,  1, 2,  }, };
                    double[,,] kernel = RotateMatrix(baseKernel, 90);
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

            public static double[,,] Sobel5x5x4 {
                get {
                    double[,] baseKernel = new double[,] { {   -5,  -4,  0,   4,  5,  },
                                                           {   -8, -10,  0,  10,  8,  },
                                                           {  -10, -20,  0,  20, 10,  },
                                                           {   -8, -10,  0,  10,  8,  },
                                                           {   -5,  -4,  0,   4,  5,  }, };
                    double[,,] kernel = RotateMatrix(baseKernel, 90);
                    return kernel;
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

            public static double[,,] Scharr5x5x4 {
                get {
                    double[,] baseKernel = new double[,] { {   -1,  -1,  0,   1,  1,  },
                                                           {   -2,  -2,  0,   2,  2,  },
                                                           {   -3,  -6,  0,   6,  3,  },
                                                           {   -2,  -2,  0,   2,  2,  },
                                                           {   -1,  -1,  0,   1,  1,  }, };
                    double[,,] kernel = RotateMatrix(baseKernel, 90);
                    return kernel;
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

        public static Bitmap ConvolutionFilter(this Bitmap sourceBitmap, double[,,] filterMatrix, double factor = 2, int bias = 0) {
            BitmapData sourceData = sourceBitmap.LockBits(new Rectangle(0, 0, sourceBitmap.Width, sourceBitmap.Height), ImageLockMode.ReadOnly,
                                                 PixelFormat.Format32bppArgb);

            byte[] pixelBuffer = new byte[sourceData.Stride * sourceData.Height];
            byte[] resultBuffer = new byte[sourceData.Stride * sourceData.Height];

            Marshal.Copy(sourceData.Scan0, pixelBuffer, 0, pixelBuffer.Length);
            sourceBitmap.UnlockBits(sourceData);

            double blue = 0.0;
            double green = 0.0;
            double red = 0.0;

            double blueCompass = 0.0;
            double greenCompass = 0.0;
            double redCompass = 0.0;

            int filterWidth = filterMatrix.GetLength(1);
            int filterHeight = filterMatrix.GetLength(0);

            int filterOffset = (filterWidth - 1) / 2;
            int calcOffset = 0;

            int byteOffset = 0;

            for (int offsetY = filterOffset; offsetY <
                sourceBitmap.Height - filterOffset; offsetY++) {
                for (int offsetX = filterOffset; offsetX <
                    sourceBitmap.Width - filterOffset; offsetX++) {
                    blue = 0;
                    green = 0;
                    red = 0;
                    byteOffset = offsetY *
                                 sourceData.Stride +
                                 offsetX * 4;

                    for (int compass = 0; compass <
                         filterMatrix.GetLength(0); compass++) {

                        blueCompass = 0.0;
                        greenCompass = 0.0;
                        redCompass = 0.0;


                        for (int filterY = -filterOffset;
                            filterY <= filterOffset; filterY++) {
                            for (int filterX = -filterOffset;
                                filterX <= filterOffset; filterX++) {
                                calcOffset = byteOffset +
                                             (filterX * 4) +
                                             (filterY * sourceData.Stride);

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

                        blue = (blueCompass > blue ? blueCompass : blue);
                        green = (greenCompass > green ? greenCompass : green);
                        red = (redCompass > red ? redCompass : red);
                    }

                    blue = factor * blue + bias;
                    green = factor * green + bias;
                    red = factor * red + bias;

                    if (blue > 255) { blue = 255; } else if (blue < 0) { blue = 0; }

                    if (green > 255) { green = 255; } else if (green < 0) { green = 0; }

                    if (red > 255) { red = 255; } else if (red < 0) { red = 0; }

                    resultBuffer[byteOffset] = (byte)(blue);
                    resultBuffer[byteOffset + 1] = (byte)(green);
                    resultBuffer[byteOffset + 2] = (byte)(red);
                    resultBuffer[byteOffset + 3] = 255;
                }
            }

            Bitmap resultBitmap = new Bitmap(sourceBitmap.Width, sourceBitmap.Height);

            BitmapData resultData = resultBitmap.LockBits(new Rectangle(0, 0, resultBitmap.Width, resultBitmap.Height),
                ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);

            Marshal.Copy(resultBuffer, 0, resultData.Scan0, resultBuffer.Length);
            resultBitmap.UnlockBits(resultData);

            return resultBitmap;
        }

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
    }
}
