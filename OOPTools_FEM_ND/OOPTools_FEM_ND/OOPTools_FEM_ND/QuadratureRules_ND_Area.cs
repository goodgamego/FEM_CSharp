using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OOPTools_FEM_1D;
using OOPTools_Math;

namespace OOPTools_FEM_ND
{
    [Serializable]
    public class QuadratureRules_ND_Area
    {
        /// <summary>
        /// Developed by: Mehrdad Negahban
        /// Date: 11/12/2012
        ///
        /// Purpose: Construct Quadrature points and weights for different number of integration points
        /// Comments: Gaussian 
        ///
        /// Date modified:
        /// Modified by:
        /// Comments:
        /// </summary>
        public static QuadratureRule_ND Make_QuadratureRule_Rectangular_Gaussian_MxNPoints(int M_NIP, int N_NIP)
        {
            QuadratureRule QR_M = GaussianQuadratureRule.MakeGaussianQuadrature(M_NIP);
            QuadratureRule QR_N = GaussianQuadratureRule.MakeGaussianQuadrature(N_NIP);
            QuadratureRule_ND QR_ND = new QuadratureRule_ND();
            QR_ND.NIP = M_NIP*N_NIP;
            QR_ND.wi = new double[QR_ND.NIP];
            QR_ND.Xi = new Vector[QR_ND.NIP];
            int Index = 0;
            for (int i = 0; i < M_NIP; i++)
            {
                for (int j = 0; j < N_NIP; j++)
                {
                    QR_ND.wi[Index] = QR_M.wi[i] * QR_N.wi[j];
                    QR_ND.Xi[Index] = new Vector(2);
                    QR_ND.Xi[Index].Values[0] = QR_M.Xi[i];
                    QR_ND.Xi[Index].Values[1] = QR_N.Xi[j];
                    Index++;
                }
            }
            return QR_ND;
        }
        public static QuadratureRule_ND Make_QuadratureRule_Triangle_Gaussian(int PolynomialOrder)
        {
            QuadratureRules_ND_Area QR_Area = new QuadratureRules_ND_Area();
            if (PolynomialOrder == 0)
            {
                return QR_Area.Make_QuadratureRule_Triangle_Gaussian_Order_1();
            }
            else if (PolynomialOrder == 1)
            {
                return QR_Area.Make_QuadratureRule_Triangle_Gaussian_Order_1();
            }
            else if (PolynomialOrder == 2)
            {
                return QR_Area.Make_QuadratureRule_Triangle_Gaussian_Order_2();
            }
            else if (PolynomialOrder == 3)
            {
                return QR_Area.Make_QuadratureRule_Triangle_Gaussian_Order_3();
            }
            else if (PolynomialOrder == 4)
            {
                return QR_Area.Make_QuadratureRule_Triangle_Gaussian_Order_5();
            }
            else if (PolynomialOrder == 5)
            {
                return QR_Area.Make_QuadratureRule_Triangle_Gaussian_Order_5();
            }
            else
            {
                return QR_Area.Make_QuadratureRule_Triangle_Gaussian_Order_5();
            }
        }
        public QuadratureRule_ND Make_QuadratureRule_Triangle_Gaussian_Order_1()
        {
            QuadratureRule_ND QR = Initialize_2D_QuadratureRule(1);
            QR.wi[0] = 1.0D;
            double Xi = 1.0D / 3.0D;
            QR.Xi[0].Values[0] = Xi;
            QR.Xi[0].Values[1] = Xi;
            return QR;
        }
        public QuadratureRule_ND Make_QuadratureRule_Triangle_Gaussian_Order_2()
        {
            QuadratureRule_ND QR = Initialize_2D_QuadratureRule(3);

            double Wi = 1.0D / 3.0D;

            QR.wi[0] = Wi;
            QR.Xi[0].Values[0] = 0.5D;
            QR.Xi[0].Values[1] = 0.5D;

            QR.wi[1] = Wi;
            QR.Xi[1].Values[0] = 0.0D;
            QR.Xi[1].Values[1] = 0.5D;

            QR.wi[2] = Wi;
            QR.Xi[2].Values[0] = 0.5D;
            QR.Xi[2].Values[1] = 0.0D;

            return QR;
        }
        public QuadratureRule_ND Make_QuadratureRule_Triangle_Gaussian_Order_3()
        {
            QuadratureRule_ND QR = Initialize_2D_QuadratureRule(4);

            double Xi_0 = 1.0D / 3.0D;

            QR.wi[0] = -27.0D/48.0D;
            QR.Xi[0].Values[0] = Xi_0;
            QR.Xi[0].Values[1] = Xi_0;

            double Wi = 25.0D / 48.0D;

            QR.wi[1] = Wi;
            QR.Xi[1].Values[0] = 0.6D;
            QR.Xi[1].Values[1] = 0.2D;

            QR.wi[2] = Wi;
            QR.Xi[2].Values[0] = 0.2D;
            QR.Xi[2].Values[1] = 0.6D;

            QR.wi[3] = Wi;
            QR.Xi[3].Values[0] = 0.2D;
            QR.Xi[3].Values[1] = 0.2D;

            return QR;
        }
        public QuadratureRule_ND Make_QuadratureRule_Triangle_Gaussian_Order_5()
        {
            QuadratureRule_ND QR = Initialize_2D_QuadratureRule(7);

            double Xi_0 = 1.0D / 3.0D;

            QR.wi[0] = 0.225;
            QR.Xi[0].Values[0] = Xi_0;
            QR.Xi[0].Values[1] = Xi_0;

            double Wi_1 = (155.0D - Math.Sqrt(15.0D))/1200.0D;
            double Wi_2 = (155.0D - Math.Sqrt(15.0D)) / 1200.0D;

            double t_1 = (1.0D + Math.Sqrt(15.0D)) / 7.0D;
            double A_1 = (1.0D + 2.0D * t_1) / 3.0D;
            double B_1 = (1.0D - t_1) / 3.0D;
            
            QR.wi[1] = Wi_1;
            QR.Xi[1].Values[0] = A_1;
            QR.Xi[1].Values[1] = B_1;

            QR.wi[2] = Wi_1;
            QR.Xi[2].Values[0] = B_1;
            QR.Xi[2].Values[1] = A_1;

            QR.wi[3] = Wi_1;
            QR.Xi[3].Values[0] = B_1;
            QR.Xi[3].Values[1] = B_1;

            double t_2 = (1.0D - Math.Sqrt(15.0D)) / 7.0D;
            double A_2 = (1.0D + 2.0D * t_2) / 3.0D;
            double B_2 = (1.0D - t_2) / 3.0D;

            QR.wi[4] = Wi_2;
            QR.Xi[4].Values[0] = A_2;
            QR.Xi[4].Values[1] = B_2;

            QR.wi[5] = Wi_2;
            QR.Xi[5].Values[0] = B_2;
            QR.Xi[5].Values[1] = A_2;

            QR.wi[6] = Wi_2;
            QR.Xi[6].Values[0] = B_2;
            QR.Xi[6].Values[1] = B_2;

            return QR;
        }
        public QuadratureRule_ND Initialize_2D_QuadratureRule(int NumberOfIntegrationPoints)
        {
            QuadratureRule_ND QR = new QuadratureRule_ND();
            QR.NIP = NumberOfIntegrationPoints;
            QR.wi = new double[NumberOfIntegrationPoints];
            QR.Xi = new Vector[NumberOfIntegrationPoints];
            for (int i = 0; i < NumberOfIntegrationPoints; i++)
            {
                QR.Xi[i] = new Vector(2);
            }
            return QR;
        }
        
    }
}
