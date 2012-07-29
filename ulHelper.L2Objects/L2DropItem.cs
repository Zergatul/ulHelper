using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ulHelper.Packets;

namespace ulHelper.L2Objects
{
    public class L2DropItem : L2Object
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }
        public long Count { get; set; }
        public int ItemID { get; set; }

        public L2DropItem()
        {
        }

        public L2DropItem(SpawnItem pck)
        {
            this.ObjectID = pck.ObjectID;
            this.ItemID = pck.ItemID;
            this.Count = pck.Count;
            this.X = pck.X;
            this.Y = pck.Y;
            this.Z = pck.Z;
        }

        public L2DropItem(DropItem pck)
        {
            this.ObjectID = pck.ObjectID;
            this.ItemID = pck.ItemID;
            this.Count = pck.Count;
            this.X = pck.X;
            this.Y = pck.Y;
            this.Z = pck.Z;
        }
    }
}