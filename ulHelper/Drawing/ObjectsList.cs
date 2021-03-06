﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using ulHelper.L2Objects;
using ComponentFactory.Krypton.Toolkit;
using ulHelper.App.Tooltips;
using ulHelper.GameInfo;

namespace ulHelper.App.Drawing
{
    public class ObjectsList : IDisposable
    {
        static Color borderColor = Color.FromArgb(0x3C, 0x41, 0x51);
        static Pen borderPen = new Pen(borderColor);
        static Brush hoverBrush = new SolidBrush(Color.FromArgb(0xA5, 0x9B, 0x04));
        static int itemHeight = 13;
        static Bitmap unknownClass;

        public static void Load()
        {
            var buf = new Bitmap(@"Images\UnknownClass.png");
            unknownClass = new Bitmap(11, 11);
            using (var g = Graphics.FromImage(unknownClass))
                g.DrawImage(buf, new RectangleF(0, 0, 11, 11));
            buf.Dispose();
        }

        int x, y, width, height;
        ObjectsPanel panel;
        PictureBox pb;
        VScrollBar scroll;
        GameWorld world;
        MiniHPBar hpBar;
        Mini5HPBar hpBar5;
        Mini5MPBar mpBar5;
        Rectangle clip;
        int hoveredIndex;
        CheckTextButton showWar, showCharacters, showNpc, showDrop;
        List<L2Object> objList;
        CharacterToolTip characterToolTip;
        NpcToolTip npcToolTip;
        ulHelper.App.Tooltips.ToolTip currentToolTip;

        public event EventHandler<ObjectClickEventArgs> ObjectClick;

        public ObjectsList(ObjectsPanel panel, PictureBox pb, GameWorld world, int x, int y, int width, int height,
            CheckTextButton showWar, CheckTextButton showCharacters, CheckTextButton showNpc, CheckTextButton showDrop,
            CharacterToolTip characterToolTip, NpcToolTip npcToolTip)
        {
            this.panel = panel;
            this.pb = pb;
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
            this.world = world;
            this.world.L2LiveObjectUpdate += world_L2LiveObjectUpdate;
            this.world.AddCharacter += (s, e) => { world_ObjectsChanged(); };
            //this.world.AddDrop += (s, e) => { world_ObjectsChanged(); };
            this.world.AddNpc += (s, e) => { world_ObjectsChanged(); };
            this.world.DeleteCharacter += (s, e) => { world_ObjectsChanged(); };
            //this.world.DeleteDrop += (s, e) => { world_ObjectsChanged(); };
            this.world.DeleteNpc += (s, e) => { world_ObjectsChanged(); };
            this.hoveredIndex = -1;
            this.showWar = showWar;
            this.showCharacters = showCharacters;
            this.showNpc = showNpc;
            this.showDrop = showDrop;
            this.objList = new List<L2Object>();
            this.characterToolTip = characterToolTip;
            this.npcToolTip = npcToolTip;

            scroll = new VScrollBar();
            scroll.Height = height - 1;
            scroll.Left = pb.Left + x + width - scroll.Width;
            scroll.Top = pb.Top + y + 1;
            scroll.LargeChange = height / itemHeight;
            pb.FindForm().Controls.Add(scroll);
            scroll.BringToFront();
            scroll.ValueChanged += scroll_ValueChanged;

            this.clip = new Rectangle(x + 1, y + 1, width - 2 - scroll.Width, height - 2);

            pb.MouseMove += pb_MouseMove;
            pb.MouseLeave += pb_MouseLeave;
            pb.MouseClick += pb_MouseClick;
            pb.FindForm().MouseWheel += ObjectsList_MouseWheel;

            hpBar = new MiniHPBar(72);
            hpBar5 = new Mini5HPBar(72);
            mpBar5 = new Mini5MPBar(72);
        }

        void world_L2LiveObjectUpdate(object sender, L2Objects.Events.L2LiveObjectEventArgs e)
        {
            if (currentToolTip != null)
                if (currentToolTip == characterToolTip)
                {
                    if (e.LiveObject == characterToolTip.Character)
                        characterToolTip.Update();
                }
                else
                {
                    if (e.LiveObject == npcToolTip.Npc)
                        npcToolTip.Update();
                }
        }

        void world_ObjectsChanged()
        {
            if (currentToolTip != null)
            {
                bool change = false;
                if (currentToolTip == characterToolTip)
                {
                    if (0 <= hoveredIndex && hoveredIndex < objList.Count)
                        if (characterToolTip.Character != objList[hoveredIndex])
                            change = true;
                }
                else
                {
                    if (0 <= hoveredIndex && hoveredIndex < objList.Count)
                        if (npcToolTip.Npc != objList[hoveredIndex])
                            change = true;
                }
                if (change)
                {
                    currentToolTip.Hide();
                    if (objList[hoveredIndex] is L2Character)
                    {
                        characterToolTip.Character = objList[hoveredIndex] as L2Character;
                        currentToolTip = characterToolTip;
                    }
                    else
                    {
                        npcToolTip.Npc = objList[hoveredIndex] as L2Npc;
                        currentToolTip = npcToolTip;
                    }
                    currentToolTip.Update();
                }
            }
        }

        void pb_MouseClick(object sender, MouseEventArgs e)
        {
            if (hoveredIndex != -1)
                if (ObjectClick != null)
                    ObjectClick(this, new ObjectClickEventArgs(objList[hoveredIndex]));
        }

