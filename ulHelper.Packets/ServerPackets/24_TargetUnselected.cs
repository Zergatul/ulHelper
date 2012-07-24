using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ulHelper.Packets
{
    /// <summary>
    /// ID = 24
    /// </summary>
    public class TargetUnselected : ServerPacket
    {
        public int TargetID { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }

        public TargetUnselected(ServerPacket pck)
            : base(pck)
        {
            this.TargetID = ReadInt();
            this.X = ReadInt();
            this.Y = ReadInt();
            this.Z = ReadInt();
        }
    }
}