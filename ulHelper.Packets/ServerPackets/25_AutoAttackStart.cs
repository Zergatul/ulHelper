using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ulHelper.Packets
{
    /*
    25=AutoAttackStart:d(TargetID)
    */
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