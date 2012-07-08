using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using ulHelper.L2Objects;
using ulHelper.App.Drawing;
using System.Threading;

namespace ulHelper.App.Tooltips
{
    public class NpcToolTip : IToolTip, IDisposable
    {
        public L2Npc Npc;
        ToolTipForm form;
        System.Drawing.Drawing2D.GraphicsPath regionPath;

        public NpcToolTip()
        {
            form = new ToolTipForm();
            form.Width = 200;
            form.Height = 135;
            form.PictureBox.Paint += pb_Paint;

            regionPath = new System.Drawing.Drawing2D.GraphicsPath();
            int r = 8;
            regionPath.StartFigure();
            regionPath.AddLine(0, 0, r, 0);
            regionPath.AddArc(form.Width - 1 - 2 * r, 0, 2 * r, 2 * r, -90, 90);
            regionPath.AddArc(form.Width - 1 - 2 * r, form.Height - 1 - 2 * r, 2 * r, 2 * r, 0, 90);
            regionPath.AddArc(0, form.Height - 1 - 2 * r, 2 * r, 2 * r, 90, 90);
            regionPath.CloseFigure();
            form.Region = new Region(regionPath);
        }

        public virtual void Update()
        {
            if (Visible)
                form.UpdateImage();
        }

        public virtual void Show()
        {
            form.ShowToolTip();
        }

        public virtual void Show(Point loc)
        {
            form.ShowToolTip(loc);
        }

        public virtual void Hide()
        {
            if (form.InvokeRequired)
                form.Invoke((ThreadStart)form.HideToolTip);
            else
                form.HideToolTip();
        }

        public virtual bool Visible
        {
            get
            {
                return form.Visible;
            }
            set
            {
                if (value)
                    form.ShowToolTip();
                else
                    form.HideToolTip();
            }
        }

        void pb_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            e.Graphics.Clear(ToolTipForm.BgColor);
            e.Graphics.TranslateTransform(-0.5f, -0.5f);
            e.Graphics.DrawPath(ToolTipForm.BorderPen, regionPath);
            e.Graphics.ResetTransform();
            if (Npc != null)
            {
                int y = 3;
                e.Graphics.DrawString("Name: " + Npc.Name, GUI.Font, Brushes.Black, 3, y);
                y += 12;
                e.Graphics.DrawString("ID: " + Npc.NpcID, GUI.Font, Brushes.Black, 3, y);
                y += 12;
                e.Graphics.DrawString("Level: " + Npc.Level, GUI.Font, Brushes.Black, 3, y);
                y += 12;
                e.Graphics.DrawString("Unique name: " + Npc.PetName, GUI.Font, Brushes.Black, 3, y);
                y += 12;
                e.Graphics.DrawString("Owner name: " + Npc.OwnerName, GUI.Font, Brushes.Black, 3, y);
                y += 12;
                e.Graphics.DrawString("HP: " + Npc.CurHP + "/" + Npc.MaxHP, GUI.Font, Brushes.Black, 3, y);
                y += 12;
                e.Graphics.DrawString("MP: " + Npc.CurMP + "/" + Npc.MaxMP, GUI.Font, Brushes.Black, 3, y);
                y += 12;
                e.Graphics.DrawString("Speed: " + Npc.Speed, GUI.Font, Brushes.Black, 3, y);
                y += 12;
                e.Graphics.DrawString("AtkSpeed: " + Npc.AtkSpeed, GUI.Font, Brushes.Black, 3, y);
                y += 12;
                e.Graphics.DrawString("CastSpeed: " + Npc.CastSpeed, GUI.Font, Brushes.Black, 3, y);
                y += 12;
            }
        }

        #region Dispose pattern

        private bool _disposed;

        public virtual void Dispose()
        {
            if (!_disposed)
            {
                form.DisposeResources();
                form.Dispose();
                _disposed = true;
            }
        }

        #endregion
    }
}