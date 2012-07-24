using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ulHelper.Packets
{
    public abstract class ItemInfo
    {
        public int ObjectID { get; set; }
        public int ItemID { get; set; }
        public int EquipSlot { get; set; }
        public long Count { get; set; }
        public short Type2 { get; set; }
        public short CustomType1 { get; set; }
        public short IsEquipped { get; set; }
        public int BodyPart { get; set; }
        public short Enchant { get; set; }
        public short CustomType2 { get; set; }
        public int AugmentID { get; set; }
        public int ShadowTime { get; set; }
        public int TempTime { get; set; }
        public short AttackAttr { get; set; }
        public short AttackAttrValue { get; set; }
        public short DefFire { get; set; }
        public short DefWater { get; set; }
        public short DefWind { get; set; }
        public short DefEarth { get; set; }
        public short DefHoly { get; set; }
        public short DefDark { get; set; }
        public short EnchantOptions1 { get; set; }
        public short EnchantOptions2 { get; set; }
        public short EnchantOptions3 { get; set; }
    }
}