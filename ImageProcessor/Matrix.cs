using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageProcessor {
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
                double[,,] baseKernel = new double[,,] { // Robert's X Operator
                                                             { { -1,  0,  1 },
                                                               { -2,  0,  2 },
                                                               { -1,  0,  1 } },
                                                             // Robert's Y Operator
                                                             { { -1, -2, -1 },
                                                               {  0,  0,  0 },
                                                               {  1,  2,  1 }, }};
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
    }
}
