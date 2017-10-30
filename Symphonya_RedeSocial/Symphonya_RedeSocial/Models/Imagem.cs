using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;

namespace Symphonya_RedeSocial.Models
{
    public class Imagem
    {
        public Bitmap ResizeImage(Stream stream, int? width, int? height)
        {
            System.Drawing.Bitmap bmpOut = null;
            const int defaultWidth = 1024;
            const int defaultHeight = 768;
            int lnWidth = width == null ? defaultWidth : (int)width;
            int lnHeight = height == null ? defaultHeight : (int)height;
            try
            {
                Bitmap loBMP = new Bitmap(stream);
                ImageFormat loFormat = loBMP.RawFormat;

                decimal lnRatio;
                int lnNewWidth = 0;
                int lnNewHeight = 0;

                //if (loBMP.Width < lnWidth && loBMP.Height < lnHeight)
                //{
                //    return loBMP;
                //}

                if (loBMP.Width > loBMP.Height)
                {
                    lnNewHeight = (int)height;
                    lnNewWidth = (int)width;
                }
                else
                {
                    lnNewHeight = (int)height;
                    lnNewWidth = (int)width;
                }

                if(loBMP.Width == loBMP.Height)
                {
                    lnNewHeight = (int)height;
                    lnNewWidth = (int)width;
                }

                bmpOut = new Bitmap(lnNewWidth, lnNewHeight);
                Graphics g = Graphics.FromImage(bmpOut);
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.Low;
                g.FillRectangle(Brushes.White, 0, 0, lnNewWidth, lnNewHeight);
                g.DrawImage(loBMP, 0, 0, lnNewWidth, lnNewHeight);
                loBMP.Dispose();
            }
            catch
            {
                return null;
            }
            return bmpOut;
        }
    }
}