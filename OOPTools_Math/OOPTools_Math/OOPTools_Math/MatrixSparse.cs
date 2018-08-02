using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using OOPTools;

namespace OOPTools_Math
{
    [Serializable]
    public class MatrixSparse : MatrixObject
    {
        /// <summary>
        /// Sparce matrix liner equation solver
        /// Method: Biconjugate Gradient method or Gaussian Elimination
        /// Usage: Solving symmetric and non-symmetric systems Ax = b
        /// 
        /// Matrix storage: n Number of equations
        ///                 A double[n][NumberOfNonzeroInRow] is the coefficent matrix organized by row
        ///                 B int[n][NumberOfNonzeroInRow] is the column index of A[i][j]
        /// By: Mehrdad Negahban
        /// Date: 03-03-2007
        /// Modified: 03-03-2007
        /// </summary>
        //public int n; // Number of equations
        public double[][] A; // Coefficient Matrix
        public int[][] B; // Column index of each entry in A
        public int[] LastElementUsed;
        public SolvingMethod Solver;

        
        public MatrixSparse()
        {
            Solver = SolvingMethod.BiconjugateGradient;
        }

        public override double[] SolveLinearSystem(double[] RightHandSide)
        {
            StopWatchTimer timer = new StopWatchTimer();
            /*timer.StartTimer();
            this.ClearSmallNumbers(1.0E-15);
            timer.StopTimer();
            Console.Write("  -Time to remove zero from sparse = " + timer.TimeElapsedInSeconds().ToString() + "\n\n");
             * */
            switch (Solver)
            {
                case SolvingMethod.BiconjugateGradient:

                    SolveSparseMatrix solverBiconjugateGradient = new SolveSparseMatrix();
                    solverBiconjugateGradient.n = n;
                    solverBiconjugateGradient.A = A;
                    solverBiconjugateGradient.B = B;
                    return solverBiconjugateGradient.SolveLinearSystem(1.0E-7D, 10, RightHandSide);

                case SolvingMethod.GaussianElimination:
                    SolverSparseGaussianElimination solverGaussianElimination = new SolverSparseGaussianElimination();
                    solverGaussianElimination.A = this;
                    double Det;
                    double[] solution = solverGaussianElimination.SolveLinearSystem(RightHandSide, out Det);
                    return solution;

                default:

                    SolveSparseMatrix solver = new SolveSparseMatrix();
                    solver.n = n;
                    solver.A = A;
                    solver.B = B;
                    return solver.SolveLinearSystem(1.0E-7D, 10, RightHandSide);
            }
        }
        public void ClearElementsButSaveStructure()
        {
            for (int i = 0; i < n; i++)
            {
                int ne = A[i].Length;
                A[i] = new double[ne];
            }

        }
        
        public bool IsInArray(int Value, int[] Array)
        {
            bool exists = false;
            for (int i = 0; i < Array.Length; i++)
            {
                if (Array[i] == Value)
                {
                    exists = true;
                    break;
                }
            }
            return exists;
        }
        public int Count1InArray(int[] Array)
        {
            int count = 0;
            for (int i = 0; i < Array.Length; i++)
            {
                if (Array[i] == 1)
                {
                    count++;
                }
            }
            return count;
        }
        public void PutNonzeroColumnsInB(int[] x, int[] b)
        {
            int Index = 0;
            for (int i = 0; i < x.Length; i++)
            {
                if (x[i] == 1)
                {
                    b[Index] = i;
                    Index++;
                }
            }
        }


        public override void AddToMatrixElement(int row, int col, double value)
        {
            int Index = this.IndexOfValueInArray(col, B[row], ref LastElementUsed[row]);
            //LastElementUsed[row] = Index;
            if (Index > -1)
            {
                A[row][Index] += value;
            }
            else
            {
                WriteToDisplay.WriteInfo("Error in matrix input: Array location not in sparse matrix\n");
            }
        }
        public override void SetMatrixElement(int row, int col, double value)
        {
            int Index = this.IndexOfValueInArray(col, B[row], ref LastElementUsed[row]);
            //LastElementUsed[row] = Index;
            if (Index > -1)
            {
                A[row][Index] = value;
            }
            else
            {
                WriteToDisplay.WriteInfo("Error in matrix input: Array location not in sparse matrix\n");
            }
        }
        
        public int IndexOfValueInArray(int Value, int[] Array, ref int lastElementUsed)
        {
            return ArrayTools.FindIndexInArryBySecantMethod(Array, Value, 0, Array.Length - 1);
           // int Index = -1;
            /*
            if (Array[lastElementUsed] <= Value)
            {
                //return MNTools.ArrayTools.FindIndexInArryBySecantMethod(Array, Value, lastElementUsed, Array.Length - 1);
                
                for (int i = lastElementUsed; i < Array.Length; i++)
                {
                    if (Array[i] == Value)
                    {
                        //Index = i;
                        lastElementUsed = i;
                        return i;
                    }
                }
                 
            }
            else
            {
                //return MNTools.ArrayTools.FindIndexInArryBySecantMethod(Array, Value, 0, lastElementUsed);
               
                for (int i = 0; i < Array.Length; i++)
                {
                    if (Array[i] == Value)
                    {
                        //Index = i;
                        lastElementUsed = i;
                        return i;
                    }
                }
                 
            }
            return -1;// Index;
            */
        }
        public double GetMatrixElement(int row, int col)
        {
            int index = this.IndexOfValueInArray(col,B[row], ref LastElementUsed[row]);
            //LastElementUsed[row] = index;
            if(index>-1)
            {
                return A[row][index];
            }
            else
            {
                return 0.0d;// double.NaN;
            }
        }
        public void ClearSmallNumbers(double smallestValueToHold)
        {
            for (int i = 0; i < n; i++)
            {
                int numberOfZeros = this.CountZeros(A[i], smallestValueToHold);
                int newSize = A[i].Length - numberOfZeros;
                double[] newRowValues = new double[newSize];
                int[] newRow = new int[newSize];
                int index =0;
                for (int j = 0; j < A[i].Length; j++)
                {
                    if (!(Math.Abs(A[i][j]) < smallestValueToHold))
                    {
                        newRow[index] = B[i][j];
                        newRowValues[index] = A[i][j];
                        index++;
                    }
                }
                    B[i] = newRow;
                    A[i] = newRowValues;
            }
        }
        private int CountZeros(double[] array, double smallestValueToHold)
        {
            int zeros = 0;
            for (int i = 0; i < array.Length; i++)
            {
                if (Math.Abs(array[i]) < smallestValueToHold) zeros++;
            }
            return zeros;
        }
        public enum SolvingMethod
        {
            BiconjugateGradient,
            GaussianElimination
        }
    }

