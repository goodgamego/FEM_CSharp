using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using OOPTools_Math;
using OOPTools_FEM_1D;
using OOPTools;

namespace OOPTools_FEM_ND
{
    [Serializable]
    public class ParametricInterpolation_ND_Square : ParametricInterpolation_ND
    {
        /// <summary>
        /// Developed by: Mehrdad Negahban
        /// Date: 11/12/2012
        ///
        /// Purpose:    Provide interpolation shape functions and their derivatives for 2x2 Square
        /// Comments:   Xi is the parametric variable of space
        ///             X is the real variable of space
        ///             
        /// Date modified:
        /// Modified by:
        /// Comments:
        /// </summary>
        public int NNPE_m, NNPE_n;
        ParametricInterpolation_Lagrange ParametricInterpolation_1D_M, ParametricInterpolation_1D_N;
        public ParametricInterpolation_ND_Square()
        {
        }
        public ParametricInterpolation_ND_Square(int nnpe_m, int nnpe_n)
        {
            NNPE_m = nnpe_m;
            NNPE_n = nnpe_n;
            NNPE = NNPE_m * NNPE_n;
            ParametricInterpolation_1D_M = new ParametricInterpolation_Lagrange(NNPE_m);
            ParametricInterpolation_1D_N = new ParametricInterpolation_Lagrange(NNPE_n);
            NodeXi = Calculate_ElementNodal_Xi();
        }
        public override Vector[] Calculate_ElementNodal_Xi()
        {
            Vector[] nodeXi = new Vector[NNPE];
            double DX0 = 2.0D / Convert.ToDouble(NNPE_m - 1);
            double DX1 = 2.0D / Convert.ToDouble(NNPE_n - 1);
            int Index = 0;
            for (int j = 0; j < NNPE_n; j++)
            {
                double X1 = -1.0D + Convert.ToDouble(j) * DX1;
                for (int i = 0; i < NNPE_m; i++)
                {
                    nodeXi[Index] = new Vector(2);
                    nodeXi[Index].Values[1] = X1;
                    nodeXi[Index].Values[0] = -1.0D + Convert.ToDouble(i) * DX0;
                    Index++;
                }
            }
            return nodeXi;
        }
        public override Vector Calculate_ShapeFunctions(Vector Xi)
        {
            Vector N = new Vector(NNPE);
            Vector N_1D_M = ParametricInterpolation_1D_M.CalculateShapeFunctions(Xi.Values[0]);
            Vector N_1D_N = ParametricInterpolation_1D_N.CalculateShapeFunctions(Xi.Values[1]);
            int Index = 0;
            for (int j = 0; j < NNPE_n; j++)
            {
                for (int i = 0; i < NNPE_m; i++)
                {
                    N.Values[Index] = N_1D_M.Values[i] * N_1D_N.Values[j];
                    Index++;
                }
            }
            return N;
        }
        public override Vector[] Calculate_DerivativesOfShapeFunctions_WRTXi(Vector Xi)
        {
            Vector[] DNDXi = new Vector[NNPE];
            Vector N_1D_M = ParametricInterpolation_1D_M.CalculateShapeFunctions(Xi.Values[0]);
            Vector N_1D_N = ParametricInterpolation_1D_N.CalculateShapeFunctions(Xi.Values[1]);
            Vector DNDXi_1D_M = ParametricInterpolation_1D_M.CalculateDerivativesOfShapeFunctions_WRTXi(Xi.Values[0]);
            Vector DNDXi_1D_N = ParametricInterpolation_1D_N.CalculateDerivativesOfShapeFunctions_WRTXi(Xi.Values[1]);
            int Index = 0;
            for (int j = 0; j < NNPE_n; j++)
            {
                for (int i = 0; i < NNPE_m; i++)
                {
                    DNDXi[Index] = new Vector(2);
                    DNDXi[Index].Values[0] = DNDXi_1D_M.Values[i] * N_1D_N.Values[j];
                    DNDXi[Index].Values[1] = N_1D_M.Values[i] * DNDXi_1D_N.Values[j];
                    Index++;
                }
            }
            return DNDXi;
        }
        public override Vector[] Calculate_DerivativesOfShapeFunctions_WRTXi(Vector Xi, out Vector N)
        {
            N = new Vector(NNPE);
            Vector[] DNDXi = new Vector[NNPE];
            Vector N_1D_M = ParametricInterpolation_1D_M.CalculateShapeFunctions(Xi.Values[0]);
            Vector N_1D_N = ParametricInterpolation_1D_N.CalculateShapeFunctions(Xi.Values[1]);
            Vector DNDXi_1D_M = ParametricInterpolation_1D_M.CalculateDerivativesOfShapeFunctions_WRTXi(Xi.Values[0]);
            Vector DNDXi_1D_N = ParametricInterpolation_1D_N.CalculateDerivativesOfShapeFunctions_WRTXi(Xi.Values[1]);
            int Index = 0;
            for (int j = 0; j < NNPE_n; j++)
            {
                for (int i = 0; i < NNPE_m; i++)
                {
                    N.Values[Index] = N_1D_N.Values[j] * N_1D_M.Values[i];
                    DNDXi[Index] = new Vector(2);
                    DNDXi[Index].Values[0] = DNDXi_1D_M.Values[i] * N_1D_N.Values[j];
                    DNDXi[Index].Values[1] = N_1D_M.Values[i] * DNDXi_1D_N.Values[j];
                    Index++;
                }
            }
            return DNDXi;
        }
        public override Matrix_Jagged Calculate_DerivativesOfShapeFunctions_WRTXi_Matrix_Jagged(Vector Xi)
        {
            Matrix_Jagged DNDXi = new Matrix_Jagged(NNPE,2);
            Vector N_1D_M = ParametricInterpolation_1D_M.CalculateShapeFunctions(Xi.Values[0]);
            Vector N_1D_N = ParametricInterpolation_1D_N.CalculateShapeFunctions(Xi.Values[1]);
            Vector DNDXi_1D_M = ParametricInterpolation_1D_M.CalculateDerivativesOfShapeFunctions_WRTXi(Xi.Values[0]);
            Vector DNDXi_1D_N = ParametricInterpolation_1D_N.CalculateDerivativesOfShapeFunctions_WRTXi(Xi.Values[1]);
            int Index = 0;
            for (int j = 0; j < NNPE_n; j++)
            {
                for (int i = 0; i < NNPE_m; i++)
                {
                    DNDXi.Values[Index][0] = DNDXi_1D_M.Values[i] * N_1D_N.Values[j];
                    DNDXi.Values[Index][1] = N_1D_M.Values[i] * DNDXi_1D_N.Values[j];
                    Index++;
                }
            }
            return DNDXi;
        }
        public override Matrix_Jagged Calculate_DerivativesOfShapeFunctions_WRTXi_Matrix_Jagged(Vector Xi, out Vector N)
        {
            N = new Vector(NNPE);
            Matrix_Jagged DNDXi = new Matrix_Jagged(NNPE, 2);
            Vector N_1D_M = ParametricInterpolation_1D_M.CalculateShapeFunctions(Xi.Values[0]);
            Vector N_1D_N = ParametricInterpolation_1D_N.CalculateShapeFunctions(Xi.Values[1]);
            Vector DNDXi_1D_M = ParametricInterpolation_1D_M.CalculateDerivativesOfShapeFunctions_WRTXi(Xi.Values[0]);
            Vector DNDXi_1D_N = ParametricInterpolation_1D_N.CalculateDerivativesOfShapeFunctions_WRTXi(Xi.Values[1]);
            int Index = 0;
            for (int j = 0; j < NNPE_n; j++)
            {
                for (int i = 0; i < NNPE_m; i++)
                {
                    N.Values[Index] = N_1D_N.Values[j] * N_1D_M.Values[i];
                    DNDXi.Values[Index][0] = DNDXi_1D_M.Values[i] * N_1D_N.Values[j];
                    DNDXi.Values[Index][1] = N_1D_M.Values[i] * DNDXi_1D_N.Values[j];
                    Index++;
                }
            }
            return DNDXi;
        }

