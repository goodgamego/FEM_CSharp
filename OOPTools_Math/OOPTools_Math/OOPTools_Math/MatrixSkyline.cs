using System;
using System.Collections.Generic;
using System.Text;

namespace OOPTools_Math
{
    
    [Serializable]
    public class MatrixSkyline : MatrixObject
    {
        // public int n; // Number of equations
        public double[] A; // Coefficient Matrix
        public int[] JDiag; //Pointer for diagonal terms in A

        public MatrixSkyline()
        {
        }
        public override double[] SolveLinearSystem(double[] RightHandSide)
        {
            SolveSkylineMatrix solver = new SolveSkylineMatrix();
            solver.Solver(n, JDiag, A, RightHandSide, 0);
            return RightHandSide;
        }
        public double[] SolveLinearSystemLUDecomp(double[] RightHandSide)
        {
            SolveSkylineMatrix solver = new SolveSkylineMatrix();
            solver.Solver(n, JDiag, A, RightHandSide, 1);
            return RightHandSide;
        }
        public double[] SolveLinearSystemBackSub(double[] RightHandSide)
        {
            SolveSkylineMatrix solver = new SolveSkylineMatrix();
            solver.Solver(n, JDiag, A, RightHandSide, 2);
            return RightHandSide;
        }
        public override void AddToMatrixElement(int row, int col, double value)
        {
            if (row > col)
            {
                //A[JDiag[row] - (row - col)] += value;
            }
            else
            {
                A[JDiag[col] - (col - row)] += value;
            }
        }
        public override void SetMatrixElement(int row, int col, double value)
        {
            if (row > col)
            {
                //A[JDiag[row] - (row - col)] += value;
            }
            else
            {
                A[JDiag[col] - (col - row)] = value;
            }
        }
    }
    public class SolveSkylineMatrix
    {
        public SolveSkylineMatrix()
        {
        }

        public void Solver(int n, int[] JDiag, double[] A, double[] b, int kkk)
        {


            /// <summary> 
            /// Solves a linear system of equations where the coefficent matrix is in Skyline storage form
            /// Mehrdad Negahban (Adapted from R.L. Taylor)
            /// Started: 6-2-05
            /// Modified:
            /// 
            /// ***********************************************************************************
            /// 
            /// A		Coefficient Matrix
            /// JDiag	Pointer for diagonal terms in A
            /// b		Right hand side on input, solution on output
            /// n		Number of equations
            /// kkk		=0	Solve
            ///			=1	LU decomposition
            ///			=2	Reduce load vector and back substitute
            /// 
            /// <summary>

            //
            //         ( FACTOR THE MATRIX "A" TO "UT*D*U" AND REDUCE THE VECTOR B)
            //

            int Jr, Jd, Jh, Id, Is, Ie, Ih, Ir, k;
            double d;

            Jr = 0;
            for (int j = 1; j < n + 1; j++)
            {
                Jd = JDiag[j];
                Jh = Jd - Jr;
                Is = j - Jh + 2;
                if (Jh - 2 < 0) goto l600;
                if (Jh - 2 == 0) goto l300;
                if (kkk == 2) goto l500;
                Ie = j - 1;
                k = Jr + 2;
                Id = JDiag[Is - 1];

                //
                //         (REDUCE ALL EQUATIONS EXCEPT DIAGONALS)
                //

                for (int i = Is; i < Ie + 1; i++)
                {
                    Ir = Id;
                    Id = JDiag[i];
                    Ih = Math.Min(Id - Ir - 1, i - Is + 1);
                    if (Ih > 0) A[k] += -DotRange(A, k - Ih, A, Id - Ih, Ih);
                    k++;
                }

                //      
            //        ( REDUCE THE DIAGONAL)
            //

                l300: if (kkk == 2) goto l500;

                Ir = Jr + 1;
                Ie = Jd - 1;
                k = j - Jd;
                for (int i = Ir; i < Ie + 1; i++)
                {
                    Id = JDiag[k + i];
                    if (A[Id] == 0.0) continue;
                    d = A[i];
                    A[i] = A[i] / A[Id];
                    A[Jd] += -d * A[i];
                }

                //
            //          ( REDUCE THE LOAD VECTOR)
            //

                l500: if (kkk != 1) b[j] += -DotRange(A, Jr + 1, b, Is - 1, Jh - 1);
            l600: Jr = Jd;
            }
            if (kkk == 1) return;

            //
            //          (DIVIDE BY DIAGONAL PIVOTS)
            //

            for (int i = 1; i < n + 1; i++)
            {
                Id = JDiag[i];
                if (A[Id] != 0.0D) b[i] = b[i] / A[Id];
            }

            //
            //           ( BACK SUBSTITUTION)
            //
            int j1;
            j1 = n;
            Jd = JDiag[j1];

        l800: d = b[j1];

            j1--;
            if (j1 <= 0) return;
            Jr = JDiag[j1];
            if ((Jd - Jr) <= 1) goto l1000;
            Is = j1 - Jd + Jr + 2;
            k = Jr - Is + 1;
            for (int i = Is; i < j1 + 1; i++)
            {
                b[i] += -A[i + k] * d;
            }
        l1000: Jd = Jr;
            goto l800;
        }
        public double DotRange(double[] a, int aIndex, double[] b, int bIndex, int n)
        {
            /// <sumary>
            /// Takes the dot product of two vectors a and b of length n
            /// <sumary>

            double dot = 0.0D;
            for (int i = 0; i < n; i++)
            {
                dot += a[aIndex + i] * b[bIndex + i];
            }

            return dot;
        }
    }

}

