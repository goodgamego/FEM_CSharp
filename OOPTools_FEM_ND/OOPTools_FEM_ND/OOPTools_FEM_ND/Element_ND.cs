using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OOPTools_Math;

namespace OOPTools_FEM_ND
{
    [Serializable]
    public class Element_ND
    {
        /// <summary>
        /// Developed by: Mehrdad Negahban
        /// Date: 11/12/2012
        ///
        /// Purpose:    Multi-dimensional root element
        /// Comments:   Uses parametric interpolation 
        ///             Uses quadrature integration
        ///
        /// Date modified:
        /// Modified by: 
        /// Comments:
        /// </summary>
        public Node_ND[] ElementNodes;
        public static Vector U; //current U
        public static double Time;
        public delegate double Function_Scalar_X(Vector X);//Delegate (function pointer) for scalar functions of type A(x) 
        public delegate double Function_Scalar_XAndT(Vector X, double Time); //Delegate (function pointer) for scalar functions of type w_o(x,t)
        public delegate Vector Function_Vector_X(Vector X);//Delegate (function pointer) for functions of vector type A(x) 
        public delegate Vector Function_Vector_XAndT(Vector X, double Time); //Delegate (function pointer) for vector functions of type w_o(x,t)
        public Integrator_ND_Quadrature FeQuad; //Integrator for Fe
        public Integrator_ND_Quadrature KeQuad; //Integrator for Ke
        public Integrator_ND_Quadrature CeQuad; //Integrator for Ce
        public Integrator_ND_Quadrature MeQuad; //Integrator for Me
        public ParametricInterpolation_ND Interpolator; //Parametric interpolation for element
        public enum Element_ND_Output_Types { };
        public Vector Calculate_Fe()
        {
            Integrator_ND_Quadrature.Function_Vector Fun = new Integrator_ND_Quadrature.Function_Vector(IntegralArgument_Fe);
            return FeQuad.IntegrateFunction(Fun);
        }
        public virtual Vector IntegralArgument_Fe(Vector Xi)
        {
            //Override with argument of load function as a vector
            Vector Fe = new Vector();
            return Fe;
        }
        public Matrix_Jagged Calculate_Ke()
        {
            Integrator_ND_Quadrature.Function_Matrix_Jagged Fun = new Integrator_ND_Quadrature.Function_Matrix_Jagged(IntegralArgument_Ke);
            return KeQuad.IntegrateFunction(Fun);
        }
        public virtual Matrix_Jagged IntegralArgument_Ke(Vector Xi)
        {
            //Override with argument of Ke as a matrix
            Matrix_Jagged Ke = new Matrix_Jagged();
            return Ke;
        }
        public Matrix_Jagged Calculate_Ce()
        {
            Integrator_ND_Quadrature.Function_Matrix_Jagged Fun = new Integrator_ND_Quadrature.Function_Matrix_Jagged(IntegralArgument_Ce);
            return CeQuad.IntegrateFunction(Fun);
        }
        public virtual Matrix_Jagged IntegralArgument_Ce(Vector Xi)
        {
            //Override with argument of Ce as a matrix
            Matrix_Jagged Ce = new Matrix_Jagged();
            return Ce;
        }
        public Matrix_Jagged Calculate_Me()
        {
            Integrator_ND_Quadrature.Function_Matrix_Jagged Fun = new Integrator_ND_Quadrature.Function_Matrix_Jagged(IntegralArgument_Me);
            return MeQuad.IntegrateFunction(Fun);
        }
        public virtual Matrix_Jagged IntegralArgument_Me(Vector Xi)
        {
            //Override with argument of Ce as a matrix (Do not include Le/L_Xi factor)
            Matrix_Jagged Me = new Matrix_Jagged();
            return Me;
        }
        public void CalculateAndAssemble_Fe(ref Vector F)
        {
            Vector Fe = Calculate_Fe();
            Assemble(ref F, Fe);
        }
        public void CalculateAndAssemble_Ke(ref Matrix_Jagged K)
        {
            Matrix_Jagged Ke = Calculate_Ke();
            Assemble(ref K, Ke);
        }
        public void CalculateAndAssemble_Ke(ref MatrixSparseLinkedList K)
        {
            Matrix_Jagged Ke = Calculate_Ke();
            Assemble(ref K, Ke);
        }
        public void CalculateAndAssemble_Ce(ref Matrix_Jagged C)
        {
            Matrix_Jagged Ce = Calculate_Ce();
            Assemble(ref C, Ce);
        }
        public void CalculateAndAssemble_Ce(ref MatrixSparseLinkedList C)
        {
            Matrix_Jagged Ce = Calculate_Ce();
            Assemble(ref C, Ce);
        }
        public void CalculateAndAssemble_Me(ref Matrix_Jagged M)
        {
            Matrix_Jagged Me = Calculate_Me();
            Assemble(ref M, Me);
        }
        public void CalculateAndAssemble_Me(ref MatrixSparseLinkedList M)
        {
            Matrix_Jagged Me = Calculate_Me();
            Assemble(ref M, Me);
        }
        public virtual void Assemble(ref Vector F, Vector Fe)
        {
            //Need to implement 
        }
        public virtual void Assemble(ref Matrix_Jagged K, Matrix_Jagged Ke)
        {
            //Need to implement 
        }
        public virtual void Assemble(ref MatrixSparseLinkedList K, Matrix_Jagged Ke)
        {
            //Need to implement 
        }
        public void CalculateAndAssemble_FeAndKe(ref Vector F, ref Matrix_Jagged K)
        {
            CalculateAndAssemble_Fe(ref F);
            CalculateAndAssemble_Ke(ref K);
        }
        public void CalculateAndAssemble_FeAndKe(ref Vector F, ref MatrixSparseLinkedList K)
        {
            CalculateAndAssemble_Fe(ref F);
            CalculateAndAssemble_Ke(ref K);
        }
        public void CalculateAndAssemble_FeAndKeAndCe(ref Vector F, ref Matrix_Jagged K, ref Matrix_Jagged C)
        {
            CalculateAndAssemble_FeAndKe(ref F, ref K);
            CalculateAndAssemble_Ce(ref C);
        }
        public void CalculateAndAssemble_FeAndKeAndCe(ref Vector F, ref MatrixSparseLinkedList K, ref MatrixSparseLinkedList C)
        {
            CalculateAndAssemble_FeAndKe(ref F, ref K);
            CalculateAndAssemble_Ce(ref C);
        }
        public void CalculateAndAssemble_FeAndKeAndMe(ref Vector F, ref Matrix_Jagged K, ref Matrix_Jagged M)
        {
            CalculateAndAssemble_Me(ref M);
            CalculateAndAssemble_FeAndKe(ref F, ref K);
        }
        public void CalculateAndAssemble_FeAndKeAndMe(ref Vector F, ref MatrixSparseLinkedList K, ref MatrixSparseLinkedList M)
        {
            CalculateAndAssemble_Me(ref M);
            CalculateAndAssemble_FeAndKe(ref F, ref K);
        }
        public void CalculateAndAssemble_FeAndKeAndCeAndMe(ref Vector F, ref Matrix_Jagged K, ref Matrix_Jagged C, ref Matrix_Jagged M)
        {
            CalculateAndAssemble_FeAndKeAndCe(ref F, ref K, ref C);
            CalculateAndAssemble_Me(ref M);
        }
        public void CalculateAndAssemble_FeAndKeAndCeAndMe(ref Vector F, ref MatrixSparseLinkedList K, ref MatrixSparseLinkedList C, ref MatrixSparseLinkedList M)
        {
            CalculateAndAssemble_FeAndKeAndCe(ref F, ref K, ref C);
            CalculateAndAssemble_Me(ref M);
        }

