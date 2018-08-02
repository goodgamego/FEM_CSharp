using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OOPTools_Math;

namespace OOPTools_FEM_ND
{
    [Serializable]
    public class ParametricInterpolation_ND
    {
        /// <summary>
        /// Developed by: Mehrdad Negahban
        /// Date: 11/12/2012
        ///
        /// Purpose:    Provide interpolation shape functions and their derivatives for multi-dimensional elements
        /// Comments:   Xi is the parametric variable of space
        ///             X is the real variable of space
        ///             
        /// Date modified:
        /// Modified by:
        /// Comments:
        /// </summary>
        public int NNPE;
        public Vector[] NodeXi; //Node locations
        public ParametricInterpolation_ND()
        {
        }
        public ParametricInterpolation_ND(int NumberOfNodesPerElement)
        {
            NNPE = NumberOfNodesPerElement;
            NodeXi = new Vector[NNPE];
        }
        public virtual Vector[] Calculate_ElementNodal_Xi()
        {
            //Override with method to calculate parametric node locations
            return NodeXi;
        }
        public virtual Vector Calculate_ShapeFunctions(Vector Xi)
        {   //Override with method to calculate shape functions 
            Vector N = new Vector(NodeXi.Length);
            return N;
        }
        public virtual Vector[] Calculate_DerivativesOfShapeFunctions_WRTXi(Vector Xi)
        {   //Override with method to calculate derivative of shape functions with respect to Xi
            Vector[] dNdXi = new Vector[0];
            return dNdXi;
        }
        public virtual Vector[] Calculate_DerivativesOfShapeFunctions_WRTXi(Vector Xi, out Vector N)
        {   //Override with method to calculate derivative of shape functions with respect to Xi
            N = Calculate_ShapeFunctions(Xi); 
            Vector[] dNdXi = new Vector[0];
            return dNdXi;
        }
        public virtual Matrix_Jagged Calculate_DerivativesOfShapeFunctions_WRTXi_Matrix_Jagged(Vector Xi)
        {   //Override with method to calculate derivative of shape functions with respect to Xi
            Vector[] dNdXi = Calculate_DerivativesOfShapeFunctions_WRTXi(Xi);
            Matrix_Jagged DNDXi = new Matrix_Jagged(dNdXi);
            return DNDXi.Transpose();
        }
        public virtual Matrix_Jagged Calculate_DerivativesOfShapeFunctions_WRTXi_Matrix_Jagged(Vector Xi, out Vector N)
        {   //Override with method to calculate derivative of shape functions with respect to Xi
            N = Calculate_ShapeFunctions(Xi);
            Vector[] dNdXi = Calculate_DerivativesOfShapeFunctions_WRTXi(Xi);
            Matrix_Jagged DNDXi = new Matrix_Jagged(dNdXi);
            return DNDXi.Transpose();
        }
        #region Interpolate function
        public virtual double Interpolate_Variable(Vector Xi, double[] Variable_NodalValues)
        {
            Vector N = Calculate_ShapeFunctions(Xi);
            return Interpolate_Variable(Variable_NodalValues, N);
        }
        public virtual double Interpolate_Variable(double[] Variable_NodalValues, Vector N)
        {
            return Vector.DotProduct(Variable_NodalValues, N);
        }
        public virtual Vector Interpolate_Variable(Vector Xi, Vector[] Variable_NodalValues)
        {
            Vector N = Calculate_ShapeFunctions(Xi);
            return Interpolate_Variable(Variable_NodalValues, N);
        }
        public virtual Vector Interpolate_Variable(Vector[] Variable_NodalValues, Vector N)
        {
            int nnpe = N.Values.Length;
            Vector Value = Variable_NodalValues[0] * N.Values[0];
            for (int p = 1; p < nnpe; p++)
            {
                Value += Variable_NodalValues[p] * N.Values[p];
            }
            return Value;
        }
        public virtual Matrix Interpolate_Variable(Vector Xi, Matrix[] Variable_NodalValues)
        {
            Vector N = Calculate_ShapeFunctions(Xi);
            return Interpolate_Variable(Variable_NodalValues, N);
        }
        public virtual Matrix Interpolate_Variable(Matrix[] Variable_NodalValues, Vector N)
        {
            int nnpe = N.Values.Length;
            Matrix Value = Variable_NodalValues[0] * N.Values[0];
            for (int p = 1; p < nnpe; p++)
            {
                Value += Variable_NodalValues[p] * N.Values[p];
            }
            return Value;
        }
        #endregion 

