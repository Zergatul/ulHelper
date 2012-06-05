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

        public static void DrawAt(Graphics g, int x, int y, int width, int height, int gameX, int gameY, float scale)
        {
            float xm = GameXtoMapX(gameX);
            float ym = GameYtoMapY(gameY);
            g.DrawImage(bmp, 
                new RectangleF(x, y, width, height),
                new RectangleF(xm - scale / 2, ym - scale / 2, scale, scale),
                GraphicsUnit.Pixel); 
        }

        public static float GameXtoMapX(int x)
        {
            return 1.0f * (x + 327590) / 133.18f;
        }

        public static float GameYtoMapY(int y)
        {
            return 1.0f * (y + 261460) / 133.18f;
        }

        public static float ScaleX(int dx)
        {
            return dx / 133.18f;
        }

        public static float ScaleY(int dy)
        {
            return dy / 133.18f;
        }
    }
}