    class SolveSparseMatrix
    {
        /// <summary>
        /// Sparce matrix liner equation solver
        /// Method: Biconjugate gradient method adapted from Numerical Recipes
        /// Usage: Solving symmetric and non-symmetric systems Ax = b
        /// 
        /// Matrix storage: n Number of equations
        ///                 A double[n][NumberOfNonzeroInRow] is the coefficent matrix organized by row
        ///                 B int[n][NumberOfNonzeroInRow] is column index of A[i][j]
        /// By: Mehrdad Negahban
        /// Date: 03-03-2007
        /// Modified: 03-06-2007
        /// </summary>
        public int n; // Number of equations
        public double[][] A; // Coefficient Matrix
        public int[][] B; // Column index of each entry in A
        private SolveSparseMatrix M;// Matrix used for preconditioning 
        private double[] rightHandSide;
        public SolveSparseMatrix()
        {
        }
        private void ScaleAndRemoveZero()
        {
            //int n = A.n;
            double smallestValueToHold = 1.0E-14;
            double max = 0.0d;
            int[] Row;
            double[] RowValues;
            for (int i = 0; i < n; i++)
            {
                RowValues = A[i];
                Row = B[i];
                max = this.FindMax2(RowValues);
                for (int j = 0; j < Row.Length; j++)
                {
                    RowValues[j] /= max;
                }
                rightHandSide[i] /= max;
                /*
                if (Math.Abs(rightHandSide[i]) < smallestValueToHold)
                {
                    rightHandSide[i] = 0.0d;
                }
                 * */

                int numberOfZeros = this.CountZeros(RowValues, smallestValueToHold);
                int newSize = Row.Length - numberOfZeros;
                double[] newRowValues = new double[newSize];
                int[] newRow = new int[newSize];
                int index = 0;
                for (int j = 0; j < Row.Length; j++)
                {
                    if (!(Math.Abs(RowValues[j]) < smallestValueToHold))
                    {
                        newRow[index] = Row[j];
                        newRowValues[index] = RowValues[j];
                        index++;
                    }
                }
                B[i] = newRow;
                A[i] = newRowValues;
            }
        }
        private int CountZeros(double[] array, double smallestValueToHold)
        {
            int zeros = 0;
            for (int i = 0; i < array.Length; i++)
            {
                if (Math.Abs(array[i]) < smallestValueToHold)
                {
                    array[i] = 0.0d;
                    zeros++;
                }
            }
            return zeros;
        }
        private double FindMax2(double[] array)
        {
            double max = 0.0d;
            double absMax = 0.0d;
            double val = 0.0d;
            double absVal = 0.0d;
            for (int i = 0; i < array.Length; i++)
            {
                val = array[i];
                absVal = Math.Abs(val);
                if (absVal > absMax)
                {
                    absMax = absVal;
                    max = val;
                }
            }
            return max;
        }
       
