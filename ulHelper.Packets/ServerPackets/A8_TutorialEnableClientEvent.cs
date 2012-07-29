using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ulHelper.Packets
{
    /// <summary>
    /// ID = A8
    /// </summary>
    public class TutorialEnableClientEvent : ServerPacket
    {
        public int Event { get; set; }

        public TutorialEnableClientEvent(ServerPacket pck)
            : base(pck)
        {
            this.Event = ReadInt();
        }
    }
}