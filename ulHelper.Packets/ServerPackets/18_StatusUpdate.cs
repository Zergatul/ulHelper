using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ulHelper.Packets
{
    /*
     18=StatusUpdate:
     * d(ObjectID)d(AttribCount:Loop.01.0002)d(AttrID:Get.FSup)d(AttrValue)
     */
    /// <summary>
    /// ID = 18
    /// </summary>
    public class StatusUpdate : ServerPacket
    {
        public int ObjectID { get; set; }
        public List<AttributeInfo> Attributes { get; set; }

        public StatusUpdate(ServerPacket pck)
            : base(pck)
        {
            this.ObjectID = ReadInt();
            this.Position += 8;
            int count = ReadInt();
            this.Attributes = new List<AttributeInfo>(count);
            for (int i = 0; i < count; i++)
                this.Attributes.Add(new AttributeInfo
                {
                    Attribute = (Attribute)ReadInt(),
                    Value = ReadInt()
                });
        }

        public class AttributeInfo
        {
            public Attribute Attribute { get; set; }
            public int Value { get; set; }
        }

        public enum Attribute
        {
            Level = 0x00000001,
            Exp = 0x00000002,
            STR = 0x00000003,
            DEX = 0x00000004,
            CON = 0x00000005,
            INT = 0x00000006,
            WIT = 0x00000007,
            MEN = 0x00000008,
            CurHP = 0x00000009,
            MaxHP = 0x0000000a,
            CurMP = 0x0000000b,
            MaxMP = 0x0000000c,
            SP = 0x0000000d,
            CurWeightLimit = 0x0000000e,
            MaxWeightLimit = 0x0000000f,
            PAtk = 0x00000011,
            PatkSpeed = 0x00000012,
            PDef = 0x00000013,
            Evasion = 0x00000014,
            Accuracy = 0x00000015,
            Critical = 0x00000016,
            MAtk = 0x00000017,
            CastSpeed = 0x00000018,
            MDef = 0x00000019,
            Flag = 0x0000001a,
            Carma = 0x0000001b,
            CurCP = 0x00000021,
            MaxCP = 0x00000022
        }
    }
}