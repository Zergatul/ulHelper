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
    public class TargetPanel
    {
        PictureBox pb;
        Form form;
        bool needRedraw;
        Thread redrawThread;
        GameWorld world;

        HPBar hpNpc;
        HPBar hpChar;
        LevelBar lvl;

        public TargetPanel(Control parent, GameWorld world)
        {
            this.world = world;
            this.world.PlayerTargetUpdate += (s, e) => { this.Update(); };

            pb = new PictureBox();
            pb.Width = 230;
            pb.Height = 42;
            pb.Top = 60;
            pb.Left = 0;
            pb.Paint += pb_Paint;
            parent.Controls.Add(pb);

            form = parent.FindForm();
            form.HandleCreated += form_HandleCreated;

            hpNpc = new HPBar(pb.Width - 8 - 23);
            hpChar = new HPBar(pb.Width - 8);
            lvl = new LevelBar();
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
            lock (world.Player)
                if (world.Player.Target != null)
                    lock (world.Player.Target)
                    {
                        if (world.Player.Target is L2Character)
                        {
                            var ch = world.Player.Target as L2Character;
                            hpChar.Draw(e.Graphics, 3, 3, ch.CurHP, ch.MaxHP);
                            e.Graphics.DrawString(ch.Name, GUI.Font, GUI.NeutralBrush, 3, 15);
                        }
                        if (world.Player.Target is L2Npc)
                        {
                            var npc = world.Player.Target as L2Npc;
                            hpNpc.Draw(e.Graphics, 27, 3, npc.CurHP, npc.MaxHP);
                            lvl.Draw(e.Graphics, 3, 3, npc.Level);
                            string name = "[unknown]";
                            if (GameInfo.Npcs.ContainsKey(npc.NpcID))
                                name = GameInfo.Npcs[npc.NpcID];
                            e.Graphics.DrawString(name, GUI.Font, GUI.NpcBrush, 27, 15);
                            e.Graphics.DrawString("ID: " + npc.NpcID, GUI.Font, Brushes.Black, 4, 27);
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
            while (true)
            {
                needRedraw = false;
                if (form.IsHandleCreated)
                    pb.Invoke((ThreadStart)(() => { pb.Invalidate(); }));
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
    }
}