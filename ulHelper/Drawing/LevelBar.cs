using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace ulHelper.App.Drawing
{
    public class LevelBar
    {
        static Bitmap bmp;

        public static void Load()
        {
            bmp = new Bitmap(@"Images\Level.png");
        }

        public LevelBar()
        {
        }

        public void Draw(Graphics g, int x, int y, int level)
        {
            g.DrawImage(bmp,
                new RectangleF(x, y, 22, 20),
                new RectangleF(0, 0, 22, 20),
                GraphicsUnit.Pixel);
            var str = level.ToString();
            float strWidth = g.MeasureString(str, GUI.Font).Width;
            g.DrawString(str, GUI.Font, Brushes.White, x + (bmp.Width - strWidth) / 2, y + 4);
        }
    }
}