        #region Interpolate derivative with respect to Xi
        public virtual Vector Interpolate_DerivativeOfVariable_WRTXi(Vector Xi, double[] Variable_NodalValues)
        {
            Vector[] dNdXi = Calculate_DerivativesOfShapeFunctions_WRTXi(Xi);
            return Interpolate_DerivativeOfVariable_WRTXi(Variable_NodalValues, dNdXi);
        }
        public virtual Vector Interpolate_DerivativeOfVariable_WRTXi(double[] Variable_NodalValues, Vector[] dNdXi)
        {
            int nnpe = dNdXi.Length;
            Vector Value = Variable_NodalValues[0] * dNdXi[0];
            for (int p = 1; p < nnpe; p++)
            {
                Value += Variable_NodalValues[p] * dNdXi[p];
            }
            return Value;
        }
        public virtual Matrix_Jagged Interpolate_DerivativeOfVariable_WRTXi(Vector Xi, Vector[] Variable_NodalValues)
        {
            Vector[] dNdXi = Calculate_DerivativesOfShapeFunctions_WRTXi(Xi);
            return Interpolate_DerivativeOfVariable_WRTXi(Variable_NodalValues, dNdXi);
        }
        public virtual Matrix_Jagged Interpolate_DerivativeOfVariable_WRTXi(Vector[] Variable_NodalValues, Vector[] dNdXi)
        {
            int nnpe = dNdXi.Length;
            Matrix_Jagged Value = Matrix_Jagged.TensorProduct(Variable_NodalValues[0], dNdXi[0]);
            for (int p = 1; p < nnpe; p++)
            {
                Value += Matrix_Jagged.TensorProduct(Variable_NodalValues[p] , dNdXi[p]);
            }
            return Value;
        }
        #endregion

        #region Calculate derivative with respect to X
        public virtual Matrix_Jagged Calculate_DerivativesOfShapeFunctions_WRTX(Vector Xi,  Node_ND[] ElementNodes)
        {
            Vector[] dNdXi;
            Matrix_Jagged DXDXi, DXiDX;
            Calculate_dNdXi_DXDXi_DXiDX(Xi, ElementNodes, out dNdXi, out DXDXi, out DXiDX);

            Matrix_Jagged DNDXi = new Matrix_Jagged(dNdXi).Transpose();
            return DNDXi * DXiDX;
        }
        public virtual Matrix_Jagged Calculate_DerivativesOfShapeFunctions_WRTX(Vector Xi, Node_ND[] ElementNodes, out double Det_Jacobian)
        {
            Matrix_Jagged DXDXi;
            return Calculate_DerivativesOfShapeFunctions_WRTX(Xi, ElementNodes, out DXDXi, out Det_Jacobian);
        }
        public virtual Matrix_Jagged Calculate_DerivativesOfShapeFunctions_WRTX(Vector Xi, Node_ND[] ElementNodes, out Matrix_Jagged DXDXi, out double Det_Jacobian)
        {
            Vector[] dNdXi;
            Matrix_Jagged DXiDX;
            Calculate_dNdXi_DXDXi_DXiDX_DetJac(Xi, ElementNodes, out dNdXi, out DXDXi, out DXiDX, out Det_Jacobian);

            Matrix_Jagged DNDXi = new Matrix_Jagged(dNdXi).Transpose();
            return DNDXi * DXiDX;
        }

