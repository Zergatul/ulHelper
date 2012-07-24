using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ulHelper.Packets
{
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
                info.Passive = ReadInt();
                info.Level = ReadInt();
                info.SkillID = ReadInt();
                this.Position += 4;
                info.IsDisabled = ReadByte();
                info.CanEnchant = ReadByte();
                this.Skills.Add(info);
            }
        }

        public class SkillInfo
        {
            public int Passive { get; set; }
            public int Level { get; set; }
            public int SkillID { get; set; }
            public int IsDisabled { get; set; }
            public int CanEnchant { get; set; }
        }
    }
}