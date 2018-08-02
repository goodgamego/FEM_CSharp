using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OOPTools_Math;

namespace OOPTools_FEM_ND
{
    [Serializable]
    public class BoundaryElement_ND : Element_ND
    {
        /// <summary>
        /// Developed by: Mehrdad Negahban
        /// Date: 11/12/2012
        ///
        /// Purpose:    Multi-dimensional root element for boundary
        /// Comments:   Uses parametric interpolation 
        ///             Uses quadrature integration
        ///
        /// Date modified:
        /// Modified by: 
        /// Comments:
        /// </summary>
        public double Calculate_NDLine_DsDXi(Vector Xi, Node_ND[] ElementNoes, out Vector N)
        {
            //Get line length ratio between real line and parametric line
            Matrix_Jagged DXDXi;
            Interpolator.Calculate_N_DXDXi(Xi, ElementNodes, out N, out DXDXi);
            double DsDXi = Math.Sqrt((DXDXi.Transpose() * DXDXi).Trace());//Calculate change of variable 
            return DsDXi;
        }
        public double Calculate_NDArea_DADAo(Vector Xi, Node_ND[] ElementNoes, out Vector N)
        {
            //Get area ratio between real area and parametric area
            Matrix_Jagged DXDXi;
            Interpolator.Calculate_N_DXDXi(Xi, ElementNodes, out N, out DXDXi);
            Vector DX_1 = DXDXi.GetColumn(0);
            Vector DX_2 = DXDXi.GetColumn(1);
            double DAXDAo = Vector.CrossProduct(DX_1, DX_2).Magnitude();
            return DAXDAo;
        }
    }
}
