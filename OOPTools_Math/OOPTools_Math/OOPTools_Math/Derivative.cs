using System;

namespace OOPTools_Math
{
	/// <summary>
	/// Methods to evaluate derivatives
	/// Mehrdad Negahban
	/// 7/16/05
	/// </summary>
    [Serializable]
	public class Derivative
	{
		public delegate double Function(double x);
		public delegate double FunctionWithArguments(double x, object Arguments);
		public delegate double FunctionWithArrayOfVariables(double[] x);
		public delegate double FunctionWithArrayOfVariablesWithArguments(double[] x, object Arguments);
		public delegate double[] ArrayFunctionWithArrayOfVariables(double[] x);
		public delegate double[] ArrayFunctionWithArrayOfVariablesWithArguments(double[] x, object Arguments);
		public delegate double[,] TwoDArrayFunctionWithTwoDArrayOfVariables(double[,] x);
		public delegate double[,] TwoDArrayFunctionWithTwoDArrayOfVariablesWithArguments(double[,] x, object Arguments);
		public delegate Matrix MatrixFunction(double x);
		public delegate Matrix MatrixFunctionWithArrayArgument(double[] x);
        public delegate Matrix MatrixFunctionWithVectorArgument(double[] x);
		public delegate Matrix MatrixFunctionWithMatrixArgument(Matrix x);
        public delegate Vector VectorFunction(double x);
        public delegate Vector VectorFunctionWithArrayArgument(double[] x);
        public delegate Vector VectorFunctionWithVectorArgument(double[] x);
        public delegate Vector VectorFunctionWithMatrixArgument(Matrix x);

		public Derivative()
		{
		}
		public double DfDx(Function Func, double x)
		{
			///<sumary>
			/// retruns of the function
			/// </summary>
			return this.DfDx(Func,x,1.0E-2D,1.0E-14D);
		}
		public double DfDx(Function Func, double x, double InitialStepSize, double Precision)
		{
			double precision = Precision; // precision to evaluate derivative
			double h = InitialStepSize;	// initial step size
			double c = 10.0D; // factor do divide h (i.e., h_new =h_old/c)
			double eps = 1.0E-200D; // small number 
			int maxSteps = 100; // maximum times to divid h
			double devOld, devNew; // old derivative
			double calcPrecision; // new derivative
			// calculate initial derivative
			devOld = this.DfDx(Func,x,h);
			devNew = devOld;

			for(int i=0; i<maxSteps; i++)
			{
				// reduce h
				h/=c;
				// calculate new derivative
				devNew =this.DfDx(Func,x,h);
				//calculate precision
				if(Math.Abs(devNew)>eps)
				{
					calcPrecision =Math.Abs((devNew-devOld)/devNew);
				}
				else
				{
					// don't divide by devNew if devNew is small
					calcPrecision =Math.Abs(devNew-devOld);
				}
				// stop if precision is met
				if(calcPrecision <precision) 
				{
					return devNew;
				}
				devOld=devNew;
			}
			return devNew;
		}
		public double DfDx(Function Func, double x, double h)
		{
			double XPrev = x - h;
			double XNext = x + h;
			double FuncPrev = Func(XPrev);
			double FuncNext = Func(XNext);
			
			return (FuncNext-FuncPrev)/(2.0D*h);
		}
		public double[] DfDx(FunctionWithArrayOfVariables Func, double[] x)
		{
			///<sumary>
			/// retruns an array with all derivatives of the function
			/// </summary>
			int n = x.Length;
			double[] D = new double[n];
			for(int i=0; i<n; i++)
			{
				D[i] = DfDx(Func,x,i,1.0E-2,1.0E-14);
			}
			return D;
		}
		public double DfDx(FunctionWithArrayOfVariables Func, double[] x, int xIndex)
		{
			return DfDx(Func,x,xIndex,1.0E-2D,1.0E-14D);
		}
		
