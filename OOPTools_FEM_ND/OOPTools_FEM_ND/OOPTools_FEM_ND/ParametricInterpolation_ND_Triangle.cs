using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using OOPTools_Math;
using OOPTools_FEM_1D;

namespace OOPTools_FEM_ND
{
    [Serializable]
    public class ParametricInterpolation_ND_Triangle : ParametricInterpolation_ND
    {
        /// <summary>
        /// Developed by: Mehrdad Negahban
        /// Date: 11/23/2012
        ///
        /// Purpose:    Provide interpolation shape functions and their derivatives for 1x1 right triangle
        /// Comments:   Xi is the parametric variable of space
        ///             X is the real variable of space
        ///             Corners 0 = (1,0), 1 = (0,1), 2 = (0,0)
        ///             Area coordinate: (Xi[0], Xi[1], 1-Xi[0]-Xi[1])
        ///             NNPE_m is number of nodes per side
        ///             
        /// Date modified:
        /// Modified by:
        /// Comments:
        /// </summary>
        public int NNPE_m;
        private double DXi;
        public  ParametricInterpolation_ND_Triangle()
        {
        }
        public ParametricInterpolation_ND_Triangle(int nnpe_m)
        {
            NNPE_m = nnpe_m;
            Calculate_NumberOfNodesPerElement();
            NodeXi = Calculate_ElementNodal_Xi();
        }
        
        #region Utility tools
        public void Calculate_NumberOfNodesPerElement()
        {
            NNPE = NNPE_m * (NNPE_m - 1) / 2 + NNPE_m;
        }
        public override Vector[] Calculate_ElementNodal_Xi()
        {
            Vector[] Xi = new Vector[NNPE];
            DXi = 1.0 / Convert.ToDouble(NNPE_m - 1);
            int index = 0;
            for (int j = 0; j < NNPE_m; j++)
            {
                double Xi_1 = Convert.ToDouble(j)*DXi;
                for (int i = 0; i < NNPE_m - j; i++)
                {
                    Xi[index] = new Vector(2);
                    Xi[index].Values[0] = Convert.ToDouble(i) * DXi;
                    Xi[index].Values[1] = Xi_1;
                    index++;
                }
            }
            return Xi;
        }
        #endregion