        void ObjectsList_MouseWheel(object sender, MouseEventArgs e)
        {
            if (pb.Bounds.Contains(e.Location))
            {
                int value = scroll.Value - e.Delta / 120;
                if (0 <= value && value <= scroll.Maximum - scroll.LargeChange + 1)
                {
                    scroll.Value = value;
                    pb_MouseMove(sender, new MouseEventArgs(e.Button, e.Clicks, e.X - pb.Left, e.Y - pb.Top, e.Delta));
                }
            }
        }

        void pb_MouseLeave(object sender, EventArgs e)
        {
            if (hoveredIndex != -1)
            {
                hoveredIndex = -1;
                panel.Update();
                if (currentToolTip != null)
                {
                    currentToolTip.Hide();
                    currentToolTip = null;
                }
            }
        }

        void pb_MouseMove(object sender, MouseEventArgs e)
        {
            int index;
            if (clip.Contains(e.Location))
            {
                index = (e.Y - clip.Top) / 13 + scroll.Value;
                if (index >= objList.Count)
                    index = -1;
            }
            else
                index = -1;
            if (index != hoveredIndex)
            {
                hoveredIndex = index;
                panel.Update();
                if (hoveredIndex != -1)
                {
                    var pred = currentToolTip;
                    if (objList[hoveredIndex] is L2Character)
                    {
                        characterToolTip.Character = objList[hoveredIndex] as L2Character;
                        currentToolTip = characterToolTip;
                    }
                    else
                    {
                        npcToolTip.Npc = objList[hoveredIndex] as L2Npc;
                        currentToolTip = npcToolTip;
                    }
                    if (pred != null)
                        if (pred != currentToolTip)
                            pred.Hide();
                    currentToolTip.Update();
                }
                else
                {
                    currentToolTip.Hide();
                    currentToolTip = null;
                }
            }
            if (currentToolTip != null)
            {
                var loc = pb.PointToScreen(e.Location);
                loc.Offset(16, 16);
                currentToolTip.Show(loc);
            }
        }

        void scroll_ValueChanged(object sender, EventArgs e)
        {
            panel.Update();
        }

        public void Draw(Graphics g)
        {
            g.DrawRectangle(borderPen, x, y, width, height);
            g.SetClip(clip);
            objList.Clear();
            if (showCharacters.Checked)
                lock (world.Characters)
                    objList.AddRange(world.Characters);
            if (showNpc.Checked)
                lock (world.Npcs)
                    objList.AddRange(world.Npcs);
            if (showDrop.Checked)
                lock (world.DropItems)
                    objList.AddRange(world.DropItems);

            if (objList.Count > 0)
            {
                if (scroll.Maximum != objList.Count - 1)
                    scroll.Maximum = objList.Count - 1;
                scroll.Enabled = scroll.LargeChange < scroll.Maximum;
                int maxVisibleIndex = Math.Min(scroll.Value + scroll.LargeChange, objList.Count);
                if (maxVisibleIndex < objList.Count)
                    if (4 + itemHeight * scroll.LargeChange < y + height - 2)
                        maxVisibleIndex++;
                for (int i = scroll.Value; i < maxVisibleIndex; i++)
                {
                    int itemY = 4 + itemHeight * (i - scroll.Value);
                    if (i == hoveredIndex)
                        g.FillRectangle(hoverBrush, 4, itemY, clip.Width, 12);
                    if (objList[i] is L2Character)
                        DrawL2Character(g, objList[i] as L2Character, 4, itemY, 206);
                    if (objList[i] is L2Npc)
                        DrawNpc(g, objList[i] as L2Npc, 4, itemY, 206);
                    if (objList[i] is L2DropItem)
                        DropDrop(g, objList[i] as L2DropItem, 4, itemY, 206);
                }
            }

            g.ResetClip();
        }

        void DrawL2Character(Graphics g, L2Character ch, int x, int y, int width)
        {
            if (Info.Classes.ContainsKey(ch.ClassID))
                g.DrawImage(Info.Classes[ch.ClassID].Icon, x, y);
            else
                g.DrawImage(unknownClass,  x, y);
            if (Settings.ObjectsList.ShowMP)
            {
                hpBar5.Draw(g, x + 12, y + 1, ch.CurHP, ch.MaxHP);
                mpBar5.Draw(g, x + 12, y + 6, ch.CurMP, ch.MaxMP);
            }
            else
                hpBar.Draw(g, x + 12, y + 1, ch.CurHP, ch.MaxHP);
            g.DrawString(ch.Name, GUI.Font, GUI.NeutralBrush, x + 84, y);
        }

        void DrawNpc(Graphics g, L2Npc npc, int x, int y, int width)
        {
            if (Settings.ObjectsList.ShowMP)
            {
                hpBar5.Draw(g, x + 12, y + 1, npc.CurHP, npc.MaxHP);
                mpBar5.Draw(g, x + 12, y + 6, npc.CurMP, npc.MaxMP);
            }
            else
                hpBar.Draw(g, x + 12, y + 1, npc.CurHP, npc.MaxHP);
            g.DrawString(npc.Name, GUI.Font, GUI.NpcBrush, x + 84, y);
        }

        void DropDrop(Graphics g, L2DropItem drop, int x, int y, int width)
        {
            g.DrawString(drop.Name + " (" + drop.Count + ")", GUI.Font, GUI.DropBrush, x + 10, y);
        }

        #region Dispose pattern

        private bool _disposed;

        public virtual void Dispose()
        {
            if (!_disposed)
            {
                panel.Dispose();
                _disposed = true;
            }            
        }

        #endregion
    }
}