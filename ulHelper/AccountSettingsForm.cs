using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ComponentFactory.Krypton.Toolkit;
using ulHelper.Plugins;

namespace ulHelper.App
{
    public partial class AccountSettingsForm : KryptonForm
    {
        private AccountData AccData;

        public AccountSettingsForm(AccountData accData)
            : base()
        {
            InitializeComponent();
            this.AccData = accData;
        }

        private void AccountSettingsForm_Load(object sender, EventArgs e)
        {
            foreach (var plugin in AppPlugins.Plugins)
                pluginsCLB.Items.Add(plugin);
        }

        private void pluginsCLB_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (e.NewValue == CheckState.Checked)
            {
                var plugin = (pluginsCLB.Items[e.Index] as PluginInfo).CreateInstance(true);
                if (plugin is ParsedPacketsPlugin)
                    (plugin as ParsedPacketsPlugin).OnClientPacketAdd += OnClientPacketAdd;
                plugin.Run();
                AccData.LoadedPlugins.Add(plugin);
            }
            if (e.NewValue == CheckState.Unchecked)
            {
                var pluginInfo = pluginsCLB.Items[e.Index] as PluginInfo;
                Plugins.BasePlugin selectedPlugin = null;
                foreach (var plugin in AccData.LoadedPlugins)
                    if (plugin.GetType().FullName == pluginInfo.Type.FullName)
                    {
                        selectedPlugin = plugin;
                        break;
                    }
                selectedPlugin.Stop();
                AccData.LoadedPlugins.Remove(selectedPlugin);
            }
        }

        void OnClientPacketAdd(object sender, ClientPacketAddEventArgs e)
        {
            var plugin = sender as ParsedPacketsPlugin;
            AccData.SendPacket(e.Packet);
        }

        private void AccountSettingsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }
    }
}