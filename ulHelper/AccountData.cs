using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO;
using System.Windows.Forms;
using ulHelper.Packets;
using ulHelper.Plugins;
using ulHelper.App.Modules;
using ulHelper.L2Objects;

namespace ulHelper.App
{
    public class AccountData : IDisposable
    {
        public string Name;
        public bool Active;
        public bool Selected;
        internal GameWorld World;

        internal AccountForm Form;
        internal bool NeedTerminate;
        internal List<BasePlugin> LoadedPlugins;
        internal List<ClientPacket> SendBuffer;

        PacketsReceiveModule pckReceive;
        AccountLiveModule accLive;
        AppLiveModule appLive;
        PacketsSendModule pckSend;

        public AccountData(string name)
        {
            this.Name = name;
            this.World = new GameWorld();
            this.Form = new AccountForm(this);
            this.LoadedPlugins = new List<BasePlugin>();
            this.SendBuffer = new List<ClientPacket>();

            /*pckReceive = new PacketsReceiveModule(this);
            accLive = new AccountLiveModule(this);
            appLive = new AppLiveModule(this);
            pckSend = new PacketsSendModule(this);*/
        }

        public override string ToString()
        {
            return this.Name;
        }

        public void SendPacket(ClientPacket pck)
        {
            lock (SendBuffer)
                SendBuffer.Add(pck);
            pckSend.NewPacketInQueue();
        }

        internal void PacketReceive(ServerPacket pck)
        {
            pck = pck.Parse();
            World.Update(pck);
            foreach (var plugin in this.LoadedPlugins)
            {
                if (plugin is ulHelper.Plugins.ParsedPacketsPlugin)
                    ((ulHelper.Plugins.ParsedPacketsPlugin)plugin).OnPacket(pck);
                if (plugin is ulHelper.Plugins.UnparsedPacketsPlugin)
                    ((ulHelper.Plugins.UnparsedPacketsPlugin)plugin).OnPacket(new UnparsedPacket(pck));
            }
        }

        #region Dispose pattern

        private bool _disposed;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    NeedTerminate = true;
                    /*pckReceive.Terminate();
                    accLive.Terminate();
                    appLive.Terminate();
                    pckSend.Terminate();*/
                    this.Form.NeedTerminate = true;
                    if (this.Form.InvokeRequired)
                        this.Form.Invoke((ThreadStart)this.Form.Close);
                    else
                        this.Form.Close();
                }
                _disposed = true;
            }
        }

        #endregion
    }
}