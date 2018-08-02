using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using OOPTools_Math;
using OOPTools_FEM_1D;

namespace OOPTools_FEM_ND
{
    [Serializable]
    public class ParametricInterpolation_ND_Cube : ParametricInterpolation_ND
    {
        /// <summary>
        /// Developed by: Mehrdad Negahban
        /// Date: 11/12/2012
        ///
        /// Purpose:    Provide interpolation shape functions and their derivatives for 2x2x2 Cube
        /// Comments:   Xi is the parametric variable of space
        ///             X is the real variable of space
        ///             
        /// Date modified:
        /// Modified by:
        /// Comments:
        /// </summary>
        public int NNPE_l, NNPE_m, NNPE_n;
        ParametricInterpolation_Lagrange ParametricInterpolation_1D_L, ParametricInterpolation_1D_M, ParametricInterpolation_1D_N;
        public ParametricInterpolation_ND_Cube()
        {
        }
        public ParametricInterpolation_ND_Cube(int nnpe_l, int nnpe_m, int nnpe_n)
        {
            NNPE_l = nnpe_l;
            NNPE_m = nnpe_m;
            NNPE_n = nnpe_n;
            NNPE =  NNPE_l * NNPE_m * NNPE_n;
            ParametricInterpolation_1D_L = new ParametricInterpolation_Lagrange(NNPE_l);
            ParametricInterpolation_1D_M = new ParametricInterpolation_Lagrange(NNPE_m);
            ParametricInterpolation_1D_N = new ParametricInterpolation_Lagrange(NNPE_n);
            NodeXi = Calculate_ElementNodal_Xi();
        }

        #region Utility tools
        public override Vector[] Calculate_ElementNodal_Xi()
        {
            Vector[] nodeXi = new Vector[NNPE];
            double DX0 = 2.0D / Convert.ToDouble(NNPE_l - 1);
            double DX1 = 2.0D / Convert.ToDouble(NNPE_m - 1);
            double DX2 = 2.0D / Convert.ToDouble(NNPE_n - 1);
            int Index = 0;
            for (int k = 0; k < NNPE_n; k++)
            {
                double X2 = -1.0D + Convert.ToDouble(k) * DX2;
                for (int j = 0; j < NNPE_m; j++)
                {
                    double X1 = -1.0D + Convert.ToDouble(j) * DX1;
                    for (int i = 0; i < NNPE_l; i++)
                    {
                        nodeXi[Index] = new Vector(3);
                        nodeXi[Index].Values[2] = X2;
                        nodeXi[Index].Values[1] = X1;
                        nodeXi[Index].Values[0] = -1.0D + Convert.ToDouble(i) * DX0;
                        Index++;
                    }
                }
            }
            return nodeXi;
        }
        #endregion

        #region Interpolation
        public override Vector Calculate_ShapeFunctions(Vector Xi)
        {
            Vector N = new Vector(NNPE);
            Vector N_1D_L = ParametricInterpolation_1D_L.CalculateShapeFunctions(Xi.Values[0]);
            Vector N_1D_M = ParametricInterpolation_1D_M.CalculateShapeFunctions(Xi.Values[1]);
            Vector N_1D_N = ParametricInterpolation_1D_N.CalculateShapeFunctions(Xi.Values[2]);
            int Index = 0;
            for (int k = 0; k < NNPE_n; k++)
            {
                for (int j = 0; j < NNPE_m; j++)
                {
                    for (int i = 0; i < NNPE_l; i++)
                    {
                        N.Values[Index] = N_1D_N.Values[k] * N_1D_M.Values[j] * N_1D_L.Values[i];
                        Index++;
                    }
                }
            }
            return N;
        }
        public override Vector[] Calculate_DerivativesOfShapeFunctions_WRTXi(Vector Xi)
        {
            Vector[] DNDXi = new Vector[NNPE];
            Vector N_1D_L = ParametricInterpolation_1D_L.CalculateShapeFunctions(Xi.Values[0]);
            Vector N_1D_M = ParametricInterpolation_1D_M.CalculateShapeFunctions(Xi.Values[1]);
            Vector N_1D_N = ParametricInterpolation_1D_N.CalculateShapeFunctions(Xi.Values[2]);
            Vector DNDXi_1D_L = ParametricInterpolation_1D_L.CalculateDerivativesOfShapeFunctions_WRTXi(Xi.Values[0]);
            Vector DNDXi_1D_M = ParametricInterpolation_1D_M.CalculateDerivativesOfShapeFunctions_WRTXi(Xi.Values[1]);
            Vector DNDXi_1D_N = ParametricInterpolation_1D_N.CalculateDerivativesOfShapeFunctions_WRTXi(Xi.Values[2]);
            int Index = 0;
            for (int k = 0; k < NNPE_n; k++)
            {
                for (int j = 0; j < NNPE_m; j++)
                {
                    for (int i = 0; i < NNPE_l; i++)
                    {
                        DNDXi[Index] = new Vector(3);
                        DNDXi[Index].Values[0] = N_1D_N.Values[k] * N_1D_M.Values[j] * DNDXi_1D_L.Values[i];
                        DNDXi[Index].Values[1] = N_1D_N.Values[k] * DNDXi_1D_M.Values[j] * N_1D_L.Values[i];
                        DNDXi[Index].Values[2] = DNDXi_1D_N.Values[k] * N_1D_M.Values[j] * N_1D_L.Values[i];
                        Index++;
                    }
                }
            }
            return DNDXi;
        }
        #endregion

