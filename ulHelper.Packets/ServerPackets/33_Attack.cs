using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ulHelper.Packets
{
    /// <summary>
    /// ID = 33
    /// </summary>
    public class Attack : ServerPacket
    {
        public int AttackerObjectID { get; set; }
        public int TargetObjectID { get; set; }
        public byte Flags { get; set; }
        public int Damage { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }
        public List<AttackInfo> Hits { get; set; }
        public int TargetX { get; set; }
        public int TargetY { get; set; }
        public int TargetZ { get; set; }

        public Attack(ServerPacket pck)
            : base(pck)
        {
            this.AttackerObjectID = ReadInt();
            this.TargetObjectID = ReadInt();
            this.Flags = ReadByte();
            this.Damage = ReadInt();
            this.Position += 8;
            this.X = ReadInt();
            this.Y = ReadInt();
            this.Z = ReadInt();
            short count = ReadShort();
            this.Hits = new List<AttackInfo>(count);
            for (int i = 0; i < count; i++)
            {
                var atkInfo = new AttackInfo();
                atkInfo.TargetID = ReadInt();
                atkInfo.Damage = ReadInt();
                this.Position += 8;
                this.Hits.Add(atkInfo);
            }
            this.TargetX = ReadInt();
            this.TargetY = ReadInt();
            this.TargetZ = ReadInt();
        }

        public class AttackInfo
        {
            public int TargetID { get; set; }
            public int Damage { get; set; }
        }
    }
}