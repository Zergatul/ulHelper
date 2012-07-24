using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ulHelper.Packets
{
    /// <summary>
    /// ID = 0x21
    /// </summary>
    public class InventoryUpdate : ServerPacket
    {
        public List<InventoryItem> Inventory { get; set; }

        public InventoryUpdate(ServerPacket pck)
            : base(pck)
        {
            short count = ReadShort();
            Inventory = new List<InventoryItem>(count);
            for (int i = 0; i < count; i++)
            {
                var item = new InventoryItem();
                this.Position += 2;
                item.ObjectID = ReadInt();
                item.ItemID = ReadInt();
                this.Position += 4;
                item.Count = ReadLong();
                this.Position += 6;
                this.Position += 4;
                this.Position += 4;
                this.Position += 3 * 4;
                this.Position += 11 * 2;
                Inventory.Add(item);
            }
        }

        public class InventoryItem
        {
            public int ObjectID { get; set; }
            public int ItemID { get; set; }
            public long Count { get; set; }
        }
    }
}