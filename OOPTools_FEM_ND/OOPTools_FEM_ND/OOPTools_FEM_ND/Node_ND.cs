using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OOPTools_Math;

namespace OOPTools_FEM_ND
{
    [Serializable]
    public class Node_ND
    {
        /// <summary>
        /// Developed by: Mehrdad Negahban
        /// Date: 11/12/2012
        ///
        /// Purpose: Node root for multi-dimensional problems
        /// Comments:
        ///
        /// Date modified:
        /// Modified by:
        /// Comments:
        /// </summary>
        public Vector X; //Node coordinate
        public Node_ND_Unknowns Unknowns;
        public static void Set_Node_DisplayValues(Node_ND[] Nodes, Vector NodeValues)
        {
            int NN = Nodes.Length;
            for (int i = 0; i < NN; i++)
            {
                Nodes[i].Unknowns.DisplayValue = NodeValues.Values[i];
            }
        }
        public static void Set_UnknownForNode(Node_ND[] Nodes, Vector GlobalUnknowns)
        {
            int NN = Nodes.Length;
            for (int i = 0; i < NN; i++)
            {
                Nodes[i].Unknowns.Set_UnknownFromGlobalUnknowns(GlobalUnknowns);
            }
        }
    }
    public class Node_ND_Unknowns
    {
        public double DisplayValue;
        public virtual void Set_UnknownFromGlobalUnknowns(Vector GlobalUnknowns)
        {
        }
    }

    [Serializable]
    public class Node_ND_Unknowns_ScalarField : Node_ND_Unknowns
    {
        public double Unknown; //Unknown value
        public int UnknownDoF; //Node number
        public override void Set_UnknownFromGlobalUnknowns(Vector GlobalUnknowns)
        {
                Unknown = GlobalUnknowns.Values[UnknownDoF];
        }
    }

    [Serializable]
    public class Node_ND_Unknowns_VectorField : Node_ND_Unknowns
    {
        public Vector Unknowns; //Node unknowns
        public int[] UnknownDoFs; //Unknown DoF
        public override void Set_UnknownFromGlobalUnknowns(Vector GlobalUnknowns)
        {
            int NU = UnknownDoFs.Length;
            for (int i = 0; i < NU; i++)
            {
                Unknowns.Values[i] = GlobalUnknowns.Values[UnknownDoFs[i]];
            }
        }
    }

    [Serializable]
    public class Node_ND_Unknowns_MultiPhysics : Node_ND_Unknowns
    {
        public Vector[] Unknowns;
        public int[][] UnknownDoFs;
        public override void Set_UnknownFromGlobalUnknowns(Vector GlobalUnknowns)
        {
            int NPhysics = UnknownDoFs.Length;
            for (int i = 0; i < NPhysics; i++)
            {
                int NDoFPerPhysics = UnknownDoFs[i].Length;
                for (int j = 0; j < NDoFPerPhysics; j++)
                {
                    Unknowns[i].Values[j] = GlobalUnknowns.Values[UnknownDoFs[i][j]];
                }
            }
        }
    }
}
