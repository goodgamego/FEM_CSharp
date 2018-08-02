using System;

namespace OOPTools_Math
{
	/// <summary>
	/// Summary description for Matrix class
    /// 
    /// Mehrdad Negahban
    /// 2005
    /// Modified: 12-30-2010, 03-16-2012
	/// </summary>
    [Serializable]
    public struct Matrix_Jagged
	{
		private int rows;
		private int columns;
		private double[][] matrix;

		public int Rows
		{
			get
			{
                rows = Values.GetLength(0);
				return rows;
			}
			set
			{
				rows = value;
			}
		}
		public int Columns
		{
			get
			{
                columns = Values[0].GetLength(0);
				return columns;
			}
			set
			{
				columns = value;
			}
			
		}
		public double[][] Values
		{
			get
			{
				return matrix;
			}
			set
			{
				matrix = value;
			}
		}

		public Matrix_Jagged(int NumberOfRows, int NumberOfColumns)
		{
			rows = NumberOfRows;
			columns = NumberOfColumns;
            index = new int[0];
            matrix = ArrayTools.NewJaggedArray(rows, columns);
		}
		public Matrix_Jagged(Matrix m)
		{
			rows = m.Rows;
			columns = m.Columns;
            index = new int[0];
			matrix = new double[rows][];
			for (int i=0; i<rows; i++)
			{
                double[] TempRow = new double[columns];
                for (int j = 0; j < columns; j++) TempRow[j] = m.Values[i, j];
                matrix[i] = TempRow;
			}
		}
        public Matrix_Jagged(Matrix_Jagged m)
		{
			rows = m.Rows;
			columns = m.Columns;
            index = new int[0];
			matrix = new double[rows][];
			for (int i=0; i<rows; i++)
			{
                double[] TempRow = new double[columns];
                for (int j = 0; j < columns; j++) TempRow[j] = m.Values[i][j];
                matrix[i] = TempRow;
			}
		}
		public Matrix_Jagged(double[,] mArray)
		{
            rows = mArray.GetLength(0);
            columns = mArray.GetLength(1);
            matrix = new double[rows][];
            index = new int[0];
            for (int i = 0; i < rows; i++)
            {
                double[] TempRow = new double[columns];
                for (int j = 0; j < columns; j++) TempRow[j] = mArray[i, j];
                matrix[i] = TempRow;
            }
        }
        public Matrix_Jagged(double[][] mArray)
        {
            /// double[][] array needs to be rectangular array (i.e., similar to [,])
            rows = mArray.GetLength(0);
            columns = mArray[0].GetLength(0);
            index = new int[0];
            matrix = new double[rows][];
			for (int i=0; i<rows; i++)
			{
                double[] TempRow = new double[columns];
                for (int j = 0; j < columns; j++) TempRow[j] = mArray[i][j];
                matrix[i] = TempRow;
			}
        }
        public Matrix_Jagged(Vector X)
        {
            /// Makes a column matrix with vector as a column
            rows = X.Values.Length;
            columns = 1;
            index = new int[0];
            matrix = ArrayTools.JaggedMatrix_MakeFromColumnVectors(X.Values);
        }
        public Matrix_Jagged(Vector X, Vector Y)
        {
            /// Makes a matrix with each vector as a column
            rows = X.Values.Length;
            columns = 2;
            index = new int[0];
            matrix = ArrayTools.JaggedMatrix_MakeFromColumnVectors(X.Values, Y.Values);
        }
        public Matrix_Jagged(Vector X, Vector Y, Vector Z)
        {
            /// Makes a matrix with each vector as a column
            rows = X.Values.Length;
            columns = 3;
            index = new int[0];
            matrix = ArrayTools.JaggedMatrix_MakeFromColumnVectors(X.Values, Y.Values, Z.Values);
        }
        public Matrix_Jagged(Vector[] X)
        {
            /// Makes a matrix with each vector as a column
            rows = X[0].Values.GetLength(0);
            columns = X.GetLength(0);
            index = new int[0];
            matrix = ArrayTools.NewJaggedArray(rows, columns);
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    matrix[i][j] = X[j].Values[i];
                }
            }
        }
        public Matrix_Jagged(double[] X)
        {
            /// Makes a matrix with each vector as a column
            rows = X.Length;
            columns = 1;
            index = new int[0];
            matrix = ArrayTools.JaggedMatrix_MakeFromColumnVectors(X);
        }
        public Matrix_Jagged(double[] X, double[] Y)
        {
            /// Makes a matrix with each vector as a column
            rows = X.Length;
            columns = 2;
            index = new int[0];
            matrix = ArrayTools.JaggedMatrix_MakeFromColumnVectors(X, Y);
        }
        public Matrix_Jagged(double[] X, double[] Y, double[] Z)
        {
            /// Makes a matrix with each vector as a column
            rows = X.Length;
            columns = 3;
            index = new int[0];
            matrix = ArrayTools.JaggedMatrix_MakeFromColumnVectors(X, Y, Z);
        }
        public static Matrix_Jagged TensorProduct(double[] X, double[] Y)
        {
            /// Makes a matrix that is a tensor product of the two vectors
            /// M[i][j] = X[i]*Y[j]
            Matrix_Jagged Matrix1 = new Matrix_Jagged(ArrayTools.JaggedMatrix_TensorProduct(X,Y));
            return Matrix1;
        }
        public static Matrix_Jagged TensorProduct(Vector X, Vector Y)
        {
            /// Makes a matrix that is a tensor product of the two vectors
            /// M[i][j] = X[i]*Y[j]
            return TensorProduct(X.Values, Y.Values);
        }
        public static Matrix_Jagged TensorProduct(Vector X, double[] Y)
        {
            /// Makes a matrix that is a tensor product of the two vectors
            /// M[i][j] = X[i]*Y[j]
            return TensorProduct(X.Values, Y);
        }
        public static Matrix_Jagged TensorProduct(double[] X, Vector Y)
        {
            /// Makes a matrix that is a tensor product of the two vectors
            /// M[i][j] = X[i]*Y[j]
            return TensorProduct(X, Y.Values);
        }
        public void RowMatrix(double[] X)
        {
            /// Makes a row matrix out of a one dimensional array of numbers
            rows = 1;
            columns = X.Length;
            matrix = new double[1][];
            matrix[0] = X;
            for (int j = 0; j < columns; j++) matrix[0][j] = X[j];
        }
        public void RowMatrix(Vector X)
        {
            /// Makes a row matrix out of a one dimensional array of numbers
            RowMatrix(X.Values);
        }
        public void RowMatrix(double[] X, double[] Y)
        {
            /// Makes a row matrix out of a one dimensional array of numbers
            rows = 2;
            columns = X.Length;
            matrix = ArrayTools.JaggedMatrix_MakeFromRowVectors(X, Y);
        }
        public void RowMatrix(Vector X, double[] Y)
        {
            /// Makes a row matrix out of a one dimensional array of numbers
            RowMatrix(X.Values, Y);
        }
        public void RowMatrix(double[] X, Vector Y)
        {
            /// Makes a row matrix out of a one dimensional array of numbers
            RowMatrix(X, Y.Values);
        }
        public void RowMatrix(Vector X, Vector Y)
        {
            /// Makes a row matrix out of a one dimensional array of numbers
            RowMatrix(X.Values, Y.Values);
        }
        public void RowMatrix(double[] X, double[] Y, double[] Z)
        {
            /// Makes a row matrix out of a one dimensional array of numbers
            rows = 3;
            columns = X.Length;
            matrix = ArrayTools.JaggedMatrix_MakeFromRowVectors(X, Y, Z); 
        }
        public void RowMatrix(Vector X, Vector Y, Vector Z)
        {
            /// Makes a row matrix out of a one dimensional array of numbers
            RowMatrix(X.Values, Y.Values, Z.Values);
        }
        public void RowMatrix(double[][] X)
        {
            /// Makes a row matrix out of a one dimensional array of numbers
            rows = X.GetLength(0);
            columns = X[0].Length;
            matrix = ArrayTools.JaggedMatrix_MakeFromRowVectors(X);
        }
        public void RowMatrix(Vector[] X)
        {
            /// Makes a row matrix out of a one dimensional array of numbers
            rows = X.GetLength(0);
            columns = X[0].Values.Length;
            matrix = new double[rows][];
            for (int i = 0; i < rows; i++)
            {
                Vector thisRow = new Vector(X[i]);
                matrix[i] = thisRow.Values;
            }
        }
        public void ColumnMatrix(double[] X)
        {
            /// Makes a column matrix out of a one dimensional array of numbers
            rows = X.Length;
            columns = 1;
            matrix = ArrayTools.JaggedMatrix_MakeFromColumnVectors(X); 
        }
        public Vector GetColumn(int ColumnNumber)
        {
            int colLength = this.Values.GetLength(0);
            Vector col = new Vector(colLength);
            for (int i = 0; i < colLength; i++)
            {
                col.Values[i] = this.Values[i][ColumnNumber];
            }
            return col;
        }
        public Vector GetRow(int RowNumber)
        {
            Vector row = new Vector(this.Values[RowNumber]);
            return row;
        }

        public double[,] ConvertToArray()
        {
            double[,] JagM = new double[this.Rows, this.Columns];
            for (int i = 0; i < this.Rows; i++)
            {
                double[] TempRow = this.Values[i];
                for (int j = 0; j < this.Columns; j++)
                {
                    JagM[i, j] = TempRow[j];
                }
            }
            return JagM;
        }
        public double[][] ConvertToJaggedArray()
        {
            Matrix_Jagged tempMatrix  = new Matrix_Jagged(this);
            return tempMatrix.Values;
        }
        public Matrix_Jagged SetNaN()
        {
            for (int i = 0; i < this.Rows; i++)
            {
                double[] TempRow = this.Values[i];
                for (int j = 0; j < this.Columns; j++)
                {
                    TempRow[j] = System.Double.NaN;
                }
            }
            return this;
        }

        #region Multiplication
        /// <summary>
        /// Multiplies two matricies of different type and returns a new jagged matrix
        /// </summary>
        /// <param name="matrix1"></param>
        /// <param name="matrix2"></param>
        /// <returns></returns>
        public static Matrix_Jagged Multiplication(Matrix_Jagged matrix1, Matrix_Jagged matrix2)
		{
            Matrix_Jagged matrix3 = new Matrix_Jagged();
            matrix3.Values = ArrayTools.MatrixMultiply(matrix1.Values, matrix2.Values);
			return matrix3;
		}
        public static Matrix_Jagged Multiplication(Matrix_Jagged matrix1, double[,] matrix2)
        {
            Matrix_Jagged matrix3 = new Matrix_Jagged(matrix1.Rows, matrix2.GetLength(1));

            if (matrix1.Columns == matrix2.GetLength(0))
            {
                for (int i = 0; i < matrix3.Rows; i++)
                {
                    double[] TempRow1 = matrix1.Values[i];
                    double[] TempRow3 = matrix3.Values[i];
                    for (int j = 0; j < matrix3.Columns; j++)
                    {
                        for (int k = 0; k < matrix1.Columns; k++) TempRow3[j] += TempRow1[k] * matrix2[k, j];
                    }
                }
            }
            else
            {
                for (int i = 0; i < matrix3.Rows; i++)
                {
                    double[] TempRow3 = matrix3.Values[i];
                    for (int j = 0; j < matrix3.Columns; j++)
                    {
                        TempRow3[j] += System.Double.NaN;
                    }
                }

            }
            return matrix3;
        }
        public static Vector Multiplication(Matrix_Jagged matrix1, double[] matrix2)
        {
            Vector matrix3 = new Vector();
            matrix3.Values = ArrayTools.MatrixMultiply(matrix1.Values, matrix2);
            return matrix3;
        }
        public static Vector Multiplication(Matrix_Jagged matrix1, Vector matrix2)
        {
            return matrix1*matrix2.Values;
        }
        public static Matrix_Jagged Multiplication(double[] matrix1, Matrix_Jagged matrix2)
        {
            Matrix_Jagged matrix3 = new Matrix_Jagged(ArrayTools.MatrixMultiply(matrix1, matrix2.Values));
            return matrix3;
        }
        public static Matrix_Jagged Multiplication(Vector matrix1, Matrix_Jagged matrix2)
        {
            return matrix1.Values * matrix2;
        }
        public static Matrix_Jagged Multiplication(double[,] matrix1, Matrix_Jagged matrix2)
        {
            Matrix_Jagged matrix3 = new Matrix_Jagged(matrix1.GetLength(0), matrix2.Columns);

            if (matrix1.GetLength(1) == matrix2.Rows)
            {
                for (int i = 0; i < matrix3.Rows; i++)
                {
                    double[] TempRow3 = matrix3.Values[i];
                    for (int j = 0; j < matrix3.Columns; j++)
                    {
                        for (int k = 0; k < matrix1.GetLength(1); k++) TempRow3[j] += matrix1[i, k] * matrix2.Values[k][j];
                    }
                }
            }
            else
            {
                for (int i = 0; i < matrix3.Rows; i++)
                {
                    double[] TempRow3 = matrix3.Values[i];
                    for (int j = 0; j < matrix3.Columns; j++)
                    {
                        TempRow3[j] += System.Double.NaN;
                    }
                }
            }
            return matrix3;
        }
        public static Matrix_Jagged Multiplication(double scalar, Matrix_Jagged matrix1)
		{
            Matrix_Jagged matrix3 = new Matrix_Jagged();
            matrix3.Values = ArrayTools.Multiply(scalar,matrix1.Values);
			return matrix3;
		}
        public static Matrix_Jagged Multiplication(Matrix_Jagged matrix1, double scalar)
		{
            Matrix_Jagged matrix3 = new Matrix_Jagged();
            matrix3.Values = ArrayTools.Multiply(scalar, matrix1.Values);
            return matrix3;
		}

        // Operator "*"
        public static Matrix_Jagged operator *(Matrix_Jagged matrix1, Matrix_Jagged matrix2)
		{
            return Multiplication(matrix1, matrix2); 
		}
        public static Matrix_Jagged operator *(Matrix_Jagged matrix1, double[,] matrix2)
        {
            return Multiplication(matrix1, matrix2);
        }
        public static Vector operator *(Matrix_Jagged matrix1, double[] matrix2)
        {
            return Multiplication(matrix1, matrix2);
        }
        public static Vector operator *(Matrix_Jagged matrix1, Vector matrix2)
        {
            return Multiplication(matrix1, matrix2);
        }
        public static Matrix_Jagged operator *(double[] matrix1, Matrix_Jagged matrix2)
        {
            return Multiplication(matrix1, matrix2);
        }
        public static Matrix_Jagged operator *(Vector matrix1, Matrix_Jagged matrix2)
        {
            return Multiplication(matrix1.Values, matrix2);
        }
        public static Matrix_Jagged operator *(double[,] matrix1, Matrix_Jagged matrix2)
        {
            return Multiplication(matrix1, matrix2);
        }
        public static Matrix_Jagged operator *(double scalar, Matrix_Jagged matrix1)
		{
            return Multiplication(scalar, matrix1);
		}
        public static Matrix_Jagged operator *(Matrix_Jagged matrix1, double scalar)
		{
            return Multiplication(scalar, matrix1);
		}

        // Times operations of this object
        public  Matrix_Jagged Times(Matrix_Jagged matrix2)
        {
            return Multiplication(this, matrix2);
        }
        public Matrix_Jagged Times(double[,] matrix2)
        {
            return Multiplication(this, matrix2);
        }
        public Vector Times(double[] matrix2)
        {
            return Multiplication(this, matrix2);
        }
        public Vector Times(Vector matrix2)
        {
            return Multiplication(this, matrix2);
        }
        public Matrix_Jagged Times(double scalar)
        {
            return Multiplication(scalar, this);
        }
        #endregion

        #region Division
        /// <summary>
        /// Devides all elements of a matrix by the scalar and return a new jagged matrix
        /// </summary>
        /// <param name="matrix1"></param>
        /// <param name="scalar"></param>
        /// <returns></returns>
        public static Matrix_Jagged Division(Matrix_Jagged matrix1, double scalar)
		{
			double inverse = 1.0d/scalar;		
			return inverse*matrix1;
		}
        
        // Operator "/"
        public static Matrix_Jagged operator /(Matrix_Jagged matrix1, double scalar)
        {
            return Division(matrix1, scalar);
        }

        // Divide for object
        public Matrix_Jagged Divide(double scalar)
        {
            return Division(this, scalar);
        }
        #endregion

        #region Subtraction
        /// <summary>
        /// Return a new jagged matrix that is negative the original
        /// </summary>
        /// <param name="matrix1"></param>
        /// <returns></returns>
        public static Matrix_Jagged Negative(Matrix_Jagged matrix1)
        {
            Matrix_Jagged matrix3 = new Matrix_Jagged(matrix1.Rows, matrix1.Columns);
            matrix3.Values = ArrayTools.Negative(matrix1.Values);
            return matrix3;
        }
        /// <summary>
        /// Subtracts two matrices of different type and returns a jagged matrix
        /// </summary>
        /// <param name="matrix1"></param>
        /// <param name="matrix2"></param>
        /// <returns></returns>
        public static Matrix_Jagged Subtraction(Matrix_Jagged matrix1, Matrix_Jagged matrix2)
        {
            Matrix_Jagged matrix3 = new Matrix_Jagged(matrix1.Rows, matrix2.Columns);

            if (matrix1.Rows == matrix2.Rows && matrix1.Columns == matrix2.Columns)
            {
                matrix3.Values =ArrayTools.Subtract(matrix1.Values, matrix2.Values);
            }
            else
            {
                matrix3.SetNaN();
            }
            return matrix3;
        }

        public static Matrix_Jagged Subtraction(double[][] matrix1, Matrix_Jagged matrix2)
        {
            Matrix_Jagged matrix3 = new Matrix_Jagged(matrix1.GetLength(0), matrix2.Columns);

            if (matrix1.GetLength(0) == matrix2.Rows && matrix1.GetLength(1) == matrix2.Columns)
            {
                matrix3.Values =ArrayTools.Subtract(matrix1, matrix2.Values);
            }
            else
            {
                matrix3.SetNaN();
            }
            return matrix3;
        }
        public static Matrix_Jagged Subtraction(Matrix_Jagged matrix1, double[][] matrix2)
        {
            Matrix_Jagged matrix3 = new Matrix_Jagged(matrix1.Rows, matrix2.GetLength(1));

            if (matrix1.Rows == matrix2.GetLength(0) && matrix1.Columns== matrix2.GetLength(1))
            {
                matrix3.Values =ArrayTools.Subtract(matrix1.Values, matrix2);
            }
            else
            {
                matrix3.SetNaN();
            }
            return matrix3;
        }
        public static Matrix_Jagged Subtraction(double[,] matrix1, Matrix_Jagged matrix2)
        {
            Matrix_Jagged matrix3 = new Matrix_Jagged(matrix2.rows, matrix2.Columns);
            if (matrix1.GetLength(0) == matrix2.Rows && matrix1.GetLength(1) == matrix2.Columns)
            {
                for (int i = 0; i < matrix3.Rows; i++)
                {
                    double[] TempRow2 = matrix2.Values[i];
                    double[] TempRow3 = matrix3.Values[i];
                    for (int j = 0; j < matrix3.Columns; j++)
                    {
                        TempRow3[j] = matrix1[i, j] - TempRow2[j];
                    }
                }
            }
            else
            {
                matrix3.SetNaN();
            }
            return matrix3;
        }
        public static Matrix_Jagged Subtraction(Matrix_Jagged matrix1, double[,] matrix2)
        {
            Matrix_Jagged matrix3 = new Matrix_Jagged(matrix1.rows, matrix1.Columns);
            if (matrix1.Rows == matrix2.GetLength(0) && matrix1.Columns == matrix2.GetLength(1))
            {
                for (int i = 0; i < matrix3.Rows; i++)
                {
                    double[] TempRow1 = matrix1.Values[i];
                    double[] TempRow3 = matrix3.Values[i];
                    for (int j = 0; j < matrix3.Columns; j++)
                    {
                        TempRow3[j] = TempRow1[j] - matrix2[i, j];
                    }
                }
            }
            else
            {
                matrix3.SetNaN();
            }
            return matrix3;
        }

        // Operator "-"
        public static Matrix_Jagged operator -(Matrix_Jagged matrix1, Matrix_Jagged matrix2)
        {
            return Subtraction(matrix1, matrix2); 
        }
        public static Matrix_Jagged operator -(Matrix_Jagged matrix1)
        {
           return Negative(matrix1);
        }
        public static Matrix_Jagged operator -(double[][] matrix1, Matrix_Jagged matrix2)
        {
            return Subtraction(matrix1, matrix2);
        }
        public static Matrix_Jagged operator -(Matrix_Jagged matrix1, double[][] matrix2)
        {
            return Subtraction(matrix1, matrix2);
        }
        public static Matrix_Jagged operator -(double[,] matrix1, Matrix_Jagged matrix2)
        {
            return Subtraction(matrix1, matrix2);
        }
        public static Matrix_Jagged operator -(Matrix_Jagged matrix1, double[,] matrix2)
        {
            return Subtraction(matrix1, matrix2);
        }

        // Minus operator on object to get new jagged matrix
        /// <summary>
        /// Subtract matrix from this object and return a new matrix
        /// </summary>
        /// <param name="matrix2"></param>
        /// <returns></returns>
        public Matrix_Jagged Minus(Matrix_Jagged matrix2)
        {
            return Subtraction(this, matrix2);
        }
        public Matrix_Jagged Minus()
        {
            return Negative(this);
        }
        public Matrix_Jagged Minus(double[][] matrix2)
        {
            return Subtraction(this, matrix2);
        }
        public Matrix_Jagged Minus(double[,] matrix2)
        {
            return Subtraction(this, matrix2);
        }
        #endregion

        #region Addition
        /// <summary>
        /// Addition of two matrix like objects to give a new jagged matrix
        /// </summary>
        /// <param name="matrix1"></param>
        /// <param name="matrix2"></param>
        /// <returns></returns>
        public static Matrix_Jagged Addition(Matrix_Jagged matrix1, Matrix_Jagged matrix2)
        {
            Matrix_Jagged matrix3 = new Matrix_Jagged(matrix1.Rows, matrix2.Columns);

            if (matrix1.Rows == matrix2.Rows && matrix1.Columns == matrix2.Columns)
            {
                matrix3.Values = ArrayTools.Add(matrix1.Values, matrix2.Values);
            }
            else
            {
                matrix3.SetNaN();
            }
            return matrix3;
        }
        public static Matrix_Jagged Addition(double[][] matrix1, Matrix_Jagged matrix2)
        {
            Matrix_Jagged matrix3 = new Matrix_Jagged(matrix1.GetLength(0), matrix2.Columns);

            if (matrix1.GetLength(0) == matrix2.Rows && matrix1.GetLength(1) == matrix2.Columns)
            {
                matrix3.Values =ArrayTools.Add(matrix1, matrix2.Values);
            }
            else
            {
                matrix3.SetNaN();
            }
            return matrix3;
        }
        public static Matrix_Jagged Addition(Matrix_Jagged matrix1, double[][] matrix2)
        {
            Matrix_Jagged matrix3 = new Matrix_Jagged(matrix1.Rows, matrix2.GetLength(1));

            if (matrix1.Rows == matrix2.GetLength(0) && matrix1.Columns == matrix2.GetLength(1))
            {
                matrix3.Values =ArrayTools.Add(matrix1.Values, matrix2);
            }
            else
            {
                matrix3.SetNaN();
            }
            return matrix3;
        }
        public static Matrix_Jagged Addition(double[,] matrix1, Matrix_Jagged matrix2)
        {
            Matrix_Jagged matrix3 = new Matrix_Jagged(matrix2.rows, matrix2.Columns);
            if (matrix1.GetLength(0) == matrix2.Rows && matrix1.GetLength(1) == matrix2.Columns)
            {
                for (int i = 0; i < matrix3.Rows; i++)
                {
                    double[] TempRow2 = matrix2.Values[i];
                    double[] TempRow3 = matrix3.Values[i];
                    for (int j = 0; j < matrix3.Columns; j++)
                    {
                        TempRow3[j] = matrix1[i, j] + TempRow2[j];
                    }
                }
            }
            else
            {
                matrix3.SetNaN();
            }
            return matrix3;
        }
        public static Matrix_Jagged Addition(Matrix_Jagged matrix1, double[,] matrix2)
        {
            Matrix_Jagged matrix3 = new Matrix_Jagged(matrix1.rows, matrix1.Columns);
            if (matrix1.Rows == matrix2.GetLength(0) && matrix1.Columns == matrix2.GetLength(1))
            {
                for (int i = 0; i < matrix3.Rows; i++)
                {
                    double[] TempRow1 = matrix1.Values[i];
                    double[] TempRow3 = matrix3.Values[i];
                    for (int j = 0; j < matrix3.Columns; j++)
                    {
                        TempRow3[j] = TempRow1[j] + matrix2[i, j];
                    }
                }
            }
            else
            {
                matrix3.SetNaN();
            }
            return matrix3;
        }
        
        // Operator "+"
        public static Matrix_Jagged operator +(Matrix_Jagged matrix1, Matrix_Jagged matrix2)
        {
            return Addition(matrix1, matrix2); 
        }
        public static Matrix_Jagged operator +(double[][] matrix1, Matrix_Jagged matrix2)
        {
            return Addition(matrix1, matrix2);
        }
        public static Matrix_Jagged operator +(Matrix_Jagged matrix1, double[][] matrix2)
        {
            return Addition(matrix1, matrix2);
        }
        public static Matrix_Jagged operator +(double[,] matrix1, Matrix_Jagged matrix2)
        {
            return Addition(matrix1, matrix2);
        }
        public static Matrix_Jagged operator +(Matrix_Jagged matrix1, double[,] matrix2)
        {
            return Addition(matrix1, matrix2);
        }

        // Addition with similar objects to get a new object
        /// <summary>
        /// Add to this and return a jagged matrix
        /// </summary>
        /// <param name="matrix2"></param>
        /// <returns></returns>
        public Matrix_Jagged Plus(Matrix_Jagged matrix2)
        {
            return Addition(this, matrix2);
        }
        public Matrix_Jagged Plus(double[][] matrix2)
        {
            return Addition(this, matrix2);
        }
        public Matrix_Jagged Plus(double[,] matrix2)
        {
            return Addition(this, matrix2);
        }
        #endregion

        #region Exponent
        /// <summary>
        /// Exponent (all superscripts) type operations on matrix to give a new jagged matrix
        /// </summary>
        /// <param name="matrix1"></param>
        /// <param name="power"></param>
        /// <returns></returns>
        public static Matrix_Jagged Exponent(Matrix_Jagged matrix1, int power)
		{
            Matrix_Jagged matrix3 = new Matrix_Jagged(matrix1);

			int AbsPower;
			int Sign;

			if (matrix1.Rows == matrix1.Columns)
			{
				if (power >= 0)
				{
					AbsPower = power;
					Sign = 1;
				}
				else
				{
					AbsPower = -power;
					Sign =-1;
				}
				
				for (int i=0 ; i< AbsPower-1 ; i++)
				{
					matrix3=matrix3*matrix1;
				}

				if (Sign == 1)
				{
					return matrix3;
				}
				else
				{
					matrix3 = matrix3.Invert();
					return matrix3;
				}
			}
			else
			{
                matrix3.SetNaN();
				return matrix3;
			}
		}
        public static Matrix_Jagged Exponent(Matrix_Jagged matrix1, string c)
		{
            Matrix_Jagged output = new Matrix_Jagged(matrix1.Columns, matrix1.Rows);

			if(c=="T")
			{
				for (int i=0; i<matrix1.Rows; i++)
				{
					for (int j=0; j<matrix1.Columns; j++) output.Values[j][i] = matrix1.Values[i][j];
				}
			}
			else if ( c == "Sym")
			{
				output = (matrix1+(matrix1^"T"))/2.0D;
			}
			else if (c == "sym")
			{
				output = matrix1^"Sym";
			}
			else if (c == "Scew")
			{
				output = (matrix1-(matrix1^"T"))/2.0D;
			}
			else if (c == "scew")
			{
				output = matrix1^"Scew";
			}

			return output;
		
		}
        // Operatir "^"
        public static Matrix_Jagged operator ^(Matrix_Jagged matrix1, int power)
        {
            return Exponent(matrix1, power);
        }
        public static Matrix_Jagged operator ^(Matrix_Jagged matrix1, string c)
        {
            return Exponent(matrix1, c);
        }

        /// <summary>
        /// Power operators that return a jagged matrix
        /// </summary>
        /// <param name="matrix1"></param>
        /// <param name="power"></param>
        /// <returns></returns>
        public Matrix_Jagged Power(int power)
        {
            return Exponent(this, power);
        }
        public Matrix_Jagged Power(string c)
        {
            return Exponent(this, c);
        }
        #endregion

        public void Zero()
		{
			for(int i=0; i<this.Rows; i++)
			{
				for (int j=0; j<this.Columns; j++) this.Values[i][j] = 0.0D;
			}

		}

		public void Identity()
		{
			if(this.Rows == this.Columns)
			{
				this.Zero();
				for(int i=0; i<this.Rows; i++)
				{
					this.Values[i][i] = 1.0D;
				}
			}
			else
			{
                this.SetNaN();
			}
		}

		public Matrix_Jagged Invert()
		{
            Matrix_Jagged inverse = new Matrix_Jagged(this.Rows, this.Columns);
			double[][] temp = inverse.Values;
			InvMatrix(this.Values, this.rows, out temp);
			inverse.Values = temp;
			return inverse;
		}
        public Matrix_Jagged Invert(out double Det)
        {
            Matrix_Jagged inverse = new Matrix_Jagged(this.Rows, this.Columns);
            double[][] temp = inverse.Values;
            InvMatrix(this.Values, this.rows, out temp, out Det);
            inverse.Values = temp;
            return inverse;
        }
        public Matrix_Jagged Invert(Matrix_Jagged matrix)
		{
            Matrix_Jagged inverse = new Matrix_Jagged(matrix.rows, matrix.columns);
			
			double[][] temp;
			InvMatrix(matrix.Values, matrix.rows, out temp);
			inverse.Values = temp;
			return inverse;
		}

        public Matrix_Jagged Transpose()
		{
            Matrix_Jagged transpose = new Matrix_Jagged(this.Columns, this.Rows);
			for (int i=0; i<this.Rows; i++)
			{
				for (int j=0; j<this.Columns; j++) transpose.Values[j][i] = this.Values[i][j];
			}
			return transpose;
		}
        public Matrix_Jagged Transpose(Matrix_Jagged matrix)
		{
            Matrix_Jagged transpose = new Matrix_Jagged(matrix.Columns, matrix.Rows);
			for (int i=0; i<matrix.Rows; i++)
			{
                double[] TempRow = matrix.Values[i];
                for (int j = 0; j < matrix.Columns; j++) transpose.Values[j][i] = TempRow[j];
			}
			return transpose;
		}

		public double Det()
		{
			double det;
			det = DetMatrix(this.Values, this.Rows);
			return det;
		}
        public double Det(Matrix_Jagged matrix)
		{
			double det;
			det = DetMatrix(matrix.Values, matrix.Rows);
			return det;
		}
        public double Det3x3(Matrix_Jagged matrix)
		{
			double det;
			
			det = matrix.Values[0][0]*matrix.Values[1][1]*matrix.Values[2][2];
			det += matrix.Values[0][1]*matrix.Values[1][2]*matrix.Values[2][0];
			det += matrix.Values[0][2]*matrix.Values[1][0]*matrix.Values[2][1];
			det += -matrix.Values[2][0]*matrix.Values[1][1]*matrix.Values[0][2];
			det += -matrix.Values[2][1]*matrix.Values[1][2]*matrix.Values[0][0];
			det += -matrix.Values[2][2]*matrix.Values[1][0]*matrix.Values[0][1];

			return det;
		}


        public double Trace(Matrix_Jagged matrix)
		{
			double trace = 0.0D;
			for (int i=0 ; i< matrix.Rows ; i++)
			{
				trace += matrix.Values[i][i];
			}

			return trace;
		}
		public double Trace()
		{
			double trace = 0.0D;
			for (int i=0 ; i< this.Rows ; i++)
			{
				trace += this.Values[i][i];
			}

			return trace;
		}

		public void InitializeToZero()
		{
			for (int i=0 ; i< this.Rows ; i++)
			{
                double[] TempRow = matrix[i];
                for (int j = 0; j < this.Columns; j++) TempRow[j] = 0.0D;
			}
		}

        public Matrix_Jagged RotationMatrixAboutAnAxis(double RotationAngle_Rad, double[] UnitVectorAlonRotationAxis)
        {
            Matrix_Jagged NewMatrix = new Matrix_Jagged(3, 3);
            NewMatrix.Values = ArrayTools.RotationMatrixAboutAxis_Jagged(RotationAngle_Rad, UnitVectorAlonRotationAxis);
            return NewMatrix;
        }
        public Matrix_Jagged RotationMatrixAboutAxis0(double RotationAngle_Rad)
        {
            Matrix_Jagged NewMatrix = new Matrix_Jagged();
            NewMatrix.Values = ArrayTools.RotationMatrixAbout0Axis_Jagged(RotationAngle_Rad);
            return NewMatrix;
        }
        public Matrix_Jagged RotationMatrixAboutAxis1(double RotationAngle_Rad)
        {
            Matrix_Jagged NewMatrix = new Matrix_Jagged();
            NewMatrix.Values = ArrayTools.RotationMatrixAbout1Axis_Jagged(RotationAngle_Rad);
            return NewMatrix;
        }
        public Matrix_Jagged RotationMatrixAboutAxis2(double RotationAngle_Rad)
        {
            Matrix_Jagged NewMatrix = new Matrix_Jagged();
            NewMatrix.Values = ArrayTools.RotationMatrixAbout2Axis_Jagged(RotationAngle_Rad);
            return NewMatrix;
        }
        
        public Vector SolveLinearSystem(Vector RightHandSide)
        {
            Vector Solution = new Vector();
            Solution.Values = SolveLinearSystem(RightHandSide.Values);
            return Solution;
        }
        public Matrix_Jagged SolveLinearSystem(Matrix_Jagged RightHandSide)
        {
            int n = RightHandSide.Values.GetLength(0);
            double[] u = new double[n];
            for (int i = 0; i < n; i++)
            {
                u[i] = RightHandSide.Values[i][0];
            }
            Matrix_Jagged Solution = new Matrix_Jagged(SolveLinearSystem(u));
            return Solution;  
        }
        public double[] SolveLinearSystem(double[] RightHandSide)
        {
            int n = RightHandSide.Length;
            int[] index ;
            double d;
            Matrix_Jagged.ludcmp(this.matrix, n, out index, out d);
            Matrix_Jagged.lubksb(this.matrix, n, index, RightHandSide);
            return RightHandSide;
        }
        private int[] index;
        public void SolveLinearSystem_LUDecomp()
        {
            int n = this.matrix.GetLength(0);
            double d;
            Matrix_Jagged.ludcmp(this.matrix, n, out index, out d);
        }
        public void SolveLinearSystem_LUDecomp(out double Det)
        {
            int n = this.matrix.GetLength(0);
            Matrix_Jagged.ludcmp(this.matrix, n, out index, out Det);
            for (int i = 0; i < n; i++)
            {
                Det *= Values[i][i];
            }
        }
        public Vector SolveLinearSystem_BackSub(Vector RightHandSide)
        {
            int n = RightHandSide.Values.Length;
            Matrix_Jagged.lubksb(this.matrix, n, index, RightHandSide.Values);
            return RightHandSide;
        }
		/*
		C ********************************************************
		C ********************************************************
		C
		C     Back substitution for LU decomposition
		C     From NUMERICAL RECIPES
		C     lubksb
		C
		C     Translated from Fortran to C# by M. Negahban (10/29/02)
		C
		C ********************************************************
		*/

		
        public static void lubksb(double[][] a, int n,  int[] indx, double[] b)
        {
            int i, j, ll;
            int ii = -1;
            double sum;
            for (i = 0; i < n; i++)
            {
                ll = indx[i];
                sum = b[ll];
                b[ll] = b[i];
                if (ii != -1)
                {
                    double[] aI = a[i];
                    for (j = ii; j < i; j++)
                    {
                        sum += -aI[j] * b[j];
                    }
                }
                else
                {
                    if (sum != 0.0d)
                    {
                        ii = i;
                    }
                }
                b[i] = sum;
            }
            for (i = n - 1; i > -1; i--)
            {
                sum = b[i];
                double[] aI = a[i];
                if (i < n - 1)
                {
                    for (j = i + 1; j < n; j++)
                    {
                        sum += -aI[j] * b[j];
                    }
                }
                b[i] = sum / aI[i];
            }
        }

		/*
		C ********************************************************
		C ********************************************************
		C
		C     LU decomposition
		C     From NUMERICAL RECIPES
		C     ludcmp
		C
		C     Translated from Fortran to C# by M. Negahban (10/29/02)
		C
		C ********************************************************
		*/


        public static void ludcmp(double[][] a, int n,  out int[] indx, out double d)
        {
            double[] vv = new double[n];
            indx = new int[n];
            double tiny = 1.0e-20D;
            d = 1.0D;
            int i, j, k, imax;
            double aamax, sum, dum;
            imax = -1;

            for (i = 0; i < n; i++)
            {
                aamax = 0.0D;
                double[] aI = a[i];
                for (j = 0; j < n; j++)
                {
                    if (System.Math.Abs(aI[j]) > aamax) aamax = System.Math.Abs(aI[j]);
                }

                if (aamax == 0.0D) Console.Write("Singular Matrix");

                vv[i] = 1.0D / aamax;
            }
            for (j = 0; j < n; j++)
            {
                if (j > 0)
                {
                    for (i = 0; i < j; i++)
                    {
                        sum = a[i][j];
                        if (i > 0)
                        {
                            for (k = 0; k < i; k++) sum += -a[i][k] * a[k][j];
                            a[i][j] = sum;
                        }
                    }
                }
                aamax = 0.0D;
                for (i = j; i < n; i++)
                {
                    sum = a[i][j];
                    if (j > 0)
                    {
                        for (k = 0; k < j; k++) sum += -a[i][k] * a[k][j];
                        a[i][j] = sum;
                    }
                    dum = vv[i] * System.Math.Abs(sum);
                    if (dum >= aamax)
                    {
                        imax = i;
                        aamax = dum;
                    }
                }
                double[] aJ = a[j];
                if (j != imax)
                {
                    double[] aIMax = a[imax];
                    for (k = 0; k < n; k++)
                    {
                        dum = aIMax[k];
                        aIMax[k] = aJ[k];
                        aJ[k] = dum;
                    }
                    d = -d;
                    vv[imax] = vv[j];
                }
                indx[j] = imax;
                if (j != n - 1)
                {
                    if (aJ[j] == 0.0D) aJ[j] = tiny;
                    dum = 1.0D / a[j][j];
                    for (i = j + 1; i < n; i++) a[i][j] = a[i][j] * dum;
                }
            }
            if (a[n - 1][n - 1] == 0.0D) a[n - 1][n - 1] = tiny;
        }

		/*
		C
		C **********************************************
		C
		C     Calculate inverse and determinant of a matrix
		C     InvMatrix
		C
		C **********************************************
		*/
        public static void InvMatrix(double[][] a, int n, out double[][] ai, out double det)
        {
            /*
            * a[][] square matrix to be inverted
            * n number of rows or columns of a
            * ai[][] inverse of a
            */
            int i, j;
            double[][] ta = new double[n][];
            double[] l = new double[n];
            ai = new double[n][];
            int[] indx;
            double d;
            // store a copy of a in ta
            for (i = 0; i < n; i++)
            {
                double[] taI= new double[n];
                double[] aI = a[i];
                for (j = 0; j < n; j++)
                {
                    taI[j] = aI[j];
                }
                ta[i] = taI;
                ai[i] = new double[n];
            }
            // LU decompose a
            ludcmp(ta, n, out indx, out d);
            det = d;
            for (i = 0; i < n; i++)
            {
                det = det * ta[i][i];
            }
            for (j = 0; j < n; j++)
            {
                for (i = 0; i < n; i++)
                {
                    if (i == j) l[i] = 1.0D;
                    else l[i] = 0.0D;
                }

                // call back substitution
                lubksb(ta, n, indx, l);
                for (i = 0; i < n; i++) ai[i][j] = l[i];
            }
        }

		/*
		C
		C **********************************************
		C
		C     Calculate inverse of a matrix
		C     InvMatrix
		C
		C **********************************************
		*/
        public static void InvMatrix(double[][] a, int n, out double[][] ai)
        {
            /*
            * a[][] square matrix to be inverted
            * n number of rows or columns of a
            * ai[][] inverse of a
            */
            int i, j;
            double[][] ta = new double[n][];
            double[] l = new double[n];
            ai = new double[n][];
            int[] indx;
            double d;
            // store a copy of a in ta
            for (i = 0; i < n; i++)
            {
                double[] taI = new double[n];
                double[] aI = a[i];
                for (j = 0; j < n; j++)
                {
                    taI[j] = aI[j];
                }
                ta[i] = taI;
                ai[i] = new double[n];
            }
            // LU decompose a
            ludcmp(ta, n, out indx, out d);
            
            for (j = 0; j < n; j++)
            {
                for (i = 0; i < n; i++)
                {
                    if (i == j) l[i] = 1.0D;
                    else l[i] = 0.0D;
                }

                // call back substitution
                lubksb(ta, n, indx, l);
                for (i = 0; i < n; i++) ai[i][j] = l[i];
            }
        }
		/*
		C
		C **********************************************
		C
		C     Calculate determinant of a matrix
		C     DetMatrix
		C
		C **********************************************
		*/
        public static double DetMatrix(double[][] at, int n)
        {
            /*
            * a[][] square matrix to be inverted
            * n number of rows or columns of a
            * ai[][] inverse of a
            * det is determenant of a[,]
            */
            int i, j;
            double[][] a = new double[n][];
            for (i = 0; i < n; i++)
            {
                double[] aI = new double[n];
                double[] atI = at[i];
                for (j = 0; j < n; j++) aI [j] = atI[j];
                a[i] = aI;
            }

            int[] indx;
            double d, det;
            // LU decompose a
            ludcmp(a, n,  out indx, out d);
            det = d;
            for (i = 0; i < n; i++)
            {
                det = det * a[i][i];
            }
            return det;

        }
		public override string ToString()
		{
			string output = "";
			output += "Number of rows: " + this.Rows.ToString()+"\n";
			output += "Number of columns: " + this.Columns.ToString()+"\n";
			for(int i= 0; i< this.Rows ; i++)
			{
				output += "Row "+i.ToString()+": "+"\n";
				for(int j=0; j<this.Columns; j++)
				{
					output += "  Row "+i.ToString()+" Column "+j.ToString()+": "+ this.Values[i][j].ToString()+"\n";
				}
			}

			return output;
		}

        public  double[] CalculatePrincipalValues()
        {
            // Returns the eigenvalues in the form of an array of numbers (not sorted)
            if (this.Columns == this.rows)
            {
                int n = this.rows;
                double Realn = Convert.ToDouble(n);
                double RealnSquared = Realn * Realn;
                int np = n;
                int nrot = 0;
                int p, i, ip, iq;

                double[][] a = new double[n][];
                double[][] v = new double[n][];
                double[] d = new double[n];

                for (i = 0; i < n; i++)
                {
                    double[] RowI = this.Values[i];
                    double[] aI = new double[n];
                    for (p = 0; p < n; p++)
                    {
                        aI[p] = RowI[p];
                    }
                    a[i] = aI;
                    v[i] = new double[n];
                }

                ////////////////////////////////////////////////////////////////

                int nmax = 500;
                double c, g, h, s, sm, t, tau, theta, tresh;
                double[] b = new double[nmax];
                double[] z = new double[nmax];

                for (p = 0; p < n; p++)
                {
                    v[p][p] = 1.0d;
                }

                for (p = 0; p < n; p++)
                {
                    b[p] = a[p][p];
                    d[p] = b[p];
                    z[p] = 0.0d;
                }


                nrot = 0;

                for (i = 0; i < 50; i++)
                {

                    sm = 0.0d;
                    for (ip = 1; ip < n; ip++)
                    {
                        double[] aRow = a[ip - 1];
                        for (iq = ip + 1; iq < n + 1; iq++)
                        {
                            sm = sm + Math.Abs(aRow[iq - 1]);
                        }
                    }


                    ///////////////////////////////////////////////////

                    if (sm == 0.0d) break;


                    if (i < 3)
                    {
                        tresh = 0.2d * sm / RealnSquared;
                    }
                    else
                    {
                        tresh = 0.0d;
                    }
                    /////////////////////////////////////////////////

                    for (ip = 1; ip < n; ip++)
                    {
                        double[] aIpM1 = a[ip - 1];
                        double zIpM1 = z[ip - 1];
                        double dIpM1 = d[ip - 1];
                        for (iq = ip + 1; iq < n + 1; iq++)
                        {
                            double aIpM1IqM1 = aIpM1[iq - 1];
                            double[] aIqM1 = a[iq - 1];
                            double dIqM1 = d[iq - 1];
                            g = 100.0d * Math.Abs(aIpM1IqM1);
                            if ((i > 3) && (Math.Abs(dIpM1) + g == Math.Abs(dIpM1)) && (Math.Abs(dIqM1) + g == Math.Abs(dIqM1)))
                            {
                                aIpM1IqM1 = 0.0d;
                            }
                            else if (Math.Abs(aIpM1IqM1) > tresh)
                            {
                                h = dIqM1 - dIpM1;
                                if (Math.Abs(h) + g == Math.Abs(h))
                                {
                                    t = aIpM1IqM1 / h;
                                }
                                else
                                {
                                    theta = 0.5d * h / aIpM1IqM1;
                                    t = 1.0d / (Math.Abs(theta) + Math.Sqrt(1.0d + theta * theta));
                                    if (theta < 0.0d) t = -t;
                                }
                                c = 1.0d / Math.Sqrt(1.0d + t * t);
                                s = t * c;
                                tau = s / (1.0d + c);
                                h = t * aIpM1IqM1;
                                zIpM1 -= h;
                                z[iq - 1] += h;
                                dIpM1 -= h;
                                dIqM1 += h;
                                aIpM1IqM1 = 0.0d;


                                for (int j = 1; j < ip; j++)
                                {
                                    g = a[j - 1][ip - 1];
                                    h = a[j - 1][iq - 1];
                                    a[j - 1][ip - 1] = g - s * (h + g * tau);
                                    a[j - 1][iq - 1] = h + s * (g - h * tau);
                                }
                                for (int j = ip + 1; j < iq; j++)
                                {
                                    g = a[ip - 1][j - 1];
                                    h = a[j - 1][iq - 1];
                                    a[ip - 1][j - 1] = g - s * (h + g * tau);
                                    a[j - 1][iq - 1] = h + s * (g - h * tau);
                                }
                                for (int j = iq + 1; j < n + 1; j++)
                                {
                                    g = aIpM1[j - 1];
                                    h = aIqM1[j - 1];
                                    aIpM1[j - 1] = g - s * (h + g * tau);
                                    aIqM1[j - 1] = h + s * (g - h * tau);
                                }
                                for (int j = 1; j < n + 1; j++)
                                {
                                    g = v[j - 1][ip - 1];
                                    h = v[j - 1][iq - 1];
                                    v[j - 1][ip - 1] = g - s * (h + g * tau);
                                    v[j - 1][iq - 1] = h + s * (g - h * tau);
                                }
                                nrot = nrot + 1;
                            }



                        }
                    }



                    for (ip = 1; ip < n + 1; ip++)
                    {
                        b[ip - 1] = b[ip - 1] + z[ip - 1];
                        d[ip - 1] = b[ip - 1];
                        z[ip - 1] = 0.0d;
                    }


                }


                
                return d;
            }
            else
            {
                double[] d = new double[this.Rows];
                for (int i = 0; i < this.rows; i++)
                {
                    d[i] = double.NaN;
                }
                return d;
            }

        }
        public Vector SolveUsingSingularValueDecomposition(Vector RHS, out double ConditionNumber)//, out double rank, out double nullity)
        {
            Matrix_Jagged A = this;
            Matrix_Jagged V;
            Vector W;
            SingularValueDecomposition(ref A, out W, out V);

            Vector x = (A ^ "T" )* RHS;
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
            x = V * x;
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
            return  x;

        }
        public Vector SolveUsingSingularValueDecomposition(Vector RHS, out double ConditionNumber, out Vector W, out Matrix_Jagged V)//, out double rank, out double nullity)
        {
            Matrix_Jagged A = this;
            //Matrix V;
            //Vector W;
            SingularValueDecomposition(ref A, out W, out V);

            Vector x = (A ^ "T") * RHS;
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
            x = V * x;
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
        public void SingularValueDecomposition(ref Matrix_Jagged A, out Vector W, out Matrix_Jagged V)
        {
            //Adaptation of singular value decomposition from Numerical Recipes
            // A = U W V^T
            // Replaces A with U
            int m = A.Rows;
            int n = A.Values[0].Length;
            double[][] u = A.Values;
            V = new Matrix_Jagged(n, n);
            double[][] v = V.Values;
            bool flag;
            W = new Vector(m);
            double[] w = W.Values;
            int i, its, j, jj, k, l, nm;
            nm = 0;
            l = 0;
            double anorm, c, f, g, h, s, scale, x, y, z;
            double[] rv1 = new double[n];
            g = scale = anorm = 0.0;
            double eps = 1.0E-20;
            for (i = 0; i < n; i++)
            {
                l = i + 2;
                rv1[i] = scale * g;
                g = s = scale = 0.0;
                if (i < m)
                {
                    for (k = i; k < m; k++) scale += Math.Abs(u[k][i]);
                    if (scale != 0.0)
                    {
                        for (k = i; k < m; k++)
                        {
                            u[k][i] /= scale;
                            s += u[k][i] * u[k][i];
                        }
                        f = u[i][i];
                        g = -FortranSign(Math.Sqrt(s), f);
                        h = f * g - s;
                        u[i][i] = f - g;
                        for (j = l - 1; j < n; j++)
                        {
                            for (s = 0.0, k = i; k < m; k++) s += u[k][i] * u[k][j];
                            f = s / h;
                            for (k = i; k < m; k++) u[k][j] += f * u[k][i];
                        }
                        for (k = i; k < m; k++) u[k][i] *= scale;
                    }
                }
                w[i] = scale * g;
                g = s = scale = 0.0;
                if (i + 1 <= m && i + 1 != n)
                {
                    for (k = l - 1; k < n; k++) scale += Math.Abs(u[i][k]);
                    if (scale != 0.0)
                    {
                        for (k = l - 1; k < n; k++)
                        {
                            u[i][k] /= scale;
                            s += u[i][k] * u[i][k];
                        }
                        f = u[i][l - 1];
                        g = -FortranSign(Math.Sqrt(s), f);
                        h = f * g - s;
                        u[i][l - 1] = f - g;
                        for (k = l - 1; k < n; k++) rv1[k] = u[i][k] / h;
                        for (j = l - 1; j < m; j++)
                        {
                            for (s = 0.0, k = l - 1; k < n; k++) s += u[j][k] * u[i][k];
                            for (k = l - 1; k < n; k++) u[j][k] += s * rv1[k];
                        }
                        for (k = l - 1; k < n; k++) u[i][k] *= scale;
                    }
                }
                anorm = Math.Max(anorm, (Math.Abs(w[i]) + Math.Abs(rv1[i])));
            }
            for (i = n - 1; i >= 0; i--)
            { //Accumulation of right-hand transformations.
                if (i < n - 1)
                {
                    if (g != 0.0)
                    {
                        for (j = l; j < n; j++) //Double division to avoid possible under ow.
                            v[j][i] = (u[i][j] / u[i][l]) / g;
                        for (j = l; j < n; j++)
                        {
                            for (s = 0.0, k = l; k < n; k++) s += u[i][k] * v[k][j];
                            for (k = l; k < n; k++) v[k][j] += s * v[k][i];
                        }
                    }
                    for (j = l; j < n; j++) v[i][j] = v[j][i] = 0.0;
                }
                v[i][i] = 1.0;
                g = rv1[i];
                l = i;
            }
            for (i = Math.Min(m, n) - 1; i >= 0; i--)
            { //Accumulation of left-hand transformations.
                l = i + 1;
                g = w[i];
                for (j = l; j < n; j++) u[i][j] = 0.0;
                if (g != 0.0)
                {
                    g = 1.0 / g;
                    for (j = l; j < n; j++)
                    {
                        for (s = 0.0, k = l; k < m; k++) s += u[k][i] * u[k][j];
                        f = (s / u[i][i]) * g;
                        for (k = i; k < m; k++) u[k][j] += f * u[k][i];
                    }
                    for (j = i; j < m; j++) u[j][i] *= g;
                }
                else for (j = i; j < m; j++) u[j][i] = 0.0;
                ++u[i][i];
            }
            for (k = n - 1; k >= 0; k--)
            { //Diagonalization of the bidiagonal form: Loop over
                for (its = 0; its < 30; its++)
                { //singular values, and over allowed iterations.
                    flag = true;
                    for (l = k; l >= 0; l--)
                    { //Test for splitting.
                        nm = l - 1;
                        if (l == 0 || Math.Abs(rv1[l]) <= eps * anorm)
                        {
                            flag = false;
                            break;
                        }
                        if (Math.Abs(w[nm]) <= eps * anorm) break;
                    }
                    if (flag)
                    {
                        c = 0.0; //Cancellation of rv1[l], if l > 0.
                        s = 1.0;
                        for (i = l; i < k + 1; i++)
                        {
                            f = s * rv1[i];
                            rv1[i] = c * rv1[i];
                            if (Math.Abs(f) <= eps * anorm) break;
                            g = w[i];
                            h = PYTHAG(f, g);
                            w[i] = h;
                            h = 1.0 / h;
                            c = g * h;
                            s = -f * h;
                            for (j = 0; j < m; j++)
                            {
                                y = u[j][nm];
                                z = u[j][i];
                                u[j][nm] = y * c + z * s;
                                u[j][i] = z * c - y * s;
                            }
                        }
                    }
                    z = w[k];
                    if (l == k)
                    { //Convergence.
                        if (z < 0.0)
                        { //Singular value is made nonnegative.
                            w[k] = -z;
                            for (j = 0; j < n; j++) v[j][k] = -v[j][k];
                        }
                        break;
                    }
                    if (its == 29) Console.Write("no convergence in 30 svdcmp iterations");
                    x = w[l]; //Shift from bottom 2-by-2 minor.
                    nm = k - 1;
                    y = w[nm];
                    g = rv1[nm];
                    h = rv1[k];
                    f = ((y - z) * (y + z) + (g - h) * (g + h)) / (2.0 * h * y);
                    g = PYTHAG(f, 1.0);
                    f = ((x - z) * (x + z) + h * ((y / (f + FortranSign(g, f))) - h)) / x;
                    c = s = 1.0; //Next QR transformation:
                    for (j = l; j <= nm; j++)
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
                        f = x * c + g * s;
                        g = g * c - x * s;
                        h = y * s;
                        y *= c;
                        for (jj = 0; jj < n; jj++)
                        {
                            x = v[jj][j];
                            z = v[jj][i];
                            v[jj][j] = x * c + z * s;
                            v[jj][i] = z * c - x * s;
                        }
                        z = PYTHAG(f, h);
                        w[j] = z; //Rotation can be arbitrary if z D 0.
                        if (z!=0.0D)
                        {
                            z = 1.0 / z;
                            c = f * z;
                            s = h * z;
                        }
                        f = c * g + s * y;
                        x = c * y - s * g;
                        for (jj = 0; jj < m; jj++)
                        {
                            y = u[jj][j];
                            z = u[jj][i];
                            u[jj][j] = y * c + z * s;
                            u[jj][i] = z * c - y * s;
                        }
                    }
                    rv1[l] = 0.0;
                    rv1[k] = f;
                    w[k] = x;
                }
            }
        }


        public double PYTHAG (double a, double b)
        {
            //Adapted from Numerical Recipes
            //Computes (a^2+b^2)^(1/2) without destructive underflow or overflow
            double absa, absb;
            absa = Math.Abs(a);
            absb = Math.Abs(b);
            if(absa > absb)
            {
                return absa*Math.Sqrt(1.0D+(absb/absa)*(absb/absa));
            }
            else
            {
                if(absb == 0.0D)
                {
                    return 0.0D;
                }
                else
                {
                    return absb*Math.Sqrt(1.0D+(absa/absb)*(absa/absb));
                }
            }
        }
        public double FortranSign(double x, double y)
        {
            if(y>=0.0D)
            {
                return Math.Abs(x);
            }
            else
            {
                return -Math.Abs(x);
            }
        }
	}
    [Serializable]
	public class IntMatrix_Jagged
	{
		private int rows;
		private int columns;
		private int[][] matrix;

		public int Rows
		{
			get
			{
                rows = matrix.GetLength(0);
				return rows;
			}
			set
			{
				rows = value;
			}
		}
		public int Columns
		{
			get
			{
                columns = matrix[0].GetLength(0);
				return columns;
			}
			set
			{
				columns = value;
			}
			
		}
		public int[][] Values
		{
			get
			{
				return matrix;
			}
			set
			{
				matrix = value;
			}
		}

		public IntMatrix_Jagged(int NumberOfRows, int NumberOfColumns)
		{
			this.Rows = NumberOfRows;
			this.Columns = NumberOfColumns;
			this.matrix = new int[rows][];
            for (int i = 0; i < NumberOfRows; i++)
            {
                this.matrix[i] = new int[NumberOfColumns];
            }
		}
	}
}
