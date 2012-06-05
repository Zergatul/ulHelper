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

        PlayerPanel playerPanel;
        TargetPanel targetPanel;
        Radar radar;
        ObjectsPanel objects;

        public AccountForm(AccountData accData)
            : base()
        {
            InitializeComponent();
            this.accData = accData;
            settings = new AccountSettingsForm(accData);
            PrepareControls();
        }

        void PrepareControls()
        {
            playerPanel = new PlayerPanel(kryptonPanel, accData.World);
            targetPanel = new TargetPanel(kryptonPanel, accData.World);
            radar = new Radar(kryptonPanel, accData.World);
            objects = new ObjectsPanel(kryptonPanel, accData.World);
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
        }

        private void button1_Click(object sender, EventArgs e)
        {
            settings.Visible = !settings.Visible;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var pck = new RequestUserCommand();
            pck.Command = 0;
            accData.SendPacket(pck);
        }
    }
}