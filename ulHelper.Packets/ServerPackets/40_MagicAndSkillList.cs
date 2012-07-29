using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ulHelper.Packets
{
    /// <summary>
    /// ID = 40
    /// </summary>
    public class MagicAndSkillList : ServerPacket
    {
        public int ObjectID { get; set; }

        public MagicAndSkillList(ServerPacket pck)
            : base(pck)
        {
            this.ObjectID = ReadInt();
            int hz1 = ReadInt();
            int hz2 = ReadInt();
        }
    }
}