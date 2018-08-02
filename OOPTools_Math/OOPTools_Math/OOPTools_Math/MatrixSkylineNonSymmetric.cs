using System;
using System.Collections.Generic;
using System.Text;

namespace OOPTools_Math
{
    [Serializable]
    public class MatrixSkylineNonSymmetric :  MatrixObject 
    {
        //public int n; // Number of equations
        public double[][] U; // Coefficient Matrix
        public double[][] L; 
      
        public MatrixSkylineNonSymmetric()
        {
        }

        public override double[] SolveLinearSystem(double[] RightHandSide)
        {
            SolveSkylineNonSymmetric solver = new SolveSkylineNonSymmetric();
            solver.Solver(n, ref L, ref U, ref RightHandSide);

            return RightHandSide;
        }


        public override void AddToMatrixElement(int row, int col, double value)
        {
            if (row > col)
            {
                int w = L[row].Length;
                L[row][col-(row-w+1)] += value;
            }
            else if (row < col)
            {
                int h = U[col].Length;
                U[col][row-(col-h+1)] += value;
            }
            else
            {
                U[col][U[col].Length - 1] += value;
                L[row][L[row].Length - 1] += value;
            }
        }
        public override void SetMatrixElement(int row, int col, double value)
        {
            if (row > col)
            {
                int w = L[row].Length;
                L[row][col - (row - w + 1)] = value;
            }
            else if (row < col)
            {
                int h = U[col].Length;
                U[col][row - (col - h + 1)] = value;
            }
            else
            {
                U[col][U[col].Length - 1] = value;
                L[row][L[row].Length - 1] = value;
            }
        }
    }
    public class SolveSkylineNonSymmetric
    {
        public SolveSkylineNonSymmetric()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public void Solver(int n, ref double[][] L, ref double[][] U, ref double[] x )
        {
            ///<Summary>
            ///Solver for a Skyline stored non-symmetric matrix A
            ///</Summary>
            ///<Author>
            ///Mehrdad Negahban
            ///</Author>
            ///<Started>
            ///6-15-05
            ///</Started>
            ///<Modified>
            ///6-17-05
            ///</Modified>

            /*	

                L[i][k]		Lower triangular part of Aij with Uii stored on diagonal; 
                            i=row, j=column, k=j-startIndexL
                            startIndexL=i-L[i].Length+1=location of first non-zero column 
                            L[i][L[i].Length-1]=diagonal term=Aii 
             *  Storage structure L[i][k] for k=0,1,2,3,4
             *     *
             *       *
             *         *
             *           *
             *             *
             *               *
             *   i     0 1 2 3 4
             *                   *
             *                      *
                U[j][m]		Upper triangular part of A including diagonal
                            j=column, i=row, m=i-startIndexU 
                            startIndexU=j-U[j].Length+1=location of first non-zero row
             *   Storage structure U[j][m] for m=0,1,2,3,4
             *                 j
             *     *
             *       *
             *         *       0
             *           *     1
             *             *   2
             *               * 3
             *                 4
             *                   *
             *                      *
                x			Right-hand-side on call, solution on return
                n			number of rows/columns
			
            */
            this.Precondition(n, L, U, x);
            L[0][0] = 1.0D;
            for (int i = 1; i < n; i++)
            {
                int startIndexLi = i - L[i].Length + 1;
                int startIndexUi = i - U[i].Length + 1;
                int iIndex = i - startIndexUi;

                for (int j = 0; j < i; j++)
                {
                    int startIndexUj = j - U[j].Length + 1;
                    int jIndex = j - startIndexLi;
                    if (jIndex > -1)
                    {
                        L[i][jIndex] = (L[i][jIndex] - DotSky1(ref L[i], ref U[j], i, j, j - 1)) / U[j][U[j].Length - 1];
                    }

                    //int startIndexLj = j-L[j].Length+1;
                    if (iIndex > -1)
                    {
                        U[i][iIndex] -= DotSky1(ref L[j], ref U[i], j, i, j - 1);
                    }
                }
                L[i][L[i].Length - 1] = 1.0D;
                U[i][U[i].Length - 1] -= DotSky1(ref L[i], ref U[i], i, i, i - 1);
            }

            //	Forward substitution

            for (int i = 1; i < n; i++)
            {
                x[i] -= DotSky2(ref L[i], ref x, i, i - 1);
            }

            //	Back substitution

            x[n - 1] = x[n - 1] / U[n - 1][U[n - 1].Length - 1];
            for (int i = n - 2; i > -1; i--)
            {
                x[i] = (x[i] - DotSky3(ref U, ref x, i, i + 1, n - 1)) / U[i][U[i].Length - 1];
            }

        }

        private double DotSky1(ref double[] L, ref double[] U, int i, int j, int m)
        {
            // Sum from 0 to m over k of L_ik*U_kj 

            int startIndexL = i - L.Length + 1;
            int startIndexU = j - U.Length + 1;

            int s = startIndexL;
            if (s < startIndexU) s = startIndexU;


            double Dot = 0.0D;
            for (int k = s; k < m + 1; k++)
            {
                Dot += L[k - startIndexL] * U[k - startIndexU];
            }
            return Dot;
        }
        private double DotSky2(ref double[] L, ref double[] x, int i, int m)
        {
            // Sum from 0 to m over j of L_ij*x_j in forward substitution

            int startIndexL = i - L.Length + 1;

            double Dot = 0.0D;
            for (int j = startIndexL; j < m + 1; j++)
            {
                Dot += L[j - startIndexL] * x[j];
            }
            return Dot;
        }
        private double DotSky3(ref double[][] U, ref double[] x, int i, int m, int n)
        {
            // Sum from m to n over j of U_ij*x_j in back substitution

            double Dot = 0.0D;
            for (int j = m; j < n + 1; j++)
            {
                // starting row of U skyline = j-U[j].Length+1
                int index = i - (j - U[j].Length + 1);
                if (index > -1)
                {
                    Dot += U[j][index] * x[j];
                }
            }
            return Dot;
        }
        private void Precondition(int n, double[][] L, double[][] U, double[] x)
        {
            double[] M = new double[n];
            double MinFactor = 1.0E-20D;
            for (int i = 0; i < n; i++)
            {
                M[i] = U[i][U[i].Length - 1];
                int nLe = L[i].Length;
                double Min = M[i] * MinFactor;
                if (Math.Abs(x[i]) < Min )
                {
                    x[i] = 0.0D;
                }
                else
                {
                    x[i] = x[i] / M[i];
                }
                for (int j = 0; j < nLe; j++)
                {
                    if (Math.Abs(L[i][j]) < Min)
                    {
                        L[i][j] = 0.0D;
                    }
                    else
                    {
                        L[i][j] = L[i][j] / M[i];
                    }
                }
            }
            for (int i = 0; i < n; i++)
            {
                int nUe = U[i].Length;
                int startIndexU = i - nUe + 1;
                for (int j = 0; j < nUe; j++)
                {
                    int Index = startIndexU + j;
                    if (U[i][j] < M[Index] * MinFactor)
                    {
                        U[i][j] = 0.0D;
                    }
                    else
                    {
                        U[i][j] = U[i][j] / M[Index];
                    }
                }
            }

        }
    }


}
