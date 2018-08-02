using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OOPTools_Math
{
    /// <summary>
    /// Tools to do array operations
    /// 
    /// Mehrdad Negahban
    /// 2007
    /// Modified in organization: 2013
    /// </summary>
    [Serializable]
    public class ArrayTools
    {
        #region Methods to create an array that is negative the original
        /// <summary>
        /// Return an array with each term equal to the negative of original array: -Array1
        /// </summary>
        /// <param name="Array1"></param>
        /// <returns></returns>
        public static double[] Negative(double[] Array1)
        {
            int ArrayLength = Array1.Length;
            double[] Array3 = new double[ArrayLength];
            for (int i = 0; i < ArrayLength; i++)
            {
                Array3[i] = -Array1[i] ;
            }
            return Array3;
        }
        public static double[,] Negative(double[,] Array1)
        {
            int iLength = Array1.GetLength(0);
            int jLength = Array1.GetLength(1);
            double[,] Array3 = new double[iLength, jLength];
            for (int i = 0; i < iLength; i++)
            {
                for (int j = 0; j < jLength; j++)
                {
                    Array3[i, j] = -Array1[i, j];
                }
            }
            return Array3;
        }
        public static double[, ,] Negative(double[, ,] Array1)
        {
            int iLength = Array1.GetLength(0);
            int jLength = Array1.GetLength(1);
            int kLength = Array1.GetLength(2);
            double[, ,] Array3 = new double[iLength, jLength, kLength];
            for (int i = 0; i < iLength; i++)
            {
                for (int j = 0; j < jLength; j++)
                {
                    for (int k = 0; k < kLength; k++)
                    {
                        Array3[i, j, k] = -Array1[i, j, k] ;
                    }
                }
            }
            return Array3;
        }
        public static double[, , ,] Negative(double[, , ,] Array1)
        {
            int iLength = Array1.GetLength(0);
            int jLength = Array1.GetLength(1);
            int kLength = Array1.GetLength(2);
            int lLength = Array1.GetLength(3);
            double[, , ,] Array3 = new double[iLength, jLength, kLength, lLength];
            for (int i = 0; i < iLength; i++)
            {
                for (int j = 0; j < jLength; j++)
                {
                    for (int k = 0; k < kLength; k++)
                    {
                        for (int l = 0; l < lLength; l++)
                        {
                            Array3[i, j, k, l] = -Array1[i, j, k, l];
                        }
                    }
                }

            }
            return Array3;
        }

        public static double[][][][] Negative(double[][][][] Array1)
        {
            int iLength = Array1.GetLength(0);
            double[][][][] Array3 = new double[iLength][][][];
            for (int i = 0; i < iLength; i++)
            {
                Array3[i] = Negative(Array1[i]);
            }
            return Array3;
        }
        public static double[][][] Negative(double[][][] Array1)
        {
            int iLength = Array1.GetLength(0);
            double[][][] Array3 = new double[iLength][][];
            for (int i = 0; i < iLength; i++)
            {
                Array3[i] = Negative(Array1[i]);
            }
            return Array3;
        }
        public static double[][] Negative(double[][] Array1)
        {
            int iLength = Array1.GetLength(0);
            double[][] Array3 = new double[iLength][];
            for (int i = 0; i < iLength; i++)
            {
                Array3[i] = Negative(Array1[i]);
            }
            return Array3;
        }
        #endregion

        #region Methods to add arrays
        /// <summary>
        /// Return an array that is the addition of the two original arrays: Array1 + Array2
        /// </summary>
        /// <param name="Array1"></param>
        /// <param name="Array2"></param>
        /// <returns></returns>
        public static double[] Add(double[] Array1, double[] Array2)
        {
            int ArrayLength = Array1.Length;
            double[] Array3 = new double[ArrayLength];
            for (int i = 0; i < ArrayLength; i++)
            {
                Array3[i] = Array1[i] + Array2[i];
            }
            return Array3;
        }
        public static double[,] Add(double[,] Array1, double[,] Array2)
        {
            int iLength = Array1.GetLength(0);
            int jLength = Array1.GetLength(1);
            double[,] Array3 = new double[iLength, jLength];
            for (int i = 0; i < iLength; i++)
            {
                for (int j = 0; j < jLength; j++)
                {
                    Array3[i, j] = Array1[i, j] + Array2[i, j];
                }
            }
            return Array3;
        }
        public static double[, ,] Add(double[, ,] Array1, double[, ,] Array2)
        {
            int iLength = Array1.GetLength(0);
            int jLength = Array1.GetLength(1);
            int kLength = Array1.GetLength(2);
            double[, ,] Array3 = new double[iLength, jLength, kLength];
            for (int i = 0; i < iLength; i++)
            {
                for (int j = 0; j < jLength; j++)
                {
                    for (int k = 0; k < kLength; k++)
                    {
                        Array3[i, j, k] = Array1[i, j, k] + Array2[i, j, k];
                    }
                }
            }
            return Array3;
        }
        public static double[, , ,] Add(double[, , ,] Array1, double[, , ,] Array2)
        {
            int iLength = Array1.GetLength(0);
            int jLength = Array1.GetLength(1);
            int kLength = Array1.GetLength(2);
            int lLength = Array1.GetLength(3);
            double[, , ,] Array3 = new double[iLength, jLength, kLength, lLength];
            for (int i = 0; i < iLength; i++)
            {
                for (int j = 0; j < jLength; j++)
                {
                    for (int k = 0; k < kLength; k++)
                    {
                        for (int l = 0; l < lLength; l++)
                        {
                            Array3[i, j, k, l] = Array1[i, j, k, l] + Array2[i, j, k, l];
                        }
                    }
                }

            }
            return Array3;
        }

        public static double[][][][] Add(double[][][][] Array1, double[][][][] Array2)
        {
            int iLength = Array1.GetLength(0);
            double[][][][] Array3 = new double[iLength][][][];
            for (int i = 0; i < iLength; i++)
            {
                Array3[i] = Add(Array1[i], Array2[i]);
            }
            return Array3;
        }
        public static double[][][] Add(double[][][] Array1, double[][][] Array2)
        {
            int iLength = Array1.GetLength(0);
            double[][][] Array3 = new double[iLength][][];
            for (int i = 0; i < iLength; i++)
            {
                Array3[i] = Add(Array1[i], Array2[i]);
            }
            return Array3;
        }
        public static double[][] Add(double[][] Array1, double[][] Array2)
        {
            int iLength = Array1.GetLength(0);
            double[][] Array3 = new double[iLength][];
            for (int i = 0; i < iLength; i++)
            {
                Array3[i] = Add(Array1[i], Array2[i]);
            }
            return Array3;
        }
        #endregion

        #region Methods to subtract arrays
        /// <summary>
        /// Return an array that is the subtraction of the two arrays: Array1 - Array2
        /// </summary>
        /// <param name="Array1"></param>
        /// <param name="Array2"></param>
        /// <returns></returns>
        public static double[] Subtract(double[] Array1, double[] Array2)
        {
            int ArrayLength = Array1.Length;
            double[] Array3 = new double[ArrayLength];
            for (int i = 0; i < ArrayLength; i++)
            {
                Array3[i] = Array1[i] - Array2[i];
            }
            return Array3;
        }
        public static double[,] Subtract(double[,] Array1, double[,] Array2)
        {
            int iLength = Array1.GetLength(0);
            int jLength = Array1.GetLength(1);
            double[,] Array3 = new double[iLength, jLength];
            for (int i = 0; i < iLength; i++)
            {
                for (int j = 0; j < jLength; j++)
                {
                    Array3[i, j] = Array1[i, j] - Array2[i, j];
                }
            }
            return Array3;
        }
        public static double[, ,] Subtract(double[, ,] Array1, double[, ,] Array2)
        {
            int iLength = Array1.GetLength(0);
            int jLength = Array1.GetLength(1);
            int kLength = Array1.GetLength(2);
            double[, ,] Array3 = new double[iLength, jLength, kLength];
            for (int i = 0; i < iLength; i++)
            {
                for (int j = 0; j < jLength; j++)
                {
                    for (int k = 0; k < kLength; k++)
                    {
                        Array3[i, j, k] = Array1[i, j, k] - Array2[i, j, k];
                    }
                }
            }
            return Array3;
        }
        public static double[, , ,] Subtract(double[, , ,] Array1, double[, , ,] Array2)
        {
            int iLength = Array1.GetLength(0);
            int jLength = Array1.GetLength(1);
            int kLength = Array1.GetLength(2);
            int lLength = Array1.GetLength(3);
            double[, , ,] Array3 = new double[iLength, jLength, kLength, lLength];
            for (int i = 0; i < iLength; i++)
            {
                for (int j = 0; j < jLength; j++)
                {
                    for (int k = 0; k < kLength; k++)
                    {
                        for (int l = 0; l < lLength; l++)
                        {
                            Array3[i, j, k, l] = Array1[i, j, k, l] - Array2[i, j, k, l];
                        }
                    }
                }

            }
            return Array3;
        }

        public static double[][][][] Subtract(double[][][][] Array1, double[][][][] Array2)
        {
            int iLength = Array1.GetLength(0);
            double[][][][] Array3 = new double[iLength][][][];
            for (int i = 0; i < iLength; i++)
            {
                Array3[i] = Subtract(Array1[i], Array2[i]);
            }
            return Array3;
        }
        public static double[][][] Subtract(double[][][] Array1, double[][][] Array2)
        {
            int iLength = Array1.GetLength(0);
            double[][][] Array3 = new double[iLength][][];
            for (int i = 0; i < iLength; i++)
            {
                Array3[i] = Subtract(Array1[i], Array2[i]);
            }
            return Array3;
        }
        public static double[][] Subtract(double[][] Array1, double[][] Array2)
        {
            int iLength = Array1.GetLength(0);
            double[][] Array3 = new double[iLength][];
            for (int i = 0; i < iLength; i++)
            {
                Array3[i] = Subtract(Array1[i], Array2[i]);
            }
            return Array3;
        }
        #endregion

        #region Methods to multiply arrays
        /// <summary>
        /// Return an array that is term by term multiplication of the array elements
        /// </summary>
        /// <param name="Array1"></param>
        /// <param name="Array2"></param>
        /// <returns></returns>
        public static double[] Multiply(double[] Array1, double[] Array2)
        {
            int ArrayLength = Array1.Length;
            double[] Array3 = new double[ArrayLength];
            for (int i = 0; i < ArrayLength; i++)
            {
                Array3[i] = Array1[i] *Array2[i];
            }
            return Array3;
        }
        public static double[,] Multiply(double[,] Array1, double[,] Array2)
        {
            int iLength = Array1.GetLength(0);
            int jLength = Array1.GetLength(1);
            double[,] Array3 = new double[iLength, jLength];
            for (int i = 0; i < iLength; i++)
            {
                for (int j = 0; j < jLength; j++)
                {
                    Array3[i, j] = Array1[i, j] * Array2[i, j];
                }
            }
            return Array3;
        }
        public static double[, ,] Multiply(double[, ,] Array1, double[, ,] Array2)
        {
            int iLength = Array1.GetLength(0);
            int jLength = Array1.GetLength(1);
            int kLength = Array1.GetLength(2);
            double[, ,] Array3 = new double[iLength, jLength, kLength];
            for (int i = 0; i < iLength; i++)
            {
                for (int j = 0; j < jLength; j++)
                {
                    for (int k = 0; k < kLength; k++)
                    {
                        Array3[i, j, k] = Array1[i, j, k] * Array2[i, j, k];
                    }
                }
            }
            return Array3;
        }
        public static double[, , ,] Multiply(double[, , ,] Array1, double[, , ,] Array2)
        {
            int iLength = Array1.GetLength(0);
            int jLength = Array1.GetLength(1);
            int kLength = Array1.GetLength(2);
            int lLength = Array1.GetLength(3);
            double[, , ,] Array3 = new double[iLength, jLength, kLength, lLength];
            for (int i = 0; i < iLength; i++)
            {
                for (int j = 0; j < jLength; j++)
                {
                    for (int k = 0; k < kLength; k++)
                    {
                        for (int l = 0; l < lLength; l++)
                        {
                            Array3[i, j, k, l] = Array1[i, j, k, l] * Array2[i, j, k, l];
                        }
                    }
                }

            }
            return Array3;
        }

        public static double[][][][] Multiply(double[][][][] Array1, double[][][][] Array2)
        {
            int iLength = Array1.GetLength(0);
            double[][][][] Array3 = new double[iLength][][][];
            for (int i = 0; i < iLength; i++)
            {
                Array3[i] = Multiply(Array1[i], Array2[i]);
            }
            return Array3;
        }
        public static double[][][] Multiply(double[][][] Array1, double[][][] Array2)
        {
            int iLength = Array1.GetLength(0);
            double[][][] Array3 = new double[iLength][][];
            for (int i = 0; i < iLength; i++)
            {
                Array3[i] = Multiply(Array1[i], Array2[i]);
            }
            return Array3;
        }
        public static double[][] Multiply(double[][] Array1, double[][] Array2)
        {
            int iLength = Array1.GetLength(0);
            double[][] Array3 = new double[iLength][];
            for (int i = 0; i < iLength; i++)
            {
                Array3[i] = Multiply(Array1[i], Array2[i]);
            }
            return Array3;
        }
        #endregion

        #region Methods to divide arrays
        /// <summary>
        /// Return an array that is the division, term by term, of the original array elements
        /// </summary>
        /// <param name="Array1"></param>
        /// <param name="Array2"></param>
        /// <returns></returns>
        public static double[] Divide(double[] Array1, double[] Array2)
        {
            int ArrayLength = Array1.Length;
            double[] Array3 = new double[ArrayLength];
            for (int i = 0; i < ArrayLength; i++)
            {
                Array3[i] = Array1[i] / Array2[i];
            }
            return Array3;
        }
        public static double[,] Divide(double[,] Array1, double[,] Array2)
        {
            int iLength = Array1.GetLength(0);
            int jLength = Array1.GetLength(1);
            double[,] Array3 = new double[iLength, jLength];
            for (int i = 0; i < iLength; i++)
            {
                for (int j = 0; j < jLength; j++)
                {
                    Array3[i, j] = Array1[i, j] / Array2[i, j];
                }
            }
            return Array3;
        }
        public static double[, ,] Divide(double[, ,] Array1, double[, ,] Array2)
        {
            int iLength = Array1.GetLength(0);
            int jLength = Array1.GetLength(1);
            int kLength = Array1.GetLength(2);
            double[, ,] Array3 = new double[iLength, jLength, kLength];
            for (int i = 0; i < iLength; i++)
            {
                for (int j = 0; j < jLength; j++)
                {
                    for (int k = 0; k < kLength; k++)
                    {
                        Array3[i, j, k] = Array1[i, j, k] / Array2[i, j, k];
                    }
                }
            }
            return Array3;
        }
        public static double[, , ,] Divide(double[, , ,] Array1, double[, , ,] Array2)
        {
            int iLength = Array1.GetLength(0);
            int jLength = Array1.GetLength(1);
            int kLength = Array1.GetLength(2);
            int lLength = Array1.GetLength(3);
            double[, , ,] Array3 = new double[iLength, jLength, kLength, lLength];
            for (int i = 0; i < iLength; i++)
            {
                for (int j = 0; j < jLength; j++)
                {
                    for (int k = 0; k < kLength; k++)
                    {
                        for (int l = 0; l < lLength; l++)
                        {
                            Array3[i, j, k, l] = Array3[i, j, k, l] / Array3[i, j, k, l];
                        }
                    }
                }

            }
            return Array3;
        }

        public static double[][][][] Divide(double[][][][] Array1, double[][][][] Array2)
        {
            int iLength = Array1.GetLength(0);
            double[][][][] Array3 = new double[iLength][][][];
            for (int i = 0; i < iLength; i++)
            {
                Array3[i] = Divide(Array1[i], Array2[i]);
            }
            return Array3;
        }
        public static double[][][] Divide(double[][][] Array1, double[][][] Array2)
        {
            int iLength = Array1.GetLength(0);
            double[][][] Array3 = new double[iLength][][];
            for (int i = 0; i < iLength; i++)
            {
                Array3[i] = Divide(Array1[i], Array2[i]);
            }
            return Array3;
        }
        public static double[][] Divide(double[][] Array1, double[][] Array2)
        {
            int iLength = Array1.GetLength(0);
            double[][] Array3 = new double[iLength][];
            for (int i = 0; i < iLength; i++)
            {
                Array3[i] = Divide(Array1[i], Array2[i]);
            }
            return Array3;
        }
        #endregion

        #region Methods to multiply by scalar and add arrays
        /// <summary>
        /// Return an array that each element is the scalar multipied by the scalar
        /// </summary>
        /// <param name="Array"></param>
        /// <param name="scalar"></param>
        /// <returns></returns>
        public static double[] Multiply(double[] Array1, double scalar)
        {
            int ArrayLength = Array1.Length;
            double[] Array2 = new double[ArrayLength];
            for (int i = 0; i < ArrayLength; i++)
            {
                Array2[i] = scalar * Array1[i];
            }
            return Array2;
        }
        public static double[,] Multiply(double[,] Array1, double scalar)
        {
            int iLength = Array1.GetLength(0);
            int jLength = Array1.GetLength(1);
            double[,] Array2 = new double[iLength,jLength];
            for (int i = 0; i < iLength; i++)
            {
                for (int j = 0; j < jLength; j++)
                {
                    Array2[i, j] = scalar * Array1[i,j];
                }
            }
            return Array2;
        }
        public static double[, ,] Multiply(double[, ,] Array1, double scalar)
        {
            int iLength = Array1.GetLength(0);
            int jLength = Array1.GetLength(1);
            int kLength = Array1.GetLength(2);
            double[,,] Array2 = new double[iLength, jLength, kLength];
            for (int i = 0; i < iLength; i++)
            {
                for (int j = 0; j < jLength; j++)
                {
                    for (int k = 0; k < kLength; k++)
                    {
                        Array2[i, j, k] = scalar* Array1[i,j,k];
                    }
                }
            }
            return Array2;
        }
        public static double[, , ,] Multiply(double[, , ,] Array1, double scalar)
        {
            int iLength = Array1.GetLength(0);
            int jLength = Array1.GetLength(1);
            int kLength = Array1.GetLength(2);
            int lLength = Array1.GetLength(3);
            double[, ,,] Array2 = new double[iLength, jLength, kLength, lLength];
            for (int i = 0; i < iLength; i++)
            {
                for (int j = 0; j < jLength; j++)
                {
                    for (int k = 0; k < kLength; k++)
                    {
                        for (int l = 0; l < lLength; l++)
                        {
                            Array2[i, j, k, l] = scalar * Array1[i, j, k, l];
                        }
                    }
                }

            }
            return Array2;
        }
        public static double[][][][] Multiply(double[][][][] Array, double scalar)
        {
            int iLength = Array.GetLength(0);
            for (int i = 0; i < iLength; i++)
            {
                Array[i] = Multiply(Array[i], scalar);
            }
            return Array;
        }
        public static double[][][] Multiply(double[][][] Array, double scalar)
        {
            int iLength = Array.GetLength(0);
            for (int i = 0; i < iLength; i++)
            {
                Array[i] = Multiply(Array[i], scalar);
            }
            return Array;
        }
        public static double[][] Multiply(double[][] Array, double scalar)
        {
            int iLength = Array.GetLength(0);
            for (int i = 0; i < iLength; i++)
            {
                Array[i] = Multiply(Array[i], scalar);
            }
            return Array;
        }


        public static double[] Multiply(double scalar, double[] Array)
        {
            return Multiply(Array, scalar);
        }
        public static double[,] Multiply(double scalar, double[,] Array)
        {
            return Multiply(Array, scalar);
        }
        public static double[, ,] Multiply(double scalar, double[, ,] Array)
        {
            return Multiply(Array, scalar);
        }
        public static double[, , ,] Multiply(double scalar, double[, , ,] Array)
        {
            return Multiply(Array, scalar);
        }
        public static double[][][][] Multiply(double scalar, double[][][][] Array)
        {
            return Multiply(Array, scalar);
        }
        public static double[][][] Multiply(double scalar, double[][][] Array)
        {
            return Multiply(Array, scalar); 
        }
        public static double[][] Multiply(double scalar, double[][] Array)
        {
            return Multiply(Array, scalar);
        }


        public static double[] MultiplyScalarAndAdd(double[] Array, double scalar, double[] ArrayToAddTo)
        {
            for (int i = 0; i < Array.Length; i++)
            {
                Array[i] *= scalar;
                ArrayToAddTo[i] += Array[i];
            }
            return ArrayToAddTo;
        }
        public static double[,] MultiplyScalarAndAdd(double[,] Array, double scalar, double[,] ArrayToAddTo)
        {
            int iLength = Array.GetLength(0);
            int jLength = Array.GetLength(1);
            for (int i = 0; i < iLength; i++)
            {
                for (int j = 0; j < jLength; j++)
                {
                    Array[i, j] *= scalar;
                    ArrayToAddTo[i, j] += Array[i, j];
                }
            }
            return ArrayToAddTo;
        }
        public static double[, ,] MultiplyScalarAndAdd(double[, ,] Array, double scalar, double[, ,] ArrayToAddTo)
        {
            int iLength = Array.GetLength(0);
            int jLength = Array.GetLength(1);
            int kLength = Array.GetLength(2);
            for (int i = 0; i < iLength; i++)
            {
                for (int j = 0; j < jLength; j++)
                {
                    for (int k = 0; k < kLength; k++)
                    {
                        Array[i, j, k] *= scalar;
                        ArrayToAddTo[i, j, k] += Array[i, j, k];
                    }
                }
            }
            return ArrayToAddTo;
        }
        public static double[, , ,] MultiplyScalarAndAdd(double[, , ,] Array, double scalar, double[, , ,] ArrayToAddTo)
        {
            int iLength = Array.GetLength(0);
            int jLength = Array.GetLength(1);
            int kLength = Array.GetLength(2);
            int lLength = Array.GetLength(3);
            for (int i = 0; i < iLength; i++)
            {
                for (int j = 0; j < jLength; j++)
                {
                    for (int k = 0; k < kLength; k++)
                    {
                        for (int l = 0; l < lLength; l++)
                        {
                            Array[i, j, k, l] *= scalar;
                            ArrayToAddTo[i, j, k, l] += Array[i, j, k, l];
                        }
                    }
                }

            }
            return ArrayToAddTo;
        }
        public static double[][][][] MultiplyScalarAndAdd(double[][][][] Array, double scalar, double[][][][] ArrayToAddTo)
        {
            int iLength = Array.GetLength(0);
            for (int i = 0; i < iLength; i++)
            {
                int jLength = Array[i].GetLength(0);
                for (int j = 0; j < jLength; j++)
                {
                    int kLength = Array[i][j].GetLength(0);
                    for (int k = 0; k < kLength; k++)
                    {
                        int lLength = Array[i][j][k].GetLength(0);
                        for (int l = 0; l < lLength; l++)
                        {
                            Array[i][j][k][l] *= scalar;
                            ArrayToAddTo[i][j][k][l] += Array[i][j][k][l];
                        }
                    }
                }

            }
            return ArrayToAddTo;
        }
        public static double[][][] MultiplyScalarAndAdd(double[][][] Array, double scalar, double[][][] ArrayToAddTo)
        {
            int iLength = Array.GetLength(0);
            for (int i = 0; i < iLength; i++)
            {
                int jLength = Array[i].GetLength(0);
                for (int j = 0; j < jLength; j++)
                {
                    int kLength = Array[i][j].GetLength(0);
                    for (int k = 0; k < kLength; k++)
                    {
                        Array[i][j][k] *= scalar;
                        ArrayToAddTo[i][j][k] += Array[i][j][k];
                    }
                }

            }
            return ArrayToAddTo;
        }
        public static double[][] MultiplyScalarAndAdd(double[][] Array, double scalar, double[][] ArrayToAddTo)
        {
            int iLength = Array.GetLength(0);
            for (int i = 0; i < iLength; i++)
            {
                int jLength = Array[i].GetLength(0);
                for (int j = 0; j < jLength; j++)
                {
                    Array[i][j] *= scalar;
                    ArrayToAddTo[i][j] += Array[i][j];
                }

            }
            return ArrayToAddTo;
        }
        
        #endregion

        #region Methods for statistical evaluations

        /// <summary>
        /// Find and return the maximum value in the array
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        public static double Max(double[] a)
        {
            int n = a.Length;
            double max = a[0];
            for (int i = 1; i < n; i++)
            {
                if (a[i] > max) max = a[i];
            }
            return max;
        }
        public static double Max(double[][] a)
        {
            int n = a.GetLength(0);
            double max = Max(a[0]);
            for (int i = 1; i < n; i++)
            {
                double value = Max(a[i]);
                if (value > max) max = value;
            }
            return max;
        }
        public static double Max(double[][][] a)
        {
            int n = a.GetLength(0);
            double max = Max(a[0]);
            for (int i = 1; i < n; i++)
            {
                double value = Max(a[i]);
                if (value > max) max = value;
            }
            return max;
        }
        public static double Max(double[][][][] a)
        {
            int n = a.GetLength(0);
            double max = Max(a[0]);
            for (int i = 1; i < n; i++)
            {
                double value = Max(a[i]);
                if (value > max) max = value;
            }
            return max;
        }
        public static double Max(double[][][][][] a)
        {
            int n = a.GetLength(0);
            double max = Max(a[0]);
            for (int i = 1; i < n; i++)
            {
                double value = Max(a[i]);
                if (value > max) max = value;
            }
            return max;
        }
        public static double Max(double[,] a)
        {
            int m = a.GetLength(0);
            int n = a.GetLength(1);
            double max = a[0, 0];
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (a[i, j] > max) max = a[i, j];
                }
            }
            return max;
        }
        public static int Max(int[] a)
        {
            int n = a.Length;
            int max = a[0];
            for (int i = 1; i < n; i++)
            {
                if (a[i] > max) max = a[i];
            }
            return max;
        }
        public static int Max(int[,] a)
        {
            int m = a.GetLength(0);
            int n = a.GetLength(1);
            int max = a[0, 0];
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (a[i, j] > max) max = a[i, j];
                }
            }
            return max;
        }

        /// <summary>
        /// Find and return the minimum value of the array
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        public static double Min(double[] a)
        {
            int n = a.Length;
            double min = a[0];
            for (int i = 1; i < n; i++)
            {
                if (a[i] < min) min = a[i];
            }
            return min;
        }
        public static double Min(double[][] a)
        {
            int n = a.GetLength(0);
            double min = Min(a[0]);
            for (int i = 1; i < n; i++)
            {
                double value = Min(a[i]);
                if (value < min) min = value;
            }
            return min;
        }
        public static double Min(double[][][] a)
        {
            int n = a.GetLength(0);
            double min = Min(a[0]);
            for (int i = 1; i < n; i++)
            {
                double value = Min(a[i]);
                if (value < min) min = value;
            }
            return min;
        }
        public static double Min(double[][][][] a)
        {
            int n = a.GetLength(0);
            double min = Min(a[0]);
            for (int i = 1; i < n; i++)
            {
                double value = Min(a[i]);
                if (value < min) min = value;
            }
            return min;
        }
        public static double Min(double[][][][][] a)
        {
            int n = a.GetLength(0);
            double min = Min(a[0]);
            for (int i = 1; i < n; i++)
            {
                double value = Min(a[i]);
                if (value < min) min = value;
            }
            return min;
        }
        public static double Min(double[,] a)
        {
            int m = a.GetLength(0);
            int n = a.GetLength(1);
            double min = a[0, 0];
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (a[i, j] < min) min = a[i, j];
                }
            }
            return min;
        }
        public static int Min(int[] a)
        {
            int n = a.Length;
            int min = a[0];
            for (int i = 1; i < n; i++)
            {
                if (a[i] < min) min = a[i];
            }
            return min;
        }
        public static int Min(int[,] a)
        {
            int m = a.GetLength(0);
            int n = a.GetLength(1);
            int min = a[0, 0];
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (a[i, j] < min) min = a[i, j];
                }
            }
            return min;
        }

        #endregion

        #region methods to calculate average
        /// <summary>
        /// Calculate the average of the elements in the array
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        public static double Average(double[] a)
        {
            int n = a.Length;
            double sum = a[0];
            for (int i = 1; i < n; i++)
            {
                sum += a[i];
            }
            return sum / Convert.ToDouble(n);
        }
        public static double[] Average(double[][] a)
        {
            int n = a.GetLength(0);
            int m = a[0].GetLength(0);
            double[] sum = new double[m];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < sum.Length; j++)
                {
                    sum[j] += a[i][j];
                }
            }
            double realn = Convert.ToDouble(n);
            for (int j = 0; j < m; j++)
            {
                sum[j] /= realn;
            }
            return sum;
        }
        #endregion

        #region methods to sort an array
        /// <summary>
        /// Return an array that sorts the elements large to small
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        public static double[] SortDown(double[] a)
        {
            int n = a.Length;
            for (int i = 0; i < n - 1; i++)
            {
                int maxIndex = IndexOfMax(a, i, n);
                SwapValues(ref a, i, maxIndex);
            }
            return a;
        }
        /// <summary>
        /// Return an array that organizes the elements of the array from small to large
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        public static double[] SortUp(double[] a)
        {
            int n = a.Length;
            for (int i = 0; i < n - 1; i++)
            {
                int minIndex = IndexOfMin(a, i, n);
                SwapValues(ref a, i, minIndex);
            }
            return a;
        }
        /// <summary>
        /// Return the index of the maximum value in the array
        /// </summary>
        /// <param name="a"></param>
        /// <param name="startIndex"></param>
        /// <param name="endIndex"></param>
        /// <returns></returns>
        private static int IndexOfMax(double[] a, int startIndex, int endIndex)
        {
            int MaxIndex = startIndex;
            double max = a[startIndex];
            for (int i = startIndex + 1; i < endIndex + 1; i++)
            {
                if (a[i] > max)
                {
                    max = a[i];
                    MaxIndex = i;
                }
            }
            return MaxIndex;
        }
        /// <summary>
        /// Return the index of the minimum value in the array
        /// </summary>
        /// <param name="a"></param>
        /// <param name="startIndex"></param>
        /// <param name="endIndex"></param>
        /// <returns></returns>
        private static int IndexOfMin(double[] a, int startIndex, int endIndex)
        {
            int MinIndex = startIndex;
            double min = a[startIndex];
            for (int i = startIndex + 1; i < endIndex + 1; i++)
            {
                if (a[i] < min)
                {
                    min = a[i];
                    MinIndex = i;
                }
            }
            return MinIndex;
        }
        /// <summary>
        /// Swap the elements in the array
        /// </summary>
        /// <param name="a"></param>
        /// <param name="index1"></param>
        /// <param name="index2"></param>
        private static void SwapValues(ref double[] a, int index1, int index2)
        {
            if (!(index1 == index2))
            {
                double temp = a[index1];
                a[index1] = a[index2];
                a[index2] = temp;
            }
        }
        /// <summary>
        /// Get an array that provides the order of the elements assending in array
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        public static int[] GetArrayContainingOrderOfElementAssending(double[] a)
        {
            int NE = a.GetLength(0);
            int[] Order = new int[NE];
            double[] Value = new double[NE];
            for (int i = 0; i < NE; i++)
            {
                Order[i] = i;
                Value[i] = a[i];
                if (i > 0)
                {
                    for (int j = 0; j < i; j++)
                    {
                        if (j == 0)
                        {
                            if ((Value[i] < Value[j]))
                            {
                                ShiftOrderArrayValuDownAtIndex(Order, Value, j, i - 1);
                                Order[j] = i;
                                Value[j] = a[i];
                            }
                            else if ((Value[i] >= Value[j]) && (Value[i] < Value[j + 1]))
                            {
                                ShiftOrderArrayValuDownAtIndex(Order, Value, j + 1, i - 1);
                                Order[j + 1] = i;
                                Value[j + 1] = a[i];
                            }
                        }
                        else
                        {
                            if ((Value[i] >= Value[j]) && (Value[i] < Value[j + 1]))
                            {
                                ShiftOrderArrayValuDownAtIndex(Order, Value, j + 1, i - 1);
                                Order[j + 1] = i;
                                Value[j + 1] = a[i];
                            }
                        }
                    }
                }
            }
            return Order;
        }
        /// <summary>
        /// Shift order array down at index
        /// </summary>
        /// <param name="Order"></param>
        /// <param name="Values"></param>
        /// <param name="ShiftStartIndex"></param>
        /// <param name="LastIndexToShift"></param>
        private static void ShiftOrderArrayValuDownAtIndex(int[] Order, double[] Values, int ShiftStartIndex, int LastIndexToShift)
        {
            for (int i = LastIndexToShift; i > ShiftStartIndex - 1; i--)
            {
                Order[i + 1] = Order[i];
                Values[i + 1] = Values[i];
            }
        }
        /// <summary>
        /// Find index item index in array using Secant Method
        /// </summary>
        /// <param name="array"></param>
        /// <param name="value"></param>
        /// <param name="startAt"></param>
        /// <param name="endAt"></param>
        /// <returns></returns>
        static public int FindIndexInArryBySecantMethod(int[] array, int value, int startAt, int endAt)
        {
            int i1 = startAt;
            int i2 = endAt;
            int i3;
            int nnn = endAt - startAt;
            int startVal = array[startAt];
            int endVal = array[endAt];
            int midVal;
            int nn;
            if (array[i1] == value)
            {
                return i1;
            }
            else if (array[i2] == value)
            {
                return i2;
            }
            else
            {
                for (int i = 0; i < nnn; i++)
                {
                    nn = i2 - i1;
                    if (nn > 7)
                    {
                        startVal = array[i1];
                        endVal = array[i2];
                        int step = (nn * (value - startVal)) / (endVal - startVal);
                        if (step < 1) step = 1;

                        i3 = i1 + step;
                        midVal = array[i3];
                        if (midVal == value)
                        {
                            return i3;
                        }
                        else if (midVal < value)
                        {
                            i1 = i3;
                        }
                        else
                        {
                            i2 = i3;
                        }

                    }
                    else
                    {

                        for (int j = i1 + 1; j < i2; j++)
                        {
                            if (array[j] == value)
                            {
                                return j;
                            }
                        }
                    }

                }
            }
            return -1;
        }
        #endregion 
                
        #region Core vector operations on arrays
        /// <summary>
        /// Calculate vector cross product (3D vectors)
        /// </summary>
        /// <param name="X1"></param>
        /// <param name="X2"></param>
        /// <returns></returns>
        public static double[] Vector_CrossProduct(double[] X1, double[] X2)
        {   // Cross product between 3D vectors X1xX2
            double[] x = new double[3];
            x[0] = X1[1] * X2[2] - X1[2] * X2[1];
            x[1] = -(X1[0] * X2[2] - X1[2] * X2[0]);
            x[2] = X1[0] * X2[1] - X1[1] * X2[0];
            return x;
        }
        /// <summary>
        /// Calculate vector dot product (N-dimensional)
        /// </summary>
        /// <param name="X1"></param>
        /// <param name="X2"></param>
        /// <returns></returns>
        public static double Vector_DotProduct(double[] X1, double[] X2)
        {   // Dot product between n-D vectors
            double value = 0.0d;
            for (int i = 0; i < X1.Length; i++)
            {
                value += X1[i] * X2[i];
            }
            return value;
        }
        /// <summary>
        /// Scale array elements by given scalar
        /// </summary>
        /// <param name="ScaleFactor"></param>
        /// <param name="X"></param>
        /// <returns></returns>
        public static double[] Vector_ScaleVector(double ScaleFactor, double[] X)
        {   // Scale components of n-D vector
            double[] value = new double[X.Length];
            for (int i = 0; i < X.Length; i++)
            {
                value[i] = ScaleFactor * X[i];
            }
            return value;
        }
        /// <summary>
        /// Calculate magnitude of array (N-dimensional): Sqrt[Sum(x_i^2)]
        /// </summary>
        /// <param name="X"></param>
        /// <returns></returns>
        public static double Vector_Magnitude(double[] X)
        {   // Magnitude of  n-D vector
            return Math.Sqrt(Vector_DotProduct(X, X));
        }
        /// <summary>
        /// Made a unit vector from the array (N-dimensional)
        /// </summary>
        /// <param name="X"></param>
        /// <returns></returns>
        public static double[] Vector_MakeUnitVector(double[] X)
        {   // Made unit vector from  n-D vector
            double scale = 1.0d / Vector_Magnitude(X);
            return Vector_ScaleVector(scale, X);
        }
        /// <summary>
        /// Scale the array by the given scale
        /// </summary>
        /// <param name="Vector"></param>
        /// <param name="Scale"></param>
        /// <returns></returns>
        public static double[] Vector_ScaleVector(double[] Vector, double Scale)
        {

            int NumberOfDimensions = Vector.Length;
            double[] Value = new double[NumberOfDimensions];
            for (int i = 0; i < NumberOfDimensions; i++)
            {
                Value[i] = Vector[i] * Scale;
            }
            return Value;
        }
        #endregion

        #region Core matrix operations on arrays
