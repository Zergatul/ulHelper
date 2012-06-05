using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace ulHelper.App.Drawing
{
    public class BitmapFunctions
    {
        public static void RemoveAlphaChannel(Bitmap bmp)
        {
            for (int x = 0; x < bmp.Width; x++)
                for (int y = 0; y < bmp.Height; y++)
                {
                    var c = bmp.GetPixel(x, y);
                    if (c.A != 255 && (c.R != 255 || c.G != 255 || c.B != 255))
                    {
                        c = Color.FromArgb(255, c.R, c.G, c.B);
                        bmp.SetPixel(x, y, c);
                    }
                }
        }
    }
}