		public double DfDx(FunctionWithArrayOfVariables Func, double[] x, int xIndex, double InitialStepSize, double Precision)
		{
			double precision = Precision; // precision to evaluate derivative
			double h = InitialStepSize;	// initial step size
			double c = 10.0D; // factor do divide h (i.e., h_new =h_old/c)
			double eps = 1.0E-100D; // small number 
			int maxSteps = 100; // maximum number of times to divid h
			double devOld, devNew; // old derivative
			double calcPrecision; // new derivative
			// calculate initial derivative
			devOld = this.DfDx(Func,x,xIndex,h);
			devNew = devOld;

			for(int i=0; i<maxSteps; i++)
			{
				// reduce h
				h/=c;
				// calculate new derivative
				devNew =this.DfDx(Func,x,xIndex,h);
				//calculate precision
				if(Math.Abs(devNew)>eps)
				{
					calcPrecision =Math.Abs((devNew-devOld)/devNew);
				}
				else
				{
					// don't divide by devNew if devNew is small
					calcPrecision =Math.Abs(devNew-devOld);
				}
				// stop if precision is met
				if(calcPrecision <precision) 
				{
					//Console.Write("number of steps = "+i.ToString()+"\n");
					//Console.Write("Calculated precision = "+calcPrecision.ToString()+"\n");
					return devNew;
				}
				devOld=devNew;
			}
			return devNew;
		}
		public double DfDx(FunctionWithArrayOfVariables Func, double[] x, int xIndex, double h)
		{
			int n = x.Length;
			double[] XPrev = new double[n];
			double[] XNext = new double[n];
			double Prev, Next;
			for(int i=0; i<n; i++)
			{
				XPrev[i] = x[i];
				XNext[i] = x[i];
				if(i==xIndex)
				{
					XPrev[i] -= h;
					XNext[i] += h;
				}
			}
			Prev = Func(XPrev);
			Next = Func(XNext);
			return (Next-Prev)/(2.0D*h);
		}
		public double[,] DfDx(ArrayFunctionWithArrayOfVariables Func, double[] x)
		{
			///<sumary>
			/// retruns an array with all derivatives of the function
			/// </summary>
			int n = x.Length;
			int m = Func(x).Length;
			double[,] D = new double[m,n];
			for(int i=0; i<m; i++)
			{
				for(int j=0; j<n; j++)
				{
					D[i,j] = DfDx(Func,x,i,j,1.0E-2D,1.0E-14D);
				}
			}
			return D;
		}
		public double DfDx(ArrayFunctionWithArrayOfVariables Func, double[] x,int FuncIndex, int xIndex)
		{
			return DfDx(Func,x,FuncIndex,xIndex,1.0E-2D,1.0E-14D);
		}
		public double DfDx(ArrayFunctionWithArrayOfVariables Func, double[] x, int FuncIndex, int xIndex, double InitialStepSize, double Precision)
		{
			double precision = Precision; // precision to evaluate derivative
			double h = InitialStepSize;	// initial step size
			double c = 10.0D; // factor do divide h (i.e., h_new =h_old/c)
			double eps = 1.0E-100D; // small number 
			int maxSteps = 100; // maximum times to divid h
			double devOld, devNew; // old derivative
			double calcPrecision; // new derivative
			// calculate initial derivative
			devOld = this.DfDx(Func,x,FuncIndex,xIndex,h);
			devNew = devOld;

			for(int i=0; i<maxSteps; i++)
			{
				// reduce h
				h/=c;
				// calculate new derivative
				devNew =this.DfDx(Func,x,FuncIndex,xIndex,h);
				//calculate precision
				if(Math.Abs(devNew)>eps)
				{
					calcPrecision =Math.Abs((devNew-devOld)/devNew);
				}
				else
				{
					// don't divide by devNew if devNew is small
					calcPrecision =Math.Abs(devNew-devOld);
				}
				// stop if precision is met
				if(calcPrecision <precision) 
				{
					//Console.Write("number of steps = "+i.ToString()+"\n");
					//Console.Write("Calculated precision = "+calcPrecision.ToString()+"\n");
					return devNew;
				}
				devOld=devNew;
			}
			return devNew;
		}
		public double DfDx(ArrayFunctionWithArrayOfVariables Func, double[] x, int FuncIndex, int xIndex, double h)
		{
			int n = x.Length;
			double[] XPrev = new double[n];
			double[] XNext = new double[n];
			double Prev, Next;
			for(int i=0; i<n; i++)
			{
				XPrev[i] = x[i];
				XNext[i] = x[i];
				if(i==xIndex)
				{
					XPrev[i] -= h;
					XNext[i] += h;
				}
			}
			Prev = Func(XPrev)[FuncIndex];
			Next = Func(XNext)[FuncIndex];
			return (Next-Prev)/(2.0D*h);
		}
		public double[,,,] DfDx(TwoDArrayFunctionWithTwoDArrayOfVariables Func, double[,] x)
		{
			///<sumary>
			/// retruns an array with all derivatives of the function
			/// </summary>
			int n1 = x.GetLength(0);
			int n2 = x.GetLength(1);
			int m1 = Func(x).GetLength(0);
			int m2 = Func(x).GetLength(1);
			double[,,,] D = new double[m1,m2,n1,n2];
			for(int i=0; i<m1; i++)
			{
				for(int j=0; j<m2; j++)
				{
					for(int k=0; k<n1; k++)
					{
						for(int l=0; l<n2; l++)
						{
							D[i,j,k,l] = DfDx(Func,x,i,j,k,l);
						}
					}
				}
			}
			return D;
		}
		public double DfDx(TwoDArrayFunctionWithTwoDArrayOfVariables Func, double[,] x,int FuncIndex1, int FuncIndex2, int xIndex1, int xIndex2)
		{
			return DfDx(Func,x,FuncIndex1,FuncIndex2,xIndex1,xIndex2,1.0E-2D,1.0E-14D);
		}
		public double DfDx(TwoDArrayFunctionWithTwoDArrayOfVariables Func, double[,] x,int FuncIndex1, int FuncIndex2, int xIndex1, int xIndex2, double InitialStepSize, double Precision)
		{
			double precision = Precision; // precision to evaluate derivative
			double h = InitialStepSize;	// initial step size
			double c = 10.0D; // factor do divide h (i.e., h_new =h_old/c)
			double eps = 1.0E-100D; // small number 
			int maxSteps = 100; // maximum times to divid h
			double devOld, devNew; // old derivative
			double calcPrecision; // new derivative
			// calculate initial derivative
			devOld = this.DfDx(Func,x,FuncIndex1,FuncIndex2,xIndex1,xIndex2,h);
			devNew = devOld;

			for(int i=0; i<maxSteps; i++)
			{
				// reduce h
				h/=c;
				// calculate new derivative
				devNew =this.DfDx(Func,x,FuncIndex1,FuncIndex2,xIndex1,xIndex2,h);
				//calculate precision
				if(Math.Abs(devNew)>eps)
				{
					calcPrecision =Math.Abs((devNew-devOld)/devNew);
				}
				else
				{
					// don't divide by devNew if devNew is small
					calcPrecision =Math.Abs(devNew-devOld);
				}
				// stop if precision is met
				if(calcPrecision <precision) 
				{
					//Console.Write("number of steps = "+i.ToString()+"\n");
					//Console.Write("Calculated precision = "+calcPrecision.ToString()+"\n");
					return devNew;
				}
				devOld=devNew;
			}
			return devNew;
		}
		public double DfDx(TwoDArrayFunctionWithTwoDArrayOfVariables Func, double[,] x,int FuncIndex1, int FuncIndex2, int xIndex1, int xIndex2,  double h)
		{
			int n1 = x.GetLength(0);
			int n2 = x.GetLength(1);
			double[,] XPrev = new double[n1,n2];
			double[,] XNext = new double[n1,n2];
			double Prev, Next;
			for(int i=0; i<n1; i++)
			{
				
				for(int j=0; j<n2; j++)
				{
				XPrev[i,j] = x[i,j];
				XNext[i,j] = x[i,j];
					if(i==xIndex1)
					{
						if(j==xIndex2)
						{
							XPrev[i,j] -= h;
							XNext[i,j] += h;
						}
					}
				}
			}
			Prev = Func(XPrev)[FuncIndex1,FuncIndex2];
			Next = Func(XNext)[FuncIndex1,FuncIndex2];
			return (Next-Prev)/(2.0D*h);
		}
		public double DfDx(FunctionWithArguments Func, object Arguments, double x, double InitialStepSize, double Precision)
		{
			double precision = Precision; // precision to evaluate derivative
			double h = InitialStepSize;	// initial step size
			double c = 10.0D; // factor do divide h (i.e., h_new =h_old/c)
			double eps = 1.0E-100D; // small number 
			int maxSteps = 100; // maximum times to divid h
			double devOld, devNew; // old derivative
			double calcPrecision; // new derivative
			// calculate initial derivative
			devOld = this.DfDx(Func,Arguments,x,h);
			devNew = devOld;

			for(int i=0; i<maxSteps; i++)
			{
				// reduce h
				h/=c;
				// calculate new derivative
				devNew =this.DfDx(Func,Arguments,x,h);
				//calculate precision
				if(Math.Abs(devNew)>eps)
				{
					calcPrecision =Math.Abs((devNew-devOld)/devNew);
				}
				else
				{
					// don't divide by devNew if devNew is small
					calcPrecision =Math.Abs(devNew-devOld);
				}
				// stop if precision is met
				if(calcPrecision <precision) 
				{
					//Console.Write("number of steps = "+i.ToString()+"\n");
					//Console.Write("Calculated precision = "+calcPrecision.ToString()+"\n");
					return devNew;
				}
				devOld=devNew;
			}
			return devNew;
		}
		public double DfDx(FunctionWithArguments Func, object Arguments, double x, double h)
		{
			double XPrev = x - h;
			double XNext = x + h;
			double FuncPrev = Func(XPrev,Arguments);
			double FuncNext = Func(XNext,Arguments);
			
			return (FuncNext-FuncPrev)/(2.0D*h);
		}
		public double[] DfDx(FunctionWithArrayOfVariablesWithArguments Func, object Arguments,  double[] x)
		{
			///<sumary>
			/// retruns an array with all derivatives of the function
			/// </summary>
			int n = x.Length;
			double[] D = new double[n];
			for(int i=0; i<n; i++)
			{
				D[i] = DfDx(Func,Arguments,x,i,1.0E-2D,1.0E-14D);
			}
			return D;
		}
		public double DfDx(FunctionWithArrayOfVariablesWithArguments Func, object Arguments,  double[] x, int xIndex)
		{
			return DfDx(Func,Arguments,x,xIndex,1.0E-2,1.0E-14);
		}
		
