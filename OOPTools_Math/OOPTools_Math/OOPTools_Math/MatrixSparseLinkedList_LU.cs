using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using OOPTools;

namespace OOPTools_Math
{
    /// <summary>
    /// Sparse matrix using linked lists: Code for LU Decomposition
    ///  
    /// Mehrdad Negahban
    /// 2007
    /// Reorganize: 2013
    /// </summary>
    public partial class MatrixSparseLinkedList : MatrixObject
    {

        public int IndexOfValueInArray(int Value, int[] Array)
        {

            for (int i = 0; i < Array.Length; i++)
            {
                if (Array[i] == Value)
                {
                    return i;
                }
            }
            return -1;
        }
        
        
        public void LUDecomposition()
        {
            DiagonalElements = new MatrixRow[n];
            InitializeLocalRows();

            for (int i = 0; i < n - 1; i++)
            {

                MatrixColumn PivotColumn = GetColumn(ref LocalRows, i);
                MatrixColumnElement MaxColumnElement;
                int newRelativePivotRow = GetRowOfMaxElementInColumn(ref PivotColumn, out MaxColumnElement);
                SwapRowDuringPivoting(i, newRelativePivotRow, ref PivotColumn, ref LocalRows);
                ScalePivotColumnToPivot(ref PivotColumn);
                DiagonalElements[i] = new MatrixRow();
                DiagonalElements[i].RowNotPopulated = false;
                DiagonalElements[i].FirsElement = PivotColumn.FirsElement.RowElement;

                MatrixColumnElement a = PivotColumn.FirsElement;
                MatrixElement b = PivotColumn.FirsElement.RowElement;
                double colVal;
                double change;
                MatrixElement StartRowSearch;
                MatrixElement c;
                if (b.NotLastItem)
                {
                    if (a.NotLastItem)
                    {
                        
                        //normal
                        while (a.NotLastItem)
                        {
                            a = a.NextElement;
                            StartRowSearch = LocalRows[a.Index].FirsElement;
                            colVal = a.RowElement.Value;
                            c = b;


                            while (c.NotLastItem)
                            {
                                c = c.NextItem;
                                change = -colVal * c.Value;
                                StartRowSearch = AddToElementInRow(ref StartRowSearch, c.Index, change);
                            }

                        }
                    }
                }
                LocalRows = GetLocalRows(LocalRows, i + 1);
            }
            DiagonalElements[n - 1].FirsElement = LocalRows[0].FirsElement;

        }
        public void LUDecomposition_Parallel()
        {
            // LUDecomposition with Parallel operations conducted for multi-processors
            // M. Negahban
            // 5-8-2011
            DiagonalElements = new MatrixRow[n];
            InitializeLocalRows();
            
            //For parallel
            double[] A_values = new double[n];
            int[] A_Indecies = new int[n];
            int NumberOfRows;

            for (int i = 0; i < n - 1; i++)
            {

                MatrixColumn PivotColumn = GetColumn(ref LocalRows, i);
                MatrixColumnElement MaxColumnElement;
                int newRelativePivotRow = GetRowOfMaxElementInColumn(ref PivotColumn, out MaxColumnElement);
                SwapRowDuringPivoting(i, newRelativePivotRow, ref PivotColumn, ref LocalRows);
                ScalePivotColumnToPivot(ref PivotColumn);
                DiagonalElements[i] = new MatrixRow();
                DiagonalElements[i].RowNotPopulated = false;
                DiagonalElements[i].FirsElement = PivotColumn.FirsElement.RowElement;

                MatrixColumnElement a = PivotColumn.FirsElement;
                MatrixElement b = PivotColumn.FirsElement.RowElement;
                if (b.NotLastItem)
                {
                    if (a.NotLastItem)
                    { 
                        //Parallel
                        NumberOfRows = 0;
                        while (a.NotLastItem)
                        {
                            a = a.NextElement;
                            A_values[NumberOfRows] = a.RowElement.Value;
                            A_Indecies[NumberOfRows] = a.Index;
                            NumberOfRows++;
                        }

                        //If you want to control level of parallelism Add
                        //ParallelOptions po = new ParallelOptions();
                        //po.MaxDegreeOfParallelism = 100; // select any number grater than 0
                        //Parallel.For(0, NumberOfRows, po, j =>
                        Parallel.For(0, NumberOfRows, j =>
                        {
                            double colValJ = A_values[j];
                            MatrixElement StartRowSearchJ = LocalRows[A_Indecies[j]].FirsElement;
                            MatrixElement cJ = b;

                            while (cJ.NotLastItem)
                            {
                                cJ = cJ.NextItem;
                                double changeJ = -colValJ * cJ.Value;
                                StartRowSearchJ = AddToElementInRow(ref StartRowSearchJ, cJ.Index, changeJ);
                            }

                        });

                    }
                }
                LocalRows = GetLocalRows(LocalRows, i + 1);
            }
            DiagonalElements[n - 1].FirsElement = LocalRows[0].FirsElement;

        }
        private int LUDecomposition_CountElements(MatrixColumn Column)
        {
            MatrixColumnElement a = Column.FirsElement;
            int count = 1;
            while (a.NotLastItem)
            {
                count++;
                a = a.NextElement;
            }
            return count;
        }
        private int LUDecomposition_CountElementsColumn(MatrixColumn Column, out MatrixColumnElement[] ColumnArray)
        {
            MatrixColumnElement a = Column.FirsElement;
            int count = 1;
            while (a.NotLastItem)
            {
                count++;
                a = a.NextElement;
            }
            ColumnArray = new MatrixColumnElement[count-1];
            a = Column.FirsElement;
            for (int i = 0; i < count-1; i++)
            {
                a = a.NextElement;
                ColumnArray[i] = a;  
            }
            return count;
        }
        private int LUDecomposition_CountElementsRow(MatrixRow Row, out MatrixElement[] RowArray)
        {
            MatrixElement a = Row.FirsElement;
            int count = 1;
            while (a.NotLastItem)
            {
                count++;
                a = a.NextItem;
            }
            RowArray = new MatrixElement[count - 1];
            a = Row.FirsElement;
            for (int i = 0; i < count - 1; i++)
            {
                a = a.NextItem;
                RowArray[i] = a;
            }
            return count;
        }
        public double CalculateDeterminant()
        {
            for (int i = 0; i < n; i++)
            {
                det *= DiagonalElements[i].FirsElement.Value;
            }
            return det;
        }
        public double[] ReorderRightHandSide(double[] RightHandSide)
        {
            double[] rhs = new double[n];
            for (int i = 0; i < n; i++)
            {
                rhs[i] = RightHandSide[RowOrder[i]];
            }
            return rhs;
        }
        public double[] ForwardElimination(double[] RightHandSide)
        {
            rightHandSide = ReorderRightHandSide(RightHandSide);
            for (int i = 1; i < n; i++)
            {
                rightHandSide[i] -= Sum1(i);
            }
            return rightHandSide;
        }
        private double Sum1(int row)
        {
            double sum = 0.0d;
            MatrixElement a = Rows[row].FirsElement;
            //bool test = true;
            while (a.NotLastItem)
            {
                if (a.Index < row)
                {
                    sum += a.Value * rightHandSide[a.Index];
                    a = a.NextItem;
                }
                else
                {
                    return sum;
                }
            }
            return sum;
        }
        public double[] BackSubstitution()
        {
            rightHandSide[n - 1] /= DiagonalElements[n - 1].FirsElement.Value;
            for (int i = n - 2; i > -1; i--)
            {
                rightHandSide[i] -= Sum2(i);
                rightHandSide[i] /= DiagonalElements[i].FirsElement.Value;
            }
            return rightHandSide;
        }
        private double Sum2(int row)
        {
            double sum = 0.0d;
            MatrixElement a = DiagonalElements[row].FirsElement;
            while (a.NotLastItem)
            {
                a = a.NextItem;
                sum += a.Value * rightHandSide[a.Index];
            }
            return sum;
        }

        
        public double[] SolveLinearSystem_LU(double[] RightHandSide)
        {
            StopWatch_Solver_LU_Total = new StopWatchTimer();
            StopWatch_Solver_LU_Decomposition = new StopWatchTimer();
            StopWatch_Solver_LU_ForwardElimination = new StopWatchTimer();
            StopWatch_Solver_LU_BackSubstitution = new StopWatchTimer();
            StopWatch_Solver_LU_CalculateDeterminant = new StopWatchTimer();

            StopWatch_Solver_LU_Total.StartTimer();

            // PLU decomposition
            StopWatch_Solver_LU_Decomposition.StartTimer();
            this.LUDecomposition();
            StopWatch_Solver_LU_Decomposition.StopTimer();

            // Forward elimination
            StopWatch_Solver_LU_ForwardElimination.StartTimer();
            this.ForwardElimination(RightHandSide);
            StopWatch_Solver_LU_ForwardElimination.StopTimer();

            //Backward elimination
            StopWatch_Solver_LU_BackSubstitution.StartTimer();
            this.BackSubstitution();
            StopWatch_Solver_LU_BackSubstitution.StopTimer();

            // Determinant
            StopWatch_Solver_LU_CalculateDeterminant.StartTimer();
            this.CalculateDeterminant();
            StopWatch_Solver_LU_CalculateDeterminant.StopTimer();

            StopWatch_Solver_LU_Total.StopTimer();

            return rightHandSide;

        }
        public StopWatchTimer StopWatch_Solver_LU_Total, StopWatch_Solver_LU_Decomposition, StopWatch_Solver_LU_ForwardElimination, StopWatch_Solver_LU_BackSubstitution, StopWatch_Solver_LU_CalculateDeterminant; 
        public double[] SolveLinearSystem_Parallel_LU(double[] RightHandSide)
        {
            StopWatch_Solver_LU_Total = new StopWatchTimer();
            StopWatch_Solver_LU_Decomposition = new StopWatchTimer();
            StopWatch_Solver_LU_ForwardElimination = new StopWatchTimer();
            StopWatch_Solver_LU_BackSubstitution = new StopWatchTimer();
            StopWatch_Solver_LU_CalculateDeterminant = new StopWatchTimer();

            StopWatch_Solver_LU_Total.StartTimer();

            // PLU decomposition
            StopWatch_Solver_LU_Decomposition.StartTimer();
            this.LUDecomposition_Parallel();
            StopWatch_Solver_LU_Decomposition.StopTimer();

            // Forward elimination
            StopWatch_Solver_LU_ForwardElimination.StartTimer();
            this.ForwardElimination(RightHandSide);
            StopWatch_Solver_LU_ForwardElimination.StopTimer();

            //Backward elimination
            StopWatch_Solver_LU_BackSubstitution.StartTimer();
            this.BackSubstitution();
            StopWatch_Solver_LU_BackSubstitution.StopTimer();

            // Determinant
            StopWatch_Solver_LU_CalculateDeterminant.StartTimer();
            this.CalculateDeterminant();
            StopWatch_Solver_LU_CalculateDeterminant.StopTimer();

            StopWatch_Solver_LU_Total.StopTimer();

            return rightHandSide;

        }
        public  double[] SolveLinearSystemForwardEliminationAndBackSubstitution(double[] RightHandSide)
        {
            WriteToDisplay.WriteInfo("\n  Solving linear system using link-list storage method and PLU decomposition\n\n");
            WriteToDisplay.WriteInfo("   Number of equations = " + n.ToString() + "\n");

            StopWatchTimer Totaltime = new StopWatchTimer();
            Totaltime.StartTimer();

            StopWatchTimer timer = new StopWatchTimer();
            //rightHandSide = RightHandSide;
            //ScaleAllRowsAndRightHandSideAndRemoveZeros();
            //this.RemoveAllZeros();

            // PLU decomposition
            timer.StartTimer();
            //this.LUDecomposition();
            timer.StopTimer();
            WriteToDisplay.WriteInfo("   PLU decomposition time = " + timer.TimeElapsedInSeconds().ToString() + "\n");

            // Forward elimination
            timer.StartTimer();
            this.ForwardElimination(RightHandSide);
            timer.StopTimer();
            WriteToDisplay.WriteInfo("   Forward elimination time = " + timer.TimeElapsedInSeconds().ToString() + "\n");

            //Backward elimination
            timer.StartTimer();
            this.BackSubstitution();
            timer.StopTimer();
            WriteToDisplay.WriteInfo("   Backward elimination time = " + timer.TimeElapsedInSeconds().ToString() + "\n");

            // Determinant
            timer.StartTimer();
            this.CalculateDeterminant();
            timer.StopTimer();
            WriteToDisplay.WriteInfo("   Determinant time = " + timer.TimeElapsedInSeconds().ToString() + "\n");

            Totaltime.StopTimer();
            WriteToDisplay.WriteInfo("\n  Total time to solve = " + Totaltime.TimeElapsedInSeconds().ToString() + "\n\n");

            return rightHandSide;

        }
        public Vector ATx_Symmetric(MatrixSparseLinkedList A, Vector x)
        {
           //Calculate the product y = A^T x
            int NumberOfRows = A.n;
            Vector y = new Vector(NumberOfRows);
            for (int i = 0; i < NumberOfRows; i++)
            {
                MatrixElement StartElement = A.Rows[i].FirsElement;
                double xi = x.Values[i];
                int index;
                while (StartElement.NotLastItem)
                {
                    index = StartElement.Index;
                    y.Values[index] += StartElement.Value * xi;
                    StartElement = StartElement.NextItem;
                }
                index = StartElement.Index;
                y.Values[index] += StartElement.Value * xi;
            }
            return y;
        }
        public Vector Ax(MatrixSparseLinkedList A, Vector x)
        {
            //Calculate the product y = A x
            int NumberOfRows = A.n;
            Vector y = new Vector(NumberOfRows);
            for (int i = 0; i < NumberOfRows; i++)
            {
                MatrixElement StartElement = A.Rows[i].FirsElement;
                double temp = 0.0D;
                int index;
                while (StartElement.NotLastItem)
                {
                    index = StartElement.Index;
                    temp += StartElement.Value * x.Values[index];
                    StartElement = StartElement.NextItem;
                }
                index = StartElement.Index;
                temp += StartElement.Value * x.Values[index];
                y.Values[i] = temp;
            }
            return y;
        }
        public Vector Ax_Parallel(MatrixSparseLinkedList A, Vector x)
        {
            //Calculate the product y = A x
            int NumberOfRows = A.n;
            Vector y = new Vector(NumberOfRows);
            Parallel.For(0, NumberOfRows, i =>
            {
                MatrixElement StartElement = A.Rows[i].FirsElement;
                double temp = 0.0D;
                int index;
                while (StartElement.NotLastItem)
                {
                    index = StartElement.Index;
                    temp += StartElement.Value * x.Values[index];
                    StartElement = StartElement.NextItem;
                }
                index = StartElement.Index;
                temp += StartElement.Value * x.Values[index];
                y.Values[i] = temp;
            });
            return y;
        }
    }
}
