using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Drawing.Imaging;
using OOPTools_Math;

namespace OOPTools_Graphics
{
    public class XYPlot
    {
        public XYGraph TheGraph;
        public Axis X_Axis, Y_Axis;
        public Title X_Axis_Title, Y_Axis_Title, Plot_Title, OOPS_Graphics_Title;

        public Vector Xo;
        public Vector X_BR;
        public Vector X_TL;

        public double PageWidth, PageHeight;
        public double GraphWidth, GraphHeight;

        public double VerticalAxisTitlePad, HorizontalAxisTitlePad, TitlePad;
        public int NumberOfTicks;

        public int NumberOfGraphs;
        public XYPlot(int numberOfGraphs)
        {
            NumberOfGraphs = numberOfGraphs;

            ResetPlot();
        }
        public void ResetPlot()
        {
            Plot_Title = new Title("Title", 18, Title.TitleOrientation.Horizontal);
            X_Axis_Title = new Title("X Axis", 18, Title.TitleOrientation.Horizontal);
            Y_Axis_Title = new Title("Y Axis", 18, Title.TitleOrientation.Vertical);
            OOPS_Graphics_Title = new Title("OOPS-Graphics", 18, Title.TitleOrientation.Horizontal);
            OOPS_Graphics_Title.ChangeFont(Color.LightGray, 18, FontStyle.Italic);

            PageHeight = 600.0D;

            Scale(1.0D);
        }
        public void Scale(double ScaleFactor)
        {           
            Plot_Title.Scale(ScaleFactor);
            X_Axis_Title.Scale(ScaleFactor);
            Y_Axis_Title.Scale(ScaleFactor);
            OOPS_Graphics_Title.Scale(ScaleFactor);

            PageHeight = ScaleFactor * PageHeight;
            PageWidth = PageHeight * (1.0 + Math.Sqrt(5)) / 2.0D;
            GraphHeight = PageHeight * 0.7;
            GraphWidth = PageWidth * 0.7;

            TheGraph = new XYGraph(NumberOfGraphs, GraphWidth, GraphHeight);
            //TheGraph.ScalePens(ScaleFactor);
            
            HorizontalAxisTitlePad = PageHeight * 0.01;
            VerticalAxisTitlePad = PageWidth * 0.01;
            TitlePad = PageHeight * 0.02;
            
            Xo = new Vector(2);
            X_BR = new Vector(2);
            X_TL = new Vector(2);
            Xo.Values[0] = PageWidth * 0.2;
            Xo.Values[1] = PageHeight * 0.85;
            X_BR.Values[0] = Xo.Values[0] + GraphWidth;
            X_BR.Values[1] = Xo.Values[1];
            X_TL.Values[0] = Xo.Values[0];
            X_TL.Values[1] = Xo.Values[1] - GraphHeight;
            
            X_Axis = new Axis(Xo, X_BR, Axis.AxisOrientation.Horizontal);
            X_Axis.Scale(ScaleFactor);
            Y_Axis = new Axis(Xo, X_TL, Axis.AxisOrientation.Vertical);
            Y_Axis.Scale(ScaleFactor);

            NumberOfTicks = 5;
        }
        public Metafile DrawPlot(Vector X, Vector Y)
        {
            Vector[] Xs = new Vector[1];
            Xs[0] = X;
            Vector[] Ys = new Vector[1];
            Ys[0] = Y;
            return DrawPlot(Xs, Ys);
        }
        public Metafile DrawPlot(Vector X, Vector[] Ys)
        {
            int ng = Ys.Length;
            Vector[] Xs = new Vector[ng];
            for (int i = 0; i < ng; i++)
            {
                Xs[i] = X;
            }
            return DrawPlot(Xs, Ys);
        }
        public Metafile DrawPlot(Vector[] Xs, Vector[] Ys)
        {
            Metafile  Plot = ImageMethods.MakeMetafile(PageWidth, PageHeight);
            Graphics g =  Graphics.FromImage(Plot);

            TheGraph = new XYGraph(Xs.Length, GraphWidth, GraphHeight);
            TheGraph.Xs = Xs;
            TheGraph.Ys = Ys;
            TheGraph.DrawGraph(Xo, g);

            X_Axis.DrawAxis(g, TheGraph.Xmin, TheGraph.Xmax, NumberOfTicks);
            Y_Axis.DrawAxis(g, TheGraph.Ymin, TheGraph.Ymax, NumberOfTicks);
            
            Vector X_Axis_Title_c = (Xo+X_BR)/2.0;
            X_Axis_Title_c.Values[1] += X_Axis.Height + HorizontalAxisTitlePad;
            X_Axis_Title.DrawTitle(g, X_Axis_Title_c);
            
            Vector Y_Axis_Title_c = (Xo + X_TL) / 2.0D;
            Y_Axis_Title_c.Values[0] -= Y_Axis.Width + VerticalAxisTitlePad;

            Y_Axis_Title.DrawTitle(g, Y_Axis_Title_c);

            Vector Title_c = X_TL+ (X_BR-Xo) / 2.0D;
            Plot_Title.GetWidthAndHeight(g);
            Title_c.Values[1] += -TitlePad-Plot_Title.Height;
            Plot_Title.DrawTitle(g, Title_c);

            Vector OPPS_Graphics_c = new Vector(2);
            OOPS_Graphics_Title.GetWidthAndHeight(g);
            OPPS_Graphics_c.Values[0] = PageWidth - HorizontalAxisTitlePad - OOPS_Graphics_Title.Width / 2.0D;
            OPPS_Graphics_c.Values[1] = PageHeight - VerticalAxisTitlePad - OOPS_Graphics_Title.Height;
            OOPS_Graphics_Title.DrawTitle(g, OPPS_Graphics_c);

            g.Dispose();
            return Plot;
        }
        
        public void setXAndYRange(double Min, double Max, double OutMin, double OutMax, double OutDX)
        {
            double DX = Max - Min;
            if (DX == 0.0D) DX = 1.0D;
            double dX = DX / Convert.ToDouble(NumberOfTicks-1);
            OutMin = Min;
            OutMax = Max;
            OutDX = dX;      
        }
    }
}
