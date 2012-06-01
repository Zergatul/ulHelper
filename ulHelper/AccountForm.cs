using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ComponentFactory.Krypton.Toolkit;
using ulHelper.Packets;
using ulHelper.L2Objects;
using System.Linq;
using System.Threading;
using ulHelper.App.Drawing;

namespace ulHelper.App
{
    public partial class AccountForm : KryptonForm
    {
        AccountSettingsForm settings;
        public Boolean NeedTerminate;
        Thread myStatusThread, targetStatusThread, objectListThread;
        bool myStatusPaintFlag, mapPaintFlag, targetStatusPaintFlag;
        AccountData accData;
        PlayerPanel playerPanel;

        public AccountForm(AccountData accData)
            : base()
        {
            InitializeComponent();
            this.accData = accData;
            settings = new AccountSettingsForm(accData);

            myStatusThread = new Thread((ThreadStart)UpdateMyStatus);
            myStatusThread.Start();
            targetStatusThread = new Thread((ThreadStart)UpdateTarget);
            targetStatusThread.Start();
            objectListThread = new Thread((ThreadStart)UpdateObjectList);
            objectListThread.Start();

            PrepareControls();
        }

        void PrepareControls()
        {
            playerPanel = new PlayerPanel(kryptonPanel, accData.World);
        }

        void UpdateObjectList()
        {
            int delay = Properties.Settings.Default.ObjectsPanelRefreshTime;
            while (!NeedTerminate)
            {
                Thread.Sleep(1);
                if (!this.IsHandleCreated)
                    continue;
                if (this.InvokeRequired)
                    this.Invoke((SimpleCall)UpdateObjectListFunc);
                Thread.Sleep(delay);
            }
        }

        private void UpdateObjectListFunc()
        {
            objectsLB.BeginUpdate();
            /*for (int i = 0; i < objectsLB.Items.Count; i++)
            {
                var l2obj = (objectsLB.Items[i] as KryptonListItem).Tag as L2LiveObject;
                if (l2obj.Deleted || (!showPlayersCB.Checked && l2obj is L2Character) || (!showNpcsCB.Checked && l2obj is L2Npc))
                {
                    try
                    {
                        objectsLB.Items.RemoveAt(i);
                    }
                    catch
                    {
                    }
                    i--;
                }
            }
            if (showPlayersCB.Checked)
                lock (characters)
                {
                    foreach (KryptonListItem item in objectsLB.Items)
                        if (item.Tag is L2Character)
                            item.ShortText = GetCharacterString(item.Tag as L2Character);
                    foreach (var c in characters.Where(_c => _c.New))
                    {
                        c.New = false;
                        Image icon = null;
                        if (GameInfo.Classes.ContainsKey(c.ClassID))
                            icon = GameInfo.Classes[c.ClassID].Icon;
                        objectsLB.Items.Add(new KryptonListItem
                        {
                            Image = icon,
                            ShortText = GetCharacterString(c),
                            Tag = c
                        });
                    }
                }
            if (showNpcsCB.Checked)
                lock (npcs)
                {
                    foreach (KryptonListItem item in objectsLB.Items)
                        if (item.Tag is L2Npc)
                            item.ShortText = GetNpcString(item.Tag as L2Npc);
                    foreach (var n in npcs.Where(_n => _n.New))
                    {
                        n.New = false;
                        objectsLB.Items.Add(new KryptonListItem
                        {
                            ShortText = GetNpcString(n),
                            Tag = n
                        });
                    }
                }*/
            objectsLB.EndUpdate();
        }

        string GetNpcString(L2Npc n)
        {
            string hp = n.CurHP + "/" + n.MaxHP;
            string name = "unknown";
            if (GameInfo.Npcs.ContainsKey(n.NpcID))
                name = GameInfo.Npcs[n.NpcID];
            return "[" + hp + "] " + name;
        }

        string GetCharacterString(L2Character c)
        {
            string className;
            string hp = c.CurHP + "/" + c.MaxHP;
            if (GameInfo.Classes.ContainsKey(c.ClassID))
                className = GameInfo.Classes[c.ClassID].Name;
            else
                className = "ID: " + c.ClassID.ToString();
            return "[" + className + "][" + hp + "]" + c.Name;
        }

        void UpdateMyStatus()
        {
            int delay = Properties.Settings.Default.PlayerPanelRefreshTime;
            while (!NeedTerminate)
            {
                Thread.Sleep(1);
                if (!this.IsHandleCreated)
                    continue;
                myStatusPaintFlag = true;
                mapPaintFlag = true;
                //myStatusPB.Invalidate();
                mapPB.Invalidate();
                Thread.Sleep(delay);
            }
        }

