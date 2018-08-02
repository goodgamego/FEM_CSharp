using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPTools_Math
{
    /// <summary>
    /// Matrix class to construct and solve sparse matrix with a linked-list structure
    /// 
    /// Mehrdad Negahban
    /// 2007
    /// Modified in organization: 2013
    /// </summary>
    [Serializable]
    public partial class MatrixSparseLinkedList : MatrixObject
    {
        public MatrixRow[] Rows;
        public double[] rightHandSide;
        protected MatrixElement[] LastElementUsed;
        public MatrixRow[] LocalRows;
        public MatrixRow[] DiagonalElements;
        public int[] RowOrder;
        protected SolutionMethod solver;
        public SolutionMethod Solver
        {
            get
            {
                return solver;
            }
        }
        public double RealZero;
        public double ReasSmall_LU;
        protected double det;
        public enum SolutionMethod
        {
            GaussianElimination,
            LUDecomposition,
            SingularValueDecomposition
        }
        public MatrixSparseLinkedList()
        {
            RealZero = 1.0E-200d;
            ReasSmall_LU = 1.0E-50d;
            solver = SolutionMethod.LUDecomposition;
        }
        public MatrixSparseLinkedList(int numberOfRows)
        {
            RealZero = 1.0E-50d;
            solver = SolutionMethod.LUDecomposition;
            InitializeMatrix(numberOfRows);
        }
        public virtual void InitializeMatrix(int numberOfRows)
        {
            n = numberOfRows;
            det = 1.0d;
            Rows = new MatrixRow[n];
            RowOrder = new int[n];
            LastElementUsed = new MatrixElement[n];
            for (int i = 0; i < n; i++)
            {
                Rows[i].RowNotPopulated = true;
                RowOrder[i] = i;
            }
        }
        public override double[] SolveLinearSystem(double[] RightHandSide)
        {
            switch (solver)
            {
                case SolutionMethod.LUDecomposition:
                    return SolveLinearSystem_LU(RightHandSide);
                case SolutionMethod.GaussianElimination:
                    return SolveLinearSystem_GaussElimination(RightHandSide);
                case SolutionMethod.SingularValueDecomposition:
                    return SoveLinearSystem_SingularValueDecomposition(RightHandSide);
                default:
                    return SolveLinearSystem_LU(RightHandSide);
            }
        }
        public override double[] SolveLinearSystem_Parallel(double[] RightHandSide)
        {
            switch (solver)
            {
                case SolutionMethod.LUDecomposition:
                    return SolveLinearSystem_Parallel_LU(RightHandSide);
                case SolutionMethod.GaussianElimination:
                    return SolveLinearSystem_Parallel_GaussElimination(RightHandSide);
                default:
                    return SolveLinearSystem_Parallel_LU(RightHandSide);
            }
        }

        #region Manipulation of matrix elements
        public override void AddToMatrixElement(int row, int col, double value)
        {
            if (Math.Abs(value) < RealZero)
            {
                return;
            }
            else
            {
                if (Rows[row].RowNotPopulated)
                {
                    MatrixElement newElement = new MatrixElement();
                    newElement.Index = col;
                    newElement.Value = value;
                    newElement.NotLastItem = false;
                    Rows[row].FirsElement = newElement;
                    Rows[row].RowNotPopulated = false;
                    LastElementUsed[row] = newElement;
                    return;
                }
                else if (col < Rows[row].FirsElement.Index)
                {
                    MatrixElement newElement = new MatrixElement();
                    newElement.Index = col;
                    newElement.Value = value;
                    newElement.NotLastItem = true;
                    newElement.NextItem = Rows[row].FirsElement;
                    Rows[row].FirsElement = newElement;
                    LastElementUsed[row] = newElement;
                    return;
                }
                else
                {
                    if (LastElementUsed[row] != null)
                    {
                        if (col >= LastElementUsed[row].Index)
                        {
                            LastElementUsed[row] = this.AddToElementInRow(ref LastElementUsed[row], col, value);
                        }
                        else
                        {
                            LastElementUsed[row] = this.AddToElementInRow(ref Rows[row].FirsElement, col, value);
                        }
                    }
                    else
                    {
                        LastElementUsed[row] = this.AddToElementInRow(ref Rows[row].FirsElement, col, value);
                    }
                }
            }
        }

        protected void AddToElementInRow(ref MatrixRow Row, int col, double value)
        {
            if (Math.Abs(value) < RealZero)
            {
                return;
            }
            else
            {
                if (Row.RowNotPopulated)
                {
                    MatrixElement newElement = new MatrixElement();
                    newElement.Index = col;
                    newElement.Value = value;
                    newElement.NotLastItem = false;
                    Row.FirsElement = newElement;
                    Row.RowNotPopulated = false;
                    return;
                }
                else
                {
                    AddToElementInRow(ref Row.FirsElement, col, value);
                }
            }
        }

        protected MatrixElement AddToElementInRow(ref MatrixElement FirstElement, int col, double value)
        {

            MatrixElement a = FirstElement;

            while (a.NotLastItem)
            {
                if (a.Index == col)
                {
                    a.Value += value;
                    return a;
                }
                else if ((a.Index < col) && (col < a.NextItem.Index))
                {
                    MatrixElement newElement = new MatrixElement();
                    newElement.Index = col;
                    newElement.Value = value;
                    newElement.NotLastItem = true;
                    newElement.NextItem = a.NextItem;
                    a.NextItem = newElement;
                    return newElement;
                }
                a = a.NextItem;
            }
            if (a.Index == col)
            {
                a.Value += value;
                return a;
            }
            else
            {
                MatrixElement newElement = new MatrixElement();
                newElement.Index = col;
                newElement.Value = value;
                newElement.NotLastItem = false;
                a.NotLastItem = true;
                a.NextItem = newElement;
                return newElement;
            }

        }
        protected MatrixElement AddToElementInRow_Parallel(ref MatrixRow LocalRow, ref MatrixElement FirstElement, int col, double value)
        {

            MatrixElement a = FirstElement;

            while (a.NotLastItem)
            {
                if (a.Index == col)
                {
                    a.Value += value;
                    return a;
                }
                else if ((a.Index < col) && (col < a.NextItem.Index))
                {
                    MatrixElement newElement = new MatrixElement();
                    newElement.Index = col;
                    newElement.Value = value;
                    newElement.NotLastItem = true;
                    newElement.NextItem = a.NextItem;
                    a.NextItem = newElement;
                    return newElement;
                }
                a = a.NextItem;
            }
            if (a.Index == col)
            {
                a.Value += value;
                return a;
            }
            else
            {
                MatrixElement newElement = new MatrixElement();
                newElement.Index = col;
                newElement.Value = value;
                newElement.NotLastItem = false;
                a.NotLastItem = true;
                a.NextItem = newElement;
                return newElement;
            }

        }

        public override void SetMatrixElement(int row, int col, double value)
        {
            if (Math.Abs(value) < RealZero)
            {
                DeleteMatrixElement(row, col);
            }
            else
            {
                if (Rows[row].RowNotPopulated)
                {
                    MatrixElement newElement = new MatrixElement();
                    newElement.Index = col;
                    newElement.Value = value;
                    newElement.NotLastItem = false;
                    Rows[row].FirsElement = newElement;
                    Rows[row].RowNotPopulated = false;
                    return;
                }
                else
                {
                    MatrixElement a = Rows[row].FirsElement;
                    if (a.Index > col)
                    {
                        MatrixElement newElement = new MatrixElement();
                        newElement.Index = col;
                        newElement.Value = value;
                        newElement.NotLastItem = true;
                        newElement.NextItem = a;
                        Rows[row].FirsElement = newElement;
                        return;
                    }
                    else
                    {

                        while (a.NotLastItem)
                        {
                            if (a.Index == col)
                            {
                                a.Value = value;
                                return;
                            }
                            else if ((a.Index < col) && (col < a.NextItem.Index))
                            {
                                MatrixElement newElement = new MatrixElement();
                                newElement.Index = col;
                                newElement.Value = value;
                                newElement.NotLastItem = true;
                                newElement.NextItem = a.NextItem;
                                a.NextItem = newElement;
                                return;
                            }
                            a = a.NextItem;
                        }
                        if (a.Index == col)
                        {
                            a.Value = value;
                            return;
                        }
                        else
                        {
                            MatrixElement newElement = new MatrixElement();
                            newElement.Index = col;
                            newElement.Value = value;
                            newElement.NotLastItem = false;
                            a.NotLastItem = true;
                            a.NextItem = newElement;
                            return;
                        }
                    }
                }
            }
        }
        public void DeleteMatrixElement(int row, int col)
        {
            MatrixRow TheRow = Rows[row];
            if (TheRow.RowNotPopulated)
            {
                return;
            }
            else
            {
                MatrixElement a = TheRow.FirsElement;
                if (a.Index > col)
                {
                    return;
                }
                else
                {
                    if (a.Index == col)
                    {
                        if (a.NotLastItem)
                        {
                            TheRow.FirsElement = a.NextItem;
                            return;
                        }
                        else
                        {
                            TheRow.RowNotPopulated = true;
                            TheRow.FirsElement = null;
                            return;
                        }

                    }
                    else
                    {
                        while (a.NotLastItem)
                        {

                            if ((a.Index < col) && (col < a.NextItem.Index))
                            {
                                return;
                            }
                            else if (a.NextItem.Index == col)
                            {
                                if (a.NextItem.NotLastItem)
                                {
                                    a.NextItem = a.NextItem.NextItem;
                                    return;
                                }
                                else
                                {
                                    a.NotLastItem = false;
                                    a.NextItem = null;
                                    return;
                                }
                            }
                            else if (a.NextItem.NotLastItem == false)
                            {
                                return;
                            }
                            a = a.NextItem;
                        }

                    }
                }
            }
        }

        public double GetMatrixElement(int row, int col)
        {
            return this.GetElementValueInRow(ref Rows[row], col);
        }
        public MatrixElement GetElementInRow(ref MatrixRow Row, int column)
        {
            if (Row.RowNotPopulated)
            {
                return null;
            }
            else
            {
                return this.GetElementInRow(ref Row.FirsElement, column);
            }
        }
        public MatrixElement GetElementInRow(ref MatrixElement StartElement, int col)
        {

            MatrixElement a = StartElement;
            if (a.Index > col)
            {
                return null;
            }
            else
            {

                while (a.NotLastItem)
                {
                    if (a.Index == col)
                    {
                        return a;
                    }
                    else if ((a.Index < col) && (col < a.NextItem.Index))
                    {
                        return null;
                    }
                    a = a.NextItem;
                }
                if (a.Index == col)
                {
                    return a;
                }
                else
                {
                    return null;
                }
            }

        }
        public MatrixElement GetElementInRowOrClosestAfter(ref MatrixRow Row, int column)
        {
            if (Row.RowNotPopulated)
            {
                return null;
            }
            else
            {
                return this.GetElementInRowOrClosestAfter(ref Row.FirsElement, column);
            }
        }
        public MatrixElement GetElementInRowOrClosestAfter(ref MatrixElement StartElement, int col)
        {

            MatrixElement a = StartElement;
            if (a.Index > col)
            {
                return null;
            }
            else
            {

                while (a.NotLastItem)
                {
                    if (a.Index == col)
                    {
                        return a;
                    }
                    else if ((a.Index < col) && (col < a.NextItem.Index))
                    {
                        return a.NextItem;
                    }
                    a = a.NextItem;
                }
                if (a.Index == col)
                {
                    return a;
                }
                else
                {
                    return null;
                }
            }

        }
        public MatrixElement GetElementInRowOrClosestBefore(ref MatrixRow Row, int column)
        {
            if (Row.RowNotPopulated)
            {
                return null;
            }
            else
            {
                return this.GetElementInRowOrClosestBefore(ref Row.FirsElement, column);
            }
        }
        public MatrixElement GetElementInRowOrClosestBefore(ref MatrixElement StartElement, int col)
        {

            MatrixElement a = StartElement;
            if (a.Index > col)
            {
                return null;
            }
            else
            {

                while (a.NotLastItem)
                {
                    if (a.Index == col)
                    {
                        return a;
                    }
                    else if ((a.Index < col) && (col < a.NextItem.Index))
                    {
                        return a;
                    }
                    a = a.NextItem;
                }
                return a;
            }

        }
        public double GetElementValueInRow(ref MatrixRow Row, int column)
        {
            if (Row.RowNotPopulated)
            {
                return 0.0d;
            }
            else
            {
                return this.GetElementValueInRow(ref Row.FirsElement, column);
            }
        }
        public double GetElementValueInRow(ref MatrixElement StartElement, int col)
        {

            MatrixElement a = StartElement;
            if (a.Index > col)
            {
                return 0.0d;
            }
            else
            {

                while (a.NotLastItem)
                {
                    if (a.Index == col)
                    {
                        return a.Value;
                    }
                    else if ((a.Index < col) && (col < a.NextItem.Index))
                    {
                        return 0.0d;
                    }
                    a = a.NextItem;
                }
                if (a.Index == col)
                {
                    return a.Value;
                }
                else
                {
                    return 0.0d;
                }
            }

        }
        public MatrixColumn GetColumn(ref MatrixRow[] rows, int col)//, int numberOfElements)
        {
            MatrixColumn NewColumn = new MatrixColumn();
            NewColumn.ColumnNotPopulated = true;
            int lenght = rows.Length;
            //numberOfElements = 0;
            //MatrixElement FirstElement;
            MatrixElement a;
            MatrixColumnElement b = new MatrixColumnElement();
            for (int i = 0; i < rows.Length; i++)
            {
                a = GetElementInRow(ref rows[i], col);
                if (!(a == null))
                {
                    MatrixColumnElement c = new MatrixColumnElement();
                    c.Index = i;
                    c.NotLastItem = false;
                    c.RowElement = a;
                    if (NewColumn.ColumnNotPopulated)
                    {
                        NewColumn.FirsElement = c;
                        b = c;
                        NewColumn.ColumnNotPopulated = false;
                    }
                    else
                    {
                        b.NextElement = c;
                        b.NotLastItem = true;
                        b = c;
                    }
                }

            }
            return NewColumn;
        }
        protected int GetRowOfMaxElementInColumn(ref MatrixColumn column)
        {
            if (column.ColumnNotPopulated)
            {
                return -1;
            }
            else
            {
                return GetRowOfMaxElementInColumn(column.FirsElement);
            }
        }
        protected int GetRowOfMaxElementInColumn(ref MatrixColumn column, out MatrixColumnElement MaxColumnElement)
        {
            if (column.ColumnNotPopulated)
            {
                MaxColumnElement = null;
                return -1;
            }
            else
            {
                return GetRowOfMaxElementInColumn(column.FirsElement, out MaxColumnElement);
            }
        }
        protected void SwapRowDuringPivoting(int PivotRow, int relativeRow, ref MatrixColumn PivotColumn, ref MatrixRow[] localRows)
        {
            if (relativeRow > 0)
            {
                int PivotIndex = PivotRow + relativeRow;
                MatrixElement a = Rows[PivotRow].FirsElement;
                Rows[PivotRow].FirsElement = Rows[PivotIndex].FirsElement;
                Rows[PivotIndex].FirsElement = a;
                /*
                 * double rhs = rightHandSide[PivotRow];
                rightHandSide[PivotRow] = rightHandSide[PivotIndex];
                rightHandSide[PivotIndex] = rhs;
                 * */
                MatrixElement b = localRows[0].FirsElement;
                localRows[0].FirsElement = localRows[relativeRow].FirsElement;
                localRows[relativeRow].FirsElement = b;

                det = -det;

                int temp = RowOrder[PivotRow];
                RowOrder[PivotRow] = RowOrder[PivotIndex];
                RowOrder[PivotIndex] = temp;

                SwapLocalRowDuringPivoting(PivotRow, relativeRow, ref PivotColumn);
            }
        }
        protected void SwapRowDuringPivoting(int PivotRow, int relativeRow, MatrixColumnElement MaxColumnElement, ref MatrixColumn PivotColumn, ref MatrixRow[] localRows)
        {
            if (relativeRow > 0)
            {
                // Swap Rows
                int PivotIndex = PivotRow + relativeRow;
                MatrixElement a = Rows[PivotRow].FirsElement;
                Rows[PivotRow].FirsElement = Rows[PivotIndex].FirsElement;
                Rows[PivotIndex].FirsElement = a;
                // Swap localRows
                MatrixElement b = localRows[0].FirsElement;
                localRows[0].FirsElement = localRows[relativeRow].FirsElement;
                localRows[relativeRow].FirsElement = b;
                // Change sign of determinant
                det = -det;
                // Swap RowOrder
                int temp = RowOrder[PivotRow];
                RowOrder[PivotRow] = RowOrder[PivotIndex];
                RowOrder[PivotIndex] = temp;
                // Swap PivotColumn
                SwapLocalRowDuringPivoting(PivotRow, relativeRow, MaxColumnElement, ref PivotColumn);
            }
        }
        protected void SwapLocalRowDuringPivoting(int PivotRow, int relativeRow, ref MatrixColumn PivotColumn)
        {
            MatrixColumnElement FirstRow = PivotColumn.FirsElement;
            MatrixColumnElement a = FirstRow;
            MatrixColumnElement aLast;
            while (a.NotLastItem)
            {
                aLast = a;
                a = a.NextElement;

                if (a.Index == relativeRow)
                {
                    if (FirstRow.NextElement.Index == a.Index)
                    {
                        FirstRow.Index = a.Index;
                        if (a.NotLastItem)
                        {
                            FirstRow.NextElement = a.NextElement;
                        }
                        else
                        {
                            FirstRow.NotLastItem = false;
                            FirstRow.NextElement = null;
                        }
                        a.Index = 0;
                        a.NextElement = FirstRow;
                        a.NotLastItem = true;
                        PivotColumn.FirsElement = a;
                    }
                    else
                    {
                        MatrixColumnElement SecondElement = FirstRow.NextElement;
                        aLast.NextElement = FirstRow;
                        FirstRow.Index = a.Index;
                        if (a.NotLastItem)
                        {
                            FirstRow.NextElement = a.NextElement;
                        }
                        else
                        {
                            FirstRow.NotLastItem = false;
                            FirstRow.NextElement = null;
                        }
                        a.Index = 0;
                        a.NextElement = SecondElement;
                        a.NotLastItem = true;
                        PivotColumn.FirsElement = a;
                    }
                    return;


                }

            }

        }
        protected void SwapLocalRowDuringPivoting(int PivotRow, int relativeRow, MatrixColumnElement MaxColumnElement, ref MatrixColumn PivotColumn)
        {
            MatrixColumnElement FirstRow = PivotColumn.FirsElement;
            MatrixColumnElement a = MaxColumnElement;
            if (FirstRow.NextElement.Index == a.Index)
            {
                FirstRow.Index = a.Index;
                if (a.NotLastItem)
                {
                    FirstRow.NextElement = a.NextElement;
                }
                else
                {
                    FirstRow.NotLastItem = false;
                    FirstRow.NextElement = null;
                }
                a.Index = 0;
                a.NextElement = FirstRow;
                a.NotLastItem = true;
                PivotColumn.FirsElement = a;
            }
            else
            {
                MatrixColumnElement SecondElement = FirstRow.NextElement;
                FirstRow.Index = a.Index;
                if (a.NotLastItem)
                {
                    FirstRow.NextElement = a.NextElement;
                }
                else
                {
                    FirstRow.NotLastItem = false;
                    FirstRow.NextElement = null;
                }
                a.Index = 0;
                a.NextElement = SecondElement;
                a.NotLastItem = true;
                PivotColumn.FirsElement = a;
            }
            return;

        }
        protected MatrixRow[] GetLocalRows(MatrixRow[] OldRows, int PivotColumn)
        {
            MatrixRow[] NewLocalRows = new MatrixRow[OldRows.Length - 1];
            MatrixElement a;
            for (int i = 0; i < NewLocalRows.Length; i++)
            {
                a = OldRows[i + 1].FirsElement;
                if (a.NotLastItem)
                {
                    if (a.NextItem.Index <= PivotColumn)
                    {
                        NewLocalRows[i].FirsElement = a.NextItem;
                    }
                    else
                    {
                        NewLocalRows[i] = OldRows[i + 1];
                    }
                }
                else
                {
                    NewLocalRows[i] = OldRows[i + 1];
                }
            }
            return NewLocalRows;
        }
        protected void InitializeLocalRows()
        {
            LocalRows = new MatrixRow[n];
            for (int i = 0; i < n; i++)
            {
                LocalRows[i] = new MatrixRow();
                LocalRows[i].FirsElement = Rows[i].FirsElement;
                LocalRows[i].RowNotPopulated = false;
            }
        }
        protected int GetRowOfMaxElementInColumn(MatrixColumnElement FirstElement)
        {
            MatrixColumnElement a = FirstElement;
            double max = a.RowElement.Value;
            int rowNumber = a.Index;
            double AbsMax = Math.Abs(max);
            double cValue;
            double cAbsValue;
            while (a.NotLastItem)
            {
                a = a.NextElement;
                cValue = a.RowElement.Value;
                cAbsValue = Math.Abs(cValue);
                if (AbsMax < cAbsValue)
                {
                    rowNumber = a.Index;
                    max = cValue;
                    AbsMax = cAbsValue;
                }

            }
            return rowNumber;
        }
        protected int GetRowOfMaxElementInColumn(MatrixColumnElement FirstElement, out MatrixColumnElement MaxColumnElement)
        {
            MatrixColumnElement a = FirstElement;
            double max = a.RowElement.Value;
            int rowNumber = a.Index;
            MaxColumnElement = a;
            double AbsMax = Math.Abs(max);
            double cValue;
            double cAbsValue;
            while (a.NotLastItem)
            {
                a = a.NextElement;
                cValue = a.RowElement.Value;
                cAbsValue = Math.Abs(cValue);
                if (AbsMax < cAbsValue)
                {
                    rowNumber = a.Index;
                    MaxColumnElement = a;
                    max = cValue;
                    AbsMax = cAbsValue;
                }

            }
            return rowNumber;
        }
        protected void ScalePivotColumnToPivot(ref MatrixColumn pivotColumn)
        {
            MatrixColumnElement a = pivotColumn.FirsElement;
            double pivotValue = a.RowElement.Value;
            while (a.NotLastItem)
            {
                a = a.NextElement;
                a.RowElement.Value /= pivotValue;
            }
        }
        public double GetMaxElementInRow(int row)
        {
            if (Rows[row].RowNotPopulated)
            {
                return 0.0D;
            }
            else
            {
                MatrixElement a = Rows[row].FirsElement;
                double max = a.Value;
                double AbsMax = Math.Abs(max);
                double cValue;
                double cAbsValue;
                while (a.NotLastItem)
                {
                    a = a.NextItem;
                    cValue = a.Value;
                    cAbsValue = Math.Abs(cValue);
                    if (AbsMax < cAbsValue)
                    {
                        max = cValue;
                        AbsMax = cAbsValue;
                    }

                }
                return max;
            }
        }
        public double GetMaxElementInRowAfterColumnJ(int row, int columnJ)
        {
            if (Rows[row].RowNotPopulated)
            {
                return 0.0d;// double.NaN;
            }
            else
            {
                MatrixElement a = Rows[row].FirsElement;
                double max;
                double AbsMax;
                if (a.Index > columnJ)
                {
                    max = a.Value;
                    AbsMax = Math.Abs(max);
                }
                else
                {
                    max = 0.0d;
                    AbsMax = 0.0d;
                }
                double cValue;
                double cAbsValue;
                while (a.NotLastItem)
                {
                    if (a.Index > columnJ)
                    {
                        a = a.NextItem;
                        cValue = a.Value;
                        cAbsValue = Math.Abs(cValue);
                        if (AbsMax < cAbsValue)
                        {
                            max = cValue;
                            AbsMax = cAbsValue;
                        }
                    }
                    else
                    {
                        a = a.NextItem;
                    }

                }
                return max;
            }
        }

        public void ScaleRow(int row, double scaleFactor)
        {
            if (Rows[row].RowNotPopulated)
            {
            }
            else
            {
                MatrixElement a = Rows[row].FirsElement;
                a.Value *= scaleFactor;
                while (a.NotLastItem)
                {
                    a = a.NextItem;
                    a.Value *= scaleFactor;
                }

            }
            rightHandSide[row] *= scaleFactor;
        }

        public void ScaleAllRowsAndRightHandSide()
        {
            for (int i = 0; i < n; i++)
            {
                double factor = GetMaxElementInRow(i);
                if ((!(double.IsNaN(factor))) && (Math.Abs(factor) > 0.0d))
                {
                    factor = 1.0d / factor;
                    ScaleRow(i, factor);

                }
            }
        }

        public void ScaleAllRowsAndRightHandSideAndRemoveZeros()
        {
            this.ScaleAllRowsAndRightHandSide();
            MatrixElement a = new MatrixElement();
            for (int i = 0; i < n; i++)
            {
                RemoveZeros(ref Rows[i]);
            }
        }
        protected void RemoveAllZeros()
        {
            for (int i = 0; i < n; i++)
            {
                RemoveZeros(ref Rows[i]);
            }
        }
        protected void RemoveZeros(ref MatrixRow Row)
        {
            MatrixElement a = Row.FirsElement;
            bool test = true;
            while (test)
            {
                if (a.NotLastItem)
                {
                    if (Math.Abs(a.NextItem.Value) < RealZero)
                    {
                        if (a.NextItem.NotLastItem)
                        {
                            a.NextItem = a.NextItem.NextItem;

                        }
                        else
                        {
                            a.NextItem = null;
                            a.NotLastItem = false;
                            test = false;
                        }
                    }
                    if (test) a = a.NextItem;

                }
                else
                {
                    test = false;
                }
            }
            a = Row.FirsElement;

            if (Math.Abs(a.Value) < RealZero)
            {
                if (a.NotLastItem)
                {
                    Row.FirsElement = a.NextItem;
                }
                else
                {
                    Row.RowNotPopulated = true;
                    Row.FirsElement = null;
                }
            }
        }
        #endregion

        #region Operators
        public static MatrixSparseLinkedList operator +(MatrixSparseLinkedList matrix1, MatrixSparseLinkedList matrix2)
        {
            MatrixSparseLinkedList matrix3 = new MatrixSparseLinkedList();
            matrix3.InitializeMatrix(matrix1.n);
            AddMatrix_ToExistingMatrix(ref matrix3, matrix1);
            AddMatrix_ToExistingMatrix(ref matrix3, matrix2);
            return matrix3;
        }
        public static MatrixSparseLinkedList operator -(MatrixSparseLinkedList matrix1, MatrixSparseLinkedList matrix2)
        {
            MatrixSparseLinkedList matrix3 = new MatrixSparseLinkedList();
            matrix3.InitializeMatrix(matrix1.n);
            AddMatrix_ToExistingMatrix(ref matrix3, matrix1);
            SubtractMatrix_FromExistingMatrix(ref matrix3, matrix2);
            return matrix3;
        }
        public static MatrixSparseLinkedList operator *(double Scalar1, MatrixSparseLinkedList matrix2)
        {
            return MultiplyScalar_ByMatrix(matrix2, Scalar1);
        }
        public static MatrixSparseLinkedList operator *(MatrixSparseLinkedList matrix1, double Scalar2)
        {
            return MultiplyScalar_ByMatrix(matrix1, Scalar2);
        }
        public static Vector operator *(MatrixSparseLinkedList matrix1, Vector Vector2)
        {
            return MultiplyExistingMatrix_ByVector(ref matrix1, Vector2);
        }
        public static MatrixSparseLinkedList operator *(MatrixSparseLinkedList matrix1, MatrixSparseLinkedList matrix2)
        {
            return MultiplyMatrix_ByMatrix(matrix1, matrix2);
        }
        public static MatrixSparseLinkedList operator /(MatrixSparseLinkedList matrix1, double Scalar2)
        {
            double Invert = 1.0D / Scalar2;
            return MultiplyScalar_ByMatrix(matrix1, Invert);
        }
        #endregion

        public override Vector MultiplyByVectorU(Vector u)
        {
            Vector Y = new Vector(n); // Make right-hand-side
            for (int i = 0; i < n; i++)
            {
                Y.Values[i] = 0.0d; // initialize RHS
                MatrixElement E = Rows[i].FirsElement; //Get row first element
                for (int j = 0; j < n; j++)
                {
                    Y.Values[i] += E.Value * u.Values[E.Index]; //multilpy
                    if (E.NotLastItem)
                    {
                        E = E.NextItem; // go to next element in row
                    }
                    else
                    {
                        break; // break if last element in row
                    }
                }
            }
            return Y;
        }

        public static void AddMatrix_ToExistingMatrix(ref MatrixSparseLinkedList ExistingMatrix, MatrixSparseLinkedList MatrixToAdd)
        {
            MatrixSparseLinkedList A = MatrixToAdd;
            int NumberOfRows = A.n;
            for (int i = 0; i < NumberOfRows; i++)
            {
                if (!A.Rows[i].RowNotPopulated)
                {
                    MatrixElement E = A.Rows[i].FirsElement;
                    while (E.NotLastItem)
                    {
                        ExistingMatrix.AddToMatrixElement(i, E.Index, E.Value);
                        E = E.NextItem;
                    }
                    ExistingMatrix.AddToMatrixElement(i, E.Index, E.Value);
                }
            }
        }
        public static void SubtractMatrix_FromExistingMatrix(ref MatrixSparseLinkedList ExistingMatrix, MatrixSparseLinkedList MatrixToSubtract)
        {
            MatrixSparseLinkedList A = MatrixToSubtract;
            int NumberOfRows = A.n;
            for (int i = 0; i < NumberOfRows; i++)
            {
                if (!A.Rows[i].RowNotPopulated)
                {
                    MatrixElement E = A.Rows[i].FirsElement;
                    while (E.NotLastItem)
                    {
                        ExistingMatrix.AddToMatrixElement(i, E.Index, -E.Value);
                        E = E.NextItem;
                    }
                    ExistingMatrix.AddToMatrixElement(i, E.Index, -E.Value);
                }
            }
        }
        public static void MultiplyScalar_ByExistingMatrix(ref MatrixSparseLinkedList ExistingMatrix, double Scalar)
        {
            MatrixSparseLinkedList A = ExistingMatrix;
            int NumberOfRows = A.n;
            for (int i = 0; i < NumberOfRows; i++)
            {
                if (!A.Rows[i].RowNotPopulated)
                {
                    MatrixElement E = A.Rows[i].FirsElement;
                    while (E.NotLastItem)
                    {
                        E.Value *= Scalar;
                        E = E.NextItem;
                    }
                    E.Value *= Scalar;
                }
            }
        }
        public static MatrixSparseLinkedList MultiplyScalar_ByMatrix(MatrixSparseLinkedList Matrix1, double Scalar)
        {
            MatrixSparseLinkedList A = Matrix1;
            int NumberOfRows = A.n;
            MatrixSparseLinkedList NewMatrix = new MatrixSparseLinkedList();
            NewMatrix.InitializeMatrix(NumberOfRows);
            for (int i = 0; i < NumberOfRows; i++)
            {
                if (!A.Rows[i].RowNotPopulated)
                {
                    MatrixElement E = A.Rows[i].FirsElement;
                    while (E.NotLastItem)
                    {
                        NewMatrix.AddToMatrixElement(i, E.Index, E.Value * Scalar);
                        E = E.NextItem;
                    }
                    NewMatrix.AddToMatrixElement(i, E.Index, E.Value * Scalar);
                }
            }
            return NewMatrix;
        }
        public static Vector MultiplyExistingMatrix_ByVector(ref MatrixSparseLinkedList ExistingMatrix, Vector V)
        {
            Vector NewVector = new Vector(ExistingMatrix.n);
            MatrixSparseLinkedList A = ExistingMatrix;
            int NumberOfRows = A.n;
            for (int i = 0; i < NumberOfRows; i++)
            {
                double Temp = 0.0D;
                if (!A.Rows[i].RowNotPopulated)
                {
                    MatrixElement E = A.Rows[i].FirsElement;
                    while (E.NotLastItem)
                    {
                        Temp += E.Value * V.Values[E.Index];
                        E = E.NextItem;
                    }
                    Temp += E.Value * V.Values[E.Index];
                }
                NewVector.Values[i] = Temp;
            }
            return NewVector;
        }
        public static MatrixSparseLinkedList MultiplyMatrix_ByMatrix(MatrixSparseLinkedList Matrix1, MatrixSparseLinkedList Matrix2)
        {
            MatrixSparseLinkedList NewMatrix = new MatrixSparseLinkedList();
            NewMatrix.InitializeMatrix(Matrix1.n);
            MatrixSparseLinkedList A = Matrix1;
            int NumberOfRows_A = A.n;
            MatrixSparseLinkedList B = Matrix2;
            int NumberOfRows_B = B.n;
            for (int i = 0; i < NumberOfRows_A; i++)
            {
                if (!A.Rows[i].RowNotPopulated)
                {
                    MatrixElement E_A = A.Rows[i].FirsElement;
                    while (E_A.NotLastItem)
                    {
                        for (int j = 0; j < NumberOfRows_B; j++)
                        {
                            if (!B.Rows[j].RowNotPopulated)
                            {
                                MatrixElement E_B = B.Rows[j].FirsElement;
                                while (E_B.NotLastItem)
                                {
                                    NewMatrix.AddToMatrixElement(i, E_B.Index, E_A.Value * E_B.Value);
                                    E_B = E_B.NextItem;
                                }
                                NewMatrix.AddToMatrixElement(i, E_B.Index, E_A.Value * E_B.Value);
                            }
                        }
                        E_A = E_A.NextItem;
                    }
                    for (int j = 0; j < NumberOfRows_B; j++)
                    {
                        if (!B.Rows[j].RowNotPopulated)
                        {
                            MatrixElement E_B = B.Rows[j].FirsElement;
                            while (E_B.NotLastItem)
                            {
                                NewMatrix.AddToMatrixElement(i, E_B.Index, E_A.Value * E_B.Value);
                                E_B = E_B.NextItem;
                            }
                            NewMatrix.AddToMatrixElement(i, E_B.Index, E_A.Value * E_B.Value);
                        }
                    }
                }
            }
            return NewMatrix;
        }


    }
    public struct MatrixRow
    {
        //public int Index;
        public bool RowNotPopulated;
        public MatrixElement FirsElement;
    }
    public class MatrixElement
    {
        public int Index;
        public double Value;
        public bool NotLastItem;
        public MatrixElement NextItem;
    }
    public struct MatrixColumn
    {
        //public int Index;
        public bool ColumnNotPopulated;
        public MatrixColumnElement FirsElement;
    }
    public class MatrixColumnElement
    {
        public int Index;
        // public double Value;
        public bool NotLastItem;
        public MatrixElement RowElement;
        public MatrixColumnElement NextElement;
    }
}
