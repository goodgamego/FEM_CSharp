using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using OOPTools_Graphics;
using OOPTools_Math;
using OOPTools_FEM_ND;
using OOPTools_FEM_1D;
using ND_Elastic;

//using OOPTools_Graphics;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Drawing.Imaging;
using System.IO;

namespace ND_Elastic_Project
{
public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            SetupPlotSelectionTypeListBox();
            SetupPlotTypeCheckedListBox();
            SetupDirectionListBox();
            SetupLeftBcListBox();
            SetupRightBcListBox();
        }
        FEMSolver_ND_Static FEMSolver;
        private void RunButton_Click(object sender, EventArgs e)
        {
            FEMSolver = new FEMSolver_ND_Static();
            int NE_W = Convert.ToInt32(NE_WTextBox.Text);
            int NE_H = Convert.ToInt32(NE_HTextBox.Text); ;
            int NE = NE_W * NE_H;
            int NNPE_W = Convert.ToInt32(NNPE_WTextBox.Text);
            int NNPE_H = Convert.ToInt32(NNPE_HTextBox.Text);
            int NN_W = NE_W * (NNPE_W - 1) + 1;
            int NN_H = NE_H * (NNPE_H - 1) + 1;
            int NN = NN_W * NN_H;
            
            FEMSolver.Unknowns_NDoF = 2*NN;//Define D.O.F.s

            int index = 0;
            Node_ND[] Nodes = new Node_ND[NN];//set up node set
            double Width = Convert.ToDouble(WTextBox.Text);
            double Height = Convert.ToDouble(HTextBox.Text);
            Vector DX1 = new Vector(2);
            DX1.Values[0] = Width / Convert.ToDouble(NN_W - 1);
            Vector DX2 = new Vector(2);
            DX2.Values[1] = Height / Convert.ToDouble(NN_H - 1);
            for (int j = 0; j < NN_H; j++)
            {
              for (int i = 0; i < NN_W; i++)
                {
                   Nodes[index] = new Node_ND();//set up node set
                   Nodes[index].X = Convert.ToDouble(i) * DX1 + Convert.ToDouble(j) * DX2;
                   Node_ND_Unknowns_VectorField Unknowns = new Node_ND_Unknowns_VectorField();
                   Unknowns.UnknownDoFs=new int[]{index*2,2*index+1};
                   Unknowns.Unknowns = new Vector(2);
                   Nodes[index].Unknowns = Unknowns;
                   index++;
                }
            }
            FEMSolver.Nodes = Nodes;

            Element_ND.Function_Scalar_X rho = new Element_ND.Function_Scalar_X(rho_Function);
            Element_ND.Function_Vector_X b = new Element_ND.Function_Vector_X(b_Function);
            ParametricInterpolation_ND_Square Interpolator = new ParametricInterpolation_ND_Square(NNPE_W, NNPE_H);
            
            //Interpolator for boundary element sets at two directions
            ParametricInterpolation_ND_Line Left_RightBCInterP = new ParametricInterpolation_ND_Line(NNPE_H);
            ParametricInterpolation_ND_Line Bottom_UpBCInterP = new ParametricInterpolation_ND_Line(NNPE_W);
            
            int NIP_Fe_W = Convert.ToInt32(Math.Ceiling(Convert.ToDouble((NNPE_W - 1) + 1) / 2.0D));
            int NIP_Fe_H = Convert.ToInt32(Math.Ceiling(Convert.ToDouble((NNPE_H - 1) + 1) / 2.0D));
            QuadratureRule_ND FeQuadRule = QuadratureRules_ND_Area.Make_QuadratureRule_Rectangular_Gaussian_MxNPoints(NIP_Fe_W+1, NIP_Fe_H+1);
            Integrator_ND_Quadrature FeQuad = new Integrator_ND_Quadrature(FeQuadRule);

            //Quadrature rules for Fe
            QuadratureRule_ND HorizontalBoundaryFQR = QuadratureRules_ND_Line.Make_QuadratureRule_Gaussian_NPoint(NIP_Fe_H + 1);
            Integrator_ND_Quadrature Horz_BFeQ = new Integrator_ND_Quadrature(HorizontalBoundaryFQR);
            QuadratureRule_ND Vetical_BoundaryFQR = QuadratureRules_ND_Line.Make_QuadratureRule_Gaussian_NPoint(NIP_Fe_W + 1);
            Integrator_ND_Quadrature Vert_BFeQ = new Integrator_ND_Quadrature(Vetical_BoundaryFQR);
            
            int NIP_Ke_W = Convert.ToInt32(Math.Ceiling(Convert.ToDouble((NNPE_W - 2) * (NNPE_W - 2) + 1) / 2.0D));
            int NIP_Ke_H = Convert.ToInt32(Math.Ceiling(Convert.ToDouble((NNPE_H - 2) * (NNPE_H - 2) + 1) / 2.0D));
            QuadratureRule_ND KeQuadRule = QuadratureRules_ND_Area.Make_QuadratureRule_Rectangular_Gaussian_MxNPoints(NIP_Ke_W+1, NIP_Ke_H+1);
            Integrator_ND_Quadrature KeQuad = new Integrator_ND_Quadrature(KeQuadRule);
            Element_ND_Elastic[] Elements = new Element_ND_Elastic[NE];
           
            double E = 71000000000;//Al elastic modulus
            double G = 26500000000;

            index = 0;
            for (int j = 0; j < NE_H; j++)
            {
                for (int i = 0; i < NE_W; i++)
                {
                    Elements[index] = new Element_ND_Elastic();
                    int Io = (NNPE_W - 1) * i +(NNPE_H - 1)*NN_W * j;
                    Elements[index].ElementNodes = new Node_ND[NNPE_W*NNPE_H];
                    int indexNodes =0;
                    for (int l = 0; l < NNPE_H; l++)
                    {
                        for (int m = 0; m < NNPE_W; m++)
                        {
                            Elements[index].ElementNodes[indexNodes] = Nodes[Io + m + NN_W * l];
                            indexNodes++;
                        }
                    }
                    Elements[index].rho = rho;
                    Elements[index].b = b;
                    Elements[index].langmuda = langmuda_Function(E,G);
                    Elements[index].miu = miu_Function(E, G);

                    Elements[index].Interpolator = Interpolator;
                    Elements[index].FeQuad = FeQuad;
                    Elements[index].KeQuad = KeQuad;
                    index++;
                }
            }
            FEMSolver.Elements = Elements;

            double U1_1 = Convert.ToDouble(U11TextBox.Text);
            double U1_2 = Convert.ToDouble(U12TextBox.Text);
            double U2_1 = Convert.ToDouble(U21TextBox.Text);
            double U2_2 = Convert.ToDouble(U22TextBox.Text);
            double F1_1 = Convert.ToDouble(F1_Xtextbox.Text);
            double F1_2 = Convert.ToDouble(F1_Ytextbox.Text);
            double F2_1 = Convert.ToDouble(F2_Xtextbox.Text);
            double F2_2 = Convert.ToDouble(F2_Ytextbox.Text);
            int NBE = 0;//NBE is number of node on on element side
            if (DirectionListBox.SelectedIndex == 0)
            {
                NBE = NE_H;
            }
            else NBE = NE_W;
              
            BoundaryElement_ND_Elastic_Line_SteadyState_DisplacementOnNode[] D_Left_BoundaryElements = new BoundaryElement_ND_Elastic_Line_SteadyState_DisplacementOnNode[NBE];
            BoundaryElement_ND_Elastic_Line_SteadyState_ForceOnNode[] F_Left_BoundaryElements = new BoundaryElement_ND_Elastic_Line_SteadyState_ForceOnNode[NBE];
            BoundaryElement_ND_Elastic_Line_SteadyState_DisplacementOnNode[] D_Right_BoundaryElements = new BoundaryElement_ND_Elastic_Line_SteadyState_DisplacementOnNode[NBE];
            BoundaryElement_ND_Elastic_Line_SteadyState_ForceOnNode[] F_Right_BoundaryElements = new BoundaryElement_ND_Elastic_Line_SteadyState_ForceOnNode[NBE];

            if (DirectionListBox.SelectedIndex == 0)//Left and Right
            {
                if (LeftBClistbox.SelectedIndex == 0)// Left Displacement
                {
                    for (int i = 0; i < NE_H; i++)
                    {
                        D_Left_BoundaryElements[i] = new BoundaryElement_ND_Elastic_Line_SteadyState_DisplacementOnNode();
                        D_Left_BoundaryElements[i].Displacements = new Matrix_Jagged(NNPE_H, 2);
                        D_Left_BoundaryElements[i].ElementNodes = new Node_ND[NNPE_H];
                        for (int j = 0; j < NNPE_H; j++)
                        {
                            D_Left_BoundaryElements[i].Displacements.Values[j][0] = U1_1;
                            D_Left_BoundaryElements[i].Displacements.Values[j][1] = U1_2;
                            D_Left_BoundaryElements[i].ElementNodes[j] = Nodes[i * (NNPE_H - 1) * NN_W + j * NN_W];
                        }
                    } 
                }
                if (LeftBClistbox.SelectedIndex == 1)//Left Force
                {
                    for (int i = 0; i < NE_H; i++)
                    {
                        F_Left_BoundaryElements[i] = new BoundaryElement_ND_Elastic_Line_SteadyState_ForceOnNode();
                        F_Left_BoundaryElements[i].ForceX = new Vector(NNPE_H);
                        F_Left_BoundaryElements[i].ForceY = new Vector(NNPE_H);
                        F_Left_BoundaryElements[i].ElementNodes = new Node_ND[NNPE_H];
                        F_Left_BoundaryElements[i].Interpolator = Left_RightBCInterP;
                        F_Left_BoundaryElements[i].FeQuad = Horz_BFeQ;
                        for (int j = 0; j < NNPE_H; j++)
                        {
                           F_Left_BoundaryElements[i].ForceX.Values[j]= F1_1;
                           F_Left_BoundaryElements[i].ForceY.Values[j] = F1_2;
                           F_Left_BoundaryElements[i].ElementNodes[j] = Nodes[i * (NNPE_H - 1) * NN_W + j * NN_W];
                        }
                    }
                }
                if (RightBClistbox.SelectedIndex == 0)// Right Displacement
                {
                    for (int i = 0; i < NE_H; i++)
                    {
                        D_Right_BoundaryElements[i] = new BoundaryElement_ND_Elastic_Line_SteadyState_DisplacementOnNode();
                        D_Right_BoundaryElements[i].Displacements = new Matrix_Jagged(NNPE_H, 2);
                        D_Right_BoundaryElements[i].ElementNodes = new Node_ND[NNPE_H];
                        for (int j = 0; j < NNPE_H; j++)
                        {
                            D_Right_BoundaryElements[i].Displacements.Values[j][0] = U2_1;
                            D_Right_BoundaryElements[i].Displacements.Values[j][1] = U2_2;
                            D_Right_BoundaryElements[i].ElementNodes[j] = Nodes[i * (NNPE_H - 1) * NN_W + j * NN_W + NN_W - 1];
                        }
                    }
                }
                if (RightBClistbox.SelectedIndex == 1)//Right Force
                {
                    for (int i = 0; i < NE_H; i++)
                    {
                        F_Right_BoundaryElements[i] = new BoundaryElement_ND_Elastic_Line_SteadyState_ForceOnNode();
                        F_Right_BoundaryElements[i].ForceX = new Vector(NNPE_H);
                        F_Right_BoundaryElements[i].ForceY = new Vector(NNPE_H);
                        F_Right_BoundaryElements[i].ElementNodes = new Node_ND[NNPE_H];
                        F_Right_BoundaryElements[i].Interpolator = Left_RightBCInterP;
                        F_Right_BoundaryElements[i].FeQuad = Horz_BFeQ;
                        for (int j = 0; j < NNPE_H; j++)
                        {
                            F_Right_BoundaryElements[i].ForceX.Values[j] = F2_1;
                            F_Right_BoundaryElements[i].ForceY.Values[j] = F2_2;
                            F_Right_BoundaryElements[i].ElementNodes[j] = Nodes[i * (NNPE_H - 1) * NN_W + j * NN_W+NN_W - 1];
                        }
                    }
                }

            }
            if (DirectionListBox.SelectedIndex == 1)//Vertical Directions
            {
                if (LeftBClistbox.SelectedIndex == 0)// Bottom Displacement
                {
                    for (int i = 0; i < NE_W; i++)
                    {
                        D_Left_BoundaryElements[i] = new BoundaryElement_ND_Elastic_Line_SteadyState_DisplacementOnNode();
                        D_Left_BoundaryElements[i].Displacements = new Matrix_Jagged(NNPE_W, 2);
                        D_Left_BoundaryElements[i].ElementNodes = new Node_ND[NNPE_W];
                        for (int j = 0; j < NNPE_W; j++)
                        {
                            D_Left_BoundaryElements[i].Displacements.Values[j][0] = U1_1;
                            D_Left_BoundaryElements[i].Displacements.Values[j][1] = U1_2;
                            D_Left_BoundaryElements[i].ElementNodes[j] = Nodes[i * (NNPE_W - 1) + j];
                        }
                    }
                }
                if (LeftBClistbox.SelectedIndex == 1)//Bottom Force
                {
                    for (int i = 0; i < NE_W; i++)
                    {
                        F_Left_BoundaryElements[i] = new BoundaryElement_ND_Elastic_Line_SteadyState_ForceOnNode();
                        F_Left_BoundaryElements[i].ForceX = new Vector(NNPE_W);
                        F_Left_BoundaryElements[i].ForceY = new Vector(NNPE_W);
                        F_Left_BoundaryElements[i].ElementNodes = new Node_ND[NNPE_W];
                        F_Left_BoundaryElements[i].Interpolator = Bottom_UpBCInterP;
                        F_Left_BoundaryElements[i].FeQuad = Vert_BFeQ;
                        for (int j = 0; j < NNPE_W; j++)
                        {
                            F_Left_BoundaryElements[i].ForceX.Values[j] = F1_1;
                            F_Left_BoundaryElements[i].ForceY.Values[j] = F1_2;
                            F_Left_BoundaryElements[i].ElementNodes[j] = Nodes[i * (NNPE_W - 1) + j];
                        }
                    }
                }
                if (RightBClistbox.SelectedIndex == 0)// Top Displacement
                {
                    for (int i = 0; i < NE_W; i++)
                    {
                        D_Right_BoundaryElements[i] = new BoundaryElement_ND_Elastic_Line_SteadyState_DisplacementOnNode();
                        D_Right_BoundaryElements[i].Displacements = new Matrix_Jagged(NNPE_W, 2);
                        D_Right_BoundaryElements[i].ElementNodes = new Node_ND[NNPE_W];
                        for (int j = 0; j < NNPE_W; j++)
                        {
                            D_Right_BoundaryElements[i].Displacements.Values[j][0] = U2_1;
                            D_Right_BoundaryElements[i].Displacements.Values[j][1] = U2_2;
                            D_Right_BoundaryElements[i].ElementNodes[j] = Nodes[NN_W * (NN_H - 1) + i * (NNPE_W - 1) + j];
                        }
                    }
                }
                if (RightBClistbox.SelectedIndex == 1)//Top Force
                {
                    for (int i = 0; i < NE_W; i++)
                    {
                        F_Right_BoundaryElements[i] = new BoundaryElement_ND_Elastic_Line_SteadyState_ForceOnNode();
                        F_Right_BoundaryElements[i].ForceX = new Vector(NNPE_W);
                        F_Right_BoundaryElements[i].ForceY = new Vector(NNPE_W);
                        F_Right_BoundaryElements[i].ElementNodes = new Node_ND[NNPE_W];
                        F_Right_BoundaryElements[i].Interpolator = Bottom_UpBCInterP;
                        F_Right_BoundaryElements[i].FeQuad = Vert_BFeQ;
                        for (int j = 0; j < NNPE_W; j++)
                        {
                            F_Right_BoundaryElements[i].ForceX.Values[j] = F2_1;
                            F_Right_BoundaryElements[i].ForceY.Values[j] = F2_2;
                            F_Right_BoundaryElements[i].ElementNodes[j] = Nodes[NN_W * (NN_H - 1) + i * (NNPE_W - 1) + j];
                        }
                    }
                }

            }
            FEMSolver.BoundaryElements= new BoundaryElement_ND_Static[NBE*2];
            for (int qq = 0; qq < NBE; qq++)
            {
                if (LeftBClistbox.SelectedIndex == 0)
                {
                    FEMSolver.BoundaryElements[qq] = D_Left_BoundaryElements[qq];
                }
                if (LeftBClistbox.SelectedIndex == 1)
                {
                    FEMSolver.BoundaryElements[qq] = F_Left_BoundaryElements[qq];
                }
                if (RightBClistbox.SelectedIndex == 0)
                {
                    FEMSolver.BoundaryElements[NBE+qq ] = D_Right_BoundaryElements[qq];
                }
                if (RightBClistbox.SelectedIndex == 1)
                {
                    FEMSolver.BoundaryElements[NBE+qq ] = F_Right_BoundaryElements[qq];
                }
            }
            if (RightBClistbox.SelectedIndex == 1 && LeftBClistbox.SelectedIndex == 1)
                MessageBox.Show("No Lateral Forces Allowed on Both Sides");
            FEMSolver.SolveFEMSystem();
            Node_ND.Set_UnknownForNode(FEMSolver.Nodes, FEMSolver.U); // Nodal Solutions            
            RePlot();
        }

        public double rho_Function(Vector X)
        {
            return 1.0D;
        }

        public Vector b_Function(Vector X)
        {
            Vector b = new Vector(2);
            b.Values[0] = Convert.ToDouble(b1TextBox.Text);
            b.Values[1] = Convert.ToDouble(b2TextBox.Text);
            return b;
        }
        public double langmuda_Function(double E, double G)
        {
            double poson = E / G / 2.0D - 1.0;
            double langmuda =E*poson/(1.0D-2.0D*poson)/(1.0D+poson);
            return langmuda; 
        }
        public double miu_Function(double E, double G)
        {
            double miu = G;
            return miu;
        }

        private void SetupPlotTypeCheckedListBox()
        {
            PlotTypeCheckedListBox.SelectionMode= SelectionMode.One;//determine selection mode

            PlotTypeCheckedListBox.BeginUpdate();
            PlotTypeCheckedListBox.Items.Add("Node", true);
            PlotTypeCheckedListBox.Items.Add("Wire Mesh", true);
            PlotTypeCheckedListBox.Items.Add("Color Fill", true);
            PlotTypeCheckedListBox.EndUpdate();
            PlotTypeCheckedListBox.CheckOnClick = true;
        }
        private void SetupPlotSelectionTypeListBox()
        {
            PlotTypeListBox.SelectionMode = SelectionMode.One;//determine selection mode

            PlotTypeListBox.BeginUpdate();
            PlotTypeListBox.Items.Add("Displacement: x-direction");
            PlotTypeListBox.Items.Add("Displacement: y-direction");
            PlotTypeListBox.EndUpdate();
            PlotTypeListBox.SelectedIndex = 0;
        }

        private void SetupDirectionListBox()
        {
            DirectionListBox.SelectionMode = SelectionMode.One;//determine selection mode

            DirectionListBox.BeginUpdate();
            DirectionListBox.Items.Add("Horizontal");
            DirectionListBox.Items.Add("Vertical");
            DirectionListBox.EndUpdate();
            DirectionListBox.SelectedIndex = 0;
        }
        private void SetupLeftBcListBox()
        {
            LeftBClistbox.SelectionMode = SelectionMode.One;//determine selection mode

            LeftBClistbox.BeginUpdate();
            LeftBClistbox.Items.Add("Displacement");
            LeftBClistbox.Items.Add("Force");
            LeftBClistbox.EndUpdate();
            LeftBClistbox.SelectedIndex = 0;
        }
        //Right Boundary Updating
        private void SetupRightBcListBox()
        {
            RightBClistbox.SelectionMode = SelectionMode.One;//determine selection mode

            RightBClistbox.BeginUpdate();
            RightBClistbox.Items.Add("Displacement");
            RightBClistbox.Items.Add("Force");
            RightBClistbox.EndUpdate();
            RightBClistbox.SelectedIndex = 0;
        }   
        private void ReplotButton_Click(object sender, EventArgs e)
        {
            RePlot();
        }
        private void RePlot()
        {
            if (PlotTypeListBox.SelectedIndex == 0)
            {
                Element_ND_Elastic.SelectedOutputType = Element_ND_Elastic.OutputTypes.Displacement_x;
            }
            else 
            {
                Element_ND_Elastic.SelectedOutputType = Element_ND_Elastic.OutputTypes.Displacement_y;
            }
            OutputPictureBox.Image = MakePlot(16);
        }
        public Metafile MakePlot(double ScaleFactor)
        {
            int PlotResolution = Convert.ToInt32(ResolutionTextBox.Text);
            Surfaces[] ElementSurfaces = Element_ND_Elastic.Make_GraphicSurfaces(FEMSolver.Elements, PlotResolution);
            ColorPlot_2D Plot = new OOPTools_Graphics.ColorPlot_2D();
            Plot.Plot_Title.Text = "Elastic Problems";
            Plot.Plot_ObjectResolution = PlotResolution;

            Plot.NodesDisplayOn = false;
            if (PlotTypeCheckedListBox.GetItemChecked(0)) Plot.NodesDisplayOn = true;
            Plot.WireMeshOn = false;
            if (PlotTypeCheckedListBox.GetItemChecked(1)) Plot.WireMeshOn = true;
            Plot.ColorPlotOn = false;
            if (PlotTypeCheckedListBox.GetItemChecked(2)) Plot.ColorPlotOn = true;
            Metafile ImagePlot = Plot.DrawPlot(ElementSurfaces);

            return ImagePlot;

        }
       
        private void PlotTypeCheckedListBox_MouseClick(object sender, MouseEventArgs e)
        {
            int SelectedIndex = PlotTypeCheckedListBox.SelectedIndex;
            PlotTypeCheckedListBox.SetItemChecked(SelectedIndex, !PlotTypeCheckedListBox.GetItemChecked(SelectedIndex));
        }



        private void SaveFigureButton_Click(object sender, EventArgs e)
        {
            OutputPictureBox.Image.Save(Application.StartupPath + "\\Figure_" + DateTime.Now.Ticks.ToString() + ".Wmf", ImageFormat.Wmf);
        }

    }
}
