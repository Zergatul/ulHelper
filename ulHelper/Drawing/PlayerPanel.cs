using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Drawing;
using ulHelper.L2Objects;

namespace ulHelper.App.Drawing
{
    public class PlayerPanel
    {
        PictureBox pb;
        bool needRedraw;
        Thread redrawThread;
        Color bgColor = Color.FromArgb(0x63, 0x6C, 0x87);
        GameWorld world;

        CPBar cp;
        HPBar hp;
        MPBar mp;

        public PlayerPanel(Control parent, GameWorld world)
        {
            this.world = world;
            this.world.PlayerStatusUpdate += (s, e) => { this.Update(); };

            pb = new PictureBox();
            pb.Width = 230;
            pb.Height = 57;
            pb.Top = 0;
            pb.Left = 0;
            pb.Paint += pb_Paint;
            parent.Controls.Add(pb);

            parent.FindForm().HandleCreated += form_HandleCreated;

            cp = new CPBar(pb.Width - 8);
            hp = new HPBar(pb.Width - 8);
            mp = new MPBar(pb.Width - 8 - 23);
        }

        void form_HandleCreated(object sender, EventArgs e)
        {
            redrawThread = new Thread((ThreadStart)RedrawThreadFunc);
            redrawThread.IsBackground = true;
            redrawThread.Start();
            Update();
        }

        void pb_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            DrawBorder(e.Graphics);
            cp.Draw(e.Graphics, 4, 4, 12332, 22332);
            hp.Draw(e.Graphics, 4, 17, 15783, 21342);
            mp.Draw(e.Graphics, 27, 30, 8902, 11921);
        }

        public void Update()
        {
            needRedraw = true;
        }

        void RedrawThreadFunc()
        {
            int delay = Properties.Settings.Default.PlayerPanelRefreshTime;
            while (true)
            {
                needRedraw = false;
                pb.Invoke((ThreadStart)(() => { pb.Invalidate(); }));
                Thread.Sleep(delay);
                while (!needRedraw)
                    Thread.Sleep(1);
            }
        }

        void DrawBorder(Graphics g)
        {
            g.Clear(bgColor);
            g.DrawRectangle(Pens.Black, 0, 0, pb.Width - 1, pb.Height - 1);
        }
    }
}