using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OOPTools_Math
{
    /// <summary>
    /// Root class for square matrix type objects providing basic interfaces to
    /// 
    ///  * Construct the matrix
    ///  * Add to the matrix
    ///  * Solve a linear system
    ///  * Do matrix multiplication
    ///  * Calculate Eigenvalues
    /// 
    /// Mehrdad Negahban
    /// 07-12-2009
    ///
    /// </summary>
    [Serializable]
    public class MatrixObject
    {
        public int n; // Number of equations (rows)
        public virtual void AddToMatrixElement(int row, int col, double value)
        {
        }
        public virtual void SetMatrixElement(int row, int col, double value)
        {
        }
        
        public virtual Vector SolveLinearSystem(Vector RightHandSide)
        {
            Vector Solution = new Vector(0);
            Solution.Values = SolveLinearSystem(RightHandSide.Values);
            Solution.Rows = Solution.Values.Length;
            return Solution;
        }
        public virtual double[] SolveLinearSystem(double[] RightHandSide)
        {
            double[] Solution = new double[0];
            return Solution;
        }
        public virtual double[] SolveLinearSystem_Parallel(double[] RightHandSide)
        {
            double[] Solution = new double[0];
            return Solution;
        }

        public virtual Vector MultiplyByVectorU(Vector u)
        {
            Vector Solution = new Vector(0);
            return Solution;
        }

        public virtual Vector GetEigenVectorForLargestEigenValue()
        {
            Vector InitialValue= new Vector(n);
            InitialValue.SetComponentsEqualToUnity();
            return GetEigenVectorForLargestEigenValue(InitialValue);
        }
        public virtual Vector GetEigenVectorForLargestEigenValue(Vector InitialValue)
        {
            Matrix_EigenValueEigenVector_Solver Eigen = new Matrix_EigenValueEigenVector_Solver();
            Vector EigenVector;
            double EigenValue;
            Eigen.CalculateFirstEigenValueAndEigenVector(this, out EigenValue, out EigenVector);
            return EigenVector;
        }
    }
}
