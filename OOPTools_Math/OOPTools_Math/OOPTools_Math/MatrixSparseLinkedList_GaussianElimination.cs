using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPTools_Math
{
    //[Serializable]
    public partial class MatrixSparseLinkedList : MatrixObject
    {    
        /// <summary>
        /// Sparse matrix using linked lists: Code for Gaussian elimination
        ///  
        /// Mehrdad Negahban
        /// 2007
        /// Reorganize: 2013
        /// </summary>
        public override Vector SolveLinearSystem(Vector RightHandSide)
        {
            Vector u = new Vector(RightHandSide.Values.Length);
            u.Values = SolveLinearSystem(RightHandSide.Values);
            return u;
        }
        public  double[] SolveLinearSystem_GaussElimination(double[] RightHandSide)
        {
            rightHandSide = RightHandSide;
            det = 1.0d;
            //ScaleAllRowsAndRightHandSide();
            Console.Write("\nSolving linear system: Gaussian Elimination\n\n   Number of equations: " + n.ToString() + "\n\n");
            this.ScaleAllRowsAndRightHandSideAndRemoveZeros();

            // Forward elimination
            double factor;
            for (int i = 0; i < n; i++)
            {
                FindNewPivotAndSwap(i);
                factor = Rows[i].FirsElement.Value;
                det *= factor;
                factor = 1.0d / factor;
                ScaleRow(i, factor);
                RemoveZeros(ref Rows[i]);

                for (int j = i + 1; j < n; j++)
                {
                    SubtractPivotFromRowToRemovePivot(j, i);
                }
            }

            // Backward substitution
            rightHandSide[n - 1] /= Rows[n - 1].FirsElement.Value;
            for (int i = n - 2; i > -1; i--)
            {
                rightHandSide[i] = BackSubMul(i);
            }

            return rightHandSide;
        }
        public double[] SolveLinearSystem_Parallel_GaussElimination(double[] RightHandSide)
        {
            rightHandSide = RightHandSide;
            det = 1.0d;
            //ScaleAllRowsAndRightHandSide();
            Console.Write("\nSolving linear system: Gaussian Elimination\n\n   Number of equations: " + n.ToString() + "\n\n");
            this.ScaleAllRowsAndRightHandSideAndRemoveZeros();

            // Forward elimination
            double factor;
            for (int i = 0; i < n; i++)
            {
                FindNewPivotAndSwap(i);
                factor = Rows[i].FirsElement.Value;
                det *= factor;
                factor = 1.0d / factor;
                ScaleRow(i, factor);
                RemoveZeros(ref Rows[i]);

                Parallel.For(i + 1, n, j =>
                {
                    SubtractPivotFromRowToRemovePivot(j, i);
                });
            }

            // Backward substitution
            rightHandSide[n - 1] /= Rows[n - 1].FirsElement.Value;
            for (int i = n - 2; i > -1; i--)
            {
                rightHandSide[i] = BackSubMul(i);
            }

            return rightHandSide;
        }
        private double BackSubMul(int Row)
        {
            MatrixElement a = Rows[Row].FirsElement;
            double piv = a.Value;
            double sum = rightHandSide[Row];
            while (a.NotLastItem)
            {
                a = a.NextItem;
                sum -= a.Value * rightHandSide[a.Index];
            }
            return sum / piv;
        }

        private void FindNewPivotAndSwap(int CurrentPivotRow)
        {
            int PivotIndex = CurrentPivotRow;
            int j = CurrentPivotRow - 1;
            double max = GetMatrixElement(CurrentPivotRow, CurrentPivotRow);
            max /= GetMaxElementInRowAfterColumnJ(CurrentPivotRow, j);
            max = Math.Abs(max);

            double value;
            for (int i = CurrentPivotRow + 1; i < n; i++)
            {
                value = GetMatrixElement(i, CurrentPivotRow);
                value /= GetMaxElementInRowAfterColumnJ(i, j);

                value = Math.Abs(value);
                if (max < value)
                {
                    max = value;
                    PivotIndex = i;
                }
            }
            if (PivotIndex != CurrentPivotRow)
            {
                MatrixElement a = Rows[CurrentPivotRow].FirsElement;
                Rows[CurrentPivotRow].FirsElement = Rows[PivotIndex].FirsElement;
                Rows[PivotIndex].FirsElement = a;
                double rhs = rightHandSide[CurrentPivotRow];
                rightHandSide[CurrentPivotRow] = rightHandSide[PivotIndex];
                rightHandSide[PivotIndex] = rhs;
                det = -det;
            }
        }
        private void SubtractPivotFromRowToRemovePivot(int Row, int PivotRow)
        {
            double pivotColumn = GetMatrixElement(Row, PivotRow);
            if (pivotColumn == 0.0d)
            {
                RemoveElementsFromStartOfRow(ref Rows[Row], PivotRow);
                return;
            }
            double factor = -pivotColumn / Rows[PivotRow].FirsElement.Value;// GetMatrixElement(PivotRow, PivotRow);
            RemoveElementsFromStartOfRow(ref Rows[Row], PivotRow);
            MatrixElement a = Rows[PivotRow].FirsElement;
            MatrixElement b = Rows[Row].FirsElement;
            bool test = true;
            if (a.NotLastItem)
            {
                if (!(a.NextItem.Index < b.Index))
                {
                    while (a.NotLastItem)
                    {
                        a = a.NextItem;
                        test = true;
                        while (test)
                        {
                            if (a.Index == b.Index)
                            {
                                b.Value += factor * a.Value;
                                test = false;
                            }
                            else if (b.NotLastItem)
                            {
                                if (a.Index >= b.NextItem.Index)
                                {
                                    b = b.NextItem;
                                }
                                else if ((a.Index > b.Index) && (a.Index < b.NextItem.Index))
                                {
                                    MatrixElement c = new MatrixElement();
                                    c.Index = a.Index;
                                    c.Value = factor * a.Value;
                                    c.NotLastItem = true;
                                    c.NextItem = b.NextItem;
                                    b.NextItem = c;
                                    b = c;
                                    test = false;
                                }
                            }
                            else
                            {
                                MatrixElement c = new MatrixElement();
                                c.Index = a.Index;
                                c.Value = factor * a.Value;
                                //c.NextItem = b.NextItem;
                                c.NotLastItem = false;
                                b.NextItem = c;
                                b.NotLastItem = true;
                                b = c;
                                test = false;
                            }
                        }
                    }
                }
                else
                {
                    a = a.NextItem;
                    MatrixElement c = new MatrixElement();
                    c.Index = a.Index;
                    c.Value = factor * a.Value;
                    c.NotLastItem = true;
                    c.NextItem = b;
                    Rows[Row].FirsElement = c;
                    b = c;
                }
            }
            rightHandSide[Row] += factor * rightHandSide[PivotRow];
        }
        private void RemoveElementsFromStartOfRow(ref MatrixRow Row, int LastColumnToRemive)
        {
            MatrixElement a = Row.FirsElement;
            bool test = true;
            while (test)
            {
                if (a.NotLastItem)
                {

                    if (a.NextItem.Index < LastColumnToRemive + 1)
                    {
                        if (a.NextItem.NextItem.NotLastItem)
                        {
                            a.NextItem = a.NextItem.NextItem;
                            //a = a.NextItem;
                        }
                        else
                        {
                            a.NotLastItem = false;
                            a.NextItem = null;
                            test = false;
                        }
                    }
                    else
                    {
                        test = false;
                    }
                }
                else
                {
                    test = false;
                }
            }
            a = Row.FirsElement;
            if (a.Index < LastColumnToRemive + 1)
            {
                if (a.NotLastItem)
                {
                    Row.FirsElement = a.NextItem;
                }
                else
                {
                    Row.FirsElement = null;
                    Row.RowNotPopulated = true;
                }
            }

        }
    }
}
