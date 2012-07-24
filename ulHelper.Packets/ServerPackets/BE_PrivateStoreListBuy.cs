using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ulHelper.Packets
{
    /// <summary>
    /// ID = BE
    /// </summary>
    public class PrivateStoreListBuy : ServerPacket
    {
        public int ObjectID { get; set; }
        public long UserAdenaCount { get; set; }
        public int UserSlotCount { get; set; }
        public List<Item> Items { get; set; }

        public PrivateStoreListBuy(ServerPacket pck)
            : base(pck)
        {
            this.ObjectID = ReadInt();
            this.UserAdenaCount = ReadLong();
            this.UserSlotCount = ReadInt();
            int count = ReadInt();
            Items = new List<Item>(count);
            for (int i = 0; i < count; i++)
            {
                var item = new Item();
                ReadItemInfo(item);
                this.Position += 4;
                item.Price = ReadLong();
                item.StorePrice = ReadLong();
                item.Count = ReadLong();
                Items.Add(item);
            }
        }

        public class Item : ItemInfo
        {
            public long Price { get; set; }
            public long StorePrice { get; set; }
            public long Count { get; set; }
        }
    }
}