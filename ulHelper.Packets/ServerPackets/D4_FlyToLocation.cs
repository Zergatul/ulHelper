using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ulHelper.Packets
{
    /// <summary>
    /// ID = D4
    /// </summary>
    public class FlyToLocation : ServerPacket
    {
        public int ObjectID { get; set; }
        public int ToX { get; set; }
        public int ToY { get; set; }
        public int ToZ { get; set; }
        public int OriX { get; set; }
        public int OriY { get; set; }
        public int OriZ { get; set; }
        public int FlyType { get; set; }

        public FlyToLocation(ServerPacket pck)
            : base(pck)
        {
            this.ObjectID = ReadInt();
            this.ToX = ReadInt();
            this.ToY = ReadInt();
            this.ToZ = ReadInt();
            this.OriX = ReadInt();
            this.OriY = ReadInt();
            this.OriZ = ReadInt();
            this.FlyType = ReadInt();
        }
    }
}