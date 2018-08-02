using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OOPTools_FEM_1D
{
    public static class GaussianQuadratureRule
    {
        /// <summary>
        /// Developed by: Mehrdad Negahban
        /// Date: 10/1/2012
        ///
        /// Purpose: Construct Gaussian Quadrature points and weights for different number of integration points
        /// Comments:
        ///
        /// Date modified:
        /// Modified by:
        /// Comments:
        /// </summary>
        public static QuadratureRule MakeGaussianQuadrature(int NIP)
        {
            return MakeGaussianQuadrature_NPoint(NIP);
            /*
            switch (NIP)
            {
                case 1:
                    return MakeGaussianQuadrature_1Point();
                case 2:
                    return MakeGaussianQuadrature_2Point();
                case 3:
                    return MakeGaussianQuadrature_3Point();
                case 4:
                    return MakeGaussianQuadrature_4Point();
                case 5:
                    return MakeGaussianQuadrature_5Point();
                default:
                    return MakeGaussianQuadrature_NPoint(NIP);
            }
             * */
        }
        private static QuadratureRule MakeGaussianQuadrature_1Point()
        {
            QuadratureRule GQ = new QuadratureRule();
            GQ.NIP = 1;
            GQ.wi = new double[1];
            GQ.Xi = new double[1];

            GQ.wi[0] = 2.0D;

            GQ.Xi[0] = 0.0D;

            return GQ;
        }
        private static QuadratureRule MakeGaussianQuadrature_2Point()
        {
            QuadratureRule GQ = new QuadratureRule();
            GQ.NIP = 2;
            GQ.wi = new double[2];
            GQ.Xi = new double[2];

            GQ.wi[0] = 1.0D;
            GQ.wi[1] = 1.0D;

            GQ.Xi[0] = -1.0D / Math.Sqrt(3.0D);
            GQ.Xi[1] = -GQ.Xi[0];

            return GQ;
        }
        private static QuadratureRule MakeGaussianQuadrature_3Point()
        {
            QuadratureRule GQ = new QuadratureRule();
            GQ.NIP = 3;
            GQ.wi = new double[3];
            GQ.Xi = new double[3];

            GQ.wi[0] = 8.0D/9.0D;
            GQ.wi[1] = 5.0D / 9.0D;
            GQ.wi[2] = GQ.wi[1];
            
            GQ.Xi[0] = 0.0D;
            GQ.Xi[1] = -Math.Sqrt(3.0D/5.0D);
            GQ.Xi[2] = -GQ.Xi[1];

            return GQ;
        }
        private static QuadratureRule MakeGaussianQuadrature_4Point()
        {
            QuadratureRule GQ = new QuadratureRule();
            GQ.NIP = 4;
            GQ.wi = new double[4];
            GQ.Xi = new double[4];

            GQ.wi[0] = (18.0D + Math.Sqrt(30.0D)) / 36.0D;
            GQ.wi[1] = GQ.wi[0];
            GQ.wi[2] = (18.0D - Math.Sqrt(30.0D)) / 36.0D;
            GQ.wi[3] = GQ.wi[2];

            GQ.Xi[0] = -Math.Sqrt((3.0D - 2.0D * Math.Sqrt(6.0D / 5.0D)) / 7.0D);
            GQ.Xi[1] = -GQ.Xi[0];
            GQ.Xi[2] = -Math.Sqrt((3.0D + 2.0D * Math.Sqrt(6.0D / 5.0D)) / 7.0D);
            GQ.Xi[3] = -GQ.Xi[2];

            return GQ;
        }
        private static QuadratureRule MakeGaussianQuadrature_5Point()
        {
            QuadratureRule GQ = new QuadratureRule();
            GQ.NIP = 5;
            GQ.wi = new double[5];
            GQ.Xi = new double[5];

            GQ.wi[0] = 128.0D/225.0D;
            GQ.wi[1] = (322.0D + 13.0D * Math.Sqrt(70.0D)) / 900.0D;
            GQ.wi[2] = GQ.wi[1];
            GQ.wi[3] = (322.0D - 13.0D * Math.Sqrt(70.0D)) / 900.0D;
            GQ.wi[4] = GQ.wi[3];

            GQ.Xi[0] = 0.0D;
            GQ.Xi[1] = -Math.Sqrt(5.0D - 2.0D * Math.Sqrt(10.0D / 7.0D)) / 3.0D;
            GQ.Xi[2] = -GQ.Xi[1];
            GQ.Xi[3] = -Math.Sqrt(5.0D + 2.0D * Math.Sqrt(10.0D / 7.0D)) / 3.0D;
            GQ.Xi[4] = -GQ.Xi[3];

            return GQ;
        }
        private static QuadratureRule MakeGaussianQuadrature_NPoint(int NumberOfPoints)
        {
            QuadratureRule GQ = new QuadratureRule();
            GQ.NIP = NumberOfPoints;
            GQ.wi = new double[NumberOfPoints];
            GQ.Xi = new double[NumberOfPoints];

            //Adapted from Numerical Recipes: 10/25/12
            const double EPS = 1.0E-14;
            double x1, x2;
            x1 = -1.0D;
            x2 = 1.0D;
            double z1, z, xm, xl, pp, p3, p2, p1;
            int n = NumberOfPoints;
            int m = (n + 1) / 2;
            xm = 0.5D * (x2 + x1);
            xl = 0.5D * (x2 - x1);
            double Rn = Convert.ToDouble(n);
            for (int i = 0; i < m; i++)
            {
                double Ri = Convert.ToDouble(i);
                z = Math.Cos(Math.PI * (Ri + 0.75D) / (Rn + 0.5D));
                do
                {
                    p1 = 1.0D;
                    p2 = 0.0D;
                    for (int j = 0; j < n; j++)
                    {
                        double Rj = Convert.ToDouble(j);
                        p3 = p2;
                        p2 = p1;
                        p1 = ((2.0D * Rj + 1.0D) * z * p2 - Rj * p3) / (Rj + 1.0D);
                    }
                    pp = Rn * (z * p1 - p2) / (z * z - 1.0D);
                    z1 = z;
                    z = z1 - p1 / pp;
                } while (Math.Abs(z - z1) > EPS);
                GQ.Xi[i] = xm - xl * z;
                GQ.Xi[n - 1 - i] = xm + xl * z;
                GQ.wi[i] = 2.0D * xl / ((1.0D - z * z) * pp * pp);
                GQ.wi[n - 1 - i] = GQ.wi[i];
            }
            return GQ;
        }
    }
}