        public double[] SolveLinearSystem(double error, int NumberOfRestarts, double[] RightHandSide)
        {
            /// Solves system Ax = b where A is a real matrix
            /// Uses preconditioning with largest value in each row
            /// error: The maximum error used to stop the iteration
            /// NumberOfRestart: number of times to reset residual to actual value r = b-Ax
            /// RightHandSide: b in Ax = b
            /// 

            rightHandSide = RightHandSide;
            this.ScaleAndRemoveZero();
            
            //Can add preconditioning- Note: need to change the initial guess
            //this.PreconditionMatrix();
            //RightHandSide = M.Ax(RightHandSide);
           
            double[] r;
            double[] rr;
            double[] Diag = this.DiagonalTerms();
            double[] x = this.xDivy(RightHandSide, Diag);//new double[n];// this.Equalx(RightHandSide); //this.Equalx(RightHandSide); //this.xDivy(RightHandSide, Diag);// Replace if you use preconditioning

            double err;
            error = error * Convert.ToDouble(n);

            double bNorm = this.Normx(RightHandSide);
            
            double bkNum;
            double bkDen = 1.0D;
            double bk;

            double akDen;
            double ak;

            double[] z;
            double[] zz = new double[n];
            double[] p = new double[n];
            double[] pp = new double[n];

            int m=0;
            err = 1.0D;

            r = Ax(x);
            r = this.xMinusy(RightHandSide, r);
            rr = this.Equalx(r);
            z =  this.xDivy(r, Diag);
            bool restart = true;
            int count = 0;
            ATransposex zzEqATpp = new ATransposex();
            zzEqATpp.SSM = this;
            for (int i = 0; i < 10*n; i++)
            {
                m = i;
                zz = this.xDivy(rr, Diag);
                bkNum = this.xDoty(z,rr);

                if (restart)
                {
                    p = this.Equalx(z);
                    pp = this.Equalx(zz);
                    restart = false;
                }
                else
                {
                    bk = bkNum / bkDen;
                    p = this.xPlusay(z, bk, p);
                    pp = this.xPlusay(zz, bk, pp);
                }

                zzEqATpp.x = pp;
                Thread ATpp = new Thread(new ThreadStart(zzEqATpp.yEqualsATx));
                ATpp.Start();

                bkDen = bkNum;
                z = Ax(p);

                akDen = this.xDoty(z,pp);
                ak = bkNum / akDen;
                //zz = ATransposex(pp);
                x = this.xPlusay(x, ak, p);
                r = this.xMinusay(r, ak, z);

                
                z = this.xDivy(r, Diag);
                err = this.Normx(r) / bNorm;
                
                ATpp.Join();
                zz = zzEqATpp.y;
                rr = this.xMinusay(rr, ak, zz);
                //Check error, restet residual and start again NumberOfRestarts times
                if (err <= error)
                {
                    if (count > NumberOfRestarts) break;
                    r = Ax(x);
                    r = this.xMinusy(RightHandSide, r);
                    rr = this.Equalx(r);
                    z = this.xDivy(r, Diag);
                    restart = true;
                    count ++;
                }
            }
            Console.Write("step = " + m.ToString() + "  Error = " + err.ToString("E3") + "\n");
            return x;
        }
        public int IndexOfTerm(int Value, int[] Array)
        {
            return ArrayTools.FindIndexInArryBySecantMethod(Array, Value, 0, Array.Length - 1);
            /*
            int Index = -1;
            for (int i = 0; i < Array.Length; i++)
            {
                if (Array[i] == Value)
                {
                    Index = i;
                    break;
                }
            }
            return Index;
             * */
        }
        private double[] DiagonalTerms()
        {
            double[] diag = new double[n];
            for (int i = 0; i < n; i++)
            {
                int idiag = this.IndexOfTerm(i, B[i]);
                diag[i] = A[i][idiag];
            }
            return diag;
        }
        private double[] xDivy(double[] x, double[] y)
        {
            double[] z = new double[n];
            for (int i = 0; i < n; i++)
            {
                z[i] = x[i] / y[i];
            }
            return z;
        }
        private double Normx(double[] x)
        {
            // Use either L2 or LInfinity norm
            return Math.Sqrt(this.xDoty(x,x));
            //return FindMax(x);
        }
        private double FindMax(double[] x)
        {
            double Max = Math.Abs(x[0]);
            for (int i = 1; i < x.Length; i++)
            {
                if (Math.Abs(x[i]) > Max) Max = Math.Abs(x[i]);
            }
            
            return Max;
        }
        private void PreconditionMatrix()
        {
            M = new SolveSparseMatrix();
            M.n = n;
            M.A = new double[n][];
            M.B = new int[n][];
            for (int i = 0; i < n; i++)
            {
                M.A[i] = new double[1];
                M.B[i] = new int[1];
                M.B[i][0] = i;
                M.A[i][0] = 1.0D / FindMaxXAndNormalize(A[i]);
            }

        }
        private double FindMaxXAndNormalize(double[] x)
        {
            double Max = Math.Abs(x[0]);
            for (int i = 1; i < x.Length; i++)
            {
                if (Math.Abs(x[i]) > Max) Max = Math.Abs(x[i]);
            }
            double Min = Max * 1.0E-20D;
            for (int i = 0; i < x.Length; i++)
            {
                if (Math.Abs(x[i]) < Min)
                {
                    x[i] = 0.0D;
                }
                else
                {
                    x[i] = x[i] / Max;
                }
            }
            return Max;
        }
       
        private double[] Ax(double[] x)
        {
            double[] y = new double[n];
            for (int i = 0; i < n; i++)
            {
                int ne = B[i].Length;
                for (int j = 0; j < ne; j++)
                {
                    y[i] += A[i][j] * x[B[i][j]];
                }
            }
            return y;
        }
        public double[] ATransposex(double[] x)
        {
            double[] y = new double[n];
            for (int i = 0; i < n; i++)
            {
                int ne = B[i].Length;
                for (int j = 0; j < ne; j++)
                {
                    y[B[i][j]] += A[i][j] * x[i];
                }
            }
            return y;
        }
        private double xDoty(double[] x, double[] y)
        {
            double z = 0.0D; ;
            for (int i = 0; i < n; i++)
            {
                z += x[i] * y[i];
            }
            return z;
        }
        private double[] Equalx(double[] x)
        {
            double[] z = new double[n];
            for (int i = 0; i < n; i++)
            {
                z[i] = x[i] ;
            }
            return z;
        }
        private double[] xPlusy(double[] x, double[] y)
        {
            double[] z = new double[n];
            for (int i = 0; i < n; i++)
            {
                z[i] = x[i] + y[i];
            }
            return z;
        }
        private double[] xMinusy(double[] x, double[] y)
        {
            double[] z = new double[n];
            for (int i = 0; i < n; i++)
            {
                z[i] = x[i] - y[i];
            }
            return z;
        }