        void UpdateTarget()
        {
            int delay = Properties.Settings.Default.TargetPanelRefreshTime;
            while (!NeedTerminate)
            {
                Thread.Sleep(1);
                if (!this.IsHandleCreated)
                    continue;
                targetStatusPaintFlag = true;
                targetPB.Invalidate();
                Thread.Sleep(delay);
            }
        }

        private void AccountForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!NeedTerminate)
            {
                Hide();
                e.Cancel = true;
                foreach (var acc in MainForm.Instance.Accounts)
                    if (acc.Form == this)
                        MainForm.Instance.accountsCLB.SetItemChecked(MainForm.Instance.accountsCLB.FindString(acc.Name), false);
            }
            else
            {
                myStatusThread.Join();
                targetStatusThread.Join();
                objectListThread.Join();
            }
        }

        private void settingsBtn_Click(object sender, EventArgs e)
        {
            settings.Visible = !settings.Visible;
        }

        private void AccountForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            
        }

        private void charPB_Paint(object sender, PaintEventArgs e)
        {
            /*if (!myStatusPaintFlag)
                return;

            if (this.Text != myChar.Name)
                this.Text = myChar.Name;

            var g = e.Graphics;
            g.FillRectangle(Brushes.White, new Rectangle(new Point(), myStatusPB.Size));
            g.DrawString("HP: " + myChar.CurHP + "/" + myChar.MaxHP, this.Font, Brushes.Black, 1, 1);
            g.DrawString("MP: " + myChar.CurMP + "/" + myChar.MaxMP, this.Font, Brushes.Black, 1, 14);
            g.DrawString("CP: " + myChar.CurCP + "/" + myChar.MaxCP, this.Font, Brushes.Black, 1, 27);
            g.DrawString("Title: " + myChar.Title, this.Font, Brushes.Black, 1, 40);*/
        }

        private void targetPB_Paint(object sender, PaintEventArgs e)
        {
            /*if (!targetStatusPaintFlag)
                return;
            var g = e.Graphics;
            g.FillRectangle(Brushes.White, new Rectangle(new Point(), targetPB.Size));
            if (myChar.Target != null)
            {
                var str = "Target: ";
                if (myChar.Target is L2Character)
                    str += (myChar.Target as L2Character).Name;
                else
                {
                    int npcID = (myChar.Target as L2Npc).NpcID;
                    if (GameInfo.Npcs.ContainsKey(npcID))
                        str += GameInfo.Npcs[npcID] + " [" + npcID + "]";
                    else
                        str += "unknown [" + npcID + "]";
                }
                g.DrawString(str, this.Font, Brushes.Black, 1, 1);
                str = "HP: " + myChar.Target.CurHP + "/" + myChar.Target.MaxHP;
                g.DrawString(str, this.Font, Brushes.Black, 1, 14);
                str = "Level: " + myChar.Target.Level;
                g.DrawString(str, this.Font, Brushes.Black, 1, 27);
            }*/
        }

        private void mapPB_Paint(object sender, PaintEventArgs e)
        {
            /*if (!mapPaintFlag)
                return;
            var g = e.Graphics;
            WorldMap.DrawAt(g, mapPB.Size, myChar.X, myChar.Y);
            g.FillEllipse(new SolidBrush(Color.Red), mapPB.Width / 2 - 2, mapPB.Height / 2 - 2, 5, 5);*/
        }

        private void showPlayersCB_CheckedChanged(object sender, EventArgs e)
        {
            /*lock (characters)
                foreach (var ch in characters)
                    ch.New = true;*/
        }

        private void showNpcsCB_CheckedChanged(object sender, EventArgs e)
        {
            /*lock (npcs)
                foreach (var n in npcs)
                    n.New = true;*/
        }

        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            /*var pck = new RequestAnswerJoinParty();
            pck.Response = 1;*/
            /*var pck = new RequestSocialAction();
            pck.Action = 3;*/
            /*var pck = new D0A0();
            pck.AuctionID = 0;*/
            /*var pck = new D09D();
            pck.ObjectID = inventory.First(ii => ii.ItemID == 17).ObjectID;
            pck.Name = "перец perez westnight";
            pck.Period = 3;
            pck.Price = 28282828;
            pck.Quantity = 1;
            accData.SendPacket(pck);*/
            var pck = new ReqBypassToServer();
            pck.Command = textBox1.Text;
            accData.SendPacket(pck);
        }

        private void objectsLB_SelectedIndexChanged(object sender, EventArgs e)
        {
            var obj = (L2LiveObject)((KryptonListItem)objectsLB.SelectedItem).Tag;
            var pck = new ulHelper.Packets.Action();
            pck.ObjectID = obj.ObjectID;
            accData.SendPacket(pck);
        }
    }
}