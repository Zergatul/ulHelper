using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ulHelper.Packets
{
    /*
    A1=PrivateStoreListSell:
        d(PlayerObjID)d(isPackageSale)q(Money)d(ItemsCount:Loop.01.0026)
        d(ObjectID)d(ItemID:Get.F0)d(Slot)q(Count)h(type2)h(custType1)h(0)
        d(BodyPart)h(enchantLvl)h(custType2)d(augment:Get.F1)d(mana)d(remainTime)
        h(AttackElem)h(AttackElemPower)
        h(DefFire)h(DefWater)h(DefWind)h(DefEarth)h(DefHoly)h(DefUnholy)
        h(enchEff1)h(enchEff2)h(enchEff3)q(price)q(refPrice)
    */
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
                item.ObjectID = ReadInt();
                item.ItemID = ReadInt();
                var hz0 = ReadInt();
                item.Count = ReadLong();
                var hz1 = ReadShort();
                var hz2 = ReadShort();
                var hz3 = ReadShort();
                var hz5 = ReadInt();
                item.PetLvl = ReadShort();
                var hz6 = ReadShort();
                var hz7 = ReadInt();
                var hz8 = ReadInt();
                var hz9 = ReadInt();
                var hz10 = ReadShort();
                var hz11 = ReadShort();
                var hz12 = ReadLong();
                var hz13 = ReadLong();
                var hz14 = ReadLong();
                item.Price = ReadLong();
                var hz15 = ReadLong();
            }
        }

        public class Item
        {
            public int ObjectID { get; set; }
            public int ItemID { get; set; }
            public long Count { get; set; }
            public short PetLvl { get; set; }
            public long Price { get; set; }
        }
    }
}