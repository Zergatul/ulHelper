using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ulHelper.Packets
{
    /*
    26=AutoAttackStop:d(TargetID)
    */
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