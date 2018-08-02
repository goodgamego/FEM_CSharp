using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using OOPTools_Math;
using OOPTools_FEM_1D;

namespace OOPTools_FEM_ND
{
    [Serializable]
    public class ParametricInterpolation_ND_Line : ParametricInterpolation_ND
    {
    /// <summary>
        /// Developed by: Mehrdad Negahban
        /// Date: 11/12/2012
        ///
        /// Purpose:    Provide interpolation shape functions and their derivatives for 2- or 3-D Line
        /// Comments:   Xi is the parametric variable of space
        ///             X is the real variable of space
        ///             
        /// Date modified:
        /// Modified by:
        /// Comments:
        /// </summary>
        public int NNPE_l;
        ParametricInterpolation_Lagrange ParametricInterpolation_1D_L;
        public ParametricInterpolation_ND_Line()
        {
        }
        public ParametricInterpolation_ND_Line(int nnpe_l)
        {
            NNPE_l = nnpe_l;
            NNPE =  NNPE_l;
            ParametricInterpolation_1D_L = new ParametricInterpolation_Lagrange(NNPE_l);
            NodeXi = Calculate_ElementNodal_Xi();
        }
        public override Vector[] Calculate_ElementNodal_Xi()
        {
            Vector[] nodeXi = new Vector[NNPE];
            double DX0 = 2.0D / Convert.ToDouble(NNPE_l - 1);
            int Index = 0;
            for (int i = 0; i < NNPE_l; i++)
            {
                nodeXi[Index] = new Vector(1);
                nodeXi[Index].Values[0] = -1.0D + Convert.ToDouble(i) * DX0;
                Index++;
            }
            return nodeXi;
        }
        public override Vector Calculate_ShapeFunctions(Vector Xi)
        {
            Vector N_1D_L = ParametricInterpolation_1D_L.CalculateShapeFunctions(Xi.Values[0]);
            return N_1D_L;
        }
        public override Vector[] Calculate_DerivativesOfShapeFunctions_WRTXi(Vector Xi, out Vector N)
        {
            N = Calculate_ShapeFunctions(Xi);
            Vector[] DNDXi = new Vector[NNPE];
            Vector DNDXi_1D_L = ParametricInterpolation_1D_L.CalculateDerivativesOfShapeFunctions_WRTXi(Xi.Values[0]);
            int Index = 0;
            for (int i = 0; i < NNPE_l; i++)
            {
                DNDXi[Index] = new Vector(1);
                DNDXi[Index].Values[0] = DNDXi_1D_L.Values[i];
                Index++;
            }

            return DNDXi;
        }
    }
}
