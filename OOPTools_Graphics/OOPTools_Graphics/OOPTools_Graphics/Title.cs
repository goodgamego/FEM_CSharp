using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing.Drawing2D;
using System.Drawing;
using OOPTools_Math;

namespace OOPTools_Graphics
{
    public class Title
    {
        public Font TitleFont;
        public Brush TextBrush; //Background
        public string Text;
        public TitleOrientation Orientation;
        private SizeF TextDimensions;
        public double Width, Height;
        public Title(string title, float FontSize, TitleOrientation orientation)
        {
            Text = title;
            TitleFont = new Font("Times New Roman", FontSize, FontStyle.Bold);
            TextBrush = new SolidBrush(Color.Black);
            Orientation = orientation;
        }
        public void DrawTitle(Graphics g, Vector x_c)
        {
            GetWidthAndHeight(g);
            switch (Orientation)
            {
                case TitleOrientation.Horizontal:
                    PlotHorizontal(g, x_c);
                    break;
                case TitleOrientation.Vertical:
                    PlotVertical(g, x_c);
                    break;
            }
        }
        public void GetWidthAndHeight(Graphics g)
        {
            TextDimensions = g.MeasureString(Text,TitleFont);
            Width = TextDimensions.Width;
            Height = TextDimensions.Height;

        }
        private void PlotHorizontal(Graphics g, Vector x_c)
        {
            double x_o = x_c.Values[0] - Width/ 2.0D;
            double y_o = x_c.Values[1];
            PointF Corner = new PointF(Convert.ToSingle(x_o), Convert.ToSingle(y_o));
            g.DrawString(Text, TitleFont, TextBrush, Corner);
        }
        private void PlotVertical(Graphics g, Vector x_c)
        {
            g.ResetTransform();
            
            g.TranslateTransform(Convert.ToSingle(x_c.Values[0]-Height), Convert.ToSingle(x_c.Values[1]+Width/ 2.0D));
            g.RotateTransform(-90);
            g.DrawString(Text, TitleFont, TextBrush, 0,0);
            g.ResetTransform();
        }
        public void ChangeFont(Color TextFontColor, float TextFontSize, FontStyle TextFontStyle)
        {
            TextBrush = new SolidBrush(TextFontColor);
            TitleFont = new Font("Times New Roman", TextFontSize, TextFontStyle);
        }
        public void Scale(double ScaleFactor)
        {
            float NewSize = Convert.ToSingle(ScaleFactor) * TitleFont.Size;
            TitleFont = new Font("Times New Roman", NewSize, TitleFont.Style);
        }
        public enum TitleOrientation
        {
            Horizontal, Vertical
        }
    }
}
