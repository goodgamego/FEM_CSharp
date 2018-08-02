using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OOPTools_Math;

namespace OOPTools_FEM_1D
{
    public class OneD_Solver
    {
        /// <summary>
        /// Developed by: Mehrdad Negahban
        /// Date: 10/21/2012
        ///
        /// Purpose: Root class for 1-D solvers
        /// Comments:
        ///
        /// Date modified:
        /// Modified by:
        /// Comments:
        /// </summary>
        public Node[] Nodes; //The nodes in the problem
        public Vector GetNodePositions()
        {
            int NN = Nodes.Length;
            Vector X = new Vector(NN);
            for (int i = 0; i < NN; i++)
            {
                X.Values[i] = Nodes[i].X;
            }
            return X;
        }
    }
}
