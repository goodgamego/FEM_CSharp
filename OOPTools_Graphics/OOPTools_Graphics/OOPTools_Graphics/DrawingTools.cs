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

namespace OOPTools_Graphics
{
    public class DrawingTools
    {
        public static PointF GetGraphicsPoint(Vector X)
        {
            PointF ThePoint = new PointF(Convert.ToSingle(X.Values[0]), Convert.ToSingle(X.Values[1]));
            return ThePoint;
        }
        public static PointF[] GetGraphicsPoint(Vector[] X)
        {
            int nn = X.Length;
            PointF[] ThePoints = new PointF[nn];
            for(int i=0; i<nn; i++)
            {
            ThePoints[i] = new PointF(Convert.ToSingle(X[i].Values[0]), Convert.ToSingle(X[i].Values[1]));
            }
            return ThePoints;
        }
        public static void FillRectangle(Graphics g, SolidBrush gBrush, Vector TopLeft, double Width, double Height)
        {
            float X = Convert.ToSingle(TopLeft.Values[0]);
            float Y = Convert.ToSingle(TopLeft.Values[1]);
            float W = Convert.ToSingle(Width);
            float H = Convert.ToSingle(Height);
            g.FillRectangle(gBrush, X, Y, W, H);
        }
        public static void DrawRectangle(Graphics g, Pen gPen, Vector TopLeft, double Width, double Height)
        {
            float X = Convert.ToSingle(TopLeft.Values[0]);
            float Y = Convert.ToSingle(TopLeft.Values[1]);
            float W = Convert.ToSingle(Width);
            float H = Convert.ToSingle(Height);
            g.DrawRectangle(gPen, X, Y, W, H);
        }
        public static void DrawLine(Graphics g, Pen gPen, Vector Start, Vector End)
        {
            PointF X1 = GetGraphicsPoint(Start);
            PointF X2 = GetGraphicsPoint(End);
            g.DrawLine(gPen,X1, X2);
        }
    }
    public class Line
    {
        Vector[] Nodes;
    }
    public class Area
    {
        Line[] Lines;
    }
    public class Volume
    {
        Area[] Areas;
    }
}
