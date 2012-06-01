using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ulHelper.Packets
{
    /*
     * 22=TeleportToLocation:d(TargetID)d(X)d(Y)d(Z)d(?)d(heading)
    */
    /// <summary>
    /// ID = 22
    /// </summary>
    public class TeleportToLocation : ServerPacket
    {
        public int TargetID { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }

        public TeleportToLocation(ServerPacket pck)
            : base(pck)
        {
            TargetID = ReadInt();
        }
    }
}