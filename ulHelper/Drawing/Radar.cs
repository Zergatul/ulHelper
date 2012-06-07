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
    public class Radar
    {
        PictureBox pb;
        Form form;
        Thread redrawThread;
        GameWorld world;
        float scale;
        static Pen NeutralPen = new Pen(GUI.NeutralColor, 2f);
        static Pen PlayerPen = new Pen(GUI.PlayerColor, 2f);

        public Radar(Control parent, GameWorld world)
        {
            this.world = world;
            this.scale = 100;

            pb = new PictureBox();
            pb.Width = 230;
            pb.Height = 230;
            pb.Top = 104;
            pb.Left = 0;
            pb.Paint += pb_Paint;
            parent.Controls.Add(pb);

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
            }
        }

        void form_HandleCreated(object sender, EventArgs e)
        {
            redrawThread = new Thread((ThreadStart)RedrawThreadFunc);
            redrawThread.IsBackground = true;
            redrawThread.Start();
        }

        void pb_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            DrawBorder(e.Graphics);

            int pX, pY;
            lock (world.Player)
            {
                //WorldMap.DrawAt(e.Graphics, 3, 3, pb.Width - 6, pb.Height - 6, world.Player.X, world.Player.Y, scale);
                pX = world.Player.X;
                pY = world.Player.Y;
            }
            float xc = 1.0f * pb.Width / 2;
            float yc = 1.0f * pb.Height / 2;

            lock (world.Characters)
                foreach (var ch in world.Characters)
                {
                    int dx = ch.X - pX;
                    int dy = ch.Y - pY;
                    e.Graphics.DrawEllipse(NeutralPen,
                        xc + WorldMap.ScaleX(dx) * pb.Width / scale,
                        yc + WorldMap.ScaleX(dy) * pb.Height / scale,
                        4, 4);
                }

            e.Graphics.DrawEllipse(PlayerPen, xc - 2, yc - 2, 4, 4);
        }

        void RedrawThreadFunc()
        {
            int delay = Properties.Settings.Default.RadarRefreshTime;
            while (true)
            {
                if (form.IsHandleCreated)
                    pb.Invoke((ThreadStart)(() => { pb.Invalidate(); }));
                Thread.Sleep(delay);
            }
        }

        void DrawBorder(Graphics g)
        {
            g.Clear(GUI.BgColor);
            GUI.RoundedRectangle(g, 0, 0, pb.Width - 1, pb.Height - 1);
        }
    }
}