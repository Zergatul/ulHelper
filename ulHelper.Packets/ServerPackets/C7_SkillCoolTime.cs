using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ulHelper.Packets
{
    /*
    C7=SkillCoolTime:d(listSize:Loop.01.0004)d(skillID:Get.Skill)d(skillLvl)d(reuseDelay)d(timeRemain)
    */
    /// <summary>
    /// ID = C7
    /// </summary>
    public class SkillCoolTime : ServerPacket
    {
        public List<Skill> Skills { get; set; }

        public SkillCoolTime(ServerPacket pck)
            : base(pck)
        {
            int count = ReadInt();
            Skills = new List<Skill>(count);
            for (int i = 0; i < count; i++)
            {
                var skill = new Skill();
                skill.SkillID = ReadInt();
                int hz0 = ReadInt();
                int hz = ReadInt();
                short hz2 = ReadShort();
                short hz3 = ReadShort();
                Skills.Add(skill);
            }
        }

        public class Skill
        {
            public int SkillID {get;set;}
        }
    }
}