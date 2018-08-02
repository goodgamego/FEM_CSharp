using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OOPTools_FEM_1D;
using OOPTools_Math;

namespace OOPTools_FEM_ND
{
    [Serializable]
    public class QuadratureRules_ND_Volume
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
        public static QuadratureRule_ND Make_QuadratureRule_Rectangular_Gaussian_LxMxNPoints(int L_NIP, int M_NIP, int N_NIP)
        {
            QuadratureRule QR_L = GaussianQuadratureRule.MakeGaussianQuadrature(L_NIP);
            QuadratureRule QR_M = GaussianQuadratureRule.MakeGaussianQuadrature(M_NIP);
            QuadratureRule QR_N = GaussianQuadratureRule.MakeGaussianQuadrature(N_NIP);
            QuadratureRule_ND QR_ND = new QuadratureRule_ND();
            QR_ND.NIP = L_NIP*M_NIP * N_NIP;
            QR_ND.wi = new double[QR_ND.NIP];
            QR_ND.Xi = new Vector[QR_ND.NIP];
            int Index = 0;
            for (int i = 0; i < L_NIP; i++)
            {
                for (int j = 0; j < M_NIP; j++)
                {
                    for (int k = 0; k < N_NIP; k++)
                    {
                        QR_ND.wi[Index] = QR_L.wi[i] * QR_M.wi[j] * QR_N.wi[k];
                        QR_ND.Xi[i] = new Vector(3);
                        QR_ND.Xi[i].Values[0] = QR_L.Xi[i];
                        QR_ND.Xi[i].Values[1] = QR_M.Xi[j];
                        QR_ND.Xi[i].Values[2] = QR_N.Xi[k];
                    }
                }
            }
            return QR_ND;
        }
        public static QuadratureRule_ND Make_QuadratureRule_Tetrahedron_Gaussian(int PolynomialOrder)
        {
            QuadratureRules_ND_Volume QR_Area = new QuadratureRules_ND_Volume();
            if (PolynomialOrder == 0)
            {
                return QR_Area.Make_QuadratureRule_Tetrahedron_Gaussian_Order_1();
            }
            else if (PolynomialOrder == 1)
            {
                return QR_Area.Make_QuadratureRule_Tetrahedron_Gaussian_Order_1();
            }
            else if (PolynomialOrder == 2)
            {
                return QR_Area.Make_QuadratureRule_Tetrahedron_Gaussian_Order_2();
            }
            else if (PolynomialOrder == 3)
            {
                return QR_Area.Make_QuadratureRule_Tetrahedron_Gaussian_Order_3();
            }
            else
            {
                return QR_Area.Make_QuadratureRule_Tetrahedron_Gaussian_Order_3();
            }
        }
        public QuadratureRule_ND Make_QuadratureRule_Tetrahedron_Gaussian_Order_1()
        {
            QuadratureRule_ND QR = Initialize_3D_QuadratureRule(1);
            QR.wi[0] = 1.0D;
            double Xi = 1.0D / 4.0D;
            QR.Xi[0].Values[0] = Xi;
            QR.Xi[0].Values[1] = Xi;
            QR.Xi[0].Values[2] = Xi;
            return QR;
        }
        public QuadratureRule_ND Make_QuadratureRule_Tetrahedron_Gaussian_Order_2()
        {
            QuadratureRule_ND QR = Initialize_3D_QuadratureRule(4);

            double Wi = 1.0D / 4.0D;
            double t = 1.0D / Math.Sqrt(5.0D);
            double A = (1.0D - t) / 4.0D;
            double B = (1.0D + 3.0D * t) / 4.0D;

            QR.wi[0] = Wi;
            QR.Xi[0].Values[0] = A;
            QR.Xi[0].Values[1] = A;
            QR.Xi[0].Values[2] = A;

            QR.wi[1] = Wi;
            QR.Xi[1].Values[0] = A;
            QR.Xi[1].Values[1] = A;
            QR.Xi[1].Values[2] = B;

            QR.wi[2] = Wi;
            QR.Xi[2].Values[0] = B;
            QR.Xi[2].Values[1] = A;
            QR.Xi[2].Values[2] = A;

            QR.wi[3] = Wi;
            QR.Xi[3].Values[0] = A;
            QR.Xi[3].Values[1] = B;
            QR.Xi[3].Values[2] = A;

            return QR;
        }
        public QuadratureRule_ND Make_QuadratureRule_Tetrahedron_Gaussian_Order_3()
        {
            QuadratureRule_ND QR = Initialize_3D_QuadratureRule(5);

            double Xi_0 = 1.0D / 4.0D;

            QR.wi[0] = -4.0D / 5.0D;
            QR.Xi[0].Values[0] = Xi_0;
            QR.Xi[0].Values[1] = Xi_0;
            QR.Xi[0].Values[2] = Xi_0;

            double Wi = 9.0D / 20.0D;
            double A = 1.0D / 3.0D;
            double B = 1.0D / 6.0D;

            QR.wi[1] = Wi;
            QR.Xi[1].Values[0] = A;
            QR.Xi[1].Values[1] = B;
            QR.Xi[1].Values[2] = B;

            QR.wi[2] = Wi;
            QR.Xi[2].Values[0] = B;
            QR.Xi[2].Values[1] = A;
            QR.Xi[2].Values[2] = B;

            QR.wi[3] = Wi;
            QR.Xi[3].Values[0] = B;
            QR.Xi[3].Values[1] = B;
            QR.Xi[3].Values[2] = A;

            QR.wi[4] = Wi;
            QR.Xi[4].Values[0] = B;
            QR.Xi[4].Values[1] = B;
            QR.Xi[4].Values[2] = B;

            return QR;
        }
        public QuadratureRule_ND Initialize_3D_QuadratureRule(int NumberOfIntegrationPoints)
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
