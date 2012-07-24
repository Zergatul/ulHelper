using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ulHelper.Packets
{
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
                skill.SkillLvl = ReadInt();
                skill.ReuseBase = ReadInt();
                skill.ReuseCurrent = ReadInt();
                Skills.Add(skill);
            }
        }

        public class Skill
        {
            public int SkillID {get;set;}
            public int SkillLvl { get; set; }
            public int ReuseBase { get; set; }
            public int ReuseCurrent { get; set; }
        }
    }
}