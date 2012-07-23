using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace ulHelper.App.Drawing
{
    public abstract class SimpleBar
    {
        protected Bitmap _bg, _active;
        protected int _width, _height;

        public void Draw(Graphics g, int left, int top, int cur, int max)
        {
            float pos = max == 0 ? 0 : 1f * _width * cur / max;
            g.DrawImage(_bg, left, top);
            g.DrawImage(_active, left, top, pos, _height);
        }
    }
}