        #region Graphics surfaces tools
        public override Surfaces Make_GraphicsSurfaces(Node_ND[] ElementNodes, int Resolution)
        {
            Node_ND[][] Side_Nodes;
            int[][] NNPE_Side;
            Get_Side_Nodes(ElementNodes, out NNPE_Side, out Side_Nodes);
            Surfaces TheSurfaces = new Surfaces();
            TheSurfaces.Sides = new Side[6];
            for (int i = 0; i < 3; i++)
            {
                Make_Side(Side_Nodes[i], NNPE_Side[i], Resolution, out TheSurfaces.Sides[i]);
                Make_Side(Side_Nodes[i+3], NNPE_Side[i], Resolution, out TheSurfaces.Sides[i+3]);
            }
            return TheSurfaces;
        }
        private void Make_Side(Node_ND[] Side_Nodes, int[] NNPE_Side, int Resolution, out Side TheSide)
        {
            TheSide = new Side();
            TheSide.NodeLocations = Get_ElementNodal_X(Side_Nodes);
            TheSide.MeshLines = new Vector[4][];
            TheSide.MeshLines[0] = new Vector[NNPE_Side[0]];
            TheSide.MeshLines[1] = new Vector[NNPE_Side[1]];
            TheSide.MeshLines[2] = new Vector[NNPE_Side[0]];
            TheSide.MeshLines[3] = new Vector[NNPE_Side[1]];
            for (int i = 0; i < NNPE_Side[0]; i++)
            {
                TheSide.MeshLines[0][i] = TheSide.NodeLocations[i];
                TheSide.MeshLines[2][i] = TheSide.NodeLocations[NNPE_Side[0] * NNPE_Side[1]-1];
            }
            for (int i = 0; i < NNPE_Side[1]; i++)
            {
                TheSide.MeshLines[1][i] = TheSide.NodeLocations[NNPE_Side[0] * i - 1];
                TheSide.MeshLines[3][i] = TheSide.NodeLocations[NNPE_Side[0] * (NNPE_Side[1] - 1) - NNPE_Side[0] * i];
            }
            Make_GraphicsAreasForSide_local(Side_Nodes, NNPE_Side, Resolution, ref TheSide);
        }
        private void Get_Side_Nodes(Node_ND[] ElementNodes, out int[][] NNPE_Side, out Node_ND[][] Side_Nodes)
        {
            NNPE_Side = new int[3][];
            NNPE_Side[0] = new int[2];
            NNPE_Side[0][0] = NNPE_l;
            NNPE_Side[0][1] = NNPE_m;
            NNPE_Side[1] = new int[2];
            NNPE_Side[1][0] = NNPE_m;
            NNPE_Side[1][1] = NNPE_n;
            NNPE_Side[2] = new int[2];
            NNPE_Side[2][0] = NNPE_l;
            NNPE_Side[2][1] = NNPE_n;
            int[] NNPS = new int[3];
            NNPS[0] = NNPE_l * NNPE_m;
            NNPS[1] = NNPE_m * NNPE_n;
            NNPS[2] = NNPE_n * NNPE_l;
            Side_Nodes = new Node_ND[6][];
            for (int i = 0; i < 3; i++)
            {
                Side_Nodes[i] = new Node_ND[NNPS[i]];
                Side_Nodes[i+3] = new Node_ND[NNPS[i]];
            }
            int index = 0;
            for (int j = 0; j < NNPE_m; j++)
            {
                for (int i = 0; i < NNPE_l; i++)
                {
                    Side_Nodes[0][index] = ElementNodes[index];
                    Side_Nodes[3][index] = ElementNodes[NNPS[0] * (NNPE_n - 1) + index];
                    index++;
                }
            }
            index = 0;
            for (int j = 0; j < NNPE_n; j++)
            {
                for (int i = 0; i < NNPE_m; i++)
                {
                    Side_Nodes[1][index] = ElementNodes[j * NNPS[0] + NNPE_l * i];
                    Side_Nodes[4][index] = ElementNodes[j * NNPS[0] + (NNPE_l - 1) * i];
                    index++; ;
                }
            }
            index = 0;
            for (int j = 0; j < NNPE_n; j++)
            {
                for (int i = 0; i < NNPE_l; i++)
                {
                    Side_Nodes[2][index] = ElementNodes[j * NNPS[0] + i];
                    Side_Nodes[5][index] = ElementNodes[j * NNPS[0] + (NNPE_l - 1) + NNPE_l * (NNPE_m - 1) + i];
                    index++; ;
                }
            }
        }
        private void Make_GraphicsAreas(Node_ND[] Side_Nodes, int[] NNPE_Side, int Resolution, ref Side TheSide)
        {
            ParametricInterpolation_ND_Square Node_Interpolator = new ParametricInterpolation_ND_Square(NNPE_Side[0], NNPE_Side[1]);
            int Res_NNPE_l = Resolution * (NNPE_Side[0] - 1) + 1;
            int Res_NNPE_m = Resolution * (NNPE_Side[1] - 1) + 1;
            ParametricInterpolation_ND_Square Resolution_Interpolaor = new ParametricInterpolation_ND_Square(Res_NNPE_l, Res_NNPE_m);
            Vector Area_DisplayValues = Calculate_Area_DisplayValues(Side_Nodes, Node_Interpolator, Resolution_Interpolaor);
            Vector[] Area_X = Calculate_Area_Locations(Side_Nodes, Node_Interpolator, Resolution_Interpolaor);
            int NA = Resolution * (NNPE_Side[0] - 1) * Resolution * (NNPE_Side[1] - 1);
            TheSide.Areas = new Vector[NA][];
            TheSide.AreaValues = new Vector[NA];
            int index_1 = 0;
            int index_2 = NNPE_Side[0];
            int index = 0;
            for (int j = 0; j < Res_NNPE_m - 1; j++)
            {
                for (int i = 0; i < Res_NNPE_l - 1; i++)
                {
                    TheSide.Areas[index] = new Vector[4];
                    TheSide.AreaValues[index] = new Vector(4);
                    TheSide.Areas[index][0] = Area_X[index_1 + i];
                    TheSide.Areas[index][1] = Area_X[index_1 + i + 1];
                    TheSide.Areas[index][2] = Area_X[index_2 + i + 1];
                    TheSide.Areas[index][3] = Area_X[index_2 + i];
                    //TheSide.Areas[index][4] = TheSide.Areas[index][0];
                    TheSide.AreaValues[index].Values[0] = Area_DisplayValues.Values[index_1 + i];
                    TheSide.AreaValues[index].Values[1] = Area_DisplayValues.Values[index_1 + i + 1];
                    TheSide.AreaValues[index].Values[2] = Area_DisplayValues.Values[index_2 + i + 1];
                    TheSide.AreaValues[index].Values[3] = Area_DisplayValues.Values[index_2 + i];
                    //TheSide.AreaValues[index].Values[4] = TheSide.AreaValues[index].Values[0];
                    index++;
                }
                index_1 = index_2;
                index_2 += NNPE_Side[0];
            }
        }
        private void Make_GraphicsAreas(Node_ND[] Side_Nodes, int[] NNPE_Side, ref Side TheSide)
        {
            Vector Node_DisplayValues = Get_Nodal_DisplayValues(Side_Nodes);
            int NA = (NNPE_Side[0] - 1) * (NNPE_Side[1] - 1);
            TheSide.Areas = new Vector[NA][];
            TheSide.AreaValues = new Vector[NA];
            int index_1 = 0;
            int index_2 = NNPE_Side[0];
            int index = 0;
            for (int j = 0; j < NNPE_Side[1] - 1; j++)
            {
                for (int i = 0; i < NNPE_Side[0] - 1; i++)
                {
                    TheSide.Areas[index] = new Vector[4];
                    TheSide.AreaValues[index] = new Vector(4);
                    TheSide.Areas[index][0] = TheSide.NodeLocations[index_1 + i];
                    TheSide.Areas[index][1] = TheSide.NodeLocations[index_1 + i + 1];
                    TheSide.Areas[index][2] = TheSide.NodeLocations[index_2 + i + 1];
                    TheSide.Areas[index][3] = TheSide.NodeLocations[index_2 + i];
                    //TheSide.Areas[index][4] = TheSide.Areas[index][0];
                    TheSide.AreaValues[index].Values[0] = Node_DisplayValues.Values[index_1 + i];
                    TheSide.AreaValues[index].Values[1] = Node_DisplayValues.Values[index_1 + i + 1];
                    TheSide.AreaValues[index].Values[2] = Node_DisplayValues.Values[index_2 + i + 1];
                    TheSide.AreaValues[index].Values[3] = Node_DisplayValues.Values[index_2 + i];
                    //TheSide.AreaValues[index].Values[4] = TheSide.AreaValues[index].Values[0];
                    index++;
                }
                index_1 = index_2;
                index_2 += NNPE_Side[0];
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
        private Vector Calculate_Area_DisplayValues(Node_ND[] ElementNodes, ParametricInterpolation_ND_Square Nodes_Interpolator, ParametricInterpolation_ND_Square TheResInterpolation)
        {
            int NNPE = TheResInterpolation.NNPE;
            Vector DisplayValues = new Vector(NNPE);
            Vector NodalDisplayValues = Get_Nodal_DisplayValues(ElementNodes);
            for (int i = 0; i < NNPE; i++)
            {
                DisplayValues.Values[i] = Nodes_Interpolator.Interpolate_Variable(TheResInterpolation.NodeXi[i], NodalDisplayValues.Values);
            }
            return DisplayValues;
        }
        private Vector[] Calculate_Area_Locations(Node_ND[] ElementNodes, ParametricInterpolation_ND_Square Nodes_Interpolator, ParametricInterpolation_ND_Square TheResInterpolation)
        {
            int NNPE = TheResInterpolation.NNPE;
            Vector[] X = new Vector[NNPE];
            Vector[] Nodal_X = Get_ElementNodal_X(ElementNodes);
            for (int i = 0; i < NNPE; i++)
            {
                X[i] = Nodes_Interpolator.Interpolate_Variable(TheResInterpolation.NodeXi[i], Nodal_X);
            }
            return X;
        }
        public void Make_GraphicsAreasForSide_local(Node_ND[] Side_Nodes, int[] NNPE_Side,  int Resolution, ref Side TheSide)
        {
            if (Resolution == 1)
            {
                Make_GraphicsAreas(Side_Nodes, NNPE_Side, ref TheSide);
            }
            else
            {
                Make_GraphicsAreas(Side_Nodes, NNPE_Side, Resolution, ref TheSide);
            }
        }
        public override void Change_GraphicsSurfaces_Values(Node_ND[] ElementNodes, int Resolution, ref Surfaces TheInputSurfaces)
        {
            Node_ND[][] Side_Nodes;
            int[][] NNPE_Side;
            Get_Side_Nodes(ElementNodes, out NNPE_Side, out Side_Nodes);
            for (int i = 0; i < 3; i++)
            {
                Change_GraphicsSurfaces_Values_local(Side_Nodes[i], NNPE_Side[i], Resolution, ref TheInputSurfaces.Sides[i]);
                Change_GraphicsSurfaces_Values_local(Side_Nodes[i + 3], NNPE_Side[i], Resolution, ref TheInputSurfaces.Sides[i + 3]);
            }

        }
        private void Change_GraphicsSurfaces_Values_local(Node_ND[] Side_Nodes, int[] NNPE_Side, int Resolution, ref Side TheSide)
        {
            if (Resolution == 1)
            {
                Chage_GraphicsAreas_Values(Side_Nodes, NNPE_Side, ref TheSide);
            }
            else
            {
                Chage_GraphicsAreas_Values(Side_Nodes, NNPE_Side, Resolution, ref TheSide);
            }
        }
        private void Chage_GraphicsAreas_Values(Node_ND[] Side_Nodes, int[] NNPE_Side, int Resolution, ref Side TheSide)
        {
            ParametricInterpolation_ND_Square Node_Interpolator = new ParametricInterpolation_ND_Square(NNPE_Side[0], NNPE_Side[1]);
            int Res_NNPE_l = Resolution * (NNPE_Side[0] - 1) + 1;
            int Res_NNPE_m = Resolution * (NNPE_Side[1] - 1) + 1;
            ParametricInterpolation_ND_Square Resolution_Interpolaor = new ParametricInterpolation_ND_Square(Res_NNPE_l, Res_NNPE_m);
            Vector Area_DisplayValues = Calculate_Area_DisplayValues(Side_Nodes, Node_Interpolator, Resolution_Interpolaor);
            int NA = Resolution * (NNPE_Side[0] - 1) * Resolution * (NNPE_Side[1] - 1);
            TheSide.AreaValues = new Vector[NA];
            int index_1 = 0;
            int index_2 = NNPE_Side[0];
            int index = 0;
            for (int j = 0; j < Res_NNPE_m - 1; j++)
            {
                for (int i = 0; i < Res_NNPE_l - 1; i++)
                {
                    TheSide.AreaValues[index] = new Vector(4);
                    TheSide.AreaValues[index].Values[0] = Area_DisplayValues.Values[index_1 + i];
                    TheSide.AreaValues[index].Values[1] = Area_DisplayValues.Values[index_1 + i + 1];
                    TheSide.AreaValues[index].Values[2] = Area_DisplayValues.Values[index_2 + i + 1];
                    TheSide.AreaValues[index].Values[3] = Area_DisplayValues.Values[index_2 + i];
                    //TheSide.AreaValues[index].Values[4] = TheSide.AreaValues[index].Values[0];
                    index++;
                }
                index_1 = index_2;
                index_2 += NNPE_Side[0];
            }
        }
        private void Chage_GraphicsAreas_Values(Node_ND[] Side_Nodes, int[] NNPE_Side, ref Side TheSide)
        {
            Vector Node_DisplayValues = Get_Nodal_DisplayValues(Side_Nodes);
            int NA = (NNPE_Side[0] - 1) * (NNPE_Side[1] - 1);
            TheSide.AreaValues = new Vector[NA];
            int index_1 = 0;
            int index_2 = NNPE_Side[0];
            int index = 0;
            for (int j = 0; j < NNPE_Side[1] - 1; j++)
            {
                for (int i = 0; i < NNPE_Side[0] - 1; i++)
                {
                    TheSide.AreaValues[index] = new Vector(4);
                    TheSide.AreaValues[index].Values[0] = Node_DisplayValues.Values[index_1 + i];
                    TheSide.AreaValues[index].Values[1] = Node_DisplayValues.Values[index_1 + i + 1];
                    TheSide.AreaValues[index].Values[2] = Node_DisplayValues.Values[index_2 + i + 1];
                    TheSide.AreaValues[index].Values[3] = Node_DisplayValues.Values[index_2 + i];
                    //TheSide.AreaValues[index].Values[4] = TheSide.AreaValues[index].Values[0];
                    index++;
                }
                index_1 = index_2;
                index_2 += NNPE_Side[0];
            }
        }
        #endregion
    }
}
