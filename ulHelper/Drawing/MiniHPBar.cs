﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace ulHelper.App.Drawing
{
    public class MiniHPBar : SimpleBar
    {
        static Bitmap leftBmp, rightBmp, centerBmp, leftBgBmp, rightBgBmp, centerBgBmp;

        public static void Load()
        {
            leftBmp = new Bitmap(@"Images\HP_mini_left.png");
            centerBmp = new Bitmap(@"Images\HP_mini_center.png");
            rightBmp = new Bitmap(@"Images\HP_mini_right.png");

            leftBgBmp = new Bitmap(@"Images\HP_mini_bg_left.png");
            centerBgBmp = new Bitmap(@"Images\HP_mini_bg_center.png");
            rightBgBmp = new Bitmap(@"Images\HP_mini_bg_right.png");
        }

        public MiniHPBar(int width)
        {
            this._width = width;
            this._height = 10;
            this._bg = new Bitmap(width, 10);
            this._active = new Bitmap(width, 10);
            PrepareBitmaps();
        }

        void PrepareBitmaps()
        {
            using (var g = Graphics.FromImage(_bg))
            {
                g.DrawImage(leftBgBmp, 0, 0);
                g.DrawImage(rightBgBmp, _width - rightBgBmp.Width, 0);
                g.DrawImage(centerBgBmp,
                    new RectangleF(leftBgBmp.Width, 0, _width - leftBgBmp.Width - rightBgBmp.Width, 10),
                    new RectangleF(0, 0, 1, 10),
                    GraphicsUnit.Pixel);
            }
            using (var g = Graphics.FromImage(_active))
            {
                g.DrawImage(leftBmp, 0, 0);
                g.DrawImage(rightBmp, _width - rightBmp.Width, 0);
                g.DrawImage(centerBmp,
                    new RectangleF(leftBmp.Width, 0, _width - leftBmp.Width - rightBmp.Width, 10),
                    new RectangleF(0, 0, 1, 10),
                    GraphicsUnit.Pixel);
            }
        }
    }
}