using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ulHelper.Packets
{
    /*
    11=ItemList:
     * h(ShowWindow)h(count:Loop.01.0024)d(ObjectID)d(ItemID:Get.F0)d(LocationSlot)q(Count)
     * h(ItemType2)h(CustomType1)h(isEquipped)
     * d(BodyPart)
     * h(EnchantLevel)h(CustType2)
     * d(AugmentID:Get.F1)d(Mana)d(remainTime)
     * h(AttackElem)h(AttackElemVal)h(DefAttrFire)h(DefAttrWater)h(DefAttrWind)
     * h(DefAttrEarth)h(DefAttrHoly)h(DefAttrUnholy)h(EnchEff1)h(enchEff2)
     * h(enchEff3)h(blockedItems:Loop.02.0001)c(blockMode)d(blockItem)
    */
    /// <summary>
    /// ID = 0x21
    /// </summary>
    public class ItemList : ServerPacket
    {
        public List<InventoryItem> Inventory { get; set; }

        public ItemList(ServerPacket pck)
            : base(pck)
        {
            this.Position += 2;
            short count = ReadShort();
            Inventory = new List<InventoryItem>(count);
            for (int i = 0; i < count; i++)
            {
                var item = new InventoryItem();
                item.ObjectID = ReadInt();
                item.ItemID = ReadInt();
                this.Position += 4;
                item.Count = ReadLong();
                this.Position += 6;
                this.Position += 4;
                this.Position += 4;
                this.Position += 4 * 4;
                this.Position += 11 * 2;
                int blockedItems = ReadShort();
                this.Position += 5 * blockedItems;
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