using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ulHelper.Packets
{
    /// <summary>
    /// ID = 48
    /// </summary>
    public class MagicSkillUse : ServerPacket
    {
        public int ObjectID { get; set; }
        public int TargetID { get; set; }
        public int SkillID { get; set; }
        public int SkillLevel { get; set; }
        public int CastTimeMs { get; set; }
        public int ReuseTimeMs { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }
        public int TargetX { get; set; }
        public int TargetY { get; set; }
        public int TargetZ { get; set; }

        public MagicSkillUse(ServerPacket pck)
            : base(pck)
        {
            this.Position += 4;
            this.ObjectID = ReadInt();
            this.TargetID = ReadInt();
            this.Position++;
            this.SkillID = ReadInt();
            this.SkillLevel = ReadInt();
            this.CastTimeMs = ReadInt();
            this.Position += 4;
            this.ReuseTimeMs = ReadInt();
            this.X = ReadInt();
            this.Y = ReadInt();
            this.Z = ReadInt();
            this.Position += 4;
            this.TargetX = ReadInt();
            this.TargetY = ReadInt();
            this.TargetZ = ReadInt();
        }
    }
}