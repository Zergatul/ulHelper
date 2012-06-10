using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ulHelper.Packets;
using System.Windows.Forms;
using ulHelper.L2Objects;

namespace ulHelper.Plugins
{
    public abstract class BasePlugin
    {
        public abstract string Name { get; }
        public abstract string Info { get; }
        public abstract string Version { get; }

        public event EventHandler UserStopped;

        public abstract void Run();
        public abstract void Stop();

        public virtual void OnUserStopped()
        {
            var eh = UserStopped;
            if (eh != null)
                eh(this, EventArgs.Empty);
        }
    }

    public abstract class ParsedPacketsPlugin : BasePlugin
    {
        public event EventHandler<ClientPacketAddEventArgs> OnClientPacketAdd;

        public abstract void OnPacket(ServerPacket pck);

        public void SendPacket(ClientPacket pck)
        {
            if (OnClientPacketAdd != null)
                OnClientPacketAdd(this, new ClientPacketAddEventArgs(pck));
        }
    }

    public abstract class UnparsedPacketsPlugin : BasePlugin
    {
        public abstract void OnPacket(UnparsedPacket pck);
    }

    public abstract class L2ObjectsPlugin : BasePlugin
    {
        public GameWorld World { get; set; }
    }
}