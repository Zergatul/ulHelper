using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace ulHelper.App.Drawing
{
    public abstract class Bar
    {
        protected Bitmap _bg, _active;
        protected int _top, _left, _width, _height;
        protected BarInfoMode _mode;

        public event EventHandler RequestUpdate;

        public void OnMouseClick(int x, int y)
        {
            if (x >= _left && y >= _top && x < _left + _width && y < _top + _height)
            {
                _mode = _mode.GetNext();
                PerformRequestUpdate();
            }
        }

        protected void PerformRequestUpdate()
        {
            if (RequestUpdate != null)
                RequestUpdate(this, EventArgs.Empty);
        }

        public void Draw(Graphics g, int cur, int max)
        {
            float pos = max == 0 ? 0 : 1f * _width * cur / max;
            g.DrawImage(_bg, _left, _top);
            g.DrawImage(_active, _left, _top, pos, _height);
            string str = null;
            if (_mode == BarInfoMode.Absolutely)
                str = cur + " / " + max;
            if (_mode == BarInfoMode.Relative)
                str = Math.Round(max == 0 ? 0 : 100f * cur / max).ToString() + "%";
            if (_mode == BarInfoMode.Both)
                str = cur + " / " + max + " [" + Math.Round(max == 0 ? 0 : 100f * cur / max).ToString() + "%" + "]";
            float strWidth = g.MeasureString(str, GUI.Font).Width;
            g.DrawString(str, GUI.Font, Brushes.White, _left + (_width - strWidth) / 2, _top);
        }
    }
}