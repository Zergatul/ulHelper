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
        CheckTextButton showWar, showAlly, showNeutral, showNpc, showDrop;
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

            showWar = new CheckTextButton(pb);
            showWar.Text = "war:-";
            showWar.Left = 3; 
            showWar.Top = 171;
            showWar.Height = 15;
            showWar.Width = 42;
            showWar.FontBrush = GUI.WarBrush;
            showWar.CheckedChanged += showWar_CheckedChanged;

            showAlly = new CheckTextButton(pb);
            showAlly.Text = "ally:-";
            showAlly.Left = showWar.Left + showWar.Width + 4;
            showAlly.Top = showWar.Top;
            showAlly.Height = showWar.Height;
            showAlly.Width = showWar.Width;
            showAlly.FontBrush = GUI.AllyBrush;
            showAlly.CheckedChanged += showAlly_CheckedChanged;

            showNeutral = new CheckTextButton(pb);
            showNeutral.Text = "neu:-";
            showNeutral.Left = showAlly.Left + showAlly.Width + 4;
            showNeutral.Top = showWar.Top;
            showNeutral.Height = showWar.Height;
            showNeutral.Width = showWar.Width;
            showNeutral.Checked = true;
            showNeutral.FontBrush = GUI.NeutralBrush;
            showNeutral.CheckedChanged += showNeutral_CheckedChanged;

            showNpc = new CheckTextButton(pb);
            showNpc.Text = "npc:-";
            showNpc.Left = showNeutral.Left + showNeutral.Width + 4;
            showNpc.Top = showWar.Top;
            showNpc.Height = showWar.Height;
            showNpc.Width = showWar.Width;
            showNpc.FontBrush = GUI.NpcBrush;
            showNpc.CheckedChanged += showNpc_CheckedChanged;

            showDrop = new CheckTextButton(pb);
            showDrop.Text = "drop:-";
            showDrop.Left = showNpc.Left + showNpc.Width + 4;
            showDrop.Top = showWar.Top;
            showDrop.Height = showWar.Height;
            showDrop.Width = showWar.Width;
            showDrop.FontBrush = GUI.DropBrush;
            //showDrop.CheckedChanged += showNpc_CheckedChanged;

            form = parent.FindForm();
            form.HandleCreated += form_HandleCreated;
            list = new ObjectsList(this, pb, world, 3, 3, 223, 166, 
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

            showWar.Text = "war:999";
            showAlly.Text = "ally:-";
            showNeutral.Text = "neu:" + world.Characters.Count;
            showNpc.Text = "npc:" + world.Npcs.Count;
            showWar.Draw(e.Graphics);
            showAlly.Draw(e.Graphics);
            showNeutral.Draw(e.Graphics);
            showNpc.Draw(e.Graphics);
            /*e.Graphics.DrawString(, GUI.Font, GUI.WarBrush, showWar.Left + 12, showWar.Top - pb.Top);
            e.Graphics.DrawString("ally:-", GUI.Font, GUI.AllyBrush, showAlly.Left + 12, showAlly.Top - pb.Top);
            e.Graphics.DrawString("neu.:" + world.Characters.Count, GUI.Font, GUI.NeutralBrush, showNeutral.Left + 12, showNeutral.Top - pb.Top);
            
            e.Graphics.DrawString(, GUI.Font, GUI.NpcBrush, showNpc.Left + 12, showNpc.Top - pb.Top);*/
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
                if (redrawThread != null)
                    redrawThread.Abort();
                _disposed = true;
            }
        }

        #endregion
    }
}