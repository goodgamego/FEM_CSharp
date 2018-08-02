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
    public class ColorScale
    {
        public Title[] ScaleText;
        public double LineWidth;
        public Pen AxisPen;
        public Font AxisFont;
        public float FontSize;
        public double TickLength, TickPad;
        int NumberOfTicks;
        public Vector TopLeft;
        public Vector LeftCenter;
        public double Width, Height;
        public double ScaleWidth, ScaleHeight;
        public Vector x_c;
        public int NumberOfColorBlocks;
        public ColorScale(Vector leftCenter)
        {
            LeftCenter = leftCenter;
            Scale(1.0D);
        }
        public void Scale(double ScaleFactor)
        {
            ScaleWidth = ScaleFactor * 40.0D;
            ScaleHeight = ScaleFactor * 400.0D;
            TickLength = ScaleFactor *10.0D;
            TickPad = ScaleFactor * 3.0D;
            NumberOfTicks = 5;
            NumberOfColorBlocks = 200 * Convert.ToInt32(ScaleFactor);

            TopLeft = new Vector(LeftCenter);
            TopLeft.Values[1] -= ScaleHeight / 2.0D;

            AxisPen = new Pen(Color.Black);
            LineWidth = ScaleFactor * 4.0;
            AxisPen.Width = Convert.ToSingle(LineWidth);
            FontSize = Convert.ToSingle(ScaleFactor * 14);
            AxisFont = new Font("Times New Roman", FontSize);
        }
        public void Initialize_Calculate_WidthAndHeight(ref Graphics g, double Min, double Max)
        {
            double NTickD = Convert.ToDouble(NumberOfTicks);
            double DScale = (Max - Min) / (NTickD  - 1.0D);
            double MaxTitleWidth = 0.0D;
            ScaleText = new Title[NumberOfTicks];
            for (int i = 0; i < NumberOfTicks; i++)
            {
                ScaleText[i] = new Title((Max - Convert.ToDouble(i) * DScale).ToString("G3"), FontSize, Title.TitleOrientation.Horizontal);
                ScaleText[i].GetWidthAndHeight(g);
                if (MaxTitleWidth < ScaleText[i].Width) MaxTitleWidth = ScaleText[i].Width;
            }
            Width = ScaleWidth + TickLength + TickPad + MaxTitleWidth;
            Height = ScaleHeight + ScaleText[0].Height/2.0 + ScaleText[NumberOfTicks-1].Height/2.0D;
            x_c = new Vector(2);
            x_c.Values[0] = TopLeft.Values[0] + Width / 2.0D;
            x_c.Values[1] = TopLeft.Values[1] - ScaleText[0].Height/2.0;
        }
        public void AddColorScale(ref Graphics g)
        {
            Vector V_Width = new Vector(2);
            V_Width.Values[0] = ScaleWidth;
            Vector V_Height = new Vector(2);
            V_Height.Values[1] = ScaleHeight;

            int NCB = NumberOfColorBlocks;
            double NCBD = Convert.ToDouble(NCB);
            Vector DH = V_Height / NCBD;
            for (int i = 0; i < NCB; i++)
            {
                Vector CB_TopLeft = TopLeft + Convert.ToDouble(i) * DH;
                Color fillColor = OOPsColor.ColorScale(1.0D - Convert.ToDouble(i) / (NCBD - 1.0D));
                SolidBrush brush = new SolidBrush(fillColor);
                DrawingTools.FillRectangle(g, brush, CB_TopLeft, ScaleWidth, DH.Values[1]);
            }
            Vector TopRight = TopLeft + V_Width;
            Vector BottomRight = TopRight + V_Height;
            DrawingTools.DrawLine(g, AxisPen, TopRight, BottomRight);

            double NTicksD = Convert.ToDouble(NumberOfTicks);
            V_Height.Values[1] -= LineWidth;
            DH = V_Height / (NTicksD - 1.0D);
            Vector Start = TopRight;
            Start.Values[1] += LineWidth / 2.0D;
            Vector End = new Vector(Start);
            End.Values[0] += TickLength;
            for (int i = 0; i < NumberOfTicks; i++)
            {
                Vector H = Convert.ToDouble(i) * DH;
                DrawingTools.DrawLine(g, AxisPen, Start + H, End + H);
                Vector TitleTopC = End + H;
                TitleTopC.Values[0] += TickPad + ScaleText[i].Width / 2.0D;
                TitleTopC.Values[1] -= ScaleText[i].Height / 2.0D;
                ScaleText[i].DrawTitle(g, TitleTopC);
            }
        }
    }
}