/// <summary>
/// Multiply two arrays with matrix multiplication and return an array (must be rectangular and of correct dimensions)
/// </summary>
/// <param name="Matrix1"></param>
/// <param name="Matrix2"></param>
/// <returns></returns>
        public static double[,] MatrixMultiply(double[,] Matrix1, double[,] Matrix2)
        {
            int range = Matrix1.GetLength(1);
            if (range == Matrix2.GetLength(0))
            {
                int rows = Matrix1.GetLength(0);
                int columns = Matrix2.GetLength(1);
                double[,] Matrix3 = new double[rows, columns];
                for (int i = 0; i < rows; i++)
                {
                    for (int j = 0; j < columns; j++)
                    {
                        for (int k = 0; k < range; k++)
                        {
                            Matrix3[i, j] += Matrix1[i, k] * Matrix2[k, j];
                        }
                    }
                }
                return Matrix3;
            }
            else
            {
                return Null_2d();
            }

        }
        public static double[][] MatrixMultiply(double[][] Matrix1, double[][] Matrix2)
        {
            int range = Matrix1[0].Length;
            if (range == Matrix2.GetLength(0))
            {
                int rows = Matrix1.GetLength(0);
                int columns = Matrix2[0].Length;
                double[][] Matrix3 = new double[rows][];
                for (int i = 0; i < rows; i++)
                {
                    Matrix3[i] = new double[columns];
                    for (int j = 0; j < columns; j++)
                    {
                        for (int k = 0; k < range; k++)
                        {
                            Matrix3[i][j] += Matrix1[i][k] * Matrix2[k][j];
                        }
                    }
                }
                return Matrix3;
            }
            else
            {
                return Null_2d_Jagged();
            }
        }
        public static double[] MatrixMultiply(double[,] Matrix1, double[] Matrix2)
        {
            int range = Matrix1.GetLength(1);
            if (range == Matrix2.Length)
            {
                int rows = Matrix1.GetLength(0);
                double[] Matrix3 = new double[rows];
                for (int i = 0; i < rows; i++)
                {
                    for (int k = 0; k < range; k++)
                    {
                        Matrix3[i] += Matrix1[i, k] * Matrix2[k];
                    }
                }
                return Matrix3;
            }
            else
            {
                return Null_1d();
            }
        }
        public static double[] MatrixMultiply(double[][] Matrix1, double[] Matrix2)
        {
            int range = Matrix1[0].Length;
            if (range == Matrix2.Length)
            {
                int rows = Matrix1.GetLength(0);
                double[] Matrix3 = new double[rows];
                for (int i = 0; i < rows; i++)
                {
                    Matrix3[i] = Vector_DotProduct(Matrix1[i], Matrix2);
                }
                return Matrix3;
            }
            else
            {
                return Null_1d();
            }
        }
        public static double[] MatrixMultiply(double[] Matrix1, double[,] Matrix2)
        {
            // Assumes row Matrix1
            int range = Matrix2.Length;
            if (range == Matrix2.GetLength(0))
            {
                int columns = Matrix2.GetLength(1);
                double[] Matrix3 = new double[columns];
                for (int i = 0; i < columns; i++)
                {
                    for (int k = 0; k < range; k++)
                    {
                        Matrix3[i] += Matrix1[k] * Matrix2[k,i];
                    }
                }
                return Matrix3;
            }
            else
            {
                return Null_1d();
            }
        }
        public static double[] MatrixMultiply(double[] Matrix1, double[][] Matrix2)
        {
            // Assumes row Matrix1
            int range = Matrix1.Length;
            if (range == Matrix2.GetLength(0))
            {
                int columns = Matrix2[0].Length;
                double[] Matrix3 = new double[columns];
                double scale;
                double[] vector1;
                for (int k = 0; k < range; k++)
                {
                    scale = Matrix1[k];
                    vector1 = Matrix2[k];
                    for (int i = 0; i < columns; i++)
                    {
                        Matrix3[i] += scale * vector1[i];
                    }
                }
                return Matrix3;
            }
            else
            {
                return Null_1d();
            }
        }

        #endregion 

        #region Core tensor operations on arrays
        /// <summary>
        /// Multiply tensors by putting a dot product by using a dot product between close bases
        /// </summary>
        /// <param name="Tensor1"></param>
        /// <param name="Tensor2"></param>
        /// <returns></returns>
        public static double TensorMultiply(double[] Tensor1, double[] Tensor2)
        {
            return Vector_DotProduct(Tensor1, Tensor2);
        }
        public static double[] TensorMultiply(double[,] Tensor1, double[] Tensor2)
        {
            return MatrixMultiply(Tensor1, Tensor2);
        }
        public static double[] TensorMultiply(double[][] Tensor1, double[] Tensor2)
        {
            return MatrixMultiply(Tensor1, Tensor2);
        }
        public static double[] TensorMultiply(double[] Tensor1, double[,] Tensor2)
        {
            return MatrixMultiply(Tensor1, Tensor2);
        }
        public static double[] TensorMultiply(double[] Tensor1, double[][] Tensor2)
        {
            return MatrixMultiply(Tensor1, Tensor2);
        }
        public static double[,] TensorMultiply(double[,,] Tensor1, double[] Tensor2)
        {
            int range = Tensor1.GetLength(2);
            if (range == Tensor2.Length)
            {
                int I1Max = Tensor1.GetLength(0);
                int I2Max = Tensor1.GetLength(1);
                double[,] Tensor3 = new double[I1Max,I2Max];
                for (int i = 0; i < I1Max; i++)
                {
                    for (int j = 0; j < I2Max; j++)
                    {
                        for (int k = 0; k < range; k++)
                        {
                            Tensor3[i,j] += Tensor1[i, j, k] * Tensor2[k];
                        }
                    }
                }
                return Tensor3;
            }
            else
            {
                return Null_2d();
            }
        }
        public static double[][] TensorMultiply(double[][][] Tensor1, double[] Tensor2)
        {
            int range = Tensor1[0][0].Length;
            if (range == Tensor2.Length)
            {
                int I1Max = Tensor1.GetLength(0);
                double[][] Tensor3 = new double[I1Max][];
                for (int i = 0; i < I1Max; i++)
                {
                    Tensor3[i] = TensorMultiply(Tensor1[i], Tensor2);
                }
                return Tensor3;
            }
            else
            {
                return Null_2d_Jagged();
            }
        }
        public static double[, ,] TensorMultiply(double[, , ,] Tensor1, double[] Tensor2)
        {
            int range = Tensor1.GetLength(3);
            if (range == Tensor2.Length)
            {
                int I1Max = Tensor1.GetLength(0);
                int I2Max = Tensor1.GetLength(1);
                int I3Max = Tensor1.GetLength(2);
                double[,,] Tensor3 = new double[I1Max, I2Max, I3Max];
                for (int i = 0; i < I1Max; i++)
                {
                    for (int j = 0; j < I2Max; j++)
                    {
                        for (int k = 0; k < range; k++)
                        {
                            for (int l = 0; l < range; l++)
                            {
                                Tensor3[i, j, k] += Tensor1[i, j, k, l] * Tensor2[l];
                            }
                        }
                    }
                }
                return Tensor3;
            }
            else
            {
                return Null_3d();
            }
        }
        public static double[][][] TensorMultiply(double[][][][] Tensor1, double[] Tensor2)
        {
            int range = Tensor1[0][0][0].Length;
            if (range == Tensor2.Length)
            {
                int I1Max = Tensor1.GetLength(0);
                double[][][] Tensor3 = new double[I1Max][][];
                for (int i = 0; i < I1Max; i++)
                {
                    Tensor3[i] = TensorMultiply(Tensor1[i], Tensor2);
                }
                return Tensor3;
            }
            else
            {
                return Null_3d_Jagged();
            }
        }


        public static double[,] TensorMultiply(double[] Tensor1, double[,,] Tensor2)
        {
            int range = Tensor1.Length;
            if (range == Tensor2.GetLength(0))
            {
                int I1Max = Tensor1.GetLength(1);
                int I2Max = Tensor1.GetLength(2);
                double[,] Tensor3 = new double[I1Max, I2Max];
                for (int i = 0; i < I1Max; i++)
                {
                    for (int j = 0; j < I2Max; j++)
                    {
                        for (int k = 0; k < range; k++)
                        {
                            Tensor3[i, j] += Tensor1[k] * Tensor2[k,i,j];
                        }
                    }
                }
                return Tensor3;
            }
            else
            {
                return Null_2d();
            }
        }
        public static double[][] TensorMultiply(double[] Tensor1, double[][][] Tensor2)
        {
            int range = Tensor1.Length;
            if (range == Tensor2.GetLength(0))
            {
                int I1Max = Tensor2[0].GetLength(0);
                int I2Max = Tensor2[0][0].GetLength(0);
                double[][] Tensor3 = new double[I1Max][];
                for (int i = 0; i < I1Max; i++)
                {
                    Tensor3[i] = new double[I2Max];
                }
                double scale;
                double[][] tensor2d;
                double[] tensor1d;
                for (int k = 0; k < range; k++)
                {
                    scale = Tensor1[k];
                    tensor2d = Tensor2[k];
                    for (int i = 0; i < I1Max; i++)
                    {
                        tensor1d = tensor2d[i];
                        for (int j = 0; j < I2Max; j++)
                        {
                            Tensor3[i][j] += scale*tensor1d[j];
                        }
                    }
                }
                return Tensor3;
            }
            else
            {
                return Null_2d_Jagged();
            }
        }
        public static double[, ,] TensorMultiply(double[] Tensor1, double[,,,] Tensor2)
        {
            int range = Tensor1.Length;
            if (range == Tensor2.GetLength(0))
            {
                int I1Max = Tensor2.GetLength(1);
                int I2Max = Tensor2.GetLength(2);
                int I3Max = Tensor2.GetLength(3);
                double[, ,] Tensor3 = new double[I1Max, I2Max, I3Max];
                for (int i = 0; i < I1Max; i++)
                {
                    for (int j = 0; j < I2Max; j++)
                    {
                        for (int k = 0; k < range; k++)
                        {
                            for (int l = 0; l < range; l++)
                            {
                                Tensor3[i, j, k] += Tensor1[l]*Tensor2[l, i, j, k] ;
                            }
                        }
                    }
                }
                return Tensor3;
            }
            else
            {
                return Null_3d();
            }
        }
        public static double[][][] TensorMultiply(double[] Tensor1, double[][][][] Tensor2)
        {
            int range = Tensor1.Length;
            if (range == Tensor2.GetLength(0))
            {
                int I1Max = Tensor2[0].GetLength(0);
                int I2Max = Tensor2[0][0].GetLength(0);
                int I3Max = Tensor2[0][0][0].GetLength(0);
                double[][][] Tensor3 = new double[I1Max][][];
                for (int i = 0; i < I1Max; i++)
                {
                    Tensor3[i] = new double[I2Max][];
                    for (int j = 0; j < I2Max; j++)
                    {
                        Tensor3[i][j] = new double[I3Max];
                    }
                }
                double scale;
                double[][][] tensor3d;
                double[][] tensor2d;
                double[] tensor1d;
                for (int l = 0; l < range; l++)
                {
                    scale = Tensor1[l];
                    tensor3d = Tensor2[l];
                    for (int i = 0; i < I1Max; i++)
                    {
                        tensor2d = tensor3d[i];
                        for (int j = 0; j < I2Max; j++)
                        {
                            tensor1d = tensor2d[j];
                            for (int k = 0; k < I3Max; k++)
                            {
                                Tensor3[i][j][k] += scale * tensor1d[k];
                            }
                        }
                    }
                }
                return Tensor3;
            }
            else
            {
                return Null_3d_Jagged();
            }
        }
        /// <summary>
        /// Tensor product of two tensors to get a higher order tensor
        /// </summary>
        /// <param name="Tensor1"></param>
        /// <param name="Tensor2"></param>
        /// <returns></returns>
        public static double[,] Matrix_TensorProduct(double[] Tensor1, double[] Tensor2)
        {
            int I1Max = Tensor1.Length;
            int I2Max = Tensor2.Length;
            double[,] tensor = new double[I1Max, I2Max];
            for (int i = 0; i < I1Max; i++)
            {
                for (int j = 0; j < I2Max; j++)
                {
                    tensor[i, j] = Tensor1[i] * Tensor2[j];
                }
            }
            return tensor;
        }
        public static double[, ,] Matrix_TensorProduct(double[] Tensor1, double[] Tensor2, double[] Tensor3)
        {
            int I1Max = Tensor1.Length;
            int I2Max = Tensor2.Length;
            int I3Max = Tensor3.Length;
            double[, ,] tensor = new double[I1Max, I2Max, I3Max];
            for (int i = 0; i < I1Max; i++)
            {
                for (int j = 0; j < I2Max; j++)
                {
                    for (int k = 0; k < I3Max; k++)
                    {
                        tensor[i,j,k] = Tensor1[i] * Tensor2[j] * Tensor3[k];
                    }
                }
            }
            return tensor;
        }
        public static double[, , ,] Matrix_TensorProduct(double[] Tensor1, double[] Tensor2, double[] Tensor3, double[] Tensor4)
        {
            int I1Max = Tensor1.Length;
            int I2Max = Tensor2.Length;
            int I3Max = Tensor3.Length;
            int I4Max = Tensor4.Length;
            double[, , , ] tensor = new double[I1Max, I2Max, I3Max, I4Max];
            for (int i = 0; i < I1Max; i++)
            {
                for (int j = 0; j < I2Max; j++)
                {
                    for (int k = 0; k < I3Max; k++)
                    {
                        for (int l = 0; l < I4Max; l++)
                        {
                            tensor[i,j,k,l] = Tensor1[i] * Tensor2[j] * Tensor3[k] * Tensor4[l];
                        }
                    }
                }
            }
            return tensor;
        }
        public static double[,,] Matrix_TensorProduct(double[,] Tensor1, double[] Tensor2)
        {
            int I1Max = Tensor1.GetLength(0);
            int I2Max = Tensor1.GetLength(1);
            int I3Max = Tensor2.Length;
            double[, ,] tensor = new double[I1Max, I2Max, I3Max];
            for (int i = 0; i < I1Max; i++)
            {
                for (int j = 0; j < I2Max; j++)
                {
                    for (int k = 0; k < I3Max; k++)
                    {
                        tensor[i,j,k] = Tensor1[i,j] * Tensor2[k];
                    }
                }
            }
            return tensor;
        }
        public static double[, ,] Matrix_TensorProduct(double[] Tensor1, double[, ] Tensor2)
        {
            int I1Max = Tensor1.Length;
            int I2Max = Tensor2.GetLength(0);
            int I3Max = Tensor2.GetLength(1);
            double[, ,] tensor = new double[I1Max, I2Max, I3Max];
            for (int i = 0; i < I1Max; i++)
            {
                for (int j = 0; j < I2Max; j++)
                {
                    for (int k = 0; k < I3Max; k++)
                    {
                        tensor[i,j,k] = Tensor1[i] * Tensor2[j,k];
                    }
                }
            }
            return tensor;
        }
        public static double[, , ,] Matrix_TensorProduct(double[,] Tensor1, double[,] Tensor2)
        {
            int I1Max = Tensor1.GetLength(0);
            int I2Max = Tensor1.GetLength(1);
            int I3Max = Tensor2.GetLength(0);
            int I4Max = Tensor2.GetLength(1);
            double[, , ,] tensor = new double[I1Max, I2Max, I3Max, I4Max];
            for (int i = 0; i < I1Max; i++)
            {
                for (int j = 0; j < I2Max; j++)
                {
                    for (int k = 0; k < I3Max; k++)
                    {
                        for (int l = 0; l < I4Max; l++)
                        {
                            tensor[i,j,k,l] = Tensor1[i,j] * Tensor2[k,l];
                        }
                    }
                }
            }
            return tensor;
        }
        /// <summary>
        /// Tensor product of two tensors to get a higher order tensor in jagged matrix form
        /// </summary>
        /// <param name="Tensor1"></param>
        /// <param name="Tensor2"></param>
        /// <returns></returns>
        public static double[][] JaggedMatrix_TensorProduct(double[] Tensor1, double[] Tensor2)
        {
            int I1Max = Tensor1.Length;
            int I2Max = Tensor2.Length;
            double[][] tensor = NewJaggedArray(I1Max, I2Max);
            for (int i = 0; i < I1Max; i++)
            {
                for (int j = 0; j < I2Max; j++)
                {
                    tensor[i][j] = Tensor1[i] * Tensor2[j];
                }
            }
            return tensor;
        }
        public static double[][][] JaggedMatrix_TensorProduct(double[] Tensor1, double[] Tensor2, double[] Tensor3)
        {
            int I1Max = Tensor1.Length;
            int I2Max = Tensor2.Length;
            int I3Max = Tensor3.Length;
            double[][][] tensor = NewJaggedArray(I1Max, I2Max, I3Max);
            for (int i = 0; i < I1Max; i++)
            {
                for (int j = 0; j < I2Max; j++)
                {
                    for (int k = 0; k < I3Max; k++)
                    {
                        tensor[i][j][k] = Tensor1[i] * Tensor2[j] * Tensor3[k];
                    }
                }
            }
            return tensor;
        }
        public static double[][][][] JaggedMatrix_TensorProduct(double[] Tensor1, double[] Tensor2, double[] Tensor3, double[] Tensor4)
        {
            int I1Max = Tensor1.Length;
            int I2Max = Tensor2.Length;
            int I3Max = Tensor3.Length;
            int I4Max = Tensor4.Length;
            double[][][][] tensor = NewJaggedArray(I1Max, I2Max, I3Max,I4Max);
            for (int i = 0; i < I1Max; i++)
            {
                for (int j = 0; j < I2Max; j++)
                {
                    for (int k = 0; k < I3Max; k++)
                    {
                        for (int l = 0; l < I4Max; l++)
                        {
                            tensor[i][j][k][l] = Tensor1[i] * Tensor2[j] * Tensor3[k] * Tensor4[l];
                        }
                    }
                }
            }
            return tensor;
        }
        public static double[][][] JaggedMatrix_TensorProduct(double[][] Tensor1, double[] Tensor2)
        {
            int I1Max = Tensor1.Length;
            int I2Max = Tensor1[0].Length;
            int I3Max = Tensor2.Length;
            double[][][] tensor = NewJaggedArray(I1Max, I2Max, I3Max);
            for (int i = 0; i < I1Max; i++)
            {
                for (int j = 0; j < I2Max; j++)
                {
                    for (int k = 0; k < I3Max; k++)
                    {
                        tensor[i][j][k] = Tensor1[i][j] * Tensor2[k];
                    }
                }
            }
            return tensor;
        }
        public static double[][][] JaggedMatrix_TensorProduct(double[] Tensor1, double[][] Tensor2)
        {
            int I1Max = Tensor1.Length;
            int I2Max = Tensor2.GetLength(0);
            int I3Max = Tensor2[0].Length;
            double[][][] tensor = NewJaggedArray(I1Max, I2Max, I3Max);
            for (int i = 0; i < I1Max; i++)
            {
                for (int j = 0; j < I2Max; j++)
                {
                    for (int k = 0; k < I3Max; k++)
                    {
                        tensor[i][j][k] = Tensor1[i] * Tensor2[j][k];
                    }
                }
            }
            return tensor;
        }
        public static double[][][][] JaggedMatrix_TensorProduct(double[][] Tensor1, double[][] Tensor2)
        {
            int I1Max = Tensor1.Length;
            int I2Max = Tensor1[0].Length;
            int I3Max = Tensor2.Length;
            int I4Max = Tensor2[0].Length;
            double[][][][] tensor = NewJaggedArray(I1Max, I2Max, I3Max, I4Max);
            for (int i = 0; i < I1Max; i++)
            {
                for (int j = 0; j < I2Max; j++)
                {
                    for (int k = 0; k < I3Max; k++)
                    {
                        for (int l = 0; l < I4Max; l++)
                        {
                            tensor[i][j][k][l] = Tensor1[i][j] * Tensor2[k][l];
                        }
                    }
                }
            }
            return tensor;
        }

        /// <summary>
        /// Calcualte tensor ".." operation on arrays
        /// </summary>
        /// <param name="Tensor1"></param>
        /// <param name="Tensor2"></param>
        /// <returns></returns>
        public static double TensorMultiply_DDotH(double[,] Tensor1, double[,] Tensor2)
        {
            double range1 = Tensor1.GetLength(0);
            double range2 = Tensor1.GetLength(1);
            if (range1 == Tensor2.GetLength(1))
            {
                if (range2 == Tensor2.GetLength(0))
                {
                    double Tensor3 = 0.0D;
                    for (int m = 0; m < range1; m++)
                    {
                        for (int n = 0; n < range2; n++)
                        {
                            Tensor3 += Tensor1[m, n] * Tensor2[n, m];
                        }
                    }
                    return Tensor3;

                }
                else
                {
                    return double.NaN;
                }

            }
            else
            {
                return double.NaN;
            }

        }
        public static double[] TensorMultiply_DDotH(double[,,] Tensor1, double[,] Tensor2)
        {
            double range1 = Tensor1.GetLength(1);
            double range2 = Tensor1.GetLength(2);
            if (range1 == Tensor2.GetLength(1))
            {
                if (range2 == Tensor2.GetLength(0))
                {
                    int I1Max = Tensor1.GetLength(0);
                    double[] Tensor3 = new double[I1Max];
                    for (int i = 0; i < I1Max; i++)
                    {
                        for (int m = 0; m < range1; m++)
                        {
                            for (int n = 0; n < range2; n++)
                            {
                                Tensor3[i] += Tensor1[i, m, n] * Tensor2[n, m];
                            }
                        }
                    }
                    return Tensor3;

                }
                else
                {
                    return Null_1d();
                }

            }
            else
            {
                return Null_1d();
            }

        }
        public static double[,] TensorMultiply_DDotH(double[, ,,] Tensor1, double[,] Tensor2)
        {
            double range1 = Tensor1.GetLength(2);
            double range2 = Tensor1.GetLength(3);
            if (range1 == Tensor2.GetLength(1))
            {
                if (range2 == Tensor2.GetLength(0))
                {
                    int I1Max = Tensor1.GetLength(0);
                    int I2Max = Tensor1.GetLength(1); 
                    double[,] Tensor3 = new double[I1Max,I2Max];
                    for (int i = 0; i < I1Max; i++)
                    {
                        for (int j = 0; j < I2Max; j++)
                        {
                            for (int m = 0; m < range1; m++)
                            {
                                for (int n = 0; n < range2; n++)
                                {
                                    Tensor3[i,j] += Tensor1[i,j, m, n] * Tensor2[n, m];
                                }
                            }
                        }
                    }
                    return Tensor3;

                }
                else
                {
                    return Null_2d();
                }

            }
            else
            {
                return Null_2d();
            }

        }
        public static double[] TensorMultiply_DDotH(double[,] Tensor1, double[, ,] Tensor2)
        {
            double range1 = Tensor1.GetLength(1);
            double range2 = Tensor1.GetLength(2);
            if (range1 == Tensor2.GetLength(1))
            {
                if (range2 == Tensor2.GetLength(0))
                {
                    int I1Max = Tensor2.GetLength(2);
                    double[] Tensor3 = new double[I1Max];
                    for (int i = 0; i < I1Max; i++)
                    {
                        for (int m = 0; m < range1; m++)
                        {
                            for (int n = 0; n < range2; n++)
                            {
                                Tensor3[i] += Tensor1[m, n] * Tensor2[n, m , i];
                            }
                        }
                    }
                    return Tensor3;

                }
                else
                {
                    return Null_1d();
                }

            }
            else
            {
                return Null_1d();
            }

        }
        public static double[,] TensorMultiply_DDotH(double[,] Tensor1, double[,,,] Tensor2)
        {
            double range1 = Tensor1.GetLength(0);
            double range2 = Tensor1.GetLength(1);
            if (range1 == Tensor1.GetLength(1))
            {
                if (range2 == Tensor2.GetLength(0))
                {
                    int I1Max = Tensor2.GetLength(2);
                    int I2Max = Tensor2.GetLength(3);
                    double[,] Tensor3 = new double[I1Max, I2Max];
                    for (int i = 0; i < I1Max; i++)
                    {
                        for (int j = 0; j < I2Max; j++)
                        {
                            for (int m = 0; m < range1; m++)
                            {
                                for (int n = 0; n < range2; n++)
                                {
                                    Tensor3[i, j] += Tensor1[m, n] * Tensor2[n, m, i, j];
                                }
                            }
                        }
                    }
                    return Tensor3;

                }
                else
                {
                    return Null_2d();
                }

            }
            else
            {
                return Null_2d();
            }

        }
        

        public static double TensorMultiply_DDotH(double[][] Tensor1, double[][] Tensor2)
        {
            double range1 = Tensor1.GetLength(0);
            double range2 = Tensor1[0].Length;
            if (range1 == Tensor2[0].Length)
            {
                if (range2 == Tensor2.GetLength(0))
                {
                    double Tensor3 = 0.0D;
                    for (int m = 0; m < range1; m++)
                    {
                        for (int n = 0; n < range2; n++)
                        {
                            Tensor3 += Tensor1[m][n] * Tensor2[n][m];
                        }
                    }
                    return Tensor3;

                }
                else
                {
                    return double.NaN;
                }

            }
            else
            {
                return double.NaN;
            }

        }
        public static double[] TensorMultiply_DDotH(double[][][] Tensor1, double[][] Tensor2)
        {
            double range1 = Tensor1[0].Length;
            double range2 = Tensor1[0][0].Length;
            if (range1 == Tensor2[0].Length)
            {
                if (range2 == Tensor2.GetLength(0))
                {
                    int I1Max = Tensor1.GetLength(0);
                    double[] Tensor3 = new double[I1Max];
                    for (int i = 0; i < I1Max; i++)
                    {
                        for (int m = 0; m < range1; m++)
                        {
                            for (int n = 0; n < range2; n++)
                            {
                                Tensor3[i] += Tensor1[i][m][n] * Tensor2[n][m];
                            }
                        }
                    }
                    return Tensor3;

                }
                else
                {
                    return Null_1d();
                }

            }
            else
            {
                return Null_1d();
            }

        }
        public static double[][] TensorMultiply_DDotH(double[][][][] Tensor1, double[][] Tensor2)
        {
            double range1 = Tensor1[0][0].GetLength(0);
            double range2 = Tensor1[0][0][0].Length;
            if (range1 == Tensor2[0].Length)
            {
                if (range2 == Tensor2.GetLength(0))
                {
                    int I1Max = Tensor1.GetLength(0);
                    int I2Max = Tensor1[0].GetLength(0); 
                    double[][] Tensor3 = NewJaggedArray(I1Max,I2Max);
                    for (int i = 0; i < I1Max; i++)
                    {
                        for (int j = 0; j < I2Max; j++)
                        {
                            for (int m = 0; m < range1; m++)
                            {
                                for (int n = 0; n < range2; n++)
                                {
                                    Tensor3[i][j] += Tensor1[i][j][m][n] * Tensor2[n][m];
                                }
                            }
                        }
                    }
                    return Tensor3;

                }
                else
                {
                    return Null_2d_Jagged();
                }

            }
            else
            {
                return Null_2d_Jagged();
            }

        }
        public static double[] TensorMultiply_DDotH(double[][] Tensor1, double[][][] Tensor2)
        {
            double range1 = Tensor1.GetLength(0);
            double range2 = Tensor1[0].Length;
            if (range1 == Tensor2[0].GetLength(0))
            {
                if (range2 == Tensor2.GetLength(0))
                {
                    int I1Max = Tensor2[0][0].Length;
                    double[] Tensor3 = new double[I1Max];
                    for (int i = 0; i < I1Max; i++)
                    {
                        for (int m = 0; m < range1; m++)
                        {
                            for (int n = 0; n < range2; n++)
                            {
                                Tensor3[i] += Tensor1[m][n] * Tensor2[n][m][i];
                            }
                        }
                    }
                    return Tensor3;

                }
                else
                {
                    return Null_1d();
                }

            }
            else
            {
                return Null_1d();
            }

        }
        public static double[][] TensorMultiply_DDotH(double[][] Tensor1, double[][][][] Tensor2)
        {
            double range1 = Tensor1.GetLength(0);
            double range2 = Tensor1[0].Length;
            if (range1 == Tensor1[0].GetLength(0))
            {
                if (range2 == Tensor2.GetLength(0))
                {
                    int I1Max = Tensor2[0][0].GetLength(0);
                    int I2Max = Tensor2[0][0][0].Length;
                    double[][] Tensor3 = NewJaggedArray(I1Max, I2Max);
                    for (int i = 0; i < I1Max; i++)
                    {
                        for (int j = 0; j < I2Max; j++)
                        {
                            for (int m = 0; m < range1; m++)
                            {
                                for (int n = 0; n < range2; n++)
                                {
                                    Tensor3[i][j] += Tensor1[m][n] * Tensor2[n][m][i][j];
                                }
                            }
                        }
                    }
                    return Tensor3;

                }
                else
                {
                    return Null_2d_Jagged();
                }

            }
            else
            {
                return Null_2d_Jagged();
            }

        }
        /// <summary>
        /// Tensor ":" product of arrays
        /// </summary>
        /// <param name="Tensor1"></param>
        /// <param name="Tensor2"></param>
        /// <returns></returns>
        public static double TensorMultiply_DDotV(double[,] Tensor1, double[,] Tensor2)
        {
            double range1 = Tensor1.GetLength(0);
            double range2 = Tensor1.GetLength(1);
            if (range1 == Tensor2.GetLength(0))
            {
                if (range2 == Tensor2.GetLength(1))
                {
                    double Tensor3 = 0.0D;
                    for (int m = 0; m < range1; m++)
                    {
                        for (int n = 0; n < range2; n++)
                        {
                            Tensor3 += Tensor1[m, n] * Tensor2[m, n];
                        }
                    }
                    return Tensor3;

                }
                else
                {
                    return double.NaN;
                }

            }
            else
            {
                return double.NaN;
            }

        }
        public static double[] TensorMultiply_DDotV(double[, ,] Tensor1, double[,] Tensor2)
        {
            double range1 = Tensor1.GetLength(1);
            double range2 = Tensor1.GetLength(2);
            if (range1 == Tensor2.GetLength(0))
            {
                if (range2 == Tensor2.GetLength(1))
                {
                    int I1Max = Tensor1.GetLength(0);
                    double[] Tensor3 = new double[I1Max];
                    for (int i = 0; i < I1Max; i++)
                    {
                        for (int m = 0; m < range1; m++)
                        {
                            for (int n = 0; n < range2; n++)
                            {
                                Tensor3[i] += Tensor1[i, m, n] * Tensor2[m, n];
                            }
                        }
                    }
                    return Tensor3;

                }
                else
                {
                    return Null_1d();
                }

            }
            else
            {
                return Null_1d();
            }

        }
        public static double[,] TensorMultiply_DDotV(double[, , ,] Tensor1, double[,] Tensor2)
        {
            double range1 = Tensor1.GetLength(2);
            double range2 = Tensor1.GetLength(3);
            if (range1 == Tensor2.GetLength(0))
            {
                if (range2 == Tensor2.GetLength(1))
                {
                    int I1Max = Tensor1.GetLength(0);
                    int I2Max = Tensor1.GetLength(1);
                    double[,] Tensor3 = new double[I1Max, I2Max];
                    for (int i = 0; i < I1Max; i++)
                    {
                        for (int j = 0; j < I2Max; j++)
                        {
                            for (int m = 0; m < range1; m++)
                            {
                                for (int n = 0; n < range2; n++)
                                {
                                    Tensor3[i, j] += Tensor1[i, j, m, n] * Tensor2[m, n];
                                }
                            }
                        }
                    }
                    return Tensor3;

                }
                else
                {
                    return Null_2d();
                }

            }
            else
            {
                return Null_2d();
            }

        }
        public static double[] TensorMultiply_DDotV(double[,] Tensor1, double[, ,] Tensor2)
        {
            double range1 = Tensor1.GetLength(1);
            double range2 = Tensor1.GetLength(2);
            if (range1 == Tensor2.GetLength(0))
            {
                if (range2 == Tensor2.GetLength(1))
                {
                    int I1Max = Tensor2.GetLength(2);
                    double[] Tensor3 = new double[I1Max];
                    for (int i = 0; i < I1Max; i++)
                    {
                        for (int m = 0; m < range1; m++)
                        {
                            for (int n = 0; n < range2; n++)
                            {
                                Tensor3[i] += Tensor1[m, n] * Tensor2[m, n, i];
                            }
                        }
                    }
                    return Tensor3;

                }
                else
                {
                    return Null_1d();
                }

            }
            else
            {
                return Null_1d();
            }

        }
        public static double[,] TensorMultiply_DDotV(double[,] Tensor1, double[, , ,] Tensor2)
        {
            double range1 = Tensor1.GetLength(0);
            double range2 = Tensor1.GetLength(1);
            if (range1 == Tensor2.GetLength(0))
            {
                if (range2 == Tensor2.GetLength(1))
                {
                    int I1Max = Tensor2.GetLength(2);
                    int I2Max = Tensor2.GetLength(3);
                    double[,] Tensor3 = new double[I1Max, I2Max];
                    for (int i = 0; i < I1Max; i++)
                    {
                        for (int j = 0; j < I2Max; j++)
                        {
                            for (int m = 0; m < range1; m++)
                            {
                                for (int n = 0; n < range2; n++)
                                {
                                    Tensor3[i, j] += Tensor1[m, n] * Tensor2[m, n, i, j];
                                }
                            }
                        }
                    }
                    return Tensor3;

                }
                else
                {
                    return Null_2d();
                }

            }
            else
            {
                return Null_2d();
            }

        }


        public static double TensorMultiply_DDotV(double[][] Tensor1, double[][] Tensor2)
        {
            double range1 = Tensor1.GetLength(0);
            double range2 = Tensor1[0].Length;
            if (range1 == Tensor2.GetLength(0))
            {
                if (range2 == Tensor2[0].Length)
                {
                    double Tensor3 = 0.0D;
                    for (int m = 0; m < range1; m++)
                    {
                        Tensor3 += Vector_DotProduct(Tensor1[m], Tensor2[m]);
                    }
                    return Tensor3;

                }
                else
                {
                    return double.NaN;
                }

            }
            else
            {
                return double.NaN;
            }

        }
        public static double[] TensorMultiply_DDotV(double[][][] Tensor1, double[][] Tensor2)
        {
            double range1 = Tensor1[0].GetLength(0);
            double range2 = Tensor1[0][0].Length;
            if (range1 == Tensor2.GetLength(0))
            {
                if (range2 == Tensor2[0].Length)
                {
                    int I1Max = Tensor1.GetLength(0);
                    double[] Tensor3 = new double[I1Max];
                    for (int i = 0; i < I1Max; i++)
                    {
                        Tensor3[i] = TensorMultiply_DDotV(Tensor1[i], Tensor2);
                    }
                    return Tensor3;

                }
                else
                {
                    return Null_1d();
                }

            }
            else
            {
                return Null_1d();
            }

        }
        public static double[][] TensorMultiply_DDotV(double[][][][] Tensor1, double[][] Tensor2)
        {
            double range1 = Tensor1[0][0].GetLength(0);
            double range2 = Tensor1[0][0][0].Length;
            if (range1 == Tensor2.GetLength(0))
            {
                if (range2 == Tensor2[0].Length)
                {
                    int I1Max = Tensor1.GetLength(0);
                    int I2Max = Tensor1[0].GetLength(0);
                    double[][] Tensor3 = NewJaggedArray(I1Max, I2Max);
                    for (int i = 0; i < I1Max; i++)
                    {
                        Tensor3[i] = TensorMultiply_DDotV(Tensor1[i], Tensor2);
                    }
                    return Tensor3;

                }
                else
                {
                    return Null_2d_Jagged();
                }

            }
            else
            {
                return Null_2d_Jagged();
            }

        }
        public static double[] TensorMultiply_DDotV(double[][] Tensor1, double[][][] Tensor2)
        {
            double range1 = Tensor1.GetLength(0);
            double range2 = Tensor1[0].Length;
            if (range1 == Tensor2.GetLength(0))
            {
                if (range2 == Tensor2[0].GetLength(0))
                {
                    int I1Max = Tensor2[0][0].Length;
                    double[] Tensor3 = new double[I1Max];
                    for (int i = 0; i < I1Max; i++)
                    {
                        for (int m = 0; m < range1; m++)
                        {
                            for (int n = 0; n < range2; n++)
                            {
                                Tensor3[i] += Tensor1[m][n] * Tensor2[m][n][i];
                            }
                        }
                    }
                    return Tensor3;

                }
                else
                {
                    return Null_1d();
                }

            }
            else
            {
                return Null_1d();
            }

        }
        public static double[][] TensorMultiply_DDotV(double[][] Tensor1, double[][][][] Tensor2)
        {
            double range1 = Tensor1.GetLength(0);
            double range2 = Tensor1[0].Length;
            if (range1 == Tensor2.GetLength(0))
            {
                if (range2 == Tensor2[0].GetLength(0))
                {
                    int I1Max = Tensor2[0][0].GetLength(0);
                    int I2Max = Tensor2[0][0][0].Length;
                    double[][] Tensor3 = NewJaggedArray(I1Max, I2Max);
                    for (int i = 0; i < I1Max; i++)
                    {
                        for (int j = 0; j < I2Max; j++)
                        {
                            for (int m = 0; m < range1; m++)
                            {
                                for (int n = 0; n < range2; n++)
                                {
                                    Tensor3[i][j] += Tensor1[m][n] * Tensor2[m][n][i][j];
                                }
                            }
                        }
                    }
                    return Tensor3;

                }
                else
                {
                    return Null_2d_Jagged();
                }

            }
            else
            {
                return Null_2d_Jagged();
            }

        }
        #endregion

        #region Initialize Even length jagged array

        public static double[][] NewJaggedArray(int I1Max, int I2Max)
        {
            double[][] JaggedArray = new double[I1Max][];
            for (int i = 0; i < I1Max; i++)
            {
                JaggedArray[i] = new double[I2Max];
            }
            return JaggedArray;
        }
        public static double[][][] NewJaggedArray(int I1Max, int I2Max, int I3Max)
        {
            double[][][] JaggedArray = new double[I1Max][][];
            for (int i = 0; i < I1Max; i++)
            {
                JaggedArray[i] = new double[I2Max][];
                for (int j = 0; j < I2Max; j++)
                {
                    JaggedArray[i][j] = new double[I3Max];
                }
            }
            return JaggedArray;
        }
        public static double[][][][] NewJaggedArray(int I1Max, int I2Max, int I3Max, int I4Max)
        {
            double[][][][] JaggedArray = new double[I1Max][][][];
            for (int i = 0; i < I1Max; i++)
            {
                JaggedArray[i] = new double[I2Max][][];
                for (int j = 0; j < I2Max; j++)
                {
                    JaggedArray[i][j] = new double[I3Max][];
                    for (int k = 0; k < I3Max; k++)
                    {
                        JaggedArray[i][j][k] = new double[I4Max];
                    }
                }
            }
            return JaggedArray;
        }
        public static double[][][][][] NewJaggedArray(int I1Max, int I2Max, int I3Max, int I4Max, int I5Max)
        {
            double[][][][][] JaggedArray = new double[I1Max][][][][];
            for (int i = 0; i < I1Max; i++)
            {
                JaggedArray[i] = new double[I2Max][][][];
                for (int j = 0; j < I2Max; j++)
                {
                    JaggedArray[i][j] = new double[I3Max][][];
                    for (int k = 0; k < I3Max; k++)
                    {
                        JaggedArray[i][j][k] = new double[I4Max][];
                        for (int l = 0; l < I4Max; l++)
                        {
                            JaggedArray[i][j][k][l] = new double[I5Max];
                        }
                    }
                }
            }
            return JaggedArray;
        }
        public static double[][][][][][] NewJaggedArray(int I1Max, int I2Max, int I3Max, int I4Max, int I5Max, int I6Max)
        {
            double[][][][][][] JaggedArray = new double[I1Max][][][][][];
            for (int i = 0; i < I1Max; i++)
            {
                JaggedArray[i] = new double[I2Max][][][][];
                for (int j = 0; j < I2Max; j++)
                {
                    JaggedArray[i][j] = new double[I3Max][][][];
                    for (int k = 0; k < I3Max; k++)
                    {
                        JaggedArray[i][j][k] = new double[I4Max][][];
                        for (int l = 0; l < I4Max; l++)
                        {
                            JaggedArray[i][j][k][l] = new double[I5Max][];
                            for (int m = 0; m < I5Max; m++)
                            {
                                JaggedArray[i][j][k][l][m] = new double[I6Max];
                            }
                        }
                    }
                }
            }
            return JaggedArray;
        }

        #endregion

        #region Null arrays
        public static double[] Null_1d()
        {
            double[] NullArray = new double[1];
            NullArray[0] = double.NaN;
            return NullArray;
        }
        public static double[,] Null_2d()
        {
            double[,] NullArray = new double[1,1];
            NullArray[0,0] = double.NaN;
            return NullArray;
        }
        public static double[,,] Null_3d()
        {
            double[,,] NullArray = new double[1, 1, 1];
            NullArray[0, 0, 0] = double.NaN;
            return NullArray;
        }
        public static double[, , ,] Null_4d()
        {
            double[, , ,] NullArray = new double[1, 1, 1, 1];
            NullArray[0, 0, 0, 0] = double.NaN;
            return NullArray;
        }
        public static double[][] Null_2d_Jagged()
        {
            double[][] NullArray = new double[1][];
            NullArray[0] = new double[1];
            NullArray[0][0] = double.NaN;
            return NullArray;
        }
        public static double[][][] Null_3d_Jagged()
        {
            double[][][] NullArray = new double[1][][];
            NullArray[0] = new double[1][];
            NullArray[0][0] = new double[1];
            NullArray[0][0][0] = double.NaN;
            return NullArray;
        }
        public static double[][][][] Null_4d_Jagged()
        {
            double[][][][] NullArray = new double[1][][][];
            NullArray[0] = new double[1][][];
            NullArray[0][0] = new double[1][];
            NullArray[0][0][0] = new double[1];
            NullArray[0][0][0][0] = double.NaN;
            return NullArray;
        }
        #endregion 

        #region Make Matrix from vectors
        
        /// <summary>
        /// Make matrix from one or move column vectors
        /// </summary>
        /// <param name="Vector1"></param>
        /// <returns></returns>
        public static double[,] Matrix_MakeFromColumnVectors(double[] Vector1)
        {
            /// Makes a column matrix with vector as a column
            int rows = Vector1.Length;
            double[,] matrix = new double[rows,1];
            for (int i = 0; i < rows; i++)
            {
                matrix[i,0] = Vector1[i];
            }
            return matrix;
        }
        public static double[,] Matrix_MakeFromColumnVectors(double[] Vector1, double[] Vector2)
        {
            /// Makes a column matrix with vector as a column
            int rows = Vector1.Length;
            double[,] matrix = new double[rows, 2];
            for (int i = 0; i < rows; i++)
            {
                matrix[i, 0] = Vector1[i];
                matrix[i, 1] = Vector2[i];
            }
            return matrix;
        }
        public static double[,] Matrix_MakeFromColumnVectors(double[] Vector1, double[] Vector2, double[] Vector3)
        {
            /// Makes a column matrix with vector as a column
            int rows = Vector1.Length;
            double[,] matrix = new double[rows, 3];
            for (int i = 0; i < rows; i++)
            {
                matrix[i, 0] = Vector1[i];
                matrix[i, 1] = Vector2[i];
                matrix[i, 2] = Vector3[i];
            }
            return matrix;
        }
        public static double[,] Matrix_MakeFromColumnVectors(double[][] Vectors)
        {
            /// Makes a column matrix with vector as a row
            int columns = Vectors.GetLength(0);
            int rows = Vectors[0].Length;
            double[,] matrix = new double[rows, columns];
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    matrix[i, j] = Vectors[j][i];
                }
            }
            return matrix;
        }
        /// <summary>
        /// Make jagged matrix from one or more column vectors
        /// </summary>
        /// <param name="Vector1"></param>
        /// <returns></returns>
        public static double[][] JaggedMatrix_MakeFromColumnVectors(double[] Vector1)
        {
            /// Makes a column matrix with vector as a column
            int rows = Vector1.Length;
            double[][] matrix = new double[rows][];
            for (int i = 0; i < rows; i++)
            {
                matrix[i] = new double[1];
                matrix[i][0] = Vector1[i];
            }
            return matrix;
        }
        public static double[][] JaggedMatrix_MakeFromColumnVectors(double[] Vector1, double[] Vector2)
        {
            /// Makes a column matrix with vector as a column
            int rows = Vector1.Length;
            double[][] matrix = new double[rows][];
            for (int i = 0; i < rows; i++)
            {
                matrix[i] = new double[2];
                matrix[i][0] = Vector1[i];
                matrix[i][1] = Vector2[i];
            }
            return matrix;
        }
        public static double[][] JaggedMatrix_MakeFromColumnVectors(double[] Vector1, double[] Vector2, double[] Vector3)
        {
            /// Makes a column matrix with vector as a column
            int rows = Vector1.Length;
            double[][] matrix = new double[rows][];
            for (int i = 0; i < rows; i++)
            {
                matrix[i] = new double[3];
                matrix[i][0] = Vector1[i];
                matrix[i][1] = Vector2[i];
                matrix[i][2] = Vector3[i];
            }
            return matrix;
        }
        public static double[][] JaggedMatrix_MakeFromColumnVectors(double[][] Vectors)
        {
            /// Makes a column matrix with vector as a row
            int columns = Vectors.GetLength(0);
            int rows = Vectors[0].Length;
            double[][] matrix = NewJaggedArray(rows,columns);
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    matrix[i][j] = Vectors[j][i];
                }
            }
            return matrix;
        }


        /// <summary>
        /// Make matrix from one or move row vectors
        /// </summary>
        /// <param name="Vector1"></param>
        /// <returns></returns>
        public static double[,] Matrix_MakeFromRowVectors(double[] Vector1)
        {
            /// Makes a column matrix with vector as a row
            int columns = Vector1.Length;
            double[,] matrix = new double[1,columns];
            for (int i = 0; i < columns; i++)
            {
                matrix[0, i] = Vector1[i];
            }
            return matrix;
        }
        public static double[,] Matrix_MakeFromRowVectors(double[] Vector1, double[] Vector2)
        {
            /// Makes a column matrix with vector as a row
            int columns = Vector1.Length;
            double[,] matrix = new double[2, columns];
            for (int i = 0; i < columns; i++)
            {
                matrix[0, i] = Vector1[i];
                matrix[1, i] = Vector2[i];
            }
            return matrix;
        }
        public static double[,] Matrix_MakeFromRowVectors(double[] Vector1, double[] Vector2, double[] Vector3)
        {
            /// Makes a column matrix with vector as a row
            int columns = Vector1.Length;
            double[,] matrix = new double[3, columns];
            for (int i = 0; i < columns; i++)
            {
                matrix[0, i] = Vector1[i];
                matrix[1, i] = Vector2[i];
                matrix[2, i] = Vector3[i];
            }
            return matrix;
        }
        public static double[,] Matrix_MakeFromRowVectors(double[][] Vectors)
        {
            /// Makes a column matrix with vector as a row
            int rows = Vectors.GetLength(0);
            int columns = Vectors[0].Length;
            double[,] matrix = new double[rows, columns];
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    matrix[i,j] = Vectors[i][j];
                }
            }
            return matrix;
        }
        /// <summary>
        /// Make jagged matrix from one or more row vectors
        /// </summary>
        /// <param name="Vector1"></param>
        /// <returns></returns>
        public static double[][] JaggedMatrix_MakeFromRowVectors(double[] Vector1)
        {
            /// Makes a column matrix with vector as a row
            int columns = Vector1.Length;
            double[][] matrix = NewJaggedArray(1, columns);
            for (int i = 0; i < columns; i++)
            {
                matrix[0][i] = Vector1[i];
            }
            return matrix;
        }
        public static double[][] JaggedMatrix_MakeFromRowVectors(double[] Vector1, double[] Vector2)
        {
            /// Makes a column matrix with vector as a row
            int columns = Vector1.Length;
            double[][] matrix = NewJaggedArray(2, columns);
            for (int i = 0; i < columns; i++)
            {
                matrix[0][i] = Vector1[i];
                matrix[1][i] = Vector2[i];
            }
            return matrix;
        }
        public static double[][] JaggedMatrix_MakeFromRowVectors(double[] Vector1, double[] Vector2, double[] Vector3)
        {
            /// Makes a column matrix with vector as a row
            int columns = Vector1.Length;
            double[][] matrix = NewJaggedArray(3, columns);
            for (int i = 0; i < columns; i++)
            {
                matrix[0][i] = Vector1[i];
                matrix[1][i] = Vector2[i];
                matrix[2][i] = Vector3[i];
            }
            return matrix;
        }
        public static double[][] JaggedMatrix_MakeFromRowVectors(double[][] Vectors)
        {
            /// Makes a column matrix with vector as a row
            int rows = Vectors.GetLength(0);
            double[][] matrix = new double[rows][];
            for (int i = 0; i < rows; i++)
            {
                int columns = Vectors[i].Length;
                matrix[i] = new double[columns];
                for (int j = 0; j < columns; j++)
                {
                    matrix[i][j] = Vectors[i][j];
                }
            }
            return matrix;
        }
        #endregion

        #region Rotation of vectors and tools for this rotation
        /// <summary>
        /// Calculate the rotation matrix about arbitraty axis
        /// </summary>
        /// <param name="AngleRad"></param>
        /// <returns></returns>
        public static double[] RotationVectorAboutAxis(double[] VectorToRotate, double RotationAngle_Rad, double[] UnitVectorAlongAxisOfRotation)
        {
            double[][] Q = RotationMatrixAboutAxis_Jagged(RotationAngle_Rad, UnitVectorAlongAxisOfRotation);
            double[] NewVector = new double[3];
            double[] QRow;
            for (int i = 0; i < 3; i++)
            {
                QRow = Q[i]; 
                for (int j = 0; j < 3; j++)
                {
                    NewVector[i] += QRow[j] * VectorToRotate[j];
                }
            }
            return NewVector;
        }
        /// <summary>
        /// Calculate the rotation matrix about arbitraty axis-0
        /// </summary>
        /// <param name="AngleRad"></param>
        /// <returns></returns>
        public static double[] RotationVectorAboutAxis0(double[] VectorToRotate, double RotationAngle_Rad)
        {
            double phi = RotationAngle_Rad;
            double cphi = Math.Cos(phi);
            double sphi = Math.Sin(phi);
            double[] u = VectorToRotate;
            double[] NewVector = new double[3];
            NewVector[0] = u[0];
            NewVector[1] = cphi * u[1] - sphi * u[2];
            NewVector[2] = sphi * u[1] + cphi * u[2];
            return NewVector;
        }
        /// <summary>
        /// Calculate the rotation matrix about arbitraty axis-1
        /// </summary>
        /// <param name="AngleRad"></param>
        /// <returns></returns>
        public static double[] RotationVectorAboutAxis1(double[] VectorToRotate, double RotationAngle_Rad)
        {
            double phi = RotationAngle_Rad;
            double cphi = Math.Cos(phi);
            double sphi = Math.Sin(phi);
            double[] u = VectorToRotate;
            double[] NewVector = new double[3];
            NewVector[0] = cphi * u[0] + sphi * u[2];
            NewVector[1] = u[1];
            NewVector[2] = -sphi * u[0] + cphi * u[2];
            return NewVector;
        }
        /// <summary>
        /// Calculate the rotation matrix about arbitraty axis-2
        /// </summary>
        /// <param name="AngleRad"></param>
        /// <returns></returns>
        public static double[] RotationVectorAboutAxis2(double[] VectorToRotate, double RotationAngle_Rad)
        {
            double phi = RotationAngle_Rad;
            double cphi = Math.Cos(phi);
            double sphi = Math.Sin(phi);
            double[] u = VectorToRotate;
            double[] NewVector = new double[3];
            NewVector[0] = cphi * u[0] - sphi * u[1];
            NewVector[1] = sphi * u[0] + cphi * u[1];
            NewVector[2] = u[2];
            return NewVector;
        }
        /// <summary>
        /// Calculate the rotation matrix about arbitraty axis
        /// </summary>
        /// <param name="AngleRad"></param>
        /// <returns></returns>
        public static double[][] RotationMatrixAboutAxis_Jagged(double RotationAngle_Rad, double[] UnitVectorAlongAxisOfRotation)
        {
            double phi = RotationAngle_Rad;
            double[] ni = UnitVectorAlongAxisOfRotation;
            double[][] Q = new double[3][]; //Rotation matrix
            double cphi = Math.Cos(phi);
            double sphi = Math.Sin(phi);
            double OneMinuscphi = 1.0D-cphi;
            for (int i = 0; i < 3; i++)
            {
                Q[i] = new double[3];
                Q[i][i] = cphi;
                for (int j = 0; j < 3; j++)
                {
                    Q[i][j] += OneMinuscphi * ni[i] * ni[j];
                }
            }
            double ux = sphi * ni[0];
            double uy = sphi * ni[1];
            double uz = sphi * ni[0];
            Q[0][1] -= uz;
            Q[0][2] += uy;
            Q[1][2] -= ux;

            Q[1][0] += uz;
            Q[2][0] -= uy;
            Q[2][1] += ux;

            return Q;
        }
        /// <summary>
        /// Calculate the rotation matrix about arbitraty axis
        /// </summary>
        /// <param name="AngleRad"></param>
        /// <returns></returns>
        public static double[,] RotationMatrixAboutAxis(double RotationAngle_Rad, double[] UnitVectorAlongAxisOfRotation)
        {
            double phi = RotationAngle_Rad;
            double[] ni = UnitVectorAlongAxisOfRotation;
            double[,] Q = new double[3,3]; //Rotation matrix
            double cphi = Math.Cos(phi);
            double sphi = Math.Sin(phi);
            double OneMinuscphi = 1.0D - cphi;
            for (int i = 0; i < 3; i++)
            {
                Q[i,i] = cphi;
                for (int j = 0; j < 3; j++)
                {
                    Q[i,j] += OneMinuscphi * ni[i] * ni[j];
                }
            }
            double ux = sphi * ni[0];
            double uy = sphi * ni[1];
            double uz = sphi * ni[0];
            Q[0,1] -= uz;
            Q[0,2] += uy;
            Q[1,2] -= ux;

            Q[1,0] += uz;
            Q[2,0] -= uy;
            Q[2,1] += ux;

            return Q;
        }
        /// <summary>
        /// Calculate the rotation matrix for rotation about the 0-axis
        /// </summary>
        /// <param name="AngleRad"></param>
        /// <returns></returns>
        public static double[,] RotationMatrixAbout0Axis(double AngleRad)
        {
            double[,] Q = new double[3,3];
            Q[0, 0] = 1.0d;
            Q[1, 1] = Math.Cos(AngleRad);
            Q[2, 2] = Q[1, 1];
            Q[1, 2] = -Math.Sin(AngleRad);
            Q[2, 1] = -Q[1, 2];
            return Q;
        }
        /// <summary>
        /// Calculate the rotation matrix for rotation about the 0-axis
        /// </summary>
        /// <param name="AngleRad"></param>
        /// <returns></returns>
        public static double[][] RotationMatrixAbout0Axis_Jagged(double AngleRad)
        {
            double[][] Q = new double[3][];
            for (int i = 0; i < 3; i++)
            {
                Q[i] = new double[3];
            }
            Q[0][0] = 1.0d;
            Q[1][1] = Math.Cos(AngleRad);
            Q[2][2] = Q[1][1];
            Q[1][2] = -Math.Sin(AngleRad);
            Q[2][1] = -Q[1][2];
            return Q;
        }
        /// <summary>
        /// Calculate the rotation matrix for rotation about the 1-axis
        /// </summary>
        /// <param name="AngleRad"></param>
        /// <returns></returns>
        public static double[,] RotationMatrixAbout1Axis(double AngleRad)
        {
            double[,] Q = new double[3, 3];
            Q[1, 1] = 1.0d;
            Q[0, 0] = Math.Cos(AngleRad);
            Q[2, 2] = Q[0, 0];
            Q[0, 2] = Math.Sin(AngleRad);
            Q[2, 0] = -Q[0, 2];
            return Q;
        }
        /// <summary>
        /// Calculate the rotation matrix for rotation about the 1-axis
        /// </summary>
        /// <param name="AngleRad"></param>
        /// <returns></returns>
        public static double[][] RotationMatrixAbout1Axis_Jagged(double AngleRad)
        {
            double[][] Q = new double[3][];
            for (int i = 0; i < 3; i++)
            {
                Q[i] = new double[3];
            }
            Q[1][1] = 1.0d;
            Q[0][0] = Math.Cos(AngleRad);
            Q[2][2] = Q[0][0];
            Q[0][2] = Math.Sin(AngleRad);
            Q[2][0] = -Q[0][2];
            return Q;
        }
        /// <summary>
        /// Calculate the rotation matrix for rotation about the 2-axis
        /// </summary>
        /// <param name="AngleRad"></param>
        /// <returns></returns>
        public static double[,] RotationMatrixAbout2Axis(double AngleRad)
        {
            double[,] Q = new double[3, 3];
            Q[2, 2] = 1.0d;
            Q[0, 0] = Math.Cos(AngleRad);
            Q[1, 1] = Q[0, 0];
            Q[0, 1] = -Math.Sin(AngleRad);
            Q[1, 0] = -Q[0, 1];
            return Q;
        }
        /// <summary>
        /// Calculate the rotation matrix for rotation about the 2-axis
        /// </summary>
        /// <param name="AngleRad"></param>
        /// <returns></returns>
        public static double[][] RotationMatrixAbout2Axis_Jagged(double AngleRad)
        {
            double[][] Q = new double[3][];
            for (int i = 0; i < 3; i++)
            {
                Q[i] = new double[3];
            }
            Q[2][2] = 1.0d;
            Q[0][0] = Math.Cos(AngleRad);
            Q[1][1] = Q[0][0];
            Q[0][1] = -Math.Sin(AngleRad);
            Q[1][0] = -Q[0][1];
            return Q;
        }
        #endregion

        #region Make copy of array
        public static double[] Make_Copy(double[] Array1)
        {
            int ArrayLength = Array1.Length;
            double[] Array2 = new double[ArrayLength];
            for (int i = 0; i < ArrayLength; i++)
            {
                Array2[i] = Array1[i];
            }
            return Array2;
        }
        public static double[][] Make_Copy(double[][] Array1)
        {
            int ArrayLength = Array1.GetLength(0);
            double[][] Array2 = new double[ArrayLength][];
            for (int i = 0; i < ArrayLength; i++)
            {
                Array2[i] = Make_Copy(Array1[i]);
            }
            return Array2;
        }
        public static double[][][] Make_Copy(double[][][] Array1)
        {
            int ArrayLength = Array1.GetLength(0);
            double[][][] Array2 = new double[ArrayLength][][];
            for (int i = 0; i < ArrayLength; i++)
            {
                Array2[i] = Make_Copy(Array1[i]);
            }
            return Array2;
        }
        public static double[][][][] Make_Copy(double[][][][] Array1)
        {
            int ArrayLength = Array1.GetLength(0);
            double[][][][] Array2 = new double[ArrayLength][][][];
            for (int i = 0; i < ArrayLength; i++)
            {
                Array2[i] = Make_Copy(Array1[i]);
            }
            return Array2;
        }
        public static double[][][][][] Make_Copy(double[][][][][] Array1)
        {
            int ArrayLength = Array1.GetLength(0);
            double[][][][][] Array2 = new double[ArrayLength][][][][];
            for (int i = 0; i < ArrayLength; i++)
            {
                Array2[i] = Make_Copy(Array1[i]);
            }
            return Array2;
        }
        public static double[][][][][][] Make_Copy(double[][][][][][] Array1)
        {
            int ArrayLength = Array1.GetLength(0);
            double[][][][][][] Array2 = new double[ArrayLength][][][][][];
            for (int i = 0; i < ArrayLength; i++)
            {
                Array2[i] = Make_Copy(Array1[i]);
            }
            return Array2;
        }
        public static double[,] Make_Copy(double[,] Array1)
        {
            int ArrayLength0 = Array1.GetLength(0);
            int ArrayLength1 = Array1.GetLength(1);
            double[,] Array2 = new double[ArrayLength0, ArrayLength1];
            for (int i = 0; i < ArrayLength0; i++)
            {
                for (int j = 0; j < ArrayLength1; j++)
                {
                    Array2[i, j] = Array1[i, j];
                }
            }
            return Array2;
        }
        public static double[, ,] Make_Copy(double[, ,] Array1)
        {
            int ArrayLength0 = Array1.GetLength(0);
            int ArrayLength1 = Array1.GetLength(1);
            int ArrayLength2 = Array1.GetLength(2);
            double[, ,] Array2 = new double[ArrayLength0, ArrayLength1, ArrayLength2];
            for (int i = 0; i < ArrayLength0; i++)
            {
                for (int j = 0; j < ArrayLength1; j++)
                {
                    for (int k = 0; k < ArrayLength2; k++)
                    {
                        Array2[i, j, k] = Array1[i, j, k];
                    }
                }
            }
            return Array2;
        }
        public static double[, , ,] Make_Copy(double[, , ,] Array1)
        {
            int ArrayLength0 = Array1.GetLength(0);
            int ArrayLength1 = Array1.GetLength(1);
            int ArrayLength2 = Array1.GetLength(2);
            int ArrayLength3 = Array1.GetLength(3);
            double[, , ,] Array2 = new double[ArrayLength0, ArrayLength1, ArrayLength2, ArrayLength3];
            for (int i = 0; i < ArrayLength0; i++)
            {
                for (int j = 0; j < ArrayLength1; j++)
                {
                    for (int k = 0; k < ArrayLength2; k++)
                    {
                        for (int l = 0; l < ArrayLength3; l++)
                        {
                            Array2[i, j, k, l] = Array1[i, j, k, l];
                        }
                    }
                }
            }
            return Array2;
        }
        public static double[, , , ,] Make_Copy(double[, , , ,] Array1)
        {
            int ArrayLength0 = Array1.GetLength(0);
            int ArrayLength1 = Array1.GetLength(1);
            int ArrayLength2 = Array1.GetLength(2);
            int ArrayLength3 = Array1.GetLength(3);
            int ArrayLength4 = Array1.GetLength(4);
            double[, , , ,] Array2 = new double[ArrayLength0, ArrayLength1, ArrayLength2, ArrayLength3, ArrayLength4];
            for (int i = 0; i < ArrayLength0; i++)
            {
                for (int j = 0; j < ArrayLength1; j++)
                {
                    for (int k = 0; k < ArrayLength2; k++)
                    {
                        for (int l = 0; l < ArrayLength3; l++)
                        {
                            for (int m = 0; m < ArrayLength4; m++)
                            {
                                Array2[i, j, k, l, m] = Array1[i, j, k, l, m];
                            }
                        }
                    }
                }
            }
            return Array2;
        }
        public static double[, , , , ,] Make_Copy(double[, , , , ,] Array1)
        {
            int ArrayLength0 = Array1.GetLength(0);
            int ArrayLength1 = Array1.GetLength(1);
            int ArrayLength2 = Array1.GetLength(2);
            int ArrayLength3 = Array1.GetLength(3);
            int ArrayLength4 = Array1.GetLength(4);
            int ArrayLength5 = Array1.GetLength(5);
            double[, , , , ,] Array2 = new double[ArrayLength0, ArrayLength1, ArrayLength2, ArrayLength3, ArrayLength4, ArrayLength5];
            for (int i = 0; i < ArrayLength0; i++)
            {
                for (int j = 0; j < ArrayLength1; j++)
                {
                    for (int k = 0; k < ArrayLength2; k++)
                    {
                        for (int l = 0; l < ArrayLength3; l++)
                        {
                            for (int m = 0; m < ArrayLength4; m++)
                            {
                                for (int n = 0; n < ArrayLength5; n++)
                                {
                                    Array2[i, j, k, l, m, n] = Array1[i, j, k, l, m, n];
                                }
                            }
                        }
                    }
                }
            }
            return Array2;
        }
        #endregion
    }
}
