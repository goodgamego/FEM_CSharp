using System;
using System.Collections.Generic;
using System.Text;

namespace OOPTools_Math
{
    [Serializable]
    public class MathTools
    {

        #region Vector operations
        /// <summary>
        /// Calculate a unit vector from the given array (N-dimensional)
        /// </summary>
        /// <param name="Vector"></param>
        /// <returns></returns>
        public static double[] CalculateUnitVector(double[] Vector)
        {

            int NumberOfDimensions = Vector.Length;
            double length = CalculateLengthOfVector(Vector);
            double[] Value = new double[NumberOfDimensions];
            for (int i = 0; i < NumberOfDimensions; i++)
            {
                Value[i] = Vector[i] / length;
            }
            return Value;
        }
        /// <summary>
        /// Dot product of two vectors (N-dimensional)
        /// </summary>
        /// <param name="X1"></param>
        /// <param name="X2"></param>
        /// <returns></returns>
        public static double CalculateDotProductOfTwoVectors(double[] X1, double[] X2)
        {

            int NumberOfDimensions = X1.Length;
            double Value = 0.0d;
            for (int i = 0; i < NumberOfDimensions; i++)
            {
                Value += X2[i] * X1[i];
            }
            return Value;
        }
        /// <summary>
        /// Calculate the vector going from X1 to X2
        /// </summary>
        /// <param name="X1"></param>
        /// <param name="X2"></param>
        /// <returns></returns>
        public static double[] CalculateVectorConnectingTwoPoints(double[] X1, double[] X2)
        {

            int NumberOfDimensions = X1.Length;
            double[] Value = new double[NumberOfDimensions];
            for (int i = 0; i < NumberOfDimensions; i++)
            {
                Value[i] = X2[i] - X1[i];
            }
            return Value;
        }
        /// <summary>
        /// Add vectors (N-dimensional)
        /// </summary>
        /// <param name="X1"></param>
        /// <param name="X2"></param>
        /// <returns></returns>
        public static double[] AddVectors(double[] X1, double[] X2)
        {

            int NumberOfDimensions = X1.Length;
            double[] Value = new double[NumberOfDimensions];
            for (int i = 0; i < NumberOfDimensions; i++)
            {
                Value[i] = X2[i] + X1[i];
            }
            return Value;
        }
        /// <summary>
        /// Calculate the length of the vector (N-dimensional)
        /// </summary>
        /// <param name="X"></param>
        /// <returns></returns>
        public static double CalculateLengthOfVector(double[] X)
        {
            double Value = 0.0d;
            int NumberOfDimensions = X.Length;
            for (int i = 0; i < NumberOfDimensions; i++)
            {
                Value += X[i] * X[i];
            }
            return Math.Sqrt(Value);
        }

        #endregion 
        #region methods to rotate to rotate vectors
        /// <summary>
        /// Calculate the rotation matrix for a rotation about the axis of rotation by the angle
        /// </summary>
        /// <param name="AxisOfRotation"></param>
        /// <param name="AngleRad"></param>
        /// <returns></returns>
        public static Matrix RotationMatrixForRotationAboutAxis(double[] AxisOfRotation, double AngleRad)
        {
            double theta, phi;
            double[] axis = CalculateUnitVector(AxisOfRotation);
            if (Math.Abs(axis[2]) > 1.0d) axis[2] = Math.Sign(axis[2]) * 1.0d;
            theta = Math.Acos(axis[2]);
            if (axis[0] * axis[0] + axis[1] * axis[1] < 1.0E-14)
            {
                phi = 0.0d;
            }
            else
            {
                phi = Math.Atan2(axis[1], axis[0]);
            }
            Matrix Q1  = new Matrix(ArrayTools.RotationMatrixAbout2Axis(-phi));
            Matrix Q2 = new Matrix(ArrayTools.RotationMatrixAbout1Axis(-theta));
            Matrix Q = Q1.Transpose() * Q2.Transpose() * ArrayTools.RotationMatrixAbout2Axis(AngleRad) * Q2 * Q1;
            return Q;
        }
        /// <summary>
        /// Convet radians to degrees
        /// </summary>
        /// <param name="AngleRad"></param>
        /// <returns></returns>
        public static double RadToDeg(double AngleRad)
        {
            return AngleRad * 180.0d / Math.PI;
        }
        /// <summary>
        /// Convert degrees to radians
        /// </summary>
        /// <param name="AngleDeg"></param>
        /// <returns></returns>
        public static double DegToRad(double AngleDeg)
        {
            return AngleDeg * Math.PI / 180.0d;
        }
        
