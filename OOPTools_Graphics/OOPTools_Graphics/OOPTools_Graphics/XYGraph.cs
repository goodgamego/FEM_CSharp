using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing.Drawing2D;
using System.Drawing;
using OOPTools_Math;

namespace OOPTools_Graphics
{
    public class XYGraph
    {
        public Pen[] GraphPens;
        public int NumberOfGraphs;
        public Vector[] Xs, Ys;
        private Vector[] XsScaled, YsScaled;
        public double Xmin, Xmax, Ymin, Ymax;
        public double w_G, h_G; //Graph width and height
        public float LineThickness; //points
        private double numberOfTicks;
        public XYGraph(int numberOfGraphs, double Width, double Height)
        {
            NumberOfGraphs = numberOfGraphs;
            GraphPens = new Pen[NumberOfGraphs];
            Xs = new Vector[NumberOfGraphs];
            Ys = new Vector[NumberOfGraphs];
            XsScaled = new Vector[NumberOfGraphs];
            YsScaled = new Vector[NumberOfGraphs];
            w_G = Width;
            h_G = Height;
            LineThickness = Convert.ToSingle(h_G/100.0F);
            numberOfTicks = 5.0D;
            ScalePens(1.0D);   
        }
        public void ScalePens(double ScaleFactor)
        {
            LineThickness = Convert.ToSingle(ScaleFactor) * LineThickness;
            MakePens();
        }
        public void MakePens()
        {
            GraphPens = new Pen[NumberOfGraphs];
            for (int i = 0; i < NumberOfGraphs; i++)
            {
                GraphPens[i] = new Pen(GetBasiGraphColors(i), LineThickness);
            }         
        }
        public void InitializeGraph()
        {
            SetXAndYMinMax();
            ScaleGraphs();
        }
        private void SetXAndYMinMax()
        {
            Vector XsMax = new Vector(NumberOfGraphs);
            Vector XsMin = new Vector(NumberOfGraphs);
            Vector YsMax = new Vector(NumberOfGraphs);
            Vector YsMin = new Vector(NumberOfGraphs);
            for (int i = 0; i < NumberOfGraphs; i++)
            {
                XsMax.Values[i] = Xs[i].Max();
                XsMin.Values[i] = Xs[i].Min();
                YsMax.Values[i] = Ys[i].Max();
                YsMin.Values[i] = Ys[i].Min();
            }
            Xmin = XsMin.Min();
            Xmax = XsMax.Max();
            CorretXMinMax(ref Xmin, ref Xmax);
            Ymin = YsMin.Min();
            Ymax = YsMax.Max();
            CorretXMinMax(ref Ymin, ref Ymax);
        }
        private void ScaleGraphs()
        {
            for (int i = 0; i < NumberOfGraphs; i++)
            {
                int XLength = Xs[i].Values.Length;
                XsScaled[i] = new Vector(XLength);
                YsScaled[i] = new Vector(XLength);
                for (int j = 0; j < XLength; j++)
                {
                    XsScaled[i].Values[j] = (Xs[i].Values[j] - Xmin) * w_G / (Xmax - Xmin);
                    YsScaled[i].Values[j] = (Ys[i].Values[j] - Ymin) * h_G / (Ymax - Ymin);
                }
            }
        }
        public void DrawGraph(Vector x_o,  Graphics g)
        {
            InitializeGraph();
            for (int i = 0; i < NumberOfGraphs; i++)
            {
                int XLength = Xs[i].Values.Length;
                PointF[] GraphPoints = new PointF[XLength];
                for (int j = 0; j < XLength; j++)
                {
                    float x = Convert.ToSingle(XsScaled[i].Values[j] + x_o.Values[0]);
                    float y = Convert.ToSingle(x_o.Values[1] - YsScaled[i].Values[j]);
                    GraphPoints[j] = new PointF(x, y);
                }
                g.DrawLines(GraphPens[i], GraphPoints);
            }
        }
        private void CorretXMinMax(ref double Min, ref double Max)
        {
            if (Max - Min == 0.0D)
            {
                if (Min == 0.0D)
                {
                    Min = -1.0D;
                    Max = 1.0D;
                }
                else
                {
                    Max = Min + Math.Abs(Min);
                    Min = Min - Math.Abs(Min);
                }
            }
            AdjustMinMax(ref Min, ref Max);
        }
        private void AdjustMinMax(ref double x_min, ref double x_max)
        {
            double DX = Math.Abs(x_min - x_max)/(20.0D);
            double Sign_Min = Math.Sign(x_min);
            double Sign_Max = Math.Sign(x_max);
            double Abs_Min = Math.Abs(x_min);
            double Abs_Max = Math.Abs(x_max);
            double MinFactor = Math.Floor(Abs_Min / DX);
            if (Sign_Min < 0) MinFactor = Math.Ceiling(Abs_Min / DX);
            double MaxFactor = Math.Ceiling(Abs_Max / DX);
            if (Sign_Max < 0) MaxFactor = Math.Floor(Abs_Max / DX);
            if (MinFactor >= 1.0E-20)
            {
                if (Sign_Min < 0)
                {
                    x_min = Math.Floor(MinFactor * Abs_Min) / MinFactor;
                    x_min = -x_min;
                }
                else
                {
                    x_min = Math.Ceiling(MinFactor * Abs_Min) / MinFactor;
                }
            }
            else
            {
                x_min = 0.0D;
            }
            if (MaxFactor >= 1.0E-20)
            {
                if (Sign_Max < 0)
                {
                    x_max = Math.Ceiling(MaxFactor * Abs_Max) / MaxFactor;
                    x_max = -x_max;
                }
                else
                {
                    x_max = Math.Floor(MaxFactor * Abs_Max) / MaxFactor;
                }
            }
            else
            {
                x_max = 0.0D;
            }
        }
        public Color GetBasiGraphColors(int ColorNumber)
        {
            switch (ColorNumber)
            {
                case 0:
                    return Color.DarkBlue;
                case 1:
                    return Color.DarkGoldenrod;
                case 2:
                    return Color.Navy;
                case 3:
                    return Color.Black;
                case 4:
                    return Color.DarkRed;
                case 5:
                    return Color.DarkOrange;
                case 6:
                    return Color.Maroon;
                case 7:
                    return Color.BlueViolet;
                case 8:
                    return Color.Orange;
                case 9:
                    return Color.Red;
                case 10:
                    return Color.Indigo;
                case 11:
                    return Color.Gold;
                case 12:
                    return Color.Plum;
                default:
                    return Color.Black;
            }
        }
    }
}