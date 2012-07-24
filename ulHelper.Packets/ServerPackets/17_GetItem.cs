using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ulHelper.Packets
{
    /// <summary>
    /// ID = 17
    /// </summary>
    public class GetItem : ServerPacket
    {
        public int PlayerID { get; set; }
        public int ObjectID { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }

        public GetItem(ServerPacket pck)
            : base(pck)
        {
            this.PlayerID = ReadInt();
            this.ObjectID = ReadInt();
            this.X = ReadInt();
            this.Y = ReadInt();
            this.Z = ReadInt();
        }
    }
}