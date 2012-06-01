using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace ulHelper.App
{
    public static class WorldMap
    {
        static Bitmap bmp;

        public static void Load()
        {
            bmp = new Bitmap(@"Images\map.png");
        }

        public static void DrawAt(Graphics g, Size size, int x, int y)
        {
            float xm = GameXtoMapX(x);
            float ym = GameYtoMapY(y);
            g.DrawImage(bmp, new RectangleF(0, 0, size.Width, size.Height), new RectangleF(xm - 50, ym - 50, 100, 100), GraphicsUnit.Pixel); 
        }

        public static float GameXtoMapX(int x)
        {
            return 1.0f * (x + 327590) / 133.18f;
        }

        public static float GameYtoMapY(int y)
        {
            return 1.0f * (y + 261460) / 133.18f;
        }
    }
}