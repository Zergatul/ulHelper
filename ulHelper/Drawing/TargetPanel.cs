using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using ulHelper.L2Objects;
using System.Drawing;
using ulHelper.App.Tooltips;

namespace ulHelper.App.Drawing
{
    public class TargetPanel : IDisposable
    {
        static Bitmap settingsBmp, settingsOverBmp;

        public static void Load()
        {
            settingsBmp = new Bitmap(@"Images\settings.png");
            settingsOverBmp = new Bitmap(@"Images\settings_over.png");
        }

        PictureBox pb;
        Form form;
        bool needRedraw;
        volatile bool needTerminate;
        Thread redrawThread;
        GameWorld world;
        HPBar hpNpc;
        HPBar hpChar;
        MPBar mpNpc;
        MPBar mpChar;
        LevelBar lvl;
        bool buttonHovered;
        CharacterToolTip characterToolTip;
        NpcToolTip npcToolTip;
        ulHelper.App.Tooltips.ToolTip currentToolTip;

        public event EventHandler SettingsClick;

        public TargetPanel(Control parent, GameWorld world, CharacterToolTip charToolTip, NpcToolTip npcToolTip)
        {
            this.world = world;
            this.characterToolTip = charToolTip;
            this.npcToolTip = npcToolTip;
            this.world.PlayerTargetUpdate += world_TargetPlayerUpdate;

            pb = new PictureBox();
            pb.Width = 230;
            pb.Height = 54;
            pb.Top = 60;
            pb.Left = 0;
            pb.Paint += pb_Paint;
            parent.Controls.Add(pb);
            pb.MouseMove += pb_MouseMove;
            pb.MouseLeave += pb_MouseLeave;
            pb.MouseClick += pb_MouseClick;
            pb.MouseDown += pb_MouseDown;

            form = parent.FindForm();
            form.HandleCreated += form_HandleCreated;

            hpNpc = new HPBar(27, 3, pb.Width - 8 - 23);
            hpNpc.RequestUpdate += bar_RequestUpdate;
            hpChar = new HPBar(3, 3, pb.Width - 8);
            hpChar.RequestUpdate += bar_RequestUpdate;
            mpNpc = new MPBar(27, 16, pb.Width - 8 - 23);
            mpNpc.RequestUpdate += bar_RequestUpdate;
            mpChar = new MPBar(3, 16, pb.Width - 8);
            mpChar.RequestUpdate += bar_RequestUpdate;
            lvl = new LevelBar();
        }

        void pb_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
                return;

            if (world.User.Target != null)
            {
                if (world.User.Target is L2Character)
                {
                    hpChar.OnMouseClick(e.X, e.Y);
                    mpChar.OnMouseClick(e.X, e.Y);
                }
                if (world.User.Target is L2Npc)
                {
                    hpNpc.OnMouseClick(e.X, e.Y);
                    mpNpc.OnMouseClick(e.X, e.Y);
                }
            }
        }

        void bar_RequestUpdate(object sender, EventArgs e)
        {
            Update();
        }

        void pb_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
                return;

            if (buttonHovered)
                if (SettingsClick != null)
                    SettingsClick(this, EventArgs.Empty);            
        }

        void pb_MouseLeave(object sender, EventArgs e)
        {
            if (buttonHovered)
            {
                buttonHovered = false;
                Update();
            }
            if (currentToolTip != null)
                if (currentToolTip.Visible)
                    currentToolTip.Hide();
        }

        void pb_MouseMove(object sender, MouseEventArgs e)
        {
            bool hover = e.X >= pb.Width - 15 && e.Y >= pb.Height - 15;
            if (hover != buttonHovered)
            {
                buttonHovered = hover;
                Update();
            }

            bool toolTipVisible = e.X < pb.Width - 17 || e.Y < pb.Height - 17;
            if (world.User.Target != null)
                if (currentToolTip != null)
                    if (toolTipVisible)
                    {
                        var loc = pb.PointToScreen(e.Location);
                        loc.Offset(16, 16);
                        currentToolTip.Show(loc);
                    }
                    else
                        currentToolTip.Hide();
        }

        void world_TargetPlayerUpdate(object sender, EventArgs e)
        {
            this.Update();
            if (world.User.Target == null)
            {
                if (currentToolTip != null)
                    currentToolTip.Hide();
                currentToolTip = null;
            }
            else
            {
                if (world.User.Target is L2Character)
                {
                    characterToolTip.Character = world.User.Target as L2Character;
                    currentToolTip = characterToolTip;
                }
                else
                {
                    npcToolTip.Npc = world.User.Target as L2Npc;
                    currentToolTip = npcToolTip;
                }
                currentToolTip.Update();
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
            if (buttonHovered)
                e.Graphics.DrawImage(settingsOverBmp, pb.Width - 15, pb.Height - 15);
            else
                e.Graphics.DrawImage(settingsBmp, pb.Width - 15, pb.Height - 15);

            if (world.User.Target != null)
            {
                if (world.User.Target is L2Character)
                {
                    var ch = world.User.Target as L2Character;
                    hpChar.Draw(e.Graphics, ch.CurHP, ch.MaxHP);
                    mpChar.Draw(e.Graphics, ch.CurMP, ch.MaxMP);
                    e.Graphics.DrawString(ch.Name, GUI.Font, GUI.NeutralBrush, 2, 28);
                }
                if (world.User.Target is L2Npc)
                {
                    var npc = world.User.Target as L2Npc;
                    hpNpc.Draw(e.Graphics, npc.CurHP, npc.MaxHP);
                    mpNpc.Draw(e.Graphics, npc.CurMP, npc.MaxMP);
                    lvl.Draw(e.Graphics, 3, 3, npc.Level);
                    e.Graphics.DrawString(npc.Name, GUI.Font, GUI.NpcBrush, 2, 28);
                    var str = "ID: " + npc.NpcID;
                    if (npc.IsNameAbove)
                        str += ", " + npc.OwnerName;
                    e.Graphics.DrawString(str, GUI.Font, Brushes.Black, 2, 40);
                }
            }
        }

        public void Update()
        {
            needRedraw = true;
        }

        void RedrawThreadFunc()
        {
            int delay = Properties.Settings.Default.TargetPanelRefreshTime;
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
            GUI.CroppedRectangle(g, 0, 0, pb.Width - 1, pb.Height - 1, 17, 17);
        }

        #region Dispose pattern

        private bool _disposed;

        public virtual void Dispose()
        {
            if (!_disposed)
            {
                needRedraw = true;
                needTerminate = true;
                //redrawThread.Join();
                if (redrawThread != null)
                    redrawThread.Abort();
                _disposed = true;
            }
        }

        #endregion
    }
}