        private double[] xPlusay(double[] x, double a, double[] y)
        {
            double[] z = new double[n];
            for (int i = 0; i < n; i++)
            {
                z[i] = x[i] + a * y[i];
            }
            return z;
        }
        private double[] xMinusay(double[] x, double a, double[] y)
        {
            double[] z = new double[n];
            for (int i = 0; i < n; i++)
            {
                z[i] = x[i] - a * y[i];
            }
            return z;
        }
    }
    class ATransposex
    {
        public SolveSparseMatrix SSM;
        public double[] y;
        public double[] x;
        public ATransposex()
        {
        }
        public void yEqualsATx()
        {
            y = SSM.ATransposex(x);
        }
    }
    class SolveSparseMatrix2
    {
        /// <summary>
        /// Sparce matrix liner equation solver
        /// Method: Conjugate gradient method
        /// Usage: Solving symmetric and non-symmetric systems Ax = b
        /// 
        /// Matrix storage: n Number of equations
        ///                 A double[n][NumberOfNonzeroInRow] is the coefficent matrix organized by row
        ///                 B int[n][NumberOfNonzeroInRow] is column index of A[i][j]
        /// By: Mehrdad Negahban
        /// Date: 03-03-2007
        /// Modified: 03-03-2007
        /// </summary>
        public int n; // Number of equations
        public double[][] A; // Coefficient Matrix
        public int[][] B; // Column index of each entry in A
        private SolveSparseMatrix2 M;
        public SolveSparseMatrix2()
        {


        }
        public double[] SolveSymmetricLinearSystem(double error, double[] xo, double[] RightHandSide)
        {
            /// Only use for solving systems Ax = b where A is symmetric 
            /// error: The maximum error used to stop the iteration
            /// xo: Initial guess
            /// RightHandSide: b in Ax = b
            double[] x = xo;
            double[] p = Ax(x);
            p = this.xMinusy(RightHandSide, p);
            double[] r = new double[n];
            double[] Ap;
            double a, b;
            double rold, rnew;

            rold = this.xDoty(p, p);
            Console.Write("step = 0-" + "  Error = " + rold.ToString() + "\n");
            for (int i = 0; i < n; i++)
            {
                Ap = Ax(p);
                a = rold / this.xDoty(p, Ap);
                x = this.xPlusay(x, a, p);
                if (i == 0)
                {
                    r = this.xMinusay(p, a, Ap);
                }
                else
                {
                    r = this.xMinusay(r, a, Ap);
                }
                rnew = xDoty(r, r);
                Console.Write("step = " + i.ToString() + "  Error = " + rnew.ToString() + "\n");
                if (rnew <= error) break;
                b = rnew / rold;
                p = this.xPlusay(r, b, p);

                rold = rnew;
            }
            return x;
        }
        public double[] SolveLinearSystem(double error, double[] xo, double[] RightHandSide)
        {
            /// Solves systems Ax = b where A is a real matrix
            /// Uses preconditioning with largest value in each row
            /// error: The maximum error used to stop the iteration
            /// xo: Initial guess
            /// RightHandSide: b in Ax = b
            this.PreconditionMatrix();
            RightHandSide = M.Ax(RightHandSide);
            //RightHandSide = ATransposex(RightHandSide);
            double[] x = this.Equalx(RightHandSide);// = xo;
            double bNorm = this.Normx(RightHandSide);
            double[] p = Ax(x);
            p = this.xMinusy(RightHandSide, p);

            //p = M.Ax(p);
            //p = M.ATransposex(p);
            p = this.ATransposex(p);
            double[] r = new double[n];
            double[] Ap;
            double a, b;
            b = 1.0D;
            double rold, rnew;

            rold = this.xDoty(p, p);
            if (rold == 0.0D) return x;
            double ro = rold;
            double rperNew, rperOld;
            rperNew = 1.0D;
            rperOld = 1.0D;
            rnew = 10.0D * error;
            int m = 0;
            int maxrecalc = n / 5;
            if (maxrecalc > 1000) maxrecalc = 1000;
            int recalc = 0;
            for (int i = 0; i < 2 * n; i++)
            {
                //Ap = M.Ax(p);
                Ap = Ax(p);
                a = rold / this.xDoty(Ap, Ap);
                x = this.xPlusay(x, a, p);
                //Ap = M.ATransposex(Ap);
                Ap = ATransposex(Ap);
                if (i == 0)
                {
                    r = this.xMinusay(p, a, Ap);
                }
                else
                {
                    if (recalc < maxrecalc)
                    {
                        r = this.xMinusay(r, a, Ap);
                    }
                    else
                    {
                        r = this.Ax(x);
                        r = xMinusy(RightHandSide, r);
                        //t = M.Ax(t);
                        //t = M.ATransposex(t);
                        r = ATransposex(r);
                        recalc = 0;
                    }
                }
                rnew = xDoty(r, r);
                rperNew = this.Normx(r) / bNorm;
                Console.Write("step = " + i.ToString() + "  Error = " + rperNew.ToString("E2") + "\n");
                m = i;
                //if (rnew < error && rold<error) break;
                if (rperNew < error && rperOld < error) break;
                b = rnew / rold;
                p = this.xPlusay(r, b, p);
                recalc++;
                rold = rnew;
                rperOld = rperNew;
            }
            Console.Write("step = " + m.ToString() + "  Error = " + b.ToString() + "\n");
            Console.Write("step = " + m.ToString() + "  Error = " + rold.ToString() + "\n");
            Console.Write("step = " + m.ToString() + "  Error = " + rnew.ToString() + "\n");
            return x;
        }
        private void PreconditionMatrix()
        {
            M = new SolveSparseMatrix2();
            M.n = n;
            M.A = new double[n][];
            M.B = new int[n][];
            for (int i = 0; i < n; i++)
            {
                M.A[i] = new double[1];
                M.B[i] = new int[1];
                M.B[i][0] = i;
                M.A[i][0] = 1.0D / FindMaxXAndNormalize(A[i]);
            }

        }
        private double FindMaxXAndNormalize(double[] x)
        {
            double Max = Math.Abs(x[0]);
            for (int i = 1; i < x.Length; i++)
            {
                if (Math.Abs(x[i]) > Max) Max = Math.Abs(x[i]);
            }
            double Min = Max * 1.0E-20D;
            for (int i = 0; i < x.Length; i++)
            {
                if (Math.Abs(x[i]) < Min)
                {
                    x[i] = 0.0D;
                }
                else
                {
                    x[i] = x[i] / Max;
                }
            }
            return Max;
        }
        public double[] SolveLinearSystem_Old(double error, double[] xo, double[] RightHandSide)
        {
            /// Solves systems Ax = b where A is real matrix 
            /// error: The maximum error used to stop the iteration
            /// xo: Initial guess
            /// RightHandSide: b in Ax = b
            double[] x = xo;
            double[] p = Ax(x);
            p = this.xMinusy(RightHandSide, p);
            p = this.ATransposex(p);
            double[] r = new double[n];
            double[] Ap;
            double a, b;
            double rold, rnew;

            rold = this.xDoty(p, p);
            rnew = rold;
            int m = 0;
            int recalc = 0;
            for (int i = 0; i < 2 * n; i++)
            {
                Ap = Ax(p);
                a = rold / this.xDoty(Ap, Ap);
                x = this.xPlusay(x, a, p);
                Ap = ATransposex(Ap);
                if (i == 0)
                {
                    r = this.xMinusay(p, a, Ap);
                }
                else
                {
                    if (recalc < 30)
                    {
                        r = this.xMinusay(r, a, Ap);
                    }
                    else
                    {
                        double[] t = this.Ax(x);
                        t = xMinusy(RightHandSide, t);
                        r = ATransposex(t);
                        recalc = 0;
                    }
                }
                rnew = xDoty(r, r);
                //Console.Write("step = " + i.ToString() + "  Error = " + rnew.ToString() + "\n");
                m = i;
                if (rnew <= error) break;
                b = rnew / rold;
                p = this.xPlusay(r, b, p);
                recalc++;
                rold = rnew;
            }
            Console.Write("step = " + m.ToString() + "  Error = " + rnew.ToString() + "\n");
            return x;
        }
        private double[] Ax(double[] x)
        {
            double[] y = new double[n];
            for (int i = 0; i < n; i++)
            {
                int ne = B[i].Length;
                for (int j = 0; j < ne; j++)
                {
                    y[i] += A[i][j] * x[B[i][j]];
                }
            }
            return y;
        }
        private double[] ATransposex(double[] x)
        {
            double[] y = new double[n];
            for (int i = 0; i < n; i++)
            {
                int ne = B[i].Length;
                for (int j = 0; j < ne; j++)
                {
                    y[B[i][j]] += A[i][j] * x[i];
                }
            }
            return y;
        }
        private double[] Equalx(double[] x)
        {
            double[] z = new double[n];
            for (int i = 0; i < n; i++)
            {
                z[i] = x[i];
            }
            return z;
        }
        private double Normx(double[] x)
        {
            return Math.Sqrt(this.xDoty(x, x));
        }
        private double xDoty(double[] x, double[] y)
        {
            double z = 0.0D; ;
            for (int i = 0; i < n; i++)
            {
                z += x[i] * y[i];
            }
            return z;
        }
        private double[] xPlusy(double[] x, double[] y)
        {
            double[] z = new double[n];
            for (int i = 0; i < n; i++)
            {
                z[i] = x[i] + y[i];
            }
            return z;
        }
        private double[] xMinusy(double[] x, double[] y)
        {
            double[] z = new double[n];
            for (int i = 0; i < n; i++)
            {
                z[i] = x[i] - y[i];
            }
            return z;
        }

