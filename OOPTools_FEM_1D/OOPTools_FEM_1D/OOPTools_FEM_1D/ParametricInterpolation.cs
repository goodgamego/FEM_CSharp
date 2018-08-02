using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OOPTools_Math;

namespace OOPTools_FEM_1D
{
    public class ParametricInterpolation
    {
        /// <summary>
        /// Developed by: Mehrdad Negahban
        /// Date: 10/10/2012
        ///
        /// Purpose:Provide interpolation shape functions and their derivatives for one-dimensional parametric line elements
        /// Comments:   Linear interpolation of space
        ///             Xi is the parametric variable
        ///             X is the real space variable
        ///
        /// Date modified:
        /// Modified by:
        /// Comments:
        /// </summary>
        public int NNPE; //Number of nodes per element
        public Vector NodeXi; //Node locations
        public double Xi_start, L_Xi; //Parametric domain (Xi_start, Xi_start+L_Xi)
        public ParametricInterpolation()
        {
        }
        public ParametricInterpolation(int nnpe)
        {
            NNPE = nnpe;
            NodeXi = CalculateNodeLocations();
        }
        public virtual Vector CalculateShapeFunctions(double Xi)
        {   //Override with method to calculate shape functions 
            Vector N = new Vector(NNPE);
            return N;
        }
        public virtual Vector CalculateDerivativesOfShapeFunctions_WRTXi(double Xi)
        {   //Override with method to calculate derivative of shape functions with respect to Xi
            Vector dNdXi = new Vector(NNPE);
            return dNdXi;
        }
        public virtual Vector CalculateDerivativesOfShapeFunctions_WRTX(double Xi, double Le)
        {
            return CalculateDerivativesOfShapeFunctions_WRTXi(Xi) * (L_Xi/ Le);
        }
        public virtual double InterpolateVariable(double Xi, Vector Variable_NodalValues)
        {
            Vector N = CalculateShapeFunctions(Xi);
            return InterpolateVariable(Variable_NodalValues, N);
        }
        public virtual double InterpolateVariable(Vector Variable_NodalValues, Vector N)
        {
            return Vector.DotProduct(Variable_NodalValues, N);
        }
        public virtual double InterpolateDerivativeOfVariable_WRTXi(double Xi, Vector Variable_NodalValues)
        {
            Vector dNdXi = CalculateDerivativesOfShapeFunctions_WRTXi(Xi);
            return InterpolateDerivativeOfVariable_WRTXi(Variable_NodalValues, dNdXi);
        }
        public virtual double InterpolateDerivativeOfVariable_WRTXi(Vector Variable_NodalValues, Vector dNdXi)
        {
            return Vector.DotProduct(Variable_NodalValues, dNdXi);
        }
        public virtual double InterpolateDerivativeOfVariable_WRTX(double Xi, double Le, Vector Variable_NodalValues)
        {
            Vector dNdXi = CalculateDerivativesOfShapeFunctions_WRTXi(Xi);
            return InterpolateDerivativeOfVariable_WRTX(Le, Variable_NodalValues, dNdXi);
        }
        public virtual double InterpolateDerivativeOfVariable_WRTX(double Le, Vector Variable_NodalValues, Vector dNdXi)
        {
            return Vector.DotProduct(Variable_NodalValues, dNdXi) * L_Xi / Le;
        }
        public virtual void CalculateAndSetElementNodeXForInterpolation(double X_start, double X_end, ref Node[] ElementNodes)
        {   //Calculate linear interpolation of node locations and setup ElementNodes
            for (int i = 0; i < NNPE; i++)
            {
                ElementNodes[i].X = Calculate_X(X_start, X_end, NodeXi.Values[i]);
            }
        }
        public Vector CalculateNodeX(Node[] ElementNodes)
        {
            int nnpe = ElementNodes.Length;
            Vector X = new Vector(nnpe);
            for (int i = 0; i < nnpe; i++)
            {
                X.Values[i] = Calculate_X(ElementNodes, NodeXi.Values[i]);
            }
            return X;
        }
        public virtual double Calculate_X(double X_start, double X_end, double Xi)
        {
            return X_start + (X_end - X_start) * (Xi - Xi_start) / L_Xi;
        }
        public virtual double Calculate_X(Node[] ElementNodes, double Xi)
        {
            return Calculate_X(ElementNodes[0].X, ElementNodes[NNPE - 1].X, Xi);
        }
        public virtual Vector CalculateNodeLocations()
        {   //Uniform node spacing
            NodeXi = new Vector(NNPE);
            double DXi = L_Xi / Convert.ToDouble(NNPE - 1);
            NodeXi.Values[0] = Xi_start;
            for (int p = 1; p < NNPE; p++)
            {
                NodeXi.Values[p] = NodeXi.Values[p - 1] + DXi;
            }
            return NodeXi;
        }
    }
}