        public virtual Vector Interpolate_DerivativeOfVariable_WRTX(Vector Xi, double[] Variable_NodalValues, Node_ND[] ElementNodes)
        {
            Vector[] dNdXi;
            Matrix_Jagged DXDXi, DXiDX;
            Calculate_dNdXi_DXDXi_DXiDX(Xi, ElementNodes, out dNdXi, out DXDXi, out DXiDX);

            Vector DValueDXi = Interpolate_DerivativeOfVariable_WRTXi(Variable_NodalValues, dNdXi);
            return new Vector(ArrayTools.MatrixMultiply(DValueDXi.Values, DXiDX.Values));
        }
        public virtual Matrix_Jagged Interpolate_DerivativeOfVariable_WRTX(Vector Xi, Vector[] Variable_NodalValues, Node_ND[] ElementNodes)
        {
            Vector[] dNdXi;
            Matrix_Jagged DXDXi, DXiDX;
            Calculate_dNdXi_DXDXi_DXiDX(Xi, ElementNodes, out dNdXi, out DXDXi, out DXiDX);

            Matrix_Jagged DValueDXi = Interpolate_DerivativeOfVariable_WRTXi(Variable_NodalValues, dNdXi);
            return DValueDXi *DXiDX;
        }
        public virtual void Calculate_dNdXi_DXDXi(Vector Xi, Node_ND[] ElementNodes, out Vector[] dNdXi, out Matrix_Jagged DXDXi)
        {
            Vector[] Xp = Get_ElementNodal_X(ElementNodes);
            dNdXi = Calculate_DerivativesOfShapeFunctions_WRTXi(Xi);
            DXDXi = Interpolate_DerivativeOfVariable_WRTXi(Xp, dNdXi);
        }
        public virtual void Calculate_dNdXi_DXDXi_DXiDX(Vector Xi, Node_ND[] ElementNodes, out Vector[] dNdXi, out Matrix_Jagged DXDXi, out Matrix_Jagged DXiDX)
        {
            Calculate_dNdXi_DXDXi(Xi, ElementNodes, out dNdXi, out DXDXi);
            DXiDX = DXDXi.Invert();
        }
        public virtual void Calculate_dNdXi_DXDXi_DXiDX_DetJac(Vector Xi, Node_ND[] ElementNodes, out Vector[] dNdXi, out Matrix_Jagged DXDXi, out Matrix_Jagged DXiDX, out double DetJac)
        {
            Calculate_dNdXi_DXDXi(Xi, ElementNodes, out dNdXi, out DXDXi);
            DXiDX = DXDXi.Invert(out DetJac);
        }
        #endregion

        #region Calculate X and DXDXi
        public virtual Matrix_Jagged Calculate_Jacobian_DXDXi(Vector Xi, Node_ND[] ElementNodes)
        {
            Vector[] Xp = Get_ElementNodal_X(ElementNodes);
            return Interpolate_DerivativeOfVariable_WRTXi(Xi, Xp);
        }
        public virtual Matrix_Jagged Calculate_InverseJacobian_DXiDX(Vector Xi, Node_ND[] ElementNodes)
        {
            Matrix_Jagged DXDXi = Calculate_Jacobian_DXDXi(Xi, ElementNodes);
            return DXDXi.Invert();
        }
        public virtual Matrix_Jagged Calculate_InverseJacobian_DXiDX(Vector Xi, Node_ND[] ElementNodes, out Matrix_Jagged DXDXi, out double Det_Jacobian)
        {
            DXDXi = Calculate_Jacobian_DXDXi(Xi, ElementNodes);
            return DXDXi.Invert(out Det_Jacobian);
        }
        public virtual Vector[] Get_ElementNodal_X(Node_ND[] ElementNodes)
        {
            int nnpe = ElementNodes.Length;
            Vector[] Values = new Vector[nnpe];
            for (int p = 0; p < nnpe; p++)
            {
                Values[p] = ElementNodes[p].X;
            }
            return Values;
        }
        public virtual Vector Calculate_X(Vector Xi, Node_ND[] ElementNodes)
        {
            Vector[] Xp = Get_ElementNodal_X(ElementNodes);
            return Interpolate_Variable(Xi, Xp);
        }
        
        #endregion

