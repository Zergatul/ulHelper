using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ulHelper.Packets
{
    /// <summary>
    /// ID = 05
    /// </summary>
    public class SpawnItem : ServerPacket
    {
        public int ObjectID { get; set; }
        public int ItemID { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }
        public int Stackable { get; set; }
        public long Count { get; set; }

        public SpawnItem(ServerPacket pck)
            : base(pck)
        {
            this.ObjectID = ReadInt();
            this.ItemID = ReadInt();
            this.X = ReadInt();
            this.Y = ReadInt();
            this.Z = ReadInt();
            this.Stackable = ReadInt();
            this.Count = ReadInt();
        }
    }
}