using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using OOPTools_Math;

namespace OOPTools_FEM_ND
{
    [Serializable]
    public class BoundaryElement_ND_Transient : BoundaryElement_ND
    {
        /// <summary>
        /// Developed by: Mehrdad Negahban
        /// Date: 12/25/2012
        ///
        /// Purpose:    Multi-dimensional root element for transient boundary
        /// Comments:   
        ///
        /// Date modified:
        /// Modified by: 
        /// Comments:
        /// </summary>
        public virtual void AdjustFhat(double Time, ref Vector Fhat)
        {
        }
        public virtual void AdjustChat(double Time, ref Matrix_Jagged Chat)
        {
        }
        public virtual void AdjustChat(double Time, ref MatrixSparseLinkedList Chat)
        {
        }
        public virtual void AdjustMhat(double Time, ref Matrix_Jagged Mhat)
        {
        }
        public virtual void AdjustMhat(double Time, ref MatrixSparseLinkedList Mhat)
        {
        }
        public virtual void AdjustF(double Time, ref Vector F)
        {
        }
        public virtual void AdjustK(double Time, ref Matrix_Jagged K)
        {
        }
        public virtual void AdjustK(double Time, ref MatrixSparseLinkedList K)
        {
        }
        public virtual void AdjustC(double Time, ref Matrix_Jagged C)
        {
        }
        public virtual void AdjustC(double Time, ref MatrixSparseLinkedList C)
        {
        }
        public virtual void AdjustM(double Time, ref Matrix_Jagged M)
        {
        }
        public virtual void AdjustM(double Time, ref MatrixSparseLinkedList M)
        {
        }
        public virtual void AdjustU(double Time, ref Vector U)
        {
        }
        public virtual void AdjustV(double Time, ref Vector V)
        {
        }
    }
}
