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
        AccountData accData;

        UserPanel playerPanel;
        TargetPanel targetPanel;
        Radar radar;
        ObjectsPanel objectsPanel;

        Tooltips.CharacterToolTip characterToolTip;
        Tooltips.NpcToolTip npcToolTip;

        public AccountForm(AccountData accData)
            : base()
        {
            InitializeComponent();
            this.accData = accData;
            settings = new AccountSettingsForm(accData);
            accData.World.PlayerUpdate += World_PlayerUpdate;
            PrepareControls();
        }

        void World_PlayerUpdate(object sender, EventArgs e)
        {
            this.InvokeIfNeeded(() =>
                {
                    if (this.Name != ((GameWorld)sender).User.Name)
                        this.Name = ((GameWorld)sender).User.Name;
                });
        }

        void PrepareControls()
        {
            characterToolTip = new Tooltips.CharacterToolTip();
            npcToolTip = new Tooltips.NpcToolTip();
            playerPanel = new UserPanel(kryptonPanel, accData.World);
            targetPanel = new TargetPanel(kryptonPanel, accData.World, characterToolTip, npcToolTip);
            targetPanel.SettingsClick += targetPanel_SettingsClick;
            radar = new Radar(kryptonPanel, accData.World);
            objectsPanel = new ObjectsPanel(kryptonPanel, accData.World, characterToolTip, npcToolTip);
            objectsPanel.ObjectClick += objects_ObjectClick;
        }

        void objects_ObjectClick(object sender, ObjectClickEventArgs e)
        {
            var pck = new ulHelper.Packets.Action();
            pck.ActionID = 0;
            pck.ObjectID = e.Object.ObjectID;
            accData.SendPacket(pck);
        }

        void targetPanel_SettingsClick(object sender, EventArgs e)
        {
            settings.Visible = !settings.Visible;
            /*var pck = new ReqBypassToServer();
            pck.Command = "menu_select?ask=-303&reply=810";
            accData.SendPacket(pck);*/
        }
        
        private void AccountForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!NeedTerminate)
            {
                Hide();
                e.Cancel = true;
                foreach (var acc in Accounts.List)
                    if (acc.Form == this)
                        MainForm.Instance.accountsCLB.SetItemChecked(MainForm.Instance.accountsCLB.FindString(acc.Name), false);
            }
        }

        public void DisposeResources()
        {
            playerPanel.Dispose();
            targetPanel.Dispose();
            radar.Dispose();
            objectsPanel.Dispose();
            characterToolTip.Dispose();
            npcToolTip.Dispose();
        }
    }
}