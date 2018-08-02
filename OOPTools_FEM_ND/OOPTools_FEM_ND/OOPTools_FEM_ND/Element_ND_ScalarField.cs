using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OOPTools_Math;

namespace OOPTools_FEM_ND
{
    [Serializable]
    public class Element_ND_ScalarField : Element_ND
    {
        /// <summary>
        /// Developed by: Mehrdad Negahban
        /// Date: 11/12/2012
        ///
        /// Purpose:    Multi-dimensional root element for scalar field problems
        /// Comments:   Uses parametric interpolation 
        ///             Uses quadrature integration
        ///
        /// Date modified:
        /// Modified by: 
        /// Comments:
        /// </summary>
        public Element_ND_ScalarField()
        {
        }
        public Element_ND_ScalarField(int NNPE)
        {
            ElementNodes = new Node_ND[NNPE];
        }
        
        public override void Assemble(ref Vector F, Vector Fe)
        {
            int NNPE = ElementNodes.Length;
            for (int p = 0; p < NNPE; p++)
            {
                Node_ND_Unknowns_ScalarField TheUnknowns = (Node_ND_Unknowns_ScalarField)ElementNodes[p].Unknowns;
                F.Values[TheUnknowns.UnknownDoF] += Fe.Values[p];
            }
        }
        public override void Assemble(ref Matrix_Jagged K, Matrix_Jagged Ke)
        {
            int Ip, Iq;
            int NNPE = ElementNodes.Length;
            for (int p = 0; p < NNPE; p++)
            {
                Node_ND_Unknowns_ScalarField TheUnknowns_p = (Node_ND_Unknowns_ScalarField)ElementNodes[p].Unknowns;
                Ip = TheUnknowns_p.UnknownDoF;
                for (int q = 0; q < NNPE; q++)
                {
                    Node_ND_Unknowns_ScalarField TheUnknowns_q = (Node_ND_Unknowns_ScalarField)ElementNodes[q].Unknowns;
                    Iq = TheUnknowns_q.UnknownDoF;
                    K.Values[Ip][Iq] += Ke.Values[p][q];
                }
            }
        }
        public override void Assemble(ref MatrixSparseLinkedList K, Matrix_Jagged Ke)
        {
            int Ip, Iq;
            int NNPE = ElementNodes.Length;
            for (int p = 0; p < NNPE; p++)
            {
                Node_ND_Unknowns_ScalarField TheUnknowns_p = (Node_ND_Unknowns_ScalarField)ElementNodes[p].Unknowns;
                Ip = TheUnknowns_p.UnknownDoF;
                for (int q = 0; q < NNPE; q++)
                {
                    Node_ND_Unknowns_ScalarField TheUnknowns_q = (Node_ND_Unknowns_ScalarField)ElementNodes[q].Unknowns;
                    Iq = TheUnknowns_q.UnknownDoF;
                    K.AddToMatrixElement(Ip, Iq, Ke.Values[p][q]);
                }
            }
        }
              

        public double[] Get_ElementNodal_U(Vector GlobalUnknowns)
        {
            int NNPE = ElementNodes.Length;
            double[] Ue = new double[NNPE];
            int Ip;
            for (int p = 0; p < NNPE; p++)
            {
                Node_ND_Unknowns_ScalarField TheUnknowns_p = (Node_ND_Unknowns_ScalarField)ElementNodes[p].Unknowns;
                Ip = TheUnknowns_p.UnknownDoF;
                Ue[p] = GlobalUnknowns.Values[Ip];
            }
            return Ue;
        }
        public double[] Get_ElementNodal_U()
        {
            int NNPE = ElementNodes.Length;
            double[] Ue = new double[NNPE];
            for (int p = 0; p < NNPE; p++)
            {
                Node_ND_Unknowns_ScalarField TheUnknowns_p = (Node_ND_Unknowns_ScalarField)ElementNodes[p].Unknowns;
                Ue[p] = TheUnknowns_p.Unknown;
            }
            return Ue;
        }
        public double Calculate_U(Vector Xi)
        {
            double[] Ue = Get_ElementNodal_U();
            return Interpolator.Interpolate_Variable(Xi, Ue);
        }        
        public virtual Vector Calculate_DUDX(Vector Xi)
        {
            double[] Ue = Get_ElementNodal_U();
            return Interpolator.Interpolate_DerivativeOfVariable_WRTX(Xi, Ue, ElementNodes);
        }
        public virtual Vector[] Calculate_ElementNodal_DUDX()
        {
            double[] Ue = Get_ElementNodal_U();
            Vector[] Xi = Interpolator.Calculate_ElementNodal_Xi();
            int NNPE = Xi.Length;
            Vector[] DUDX = new Vector[NNPE];
            for (int i = 0; i < NNPE; i++)
            {
                DUDX[i] = Interpolator.Interpolate_DerivativeOfVariable_WRTX(Xi[i], Ue, ElementNodes);
            }
            return DUDX;
        }
        public Vector Get_ElementNodal_Values(Node_ND[] ElementNodes, ref Vector ValuesAtAllNodes)
        {
            int NNPE = ElementNodes.Length;
            Vector ValuesAtTheseNodes = new Vector(NNPE);
            for (int p = 0; p < NNPE; p++)
            {
                Node_ND_Unknowns_ScalarField TheUnknowns_p = (Node_ND_Unknowns_ScalarField)ElementNodes[p].Unknowns;
                ValuesAtTheseNodes.Values[p] = ValuesAtAllNodes.Values[TheUnknowns_p.UnknownDoF];
            }
            return ValuesAtTheseNodes;
        }
        public void Set_ElementNodal_DisplayValues(Node_ND[] ElementNodes, ref Vector ValuesAtAllNodes)
        {
            int NNPE = ElementNodes.Length;
            Vector ValuesAtTheseNodes = new Vector(NNPE);
            for (int p = 0; p < NNPE; p++)
            {
                Node_ND_Unknowns_ScalarField TheUnknowns_p = (Node_ND_Unknowns_ScalarField)ElementNodes[p].Unknowns;
                ElementNodes[p].Unknowns.DisplayValue = ValuesAtAllNodes.Values[TheUnknowns_p.UnknownDoF];
            }
        }
    }
}
