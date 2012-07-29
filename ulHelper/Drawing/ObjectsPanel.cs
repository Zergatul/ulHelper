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
        CheckTextButton showWar, showCharacters, showNpc, showDrop;
        ObjectsList list;

        public event EventHandler<ObjectClickEventArgs> ObjectClick;

        public ObjectsPanel(Control parent, GameWorld world, CharacterToolTip characterToolTip, NpcToolTip npcToolTip)
        {
            this.world = world;
            this.world.AddCharacter += (s, e) => { this.Update(); };
            this.world.DeleteCharacter += (s, e) => { this.Update(); };
            this.world.AddNpc += (s, e) => { this.Update(); };
            this.world.DeleteNpc += (s, e) => { this.Update(); };
            this.world.AddDrop += (s, e) => { this.Update(); };
            this.world.DeleteDrop += (s, e) => { this.Update(); };
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
            showWar.Width = 48;
            showWar.FontBrush = GUI.WarBrush;
            showWar.CheckedChanged += showWar_CheckedChanged;
            showWar.PrepareBrushes();

            showCharacters = new CheckTextButton(pb);
            showCharacters.Text = "chs:-";
            showCharacters.Left = showWar.Left + showWar.Width + 4;
            showCharacters.Top = showWar.Top;
            showCharacters.Height = showWar.Height;
            showCharacters.Width = showWar.Width;
            showCharacters.Checked = true;
            showCharacters.FontBrush = GUI.NeutralBrush;
            showCharacters.CheckedChanged += showNeutral_CheckedChanged;
            showCharacters.PrepareBrushes();

            showNpc = new CheckTextButton(pb);
            showNpc.Text = "npc:-";
            showNpc.Left = showCharacters.Left + showCharacters.Width + 4;
            showNpc.Top = showWar.Top;
            showNpc.Height = showWar.Height;
            showNpc.Width = showWar.Width;
            showNpc.FontBrush = GUI.NpcBrush;
            showNpc.CheckedChanged += showNpc_CheckedChanged;
            showNpc.PrepareBrushes();

            showDrop = new CheckTextButton(pb);
            showDrop.Text = "drop:-";
            showDrop.Left = showNpc.Left + showWar.Width + 4;
            showDrop.Top = showWar.Top;
            showDrop.Height = showWar.Height;
            showDrop.Width = showWar.Width;
            showDrop.FontBrush = GUI.AllyBrush;
            showDrop.CheckedChanged += showAlly_CheckedChanged;
            showDrop.PrepareBrushes();

            form = parent.FindForm();
            form.HandleCreated += form_HandleCreated;
            list = new ObjectsList(this, pb, world, 3, 3, 223, 166,
                showWar, showCharacters, showNpc, showDrop,
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

            showWar.Text = "war:-";
            showCharacters.Text = "chs:" + world.Characters.Count;
            showNpc.Text = "npc:" + world.Npcs.Count;
            showDrop.Text = "drop:" + world.DropItems.Count;
            showWar.Draw(e.Graphics);
            showDrop.Draw(e.Graphics);
            showCharacters.Draw(e.Graphics);
            showNpc.Draw(e.Graphics);
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