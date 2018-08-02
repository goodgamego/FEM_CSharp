using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OOPTools_Math;

namespace OOPTools_FEM_ND
{
    [Serializable]
    public class Element_ND_MultiPhysics : Element_ND
    {
                /// <summary>
        /// Developed by: Mehrdad Negahban
        /// Date: 11/12/2012
        ///
        /// Purpose:    Multi-dimensional root element for vector field problems
        /// Comments:   Uses parametric interpolation 
        ///             Uses quadrature integration
        ///
        /// Date modified:
        /// Modified by: 
        /// Comments:
        /// </summary>
        int[] PhysicsUsed; //index of Node Physics used
        public Element_ND_MultiPhysics()
        {
        }
        public Element_ND_MultiPhysics(int NNPE, int[] physicsUsed)
        {
            ElementNodes = new Node_ND[NNPE];
            PhysicsUsed = physicsUsed;
        }
        public virtual int Get_TotalNumberOfDoFPerNode(out int[] DoFPerPhysics)
        {
            DoFPerPhysics = Get_NumberOfDoFPerNode_PerPhysics();
            int NP = DoFPerPhysics.Length;
            int TotalDoF =0;
            for(int i=0; i<NP; i++)
            {
                TotalDoF += DoFPerPhysics[i];
            }
            return TotalDoF;
        }
        public virtual int[] Get_StartIndexForEachPhysics(out int NumberOfPhysics, out int TotalDoFPerNode, out int[] DoFPerPhysics)
        {
            TotalDoFPerNode = Get_TotalNumberOfDoFPerNode(out DoFPerPhysics);
            NumberOfPhysics = DoFPerPhysics.Length;
            int[] StartIndexPerPhysics = new int[NumberOfPhysics];
            StartIndexPerPhysics[0] =0;
            for(int i=1; i<NumberOfPhysics; i++)
            {
                StartIndexPerPhysics[i]=StartIndexPerPhysics[i-1]+DoFPerPhysics[i-1];
            }
            return StartIndexPerPhysics;
            
        }
        public virtual int[] Get_NumberOfDoFPerNode_PerPhysics()
        {
            int[] DoFPerPhysics = new int[0];//Add physics structure in unknown DoF
            return DoFPerPhysics;
        }
        public override void Assemble(ref Vector F, Vector Fe)
        {
            int Ip, I1p, I1pi, I1;
            int NNPE = ElementNodes.Length;

            int NumberOFPhysics, TotalDoFPerNode;
            int[] DofPerPhysics;
            int[] StartIndexPerPhysics = Get_StartIndexForEachPhysics(out NumberOFPhysics, out TotalDoFPerNode, out DofPerPhysics);
            for (int p = 0; p < NNPE; p++)
            {
                Node_ND_Unknowns_MultiPhysics Unknowns_p = (Node_ND_Unknowns_MultiPhysics)ElementNodes[p].Unknowns;
                I1p = TotalDoFPerNode * p;
                for (int i = 0; i < NumberOFPhysics; i++)
                {
                    I1pi = I1p + StartIndexPerPhysics[i];
                    for (int j = 0; j < DofPerPhysics[i]; j++)
                    {
                        I1 = I1pi + j;
                        Ip = Unknowns_p.UnknownDoFs[PhysicsUsed[i]][j];
                        F.Values[Ip] += Fe.Values[I1];
                    }
                }
            }
        }
        public override void Assemble(ref Matrix_Jagged K, Matrix_Jagged Ke)
        {
            int Ip, Iq, I1p, I1pi, I1, I2q, I2qk, I2;
            int NNPE = ElementNodes.Length;

            int NumberOFPhysics, TotalDoFPerNode;
            int[] DofPerPhysics;
            int[] StartIndexPerPhysics = Get_StartIndexForEachPhysics(out NumberOFPhysics, out TotalDoFPerNode, out DofPerPhysics);

            for (int p = 0; p < NNPE; p++)
            {
                Node_ND_Unknowns_MultiPhysics Unknowns_p = (Node_ND_Unknowns_MultiPhysics)ElementNodes[p].Unknowns;
                I1p =TotalDoFPerNode * p;
                for (int i = 0; i < NumberOFPhysics; i++)
                {
                    I1pi = I1p + StartIndexPerPhysics[i];
                    for (int j = 0; j < DofPerPhysics[i]; j++)
                    {
                        I1 = I1pi + j;
                        Ip = Unknowns_p.UnknownDoFs[PhysicsUsed[i]][j];
                        for (int q = 0; q < NNPE; q++)
                        {
                            Node_ND_Unknowns_MultiPhysics Unknowns_q = (Node_ND_Unknowns_MultiPhysics)ElementNodes[q].Unknowns;
                            I2q = TotalDoFPerNode *q;
                            for (int k = 0; k < NumberOFPhysics; k++)
                            {
                                I2qk = I2q + StartIndexPerPhysics[k];
                                for (int l = 0; l < DofPerPhysics[k]; l++)
                                {
                                    I2 = I2qk + l;
                                    Iq = Unknowns_q.UnknownDoFs[PhysicsUsed[k]][l];
                                    K.Values[Ip][Iq] += Ke.Values[I1][I2];
                                }
                            }
                        }
                    }
                }
            }

        }
        public override void Assemble(ref MatrixSparseLinkedList K, Matrix_Jagged Ke)
        {
            int Ip, Iq, I1p, I1pi, I1, I2q, I2qk, I2;
            int NNPE = ElementNodes.Length;

            int NumberOFPhysics, TotalDoFPerNode;
            int[] DofPerPhysics;
            int[] StartIndexPerPhysics = Get_StartIndexForEachPhysics(out NumberOFPhysics, out TotalDoFPerNode, out DofPerPhysics);

            for (int p = 0; p < NNPE; p++)
            {
                Node_ND_Unknowns_MultiPhysics Unknowns_p = (Node_ND_Unknowns_MultiPhysics)ElementNodes[p].Unknowns;
                I1p = TotalDoFPerNode * p;
                for (int i = 0; i < NumberOFPhysics; i++)
                {
                    I1pi = I1p + StartIndexPerPhysics[i];
                    for (int j = 0; j < DofPerPhysics[i]; j++)
                    {
                        I1 = I1pi + j;
                        Ip = Unknowns_p.UnknownDoFs[PhysicsUsed[i]][j];
                        for (int q = 0; q < NNPE; q++)
                        {
                            Node_ND_Unknowns_MultiPhysics Unknowns_q = (Node_ND_Unknowns_MultiPhysics)ElementNodes[q].Unknowns;
                            I2q = TotalDoFPerNode * q;
                            for (int k = 0; k < NumberOFPhysics; k++)
                            {
                                I2qk = I2q + StartIndexPerPhysics[k];
                                for (int l = 0; l < DofPerPhysics[k]; l++)
                                {
                                    I2 = I2qk + l;
                                    Iq = Unknowns_q.UnknownDoFs[PhysicsUsed[k]][l];
                                    K.AddToMatrixElement(Ip, Iq, Ke.Values[I1][I2]);
                                }
                            }
                        }
                    }
                }
            }

        }
    }
}
