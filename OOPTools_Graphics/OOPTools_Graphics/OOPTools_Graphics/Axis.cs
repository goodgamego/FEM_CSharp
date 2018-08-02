using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing.Drawing2D;
using System.Drawing;
using OOPTools_Math;

namespace OOPTools_Graphics
{
    public class Axis
    {
        public Title[] AxisText;
        public Pen AxisPen;
        public Font AxisFont;
        public float FontSize;
        public AxisOrientation Orientation;
        public double TickLength;
        public Vector x_i, x_f;
        public double Width, Height;
        public Vector x_c;
        public Axis(Vector x_initial, Vector x_final, AxisOrientation orientation)
        {
            x_i = x_initial;
            x_f = x_final;
            Orientation = orientation;
            Scale(1.0D);
        }
        public void Scale(double ScaleFactor)
        {
            TickLength = ScaleFactor*(x_f - x_i).Magnitude() / 100.0D;

            AxisPen = new Pen(Color.Black);
            AxisPen.Width = Convert.ToSingle(ScaleFactor * 4.0);
            FontSize = Convert.ToSingle(ScaleFactor * 16);
            AxisFont = new Font("Times New Roman", FontSize);
        }
        public void DrawAxis(Graphics g, double x_min, double x_max, double NumberOfTicks)
        {
            x_c = (x_i + x_f) / 2.0D;
            switch (Orientation)
            {
                case AxisOrientation.Horizontal:
                    DrawAxisHorizontal(g, x_min, x_max, NumberOfTicks);
                    break;
                case AxisOrientation.Vertical:
                    DrawAxisVertical(g, x_min, x_max, NumberOfTicks);
                    break;
            }
        }
        private void DrawAxisVertical(Graphics g, double x_min, double x_max, double NumberOfTicks)
        {

            PointF XiPoint = new PointF(Convert.ToSingle(x_i.Values[0]), Convert.ToSingle(x_i.Values[1]));
            PointF XfPoint = new PointF(Convert.ToSingle(x_f.Values[0]), Convert.ToSingle(x_f.Values[1]));
            g.DrawLine(AxisPen, XiPoint, XfPoint);

            double Dx = (x_max - x_min) / (NumberOfTicks - 1.0D);
            int nt = Convert.ToInt32(NumberOfTicks);
            AxisText = new Title[nt];

            for (int i = 0; i < nt; i++)
            {
                AxisText[i] = new Title(GetStringNumber(x_min + Convert.ToDouble(i) * Dx,x_min,x_max), Convert.ToInt32(FontSize), Title.TitleOrientation.Horizontal);
                AxisText[i].GetWidthAndHeight(g);
            }

            Vector DeltaX_Tick = (x_f - x_i) / (NumberOfTicks - 1.0D);
            Vector TickVector = new Vector(2);
            TickVector.Values[0] = -TickLength;
            for (int i = 0; i < Convert.ToInt32(NumberOfTicks); i++)
            {
                Vector xTick_i = x_i + Convert.ToDouble(i) * DeltaX_Tick;
                Vector xTick_f = xTick_i + TickVector;
                PointF xTick_iPoint = new PointF(Convert.ToSingle(xTick_i.Values[0]), Convert.ToSingle(xTick_i.Values[1]));
                PointF xTick_fPoint = new PointF(Convert.ToSingle(xTick_f.Values[0]), Convert.ToSingle(xTick_f.Values[1]));
                g.DrawLine(AxisPen, xTick_iPoint, xTick_fPoint);

                Vector Number_c = xTick_f + TickVector / 2.0D;
                Number_c.Values[0] += -AxisText[i].Width/2.0D;
                Number_c.Values[1] += -AxisText[i].Height/2.0D;
                AxisText[i].DrawTitle(g, Number_c);

            }
            Height = (x_f - x_i).Magnitude();
            double maxWidth = AxisText[0].Width;
            for (int i = 1; i < Convert.ToInt32(NumberOfTicks); i++)
            {
                if (AxisText[i].Width > maxWidth) maxWidth = AxisText[i].Width;
            }
            Width = (1.5 * TickLength) + maxWidth;
        }
        private string GetStringNumber(double x, double x_min, double x_max)
        {
            string NumberString;
            double x2 = x;
            double Dx = x_max - x_min;
            if (Math.Abs(x - x_min) / Dx <= 1.0E-15)
            {
                x2 = x_min;
            }
            NumberString = x2.ToString("G4");
            return NumberString;
        }
        private void DrawAxisHorizontal(Graphics g, double x_min, double x_max, double NumberOfTicks)
        {
            PointF XiPoint = new PointF(Convert.ToSingle(x_i.Values[0]),Convert.ToSingle(x_i.Values[1]));
            PointF XfPoint = new PointF(Convert.ToSingle(x_f.Values[0]), Convert.ToSingle(x_f.Values[1]));
            g.DrawLine(AxisPen, XiPoint, XfPoint);
            
            double Dx = (x_max - x_min) / (NumberOfTicks - 1.0D);
            int nt = Convert.ToInt32(NumberOfTicks);
            AxisText = new Title[nt];
            for (int i = 0; i < nt; i++)
            {
                AxisText[i] = new Title(GetStringNumber(x_min + Convert.ToDouble(i) * Dx,x_min,x_max),Convert.ToInt32(FontSize), Title.TitleOrientation.Horizontal);
            }

            Vector DeltaX_Tick = (x_f - x_i) / (NumberOfTicks - 1.0D);
            Vector TickVector = new Vector(2);
            TickVector.Values[1] = TickLength;
            for (int i = 0; i < Convert.ToInt32(NumberOfTicks); i++)
            {
                Vector xTick_i = x_i + Convert.ToDouble(i) * DeltaX_Tick;
                Vector xTick_f = xTick_i + TickVector;
                PointF xTick_iPoint = new PointF(Convert.ToSingle(xTick_i.Values[0]), Convert.ToSingle(xTick_i.Values[1]));
                PointF xTick_fPoint = new PointF(Convert.ToSingle(xTick_f.Values[0]), Convert.ToSingle(xTick_f.Values[1]));
                g.DrawLine(AxisPen, xTick_iPoint, xTick_fPoint);
                
                Vector Number_c = xTick_f + TickVector / 2.0D;
                AxisText[i].DrawTitle(g, Number_c);

            }
            Width = (x_f - x_i).Magnitude();
            Height = (1.5 * TickLength) + AxisText[0].Height;
            //x_c.Values[1] += Height ;
        }
        public enum AxisOrientation
        {
            Horizontal, Vertical
        }
    }
}