        public virtual Vector Calculate_X(Vector Xi)
        {
            return Interpolator.Calculate_X(Xi, ElementNodes);
        }
        public virtual Matrix_Jagged Calculate_Jabobian_DXDXi(Vector Xi)
        {
            return Interpolator.Calculate_Jacobian_DXDXi(Xi, ElementNodes);
        }
        public virtual Vector[] Get_ElementNodal_X()
        {
            return Interpolator.Get_ElementNodal_X(ElementNodes);
        }

        #region Display tools
        public virtual void Set_ElementNodal_DisplayValuesFromUnknowns()
        {
            //Implement value types to display
        }
        public virtual void Set_ElementNodal_DisplayValuesFromUnknowns(Vector n)
        {
            //Implement value types to display (n is normal)
        }
        public virtual Vector Get_Nodal_DisplayValues()
        {
            int NNPE = ElementNodes.Length;
            Vector DisplayValues = new Vector(NNPE);
            for (int i = 1; i < NNPE; i++)
            {
                DisplayValues.Values[i] = ElementNodes[i].Unknowns.DisplayValue;
            }
            return DisplayValues;
        }
        public virtual Surfaces Make_GraphicSurfaces(int Resolution)
        {
            //Implement for each element
            Surfaces NewSurfaces = Interpolator.Make_GraphicsSurfaces(ElementNodes, Resolution);
            return NewSurfaces;
        }
        public virtual Surfaces Make_GraphicSurfaces()
        {
            return Make_GraphicSurfaces(1);
        }
        public static Surfaces[] Make_GraphicSurfaces(Element_ND[] Elements, int Resolution)
        {
            int NE = Elements.Length;
            Surfaces[] TheSurfaces = new Surfaces[NE];
            for (int i = 0; i < NE; i++)
            {
                Elements[i].Set_ElementNodal_DisplayValuesFromUnknowns();
                TheSurfaces[i] = Elements[i].Make_GraphicSurfaces(Resolution);
            }
            return TheSurfaces;
        }
        public static Surfaces[] Make_GraphicSurfaces(Element_ND[] Elements)
        {
            return Make_GraphicSurfaces(Elements,1);
        }
        public static void Change_GraphicSurfaces_Values(Element_ND[] Elements, ref Surfaces[] TheSurfaces, int Resolution)
        {
            int NE = Elements.Length;
            for (int i = 0; i < NE; i++)
            {
                Elements[i].Change_GraphicSurfaces_Values(ref TheSurfaces[i], Resolution);
            }
        }
        public virtual void Change_GraphicSurfaces_Values(ref Surfaces TheSurfaces, int Resolution)
        {
            Set_ElementNodal_DisplayValuesFromUnknowns();
            Interpolator.Change_GraphicsSurfaces_Values(ElementNodes, Resolution, ref TheSurfaces);
        }
        #endregion

    }
    public class Surfaces
    {
        public Side[] Sides;
        public void CalculateMinMax_Location(out Vector MinX, out Vector MaxX)
        {
            Sides[0].CalculateMinMax_Location(out MinX, out MaxX);
            int NS = Sides.Length;
            for (int i = 1; i < NS; i++)
            {
                Vector iMinX, iMaxX;
                Sides[i].CalculateMinMax_Location(out iMinX, out iMaxX);
                Side.MinMaxVector(iMinX, ref MinX, ref MaxX);
                Side.MinMaxVector(iMaxX, ref MinX, ref MaxX);
            }
        }
        public void CalculateMinMax_Values(out double Min, out double Max)
        {
            Sides[0].CalculateMinMax_Values(out Min, out Max);
            int NS = Sides.Length;
            for (int i = 1; i < NS; i++)
            {
                double iMin, iMax;
                Sides[i].CalculateMinMax_Values(out iMin, out iMax);
                if (Min > iMin) Min = iMin;
                if (Max < iMax) Max = iMax;
            }
        }
        public Surfaces TranslateAndScale(Vector X_o, Vector Scale, double MinValue, double ValueScale)
        {
            Surfaces NewSurfaces = new Surfaces();
            NewSurfaces.Sides = new Side[Sides.Length];
            for (int i = 0; i < Sides.Length; i++)
            {
                NewSurfaces.Sides[i] = Sides[i].TranslateAndScale(X_o, Scale, MinValue, ValueScale);
            }
            return NewSurfaces;
        }
        public Surfaces TranslateAndScale(Vector X_o, Vector Scale)
        {
            Surfaces NewSurfaces = new Surfaces();
            NewSurfaces.Sides = new Side[Sides.Length];
            for (int i = 0; i < Sides.Length; i++)
            {
                NewSurfaces.Sides[i] = Sides[i].TranslateAndScale(X_o, Scale);
            }
            return NewSurfaces;
        }
        public static Surfaces[] TranslateAndScale(Surfaces[] TheSurfaces, Vector X_o, Vector Scale, double MinValue, double ValueScale)
        {
            Surfaces[] NewSurfaces = new Surfaces[TheSurfaces.Length];
            for (int i = 0; i < TheSurfaces.Length; i++)
            {
                NewSurfaces[i] = TheSurfaces[i].TranslateAndScale(X_o, Scale, MinValue, ValueScale);
            }
            return NewSurfaces;
        }
        public static Surfaces[] TranslateAndScale(Surfaces[] TheSurfaces, Vector X_o, Vector Scale)
        {
            Surfaces[] NewSurfaces = new Surfaces[TheSurfaces.Length];
            for (int i = 0; i < TheSurfaces.Length; i++)
            {
                NewSurfaces[i] = TheSurfaces[i].TranslateAndScale(X_o, Scale);
            }
            return NewSurfaces;
        }
        public static void CalculateMinMax_Location(Surfaces[] TheSurfaces, out Vector MinX, out Vector MaxX)
        {
            TheSurfaces[0].CalculateMinMax_Location(out MinX, out MaxX);
            int NS = TheSurfaces.Length;
            for (int i = 1; i < NS; i++)
            {
                Vector iMinX, iMaxX;
                TheSurfaces[i].CalculateMinMax_Location(out iMinX, out iMaxX);
                Side.MinMaxVector(iMinX, ref MinX, ref MaxX);
                Side.MinMaxVector(iMaxX, ref MinX, ref MaxX);
            }
        }
        public static void CalculateMinMax_Values(Surfaces[] TheSurfaces, out double Min, out double Max)
        {
            TheSurfaces[0].CalculateMinMax_Values(out Min, out Max);
            int NS = TheSurfaces.Length;
            for (int i = 1; i < NS; i++)
            {
                double iMin, iMax;
                TheSurfaces[i].CalculateMinMax_Values(out iMin, out iMax);
                if (Min > iMin) Min = iMin;
                if (Max < iMax) Max = iMax;
            }
        }
    }
    public class Side
    {
        public Vector[][] MeshLines;
        public Vector[] NodeLocations;
        public Vector[][] Areas;
        public Vector[] AreaValues;
        public void CalculateMinMax_Location(out Vector MinX, out Vector MaxX)
        {
            MinX = new Vector(NodeLocations[0]);
            MaxX = new Vector(NodeLocations[0]);
            MinMaxVector(NodeLocations, ref MinX, ref MaxX);
        }
        public void CalculateMinMax_Values(out double Min, out double Max)
        {
            Vector MinV = new Vector(AreaValues[0]);
            Vector MaxV = new Vector(AreaValues[0]);
            MinMaxVector(AreaValues, ref MinV, ref MaxV);
            Min = MinV.Min();
            Max = MaxV.Max();
        }
        public static void MinMaxVector(Vector X, ref Vector MinX, ref Vector MaxX)
        {
            for (int j = 0; j < X.Values.Length; j++)
            {
                if (MinX.Values[j] > X.Values[j]) MinX.Values[j] = X.Values[j];
                if (MaxX.Values[j] < X.Values[j]) MaxX.Values[j] = X.Values[j];
            }
        }
        public static void MinMaxVector(Vector[] X, ref Vector MinX, ref Vector MaxX)
        {
            int NP = X.Length;
            for (int i = 0; i < NP; i++)
            {
                MinMaxVector(X[i], ref MinX, ref MaxX);
            }
        }
        public Side TranslateAndScale(Vector X_o, Vector Scale, double MinValue, double ValueScale)
        {
            Side NewSide = new Side();
            NewSide.MeshLines = TranslateAndScale(MeshLines, X_o, Scale);
            NewSide.NodeLocations = TranslateAndScale(NodeLocations, X_o, Scale);
            NewSide.Areas = TranslateAndScale(Areas, X_o, Scale);
            NewSide.AreaValues = TranslateAndScale(AreaValues, MinValue, ValueScale);
            return NewSide;
        }
        public Side TranslateAndScale(Vector X_o, Vector Scale)
        {
            Side NewSide = new Side();
            NewSide.MeshLines = TranslateAndScale(MeshLines, X_o, Scale);
            NewSide.NodeLocations = TranslateAndScale(NodeLocations, X_o, Scale);
            return NewSide;
        }
        public static Vector TranslateAndScale(Vector X, double MinValue, double ValueScale)
        {
            int NP = X.Values.Length;
            Vector ScaledValues = new Vector(X);
            for (int i = 0; i < NP; i++)
            {
                ScaledValues.Values[i] -= MinValue;
                ScaledValues.Values[i] *= ValueScale;
            }
            return ScaledValues;
        }
        public static Vector[] TranslateAndScale(Vector[] X, double MinValue, double ValueScale)
        {
            int NP = X.Length;
            Vector[] ScaledValues = new Vector[NP];
            for (int i = 0; i < NP; i++)
            {
                ScaledValues[i] = TranslateAndScale(X[i], MinValue, ValueScale);
            }
            return ScaledValues;
        }
        public static Vector TranslateAndScale(Vector X, Vector X_o, Vector Scale)
        {
            Vector NewVector = X - X_o;
            for (int i = 0; i < X.Values.Length; i++)
            {
                NewVector.Values[i] *= Scale.Values[i];
            }
            return NewVector;
        }
        public static Vector[] TranslateAndScale(Vector[] X, Vector X_o, Vector Scale)
        {
            Vector[] NewVectors = new Vector[X.Length];
            for (int i = 0; i < X.Length; i++)
            {
                NewVectors[i] = TranslateAndScale(X[i], X_o, Scale);
            }
            return NewVectors;
        }
        public static Vector[][] TranslateAndScale(Vector[][] X, Vector X_o, Vector Scale)
        {
            Vector[][] NewVectors = new Vector[X.Length][];
            for (int i = 0; i < X.Length; i++)
            {
                NewVectors[i] = TranslateAndScale(X[i], X_o, Scale);
            }
            return NewVectors;
        }

    }

}
