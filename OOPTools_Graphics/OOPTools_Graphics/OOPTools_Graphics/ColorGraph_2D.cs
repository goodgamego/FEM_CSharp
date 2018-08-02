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
    public class ColorGraph_2D
    {
        public Vector X_o;
        public Pen GraphPen;
        public Brush GraphBrush;
        public Vector MinX, MaxX;
        public double Min, Max;
        public double w_G, h_G; //Graph width and height
        public float LineThickness; //points
        public float NodeSize;
        public Surfaces[] ScaledSurfaces;
        public double small;
        public bool WireMeshOn, NodesDisplayOn, ColorPlotOn;
        public ColorGraph_2D(double Width, double Height)
        {
            small = 1.0E-20;
            w_G = Width;
            h_G = Height;
            LineThickness = 2.0F;
            GraphBrush = new SolidBrush(Color.Black);
            NodeSize = 3.0F * LineThickness;
            WireMeshOn = true;
            NodesDisplayOn = true;
            ColorPlotOn = true;
            ScalePens(1.0D);
        }
        public void ScalePens(double ScaleFactor)
        {
            LineThickness = Convert.ToSingle(ScaleFactor) * LineThickness;
            GraphPen = new Pen(Color.Black, LineThickness);
        }
        public void InitializeGraph()
        {
        }
        public void DrawGraph(Graphics g, Vector x_o, Surfaces[] ElementSurfaces)
        {
            X_o = x_o;
            Surfaces[] ScaledSurfaces = TranslateAndScale(ElementSurfaces);
            if (ColorPlotOn) DrawColorFill(g, ScaledSurfaces); 
            if (WireMeshOn) DrawWireMesh(g, ScaledSurfaces);
            if (NodesDisplayOn) DrawNodes(g, ScaledSurfaces);
        }
        public void DrawGraph(Graphics g, Vector x_o, Surfaces[] ElementSurfaces, double Min, double Max)
        {
            X_o = x_o;
            Surfaces[] ScaledSurfaces = TranslateAndScale(ElementSurfaces, Min, Max);
            if (ColorPlotOn) DrawColorFill(g, ScaledSurfaces);
            if (WireMeshOn) DrawWireMesh(g, ScaledSurfaces);
            if (NodesDisplayOn) DrawNodes(g, ScaledSurfaces);
        }
        private void DrawWireMesh(Graphics g, Surfaces[] ScaledSurfaces)
        {
            int NSurf = ScaledSurfaces.Length;
            for(int i = 0; i < NSurf; i++)
            {
                int NSide = ScaledSurfaces[i].Sides.Length;
                for (int j = 0; j < NSide; j++)
                {
                    int NLines = ScaledSurfaces[i].Sides[j].MeshLines.Length;
                    for (int k = 0; k < NLines; k++)
                    {
                        Vector[] VectorPoints = ScaledSurfaces[i].Sides[j].MeshLines[k];
                        PointF[] Points = GetGraphicsPoint(VectorPoints);
                        g.DrawCurve(GraphPen, Points);
                    }
                }
            }
        }
        private void DrawNodes(Graphics g, Surfaces[] ScaledSurfaces)
        {
            int NSurf = ScaledSurfaces.Length;
            for (int i = 0; i < NSurf; i++)
            {
                int NSide = ScaledSurfaces[i].Sides.Length;
                for (int j = 0; j < NSide; j++)
                {
                    int NPoints = ScaledSurfaces[i].Sides[j].NodeLocations.Length;
                    for (int k = 0; k < NPoints; k++)
                    {
                        PointF Point = GetGraphicsPoint(ScaledSurfaces[i].Sides[j].NodeLocations[k]);
                        g.FillEllipse(GraphBrush, Point.X - NodeSize / 2.0F, Point.Y - NodeSize / 2.0F, NodeSize, NodeSize);
                        g.DrawEllipse(GraphPen, Point.X - NodeSize / 2.0F, Point.Y - NodeSize / 2.0F, NodeSize, NodeSize);
                    }
                }
            }
        }
        private void DrawColorFill(Graphics g, Surfaces[] ScaledSurfaces)
        {
            int NSurf = ScaledSurfaces.Length;
            for (int i = 0; i < NSurf; i++)
            {
                int NSide = ScaledSurfaces[i].Sides.Length;
                for (int j = 0; j < NSide; j++)
                {
                    int NAreas = ScaledSurfaces[i].Sides[j].Areas.Length;
                    for (int k = 0; k < NAreas; k++)
                    {
                        Vector[] X = ScaledSurfaces[i].Sides[j].Areas[k];
                        double[] Y = ScaledSurfaces[i].Sides[j].AreaValues[k].Values;
                        Color C1, C2;
                        Vector X1, X2;
                        int M = 5;
                        Single[] XColor;
                        Color[] Colors;
                        OOPsColor.Calculate_LineGradientBrushParameters_ForPolygon(X, Y, out X1, out X2, out C1, out C2, M, out XColor, out Colors);
                        PointF P1 = GetGraphicsPoint(X1);
                        PointF P2 = GetGraphicsPoint(X2);
                        LinearGradientBrush MyBrush = new LinearGradientBrush(P1, P2, C1, C2);
                        ColorBlend myColorBlend = new ColorBlend(M);
                        myColorBlend.Colors = Colors;
                        myColorBlend.Positions = XColor;
                        MyBrush.InterpolationColors = myColorBlend;
                        PointF[] Points = GetGraphicsPoint(X);
                        g.FillPolygon(MyBrush,Points);
                    }
                }
            }
        }
        private Vector CenterPoint(Vector[] Points)
        {
            Vector Ave = new Vector(Points[0]);
            int NP = Points.Length;
            for (int i = 1; i < NP-1; i++)
            {
                Ave += Points[i];
            }
            return Ave / Convert.ToDouble(NP-1);
        }
        private double CenterColorValue(Vector Val)
        {
            double Ave = Val.Values[0];
            int NP = Val.Values.Length;
            for (int i = 1; i < NP - 1; i++)
            {
                Ave += Val.Values[i];
            }
            return Ave / Convert.ToDouble(NP - 1);
        }
        private Surfaces[] TranslateAndScale(Surfaces[] ElementSurfaces)
        {
            Surfaces.CalculateMinMax_Location(ElementSurfaces, out MinX, out MaxX);
            Vector Scale;
            GetScalingParameters(ref MinX, ref MaxX, out Scale);

            Surfaces[] ScaledSurfaces;
            if (ColorPlotOn)
            {
                Surfaces.CalculateMinMax_Values(ElementSurfaces, out Min, out Max);
                double ValueScale;
                if (Math.Abs(Max - Min) > small)
                {
                    ValueScale = 1.0D / (Max - Min);
                }
                else if (Math.Abs(Max) > small)
                {
                    ValueScale = 1.0D / Max;
                }
                else
                {
                    ValueScale = 1.0D;
                }
                ScaledSurfaces = Surfaces.TranslateAndScale(ElementSurfaces, MinX, Scale, Min, ValueScale);
            }
            else
            {
                ScaledSurfaces = Surfaces.TranslateAndScale(ElementSurfaces, MinX, Scale);
            }
            return ScaledSurfaces;
        }
        private void GetScalingParameters(ref Vector MinX, ref Vector MaxX, out Vector Scale)
        {
            Vector DX = MaxX - MinX;
            double DX_0 = DX.Values[0];
            Scale = new Vector(2);
            DX.Values[0] = w_G / DX.Values[0];
            DX.Values[1] = h_G / DX.Values[1];
            double DX_Min = DX.Min();
            Scale.Values[0] = DX_Min;
            Scale.Values[1] = DX_Min;
            MinX.Values[0] += (DX_0 - w_G / DX_Min) / 2.0D;
        }
        private Surfaces[] TranslateAndScale(Surfaces[] ElementSurfaces, double Min, double Max)
        {
            Surfaces.CalculateMinMax_Location(ElementSurfaces, out MinX, out MaxX);
            Vector Scale;
            GetScalingParameters(ref MinX, ref MaxX, out Scale);
            
            Surfaces[] ScaledSurfaces;
            if (ColorPlotOn)
            {
                double ValueScale;
                if (Math.Abs(Max - Min) > small)
                {
                    ValueScale = 1.0D / (Max - Min);
                }
                else if (Math.Abs(Max) > small)
                {
                    ValueScale = 1.0D / Max;
                }
                else
                {
                    ValueScale = 1.0D;
                }
                ScaledSurfaces = Surfaces.TranslateAndScale(ElementSurfaces, MinX, Scale, Min, ValueScale);
            }
            else
            {
                ScaledSurfaces = Surfaces.TranslateAndScale(ElementSurfaces, MinX, Scale);
            }
            return ScaledSurfaces;
        }
        private PointF GetGraphicsPoint(Vector X)
        {
            Vector NewX = new Vector(X);
            NewX.Values[0] += X_o.Values[0];
            NewX.Values[1] = X_o.Values[1] - NewX.Values[1];
            return DrawingTools.GetGraphicsPoint(NewX);
        }
        private PointF[] GetGraphicsPoint(Vector[] X)
        {
            int NP = X.Length;
            PointF[] ThePoints= new PointF[NP];
            for (int i = 0; i < NP; i++)
            {
                ThePoints[i] = GetGraphicsPoint(X[i]);
            }
            return ThePoints;
        }
    }
   
}

