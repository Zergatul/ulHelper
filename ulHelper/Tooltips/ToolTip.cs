using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace ulHelper.App.Tooltips
{
    public abstract class ToolTip : IDisposable
    {
        protected ToolTipForm _form;
        protected System.Drawing.Drawing2D.GraphicsPath _regionPath;

        protected ToolTip(int width, int height)
        {
            this._form = new ToolTipForm();
            this._form.Width = width;
            this._form.Height = height;

            this._regionPath = new System.Drawing.Drawing2D.GraphicsPath();
            int r = 8;
            int da = 5;

            this._regionPath.StartFigure();
            this._regionPath.AddArc(0, 0, 2 * r + 1, 2 * r + 1, 180 - da, 90 + 2 * da);
            this._regionPath.AddArc(_form.Width - 2 * r - 1, 0, 2 * r + 1, 2 * r + 1, -90 - da, 90 + 2 * da);
            this._regionPath.AddArc(_form.Width - 2 * r - 1, _form.Height - 2 * r - 1, 2 * r + 1, 2 * r + 1, 0 - da, 90 + 2 * da);
            this._regionPath.AddArc(0, _form.Height - 2 * r - 1, 2 * r + 1, 2 * r + 1, 90 - da, 90 + 2 * da);
            this._regionPath.CloseFigure();

            this._form.Region = new Region(_regionPath);
        }

        public virtual void Update()
        {
            if (Visible)
                this._form.UpdateImage();
        }

        public virtual void Show()
        {
            this._form.ShowToolTip();
        }

        public virtual void Show(Point loc)
        {
            this._form.ShowToolTip(loc);
        }

        public virtual void Hide()
        {
            _form.InvokeIfNeeded(_form.HideToolTip);
        }

        public virtual bool Visible
        {
            get
            {
                return this._form.Visible;
            }
            set
            {
                if (value)
                    this._form.ShowToolTip();
                else
                    this._form.HideToolTip();
            }
        }

        #region Dispose pattern

        private bool _disposed;

        public virtual void Dispose()
        {
            if (!_disposed)
            {
                _form.DisposeResources();
                _form.Dispose();
                _disposed = true;
            }
        }

        #endregion
    }
}