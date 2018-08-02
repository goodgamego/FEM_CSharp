using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OOPTools_Math;

namespace OOPTools_FEM_1D
{
    public class ParametricInterpolation_Lagrange : ParametricInterpolation
    {
        /// <summary>
        /// Developed by: Mehrdad Negahban
        /// Date: 10/10/2012
        ///
        /// Purpose:Provide Lagrange interpolation shape functions and their derivatives for one-dimensional parametric line elements on (-1,1)
        /// Comments: Linear interpolation of space
        ///
        /// Date modified:
        /// Modified by:
        /// Comments:
        /// </summary>
        public ParametricInterpolation_Lagrange(int nnpe)
        {
            NNPE = nnpe;
            Xi_start = -1.0D;
            L_Xi = 2.0D;
            NodeXi = CalculateNodeLocations();
        }
        public override Vector CalculateShapeFunctions(double Xi)
        {
            Vector N = new Vector(NNPE);
            for (int i = 0; i < NNPE; i++)
            {
                double Num = 1.0D;
                double Den = 1.0D;
                for (int j = 0; j < NNPE; j++)
                {
                    if (j != i)
                    {
                        Num *= Xi - NodeXi.Values[j];
                        Den *= NodeXi.Values[i] - NodeXi.Values[j];
                    }
                }
                N.Values[i] = Num / Den;
            }
            return N;
        }
        public override Vector CalculateDerivativesOfShapeFunctions_WRTXi(double Xi)
        {
            Vector dNdXi = new Vector(NNPE);
            for (int i = 0; i < NNPE; i++)
            {
                double Num = 0.0D;
                double Den = 1.0D;
                for (int j = 0; j < NNPE; j++)
                {
                    if (j != i)
                    {
                        Den *= NodeXi.Values[i] - NodeXi.Values[j];
                    }
                }
                for (int j = 0; j < NNPE; j++)
                {
                    if (j != i)
                    {
                        double aTerm = 1.0D;
                        for (int k = 0; k < NNPE; k++)
                        {
                            if (k != i)
                            {
                                if (k != j)
                                {
                                    aTerm *= Xi - NodeXi.Values[k];
                                }
                            }
                        }
                        Num += aTerm;
                    }
                }
                dNdXi.Values[i] = Num / Den;
            }
            return dNdXi;

        }
    }
}
