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
    public class UserPanel : IDisposable
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

        public UserPanel(Control parent, GameWorld world)
        {
            this.world = world;
            this.world.PlayerUpdate += (s, e) => { this.Update(); };

            pb = new PictureBox();
            pb.Width = 230;
            pb.Height = 57;
            pb.Top = 0;
            pb.Left = 0;
            pb.Paint += pb_Paint;
            pb.MouseDown += pb_MouseDown;
            parent.Controls.Add(pb);

            form = parent.FindForm();
            form.HandleCreated += form_HandleCreated;

            cp = new CPBar(4, 3, pb.Width - 8);
            cp.RequestUpdate += bar_RequestUpdate;
            hp = new HPBar(4, 16, pb.Width - 8);
            hp.RequestUpdate += bar_RequestUpdate;
            mp = new MPBar(27, 29, pb.Width - 8 - 23);
            mp.RequestUpdate += bar_RequestUpdate;
            exp = new ExpBar(pb.Width - 8 - 23);
            lvl = new LevelBar();
        }

        void pb_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
                return;

            cp.OnMouseClick(e.X, e.Y);
            hp.OnMouseClick(e.X, e.Y);
            mp.OnMouseClick(e.X, e.Y);
        }

        void bar_RequestUpdate(object sender, EventArgs e)
        {
            Update();
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
            cp.Draw(e.Graphics, world.User.CurCP, world.User.MaxCP);
            hp.Draw(e.Graphics, world.User.CurHP, world.User.MaxHP);
            mp.Draw(e.Graphics, world.User.CurMP, world.User.MaxMP);
            exp.Draw(e.Graphics, 27, 42,
                world.User.Exp - Info.LevelsExp[world.User.Level],
                world.User.Level == 99 ? 0 : Info.LevelsExp[world.User.Level + 1] - Info.LevelsExp[world.User.Level]);
            lvl.Draw(e.Graphics, 4, 31, world.User.Level);
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
                /*redrawThread.Join();*/
                if (redrawThread != null)
                    redrawThread.Abort();
                _disposed = true;
            }
        }

        #endregion
    }
}