        #region Combined value calculations
        public void Calculate_X_DNDX_DetJacobian(Vector Xi, Node_ND[] ElementNodes, out Vector X, out Matrix_Jagged DNDX, out double Det_Jac)
        {
            Vector[] Xp = Get_ElementNodal_X(ElementNodes);
            Vector N;
            Vector[] dNdXi = Calculate_DerivativesOfShapeFunctions_WRTXi(Xi, out N);
            
            X = Interpolate_Variable(Xp, N);
            
            Matrix_Jagged DNDXi = new Matrix_Jagged(dNdXi).Transpose();
            Matrix_Jagged DXDXi = Interpolate_DerivativeOfVariable_WRTXi(Xp, dNdXi);
            Matrix_Jagged DXiDX = DXDXi.Invert(out Det_Jac);
            DNDX = DNDXi * DXiDX; 
        }
        public virtual void Calculate_X_N_DetJacobian(Vector Xi, Node_ND[] ElementNodes, out Vector X, out Vector N, out double Det_Jac)
        {
            Vector[] Xp = Get_ElementNodal_X(ElementNodes);
            Vector[] dNdXi = Calculate_DerivativesOfShapeFunctions_WRTXi(Xi, out N);  
            
            X = Interpolate_Variable(Xp, N);
                    
            Matrix_Jagged DXDXi = Interpolate_DerivativeOfVariable_WRTXi(Xp, dNdXi);
            Det_Jac = DXDXi.Det();
        }
        public virtual void Calculate_N_DXDXi(Vector Xi, Node_ND[] ElementNodes, out Vector N, out Matrix_Jagged DXDXi)
        {
            Vector[] Xp = Get_ElementNodal_X(ElementNodes);
            Vector[] dNdXi = Calculate_DerivativesOfShapeFunctions_WRTXi(Xi, out N);
            DXDXi = Interpolate_DerivativeOfVariable_WRTXi(Xp, dNdXi);
        }
        public virtual void Calculate_X_N_DXDXi(Vector Xi, Node_ND[] ElementNodes, out Vector X, out Vector N, out Matrix_Jagged DXDXi)
        {
            Vector[] Xp = Get_ElementNodal_X(ElementNodes);
            Vector[] dNdXi = Calculate_DerivativesOfShapeFunctions_WRTXi(Xi, out N);
            X = Interpolate_Variable(Xp, N);
            
            DXDXi = Interpolate_DerivativeOfVariable_WRTXi(Xp, dNdXi);
        }
        #endregion

        #region Drawing tools
        public virtual Surfaces Make_GraphicsSurfaces(Node_ND[] ElementNodes, Vector NodeValues, int Resolution)
        {
            Surfaces TheSurfaces = Make_GraphicsSurfaces(ElementNodes);
            return TheSurfaces;
        }
        public virtual Surfaces Make_GraphicsSurfaces(Node_ND[] ElementNodes)
        {
            return Make_GraphicsSurfaces(ElementNodes, 1);
        }
        public virtual Surfaces Make_GraphicsSurfaces(Node_ND[] ElementNodes, int Resolution)
        {
            //Implement for each interpolation type
            Surfaces TheSurfaces = new Surfaces();
            return TheSurfaces;
        }
        public virtual Surfaces Make_GraphicsSurfaces(Node_ND[] ElementNodes, Surfaces TheInputSurfaces, int Resolution)
        {
            int NS = TheInputSurfaces.Sides.Length;
            for (int i = 0; i < NS; i++)
            {
                Make_GraphicsAreasForSide(ElementNodes, Resolution, ref TheInputSurfaces.Sides[i]);
            }
            return TheInputSurfaces;
        }
        public virtual void Make_GraphicsAreasForSide(Node_ND[] ElementNodes, int Resolution, ref Side TheSide)
        {
            //Implement for each interpolation type
        }

        public virtual void Change_GraphicsSurfaces_Values(Node_ND[] ElementNodes, int Resolution, ref Surfaces TheInputSurfaces)
        {
            int NS = TheInputSurfaces.Sides.Length;
            for (int i = 0; i < NS; i++)
            {
                Change_GraphicsSurfaces_Values(ElementNodes, Resolution, ref TheInputSurfaces.Sides[i]);
            }
        }
        public virtual void Change_GraphicsSurfaces_Values(Node_ND[] ElementNodes, int Resolution, ref Side TheSide)
        {
            //Implement for each interpolation type
        }
        

        #endregion

    }
}
