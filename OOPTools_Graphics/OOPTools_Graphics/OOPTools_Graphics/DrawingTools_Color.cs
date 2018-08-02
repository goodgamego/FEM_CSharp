using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;
using System.ComponentModel;

using OOPTools_Math;

namespace OOPTools_Graphics
{
    class DrawingTools_Color
    {
    }
    public static class OOPsColor 
    {
        #region Methods for colors and color scale
        public static Color ColorScale(double Number)
        {
            // Takes a number from 0 to 1 and assigns a color to it
            Color returnColor;
            if ((Number < 0.0D))
            {
                returnColor = Color.DarkBlue;
            }
            else if ((Number >= 0.0D) && (Number < 0.2D))
            {
                returnColor = GetAColorBetweenTwoColors(Color.DarkBlue, Color.Blue, (Number - 0.0D) / 0.2D);
            }
            else if ((Number >= 0.2D) && (Number < 0.4D))
            {
                returnColor = GetAColorBetweenTwoColors(Color.Blue, Color.LightBlue, (Number - 0.2D) / 0.2D);
            }
            else if ((Number >= 0.4D) && (Number < 0.5D))
            {
                returnColor = GetAColorBetweenTwoColors(Color.LightBlue, Color.LightGreen, (Number - 0.4D) / 0.1D);
            }
            else if ((Number >= 0.5D) && (Number < 0.6D))
            {
                returnColor = GetAColorBetweenTwoColors(Color.LightGreen, Color.Yellow, (Number - 0.5D) / 0.1D);
            }
            else if ((Number >= 0.6D) && (Number < 0.7D))
            {
                returnColor = GetAColorBetweenTwoColors(Color.Yellow, Color.Orange, (Number - 0.6D) / 0.1D);
            }
            else if ((Number >= 0.7D) && (Number < 0.9D))
            {
                returnColor = GetAColorBetweenTwoColors(Color.Orange, Color.Red, (Number - 0.7D) / 0.2D);
            }
            else if ((Number >= 0.9D) && (Number < 1.0D))
            {
                returnColor = GetAColorBetweenTwoColors(Color.Red, Color.DarkRed, (Number - 0.9D) / 0.1D);
            }
            else
            {
                returnColor = Color.DarkRed;
            }

            return returnColor;

        }
        public static Color GetAColorBetweenTwoColors(Color Color1, Color Color2, double Fraction)
        {
            // Takes a number Fraction from 0 to 1 and assigns a color between Color1 and Color2
            double[] Mix1 = GetColorMix(Color1);
            double[] Mix2 = GetColorMix(Color2);
            double[] NewMix = GetColorMix(Color1);
            int[] NewMixInt = new int[NewMix.Length];
            for (int i = 0; i < NewMix.Length; i++)
            {
                NewMix[i] += Fraction * (Mix2[i] - Mix1[i]);
                NewMixInt[i] = Convert.ToInt32(NewMix[i]);
            }
            Color NewColor = Color.FromArgb(NewMixInt[0], NewMixInt[1], NewMixInt[2], NewMixInt[3]);
            return NewColor;
        }
        public static double[] GetColorMix(Color SelectedColor)
        {
            double[] Mix = new double[4];
            Mix[0] = Convert.ToDouble(SelectedColor.A);
            Mix[1] = Convert.ToDouble(SelectedColor.R);
            Mix[2] = Convert.ToDouble(SelectedColor.G);
            Mix[3] = Convert.ToDouble(SelectedColor.B);
            return Mix;
        }
        public static void GetColorsToUse(int N, double X1, double X2, out Single[] X, out Color[] C)
        {
            
            double DX = 1.0D / Convert.ToDouble(N - 1);
            double DC = (X2 - X1) * DX;
            X = new Single[N];
            C = new Color[N];
            for (int i = 0; i < N; i++)
            {
                
                X[i] = Convert.ToSingle(i) * Convert.ToSingle(DX);
                double CX = Convert.ToDouble(i) * DC + X1;
                C[i] = ColorScale(CX);
            }
        }
        #endregion

