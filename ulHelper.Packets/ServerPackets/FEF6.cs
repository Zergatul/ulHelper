using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ulHelper.Packets
{
    /// <summary>
    /// ID = FE F6
    /// </summary>
    public class FEF6 : ServerPacket
    {
        public List<SellItem> Items { get; set; }

        public FEF6(ServerPacket pck)
            : base(pck)
        {
            this.Position += 12;
            int count = ReadInt();
            this.Position += 2;
            this.Items = new List<SellItem>(count);
            for (int i = 0; i < count; i++)
            {
                var item = new SellItem();
                this.Items.Add(item);
                this.Position += 6;
                item.Price = ReadLong();
                this.Position += 3 * 4;
                item.SellerName = ReadString();
                this.Position += 4;
                item.ItemID = ReadInt();
                item.Count = ReadInt();
                this.Position += 10;
                item.Enchant = ReadInt();
                this.Position += 4;
                this.Position += 2;
                this.Position += 5 * 4;
                this.Position += 2;
            }
        }

        public class SellItem
        {
            public long Price { get; set; }
            public string SellerName { get; set; }
            public int ItemID { get; set; }
            public int Count { get; set; }
            public int Enchant { get; set; }
        }
    }
}