using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OOPTools_Math
{
    /// <summary>
    /// Calculate eigenvalue and eigenvector for a square matrix using the power method
    /// 
    /// Mehrdad Negahban
    /// 07-12-2009
    /// 
    /// Only calculates the largest eigenvector and eigenvalue
    ///
    /// </summary>
    [Serializable]
    public class Matrix_EigenValueEigenVector_Solver
    {
        public double Precision; // How close to zero should the change in eigenvalue be
        public int NumberOfEigenValueChangesChecked; // How many eigenvalue changes should be followed for precision
        public double[] DeltaEigenValues; // Array of eigenvalue changes
        public int MaximumNumberOfIterations; // Maximum number of iterations to conduct for calculating eigenvalue
        public int NumberOfIterations; // The iteration that produced the Precision
        public Matrix_EigenValueEigenVector_Solver()
        {
            //Set default values
            Precision = 1.0E-20;
            NumberOfEigenValueChangesChecked = 3;
            DeltaEigenValues = new double[NumberOfEigenValueChangesChecked];
            NumberOfIterations = 0;
            MaximumNumberOfIterations = 100;
        }
        public virtual void CalculateFirstEigenValueAndEigenVector(MatrixObject A, out double EighenValue, out Vector EigenVector)
        {
            //Set X equal to the eigenvector
            Vector X = new Vector(A.n);

            //Initialize X to a vector with all components the same
            X.SetComponentsEqualToUnity(); 
            CalculateFirstEigenValueAndEigenVector(A, X, out EighenValue, out EigenVector);
        }
        public virtual void CalculateFirstEigenValueAndEigenVector(MatrixObject A, Vector InitialValueForEigenVector, out double EighenValue, out Vector EigenVector)
        {
            //Set X equal to initial eigenvector
            Vector X = InitialValueForEigenVector;
            X.MakeThisAUnitVector();
            EighenValue = 1.0d;;

            //Hold old eigenvalue
            double OldEigenvalue = 0.0d;

            //Use power method to iteratively calculate eigenvalue and eigenvector
            for (int i = 0; i < MaximumNumberOfIterations; i++)
            {
                NumberOfIterations++;
                Vector Z = A.MultiplyByVectorU(X);
                EighenValue = Vector.DotProduct(X, Z);
                Z.MakeThisAUnitVector();
                X = Z;
                double DeltaLambda = EighenValue - OldEigenvalue;
                UpdateDeltaEigenValues(DeltaLambda);
                OldEigenvalue = EighenValue;
                if (i > NumberOfEigenValueChangesChecked - 1)
                {
                    if (ArrayTools.Vector_Magnitude(DeltaEigenValues) < Precision)
                    {
                        EigenVector = X;
                        return;
                    }
                }
            }
            EigenVector = X;
            return;
        }
        private void UpdateDeltaEigenValues(double DeltaLambda)
        {
            for (int i = 0; i < NumberOfEigenValueChangesChecked-1; i++)
            {
                DeltaEigenValues[i] = DeltaEigenValues[i + 1];
            }
            DeltaEigenValues[NumberOfEigenValueChangesChecked - 1] = DeltaLambda;
        }
    }
}
