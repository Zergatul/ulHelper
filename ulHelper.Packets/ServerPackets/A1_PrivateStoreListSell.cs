using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ulHelper.Packets
{
    /// <summary>
    /// ID = A1
    /// </summary>
    public class PrivateStoreListSell : ServerPacket
    {
        public int ObjectID { get; set; }
        public bool Package { get; set; }
        public long UserAdenaCount { get; set; }
        public int UserSlotCount { get; set; }
        public List<Item> Items { get; set; }

        public PrivateStoreListSell(ServerPacket pck)
            : base(pck)
        {
            this.ObjectID = ReadInt();
            this.Package = ReadInt() == 1;
            this.UserAdenaCount = ReadLong();
            this.UserSlotCount = ReadInt();
            int count = ReadInt();
            Items = new List<Item>(count);
            for (int i = 0; i < count; i++)
            {
                var item = new Item();
                ReadItemInfo(item);
                item.Price = ReadLong();
                item.StorePrice = ReadLong();
                Items.Add(item);
            }
        }

        public class Item : ItemInfo
        {
            public long Price { get; set; }
            public long StorePrice { get; set; }
        }
    }
}