		public double DfDx(FunctionWithArrayOfVariablesWithArguments Func, object Arguments, double[] x, int xIndex, double InitialStepSize, double Precision)
		{
			double precision = Precision; // precision to evaluate derivative
			double h = InitialStepSize;	// initial step size
			double c = 10.0D; // factor do divide h (i.e., h_new =h_old/c)
			double eps = 1.0E-100D; // small number 
			int maxSteps = 100; // maximum times to divid h
			double devOld, devNew; // old derivative
			double calcPrecision; // new derivative
			// calculate initial derivative
			devOld = this.DfDx(Func,Arguments,x,xIndex,h);
			devNew = devOld;

			for(int i=0; i<maxSteps; i++)
			{
				// reduce h
				h/=c;
				// calculate new derivative
				devNew =this.DfDx(Func,Arguments,x,xIndex,h);
				//calculate precision
				if(Math.Abs(devNew)>eps)
				{
					calcPrecision =Math.Abs((devNew-devOld)/devNew);
				}
				else
				{
					// don't divide by devNew if devNew is small
					calcPrecision =Math.Abs(devNew-devOld);
				}
				// stop if precision is met
				if(calcPrecision <precision) 
				{
					//Console.Write("number of steps = "+i.ToString()+"\n");
					//Console.Write("Calculated precision = "+calcPrecision.ToString()+"\n");
					return devNew;
				}
				devOld=devNew;
			}
			return devNew;
		}
		public double DfDx(FunctionWithArrayOfVariablesWithArguments Func, object Arguments, double[] x, int xIndex, double h)
		{
			int n = x.Length;
			double[] XPrev = new double[n];
			double[] XNext = new double[n];
			double Prev, Next;
			for(int i=0; i<n; i++)
			{
				XPrev[i] = x[i];
				XNext[i] = x[i];
				if(i==xIndex)
				{
					XPrev[i] -= h;
					XNext[i] += h;
				}
			}
			Prev = Func(XPrev,Arguments);
			Next = Func(XNext,Arguments);
			return (Next-Prev)/(2.0D*h);
		}
		public double[,] DfDx(ArrayFunctionWithArrayOfVariablesWithArguments Func, object Arguments, double[] x)
		{
			///<sumary>
			/// retruns an array with all derivatives of the function
			/// </summary>
			int n = x.Length;
			int m = Func(x,Arguments).Length;
			double[,] D = new double[m,n];
			for(int i=0; i<m; i++)
			{
				for(int j=0; j<n; j++)
				{
					D[i,j] = DfDx(Func,Arguments,x,i,j,1.0E-2D,1.0E-14D);
				}
			}
			return D;
		}
		public double DfDx(ArrayFunctionWithArrayOfVariablesWithArguments Func, object Arguments, double[] x,int FuncIndex, int xIndex)
		{
			return DfDx(Func,Arguments,x,FuncIndex,xIndex,1.0E-2D,1.0E-14D);
		}
		public double DfDx(ArrayFunctionWithArrayOfVariablesWithArguments Func, object Arguments, double[] x, int FuncIndex, int xIndex, double InitialStepSize, double Precision)
		{
			double precision = Precision; // precision to evaluate derivative
			double h = InitialStepSize;	// initial step size
			double c = 10.0D; // factor do divide h (i.e., h_new =h_old/c)
			double eps = 1.0E-100D; // small number 
			int maxSteps = 100; // maximum times to divid h
			double devOld, devNew; // old derivative
			double calcPrecision; // new derivative
			// calculate initial derivative
			devOld = this.DfDx(Func,Arguments,x,FuncIndex,xIndex,h);
			devNew = devOld;

			for(int i=0; i<maxSteps; i++)
			{
				// reduce h
				h/=c;
				// calculate new derivative
				devNew =this.DfDx(Func,Arguments,x,FuncIndex,xIndex,h);
				//calculate precision
				if(Math.Abs(devNew)>eps)
				{
					calcPrecision =Math.Abs((devNew-devOld)/devNew);
				}
				else
				{
					// don't divide by devNew if devNew is small
					calcPrecision =Math.Abs(devNew-devOld);
				}
				// stop if precision is met
				if(calcPrecision <precision) 
				{
					//Console.Write("number of steps = "+i.ToString()+"\n");
					//Console.Write("Calculated precision = "+calcPrecision.ToString()+"\n");
					return devNew;
				}
				devOld=devNew;
			}
			return devNew;
		}
		public double DfDx(ArrayFunctionWithArrayOfVariablesWithArguments Func, object Arguments, double[] x, int FuncIndex, int xIndex, double h)
		{
			int n = x.Length;
			double[] XPrev = new double[n];
			double[] XNext = new double[n];
			double Prev, Next;
			for(int i=0; i<n; i++)
			{
				XPrev[i] = x[i];
				XNext[i] = x[i];
				if(i==xIndex)
				{
					XPrev[i] -= h;
					XNext[i] += h;
				}
			}
			Prev = Func(XPrev,Arguments)[FuncIndex];
			Next = Func(XNext,Arguments)[FuncIndex];
			return (Next-Prev)/(2.0D*h);
		}
		public double[,,,] DfDx(TwoDArrayFunctionWithTwoDArrayOfVariablesWithArguments Func, object Arguments, double[,] x)
		{
			///<sumary>
			/// retruns an array with all derivatives of the function
			/// </summary>
			int n1 = x.GetLength(0);
			int n2 = x.GetLength(1);
			int m1 = Func(x,Arguments).GetLength(0);
			int m2 = Func(x,Arguments).GetLength(1);
			double[,,,] D = new double[m1,m2,n1,n2];
			for(int i=0; i<m1; i++)
			{
				for(int j=0; j<m2; j++)
				{
					for(int k=0; k<n1; k++)
					{
						for(int l=0; l<n2; l++)
						{
							D[i,j,k,l] = DfDx(Func,Arguments,x,i,j,k,l);
						}
					}
				}
			}
			return D;
		}
		public double DfDx(TwoDArrayFunctionWithTwoDArrayOfVariablesWithArguments Func, object Arguments, double[,] x,int FuncIndex1, int FuncIndex2, int xIndex1, int xIndex2)
		{
			return DfDx(Func,Arguments,x,FuncIndex1,FuncIndex2,xIndex1,xIndex2,1.0E-2D,1.0E-14D);
		}
		public double DfDx(TwoDArrayFunctionWithTwoDArrayOfVariablesWithArguments Func, object Arguments, double[,] x,int FuncIndex1, int FuncIndex2, int xIndex1, int xIndex2, double InitialStepSize, double Precision)
		{
			double precision = Precision; // precision to evaluate derivative
			double h = InitialStepSize;	// initial step size
			double c = 10.0D; // factor do divide h (i.e., h_new =h_old/c)
			double eps = 1.0E-100D; // small number 
			int maxSteps = 100; // maximum times to divid h
			double devOld, devNew; // old derivative
			double calcPrecision; // new derivative
			// calculate initial derivative
			devOld = this.DfDx(Func,Arguments,x,FuncIndex1,FuncIndex2,xIndex1,xIndex2,h);
			devNew = devOld;

			for(int i=0; i<maxSteps; i++)
			{
				// reduce h
				h/=c;
				// calculate new derivative
				devNew =this.DfDx(Func,Arguments,x,FuncIndex1,FuncIndex2,xIndex1,xIndex2,h);
				//calculate precision
				if(Math.Abs(devNew)>eps)
				{
					calcPrecision =Math.Abs((devNew-devOld)/devNew);
				}
				else
				{
					// don't divide by devNew if devNew is small
					calcPrecision =Math.Abs(devNew-devOld);
				}
				// stop if precision is met
				if(calcPrecision <precision) 
				{
					//Console.Write("number of steps = "+i.ToString()+"\n");
					//Console.Write("Calculated precision = "+calcPrecision.ToString()+"\n");
					return devNew;
				}
				devOld=devNew;
			}
			return devNew;
		}
		public double DfDx(TwoDArrayFunctionWithTwoDArrayOfVariablesWithArguments Func, object Arguments, double[,] x,int FuncIndex1, int FuncIndex2, int xIndex1, int xIndex2,  double h)
		{
			int n1 = x.GetLength(0);
			int n2 = x.GetLength(1);
			double[,] XPrev = new double[n1,n2];
			double[,] XNext = new double[n1,n2];
			double Prev, Next;
			for(int i=0; i<n1; i++)
			{
				
				for(int j=0; j<n2; j++)
				{
					XPrev[i,j] = x[i,j];
					XNext[i,j] = x[i,j];
					if(i==xIndex1)
					{
						if(j==xIndex2)
						{
							XPrev[i,j] -= h;
							XNext[i,j] += h;
						}
					}
				}
			}
			Prev = Func(XPrev,Arguments)[FuncIndex1,FuncIndex2];
			Next = Func(XNext,Arguments)[FuncIndex1,FuncIndex2];
			return (Next-Prev)/(2.0D*h);
		}
        public double[, , ,] DfDx(MatrixFunctionWithMatrixArgument Func, Matrix x)
        {
            ///<sumary>
            /// retruns an array with all derivatives of the function
            /// </summary>
            int n1 = x.Values.GetLength(0);
            int n2 = x.Values.GetLength(1);
            int m1 = Func(x).Values.GetLength(0);
            int m2 = Func(x).Values.GetLength(1);
            double[, , ,] D = new double[m1, m2, n1, n2];
            for (int i = 0; i < m1; i++)
            {
                for (int j = 0; j < m2; j++)
                {
                    for (int k = 0; k < n1; k++)
                    {
                        for (int l = 0; l < n2; l++)
                        {
                            D[i, j, k, l] = DfDx(Func, x, i, j, k, l);
                        }
                    }
                }
            }
            return D;
        }
        public double DfDx(MatrixFunctionWithMatrixArgument Func, Matrix x, int FuncIndex1, int FuncIndex2, int xIndex1, int xIndex2)
        {
            return DfDx(Func, x, FuncIndex1, FuncIndex2, xIndex1, xIndex2, 1.0E-2D, 1.0E-14D);
        }
        public double DfDx(MatrixFunctionWithMatrixArgument Func, Matrix x, int FuncIndex1, int FuncIndex2, int xIndex1, int xIndex2, double InitialStepSize, double Precision)
        {
            double precision = Precision; // precision to evaluate derivative
            double h = InitialStepSize;	// initial step size
            double c = 10.0D; // factor do divide h (i.e., h_new =h_old/c)
            double eps = 1.0E-100D; // small number 
            int maxSteps = 100; // maximum times to divid h
            double devOld, devNew; // old derivative
            double calcPrecision; // new derivative
            // calculate initial derivative
            devOld = this.DfDx(Func, x, FuncIndex1, FuncIndex2, xIndex1, xIndex2, h);
            devNew = devOld;

            for (int i = 0; i < maxSteps; i++)
            {
                // reduce h
                h /= c;
                // calculate new derivative
                devNew = this.DfDx(Func, x, FuncIndex1, FuncIndex2, xIndex1, xIndex2, h);
                //calculate precision
                if (Math.Abs(devNew) > eps)
                {
                    calcPrecision = Math.Abs((devNew - devOld) / devNew);
                }
                else
                {
                    // don't divide by devNew if devNew is small
                    calcPrecision = Math.Abs(devNew - devOld);
                }
                // stop if precision is met
                if (calcPrecision < precision)
                {
                    //Console.Write("number of steps = "+i.ToString()+"\n");
                    //Console.Write("Calculated precision = "+calcPrecision.ToString()+"\n");
                    return devNew;
                }
                devOld = devNew;
            }
            return devNew;
        }
        public double DfDx(MatrixFunctionWithMatrixArgument Func, Matrix x, int FuncIndex1, int FuncIndex2, int xIndex1, int xIndex2, double h)
        {
            int n1 = x.Values.GetLength(0);
            int n2 = x.Values.GetLength(1);
            Matrix XPrev = new Matrix(n1, n2);
            Matrix XNext = new Matrix(n1, n2);
            double Prev, Next;
            for (int i = 0; i < n1; i++)
            {

                for (int j = 0; j < n2; j++)
                {
                    XPrev.Values[i, j] = x.Values[i, j];
                    XNext.Values[i, j] = x.Values[i, j];
                    if (i == xIndex1)
                    {
                        if (j == xIndex2)
                        {
                            XPrev.Values[i, j] -= h;
                            XNext.Values[i, j] += h;
                        }
                    }
                }
            }
            Prev = Func(XPrev).Values[FuncIndex1, FuncIndex2];
            Next = Func(XNext).Values[FuncIndex1, FuncIndex2];
            return (Next - Prev) / (2.0D * h);
        }
	}
}
