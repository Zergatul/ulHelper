using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace ulHelper.App.Drawing
{
    public class HPBar
    {
        static Bitmap leftBmp, rightBmp, centerBmp, leftBgBmp, rightBgBmp, centerBgBmp;
        static Font font = new Font("Arial", 8.25f, FontStyle.Bold, GraphicsUnit.Point);

        public static void Load()
        {
            leftBmp = new Bitmap(@"Images\HP_left.png");
            centerBmp = new Bitmap(@"Images\HP_center.png");
            rightBmp = new Bitmap(@"Images\HP_right.png");

            leftBgBmp = new Bitmap(@"Images\HP_bg_left.png");
            centerBgBmp = new Bitmap(@"Images\HP_bg_center.png");
            rightBgBmp = new Bitmap(@"Images\HP_bg_right.png");
        }

        Bitmap bg, active;
        int width;

        public HPBar(int width)
        {
            this.width = width;
            bg = new Bitmap(width, 12);
            active = new Bitmap(width, 12);
            PrepareBitmaps();
        }

        unsafe void PrepareBitmaps()
        {
            using (var g = Graphics.FromImage(bg))
            {
                g.DrawImage(leftBgBmp, 0, 0);
                g.DrawImage(rightBgBmp, width - rightBgBmp.Width, 0);
                g.DrawImage(centerBgBmp,
                    new RectangleF(leftBgBmp.Width, 0, width - leftBgBmp.Width - rightBgBmp.Width, 12),
                    new RectangleF(0, 0, 1, 12),
                    GraphicsUnit.Pixel);
            }
            using (var g = Graphics.FromImage(active))
            {
                g.DrawImage(leftBmp, 0, 0);
                g.DrawImage(rightBmp, width - rightBmp.Width, 0);
                g.DrawImage(centerBmp,
                    new RectangleF(leftBmp.Width, 0, width - leftBmp.Width - rightBmp.Width, 12),
                    new RectangleF(0, 0, 1, 12),
                    GraphicsUnit.Pixel);
            }
        }

        public void Draw(Graphics g, int x, int y, int cur, int max)
        {
            float pos = 1f * width * cur / max;
            g.DrawImage(bg, x, y);
            g.DrawImage(active, x, y, pos, 12);
            var str = cur + " / " + max;
            float strWidth = g.MeasureString(str, font).Width;
            g.DrawString(str, font, Brushes.White, x + (width - strWidth) / 2, y);
        }
    }
}