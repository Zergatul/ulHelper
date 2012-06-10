using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using ulHelper.L2Objects;
using System.Drawing;

namespace ulHelper.App.Drawing
{
    public class Radar : IDisposable
    {
        static float minScale = 10;
        static float maxScale = 500;
        static Pen NeutralPen = new Pen(GUI.NeutralColor, 1.5f);
        static Pen PlayerPen = new Pen(GUI.PlayerColor, 1.5f);

        PictureBox pb;
        Form form;
        bool needTerminate;
        Thread redrawThread;
        GameWorld world;
        float scale;
        Rectangle clipRectanle;

        public Radar(Control parent, GameWorld world)
        {
            this.world = world;
            this.scale = 100;

            pb = new PictureBox();
            pb.Width = 230;
            pb.Height = 230;
            pb.Top = 116;
            pb.Left = 0;
            pb.Paint += pb_Paint;
            parent.Controls.Add(pb);

            this.clipRectanle = new Rectangle(3, 3, pb.Width - 6, pb.Height - 6);

            form = parent.FindForm();
            form.HandleCreated += form_HandleCreated;
            form.MouseWheel += Radar_MouseWheel;
        }

        void Radar_MouseWheel(object sender, MouseEventArgs e)
        {
            if (pb.Bounds.Contains(e.Location))
            {
                int wheelCount = e.Delta / 120;
                scale *= (float)Math.Pow(1.05f, -wheelCount);
                if (scale < minScale)
                    scale = minScale;
                if (scale > maxScale)
                    scale = maxScale;
            }
        }

        void form_HandleCreated(object sender, EventArgs e)
        {
            redrawThread = new Thread((ThreadStart)RedrawThreadFunc);
            redrawThread.Start();
        }

        void pb_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            DrawBorder(e.Graphics);

            int pX, pY;
            WorldMap.DrawAt(e.Graphics, 3, 3, pb.Width - 6, pb.Height - 6, world.Player.IntX, world.Player.IntY, scale);
            pX = world.Player.IntX;
            pY = world.Player.IntY;
            float xc = 1.0f * pb.Width / 2;
            float yc = 1.0f * pb.Height / 2;

            List<L2Character> characters;
            lock (world.Characters)
                characters = new List<L2Character>(world.Characters);

            e.Graphics.SetClip(clipRectanle);
            foreach (var ch in characters)
            {
                int dx = ch.IntX - pX;
                int dy = ch.IntY - pY;
                e.Graphics.DrawEllipse(NeutralPen,
                    xc + WorldMap.ScaleX(dx) * pb.Width / scale - 1.5f,
                    yc + WorldMap.ScaleX(dy) * pb.Height / scale - 1.5f,
                    3, 3);
            }
            e.Graphics.DrawEllipse(PlayerPen, xc - 1.5f, yc - 1.5f, 3, 3);
            e.Graphics.ResetClip();
        }

        void RedrawThreadFunc()
        {
            int delay = Properties.Settings.Default.RadarRefreshTime;
            while (!needTerminate)
            {
                if (form.IsHandleCreated)
                    //if (!(form as AccountForm).NeedTerminate)
                        pb.Invoke((ThreadStart)(() => { pb.Invalidate(); }));
                Thread.Sleep(delay);
            }
        }

        void DrawBorder(Graphics g)
        {
            g.Clear(GUI.BgColor);
            GUI.RoundedRectangle(g, 0, 0, pb.Width - 1, pb.Height - 1);
        }

        #region Dispose pattern

        private bool _disposed;

        public virtual void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    needTerminate = true;
                    //redrawThread.Join();
                    redrawThread.Abort();
                }
                _disposed = true;
            }
        }

        #endregion
    }
}