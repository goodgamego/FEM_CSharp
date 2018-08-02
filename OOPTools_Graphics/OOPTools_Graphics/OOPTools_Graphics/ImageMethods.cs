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


namespace OOPTools_Graphics
{
    public  class ImageMethods : Form
    {
        public static Metafile MakeMetafile(double width, double height)
        {
            ImageMethods IM = new ImageMethods();
            return IM.GetMetafile(width, height);
        }
        public static Metafile MakeMetafile(int width, int height)
        {
            ImageMethods IM = new ImageMethods();
            return IM.GetMetafile(width, height);
        }
        public static Bitmap MakeBitmap(double width, double height)
        {
            ImageMethods IM = new ImageMethods();
            return IM.GetBitmap(width, height);
        }
        public static Bitmap MakeBitmap(int width, int height)
        {
            ImageMethods IM = new ImageMethods();
            return IM.GetBitmap(width, height);
        }
        public Metafile GetMetafile(double width, double height)
        {
            return GetMetafile(Convert.ToInt32(width),Convert.ToInt32(height));
        }
        public  Metafile GetMetafile(int width, int height)
        {

            Graphics gTemp = CreateGraphics();
            IntPtr ipHdc = gTemp.GetHdc();
            int PlotWidth = width;
            int PlotHeight = height;
            Metafile mf = new Metafile(new MemoryStream(), ipHdc, new Rectangle(0, 0, PlotWidth, PlotHeight), MetafileFrameUnit.Pixel, EmfType.EmfPlusDual, "Plot");
            gTemp.ReleaseHdc(ipHdc);
            gTemp.Dispose();


            return mf;
        }
        public Bitmap GetBitmap(double width, double height)
        {
            return GetBitmap(Convert.ToInt32(width), Convert.ToInt32(height));
        }
        public Bitmap GetBitmap(int width, int Height)
        {
            Bitmap bm = new Bitmap(width, Height);
            return bm;
        }
    }
}