        private double[] xPlusay(double[] x, double a, double[] y)
        {
            double[] z = new double[n];
            for (int i = 0; i < n; i++)
            {
                z[i] = x[i] + a * y[i];
            }
            return z;
        }
        private double[] xMinusay(double[] x, double a, double[] y)
        {
            double[] z = new double[n];
            for (int i = 0; i < n; i++)
            {
                z[i] = x[i] - a * y[i];
            }
            return z;
        }
    }
    public class SolverSparseGaussianElimination
    {
        /// <summary>
        /// Solve linear system Ax=c
        /// Using partial pivoting
        /// 
        /// By: Mehrdad Negahban
        /// Date: 8/14/2007
        /// </summary>
        public MatrixSparse A;
        private double[] rightHandSide;
        private double det = 1.0d;
        private int[] RowOrder;
        private double small;
        public SolverSparseGaussianElimination()
        {
            small = 1.0E-150d;
        }
        private int[] SetupThredingArray(int First, int Last)
        {
            //Optimize calculation for threading
            int numb = Last - First + 1;
            int[] Intervals;
            if (numb < 501)
            {
                Intervals = new int[2];
                Intervals[0] = First;
                Intervals[1] = Last;
            }
            else if (numb < 2001)
            {
                int numbInt = numb / 500;
                Intervals = new int[numbInt + 1];

                for (int i = 0; i < numbInt; i++)
                {
                    Intervals[i] = First + i * 500;
                }
                Intervals[numbInt] = Last;
            }
            else if (numb < 10001)
            {
                int numbInt = numb / 1000;
                Intervals = new int[numbInt + 1];

                for (int i = 0; i < numbInt; i++)
                {
                    Intervals[i] = First + i * 1000;
                }
                Intervals[numbInt] = Last;
            }
            else
            {
                int numbInt = numb / 2000;
                Intervals = new int[numbInt + 1];

                for (int i = 0; i < numbInt; i++)
                {
                    Intervals[i] = First + i * 2000;
                }
                Intervals[numbInt] = Last;
            }
            return Intervals;

        }
        private void ScaleAndRemoveZero()
        {
            int n = A.n;
            double smallestValueToHold = small;
            double max = 0.0d;
            int[] Row;
            double[] RowValues;
            for (int i = 0; i < n; i++)
            {
                RowValues = A.A[i];
                Row = A.B[i];
                max = this.FindMax(RowValues);
                for (int j = 0; j < Row.Length; j++)
                {
                    RowValues[j] /= max;
                }
                rightHandSide[i] /= max;
                /*
                if (Math.Abs(rightHandSide[i]) < smallestValueToHold)
                {
                    rightHandSide[i] = 0.0d;
                }
                 * */

                int numberOfZeros = this.CountZeros(RowValues, smallestValueToHold);
                int newSize = Row.Length - numberOfZeros;
                double[] newRowValues = new double[newSize];
                int[] newRow = new int[newSize];
                int index = 0;
                for (int j = 0; j < Row.Length; j++)
                {
                    if (!(Math.Abs(RowValues[j]) < smallestValueToHold))
                    {
                        newRow[index] = Row[j];
                        newRowValues[index] = RowValues[j];
                        index++;
                    }
                }
                A.B[i] = newRow;
                A.A[i] = newRowValues;
            }
        }
        private void ScaleAndRemoveZeroPivot(int rowIndex)
        {
            double smallestValueToHold = small;
            
            int index = RowOrder[rowIndex]; 
            int[] Row = A.B[index];
            double[] RowValues = A.A[index];
            double max = A.GetMatrixElement(index, rowIndex);// this.FindMax(RowValues);
            for (int j = 0; j < Row.Length; j++)
            {
                RowValues[j] /= max;
            }
            rightHandSide[index] /= max;
            /*
            if (rightHandSide[index] < smallestValueToHold)
            {
               // rightHandSide[index] = 0.0d;
            }
             * */
            int numberOfZeros = this.CountZeros(RowValues, smallestValueToHold);
            int newSize = Row.Length - numberOfZeros;
            double[] newRowValues = new double[newSize];
            int[] newRow = new int[newSize];
            int index2 = 0;
            for (int j = 0; j < Row.Length; j++)
            {
                if (!(Math.Abs(RowValues[j]) < smallestValueToHold))
                {
                    newRow[index2] = Row[j];
                    newRowValues[index2] = RowValues[j];
                    index2++;
                }
            }

            A.B[index] = newRow;
            A.A[index] = newRowValues;

        }
        private int CountZeros(double[] array, double smallestValueToHold)
        {
            int zeros = 0;
            for (int i = 0; i < array.Length; i++)
            {
                if (Math.Abs(array[i]) < smallestValueToHold)
                {
                    array[i] = 0.0d;
                    zeros++;
                }
            }
            return zeros;
        }
        private double FindMax(double[] array)
        {
            double max = 0.0d;
            double absMax = 0.0d;
            double val = 0.0d;
            double absVal = 0.0d;
            for (int i = 0; i < array.Length; i++)
            {
                val = array[i];
                absVal = Math.Abs(val);
                if (absVal > absMax)
                {
                    absMax = absVal;
                    max = val;
                }
            }
            return max;
        }
        public double[] SolveLinearSystem(double[] RightHandSide, out double Determinant)
        {


            StopWatchTimer timer = new StopWatchTimer();
            StopWatchTimer timer2 = new StopWatchTimer();
            rightHandSide = RightHandSide;
            int n = A.n;

            Console.Write(" Sparse matrix solver: Gaussian Elimination- " + n.ToString() + " Equations \n\n");

            timer.StartTimer();
            this.ScaleAndRemoveZero();
            timer.StopTimer();
            Console.Write("  -Time to scale and remove zero from sparse = " + timer.TimeElapsedInSeconds().ToString() + "\n\n");

            // Initialize row order
            RowOrder = new int[n];
            for (int i = 0; i < n; i++)
            {
                RowOrder[i] = i;
            }

            // Forward Elimination
            timer.StartTimer();
            for (int i = 0; i < n - 1; i++)
            {
                //Console.Write("    *Forward elimination row = " + i.ToString() + "\n");
                // timer2.StartTimer();
                int PivotRow = FindNewPivot(i);
                // timer2.StopTimer();
                //Console.Write("     -Find new pivot time = " + timer2.TimeElapsedInSeconds().ToString() + "\n");
                if (PivotRow > -1)
                {
                    SwapRow(i, PivotRow);
                }
                //
                
                this.ScaleAndRemoveZeroPivot(i);
                //timer2.StartTimer();
                int numb = n - i - 1;
                if (true)// (numb < 1000)
                {
                    for (int j = i + 1; j < n; j++)
                    {

                        SubtractPivotRowToRemovePivotColumn(i, j);
                    }
                }
                else
                {
                    int[] Intervals = this.SetupThredingArray(i + 1, n - 1);
                    int numbInt = Intervals.Length;
                    Thread[] Ass = new Thread[numbInt - 1];
                    for (int j = 0; j < numbInt - 1; j++)
                    {
                        SubtractPivot SP = new SubtractPivot();

                        Ass[j] = new Thread(new ThreadStart(SP.Subtract));
                        SP.A = A;
                        SP.RowOrder = RowOrder;
                        SP.rightHandSide = rightHandSide;
                        SP.PivotRowNumber = i;
                        SP.FirstRowNumber = Intervals[j];
                        if (j == numbInt - 2)
                        {
                            SP.LastRowNumber = Intervals[j + 1];
                        }
                        else
                        {
                            SP.LastRowNumber = Intervals[j + 1] - 1;
                        }
                        Ass[j].Start();
                        //SubtractPivotRowToRemovePivotColumn(i, j);
                    }
                    for (int j = 0; j < numbInt - 1; j++)
                    {
                        Ass[j].Join();
                    }
                }

                //timer2.StopTimer();
                // Console.Write("     -Subtract pivot row time = " + timer2.TimeElapsedInSeconds().ToString() + "\n");
            }
            timer.StopTimer();
            Console.Write("   -Time for forward elimination = " + timer.TimeElapsedInSeconds().ToString() + "\n");

            //Calculate Determinant
            timer.StartTimer();
            Determinant = det * A.A[RowOrder[0]][0];
            for (int i = 1; i < n; i++)
            {
                Determinant *= A.A[RowOrder[i]][0];
            }
            timer.StopTimer();
            Console.Write("   -Time for calculating determinant = " + timer.TimeElapsedInSeconds().ToString() + "\n");

            if (Math.Abs(Determinant) < 1.0E-20)
            {
                Console.Write("\n      *** Matrix Singular, Determinant = " + Determinant.ToString() + " ****\n\n");
                //Console.Read();
            }

            // Back Substitution
            timer.StartTimer();
            rightHandSide[RowOrder[n - 1]] = rightHandSide[RowOrder[n - 1]] / A.A[RowOrder[n - 1]][0];
            for (int i = n - 2; i > -1; i--)
            {
                int index = RowOrder[i];
                int n2 = A.B[index].Length;
                for (int j = 1; j < n2; j++)
                {
                    rightHandSide[index] -= A.A[index][j] * rightHandSide[RowOrder[A.B[index][j]]];
                }
                rightHandSide[index] = rightHandSide[index] / A.A[index][0];
            }
            double[] newRHS = new double[n];
            for (int i = 0; i < n; i++)
            {
                newRHS[i] = rightHandSide[RowOrder[i]];
            }
            rightHandSide = newRHS;
            timer.StopTimer();
            Console.Write("   -Time for backward substitution = " + timer.TimeElapsedInSeconds().ToString() + "\n\n");
            Console.Write("\n    * Determinant = " + Determinant.ToString() + "\n\n");
            return rightHandSide;
        }
        private void SwapRow(int Index1, int Index2)
        {
            if (!(Index1 == Index2))
            {
                int temp = RowOrder[Index1];
                RowOrder[Index1] = RowOrder[Index2];
                RowOrder[Index2] = temp;
                det = -det;
            }
        }
        private int FindNewPivot(int CurrentPivotRow)
        {
            int n = A.n;
            int PivotIndex = -1;
            double max = 0.0d;
            for (int i = CurrentPivotRow; i < n; i++)
            {
                double value = A.GetMatrixElement(RowOrder[i], CurrentPivotRow);
                if (!(value == double.NaN))
                {
                    value = Math.Abs(value);
                    if (max < value)
                    {
                        max = value;
                        PivotIndex = i;
                    }
                }
            }
            return PivotIndex;
        }
        private void SubtractPivotRowToRemovePivotColumn(int PivotRowIndex, int RowIndex)
        {
            int rowIndex = RowOrder[RowIndex];
            int[] Row = A.B[rowIndex];
            if (A.IndexOfValueInArray(PivotRowIndex, Row, ref A.LastElementUsed[rowIndex]) > -1)
            {
                int pivetIndex = RowOrder[PivotRowIndex];
                int[] PivotRow = A.B[pivetIndex];
                double[] PivotRowValues = A.A[pivetIndex];
                double[] RowValues = A.A[rowIndex];

                double factor = -A.GetMatrixElement(rowIndex, PivotRowIndex) / A.GetMatrixElement(pivetIndex, PivotRowIndex);

                int[] newRow;
                double[] newRowValues;
                this.CreateNewRowTopologyForNewRowAfterPivotElimination(PivotRowIndex, RowIndex, out newRowValues, out newRow);
                int nRow = Row.Length;
                int nPivotRow = PivotRow.Length;
                int ind1 = 1;
                int ind2 = 1;
                int ind = 0;
                int nNew = newRow.Length;

                for (int i = 0; i < nNew; i++)
                {
                    if ((ind1 < nPivotRow) && (ind2 < nRow))
                    {
                        if (PivotRow[ind1] < Row[ind2])
                        {
                            newRow[ind] = PivotRow[ind1];
                            newRowValues[ind] = factor * PivotRowValues[ind1];
                            ind1++;
                            ind++;
                        }
                        else if (PivotRow[ind1] > Row[ind2])
                        {
                            newRow[ind] = Row[ind2];
                            newRowValues[ind] = RowValues[ind2];
                            ind2++;
                            ind++;
                        }
                        else
                        {
                            newRow[ind] = Row[ind2];
                            newRowValues[ind] = RowValues[ind2] + factor * PivotRowValues[ind1];
                            ind1++;
                            ind2++;
                            ind++;
                        }
                    }
                    else if ((ind1 < nPivotRow))
                    {
                        newRow[ind] = PivotRow[ind1];
                        newRowValues[ind] = factor * PivotRowValues[ind1];
                        ind1++;
                        ind++;
                    }
                    else if ((ind2 < nRow))
                    {
                        newRow[ind] = Row[ind2];
                        newRowValues[ind] = RowValues[ind2];
                        ind2++;
                        ind++;
                    }
                    else
                    {
                        goto end;
                    }
                }
            end: ;

                A.B[rowIndex] = newRow;
                A.A[rowIndex] = newRowValues;
                rightHandSide[rowIndex] += rightHandSide[pivetIndex] * factor;


            }
        }
        private void CreateNewRowTopologyForNewRowAfterPivotElimination(int PivotRowIndex, int RowIndex, out double[] values, out int[] columnIndex)
        {
            int[] PivotRow = A.B[RowOrder[PivotRowIndex]];
            int[] Row = A.B[RowOrder[RowIndex]];
            int size = this.CountNumberOfElementsInAddition(PivotRow, Row);
            values = new double[size - 1];
            columnIndex = new int[size - 1];


        }
        /*
        private int[] MakeElementsOfRowAddition(int[] PivotArray, int[] OtherArray, int[] CombinedIndex)
        {
            // count number of elements in array after pivot column removal
            int nPivot = PivotArray.Length;
            int nOther = OtherArray.Length;
            int startIndex = 0;
            int newElements = 0;
            for (int i = 0; i < nOther; i++)
            {
                for (int j = startIndex; j < nPivot; i++)
                {
                    if (OtherArray[i] == PivotArray[j])
                    {
                        startIndex = j;
                        goto next;
                    }
                    else if ((OtherArray[i] > PivotArray[j]) && (OtherArray[i] < PivotArray[j + 1]))
                    {
                        newElements++;
                        startIndex = j;
                        goto next;
                    }
                }
                if (OtherArray[i] > PivotArray[nPivot - 1])
                {
                    newElements++;
                }

            next: ;

            }
            return nPivot + newElements;
        }
         * */
        private int CountNumberOfElementsInAddition(int[] PivotArray, int[] OtherArray)
        {
            // count number of elements in array after pivot column removal
            int nPivot = PivotArray.Length;
            int nOther = OtherArray.Length;
            int startIndex = 0;
            int newElements = 0;
            for (int i = 0; i < nOther; i++)
            {
                for (int j = startIndex; j < nPivot - 1; j++)
                {
                    if (OtherArray[i] == PivotArray[j])
                    {
                        startIndex = j;
                        goto next;
                    }
                    else if ((OtherArray[i] > PivotArray[j]) && (OtherArray[i] < PivotArray[j + 1]))
                    {
                        newElements++;
                        startIndex = j;
                        goto next;
                    }
                }
                if (OtherArray[i] > PivotArray[nPivot - 1])
                {
                    newElements++;
                }

            next: ;

            }
            return nPivot + newElements;
        }
        private void AddIndexToIndexArray(int[] IndexArray, int[] index)
        {
            int n = IndexArray.Length;
            int index1 = 0;
            for (int i = 0; i < n - 1; i++)
            {
                if (index[index1] > IndexArray[i])
                {
                    if (index[index1] < IndexArray[i + 1])
                    {
                        this.InsertNewTermAtIndexInArray(IndexArray, index[index1], i + 1);
                        index1++;
                    }
                }

            }
        }
        private void InsertNewTermAtIndexInArray(int[] array, int value, int indexToInsertAt)
        {
            // Note last term is lost!!!!
            int n = array.Length;
            // Shift items forward, drop last item
            for (int i = n - 2; i > indexToInsertAt - 1; i--)
            {
                array[i + 1] = array[i];
            }
            array[indexToInsertAt] = value;
        }



    }
    public class SubtractPivot
    {
        public MatrixSparse A;
        public double[] rightHandSide;
        public int[] RowOrder;
        public int PivotRowNumber;
        public int FirstRowNumber;
        public int LastRowNumber;


