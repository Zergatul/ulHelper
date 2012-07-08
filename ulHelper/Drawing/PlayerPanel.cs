using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Drawing;
using ulHelper.L2Objects;
using ulHelper.App.Tooltips;
using ulHelper.GameInfo;

namespace ulHelper.App.Drawing
{
    public class PlayerPanel : IDisposable
    {
        PictureBox pb;
        Form form;
        bool needRedraw;
        volatile bool needTerminate;
        Thread redrawThread;
        GameWorld world;

        CPBar cp;
        HPBar hp;
        MPBar mp;
        ExpBar exp;
        LevelBar lvl;

        public PlayerPanel(Control parent, GameWorld world)
        {
            this.world = world;
            this.world.PlayerUpdate += (s, e) => { this.Update(); };

            pb = new PictureBox();
            pb.Width = 230;
            pb.Height = 57;
            pb.Top = 0;
            pb.Left = 0;
            pb.Paint += pb_Paint;
            parent.Controls.Add(pb);

            form = parent.FindForm();
            form.HandleCreated += form_HandleCreated;

            cp = new CPBar(pb.Width - 8);
            hp = new HPBar(pb.Width - 8);
            mp = new MPBar(pb.Width - 8 - 23);
            exp = new ExpBar(pb.Width - 8 - 23);
            lvl = new LevelBar();
        }

        void form_HandleCreated(object sender, EventArgs e)
        {
            redrawThread = new Thread((ThreadStart)RedrawThreadFunc);
            redrawThread.Start();
            Update();
        }

        void pb_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            DrawBorder(e.Graphics);
            cp.Draw(e.Graphics, 4, 3, world.Player.CurCP, world.Player.MaxCP);
            hp.Draw(e.Graphics, 4, 16, world.Player.CurHP, world.Player.MaxHP);
            mp.Draw(e.Graphics, 27, 29, world.Player.CurMP, world.Player.MaxMP);
            exp.Draw(e.Graphics, 27, 42,
                world.Player.Exp - Info.LevelsExp[world.Player.Level],
                world.Player.Level == 99 ? 0 : Info.LevelsExp[world.Player.Level + 1] - Info.LevelsExp[world.Player.Level]);
            lvl.Draw(e.Graphics, 4, 31, world.Player.Level);
        }

        public void Update()
        {
            needRedraw = true;
        }

        void RedrawThreadFunc()
        {
            int delay = Properties.Settings.Default.PlayerPanelRefreshTime;
            while (!needTerminate)
            {
                needRedraw = false;
                form.InvokeIfNeeded(() => { pb.Invalidate(); });
                Thread.Sleep(delay);
                while (!needRedraw)
                    Thread.Sleep(1);
            }
        }

        void DrawBorder(Graphics g)
        {
            g.Clear(GUI.BgColor);
            GUI.RoundedRectangle(g, 0, 0, pb.Width - 1, pb.Height - 1);
        }

        #region Dispose pattern

        private bool _disposed;

        public void Dispose()
        {
            if (!_disposed)
            {
                needRedraw = true;
                needTerminate = true;
                redrawThread.Join();
                _disposed = true;
            }
        }

        #endregion
    }
}