        #region Methods for color gradient
        public static void Calculate_YGradient(Vector[] X, double[] Y, out Vector Center_X, out Vector[] DX, out double Center_Y, out Vector Gradient)
        {
            int N = X.Length;
            Vector X_c = Vector.Average(X);
            Vector Sum_DX = new Vector(2);
            double Sum_Y = 0.0D;
            Vector Sum_YDX = new Vector(2);
            DX = new Vector[N];
            for (int i = 0; i < N; i++)
            {
                DX[i] = new Vector(2);
                DX[i] = X[i] - X_c;
                Sum_DX += DX[i];
            }
            OOPTools_Math.Matrix_Jagged M = new OOPTools_Math.Matrix_Jagged(2, 2);
            for (int i = 0; i < N; i++)
            {
                M += OOPTools_Math.Matrix_Jagged.TensorProduct(DX[i], DX[i]);
            }
            Matrix_Jagged A = new Matrix_Jagged(3, 3);
            A.Values[0][0] = Convert.ToDouble(N);
            A.Values[0][1] = Sum_DX.Values[0];
            A.Values[0][2] = Sum_DX.Values[1];
            A.Values[1][0] = Sum_DX.Values[0];
            A.Values[1][1] = M.Values[0][0];
            A.Values[1][2] = M.Values[0][1];
            A.Values[2][0] = Sum_DX.Values[1];
            A.Values[2][1] = M.Values[1][0];
            A.Values[2][2] = M.Values[1][1];

            double DetA;
            A.SolveLinearSystem_LUDecomp(out DetA);
            Gradient = new Vector(2);
            if (DetA < 1.0E-20)
            {
                Center_Y = ArrayTools.Average(Y);
            }
            else
            {
                for (int i = 0; i < N; i++)
                {
                    Sum_Y += Y[i];
                    Sum_YDX += Y[i] * DX[i];
                }
                Vector b = new Vector(3);
                b.Values[0] = Sum_Y;
                b.Values[1] = Sum_YDX.Values[0];
                b.Values[2] = Sum_YDX.Values[1];

                Vector x = A.SolveLinearSystem_BackSub(b);
                Center_Y = x.Values[0];
                Gradient.Values[0] = x.Values[1];
                Gradient.Values[1] = x.Values[2];
            }
            Center_X = X_c;
        }
        public static void Calculate_Gradient_PointsAndValues(Vector[] X, double[] Y, out Vector X1, out Vector X2, out double Y1, out double Y2)
        {
            Vector[] DX;
            Vector Center_X, Gradient;
            double Center_Y;
            OOPsColor.Calculate_YGradient(X, Y, out Center_X, out DX, out Center_Y, out Gradient);
            double d_min, d_max;
            Vector n = new Vector(2);
            if (Gradient.Magnitude() > 1.0E-20)
            {
                n = Gradient.UnitVector();
            }
            else
            {
                n.Values[0] = 1.0D;  
            }
            Find_MaxMinAlongDirection(DX, n, out d_min, out d_max);

            d_min *= 1.05D;
            d_max *= 1.05D;
            
            X1 = Center_X + d_min * n;
            X2 = Center_X + d_max * n;

            double n_Dot_Gradient = Vector.DotProduct(n, Gradient);
            Y1 = Center_Y + d_min * n_Dot_Gradient;
            Y2 = Center_Y + d_max * n_Dot_Gradient;
        }
        public static void Find_MaxMinAlongDirection(Vector[] X, Vector Direction_UnitVector, out double d_min, out double d_max)
        {
            double d = Vector.DotProduct(X[0], Direction_UnitVector);
            d_min = d;
            d_max = d;
             
            for (int i = 1; i < X.Length; i++)
            {
                d = Vector.DotProduct(X[i], Direction_UnitVector);
                if (d < d_min) d_min = d;
                if (d > d_max) d_max = d;
            }
        }
        public static void Calculate_LineGradientBrushParameters_ForPolygon(Vector[] X, double[] Y, out Vector X1, out Vector X2, out Color C1, out Color C2)
        {
            double Y1, Y2;
            Calculate_Gradient_PointsAndValues(X, Y, out  X1, out X2, out Y1, out Y2);
            C1 = ColorScale(Y1);
            C2 = ColorScale(Y2);
        }
        public static void Calculate_LineGradientBrushParameters_ForPolygon(Vector[] X, double[] Y, out Vector X1, out Vector X2, out Color C1, out Color C2, int N, out Single[] XColor, out Color[] Colors)
        {
            double Y1, Y2;
            Calculate_Gradient_PointsAndValues(X, Y, out  X1, out X2, out Y1, out Y2);
            C1 = ColorScale(Y1);
            C2 = ColorScale(Y2);
            OOPsColor.GetColorsToUse(N, Y1, Y2, out XColor, out Colors);
        }
        #endregion
    }
}
