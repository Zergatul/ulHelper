using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ulHelper.Packets
{
    /*
    85=MagicEffectIcons:
        h(ListCount:Loop.01.0003)d(skillID:Get.Skill)h(SkillLevel)d(Duration)
    */
    /// <summary>
    /// ID = 85
    /// </summary>
    public class BuffList : ServerPacket
    {
        public List<Buff> Buffs { get; set; }

        public BuffList(ServerPacket pck)
            : base(pck)
        {
            short count = ReadShort();
            Buffs = new List<Buff>(count);
            for (int i = 0; i < count; i++)
            {
                var buff = new Buff();
                buff.SkillID = ReadInt();
                buff.SkillLvl = ReadShort();
                buff.Duration = ReadInt();
                Buffs.Add(buff);
            }
        }

        public class Buff
        {
            public int SkillID { get; set; }
            public short SkillLvl { get; set; }
            public int Duration { get; set; }
        }
    }
}