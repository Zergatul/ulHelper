using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace ulHelper.App.Drawing
{
    public class MiniHPBar
    {
        static Bitmap bgBmp, actBmp;

        public static void Load()
        {
            bgBmp = new Bitmap(@"Images\HP_mini_bg.png");
            actBmp = new Bitmap(@"Images\HP_mini.png");
        }

        int width;
        Bitmap bg, active;

        public MiniHPBar(int width)
        {
            this.width = width;
            bg = new Bitmap(width, 10);
            active = new Bitmap(width, 10);
            PrepareBitmaps();
        }

        void PrepareBitmaps()
        {
            using (var g = Graphics.FromImage(bg))
                g.DrawImage(bgBmp,
                    new RectangleF(0, 0, width, 10),
                    new RectangleF(0, 0, 106, 10),
                    GraphicsUnit.Pixel);
            using (var g = Graphics.FromImage(active))
                g.DrawImage(actBmp,
                    new RectangleF(0, 0, width, 10),
                    new RectangleF(0, 0, 106, 10),
                    GraphicsUnit.Pixel);
        }

        public void Draw(Graphics g, int x, int y, int cur, int max)
        {
            float pos = max == 0 ? 0 : 1f * width * cur / max;
            g.DrawImage(bg, x, y);
            g.DrawImage(active, x, y, pos, 12);
        }
    }
}