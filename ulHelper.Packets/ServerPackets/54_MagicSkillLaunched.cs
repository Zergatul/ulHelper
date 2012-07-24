using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ulHelper.Packets
{
    /// <summary>
    /// ID = 54
    /// </summary>
    public class MagicSkillLaunched : ServerPacket
    {
        public int ObjectID { get; set; }
        public int SkillID { get; set; }
        public int SkillLvl { get; set; }
        public List<int> HitTargets { get; set; }

        public MagicSkillLaunched(ServerPacket pck)
            : base(pck)
        {
            this.Position += 4;
            this.ObjectID = ReadInt();
            this.SkillID = ReadInt();
            this.SkillLvl = ReadInt();
            int count = ReadInt();
            HitTargets = new List<int>(count);
            for (int i = 0; i < count; i++)
                HitTargets.Add(ReadInt());
        }
    }
}