        #region Interpolatin tools
        public override Vector Calculate_ShapeFunctions(Vector Xi)
        {
            if (NNPE_m == 2)
            {
                return Calculate_ShapeFunctions_2(Xi);
            }
            else
            {
                return Calculate_ShapeFunctions_M(Xi);
            }
        }
        private Vector Calculate_ShapeFunctions_2(Vector Xi)
        {
            Vector N = new Vector(3);
            N.Values[0] = 1.0D - Xi.Values[0] - Xi.Values[1];
            N.Values[1] = Xi.Values[0];
            N.Values[2] = Xi.Values[1];
            return N;
        }
        private Vector Calculate_ShapeFunctions_M(Vector Xi)
        {
            Vector N = new Vector(NNPE);
            int index = 0;
            double Xi_0 = Xi.Values[0];
            double Xi_1 = Xi.Values[1];
            double Xi_2 = -Xi_0 - Xi_1;
            for (int j = 0; j < NNPE_m; j++)
            {
                for (int i = 0; i < NNPE_m - j; i++)
                {
                    double Num = 1.0D;
                    double Den = 1.0D;
                    double Node_Xi_0 = NodeXi[index].Values[0];
                    double Node_Xi_1 = NodeXi[index].Values[1];
                    double Node_Xi_2 = -Node_Xi_0 - Node_Xi_1;
                    for(int i_1 =0; i_1<i; i_1++)
                    {
                        double Xi_zero = Convert.ToDouble(i_1) * DXi;
                        Num *= Xi_0 - Xi_zero;
                        Den *= Node_Xi_0 - Xi_zero;
                    }
                    for (int j_1 = 0; j_1 < j; j_1++)
                    {
                        double Xi_zero = Convert.ToDouble(j_1) * DXi;
                        Num *= Xi_1 - Xi_zero;
                        Den *= Node_Xi_1 - Xi_zero;
                    }
                    double Xi_j = Convert.ToDouble(j) * DXi;
                    for (int i_1 = i + 1; i_1 < NNPE_m - j; i_1++)
                    {
                        double Xi_zero = -Convert.ToDouble(i_1) * DXi - Xi_j;
                        Num *= Xi_2 - Xi_zero;
                        Den *= Node_Xi_2 - Xi_zero;
                    }
                    
                    N.Values[index] = Num / Den;
                    index++;
                }
            }
            return N;
        }
        public override Vector[] Calculate_DerivativesOfShapeFunctions_WRTXi(Vector Xi)
        {
            if (NNPE_m == 2)
            {
                return Calculate_DerivativesOfShapeFunctions_WRTXi_2(Xi);
            }
            else
            {
                return Calculate_DerivativesOfShapeFunctions_WRTXi_M(Xi);
            }
        }
        private Vector[] Calculate_DerivativesOfShapeFunctions_WRTXi_2(Vector Xi)
        {
            Vector[] dNdXi = new Vector[3];
            dNdXi[0] = new Vector(2);
            dNdXi[0].Values[0] = -1.0D;
            dNdXi[0].Values[1] = -1.0D;
            dNdXi[1] = new Vector(2);
            dNdXi[1].Values[0] = 1.0D;
            dNdXi[2] = new Vector(2);
            dNdXi[2].Values[1] = 1.0D;

            return dNdXi;
        }
        private Vector[] Calculate_DerivativesOfShapeFunctions_WRTXi_M(Vector Xi)
        {
            Vector[] dNdXi = new Vector[NNPE];
            int index = 0;
            double Xi_0 = Xi.Values[0];
            double Xi_1 = Xi.Values[1];
            double Xi_2 = -Xi_0 - Xi_1;
            for (int j = 0; j < NNPE_m; j++)
            {
                for (int i = 0; i < NNPE_m - j; i++)
                {
                    double Num_1 = 1.0D;
                    double Num_2 = 1.0D;
                    double Num_3 = 1.0D;
                    double Den = 1.0D;
                    double Node_Xi_0 = NodeXi[index].Values[0];
                    double Node_Xi_1 = NodeXi[index].Values[1];
                    double Node_Xi_2 = -Node_Xi_0 - Node_Xi_1;
                    for (int i_1 = 0; i_1 < i; i_1++)
                    {
                        double Xi_zero = Convert.ToDouble(i_1) * DXi;
                        Num_1 *= Xi_0 - Xi_zero;
                        Den *= Node_Xi_0 - Xi_zero;
                    }
                    for (int j_1 = 0; j_1 < j; j_1++)
                    {
                        double Xi_zero = Convert.ToDouble(j_1) * DXi;
                        Num_2 *= Xi_1 - Xi_zero;
                        Den *= Node_Xi_1 - Xi_zero;
                    }
                    double Xi_j = Convert.ToDouble(j) * DXi;
                    for (int i_1 = i + 1; i_1 < NNPE_m - j; i_1++)
                    {
                        double Xi_zero = -Convert.ToDouble(i_1) * DXi - Xi_j;
                        Num_3 *= Xi_2 - Xi_zero;
                        Den *= Node_Xi_2 - Xi_zero;
                    }

                    dNdXi[index] = new Vector(2);
                    double Temp_1 = Num_2 * Num_3; 
                    for (int m = 0; m < i; m++)
                    {
                        double Temp = 1.0D;
                        for (int i_1 = 0; i_1 < i; i_1++)
                        {
                            double Xi_zero = Convert.ToDouble(i_1) * DXi;
                            if(m!=i_1) Temp *= Xi_0- Xi_zero;
                        }
                        dNdXi[index].Values[0] += Temp_1 * Temp; 
                    }
                    Temp_1 = Num_1 * Num_3;
                    for (int m = 0; m < j; m++)
                    {
                        double Temp = 1.0D;
                        for (int j_1 = 0; j_1 < j; j_1++)
                        {
                            double Xi_zero = Convert.ToDouble(j_1) * DXi;
                            if (m != j_1) Temp *= Xi_1 - Xi_zero;
                        }
                        dNdXi[index].Values[1] += Temp_1 * Temp;
                    }
                    Temp_1 = Num_1 * Num_2;
                    for (int m = i + 1; m < NNPE_m - j; m++)
                    {
                        double Temp = 1.0D;
                        for (int i_1 = i + 1; i_1 < NNPE_m - j; i_1++)
                        {
                            double Xi_zero = -Convert.ToDouble(i_1) * DXi - Xi_j;
                            if (m != i_1) Temp *= Xi_2 - Xi_zero;
                        }
                        double Temp_2 = Temp_1 * Temp;
                        dNdXi[index].Values[0] -= Temp_2;
                        dNdXi[index].Values[1] -= Temp_2;
                    }
                    dNdXi[index].Values[0] /= Den;
                    dNdXi[index].Values[1] /= Den;
                    index++;
                }
            }
            return dNdXi;
        }
        #endregion