        public void Subtract()
        {
            for (int i = FirstRowNumber; i < LastRowNumber + 1; i++)
            {
                this.SubtractPivotRowToRemovePivotColumn(PivotRowNumber, i);
            }
        }
        private void SubtractPivotRowToRemovePivotColumn(int PivotRowIndex, int RowIndex)
        {

            int[] Row = A.B[RowOrder[RowIndex]];
            if (A.IndexOfValueInArray(PivotRowIndex, Row, ref A.LastElementUsed[RowOrder[RowIndex]]) > -1)
            {
                int[] PivotRow = A.B[RowOrder[PivotRowIndex]];
                double[] PivotRowValues = A.A[RowOrder[PivotRowIndex]];
                double[] RowValues = A.A[RowOrder[RowIndex]];

                double factor = -A.GetMatrixElement(RowOrder[RowIndex], PivotRowIndex) / A.GetMatrixElement(RowOrder[PivotRowIndex], PivotRowIndex);

                int[] newRow;
                double[] newRowValues;
                this.CreateNewRowTopologyForNewRowAfterPivotElimination(PivotRowIndex, RowIndex, out newRowValues, out newRow);
                int nRow = Row.Length;
                int nPivotRow = PivotRow.Length;
                int ind1 = 1;
                int ind2 = 1;
                int ind = 0;
                int nNew = newRow.Length;

                for (int i = 0; i < nNew; i++)
                {
                    if ((ind1 < nPivotRow) && (ind2 < nRow))
                    {
                        if (PivotRow[ind1] < Row[ind2])
                        {
                            newRow[ind] = PivotRow[ind1];
                            newRowValues[ind] = factor * PivotRowValues[ind1];
                            ind1++;
                            ind++;
                        }
                        else if (PivotRow[ind1] > Row[ind2])
                        {
                            newRow[ind] = Row[ind2];
                            newRowValues[ind] = RowValues[ind2];
                            ind2++;
                            ind++;
                        }
                        else
                        {
                            newRow[ind] = Row[ind2];
                            newRowValues[ind] = RowValues[ind2] + factor * PivotRowValues[ind1];
                            ind1++;
                            ind2++;
                            ind++;
                        }
                    }
                    else if ((ind1 < nPivotRow))
                    {
                        newRow[ind] = PivotRow[ind1];
                        newRowValues[ind] = factor * PivotRowValues[ind1];
                        ind1++;
                        ind++;
                    }
                    else if ((ind2 < nRow))
                    {
                        newRow[ind] = Row[ind2];
                        newRowValues[ind] = RowValues[ind2];
                        ind2++;
                        ind++;
                    }
                    else
                    {
                        goto end;
                    }
                }
            end: ;

                A.B[RowOrder[RowIndex]] = newRow;
                A.A[RowOrder[RowIndex]] = newRowValues;
                rightHandSide[RowOrder[RowIndex]] += rightHandSide[RowOrder[PivotRowIndex]] * factor;


            }
        }
        private void CreateNewRowTopologyForNewRowAfterPivotElimination(int PivotRowIndex, int RowIndex, out double[] values, out int[] columnIndex)
        {
            int[] PivotRow = A.B[RowOrder[PivotRowIndex]];
            int[] Row = A.B[RowOrder[RowIndex]];
            int size = this.CountNumberOfElementsInAddition(PivotRow, Row);
            values = new double[size - 1];
            columnIndex = new int[size - 1];


        }
        private int CountNumberOfElementsInAddition(int[] PivotArray, int[] OtherArray)
        {
            // count number of elements in array after pivot column removal
            int nPivot = PivotArray.Length;
            int nOther = OtherArray.Length;
            int startIndex = 0;
            int newElements = 0;
            for (int i = 0; i < nOther; i++)
            {
                for (int j = startIndex; j < nPivot - 1; j++)
                {
                    if (OtherArray[i] == PivotArray[j])
                    {
                        startIndex = j;
                        goto next;
                    }
                    else if ((OtherArray[i] > PivotArray[j]) && (OtherArray[i] < PivotArray[j + 1]))
                    {
                        newElements++;
                        startIndex = j;
                        goto next;
                    }
                }
                if (OtherArray[i] > PivotArray[nPivot - 1])
                {
                    newElements++;
                }

            next: ;

            }
            return nPivot + newElements;
        }
    }

}
