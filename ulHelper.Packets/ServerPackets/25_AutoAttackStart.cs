using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ulHelper.Packets
{
    /// <summary>
    /// ID = 25
    /// </summary>
    public class AutoAttackStart : ServerPacket
    {
        public int ObjectID { get; set; }

        public AutoAttackStart(ServerPacket pck)
            : base(pck)
        {
            this.ObjectID = ReadInt();
        }
    }
}