using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ulHelper.Packets
{
    /*
    5F=SkillList:d(ListCount:Loop.01.0005)d(isPassive)d(Level)d(SkillID:Get.Skill)c(isDisabled)c(enchanted)
    */
    /// <summary>
    /// ID = 5F
    /// </summary>
    public class SkillList : ServerPacket
    {
        public List<SkillInfo> Skills { get; set; }

        public SkillList(ServerPacket pck)
            : base(pck)
        {
            int count = ReadInt();
            this.Skills = new List<SkillInfo>(count);
            for (int i = 0; i < count; i++)
            {
                var info = new SkillInfo();
                info.Passive = ReadInt() == 1;
                info.Level = ReadInt();
                info.SkillID = ReadInt();
                this.Position += 4;
                info.hzDisabled = ReadByte() == 1;
                info.hzEnchanted = ReadByte() == 1;
                this.Skills.Add(info);
            }
        }

        public class SkillInfo
        {
            public bool Passive { get; set; }
            public int Level { get; set; }
            public int SkillID { get; set; }
            public bool hzDisabled { get; set; }
            public bool hzEnchanted { get; set; }
        }
    }
}