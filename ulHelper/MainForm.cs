using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ComponentFactory.Krypton.Toolkit;
using ulHelper.Packets;
using System.Linq;
using System.IO;
using System.Threading;
using ulHelper.App.Modules;

namespace ulHelper.App
{
    public partial class MainForm : KryptonForm
    {
        public static MainForm Instance;

        internal List<AccountData> Accounts;
        internal bool NeedTerminate;
        internal AccountManagerModule AccManager;

        public MainForm()
        {
            InitializeComponent();
            Instance = this;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            var lf = new LoadingForm();
            lf.ShowDialog();
            Accounts = new List<AccountData>();
            AccManager = new AccountManagerModule(this);

            // this for test
            Accounts.Add(new AccountData("ololo"));
            RefreshAccounts();
        }        

        internal void RefreshAccounts()
        {
            accountsCLB.BeginUpdate();
            accountsCLB.Items.Clear();
            foreach (var acc in Accounts)
            {
                accountsCLB.Items.Add(acc);
                accountsCLB.SetSelected(accountsCLB.Items.Count - 1, acc.Selected);
            }
            accountsCLB.EndUpdate();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
        }

        private void accountsCLB_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            var acc = accountsCLB.Items[e.Index] as AccountData;
            if (e.NewValue == CheckState.Checked)
            {
                acc.Form.Show();
                acc.Selected = true;
            }
            else
            {
                acc.Form.Hide();
                acc.Selected = false;
            }
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            foreach (var acc in Accounts)
                acc.Dispose();
            this.NeedTerminate = true;
            AccManager.Terminate();
        }
    }
}