using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace ulHelper.App.Drawing
{
    public class CheckTextButton
    {
        public bool Checked { get; set; }
        public string Text { get; set; }
        public int Left { get; set; }
        public int Top { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public Brush FontBrush { get; set; }

        public event EventHandler CheckedChanged;

        private PictureBox _pb;

        public CheckTextButton(PictureBox pb)
        {
            this._pb = pb;
        }

        public void Draw(Graphics g)
        {
            g.DrawRectangle(Pens.Black, Left, Top, Width, Height);
            g.DrawString(Text, GUI.Font, FontBrush, Left, Top + 1);
        }
    }
}