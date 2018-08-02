using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;
using System.ComponentModel;

using OOPTools_Math;
using OOPTools_FEM_ND;

namespace OOPTools_Graphics
{
    public class ColorPlot_2D
    {
        public ColorGraph_2D TheGraph;
        public Title Plot_Title, OOPS_Graphics_Title;

        public ColorScale Plot_ColorScale;
        public int Plot_ObjectResolution;

        public Vector Xo;
        public Vector X_TR,X_TC;

        public double PageWidth, PageHeight;
        public double GraphWidth, GraphHeight;

        public double TitlePad, ColorScalePad;
        public int NumberOfTicks;

        public bool WireMeshOn, NodesDisplayOn, ColorPlotOn;
        public bool FixedScale;

        public ColorPlot_2D()
        {
            WireMeshOn = true;
            NodesDisplayOn = true;
            ColorPlotOn = true;
            FixedScale = true;
            Plot_ObjectResolution = 1;
            ResetPlot();
        }
        public void ResetPlot()
        {
            Plot_Title = new Title("Title", 18, Title.TitleOrientation.Horizontal);
            OOPS_Graphics_Title = new Title("OOPS-Graphics", 18, Title.TitleOrientation.Horizontal);
            OOPS_Graphics_Title.ChangeFont(Color.LightGray, 18, FontStyle.Italic);

            PageHeight = 600.0D;

            Scale(1.0D);
        }
        public void Scale(double ScaleFactor)
        {           
            Plot_Title.Scale(ScaleFactor);
            OOPS_Graphics_Title.Scale(ScaleFactor);

            PageHeight = ScaleFactor * PageHeight;
            PageWidth = PageHeight * (1.0D + Math.Sqrt(5.0D)) / 2.0D;
            GraphHeight = PageHeight * 0.73D;
            GraphWidth = PageWidth * 0.73D;

            TheGraph = new ColorGraph_2D(GraphWidth, GraphHeight);
           

            TitlePad = PageHeight * 0.02D;
            ColorScalePad = PageWidth * 0.02D;
            
            Xo = new Vector(2);
            X_TR = new Vector(2);
            X_TC = new Vector(2);
            Xo.Values[0] = PageWidth * 0.10D;
            Xo.Values[1] = PageHeight * 0.9D;
            X_TR.Values[0] = Xo.Values[0] + GraphWidth;
            X_TR.Values[1] = Xo.Values[1] - GraphHeight;
            X_TC = new Vector(X_TR);
            X_TC.Values[0] -= GraphWidth / 2.0D;

            Vector X_ColorScale = new Vector(X_TR);
            X_ColorScale.Values[0] += ColorScalePad;
            X_ColorScale.Values[1] += GraphHeight / 2.0D;
            Plot_ColorScale = new ColorScale(X_ColorScale);

            NumberOfTicks = 5;
        }
        private void SetPlotTypeToDisplay()
        {
            if (WireMeshOn)
            {
                TheGraph.WireMeshOn = true;
            }
            else
            {
                TheGraph.WireMeshOn = false;
            }
            if (ColorPlotOn)
            {
                TheGraph.ColorPlotOn = true;
            }
            else
            {
                TheGraph.ColorPlotOn = false;
            }
            if (NodesDisplayOn)
            {
                TheGraph.NodesDisplayOn = true;
            }
            else
            {
                TheGraph.NodesDisplayOn = false;
            }
        }
        public Metafile DrawPlot(Surfaces[] ElementSurfaces)
        {
            Metafile Plot = ImageMethods.MakeMetafile(PageWidth, PageHeight);
            Graphics g = Graphics.FromImage(Plot);

            SetPlotTypeToDisplay();
            TheGraph.DrawGraph(g, Xo, ElementSurfaces);

            if (ColorPlotOn)
            {
                Plot_ColorScale.Initialize_Calculate_WidthAndHeight(ref g, TheGraph.Min, TheGraph.Max);
                Plot_ColorScale.x_c = new Vector(X_TR);
                Plot_ColorScale.x_c.Values[0] += ColorScalePad + Plot_ColorScale.Width / 2.0D;
                Plot_ColorScale.AddColorScale(ref g);
            }

            Vector Title_c = X_TC;
            Plot_Title.GetWidthAndHeight(g);
            Title_c.Values[1] += -TitlePad - Plot_Title.Height;
            Plot_Title.DrawTitle(g, Title_c);

            Vector OPPS_Graphics_c = new Vector(2);
            OOPS_Graphics_Title.GetWidthAndHeight(g);
            OPPS_Graphics_c.Values[0] = PageWidth - ColorScalePad - OOPS_Graphics_Title.Width / 2.0D;
            OPPS_Graphics_c.Values[1] = PageHeight - ColorScalePad - OOPS_Graphics_Title.Height;
            OOPS_Graphics_Title.DrawTitle(g, OPPS_Graphics_c);

            g.Dispose();
            return Plot;
        }
        public Metafile[] DrawPlot(Element_ND[] Elements, Node_ND[] Nodes, Vector[] GlobalUnknowns, out Vector MinV, out Vector MaxV)
        {
            int NP = GlobalUnknowns.Length;
            Metafile[] Plots = new Metafile[NP];
            Graphics[] gs = new Graphics[NP];
            for (int i = 0; i < NP; i++)
            {
                Plots[i] = ImageMethods.MakeMetafile(PageWidth, PageHeight);
                gs[i] = Graphics.FromImage(Plots[i]);
            }

            SetPlotTypeToDisplay();
            MinV = new Vector(NP);
            MaxV = new Vector(NP);
            Node_ND.Set_UnknownForNode(Nodes, GlobalUnknowns[0]);
            Surfaces[] TheSurfaces = Element_ND.Make_GraphicSurfaces(Elements, Plot_ObjectResolution);
            Surfaces.CalculateMinMax_Values(TheSurfaces, out MinV.Values[0], out MaxV.Values[0]);
            for (int i = 1; i < NP; i++)
            {
                Node_ND.Set_UnknownForNode(Nodes, GlobalUnknowns[i]);
                Element_ND.Change_GraphicSurfaces_Values(Elements, ref TheSurfaces, Plot_ObjectResolution);
                Surfaces.CalculateMinMax_Values(TheSurfaces, out MinV.Values[i], out MaxV.Values[i]);
            }
            double Min = MinV.Min();
            double Max = MaxV.Max();
            for (int i = 0; i < NP; i++)
            {
                Node_ND.Set_UnknownForNode(Nodes, GlobalUnknowns[i]);
                Element_ND.Change_GraphicSurfaces_Values(Elements, ref TheSurfaces, Plot_ObjectResolution);
                if (FixedScale)
                {
                    TheGraph.DrawGraph(gs[i], Xo, TheSurfaces, Min, Max);
                }
                else
                {
                    TheGraph.DrawGraph(gs[i], Xo, TheSurfaces, MinV.Values[i], MaxV.Values[i]);
                }
                if (ColorPlotOn)
                {
                    if (FixedScale)
                    {
                        Plot_ColorScale.Initialize_Calculate_WidthAndHeight(ref gs[i], Min, Max);
                    }
                    else
                    {
                        Plot_ColorScale.Initialize_Calculate_WidthAndHeight(ref gs[i], MinV.Values[i], MaxV.Values[i]);
                    }
                    Plot_ColorScale.x_c = new Vector(X_TR);
                    Plot_ColorScale.x_c.Values[0] += ColorScalePad + Plot_ColorScale.Width / 2.0D;
                    Plot_ColorScale.AddColorScale(ref gs[i]);
                }
                Vector Title_c = new Vector(X_TC);
                Plot_Title.GetWidthAndHeight(gs[i]);
                Title_c.Values[1] += -TitlePad - Plot_Title.Height;
                Plot_Title.DrawTitle(gs[i], Title_c);

                Vector OPPS_Graphics_c = new Vector(2);
                OOPS_Graphics_Title.GetWidthAndHeight(gs[i]);
                OPPS_Graphics_c.Values[0] = PageWidth - ColorScalePad - OOPS_Graphics_Title.Width / 2.0D;
                OPPS_Graphics_c.Values[1] = PageHeight - ColorScalePad - OOPS_Graphics_Title.Height;
                OOPS_Graphics_Title.DrawTitle(gs[i], OPPS_Graphics_c);

                gs[i].Dispose();
            }
            return Plots;
        }
    }
}
