using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ulHelper.Packets
{
    /// <summary>
    /// ID = 16
    /// </summary>
    public class DropItem : ServerPacket
    {
        public int PlayerID { get; set; }
        public int ObjectID { get; set; }
        public int ItemID { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }
        public bool Stackable { get; set; }
        public long Count { get; set; }

        public DropItem(ServerPacket pck)
            : base(pck)
        {
            this.PlayerID = ReadInt();
            this.ObjectID = ReadInt();
            this.ItemID = ReadInt();
            this.X = ReadInt();
            this.Y = ReadInt();
            this.Z = ReadInt();
            this.Stackable = ReadInt() == 1;
            this.Count = ReadLong();
        }
    }
}