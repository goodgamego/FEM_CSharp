using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using OOPTools_Math;

namespace OOPTools_FEM_ND
{
    [Serializable]
    public class BoundaryElement_ND_Static : BoundaryElement_ND
    {
        /// <summary>
        /// Developed by: Mehrdad Negahban
        /// Date: 12/25/2012
        ///
        /// Purpose:    Multi-dimensional root element for static boundary
        /// Comments:   
        ///
        /// Date modified:
        /// Modified by: 
        /// Comments:
        /// </summary>
        public virtual void AdjustFAndK(ref Vector F, ref Matrix_Jagged K)
        {
        }
        public virtual void AdjustFAndK(ref Vector F, ref MatrixSparseLinkedList K)
        {
        }
    }
}
