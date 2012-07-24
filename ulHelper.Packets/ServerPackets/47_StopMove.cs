using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ulHelper.Packets
{
    /// <summary>
    /// ID = 47
    /// </summary>
    public class StopMove : ServerPacket
    {
        public int ObjectID { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }
        public int Heading { get; set; }

        public StopMove(ServerPacket pck)
            : base(pck)
        {
            this.ObjectID = ReadInt();
            this.X = ReadInt();
            this.Y = ReadInt();
            this.Z = ReadInt();
            this.Heading = ReadInt();
        }
    }
}