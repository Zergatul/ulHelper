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
            pb.MouseClick += pb_MouseClick;
            parent.Controls.Add(pb);

            form = parent.FindForm();
            form.HandleCreated += form_HandleCreated;

            cp = new CPBar(pb.Width - 8);
            hp = new HPBar(pb.Width - 8);
            mp = new MPBar(pb.Width - 8 - 23);
            exp = new ExpBar(pb.Width - 8 - 23);
            lvl = new LevelBar();
        }

        void pb_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
                return;

            if (e.X >= 4 && e.Y >= 3 && e.X <= 4 + cp.Width && e.Y <= 3 + cp.Height)
            {
                cp.OnMouseClick();
                Update();
            }
            if (e.X >= 4 && e.Y >= 16 && e.X <= 4 + hp.Width && e.Y <= 16 + hp.Height)
            {
                hp.OnMouseClick();
                Update();
            }
            if (e.X >= 27 && e.Y >= 29 && e.X <= 27 + mp.Width && e.Y <= 29 + mp.Height)
            {
                mp.OnMouseClick();
                Update();
            }
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
            cp.Draw(e.Graphics, 4, 3, world.User.CurCP, world.User.MaxCP);
            hp.Draw(e.Graphics, 4, 16, world.User.CurHP, world.User.MaxHP);
            mp.Draw(e.Graphics, 27, 29, world.User.CurMP, world.User.MaxMP);
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
                redrawThread.Join();
                _disposed = true;
            }
        }

        #endregion
    }
}