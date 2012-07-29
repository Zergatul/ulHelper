using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;

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
        private bool _mouseOver;
        private LinearGradientBrush _mouseOverBrush;

        private static Pen _borderPen = new Pen(Color.DarkGray, 1);

        public CheckTextButton(PictureBox pb)
        {
            this._pb = pb;
            pb.MouseUp += new MouseEventHandler(pb_MouseUp);
            pb.MouseMove += new MouseEventHandler(pb_MouseMove);
            pb.MouseLeave += new EventHandler(pb_MouseLeave);
        }

        void pb_MouseLeave(object sender, EventArgs e)
        {
            if (this._mouseOver)
            {
                this._mouseOver = false;
                this._pb.Invalidate();
            }
        }

        void pb_MouseMove(object sender, MouseEventArgs e)
        {
            if (Left <= e.X && e.X <= Left + Width && Top <= e.Y && e.Y <= Top + Height)
            {
                if (!this._mouseOver)
                {
                    this._mouseOver = true;
                    this._pb.Invalidate();
                }
            }
            else
                if (this._mouseOver)
                {
                    this._mouseOver = false;
                    this._pb.Invalidate();
                }            
        }

        void pb_MouseUp(object sender, MouseEventArgs e)
        {
            if (this._mouseOver)
            {
                Checked = !Checked;
                PerformCheckedChanged();
            }
        }

        public void Draw(Graphics g)
        {
            if (this._mouseOver)
                g.FillRectangle(this._mouseOverBrush, Left, Top, Width, Height);
            if (this.Checked)
                g.DrawRectangle(_borderPen, Left, Top, Width, Height);
            int dx = (Width - (int)g.MeasureString(Text, GUI.Font).Width) / 2;
            g.DrawString(Text, GUI.Font, FontBrush, Left + dx, Top + 1);
        }

        void PerformCheckedChanged()
        {
            if (CheckedChanged != null)
                CheckedChanged(this, EventArgs.Empty);
        }

        public void PrepareBrushes()
        {
            _mouseOverBrush = new LinearGradientBrush(
                new Point(0, Top - 1), new Point(0, Top + Height + 1),
                Color.LightGray, Color.DarkRed);
        }
    }
}