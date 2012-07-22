using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using ulHelper.L2Objects;
using System.Drawing;
using ComponentFactory.Krypton.Toolkit;
using ulHelper.App.Tooltips;

namespace ulHelper.App.Drawing
{
    public class ObjectsPanel : IDisposable
    {
        PictureBox pb;
        Form form;
        bool needRedraw;
        volatile bool needTerminate;
        Thread redrawThread;
        GameWorld world;
        KryptonCheckBox showWar, showAlly, showNeutral, showNpc;
        ObjectsList list;

        public event EventHandler<ObjectClickEventArgs> ObjectClick;

        public ObjectsPanel(Control parent, GameWorld world, CharacterToolTip characterToolTip, NpcToolTip npcToolTip)
        {
            this.world = world;
            this.world.AddCharacter += (s, e) => { this.Update(); };
            this.world.DeleteCharacter += (s, e) => { this.Update(); };
            this.world.AddNpc += (s, e) => { this.Update(); };
            this.world.DeleteNpc += (s, e) => { this.Update(); };
            this.world.L2LiveObjectUpdate += (s, e) => { this.Update(); };

            pb = new PictureBox();
            pb.Width = 230;
            pb.Height = 190;
            pb.Top = 348;
            pb.Left = 0;
            pb.Paint += pb_Paint;
            parent.Controls.Add(pb);

            showWar = new KryptonCheckBox();
            showWar.Text = "";
            showWar.Left = pb.Left + 3; 
            showWar.Top = pb.Top + 174;
            showWar.AutoSize = false;
            showWar.Height = 14;
            showWar.Width = 14;
            parent.Controls.Add(showWar);
            showWar.CheckedChanged += showWar_CheckedChanged;
            showWar.BringToFront();

            showAlly = new KryptonCheckBox();
            showAlly.Text = "";
            showAlly.Left = showWar.Left + 55;
            showAlly.Top = showWar.Top;
            showAlly.AutoSize = false;
            showAlly.Height = 14;
            showAlly.Width = 14;
            parent.Controls.Add(showAlly);
            showAlly.CheckedChanged += showAlly_CheckedChanged;
            showAlly.BringToFront();

            showNeutral = new KryptonCheckBox();
            showNeutral.Text = "";
            showNeutral.Left = showAlly.Left + 58;
            showNeutral.Top = showWar.Top;
            showNeutral.AutoSize = false;
            showNeutral.Height = 14;
            showNeutral.Width = 14;
            showNeutral.Checked = true;
            parent.Controls.Add(showNeutral);
            showNeutral.CheckedChanged += showNeutral_CheckedChanged;
            showNeutral.BringToFront();

            showNpc = new KryptonCheckBox();
            showNpc.Text = "";
            showNpc.Left = showNeutral.Left + 58;
            showNpc.Top = showWar.Top;
            showNpc.AutoSize = false;
            showNpc.Height = 14;
            showNpc.Width = 14;
            parent.Controls.Add(showNpc);
            showNpc.CheckedChanged += showNpc_CheckedChanged;
            showNpc.BringToFront();

            form = parent.FindForm();
            form.HandleCreated += form_HandleCreated;
            list = new ObjectsList(this, pb, world, 3, 3, 223, 168, 
                showWar, showAlly, showNeutral, showNpc,
                characterToolTip, npcToolTip);
            list.ObjectClick += (s, e) => { if (ObjectClick != null) ObjectClick(this, e); };
        }

        void showNpc_CheckedChanged(object sender, EventArgs e)
        {
            Update();
        }

        void showNeutral_CheckedChanged(object sender, EventArgs e)
        {
            Update();
        }

        void showAlly_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        void showWar_CheckedChanged(object sender, EventArgs e)
        {
            
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
            list.Draw(e.Graphics);

            e.Graphics.DrawString("war:-", GUI.Font, GUI.WarBrush, showWar.Left + 12, showWar.Top - pb.Top);
            e.Graphics.DrawString("ally:-", GUI.Font, GUI.AllyBrush, showAlly.Left + 12, showAlly.Top - pb.Top);
            e.Graphics.DrawString("neu.:" + world.Characters.Count, GUI.Font, GUI.NeutralBrush, showNeutral.Left + 12, showNeutral.Top - pb.Top);
            
            e.Graphics.DrawString("npc:" + world.Npcs.Count, GUI.Font, GUI.NpcBrush, showNpc.Left + 12, showNpc.Top - pb.Top);
        }

        public void Update()
        {
            needRedraw = true;
        }

        void RedrawThreadFunc()
        {
            int delay = Properties.Settings.Default.ObjectsPanelRefreshTime;
            while (!needTerminate)
            {
                needRedraw = false;
                if (form.IsHandleCreated)
                    pb.Invoke((ThreadStart)(() => { if (form.IsHandleCreated) pb.Invalidate(); }));
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
                /*needRedraw = true;
                needTerminate = true;
                redrawThread.Join();*/
                redrawThread.Abort();
                _disposed = true;
            }
        }

        #endregion
    }
}