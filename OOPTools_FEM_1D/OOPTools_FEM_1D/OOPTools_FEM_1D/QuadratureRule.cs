using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OOPTools_FEM_1D
{
    public class QuadratureRule
    {
        /// <summary>
        /// Developed by: Mehrdad Negahban
        /// Date: 10/1/2012
        ///
        /// Purpose: Data structure to store one dimensional  quadratures
        /// Comments:
        ///
        /// Date modified:
        /// Modified by:
        /// Comments:
        /// </summary>
        public int NIP; //Number of integration points
        public double[] wi; //Integration weights
        public double[] Xi; //Integration points
    }
}
