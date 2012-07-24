using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ulHelper.Packets
{
    /// <summary>
    /// ID = 26
    /// </summary>
    public class AutoAttackStop : ServerPacket
    {
        public int ObjectID { get; set; }

        public AutoAttackStop(ServerPacket pck)
            : base(pck)
        {
            this.ObjectID = ReadInt();
        }
    }
}