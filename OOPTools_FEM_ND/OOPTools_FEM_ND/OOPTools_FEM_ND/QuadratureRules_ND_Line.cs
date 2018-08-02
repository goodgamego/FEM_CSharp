using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OOPTools_FEM_1D;
using OOPTools_Math;

namespace OOPTools_FEM_ND
{
    [Serializable]
    public class QuadratureRules_ND_Line
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
        public static QuadratureRule_ND Make_QuadratureRule_Gaussian_NPoint(int NIP)
        {
            QuadratureRule QR = GaussianQuadratureRule.MakeGaussianQuadrature(NIP);
            QuadratureRule_ND QR_ND = new QuadratureRule_ND();
            QR_ND.NIP = NIP;
            QR_ND.wi = QR.wi;
            QR_ND.Xi = new Vector[NIP];
            for (int i = 0; i < NIP; i++)
            {
                QR_ND.Xi[i] = new Vector(1);
                QR_ND.Xi[i].Values[0] = QR.Xi[i];
            }
            return QR_ND;
        }
    }
}