        #region Graphics surfaces tools
        public override Surfaces Make_GraphicsSurfaces(Node_ND[] ElementNodes, int Resolution)
        {
            Surfaces TheSurfaces = new Surfaces();
            TheSurfaces.Sides = new Side[1];
            TheSurfaces.Sides[0] = new Side();
            Side TheSide = new Side();
            TheSide.NodeLocations = Get_ElementNodal_X(ElementNodes);
            TheSide.MeshLines = new Vector[3][];
            TheSide.MeshLines[0] = new Vector[NNPE_m];
            TheSide.MeshLines[1] = new Vector[NNPE_m];
            TheSide.MeshLines[2] = new Vector[NNPE_m];
            int index_1 = 0;
            for (int i = 0; i < NNPE_m; i++)
            {
                TheSide.MeshLines[0][i] = TheSide.NodeLocations[i];
                TheSide.MeshLines[1][i] = TheSide.NodeLocations[index_1 + NNPE_m - 1];
                TheSide.MeshLines[2][NNPE_m - 1 - i] = TheSide.NodeLocations[index_1];
                index_1 += NNPE_m - i;
            }
            TheSurfaces.Sides[0] = TheSide;
            Make_GraphicsAreasForSide(ElementNodes, Resolution, ref TheSurfaces.Sides[0]);
            return TheSurfaces;
        }
        private void Make_GraphicsAreas(Node_ND[] ElementNodes, int Resolution, ref Side TheSide)
        {
            int Res_NNPE_m = Resolution * (NNPE_m - 1) + 1;
            ParametricInterpolation_ND_Triangle TempInter = new ParametricInterpolation_ND_Triangle(Res_NNPE_m);
            Vector Area_DisplayValues = Calculate_Area_DisplayValues(ElementNodes, TempInter);
            Vector[] Area_X = Calculate_Area_Locations(ElementNodes, TempInter);
            int NA = Resolution * (NNPE_m - 1) * Resolution * (NNPE_m - 1);
            TheSide.Areas = new Vector[NA][];
            TheSide.AreaValues = new Vector[NA];
            int index_1 = 0;
            int index_2 = Resolution * (NNPE_m - 1) + 1;
            int index = 0;
            for (int i = 0; i < Resolution * (NNPE_m - 1); i++)
            {
                int N = Resolution * (NNPE_m - 1) + 1 - i;
                for (int j = 0; j < N-2; j++)
                {
                    TheSide.Areas[index] = new Vector[3];
                    TheSide.AreaValues[index] = new Vector(3);
                    TheSide.Areas[index][0] = Area_X[index_1 + j];
                    TheSide.Areas[index][1] = Area_X[index_1 + j + 1];
                    TheSide.Areas[index][2] = Area_X[index_2 + j];
                    //TheSide.Areas[index][3] = TheSide.Areas[index][0];
                    TheSide.AreaValues[index].Values[0] = Area_DisplayValues.Values[index_1 + j];
                    TheSide.AreaValues[index].Values[1] = Area_DisplayValues.Values[index_1 + j + 1];
                    TheSide.AreaValues[index].Values[2] = Area_DisplayValues.Values[index_2 + j];
                    //TheSide.AreaValues[index].Values[3] = TheSide.AreaValues[index].Values[0];
                    index++;
                    TheSide.Areas[index] = new Vector[3];
                    TheSide.AreaValues[index] = new Vector(3);
                    TheSide.Areas[index][0] = Area_X[index_1 + j + 1];
                    TheSide.Areas[index][1] = Area_X[index_2 + j + 1];
                    TheSide.Areas[index][2] = Area_X[index_2 + j];
                    //TheSide.Areas[index][3] = TheSide.Areas[index][0];
                    TheSide.AreaValues[index].Values[0] = Area_DisplayValues.Values[index_1 + j + 1];
                    TheSide.AreaValues[index].Values[1] = Area_DisplayValues.Values[index_2 + j + 1];
                    TheSide.AreaValues[index].Values[2] = Area_DisplayValues.Values[index_2 + j];
                    //TheSide.AreaValues[index].Values[3] = TheSide.AreaValues[index].Values[0];
                    index++;
                }
                TheSide.Areas[index] = new Vector[3];
                TheSide.AreaValues[index] = new Vector(3);
                TheSide.Areas[index][0] = Area_X[index_1 + N - 2];
                TheSide.Areas[index][1] = Area_X[index_1 + N - 1];
                TheSide.Areas[index][2] = Area_X[index_2 + N - 2];
                //TheSide.Areas[index][3] = TheSide.Areas[index][0];
                TheSide.AreaValues[index].Values[0] = Area_DisplayValues.Values[index_1 + N - 2];
                TheSide.AreaValues[index].Values[1] = Area_DisplayValues.Values[index_1 + N - 1];
                TheSide.AreaValues[index].Values[2] = Area_DisplayValues.Values[index_2 + N - 2];
                //TheSide.AreaValues[index].Values[3] = TheSide.AreaValues[index].Values[0];
                index++;

                index_1 = index_2;
                index_2 += N-1;
            }
        }
        private void Make_GraphicsAreas(Node_ND[] ElementNodes, ref Side TheSide)
        {
            Vector Node_DisplayValues = Get_Nodal_DisplayValues(ElementNodes);
            int NA = (NNPE_m - 1) * (NNPE_m - 1);
            TheSide.Areas = new Vector[NA][];
            TheSide.AreaValues = new Vector[NA];
            int index_1 = 0;
            int index_2 = (NNPE_m - 1) + 1;
            int index = 0;
            for (int i = 0; i < (NNPE_m - 1); i++)
            {
                int N = (NNPE_m - 1) + 1 - i;
                for (int j = 0; j < N - 2; j++)
                {
                    TheSide.Areas[index] = new Vector[3];
                    TheSide.AreaValues[index] = new Vector(3);
                    TheSide.Areas[index][0] = TheSide.NodeLocations[index_1 + j];
                    TheSide.Areas[index][1] = TheSide.NodeLocations[index_1 + j + 1];
                    TheSide.Areas[index][2] = TheSide.NodeLocations[index_2 + j];
                    //TheSide.Areas[index][3] = TheSide.Areas[index][0];
                    TheSide.AreaValues[index].Values[0] = Node_DisplayValues.Values[index_1 + j];
                    TheSide.AreaValues[index].Values[1] = Node_DisplayValues.Values[index_1 + j + 1];
                    TheSide.AreaValues[index].Values[2] = Node_DisplayValues.Values[index_2 + j];
                    //TheSide.AreaValues[index].Values[3] = TheSide.AreaValues[index].Values[0];
                    index++;
                    TheSide.Areas[index] = new Vector[4];
                    TheSide.AreaValues[index] = new Vector(4);
                    TheSide.Areas[index][0] = TheSide.NodeLocations[index_1 + j + 1];
                    TheSide.Areas[index][1] = TheSide.NodeLocations[index_2 + j + 1];
                    TheSide.Areas[index][2] = TheSide.NodeLocations[index_2 + j];
                    //TheSide.Areas[index][3] = TheSide.Areas[index][0];
                    TheSide.AreaValues[index].Values[0] = Node_DisplayValues.Values[index_1 + j + 1];
                    TheSide.AreaValues[index].Values[1] = Node_DisplayValues.Values[index_2 + j + 1];
                    TheSide.AreaValues[index].Values[2] = Node_DisplayValues.Values[index_2 + j];
                    //TheSide.AreaValues[index].Values[3] = TheSide.AreaValues[index].Values[0];
                    index++;
                }
                TheSide.Areas[index] = new Vector[3];
                TheSide.AreaValues[index] = new Vector(3);
                TheSide.Areas[index][0] = TheSide.NodeLocations[index_1 + N - 2];
                TheSide.Areas[index][1] = TheSide.NodeLocations[index_1 + N - 1];
                TheSide.Areas[index][2] = TheSide.NodeLocations[index_2 + N - 2];
                //TheSide.Areas[index][3] = TheSide.Areas[index][0];
                TheSide.AreaValues[index].Values[0] = Node_DisplayValues.Values[index_1 + N - 2];
                TheSide.AreaValues[index].Values[1] = Node_DisplayValues.Values[index_1 + N - 1];
                TheSide.AreaValues[index].Values[2] = Node_DisplayValues.Values[index_2 + N - 2];
                //TheSide.AreaValues[index].Values[3] = TheSide.AreaValues[index].Values[0];
                index++;

                index_1 = index_2;
                index_2 += N - 1;
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
        private Vector Calculate_Area_DisplayValues(Node_ND[] ElementNodes, ParametricInterpolation_ND_Triangle TheResInterpolation)
        {
            int NNPE = TheResInterpolation.NNPE;
            Vector DisplayValues = new Vector(NNPE);
            Vector NodalDisplayValues = Get_Nodal_DisplayValues(ElementNodes);
            for (int i = 0; i < NNPE; i++)
            {
                DisplayValues.Values[i] = Interpolate_Variable(TheResInterpolation.NodeXi[i],NodalDisplayValues.Values);
            }
            return DisplayValues;
        }
        private Vector[] Calculate_Area_Locations(Node_ND[] ElementNodes, ParametricInterpolation_ND_Triangle TheResInterpolation)
        {
            int NNPE = TheResInterpolation.NNPE;
            Vector[] X = new Vector[NNPE];
            Vector[] Nodal_X = Get_ElementNodal_X(ElementNodes);
            for (int i = 0; i < NNPE; i++)
            {
                X[i] = Interpolate_Variable(TheResInterpolation.NodeXi[i], Nodal_X);
            }
            return X;
        }
        public override void Make_GraphicsAreasForSide(Node_ND[] ElementNodes, int Resolution, ref Side TheSide)
        {
            if (Resolution == 1)
            {
                Make_GraphicsAreas(ElementNodes, ref TheSide);
            }
            else
            {
                Make_GraphicsAreas(ElementNodes, Resolution, ref TheSide);
            }
        }
        public override void Change_GraphicsSurfaces_Values(Node_ND[] ElementNodes, int Resolution, ref Side TheSide)
        {
            if (Resolution == 1)
            {
                Chage_GraphicsAreas_Values(ElementNodes, ref TheSide);
            }
            else
            {
                Chage_GraphicsAreas_Values(ElementNodes, ref TheSide, Resolution);
            }
        }
        private void Chage_GraphicsAreas_Values(Node_ND[] ElementNodes, ref Side TheSide, int Resolution)
        {
            int Res_NNPE_m = Resolution * (NNPE_m - 1) + 1;
            ParametricInterpolation_ND_Triangle TempInter = new ParametricInterpolation_ND_Triangle(Res_NNPE_m);
            Vector Area_DisplayValues = Calculate_Area_DisplayValues(ElementNodes, TempInter);
            int NA = Resolution * (NNPE_m - 1) * Resolution * (NNPE_m - 1);
            TheSide.AreaValues = new Vector[NA];
            int index_1 = 0;
            int index_2 = Resolution * (NNPE_m - 1) + 1;
            int index = 0;
            for (int i = 0; i < Resolution * (NNPE_m - 1); i++)
            {
                int N = Resolution * (NNPE_m - 1) + 1 - i;
                for (int j = 0; j < N - 2; j++)
                {
                    TheSide.AreaValues[index] = new Vector(3);
                    TheSide.AreaValues[index].Values[0] = Area_DisplayValues.Values[index_1 + j];
                    TheSide.AreaValues[index].Values[1] = Area_DisplayValues.Values[index_1 + j + 1];
                    TheSide.AreaValues[index].Values[2] = Area_DisplayValues.Values[index_2 + j];
                    //TheSide.AreaValues[index].Values[3] = TheSide.AreaValues[index].Values[0];
                    index++;
                    TheSide.AreaValues[index] = new Vector(3);
                    TheSide.AreaValues[index].Values[0] = Area_DisplayValues.Values[index_1 + j + 1];
                    TheSide.AreaValues[index].Values[1] = Area_DisplayValues.Values[index_2 + j + 1];
                    TheSide.AreaValues[index].Values[2] = Area_DisplayValues.Values[index_2 + j];
                    //TheSide.AreaValues[index].Values[3] = TheSide.AreaValues[index].Values[0];
                    index++;
                }
                TheSide.AreaValues[index] = new Vector(3);
                TheSide.AreaValues[index].Values[0] = Area_DisplayValues.Values[index_1 + N - 2];
                TheSide.AreaValues[index].Values[1] = Area_DisplayValues.Values[index_1 + N - 1];
                TheSide.AreaValues[index].Values[2] = Area_DisplayValues.Values[index_2 + N - 2];
                //TheSide.AreaValues[index].Values[3] = TheSide.AreaValues[index].Values[0];
                index++;

                index_1 = index_2;
                index_2 += N - 1;
            }
        }
        private void Chage_GraphicsAreas_Values(Node_ND[] ElementNodes, ref Side TheSide)
        {
            Vector Node_DisplayValues = Get_Nodal_DisplayValues(ElementNodes);
            int NA = (NNPE_m - 1) * (NNPE_m - 1);
            TheSide.AreaValues = new Vector[NA];
            int index_1 = 0;
            int index_2 = (NNPE_m - 1) + 1;
            int index = 0;
            for (int i = 0; i < (NNPE_m - 1); i++)
            {
                int N = (NNPE_m - 1) + 1 - i;
                for (int j = 0; j < N - 2; j++)
                {
                    TheSide.AreaValues[index] = new Vector(3);
                    TheSide.AreaValues[index].Values[0] = Node_DisplayValues.Values[index_1 + j];
                    TheSide.AreaValues[index].Values[1] = Node_DisplayValues.Values[index_1 + j + 1];
                    TheSide.AreaValues[index].Values[2] = Node_DisplayValues.Values[index_2 + j];
                    //TheSide.AreaValues[index].Values[3] = TheSide.AreaValues[index].Values[0];
                    index++;
                    TheSide.AreaValues[index] = new Vector(3);
                    TheSide.AreaValues[index].Values[0] = Node_DisplayValues.Values[index_1 + j + 1];
                    TheSide.AreaValues[index].Values[1] = Node_DisplayValues.Values[index_2 + j + 1];
                    TheSide.AreaValues[index].Values[2] = Node_DisplayValues.Values[index_2 + j];
                    //TheSide.AreaValues[index].Values[3] = TheSide.AreaValues[index].Values[0];
                    index++;
                }
                TheSide.AreaValues[index] = new Vector(3);
                TheSide.AreaValues[index].Values[0] = Node_DisplayValues.Values[index_1 + N - 2];
                TheSide.AreaValues[index].Values[1] = Node_DisplayValues.Values[index_1 + N - 1];
                TheSide.AreaValues[index].Values[2] = Node_DisplayValues.Values[index_2 + N - 2];
                //TheSide.AreaValues[index].Values[3] = TheSide.AreaValues[index].Values[0];
                index++;

                index_1 = index_2;
                index_2 += N - 1;
            }
        }
        #endregion
    }
}
