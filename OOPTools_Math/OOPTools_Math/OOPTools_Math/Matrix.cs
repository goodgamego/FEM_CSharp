using System;

namespace OOPTools_Math
{
	/// <summary>
	/// Summary description for Matrix class
    /// 
    /// Mehrdad Negahban
    /// 2005
    /// Modified: 12-30-2010
	/// </summary>
    [Serializable]
    public struct Matrix 
	{
		private int rows;
		private int columns;
		private double[,] matrix;

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
                columns = Values.GetLength(1);
				return columns;
			}
			set
			{
				columns = value;
			}
			
		}
		public double[,] Values
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

		public Matrix(int NumberOfRows, int NumberOfColumns)
		{
			rows = NumberOfRows;
			columns = NumberOfColumns;
			matrix = new double[rows,columns];
            index = new int[0];
		}
		public Matrix(Matrix m)
		{
			rows = m.Rows;
			columns = m.Columns;
			matrix = new double[rows,columns];
            index = new int[0];
			for (int i=0; i<rows; i++)
			{
				for (int j=0; j<columns; j++) matrix[i,j]=m.Values[i,j];
			}
		}
		public Matrix(double[,] mArray)
		{
			rows = mArray.GetLength(0);
			columns = mArray.GetLength(1);
			matrix = new double[rows,columns];
            index = new int[0];
			for (int i=0; i<rows; i++)
			{
				for (int j=0; j<columns; j++) matrix[i,j]=mArray[i,j];
			}
		}
        public Matrix(double[][] mArray)
        {
            /// double[][] array needs to be rectangular array (i.e., similar to [,])
            rows = mArray.GetLength(0);
            columns = mArray[0].GetLength(0);
            matrix = new double[rows, columns];
            index = new int[0];
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++) matrix[i, j] = mArray[i][j];
            }
        }
        public Matrix(double[] X)
        {
            /// Makes a column matrix out of a one dimensional array of numbers
            //ColumnMatrix(X);
            rows = X.Length;
            columns = 1;
            matrix = new double[rows, columns];
            index = new int[0];
            for (int i = 0; i < rows; i++) matrix[i, 0] = X[i];
        }
        public Matrix(Vector X)
        {
            /// Makes a column matrix with vector as a column
            rows = X.Values.Length;
            columns = 1;
            matrix = new double[rows, columns];
            index = new int[0];
            for (int i = 0; i < rows; i++) matrix[i, 0] = X.Values[i];
        }
        public Matrix(Vector X, Vector Y)
        {
            /// Makes a matrix with each vector as a column
            rows = X.Values.Length;
            columns = 2;
            matrix = new double[rows, columns];
            index = new int[0];
            for (int i = 0; i < rows; i++)
            {
                matrix[i, 0] = X.Values[i];
                matrix[i, 1] = Y.Values[i];
            }
        }
        public Matrix(Vector X, Vector Y, Vector Z)
        {
            /// Makes a matrix with each vector as a column
            rows = X.Values.Length;
            columns = 3;
            matrix = new double[rows, columns];
            index = new int[0];
            for (int i = 0; i < rows; i++)
            {
                matrix[i, 0] = X.Values[i];
                matrix[i, 1] = Y.Values[i];
                matrix[i, 2] = Z.Values[i];
            }
        }
        public Matrix(Vector[] X)
        {
            /// Makes a matrix with each vector as a column
            rows = X[0].Values.Length;
            columns = X.Length;
            matrix = new double[rows, columns];
            index = new int[0];
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    matrix[i, j] = X[j].Values[i];
                }
            }
        }
        public Matrix TensorProduct(Vector X, Vector Y)
        {
            /// Makes a matrix that is a tensor product of the two vectors
            /// M[i,j] = X[i]*Y[j]
            int nRows = X.Values.Length;
            int nColumns = Y.Values.Length;
            Matrix Matrix1 = new Matrix(nRows, nColumns);

            for (int i = 0; i < nRows; i++)
            {
                for (int j = 0; j < nColumns; j++) Matrix1.Values[i, j] = X.Values[i] * Y.Values[j];
            }
            return Matrix1;
        }
        public void RowMatrix(double[] X)
        {
            /// Makes a row matrix out of a one dimensional array of numbers
            rows = 1;
            columns = X.Length;
            matrix = new double[rows, columns];
            
            for (int j = 0; j < columns; j++) matrix[0, j] = X[j];

        }
        public void ColumnMatrix(double[] X)
        {
            /// Makes a column matrix out of a one dimensional array of numbers
            rows = X.Length;
            columns = 1;
            matrix = new double[rows, columns];

            for (int i = 0; i < rows; i++) matrix[i, 0] = X[i];

        }
        public double[] GetColumn(int ColumnNumber)
        {
            int colLength = this.Values.GetLength(0);
            double[] col = new double[colLength];
            for (int i = 0; i < colLength; i++)
            {
                col[i] = this.Values[i, ColumnNumber];
            }
            return col;
        }
        public double[] GetRow(int RowNumber)
        {
            int rowLength = this.Values.GetLength(1);
            double[] row = new double[rowLength];
            for (int i = 0; i < rowLength; i++)
            {
                row[i] = this.Values[RowNumber, i];
            }
            return row;
        }

        public double[][] ConvertToJaggedArray()
        {
            double[][] JagM = new double[this.Rows][];
            for (int i = 0; i < this.Rows; i++)
            {
                JagM[i] = new double[this.Columns];
                for (int j = 0; j < this.Columns; j++)
                {
                    JagM[i][j] = this.Values[i, j];
                }
            }
            return JagM;
        }
		public  Matrix SetNaN()
		{
				for (int i=0 ; i< this.Rows ; i++)
				{
					for (int j=0 ; j< this.Columns ; j++)
					{
						this.Values[i,j] = System.Double.NaN;
					}
				}
				
			return this;
		}

        #region Operator Multiplication
        public static Matrix Multiply(Matrix matrix1, Matrix matrix2)
        {
            Matrix matrix3 = new Matrix(matrix1.Rows, matrix2.Columns);

            if (matrix1.Columns == matrix2.Rows)
            {
                for (int i = 0; i < matrix3.Rows; i++)
                {
                    for (int j = 0; j < matrix3.Columns; j++)
                    {
                        for (int k = 0; k < matrix1.Columns; k++) matrix3.Values[i, j] += matrix1.Values[i, k] * matrix2.Values[k, j];
                    }
                }
            }
            else
            {
                for (int i = 0; i < matrix3.Rows; i++)
                {
                    for (int j = 0; j < matrix3.Columns; j++)
                    {
                        matrix3.Values[i, j] += System.Double.NaN;
                    }
                }

            }
            return matrix3;
        }
        public static Matrix Multiply(Matrix matrix1, double[,] matrix2)
        {
            Matrix matrix3 = new Matrix(matrix1.Rows, matrix2.GetLength(1));

            if (matrix1.Columns == matrix2.GetLength(0))
            {
                for (int i = 0; i < matrix3.Rows; i++)
                {
                    for (int j = 0; j < matrix3.Columns; j++)
                    {
                        for (int k = 0; k < matrix1.Columns; k++) matrix3.Values[i, j] += matrix1.Values[i, k] * matrix2[k, j];
                    }
                }
            }
            else
            {
                for (int i = 0; i < matrix3.Rows; i++)
                {
                    for (int j = 0; j < matrix3.Columns; j++)
                    {
                        matrix3.Values[i, j] += System.Double.NaN;
                    }
                }

            }
            return matrix3;
        }

        public static Matrix Multiply(Matrix matrix1, double[] matrix2)
        {
            Matrix matrix3 = new Matrix(matrix1.Rows, 1);

            if (matrix1.Columns == matrix2.Length)
            {
                for (int i = 0; i < matrix3.Rows; i++)
                {
                    for (int k = 0; k < matrix1.Columns; k++) matrix3.Values[i, 0] += matrix1.Values[i, k] * matrix2[k];
                }
            }
            else
            {
                for (int i = 0; i < matrix3.Rows; i++)
                {
                    matrix3.Values[i, 0] = System.Double.NaN;
                }

            }
            return matrix3;
        }
        public static Vector Multiply(Matrix matrix1, Vector matrix2)
        {
            Matrix matrix3 = matrix1 * matrix2.Values;
            Vector newVector = new Vector(matrix3.Rows);
            for (int i = 0; i < matrix3.Rows; i++)
            {
                newVector.Values[i] = matrix3.Values[i, 0];
            }
            return newVector;
        }

        public static Matrix Multiply(double[] matrix1, Matrix matrix2)
        {
            Matrix matrix3 = new Matrix(1, matrix2.columns);

            if (matrix1.Length == matrix2.Rows)
            {
                for (int i = 0; i < matrix3.Columns; i++)
                {
                    for (int k = 0; k < matrix2.Rows; k++) matrix3.Values[0, i] += matrix1[k] * matrix2.Values[k, i];
                }
            }
            else
            {
                for (int i = 0; i < matrix3.Columns; i++)
                {
                    matrix3.Values[0, i] = System.Double.NaN;
                }

            }
            return matrix3;
        }
        public static Matrix Multiply(Vector matrix1, Matrix matrix2)
        {
            return matrix1.Values * matrix2;
        }


        public static Matrix Multiply(double[,] matrix1, Matrix matrix2)
        {
            Matrix matrix3 = new Matrix(matrix1.GetLength(0), matrix2.Columns);

            if (matrix1.GetLength(1) == matrix2.Rows)
            {
                for (int i = 0; i < matrix3.Rows; i++)
                {
                    for (int j = 0; j < matrix3.Columns; j++)
                    {
                        for (int k = 0; k < matrix1.GetLength(1); k++) matrix3.Values[i, j] += matrix1[i, k] * matrix2.Values[k, j];
                    }
                }
            }
            else
            {
                for (int i = 0; i < matrix3.Rows; i++)
                {
                    for (int j = 0; j < matrix3.Columns; j++)
                    {
                        matrix3.Values[i, j] += System.Double.NaN;
                    }
                }

            }
            return matrix3;
        }

        public static Matrix Multiply(double scalar, Matrix matrix1)
        {
            Matrix matrix3 = new Matrix(matrix1.Rows, matrix1.Columns);

            for (int i = 0; i < matrix3.Rows; i++)
            {
                for (int j = 0; j < matrix3.Columns; j++)
                {
                    matrix3.Values[i, j] += scalar * matrix1.Values[i, j];
                }
            }

            return matrix3;
        }

        public static Matrix Multiply(Matrix matrix1, double scalar)
        {
            Matrix matrix3 = new Matrix(matrix1.Rows, matrix1.Columns);

            for (int i = 0; i < matrix3.Rows; i++)
            {
                for (int j = 0; j < matrix3.Columns; j++)
                {
                    matrix3.Values[i, j] += scalar * matrix1.Values[i, j];
                }
            }

            return matrix3;
        }
        // Operator on matrix multiplication
        public static Matrix operator *(Matrix matrix1, Matrix matrix2)
		{
            return Multiply(matrix1, matrix2); 
		}
        public static Matrix operator *(Matrix matrix1, double[,] matrix2)
        {
            return Multiply(matrix1, matrix2);
        }
        
        public static Matrix operator *(Matrix matrix1, double[] matrix2)
        {
            return Multiply(matrix1, matrix2);
        }
        public static Vector operator *(Matrix matrix1, Vector matrix2)
        {
            return Multiply(matrix1, matrix2);
        }

        public static Matrix operator *(double[] matrix1, Matrix matrix2)
        {
            return Multiply(matrix1, matrix2);
        }
        public static Matrix operator *(Vector matrix1, Matrix matrix2)
        {
            return Multiply(matrix1, matrix2);
        }
        public static Matrix operator *(double[,] matrix1, Matrix matrix2)
        {
            return Multiply(matrix1, matrix2);
        }

		public static Matrix operator *(double scalar, Matrix matrix1)
		{
            return Multiply(scalar, matrix1);
		}
		public static Matrix operator *(Matrix matrix1, double scalar)
		{
            return Multiply(scalar, matrix1);
		}
        #endregion

        #region Division
        public static Matrix Division(Matrix matrix1, double scalar)
        {
            Matrix matrix3 = new Matrix(matrix1.Rows, matrix1.Columns);
            double inverse = 1.0d / scalar;
            matrix3.Values = ArrayTools.Multiply(inverse, matrix1.Values);
            return matrix3;
        }
        public static Matrix operator /(Matrix matrix1, double scalar)
		{
            return Division(matrix1, scalar);
		}
        #endregion

        #region Addition
        public static Matrix Addition(Matrix matrix1, Matrix matrix2)
		{
			Matrix matrix3 = new Matrix(matrix1.Rows,matrix2.Columns);

			if (matrix1.Rows == matrix2.Rows && matrix1.Columns == matrix2.Columns)
			{
                matrix3.Values =ArrayTools.Add(matrix1.Values, matrix2.Values);
			}
			else
			{
                matrix3.SetNaN();
			}
			return matrix3;
		}
        public static Matrix Addition(double[,] matrix1, Matrix matrix2)
        {
            Matrix matrix3 = new Matrix(matrix1.GetLength(0), matrix2.Columns);

            if (matrix1.GetLength(0) == matrix2.Rows && matrix1.GetLength(1) == matrix2.Columns)
            {
                matrix3.Values = ArrayTools.Add(matrix1, matrix2.Values);
            }
            else
            {
                matrix3.SetNaN();
            }
            return matrix3;
        }
        public static Matrix Addition(Matrix matrix1, double[,] matrix2)
        {
            Matrix matrix3 = new Matrix(matrix1.Rows, matrix2.GetLength(1));

            if (matrix1.Rows == matrix2.GetLength(0) && matrix1.Columns == matrix2.GetLength(1))
            {
                matrix3.Values = ArrayTools.Add(matrix1.Values, matrix2);
            }
            else
            {
                matrix3.SetNaN();
            }
            return matrix3;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="matrix1"></param>
        /// <param name="matrix2"></param>
        /// <returns></returns>
        public static Matrix operator +(Matrix matrix1, Matrix matrix2)
        {
            return Addition(matrix1, matrix2);  
        }
        public static Matrix operator +(double[,] matrix1, Matrix matrix2)
        {
            return Addition(matrix1, matrix2); 
        }
        public static Matrix operator +(Matrix matrix1, double[,] matrix2)
        {
            return Addition(matrix1, matrix2); 
        }
        #endregion

        #region Subtraction
        public static Matrix Subtract(Matrix matrix1, Matrix matrix2)
        {
            Matrix matrix3 = new Matrix(matrix1.Rows, matrix2.Columns);

            if (matrix1.Rows == matrix2.Rows && matrix1.Columns == matrix2.Columns)
            {
                matrix3.Values = ArrayTools.Subtract(matrix1.Values, matrix2.Values);
            }
            else
            {
                matrix3.SetNaN();
            }
            return matrix3;
        }
        public static Matrix Negative(Matrix matrix1)
        {
            Matrix matrix3 = new Matrix(matrix1.Rows, matrix1.Columns);
            matrix3.Values = ArrayTools.Negative(matrix1.Values);
            return matrix3;
        }
        public static Matrix Subtract(double[,] matrix1, Matrix matrix2)
        {
            Matrix matrix3 = new Matrix(matrix1.GetLength(0), matrix2.Columns);

            if (matrix1.GetLength(0) == matrix2.Rows && matrix1.GetLength(1) == matrix2.Columns)
            {
                matrix3.Values = ArrayTools.Subtract(matrix1, matrix2.Values);
            }
            else
            {
                matrix3.SetNaN();
            }
            return matrix3;
        }
        public static Matrix Subtract(Matrix matrix1, double[,] matrix2)
        {
            Matrix matrix3 = new Matrix(matrix1.Rows, matrix2.GetLength(1));

            if (matrix1.Rows == matrix2.GetLength(0) && matrix1.Columns == matrix2.GetLength(1))
            {
                matrix3.Values = ArrayTools.Subtract(matrix1.Values, matrix2);
            }
            else
            {
                matrix3.SetNaN();
            }
            return matrix3;
        }

        public static Matrix operator -(Matrix matrix1, Matrix matrix2)
        {
            return Subtract(matrix1, matrix2); 
        }
        public static Matrix operator -(Matrix matrix1)
        {
            return Negative(matrix1);
        }
        public static Matrix operator -(double[,] matrix1, Matrix matrix2)
        {
            return Subtract(matrix1, matrix2); 
        }
        public static Matrix operator -(Matrix matrix1, double[,] matrix2)
        {
            return Subtract(matrix1, matrix2); 
        }
        #endregion


        public Matrix RotationMatrixAboutAnAxis(double RotationAngle_Rad, double[] UnitVectorAlonRotationAxis)
        {
            Matrix NewMatrix = new Matrix(3, 3);
            NewMatrix.Values = ArrayTools.RotationMatrixAboutAxis(RotationAngle_Rad, UnitVectorAlonRotationAxis);
            return NewMatrix;
        }
        public Matrix RotationMatrixAboutAxis0(double RotationAngle_Rad)
        {
            Matrix NewMatrix = new Matrix();
            NewMatrix.Values = ArrayTools.RotationMatrixAbout0Axis(RotationAngle_Rad);
            return NewMatrix;
        }
        public Matrix RotationMatrixAboutAxis1(double RotationAngle_Rad)
        {
            Matrix NewMatrix = new Matrix();
            NewMatrix.Values = ArrayTools.RotationMatrixAbout1Axis(RotationAngle_Rad);
            return NewMatrix;
        }
        public Matrix RotationMatrixAboutAxis2(double RotationAngle_Rad)
        {
            Matrix NewMatrix = new Matrix();
            NewMatrix.Values = ArrayTools.RotationMatrixAbout2Axis(RotationAngle_Rad);
            return NewMatrix;
        }

        #region Power
        public static Matrix operator ^(Matrix matrix1, int power)
		{

			Matrix matrix3 = new Matrix(matrix1);

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

		public static Matrix operator ^(Matrix matrix1, string c)
		{
			Matrix output = new Matrix(matrix1.Columns,matrix1.Rows);

			if(c=="T")
			{
				for (int i=0; i<matrix1.Rows; i++)
				{
					for (int j=0; j<matrix1.Columns; j++) output.Values[j,i] = matrix1.Values[i,j];
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
        #endregion

        public void Zero()
		{
			for(int i=0; i<this.Rows; i++)
			{
				for (int j=0; j<this.Columns; j++) this.Values[i,j] = 0.0D;
			}

		}

		public void Identity()
		{
			if(this.Rows == this.Columns)
			{
				this.Zero();
				for(int i=0; i<this.Rows; i++)
				{
					this.Values[i,i] = 1.0D;
				}
			}
			else
			{
                this.SetNaN();
			}
		}

		public Matrix Invert()
		{
			Matrix inverse = new Matrix(this.Rows,this.Columns);
			double[,] temp = new double[this.Rows,this.Columns];
			InvMatrix(this.Values, this.rows, out temp);
			inverse.Values = temp;
			return inverse;
		}
		public Matrix Invert(Matrix matrix)
		{
			Matrix inverse = new Matrix(matrix.rows,matrix.columns);
			
			double[,] temp = new double[this.Rows,this.Columns];
			InvMatrix(matrix.Values, matrix.rows, out temp);
			inverse.Values = temp;
			return inverse;
		}
        public Matrix Invert(Matrix matrix, out double det)
        {
            Matrix inverse = new Matrix(matrix.rows, matrix.columns);

            double[,] temp = new double[this.Rows, this.Columns];
            InvMatrix(matrix.Values, matrix.rows, out temp, out det);
            inverse.Values = temp;
            return inverse;
        }

		public Matrix Transpose()
		{
			Matrix transpose = new Matrix(this.Columns,this.Rows);
			for (int i=0; i<this.Rows; i++)
			{
				for (int j=0; j<this.Columns; j++) transpose.Values[j,i] = this.Values[i,j];
			}
			return transpose;
		}
		public Matrix Transpose(Matrix matrix)
		{
			Matrix transpose = new Matrix(matrix.Columns,matrix.Rows);
			for (int i=0; i<matrix.Rows; i++)
			{
				for (int j=0; j<matrix.Columns; j++) transpose.Values[j,i] = matrix.Values[i,j];
			}
			return transpose;
		}

		public double Det()
		{
			double det;
			det = DetMatrix(this.Values, this.rows);
			return det;
		}
		public double Det(Matrix matrix)
		{
			double det;
			det = DetMatrix(matrix.Values, matrix.rows);
			return det;
		}
		public double Det3x3(Matrix matrix)
		{
			double det;
			
			det = matrix.Values[0,0]*matrix.Values[1,1]*matrix.Values[2,2];
			det += matrix.Values[0,1]*matrix.Values[1,2]*matrix.Values[2,0];
			det += matrix.Values[0,2]*matrix.Values[1,0]*matrix.Values[2,1];
			det += -matrix.Values[2,0]*matrix.Values[1,1]*matrix.Values[0,2];
			det += -matrix.Values[2,1]*matrix.Values[1,2]*matrix.Values[0,0];
			det += -matrix.Values[2,2]*matrix.Values[1,0]*matrix.Values[0,1];

			return det;
		}


		public double Trace(Matrix matrix)
		{
			double trace = 0.0D;
			for (int i=0 ; i< matrix.Rows ; i++)
			{
				trace += matrix.Values[i,i];
			}

			return trace;
		}
		public double Trace()
		{
			double trace = 0.0D;
			for (int i=0 ; i< this.Rows ; i++)
			{
				trace += this.Values[i,i];
			}

			return trace;
		}

		public void InitializeToZero()
		{
			for (int i=0 ; i< this.Rows ; i++)
			{
				for (int j=0; j< this.Columns ; j++) this.Values[i,j]=0.0D;
			}
		}
        
        public Vector SolveLinearSystem(Vector RightHandSide)
        {
            Vector Solution = new Vector();
            Solution.Values = SolveLinearSystem(RightHandSide.Values);
            return Solution;
        }
        public Matrix SolveLinearSystem(Matrix RightHandSide)
        {
            int n = RightHandSide.Values.GetLength(0);
            double[] u = new double[n];
            for (int i = 0; i < n; i++)
            {
                u[i] = RightHandSide.Values[i, 0];
            }
            Matrix Solution = new Matrix(SolveLinearSystem(u));
            return Solution;  
        }
        public double[] SolveLinearSystem(double[] RightHandSide)
        {
            int n = RightHandSide.Length;
            int[] index ;
            double d;
            Matrix.ludcmp(this.matrix, n, n, out index,out d);
            Matrix.lubksb(this.matrix, n, n, index, RightHandSide);
            return RightHandSide;
        }
        private int[] index;
        public void SolveLinearSystem_LUDecomp()
        {
            int n = this.matrix.GetLength(0);
            double d;
            Matrix.ludcmp(this.matrix, n, n, out index, out d);
        }
        public void SolveLinearSystem_LUDecomp(out double Det)
        {
            int n = this.matrix.GetLength(0);
            Matrix.ludcmp(this.matrix, n, n, out index, out Det);
            for (int i = 0; i < n; i++)
            {
                Det *= Values[i,i];
            }
        }
        public Vector SolveLinearSystem_BackSub(Vector RightHandSide)
        {
            int n = RightHandSide.Values.Length;
            Matrix.lubksb(this.matrix, n, n, index, RightHandSide.Values);
            return RightHandSide;
        }
        public Vector SolveLinearSystem_LUDecomp_ForAndBackSubstitution(Vector RightHandSide)
        {
            int n = RightHandSide.Values.Length;
            Matrix.lubksb(this.matrix, n, n, index, RightHandSide.Values);
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

		public static void lubksb(double[,] a, int n, int np,  int[] indx,  double[] b)
		{
			int i,j,ll;
			int ii=-1;
			double sum;
			for (i=0 ; i<n ; i++)
			{
				ll = indx[i];
				sum = b[ll];
				b[ll] = b[i];
				if (ii != -1)
				{
					for (j=ii ; j<i ; j++)
					{
						sum += -a[i,j]*b[j];
					}
				}
				else
				{
					if(sum != 0.0d)
					{
						ii=i;
					}
				}
				b[i]=sum;
			}
			for (i=n-1 ; i>-1; i--)
			{
				sum = b[i];
				if (i < n-1)
				{
					for (j = i+1; j<n ; j++)
					{
						sum += -a[i,j]*b[j];
					}
				}
				b[i] = sum/a[i,i];
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

		public static void ludcmp(double[,] a, int n, int np, out int[] indx,  out double d)
		{
			double[] vv = new double[n];
			indx = new int[n];
			double tiny =1.0e-20D;
			d=1.0D;
			int i,j,k,imax;
			double aamax,sum,dum;
			imax = -1;

			for ( i=0 ; i<n ; i++)
			{
				aamax =0.0D;
				for ( j=0 ; j<n ; j++)
				{
					if (System.Math.Abs(a[i,j])>aamax) aamax = System.Math.Abs(a[i,j]);
				}

				if (aamax == 0.0D)Console.Write("Singular Matrix");
			
				vv[i] = 1.0D/aamax;
			}
			for (j=0 ; j<n ; j++)
			{
				if (j > 0)
				{
					for (i =0 ; i<j ; i++)
					{
						sum = a[i,j];
						if (i>0)
						{
							for ( k=0 ; k<i ; k++) sum += -a[i,k]*a[k,j];
							a[i,j] = sum;
						}
					}
				}
				aamax = 0.0D;
				for (i=j ; i<n ; i++)
				{
					sum = a[i,j];
					if (j>0)
					{
						for (k=0 ; k<j ; k++) sum += -a[i,k]*a[k,j];
						a[i,j]=sum;
					}
					dum = vv[i]*System.Math.Abs(sum);
					if (dum >= aamax)
					{
						imax = i;
						aamax = dum;
					}
				}
				if (j != imax)
				{
					for (k=0 ; k<n ; k++)
					{
						dum = a[imax,k];
						a[imax,k] = a[j,k];
						a[j,k] = dum;
					}
					d = -d;
					vv[imax] = vv[j];
				}
				indx[j] = imax;
				if (j != n-1)
				{
					if (a[j,j]==0.0D) a[j,j] = tiny;
					dum = 1.0D/a[j,j];
					for (i=j+1 ; i<n ; i++) a[i,j] = a[i,j]*dum;
				}
			}
			if(a[n-1,n-1] == 0.0D) a[n-1,n-1] = tiny;
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
		public static void InvMatrix(double[,] a, int n, out double[,] ai, out double det)
		{
			/*
			* a[,] square matrix to be inverted
			* n number of rows or columns of a
			* ai[,] inverse of a
			*/
			int i,j;
			double[,] ta = new double[n,n];
			double[] l = new double[n];
			ai = new double[n,n];
			int[] indx; 
			double d;
			// store a copy of a in ta
			for (i=0 ; i<n ; i++)
			{
				for (j=0 ; j<n ; j++)
				{
					ta[i,j] = a[i,j];
				}
			}
			// LU decompose a
			ludcmp(ta,n,n,out indx,out d);
			det = d;
			for (i=0 ; i<n ; i++)
			{
				det=det*ta[i,i];
			}
			for (j=0 ; j<n ; j++)
			{
				for (i=0 ; i<n ; i++)
				{
					if (i == j) l[i] =1.0D;
					else l[i] = 0.0D;
				}
			
				// call back substitution
				lubksb(ta,n,n,indx,l);
				for (i=0 ; i<n ; i++) ai[i,j] =l[i];
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
		public static void InvMatrix(double[,] a, int n, out double[,] ai)
		{
			/*
			* a[,] square matrix to be inverted
			* n number of rows or columns of a
			* ai[,] inverse of a
			*/
			int i,j;
			double[,] ta = new double[n,n];
			double[] l = new double[n];
			ai = new double[n,n];
			int[] indx; 
			double d;
			// store a copy of a in ta
			for (i=0 ; i<n ; i++)
			{
				for (j=0 ; j<n ; j++)
				{
					ta[i,j] = a[i,j];
				}
			}
			// LU decompose a
			ludcmp(ta,n,n,out indx,out d);
			
			for (j=0 ; j<n ; j++)
			{
				for (i=0 ; i<n ; i++)
				{
					if (i == j) l[i] =1.0D;
					else l[i] = 0.0D;
				}
			
				// call back substitution
				lubksb(ta,n,n,indx,l);
				for (i=0 ; i<n ; i++) ai[i,j] =l[i];
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
		public static double DetMatrix(double[,] at, int n)
		{
			/*
			* a[,] square matrix to be inverted
			* n number of rows or columns of a
			* ai[,] inverse of a
			* det is determenant of a[,]
			*/
			int i,j;
			double[,] a = new double[n,n];
			for (i=0 ; i<n ; i++)
			{
				for (j=0 ; j<n ; j++)a[i,j]=at[i,j];
			}
			
			int[] indx; 
			double d,det;
			// LU decompose a
			ludcmp(a,n,n,out indx,out d);
			det = d;
			for (i=0 ; i<n ; i++)
			{
				det=det*a[i,i];
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
					output += "  Row "+i.ToString()+" Column "+j.ToString()+": "+ this.Values[i,j].ToString()+"\n";
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
                int np = n;
                int nrot = 0;
                int p, i, ip, iq;

                double[,] a = new double[n, n];
                double[,] v = new double[n, n];
                double[] d = new double[n];

                for (i = 0; i < n; i++)
                {
                    for (p = 0; p < n; p++)
                    {
                        a[i, p] = this.Values[i, p];
                    }
                }

                ////////////////////////////////////////////////////////////////

                int nmax = 500;
                double c, g, h, s, sm, t, tau, theta, tresh;
                double[] b = new double[nmax];
                double[] z = new double[nmax];

                for (p = 0; p < n; p++)
                {
                    v[p, p] = 1.0d;
                }

                for (p = 0; p < n; p++)
                {
                    b[p] = a[p, p];
                    d[p] = b[p];
                    z[p] = 0.0d;
                }


                nrot = 0;

                for (i = 0; i < 50; i++)
                {

                    sm = 0.0d;
                    for (ip = 1; ip < n; ip++)
                    {
                        for (iq = ip + 1; iq < n + 1; iq++)
                        {
                            sm = sm + Math.Abs(a[ip - 1, iq - 1]);
                        }
                    }


                    ///////////////////////////////////////////////////

                    if (sm == 0.0d) break;


                    if (i < 3)
                    {
                        tresh = 0.2d * sm / (n * n);
                    }
                    else
                    {
                        tresh = 0.0d;
                    }
                    /////////////////////////////////////////////////

                    for (ip = 1; ip < n; ip++)
                    {
                        for (iq = ip + 1; iq < n + 1; iq++)
                        {
                            g = 100.0d * Math.Abs(a[ip - 1, iq - 1]);
                            if ((i > 3) && (Math.Abs(d[ip - 1]) + g == Math.Abs(d[ip - 1])) && (Math.Abs(d[iq - 1]) + g == Math.Abs(d[iq - 1])))
                            {
                                a[ip - 1, iq - 1] = 0.0d;
                            }
                            else if (Math.Abs(a[ip - 1, iq - 1]) > tresh)
                            {
                                h = d[iq - 1] - d[ip - 1];
                                if (Math.Abs(h) + g == Math.Abs(h))
                                {
                                    t = a[ip - 1, iq - 1] / h;
                                }
                                else
                                {
                                    theta = 0.5d * h / a[ip - 1, iq - 1];
                                    t = 1.0d / (Math.Abs(theta) + Math.Sqrt(1.0d + theta * theta));
                                    if (theta < 0.0d) t = -t;
                                }
                                c = 1.0d / Math.Sqrt(1.0d + t * t);
                                s = t * c;
                                tau = s / (1.0d + c);
                                h = t * a[ip - 1, iq - 1];
                                z[ip - 1] = z[ip - 1] - h;
                                z[iq - 1] = z[iq - 1] + h;
                                d[ip - 1] = d[ip - 1] - h;
                                d[iq - 1] = d[iq - 1] + h;
                                a[ip - 1, iq - 1] = 0.0d;


                                for (int j = 1; j < ip; j++)
                                {
                                    g = a[j - 1, ip - 1];
                                    h = a[j - 1, iq - 1];
                                    a[j - 1, ip - 1] = g - s * (h + g * tau);
                                    a[j - 1, iq - 1] = h + s * (g - h * tau);
                                }
                                for (int j = ip + 1; j < iq; j++)
                                {
                                    g = a[ip - 1, j - 1];
                                    h = a[j - 1, iq - 1];
                                    a[ip - 1, j - 1] = g - s * (h + g * tau);
                                    a[j - 1, iq - 1] = h + s * (g - h * tau);
                                }
                                for (int j = iq + 1; j < n + 1; j++)
                                {
                                    g = a[ip - 1, j - 1];
                                    h = a[iq - 1, j - 1];
                                    a[ip - 1, j - 1] = g - s * (h + g * tau);
                                    a[iq - 1, j - 1] = h + s * (g - h * tau);
                                }
                                for (int j = 1; j < n + 1; j++)
                                {
                                    g = v[j - 1, ip - 1];
                                    h = v[j - 1, iq - 1];
                                    v[j - 1, ip - 1] = g - s * (h + g * tau);
                                    v[j - 1, iq - 1] = h + s * (g - h * tau);
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
            Matrix A = this;
            Matrix V;
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
        public Vector SolveUsingSingularValueDecomposition(Vector RHS, out double ConditionNumber, out Vector W, out Matrix V)//, out double rank, out double nullity)
        {
            Matrix A = this;
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
        public void SingularValueDecomposition(ref Matrix A, out Vector W, out Matrix V)
        {
            //Adaptation of singular value decomposition from Numerical Recipes
            // A = U W V^T
            // Replaces A with U
            int m = A.Rows;
            int n = A.Columns;
            double[,] u = A.Values;
            V = new Matrix(n, n);
            double[,] v = V.Values;
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
                    for (k = i; k < m; k++) scale += Math.Abs(u[k,i]);
                    if (scale != 0.0)
                    {
                        for (k = i; k < m; k++)
                        {
                            u[k,i] /= scale;
                            s += u[k,i] * u[k,i];
                        }
                        f = u[i,i];
                        g = -FortranSign(Math.Sqrt(s), f);
                        h = f * g - s;
                        u[i,i] = f - g;
                        for (j = l - 1; j < n; j++)
                        {
                            for (s = 0.0, k = i; k < m; k++) s += u[k,i] * u[k,j];
                            f = s / h;
                            for (k = i; k < m; k++) u[k,j] += f * u[k,i];
                        }
                        for (k = i; k < m; k++) u[k,i] *= scale;
                    }
                }
                w[i] = scale * g;
                g = s = scale = 0.0;
                if (i + 1 <= m && i + 1 != n)
                {
                    for (k = l - 1; k < n; k++) scale += Math.Abs(u[i,k]);
                    if (scale != 0.0)
                    {
                        for (k = l - 1; k < n; k++)
                        {
                            u[i,k] /= scale;
                            s += u[i,k] * u[i,k];
                        }
                        f = u[i,l - 1];
                        g = -FortranSign(Math.Sqrt(s), f);
                        h = f * g - s;
                        u[i,l - 1] = f - g;
                        for (k = l - 1; k < n; k++) rv1[k] = u[i,k] / h;
                        for (j = l - 1; j < m; j++)
                        {
                            for (s = 0.0, k = l - 1; k < n; k++) s += u[j,k] * u[i,k];
                            for (k = l - 1; k < n; k++) u[j,k] += s * rv1[k];
                        }
                        for (k = l - 1; k < n; k++) u[i,k] *= scale;
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
                            v[j,i] = (u[i,j] / u[i,l]) / g;
                        for (j = l; j < n; j++)
                        {
                            for (s = 0.0, k = l; k < n; k++) s += u[i,k] * v[k,j];
                            for (k = l; k < n; k++) v[k,j] += s * v[k,i];
                        }
                    }
                    for (j = l; j < n; j++) v[i,j] = v[j,i] = 0.0;
                }
                v[i,i] = 1.0;
                g = rv1[i];
                l = i;
            }
            for (i = Math.Min(m, n) - 1; i >= 0; i--)
            { //Accumulation of left-hand transformations.
                l = i + 1;
                g = w[i];
                for (j = l; j < n; j++) u[i,j] = 0.0;
                if (g != 0.0)
                {
                    g = 1.0 / g;
                    for (j = l; j < n; j++)
                    {
                        for (s = 0.0, k = l; k < m; k++) s += u[k,i] * u[k,j];
                        f = (s / u[i,i]) * g;
                        for (k = i; k < m; k++) u[k,j] += f * u[k,i];
                    }
                    for (j = i; j < m; j++) u[j,i] *= g;
                }
                else for (j = i; j < m; j++) u[j,i] = 0.0;
                ++u[i,i];
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
                                y = u[j,nm];
                                z = u[j,i];
                                u[j,nm] = y * c + z * s;
                                u[j,i] = z * c - y * s;
                            }
                        }
                    }
                    z = w[k];
                    if (l == k)
                    { //Convergence.
                        if (z < 0.0)
                        { //Singular value is made nonnegative.
                            w[k] = -z;
                            for (j = 0; j < n; j++) v[j,k] = -v[j,k];
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
                            x = v[jj,j];
                            z = v[jj,i];
                            v[jj,j] = x * c + z * s;
                            v[jj,i] = z * c - x * s;
                        }
                        z = PYTHAG(f, h);
                        w[j] = z; //Rotation can be arbitrary if z D 0.
                        if (z != 0.0D)
                        {
                            z = 1.0 / z;
                            c = f * z;
                            s = h * z;
                        }
                        f = c * g + s * y;
                        x = c * y - s * g;
                        for (jj = 0; jj < m; jj++)
                        {
                            y = u[jj,j];
                            z = u[jj,i];
                            u[jj,j] = y * c + z * s;
                            u[jj,i] = z * c - y * s;
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
	public class IntMatrix
	{
		private int rows;
		private int columns;
		private int[,] matrix;

		public int Rows
		{
			get
			{
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
				return columns;
			}
			set
			{
				columns = value;
			}
			
		}
		public int[,] Values
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

		public IntMatrix(int NumberOfRows, int NumberOfColumns)
		{
			this.Rows = NumberOfRows;
			this.Columns = NumberOfColumns;
			this.matrix = new int[rows,columns];
		}
	}
}
