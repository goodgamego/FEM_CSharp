using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OOPTools_Math;

namespace OOPTools_FEM_ND
{
    [Serializable]
    public class Element_ND_VectorField : Element_ND
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
        public Element_ND_VectorField()
        {
        }
        public Element_ND_VectorField(int NNPE)
        {
            ElementNodes = new Node_ND[NNPE];
        }

        public override void Assemble(ref Vector F, Vector Fe)
        {
            int Ip;
            int NNPE = ElementNodes.Length;
            for (int p = 0; p < NNPE; p++)
            {
                Node_ND_Unknowns_VectorField Unknown_p = (Node_ND_Unknowns_VectorField)ElementNodes[p].Unknowns;
                int NDFp = Unknown_p.UnknownDoFs.Length;
                for (int i = 0; i < NDFp; i++)
                {
                    Ip = Unknown_p.UnknownDoFs[i];
                    F.Values[Ip] += Fe.Values[NDFp * p + i];
                }
            }
        }
        public override void Assemble(ref Matrix_Jagged K, Matrix_Jagged Ke)
        {
            int Ip, Iq;
            int NNPE = ElementNodes.Length;
            for (int p = 0; p < NNPE; p++)
            {
                Node_ND_Unknowns_VectorField Unknown_p = (Node_ND_Unknowns_VectorField)ElementNodes[p].Unknowns;
                int NDFp = Unknown_p.UnknownDoFs.Length;
                for (int i = 0; i < NDFp; i++)
                {
                    Ip = Unknown_p.UnknownDoFs[i];
                    for (int q = 0; q < NNPE; q++)
                    {
                        Node_ND_Unknowns_VectorField Unknown_q = (Node_ND_Unknowns_VectorField)ElementNodes[q].Unknowns;
                        int NDFq = Unknown_q.UnknownDoFs.Length;
                        for (int j = 0; j < NDFq; j++)
                        {
                            Iq = Unknown_q.UnknownDoFs[j];
                            K.Values[Ip][Iq] += Ke.Values[NDFp * p + i][NDFq * q + j];
                        }
                    }
                }

            }

        }
        public override void Assemble(ref MatrixSparseLinkedList K, Matrix_Jagged Ke)
        {
            int Ip, Iq;
            int NNPE = ElementNodes.Length;
            for (int p = 0; p < NNPE; p++)
            {
                Node_ND_Unknowns_VectorField Unknown_p = (Node_ND_Unknowns_VectorField)ElementNodes[p].Unknowns;
                int NDFp = Unknown_p.UnknownDoFs.Length;
                for (int i = 0; i < NDFp; i++)
                {
                    Ip = Unknown_p.UnknownDoFs[i];
                    for (int q = 0; q < NNPE; q++)
                    {
                        Node_ND_Unknowns_VectorField Unknown_q = (Node_ND_Unknowns_VectorField)ElementNodes[q].Unknowns;
                        int NDFq = Unknown_q.UnknownDoFs.Length;
                        for (int j = 0; j < NDFq; j++)
                        {
                            Iq = Unknown_q.UnknownDoFs[j];
                            K.AddToMatrixElement(Ip, Iq, Ke.Values[NNPE * p + i][NNPE * q + j]);
                        }
                    }
                }

            }

        }
        public Vector[] Get_ElementNodal_U(Vector GlobalU)
        {
            int NNPE = ElementNodes.Length;
            Vector[] Ue = new Vector[NNPE];
            for (int p = 0; p < NNPE; p++)
            {
                Node_ND_Unknowns_VectorField Unknown_p = (Node_ND_Unknowns_VectorField)ElementNodes[p].Unknowns;
                int NDFp = Unknown_p.UnknownDoFs.Length;
                for (int i = 0; i < NDFp; i++)
                {
                    Ue[p].Values[i] = GlobalU.Values[Unknown_p.UnknownDoFs[i]];
                }
            }
            return Ue;
        }
        public Vector[] Get_ElementNodal_U()
        {
            int NNPE = ElementNodes.Length;
            Vector[] Ue = new Vector[NNPE];
            for (int p = 0; p < NNPE; p++)
            {
                Node_ND_Unknowns_VectorField Unknown_p = (Node_ND_Unknowns_VectorField)ElementNodes[p].Unknowns;
                Ue[p]=Unknown_p.Unknowns;
            }
            return Ue;
        }
        public Vector Calculate_U(Vector Xi)
        {
            Vector[] Ue = Get_ElementNodal_U(Element_ND.U);
            return Interpolator.Interpolate_Variable(Xi, Ue);
        }
        public Matrix_Jagged[] Calculate_ElementNodal_DUDX()
        {
            int NNPE = ElementNodes.Length;
            Matrix_Jagged[] DuDX = new Matrix_Jagged[NNPE];
            Vector[] Ue = Get_ElementNodal_U(Element_ND.U);
            Vector[] Xip = Interpolator.NodeXi;
            for (int p = 0; p < NNPE; p++)
            {
                DuDX[p] = Interpolator.Interpolate_DerivativeOfVariable_WRTX(Xip[p], Ue, ElementNodes);
            }
            return DuDX;
        }

        public virtual Matrix_Jagged Calculate_DUDX(Vector Xi)
        {
            Vector[] Ue = Get_ElementNodal_U(Element_ND.U);
            return Interpolator.Interpolate_DerivativeOfVariable_WRTX(Xi, Ue, ElementNodes);
        }
    }
}
