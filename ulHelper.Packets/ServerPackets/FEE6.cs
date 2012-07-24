using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ulHelper.Packets
{
    /// <summary>
    /// ID = FE E6
    /// </summary>
    public class FEE6 : ServerPacket
    {
        public int ObjectID { get; set; }
        public List<Buff> Buffs { get; set; }

        public FEE6(ServerPacket pck)
            : base(pck)
        {
            this.ObjectID = ReadInt();
            short count = ReadShort();
            Buffs = new List<Buff>(count);
            for (int i = 0; i < count; i++)
            {
                var buff = new Buff();
                buff.SkillID = ReadInt();
                buff.SkillLvl = ReadShort();
                var hz = ReadInt();
                buff.RemainSec = ReadInt();
                buff.BufferObjectID = ReadInt();
                Buffs.Add(buff);
            }
            var hz2 = ReadShort();
        }

        public class Buff
        {
            public int SkillID { get; set; }
            public short SkillLvl { get; set; }
            public int RemainSec { get; set; }
            public int BufferObjectID { get; set; }
        }
    }
}