using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ulHelper.Packets
{
    /*
    BE=PrivateStoreListBuy:
        d(PlayerID)q(Money)d(ItemsCount:Loop.01.0028)
        d(ObjectID)d(ItemID:Get.F0)d(Slot)q(Count)
        h(type2)h(custType1)h(0)d(BodyPart)h(enchantLvl)h(custType2)
        d(augment:Get.F1)d(mana)d(remainTime)
        h(AttackElem)h(AttackElemPower)
        h(DefFire)h(DefWater)h(DefWind)h(DefEarth)h(DefHoly)h(DefUnholy)
        h(enchEff1)h(enchEff2)h(enchEff3)
        d(objID)q(price)q(refPrice)q(StoreCnt)
    */
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
                item.ObjectID = ReadInt();
                item.ItemID = ReadInt();
                int hz1 = ReadInt();
                long hz2 = ReadLong();
                long hz3 = ReadLong();
                short xx1 = ReadShort();
                short xx2 = ReadShort();
                short xx3 = ReadShort();
                int hz4 = ReadInt();
                int hz5 = ReadInt();
                int hz6 = ReadInt();
                short hz7 = ReadShort();
                short hz8 = ReadShort();
                long hz9 = ReadLong();
                long hz10 = ReadLong();
                long hz11 = ReadLong();
                int hz12 = ReadInt();
                item.Price = ReadLong();
                int hz15 = ReadInt();
                int hz16 = ReadInt();
                item.Count = ReadLong();
                Items.Add(item);
            }
        }

        public class Item
        {
            public int ObjectID { get; set; }
            public int ItemID { get; set; }
            public long Price { get; set; }
            public long Count { get; set; }
        }
    }
}