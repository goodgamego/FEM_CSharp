using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OOPTools_Math
{
    public class MatrixTools
    {
        #region Rotation of vectors and tools for this rotation
        /// <summary>
        /// Calculate the rotation matrix about arbitraty axis
        /// </summary>
        /// <param name="AngleRad"></param>
        /// <returns></returns>
        public static double[] RotationVectorAboutAxis(double[] VectorToRotate, double RotationAngle_Rad, double[] UnitVectorAlongAxisOfRotation)
        {
            double[][] Q = RotationMatrixAboutAxis_Jagged(RotationAngle_Rad, UnitVectorAlongAxisOfRotation);
            double[] NewVector = new double[3];
            double[] QRow;
            for (int i = 0; i < 3; i++)
            {
                QRow = Q[i];
                for (int j = 0; j < 3; j++)
                {
                    NewVector[i] += QRow[j] * VectorToRotate[j];
                }
            }
            return NewVector;
        }
        /// <summary>
        /// Calculate the rotation matrix about arbitraty axis-0
        /// </summary>
        /// <param name="AngleRad"></param>
        /// <returns></returns>
        public static double[] RotationVectorAboutAxis0(double[] VectorToRotate, double RotationAngle_Rad)
        {
            double phi = RotationAngle_Rad;
            double cphi = Math.Cos(phi);
            double sphi = Math.Sin(phi);
            double[] u = VectorToRotate;
            double[] NewVector = new double[3];
            NewVector[0] = u[0];
            NewVector[1] = cphi * u[1] - sphi * u[2];
            NewVector[2] = sphi * u[1] + cphi * u[2];
            return NewVector;
        }
        /// <summary>
        /// Calculate the rotation matrix about arbitraty axis-1
        /// </summary>
        /// <param name="AngleRad"></param>
        /// <returns></returns>
        public static double[] RotationVectorAboutAxis1(double[] VectorToRotate, double RotationAngle_Rad)
        {
            double phi = RotationAngle_Rad;
            double cphi = Math.Cos(phi);
            double sphi = Math.Sin(phi);
            double[] u = VectorToRotate;
            double[] NewVector = new double[3];
            NewVector[0] = cphi * u[0] + sphi * u[2];
            NewVector[1] = u[1];
            NewVector[2] = -sphi * u[0] + cphi * u[2];
            return NewVector;
        }
        /// <summary>
        /// Calculate the rotation matrix about arbitraty axis-2
        /// </summary>
        /// <param name="AngleRad"></param>
        /// <returns></returns>
        public static double[] RotationVectorAboutAxis2(double[] VectorToRotate, double RotationAngle_Rad)
        {
            double phi = RotationAngle_Rad;
            double cphi = Math.Cos(phi);
            double sphi = Math.Sin(phi);
            double[] u = VectorToRotate;
            double[] NewVector = new double[3];
            NewVector[0] = cphi * u[0] - sphi * u[1];
            NewVector[1] = sphi * u[0] + cphi * u[1];
            NewVector[2] = u[2];
            return NewVector;
        }
        /// <summary>
        /// Calculate the rotation matrix about arbitraty axis
        /// </summary>
        /// <param name="AngleRad"></param>
        /// <returns></returns>
        public static double[][] RotationMatrixAboutAxis_Jagged(double RotationAngle_Rad, double[] UnitVectorAlongAxisOfRotation)
        {
            double phi = RotationAngle_Rad;
            double[] ni = UnitVectorAlongAxisOfRotation;
            double[][] Q = new double[3][]; //Rotation matrix
            double cphi = Math.Cos(phi);
            double sphi = Math.Sin(phi);
            double OneMinuscphi = 1.0D - cphi;
            for (int i = 0; i < 3; i++)
            {
                Q[i] = new double[3];
                Q[i][i] = cphi;
                for (int j = 0; j < 3; j++)
                {
                    Q[i][j] += OneMinuscphi * ni[i] * ni[j];
                }
            }
            double ux = sphi * ni[0];
            double uy = sphi * ni[1];
            double uz = sphi * ni[0];
            Q[0][1] -= uz;
            Q[0][2] += uy;
            Q[1][2] -= ux;

            Q[1][0] += uz;
            Q[2][0] -= uy;
            Q[2][1] += ux;

            return Q;
        }
        /// <summary>
        /// Calculate the rotation matrix about arbitraty axis
        /// </summary>
        /// <param name="AngleRad"></param>
        /// <returns></returns>
        public static double[,] RotationMatrixAboutAxis(double RotationAngle_Rad, double[] UnitVectorAlongAxisOfRotation)
        {
            double phi = RotationAngle_Rad;
            double[] ni = UnitVectorAlongAxisOfRotation;
            double[,] Q = new double[3, 3]; //Rotation matrix
            double cphi = Math.Cos(phi);
            double sphi = Math.Sin(phi);
            double OneMinuscphi = 1.0D - cphi;
            for (int i = 0; i < 3; i++)
            {
                Q[i, i] = cphi;
                for (int j = 0; j < 3; j++)
                {
                    Q[i, j] += OneMinuscphi * ni[i] * ni[j];
                }
            }
            double ux = sphi * ni[0];
            double uy = sphi * ni[1];
            double uz = sphi * ni[0];
            Q[0, 1] -= uz;
            Q[0, 2] += uy;
            Q[1, 2] -= ux;

            Q[1, 0] += uz;
            Q[2, 0] -= uy;
            Q[2, 1] += ux;

            return Q;
        }
        /// <summary>
        /// Calculate the rotation matrix for rotation about the 0-axis
        /// </summary>
        /// <param name="AngleRad"></param>
        /// <returns></returns>
        public static double[,] RotationMatrixAbout0Axis(double AngleRad)
        {
            double[,] Q = new double[3, 3];
            Q[0, 0] = 1.0d;
            Q[1, 1] = Math.Cos(AngleRad);
            Q[2, 2] = Q[1, 1];
            Q[1, 2] = -Math.Sin(AngleRad);
            Q[2, 1] = -Q[1, 2];
            return Q;
        }
        /// <summary>
        /// Calculate the rotation matrix for rotation about the 0-axis
        /// </summary>
        /// <param name="AngleRad"></param>
        /// <returns></returns>
        public static double[][] RotationMatrixAbout0Axis_Jagged(double AngleRad)
        {
            double[][] Q = new double[3][];
            for (int i = 0; i < 3; i++)
            {
                Q[i] = new double[3];
            }
            Q[0][0] = 1.0d;
            Q[1][1] = Math.Cos(AngleRad);
            Q[2][2] = Q[1][1];
            Q[1][2] = -Math.Sin(AngleRad);
            Q[2][1] = -Q[1][2];
            return Q;
        }
        /// <summary>
        /// Calculate the rotation matrix for rotation about the 1-axis
        /// </summary>
        /// <param name="AngleRad"></param>
        /// <returns></returns>
        public static double[,] RotationMatrixAbout1Axis(double AngleRad)
        {
            double[,] Q = new double[3, 3];
            Q[1, 1] = 1.0d;
            Q[0, 0] = Math.Cos(AngleRad);
            Q[2, 2] = Q[0, 0];
            Q[0, 2] = Math.Sin(AngleRad);
            Q[2, 0] = -Q[0, 2];
            return Q;
        }
        /// <summary>
        /// Calculate the rotation matrix for rotation about the 1-axis
        /// </summary>
        /// <param name="AngleRad"></param>
        /// <returns></returns>
        public static double[][] RotationMatrixAbout1Axis_Jagged(double AngleRad)
        {
            double[][] Q = new double[3][];
            for (int i = 0; i < 3; i++)
            {
                Q[i] = new double[3];
            }
            Q[1][1] = 1.0d;
            Q[0][0] = Math.Cos(AngleRad);
            Q[2][2] = Q[0][0];
            Q[0][2] = Math.Sin(AngleRad);
            Q[2][0] = -Q[0][2];
            return Q;
        }
        /// <summary>
        /// Calculate the rotation matrix for rotation about the 2-axis
        /// </summary>
        /// <param name="AngleRad"></param>
        /// <returns></returns>
        public static double[,] RotationMatrixAbout2Axis(double AngleRad)
        {
            double[,] Q = new double[3, 3];
            Q[2, 2] = 1.0d;
            Q[0, 0] = Math.Cos(AngleRad);
            Q[1, 1] = Q[0, 0];
            Q[0, 1] = -Math.Sin(AngleRad);
            Q[1, 0] = -Q[0, 1];
            return Q;
        }
        /// <summary>
        /// Calculate the rotation matrix for rotation about the 2-axis
        /// </summary>
        /// <param name="AngleRad"></param>
        /// <returns></returns>
        public static double[][] RotationMatrixAbout2Axis_Jagged(double AngleRad)
        {
            double[][] Q = new double[3][];
            for (int i = 0; i < 3; i++)
            {
                Q[i] = new double[3];
            }
            Q[2][2] = 1.0d;
            Q[0][0] = Math.Cos(AngleRad);
            Q[1][1] = Q[0][0];
            Q[0][1] = -Math.Sin(AngleRad);
            Q[1][0] = -Q[0][1];
            return Q;
        }
        #endregion
    }
}