        #region Graphics surfaces tools
        public override Surfaces Make_GraphicsSurfaces(Node_ND[] ElementNodes, int Resolution)
        {
            Surfaces TheSurfaces = new Surfaces();
            TheSurfaces.Sides = new Side[1];
            TheSurfaces.Sides[0] = new Side();
            Side TheSide = new Side();
            TheSide.NodeLocations = Get_ElementNodal_X(ElementNodes);
            TheSide.MeshLines = new Vector[4][];
            TheSide.MeshLines[0] = new Vector[NNPE_m];
            TheSide.MeshLines[1] = new Vector[NNPE_n];
            TheSide.MeshLines[2] = new Vector[NNPE_m];
            TheSide.MeshLines[3] = new Vector[NNPE_n];
            for (int i = 0; i < NNPE_m; i++)
            {
                TheSide.MeshLines[0][i] = TheSide.NodeLocations[i];
                TheSide.MeshLines[2][NNPE_m - 1 - i] = TheSide.NodeLocations[NNPE_m * (NNPE_n - 1) + i];
            }
            for (int i = 0; i < NNPE_n; i++)
            {
                TheSide.MeshLines[1][i] = TheSide.NodeLocations[NNPE_m - 1 + NNPE_m * i];
                TheSide.MeshLines[3][NNPE_n - 1 - i] = TheSide.NodeLocations[NNPE_m * i];
            }
            TheSurfaces.Sides[0] = TheSide;
            Make_GraphicsAreasForSide(ElementNodes, Resolution, ref TheSurfaces.Sides[0]);
            return TheSurfaces;
        }
        private void MakeGraphicsAreas(Node_ND[] ElementNodes, ref Side TheSide)
        {
            int NA = (NNPE_m - 1) * (NNPE_n - 1);
            TheSide.Areas = new Vector[NA][];
            TheSide.AreaValues = new Vector[NA];
            Vector Node_DisplayValues = Get_Nodal_DisplayValues(ElementNodes);
            int index = 0;
            for (int i = 0; i < NNPE_m - 1; i++)
            {
                for (int j = 0; j < NNPE_n - 1; j++)
                {
                    TheSide.Areas[index] = new Vector[4];
                    TheSide.AreaValues[index] = new Vector(4);
                    TheSide.Areas[index][0] = TheSide.NodeLocations[NNPE_m * j + i];
                    TheSide.AreaValues[index].Values[0] = Node_DisplayValues.Values[NNPE_m * j + i];
                    TheSide.Areas[index][1] = TheSide.NodeLocations[NNPE_m * j + i + 1];
                    TheSide.AreaValues[index].Values[1] = Node_DisplayValues.Values[NNPE_m * j + i + 1];
                    TheSide.Areas[index][2] = TheSide.NodeLocations[NNPE_m * (j + 1) + i + 1];
                    TheSide.AreaValues[index].Values[2] = Node_DisplayValues.Values[NNPE_m * (j + 1) + i + 1];
                    TheSide.Areas[index][3] = TheSide.NodeLocations[NNPE_m * (j + 1) + i];
                    TheSide.AreaValues[index].Values[3] = Node_DisplayValues.Values[NNPE_m * (j + 1) + i];
                    //TheSide.Areas[index][4] = TheSide.Areas[index][0];
                    //TheSide.AreaValues[index].Values[4] = TheSide.AreaValues[index].Values[0]; 
                    index++;
                }
            }
        }
        public virtual Vector Get_Nodal_DisplayValues(Node_ND[] ElementNodes)
        {
            int NNPE = ElementNodes.Length;
            Vector DisplayValues = new Vector(NNPE);
            for (int i = 0; i < NNPE; i++)
            {
                DisplayValues.Values[i] = ElementNodes[i].Unknowns.DisplayValue;
            }
            return DisplayValues;
        }
        public override void Make_GraphicsAreasForSide(Node_ND[] ElementNodes, int Resolution, ref Side TheSide)
        {
            if (Resolution == 1)
            {
                MakeGraphicsAreas(ElementNodes, ref TheSide);
            }
            else
            {
                Vector Node_DisplayValues = Get_Nodal_DisplayValues(ElementNodes);
                int NA = Resolution * Resolution * (NNPE_m - 1) * (NNPE_n - 1);
                TheSide.Areas = new Vector[NA][];
                TheSide.AreaValues = new Vector[NA];
                int index = 0;
                Vector X_o = new Vector(2);
                X_o.Values[0] = -1.0D;
                X_o.Values[1] = -1.0D;
                Vector DX1 = new Vector(2);
                DX1.Values[0] = 2.0D / Convert.ToDouble(Resolution * (NNPE_m - 1));
                Vector DX2 = new Vector(2);
                DX2.Values[1] = 2.0D / Convert.ToDouble(Resolution * (NNPE_n - 1));
                for (int i = 0; i < Resolution*(NNPE_m - 1); i++)
                {
                    for (int j = 0; j < Resolution*(NNPE_n - 1); j++)
                    {
                        TheSide.Areas[index] = new Vector[4];
                        TheSide.AreaValues[index] = new Vector(4);

                        Vector Xi = X_o + Convert.ToDouble(i) * DX1 + Convert.ToDouble(j) * DX2;
                        Vector N = Calculate_ShapeFunctions(Xi);
                        TheSide.Areas[index][0] = Interpolate_Variable(TheSide.NodeLocations, N);
                        TheSide.AreaValues[index].Values[0] = Interpolate_Variable(Node_DisplayValues.Values, N);

                        Xi += DX1;
                        N = Calculate_ShapeFunctions(Xi);
                        TheSide.Areas[index][1] = Interpolate_Variable(TheSide.NodeLocations, N);
                        TheSide.AreaValues[index].Values[1] = Interpolate_Variable(Node_DisplayValues.Values, N);
                        Xi += DX2;
                        N = Calculate_ShapeFunctions(Xi);
                        TheSide.Areas[index][2] = Interpolate_Variable(TheSide.NodeLocations, N);
                        TheSide.AreaValues[index].Values[2] = Interpolate_Variable(Node_DisplayValues.Values, N);
                        Xi -= DX1;
                        N = Calculate_ShapeFunctions(Xi);
                        TheSide.Areas[index][3] = Interpolate_Variable(TheSide.NodeLocations, N);
                        TheSide.AreaValues[index].Values[3] = Interpolate_Variable(Node_DisplayValues.Values, N);

                        //TheSide.Areas[index][4] = TheSide.Areas[index][0];
                        //TheSide.AreaValues[index].Values[4] = TheSide.AreaValues[index].Values[0];
                        index++;
                    }
                }
            }
        }
        public override void Change_GraphicsSurfaces_Values(Node_ND[] ElementNodes, int Resolution, ref Side TheSide)
        {
            if (Resolution == 1)
            {
                Change_GraphicsAreas_Values(ElementNodes, ref TheSide);
            }
            else
            {
                Vector Node_DisplayValues = Get_Nodal_DisplayValues(ElementNodes);
                int NA = Resolution * Resolution * (NNPE_m - 1) * (NNPE_n - 1);
                TheSide.AreaValues = new Vector[NA];
                int index = 0;
                Vector X_o = new Vector(2);
                X_o.Values[0] = -1.0D;
                X_o.Values[1] = -1.0D;
                Vector DX1 = new Vector(2);
                DX1.Values[0] = 2.0D / Convert.ToDouble(Resolution * (NNPE_m - 1));
                Vector DX2 = new Vector(2);
                DX2.Values[1] = 2.0D / Convert.ToDouble(Resolution * (NNPE_n - 1));
                for (int i = 0; i < Resolution * (NNPE_m - 1); i++)
                {
                    for (int j = 0; j < Resolution * (NNPE_n - 1); j++)
                    {
                        TheSide.Areas[index] = new Vector[4];
                        TheSide.AreaValues[index] = new Vector(4);

                        Vector Xi = X_o + Convert.ToDouble(i) * DX1 + Convert.ToDouble(j) * DX2;
                        Vector N = Calculate_ShapeFunctions(Xi);
                        TheSide.AreaValues[index].Values[0] = Interpolate_Variable(Node_DisplayValues.Values, N);

                        Xi += DX1;
                        N = Calculate_ShapeFunctions(Xi);
                        TheSide.AreaValues[index].Values[1] = Interpolate_Variable(Node_DisplayValues.Values, N);
                        Xi += DX2;
                        N = Calculate_ShapeFunctions(Xi);
                        TheSide.AreaValues[index].Values[2] = Interpolate_Variable(Node_DisplayValues.Values, N);
                        Xi -= DX1;
                        N = Calculate_ShapeFunctions(Xi);
                        TheSide.AreaValues[index].Values[3] = Interpolate_Variable(Node_DisplayValues.Values, N);

                        //TheSide.AreaValues[index].Values[4] = TheSide.AreaValues[index].Values[0];
                        index++;
                    }
                }
            }
        }
        private void Change_GraphicsAreas_Values(Node_ND[] ElementNodes, ref Side TheSide)
        {
            int NA = (NNPE_m - 1) * (NNPE_n - 1);
            TheSide.AreaValues = new Vector[NA];
            Vector Node_DisplayValues = Get_Nodal_DisplayValues(ElementNodes);
            int index = 0;
            for (int i = 0; i < NNPE_m - 1; i++)
            {
                for (int j = 0; j < NNPE_n - 1; j++)
                {
                    TheSide.AreaValues[index] = new Vector(4);
                    TheSide.AreaValues[index].Values[0] = Node_DisplayValues.Values[NNPE_m * j + i];
                    TheSide.AreaValues[index].Values[1] = Node_DisplayValues.Values[NNPE_m * j + i + 1];
                    TheSide.AreaValues[index].Values[2] = Node_DisplayValues.Values[NNPE_m * (j + 1) + i + 1];
                    TheSide.AreaValues[index].Values[3] = Node_DisplayValues.Values[NNPE_m * (j + 1) + i];
                    //TheSide.AreaValues[index].Values[4] = TheSide.AreaValues[index].Values[0];
                    index++;
                }
            }
        }
        #endregion
    }
}
