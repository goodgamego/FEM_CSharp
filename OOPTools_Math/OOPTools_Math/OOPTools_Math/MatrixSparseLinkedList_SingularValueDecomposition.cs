using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPTools_Math
{
    public partial class MatrixSparseLinkedList : MatrixObject
    {
        /// <summary>
        /// Sparse matrix using linked lists: Code for Singular Value Decomposition
        /// Adapted from Numerical Recipes 
        /// Mehrdad Negahban
        /// 2009
        /// Reorganize: 2013
        /// </summary>
        public double[] SoveLinearSystem_SingularValueDecomposition(double[] rightHandSide)
        {
            Vector RHS = new Vector(rightHandSide);
            double ConditionNumber;
            return SolveUsingSingularValueDecomposition(RHS, out ConditionNumber).Values;
        }
        public Vector SolveUsingSingularValueDecomposition(Vector RHS, out double ConditionNumber)
        {
            MatrixSparseLinkedList A = this;
            MatrixSparseLinkedList V;
            Vector W;
            SingularValueDecomposition(ref A, out W, out V);

            Vector x = ATx_Symmetric(A, RHS);
            int n = x.Values.Length;
            for (int i = 0; i < n; i++)
            {
                if (W.Values[i] == 0.0D)
                {
                    x.Values[i] = 0.0D;
                }
                else
                {
                    x.Values[i] /= W.Values[i];
                }
            }
            x = Ax_Parallel(V, x);
            double Max = W.Max();
            double Min = W.Min();
            if (Min == 0.0D)
            {
                ConditionNumber = 1.0E100;
            }
            else
            {
                ConditionNumber = Max / Min;
            }
            return x;

        }
        public Vector SolveUsingSingularValueDecomposition(Vector RHS, out double ConditionNumber, out Vector W, out MatrixSparseLinkedList V)
        {
            MatrixSparseLinkedList A = this;
            //Matrix V;
            //Vector W;
            SingularValueDecomposition(ref A, out W, out V);

            Vector x = ATx_Symmetric(A, RHS);
            int n = x.Values.Length;
            for (int i = 0; i < n; i++)
            {
                if (W.Values[i] == 0.0D)
                {
                    x.Values[i] = 0.0D;
                }
                else
                {
                    x.Values[i] /= W.Values[i];
                }
            }
            x = Ax(V, x);
            double Max = W.Max();
            double Min = W.Min();
            if (Min == 0.0D)
            {
                ConditionNumber = 1.0E100;
            }
            else
            {
                ConditionNumber = Max / Min;
            }
            return x;

        }
        public void SingularValueDecomposition(ref MatrixSparseLinkedList A, out Vector W, out MatrixSparseLinkedList V)
        {
            //Adaptation of singular value decomposition from Numerical Recipes
            // A = U W V^T
            // Replaces A with U
            int m, mp, n, np, NMAX;
            m = A.n;
            n = A.n;
            mp = m;
            np = n;
            MatrixSparseLinkedList a = A;
            V = new MatrixSparseLinkedList();
            V.InitializeMatrix(n);
            MatrixSparseLinkedList v = V;
            double[] w = new double[np];

            NMAX = np;

            int l = 0;
            int nm = 0;
            int i, its, j, jj, k;
            double anorm, c, f, g, h, s, scale, x, y, z;
            double[] rv1 = new double[NMAX];
            g = 0.0d;
            scale = 0.0d;
            anorm = 0.0d;
            for (i = 0; i < n; i++)
            {
                l = i + 2;
                rv1[i] = scale * g;
                g = 0.0d;
                s = 0.0d;
                scale = 0.0d;
                if (i < m)
                {
                    for (k = i; k < m; k++)
                    {
                        scale += Math.Abs(a.GetMatrixElement(k, i));
                    }
                    if (scale != 0.0d)
                    {
                        for (k = i; k < m; k++)
                        {
                            double temp = a.GetMatrixElement(k, i);
                            temp /= scale;
                            s += temp * temp;
                        }
                        f = a.GetMatrixElement(i, i);
                        g = -FortranSign(Math.Sqrt(s), f);
                        h = f * g - s;
                        a.SetMatrixElement(i, i, f - g);
                        for (j = l - 1; j < n; j++)
                        {
                            s = 0.0d;
                            for (k = i; k < m; k++)
                            {
                                MatrixRow TempRow = Rows[k];
                                s += a.GetElementValueInRow(ref TempRow, i) * a.GetElementValueInRow(ref TempRow, j);
                            }
                            f = s / h;
                            for (k = i; k < m; k++)
                            {
                                MatrixRow TempRow = Rows[k];
                                a.AddToElementInRow(ref TempRow, j, f * a.GetElementValueInRow(ref TempRow, i));
                            }
                        }
                        for (k = i; k < m; k++)
                        {
                            double temp = a.GetMatrixElement(k, i);
                            temp *= scale;
                            a.SetMatrixElement(k, i, temp);
                        }
                    }
                }
                w[i] = scale * g;
                g = 0.0d;
                s = 0.0d;
                scale = 0.0d;
                if ((i + 1 <= m) && (i + 1 != n))
                {
                    MatrixRow TempRowI = Rows[i];
                    MatrixElement ElementLMinus1 = a.GetElementInRowOrClosestAfter(ref TempRowI, l - 1);
                    MatrixElement StartElement = ElementLMinus1;
                    if (StartElement != null)
                    {
                        while (StartElement.NotLastItem)
                        {
                            scale += Math.Abs(StartElement.Value);
                            StartElement = StartElement.NextItem;
                        }
                        scale += Math.Abs(StartElement.Value);
                        if (scale != 0.0d)
                        {
                            StartElement = ElementLMinus1;
                            while (StartElement.NotLastItem)
                            {
                                StartElement.Value /= scale;
                                s += StartElement.Value * StartElement.Value;
                                StartElement = StartElement.NextItem;
                            }
                            StartElement.Value /= scale;
                            s += StartElement.Value * StartElement.Value;
                            if (ElementLMinus1.Index == l - 1)
                            {
                                f = ElementLMinus1.Value;
                                g = -FortranSign(Math.Sqrt(s), f);
                                h = f * g - s;
                                ElementLMinus1.Value = f - g;
                            }
                            else
                            {
                                f = 0.0;
                                g = -FortranSign(Math.Sqrt(s), f);
                                h = -s;
                                a.AddToElementInRow(ref TempRowI, l - 1, f - g);
                                ElementLMinus1 = a.GetElementInRowOrClosestAfter(ref TempRowI, l - 1);
                            }
                            for (k = l - 1; k < n; k++)
                            {
                                rv1[k] = a.GetElementValueInRow(ref ElementLMinus1, k) / h;
                            }
                            for (j = l - 1; j < m; j++)
                            {
                                s = 0.0d;
                                MatrixRow TempRowJ = Rows[j];
                                MatrixElement ElementRowJLMinus1 = a.GetElementInRowOrClosestAfter(ref TempRowJ, l - 1);
                                MatrixElement StartElementJ = ElementRowJLMinus1;
                                for (k = l - 1; k < n; k++)
                                {
                                    s += a.GetElementValueInRow(ref ElementRowJLMinus1, k) * a.GetElementValueInRow(ref ElementLMinus1, k);
                                }
                                for (k = l - 1; k < n; k++)
                                {
                                    a.AddToElementInRow(ref ElementRowJLMinus1, k, s * rv1[k]);
                                }
                            }
                            StartElement = ElementLMinus1;
                            while (StartElement.NotLastItem)
                            {
                                StartElement.Value *= scale;
                                StartElement = StartElement.NextItem;
                            }
                            StartElement.Value *= scale;
                        }
                    }
                }
                anorm = Math.Max(anorm, (Math.Abs(w[i]) + Math.Abs(rv1[i])));
            }

            for (i = n - 1; i > -1; i--)
            {
                MatrixRow VRowI = V.Rows[i];
                if (i < n - 1)
                {
                    if (g != 0.0d)
                    {
                        MatrixRow ARowI = Rows[i];
                        double AIl = a.GetElementValueInRow(ref ARowI, l);
                        MatrixElement StartElement = a.GetElementInRowOrClosestBefore(ref ARowI, l);
                        for (j = l; j < n; j++)
                        {
                            double temp = (a.GetElementValueInRow(ref StartElement, j) / AIl) / g;
                            V.SetMatrixElement(j, i, temp);
                        }
                        for (j = l; j < n; j++)
                        {
                            s = 0.0d;
                            for (k = l; k < n; k++)
                            {
                                s += a.GetElementValueInRow(ref StartElement, k) * V.GetMatrixElement(k, j);
                            }
                            for (k = l; k < n; k++)
                            {
                                V.AddToMatrixElement(k, j, s * V.GetMatrixElement(k, i));
                            }
                        }
                    }
                    for (j = l; j < n; j++)
                    {
                        V.DeleteMatrixElement(i, j);
                        V.DeleteMatrixElement(j, i);
                    }
                }
                V.SetMatrixElement(i, i, 1.0D);
                g = rv1[i];
                l = i;
            }
            for (i = Math.Min(m, n) - 1; i > -1; i--)
            {
                l = i + 1;
                g = w[i];
                for (j = l; j < n; j++)
                {
                    a.DeleteMatrixElement(i, j);
                }
                if (g != 0.0d)
                {
                    g = 1.0d / g;
                    for (j = l; j < n; j++)
                    {
                        s = 0.0d;
                        for (k = l; k < m; k++)
                        {
                            s += a.GetMatrixElement(k, i) * a.GetMatrixElement(k, j);
                        }
                        f = (s / a.GetMatrixElement(i, i)) * g;
                        for (k = i; k < m; k++)//i was l
                        {
                            double temp = f * a.GetMatrixElement(k, i);
                            a.AddToMatrixElement(k, j, temp);
                        }
                    }
                    for (j = i; j < m; j++)
                    {
                        double temp = a.GetMatrixElement(j, i) * g;
                        a.SetMatrixElement(j, i, temp);
                    }
                }
                else
                {
                    for (j = i; j < m; j++)
                    {
                        a.DeleteMatrixElement(j, i);
                    }
                }
                a.AddToMatrixElement(i, i, 1.0D);
            }
            for (k = n - 1; k > -1; k--)
            {
                for (its = 0; its < 30; its++)
                {
                    for (l = k; l > -1; l--)
                    {
                        nm = l - 1;
                        if ((Math.Abs(rv1[l]) + anorm) == anorm) goto L2;
                        if ((Math.Abs(w[nm]) + anorm) == anorm) goto L1;
                    }
                L1: c = 0.0d;
                    s = 1.0d;
                    for (i = l; i < k + 1; i++)
                    {
                        f = s * rv1[i];
                        rv1[i] *= c;
                        if ((Math.Abs(f) + anorm) == anorm) goto L2;
                        g = w[i];
                        h = PYTHAG(f, g);
                        w[i] = h;
                        h = 1.0d / h;
                        c = (g * h);
                        s = -(f * h);
                        for (j = 0; j < m; j++)
                        {
                            y = a.GetMatrixElement(j, nm);
                            z = a.GetMatrixElement(j, i);
                            a.SetMatrixElement(j, nm, (y * c) + (z * s));
                            a.SetMatrixElement(j, i, -(y * s) + (z * c));
                        }
                    }
                L2: z = w[k];
                    if (l == k)
                    {
                        if (z < 0.0d)
                        {
                            w[k] = -z;
                            for (j = 0; j < n; j++)
                            {
                                MatrixElement VJK = V.GetElementInRow(ref V.Rows[j], k);
                                if (VJK != null)
                                {
                                    VJK.Value = -VJK.Value;
                                }
                                // V.SetMatrixElement(j, k, -V.GetMatrixElement(j, k));
                            }
                        }
                        goto L3;
                    }
                    if (its == 29)
                    {
                        Console.Write("no convergence in svdcmp");
                    }
                    x = w[l];
                    nm = k - 1;
                    y = w[nm];
                    g = rv1[nm];
                    h = rv1[k];
                    f = ((y - z) * (y + z) + (g - h) * (g + h)) / (2.0d * h * y);
                    g = PYTHAG(f, 1.0d);
                    f = ((x - z) * (x + z) + h * ((y / (f + FortranSign(g, f))) - h)) / x;
                    c = 1.0d;
                    s = 1.0d;
                    for (j = l; j < nm; j++)
                    {
                        i = j + 1;
                        g = rv1[i];
                        y = w[i];
                        h = s * g;
                        g = c * g;
                        z = PYTHAG(f, h);
                        rv1[j] = z;
                        c = f / z;
                        s = h / z;
                        f = (x * c) + (g * s);
                        g = -(x * s) + (g * c);
                        h = y * s;
                        y = y * c;
                        for (jj = 0; jj < n; jj++)
                        {
                            x = V.GetMatrixElement(jj, j);
                            z = V.GetMatrixElement(jj, i);
                            V.SetMatrixElement(jj, j, (x * c) + (z * s));
                            V.SetMatrixElement(jj, i, -(x * s) + (z * c));
                        }
                        z = PYTHAG(f, h);
                        w[j] = z;
                        if (z != 0.0d)
                        {
                            z = 1.0d / z;
                            c = f * z;
                            s = h * z;
                        }
                        f = (c * g) + (s * y);
                        x = -(s * g) + (c * y);
                        for (jj = 0; jj < m; jj++)
                        {
                            y = a.GetMatrixElement(jj, j);
                            z = a.GetMatrixElement(jj, i);
                            a.SetMatrixElement(jj, j, (y * c) + (z * s));
                            a.SetMatrixElement(jj, i, -(y * s) + (z * c));
                        }
                    }
                    rv1[l] = 0.0d;
                    rv1[k] = f;
                    w[k] = x;
                }
            L3: continue;
            }
            W = new Vector();
            W.Values = w;
        }
        public double PYTHAG(double a, double b)
        {
            //Adapted from Numerical Recipes
            //Computes (a^2+b^2)^(1/2) without destructive underflow or overflow
            double absa, absb;
            absa = Math.Abs(a);
            absb = Math.Abs(b);
            if (absa > absb)
            {
                return absa * Math.Sqrt(1.0D + (absb / absa) * (absb / absa));
            }
            else
            {
                if (absb == 0.0D)
                {
                    return 0.0D;
                }
                else
                {
                    return absb * Math.Sqrt(1.0D + (absa / absb) * (absa / absb));
                }
            }
        }
        public double FortranSign(double x, double y)
        {
            if (y >= 0.0D)
            {
                return Math.Abs(x);
            }
            else
            {
                return -Math.Abs(x);
            }
        }
    }
}
