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
using ulHelper.App.Drawing;

namespace ulHelper.App
{
    public partial class MainForm : BaseForm
    {
        public static MainForm Instance;
        public static bool DebugDraw = false;
        static Random rnd = new Random();

        public static volatile bool NeedTerminate;

        AccountList accList;

        public MainForm() : 
            base()
        {
            InitializeComponent();
            Instance = this;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            var lf = new LoadingForm();
            lf.ShowDialog();
            AccountManagerModule.NewAccount += AccountManagerModuler_NewAccount;

            accList = new AccountList(this, 5, 38, this.Width - 9, this.Height - 43);

            if (DebugDraw)
            {
                Accounts.List.Add(new AccountData("ololo"));
                Accounts.List.First().World.Npcs.Add(new L2Objects.L2Npc
                {
                    CurHP = 12344,
                    MaxHP = 19222,
                    Level = 99,
                    Name = "Волякасик",
                    NpcID = 93120
                });
                Accounts.List.First().World.User.Target = Accounts.List.First().World.Npcs.First();
                for (int i = 0; i < 20; i++)
                    Accounts.List.First().World.Characters.Add(new L2Objects.L2Character
                    {
                        X = (int)Math.Round(3000 * Math.Sin(2 * i * Math.PI / 20)),
                        Y = (int)Math.Round(3000 * Math.Cos(2 * i * Math.PI / 20)),
                        Name = "kokoko" + i,
                        CurHP = rnd.Next(1, 20) * 100,
                        MaxHP = 2000,
                        ClassID = 140 + i
                    });
                Accounts.List.First().World.___OnAddCharacter();
                RefreshAccounts();
            }
        }

        void AccountManagerModuler_NewAccount(object sender, EventArgs e)
        {
            this.InvokeIfNeeded(RefreshAccounts);
            //Accounts.List.First().Form.Show();
        }        

        internal void RefreshAccounts()
        {
            /*accountsCLB.BeginUpdate();
            accountsCLB.Items.Clear();
            foreach (var acc in Accounts.List)
            {
                accountsCLB.Items.Add(acc);
                accountsCLB.SetSelected(accountsCLB.Items.Count - 1, acc.Selected);
            }
            accountsCLB.EndUpdate();*/
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
        }

        private void accountsCLB_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            /*var acc = accountsCLB.Items[e.Index] as AccountData;
            if (e.NewValue == CheckState.Checked)
            {
                acc.Form.Show();
                acc.Selected = true;
            }
            else
            {
                acc.Form.Hide();
                acc.Selected = false;
            }*/
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            foreach (var acc in Accounts.List)
                acc.Dispose();
            NeedTerminate = true;
            AccountManagerModule.Terminate();
        }
    }
}