        public static Matrix CalculateRotationMatrixUsingDirectionsOnCircle(double[] Point1, double[] Point2, double Diagonal)
        {
            /// Using direction circle
            /// 
            double[] DeltaX = CalculateVectorConnectingTwoPoints(Point1, Point2);
            //MessageBox.Show("DeltaX = "+DeltaX[0].ToString() + "  " + DeltaX[1].ToString());
            double[] X1_3D = new double[3];
            double[] X2_3D = new double[3];
            double theta = Math.Atan2(DeltaX[1], DeltaX[0]);
            double thetaDeg = RadToDeg(theta);
            if ((thetaDeg > -30.0d) && (thetaDeg <= 30.0d))
            {
                double phi = 30.0d + thetaDeg;
                phi = DegToRad(phi * 90.0d / 120.0d);
                X2_3D[1] = Math.Cos(phi);
                X2_3D[2] = Math.Sin(phi);
                phi = 90.0d + thetaDeg;
                phi = DegToRad(phi * 90.0d / 120.0d);
                X1_3D[2] = Math.Cos(phi);
                X1_3D[0] = Math.Sin(phi);
            }
            else if ((thetaDeg > 30.0d) && (thetaDeg <= 90.0d))
            {
                double phi = 30.0d + thetaDeg;
                phi = DegToRad(phi * 90.0d / 120.0d);
                X2_3D[1] = Math.Cos(phi);
                X2_3D[2] = Math.Sin(phi);
                phi = -30.0d + thetaDeg;
                phi = DegToRad(phi * 90.0d / 120.0d);
                X1_3D[0] = Math.Cos(phi);
                X1_3D[1] = Math.Sin(phi);
            }
            else if ((thetaDeg > 90.0d) && (thetaDeg <= 150.0d))
            {
                double phi = -90.0d + thetaDeg;
                phi = DegToRad(phi * 90.0d / 120.0d);
                X2_3D[2] = Math.Cos(phi);
                X2_3D[0] = Math.Sin(phi);
                phi = -30.0d + thetaDeg;
                phi = DegToRad(phi * 90.0d / 120.0d);
                X1_3D[0] = Math.Cos(phi);
                X1_3D[1] = Math.Sin(phi);
            }
            else if ((thetaDeg > 150.0d) && (thetaDeg <= 210.0d))
            {
                double phi = -90.0d + thetaDeg;
                phi = DegToRad(phi * 90.0d / 120.0d);
                X2_3D[2] = Math.Cos(phi);
                X2_3D[0] = Math.Sin(phi);
                phi = -150.0d + thetaDeg;
                phi = DegToRad(phi * 90.0d / 120.0d);
                X1_3D[1] = Math.Cos(phi);
                X1_3D[2] = Math.Sin(phi);
            }
            else if ((thetaDeg > 210.0d) && (thetaDeg <= 270.0d))
            {
                double phi = -210.0d + thetaDeg;
                phi = DegToRad(phi * 90.0d / 120.0d);
                X2_3D[0] = Math.Cos(phi);
                X2_3D[1] = Math.Sin(phi);
                phi = -150.0d + 60.0d + thetaDeg;
                phi = DegToRad(phi * 90.0d / 120.0d);
                X1_3D[1] = Math.Cos(phi);
                X1_3D[2] = Math.Sin(phi);
            }
            else if ((thetaDeg > 270.0d) && (thetaDeg <= 330.0d))
            {
                double phi = -210.0d + thetaDeg;
                phi = DegToRad(phi * 90.0d / 120.0d);
                X2_3D[0] = Math.Cos(phi);
                X2_3D[1] = Math.Sin(phi);
                phi = -270.0d + thetaDeg;
                phi = DegToRad(phi * 90.0d / 120.0d);
                X1_3D[2] = Math.Cos(phi);
                X1_3D[0] = Math.Sin(phi);
            }

            double RotationAngleRad = ArrayTools.Vector_Magnitude(DeltaX) * Math.PI*0.5d / Diagonal;
            double[] RotationAxis = ArrayTools.Vector_CrossProduct(X1_3D, X2_3D);
            Matrix Q = RotationMatrixForRotationAboutAxis(RotationAxis, RotationAngleRad);
            return Q;
        }
        #endregion
    }
}
