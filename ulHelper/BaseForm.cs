using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace ulHelper.App
{
    public partial class BaseForm : Form
    {
        static Color bgColor = Color.FromArgb(0x63, 0x6C, 0x87);

        GraphicsPath _borderPath;
        Pen _borderPen;
        GraphicsPath _captionPath;
        LinearGradientBrush _captionBrush;
        int _captionHeight = 18;
        int _borderRadius = 5;
        int _captionUp = 2;
        Font _captionFont;
        bool _drag;
        bool _onCloseBtn;
        Point _dragPoint;
        Rectangle _closeBtnRect;
        Pen _closeBtnPen;
        LinearGradientBrush _btnSelectedBrush;

        public BaseForm()
        {
            InitializeComponent();

            _borderPen = new Pen(Color.Black, 3f);
            _captionBrush = new LinearGradientBrush(
                new Point(0, _borderRadius - 2),
                new Point(0, _borderRadius + _captionHeight - 2),
                Color.FromArgb(0x05, 0x06, 0x06),
                Color.FromArgb(0x59, 0x61, 0x7A));
            _btnSelectedBrush = new LinearGradientBrush(
                new Point(0, 5),
                new Point(0, 5 + 14),
                Color.FromArgb(0x9F, 0x9F, 0xA0),
                Color.FromArgb(0x45, 0x46, 0x46));
            _captionFont = new Font("Tahoma", 11, FontStyle.Regular, GraphicsUnit.Pixel);
            _closeBtnPen = new Pen(Color.White, 2);

            this.BackColor = bgColor;
            this.MouseDown += BaseForm_MouseDown;
            this.MouseMove += BaseForm_MouseMove;
            this.MouseUp += BaseForm_MouseUp;
            this.GotFocus += BaseForm_GotFocus;
            this.LostFocus += BaseForm_LostFocus;
            this.SizeChanged += BaseForm_SizeChanged;
            this.Paint += BaseForm_Paint;
        }

        void BaseForm_LostFocus(object sender, EventArgs e)
        {
            Invalidate();
        }

        void BaseForm_GotFocus(object sender, EventArgs e)
        {
            Invalidate();
        }

        void BaseForm_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Y >= _borderRadius - _captionUp && e.Y < _borderRadius - _captionUp + _captionHeight)
            {
                _drag = true;
                _dragPoint = e.Location;
            }
        }

        void BaseForm_MouseMove(object sender, MouseEventArgs e)
        {
            if (_closeBtnRect.Contains(e.Location))
            {
                if (!_onCloseBtn)
                {
                    _onCloseBtn = true;
                    Invalidate();
                    return;
                }
            }
            else
            {
                if (_onCloseBtn)
                {
                    _onCloseBtn = false;
                    this.Invalidate();
                }
            }
            if (_drag)
            {
                var p = this.PointToScreen(e.Location);
                this.Location = new Point(p.X - _dragPoint.X, p.Y - _dragPoint.Y);
            }
        }

        void BaseForm_MouseUp(object sender, MouseEventArgs e)
        {
            _drag = false;

            if (_closeBtnRect.Contains(e.Location))
                this.Close();
        }

        void BaseForm_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawPath(_borderPen, _borderPath);
            DrawCaption(e.Graphics);
        }

        void DrawCaption(Graphics g)
        {
            g.FillPath(_captionBrush, _captionPath);
            Brush captBrush = this.Focused ? Brushes.White : Brushes.LightGray;
            g.DrawString(this.Text, _captionFont, captBrush, _borderRadius + 2, _borderRadius);

            if (_onCloseBtn)
            {
                g.FillRectangle(_btnSelectedBrush, _closeBtnRect);
                g.DrawRectangle(Pens.Brown, _closeBtnRect);
            }
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.DrawLine(_closeBtnPen, _closeBtnRect.Left + 3, _closeBtnRect.Top + 3, _closeBtnRect.Right - 3, _closeBtnRect.Bottom - 3);
            g.DrawLine(_closeBtnPen, _closeBtnRect.Left + 3, _closeBtnRect.Bottom - 3, _closeBtnRect.Right - 3, _closeBtnRect.Top + 3);
            g.SmoothingMode = SmoothingMode.Default;
        }

        private void BaseForm_SizeChanged(object sender, EventArgs e)
        {
            int r = _borderRadius;
            int da = 5;

            _borderPath = new GraphicsPath();
            _borderPath.StartFigure();
            _borderPath.AddArc(0, 0, 2 * r + 1, 2 * r + 1, 180 - da, 90 + 2 * da);
            _borderPath.AddArc(Width - 2 * r - 1, 0, 2 * r + 1, 2 * r + 1, -90 - da, 90 + 2 * da);
            _borderPath.AddArc(Width - 2 * r - 1, Height - 2 * r - 1, 2 * r + 1, 2 * r + 1, 0 - da, 90 + 2 * da);
            _borderPath.AddArc(0, Height - 2 * r - 1, 2 * r + 1, 2 * r + 1, 90 - da, 90 + 2 * da);
            _borderPath.CloseFigure();

            int cu = _captionUp;
            _captionPath = new GraphicsPath();
            _captionPath.StartFigure();
            _captionPath.AddArc(r - cu, r - cu, 2 * r + 1, 2 * r + 1, 180 - da, 90 + 2 * da);
            _captionPath.AddArc(Width - 3 * r - 1 + cu, r - cu, 2 * r + 1, 2 * r + 1, -90 - da, 90 + 2 * da);
            _captionPath.AddArc(Width - 3 * r - 1 + cu, _captionHeight - r - 1 - cu, 2 * r + 1, 2 * r + 1, 0 - da, 90 + 2 * da);
            _captionPath.AddArc(r - cu, _captionHeight - r - 1 - cu, 2 * r + 1, 2 * r + 1, 90 - da, 90 + 2 * da);
            _captionPath.CloseFigure();

            this.Region = new Region(_borderPath);

            _closeBtnRect = new Rectangle(this.Width - 21, 5, 14, 14);
        }
    }
}