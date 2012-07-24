using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ulHelper.Packets
{
    /// <summary>
    /// ID = 72
    /// </summary>
    public class MoveToPawn : ServerPacket
    {
        public int CharID { get; set; }
        public int TargetID { get; set; }
        public int Distance { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }
        public int TargetX { get; set; }
        public int TargetY { get; set; }
        public int TargetZ { get; set; }

        public MoveToPawn(ServerPacket pck)
            : base(pck)
        {
            this.CharID = ReadInt();
            this.TargetID = ReadInt();
            this.Distance = ReadInt();
            this.X = ReadInt();
            this.Y = ReadInt();
            this.Z = ReadInt();
            this.TargetX = ReadInt();
            this.TargetY = ReadInt();
            this.TargetZ = ReadInt();
        }
    }
}