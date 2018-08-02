using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OOPTools_Math;

namespace OOPTools_FEM_1D
{
    public class Element
    {
        /// <summary>
        /// Developed by: Mehrdad Negahban
        /// Date: 10/7/2012
        ///
        /// Purpose:    One dimensional root element
        /// Comments:   Uses parametric interpolation 
        ///             Uses quadrature integration
        ///
        /// Date modified:
        /// Modified by: 
        /// Comments:
        /// </summary>
        public delegate double Function_Scalar_X(double X);//Delegate (function pointer) for functions of type A(x) 
        public delegate double Function_Scalar_XAndT(double X, double Time); //Delegate (function pointer) for functions of type w_o(x,t)
        public int NNPE; //Number of nodes per element
        public Node[] ElementNodes; //Nodes of element
        public Integrator_Quadrature FeQuad; //Integrator for Fe
        public Integrator_Quadrature KeQuad; //Integrator for Ke
        public Integrator_Quadrature CeQuad; //Integrator for Ce
        public Integrator_Quadrature MeQuad; //Integrator for Me
        public ParametricInterpolation Interpolator; //Parametric interpolation for element
        public static Vector U; //current U
        public double Le; //Element length
        public Element()
        {
        }
        public Element(int nnpe)
        {
            NNPE = nnpe;
            ElementNodes = new Node[NNPE];
        }
        public Vector Calculate_Fe()
        {
            Integrator_Quadrature.Function_Vector Fun = new Integrator_Quadrature.Function_Vector(Fe_IntegralArgument);
            return FeQuad.IntegrateFunction(Fun)*(Le/Interpolator.L_Xi);
        }
        public virtual Vector Fe_IntegralArgument(double Xi)
        {
            //Override with argument of load function as a vector (Do not include Le/L_Xi factor)
            Vector Fe = new Vector();
            return Fe;
        }
        public Matrix Calculate_Ke()
        {
            Integrator_Quadrature.Function_Matrix Fun = new Integrator_Quadrature.Function_Matrix(Ke_IntegralArgument);
            return KeQuad.IntegrateFunction(Fun) * (Interpolator.L_Xi / Le);
        }
        public virtual Matrix Ke_IntegralArgument(double Xi)
        {
            //Override with argument of Ke as a matrix (Do not include L_Xi/Le factor)
            Matrix Ke = new Matrix();
            return Ke;
        }
        public Matrix Calculate_Ce()
        {
            Integrator_Quadrature.Function_Matrix Fun = new Integrator_Quadrature.Function_Matrix(Ce_IntegralArgument);
            return CeQuad.IntegrateFunction(Fun) * (Le / Interpolator.L_Xi);
        }
        public virtual Matrix Ce_IntegralArgument(double Xi)
        {
            //Override with argument of Ce as a matrix (Do not include Le/L_Xi factor)
            Matrix Ce = new Matrix();
            return Ce;
        }
        public Matrix Calculate_Me()
        {
            Integrator_Quadrature.Function_Matrix Fun = new Integrator_Quadrature.Function_Matrix(Me_IntegralArgument);
            return MeQuad.IntegrateFunction(Fun) * (Le / Interpolator.L_Xi);
        }
        public virtual Matrix Me_IntegralArgument(double Xi)
        {
            //Override with argument of Ce as a matrix (Do not include Le/L_Xi factor)
            Matrix Me = new Matrix();
            return Me;
        }
        public void CalculateAndAssemble_Fe(ref Vector F)
        {
            Vector Fe = Calculate_Fe();
            Assemble(ref F, Fe);
        }
        public void CalculateAndAssemble_Ke(ref Matrix K)
        {
            Matrix Ke = Calculate_Ke();
            Assemble(ref K, Ke);
        }
        public void CalculateAndAssemble_Ce(ref Matrix C)
        {
            Matrix Ce = Calculate_Ce();
            Assemble(ref C, Ce);
        }
        public void CalculateAndAssemble_Me(ref Matrix M)
        {
            Matrix Me = Calculate_Me();
            Assemble(ref M, Me);
        }
        public void Assemble(ref Vector F, Vector Fe)
        {
            int Ip;
            for (int p = 0; p < NNPE; p++)
            {
                Ip = ElementNodes[p].I;
                F.Values[Ip] += Fe.Values[p];
            }
        }
        public void Assemble(ref Matrix K, Matrix Ke)
        {
            int Ip, Iq;
            for (int p = 0; p < NNPE; p++)
            {
                Ip = ElementNodes[p].I;
                for (int q = 0; q < NNPE; q++)
                {
                    Iq = ElementNodes[q].I;
                    K.Values[Ip, Iq] += Ke.Values[p, q];
                }
            }
        }
        public void CalculateAndAssemble_FeAndKe(ref Vector F, ref Matrix K)
        {
            CalculateAndAssemble_Fe(ref F);
            CalculateAndAssemble_Ke(ref K);
        }
        public void CalculateAndAssemble_FeAndKeAndCe(ref Vector F, ref Matrix K, ref Matrix C)
        {
            CalculateAndAssemble_FeAndKe(ref F, ref K);
            CalculateAndAssemble_Ce(ref C);
        }
        public void CalculateAndAssemble_FeAndKeAndMe(ref Vector F, ref Matrix K, ref Matrix M)
        {
            CalculateAndAssemble_Me(ref M);
            CalculateAndAssemble_FeAndKe(ref F, ref K);
        }
        public void CalculateAndAssemble_FeAndKeAndCeAndMe(ref Vector F, ref Matrix K, ref Matrix C, ref Matrix M)
        {
            CalculateAndAssemble_FeAndKeAndCe(ref F, ref K, ref C);
            CalculateAndAssemble_Me(ref M);
        }
        public void Calculate_Le()
        {
            Le = ElementNodes[NNPE - 1].X - ElementNodes[0].X;
        }

        public virtual Vector GetNodeLocations()
        {
            Vector X = new Vector(NNPE);
            for (int i = 0; i < NNPE; i++)
            {
                X.Values[i] = ElementNodes[i].X;
            }
            return X;
        }
        public virtual Vector GetElementNodeU(Vector U)
        {
            Vector Ue = new Vector(NNPE);
            for (int i = 0; i < NNPE; i++)
            {
                Ue.Values[i] = U.Values[ElementNodes[i].I];
            }
            return Ue;
        }
        public virtual Vector GetElementNodeDUDX()
        {
            Vector DuDX = new Vector(NNPE);
            Vector Ue = GetElementNodeU(Element.U);
            for (int i = 0; i < NNPE; i++)
            {
                DuDX.Values[i] = Interpolator.InterpolateDerivativeOfVariable_WRTX(Interpolator.NodeXi.Values[i], Le, Ue);
            }
            return DuDX;
        }
        public virtual double CalculateU(double Xi)
        {
            Vector Ue = GetElementNodeU(Element.U);
            return Interpolator.InterpolateVariable(Xi, Ue);
        }
        public virtual double CalculateDuDX(double Xi)
        {
            Vector Ue = GetElementNodeU(Element.U);
            return Interpolator.InterpolateDerivativeOfVariable_WRTX(Xi, Le, Ue);
        }
        public virtual double CalculateDuDX(double Xi, Vector DNDXi)
        {
            Vector Ue = GetElementNodeU(Element.U);
            return Interpolator.InterpolateDerivativeOfVariable_WRTX(Le,Ue, DNDXi);
        }
    }
}
