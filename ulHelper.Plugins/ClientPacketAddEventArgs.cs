using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ulHelper.Packets;

namespace ulHelper.Plugins
{
    public class ClientPacketAddEventArgs : EventArgs
    {
        public ClientPacket Packet { get; set; }

        public ClientPacketAddEventArgs(ClientPacket packet)
        {
            this.Packet = packet;
        }
    }
}