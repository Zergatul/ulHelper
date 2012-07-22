using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace ulHelper.App.Drawing
{
    public class MPBar : Bar
    {
        static Bitmap leftBmp, rightBmp, centerBmp, leftBgBmp, rightBgBmp, centerBgBmp;

        public static void Load()
        {
            leftBmp = new Bitmap(@"Images\MP_left.png");
            centerBmp = new Bitmap(@"Images\MP_center.png");
            rightBmp = new Bitmap(@"Images\MP_right.png");

            leftBgBmp = new Bitmap(@"Images\MP_bg_left.png");
            centerBgBmp = new Bitmap(@"Images\MP_bg_center.png");
            rightBgBmp = new Bitmap(@"Images\MP_bg_right.png");
            BitmapFunctions.RemoveAlphaChannel(leftBgBmp);
            BitmapFunctions.RemoveAlphaChannel(centerBgBmp);
            BitmapFunctions.RemoveAlphaChannel(rightBgBmp);
        }

        public MPBar(int left, int top, int width)
        {
            this._left = left;
            this._top = top;
            this._width = width;
            this._height = 12;
            this._bg = new Bitmap(width, 12);
            this._active = new Bitmap(width, 12);
            this._mode = BarInfoMode.Absolutely;
            PrepareBitmaps();
        }

        unsafe void PrepareBitmaps()
        {
            using (var g = Graphics.FromImage(_bg))
            {
                g.DrawImage(leftBgBmp, 0, 0);
                g.DrawImage(rightBgBmp, _width - rightBgBmp.Width, 0);
                g.DrawImage(centerBgBmp,
                    new RectangleF(leftBgBmp.Width, 0, _width - leftBgBmp.Width - rightBgBmp.Width, 12),
                    new RectangleF(0, 0, 1, 12),
                    GraphicsUnit.Pixel);
            }
            using (var g = Graphics.FromImage(_active))
            {
                g.DrawImage(leftBmp, 0, 0);
                g.DrawImage(rightBmp, _width - rightBmp.Width, 0);
                g.DrawImage(centerBmp,
                    new RectangleF(leftBmp.Width, 0, _width - leftBmp.Width - rightBmp.Width, 12),
                    new RectangleF(0, 0, 1, 12),
                    GraphicsUnit.Pixel);
            }
        }
    }
}