using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ulHelper.Packets
{
    /// <summary>
    /// ID = 9F
    /// </summary>
    public class StaticObject : ServerPacket
    {
        public int StaticObjectID { get; set; }
        public int ObjectID { get; set; }
        public int Type { get; set; }
        public int IsTargetable { get; set; }
        public int MeshIndex { get; set; }
        public int IsClosed { get; set; }
        public int IsEnemy { get; set; }
        public int CurHP { get; set; }
        public int MaxHP { get; set; }
        public int ShowHP { get; set; }
        public int DamageGrade { get; set; }

        public StaticObject(ServerPacket pck)
            : base(pck)
        {
            this.StaticObjectID = ReadInt();
            this.ObjectID = ReadInt();
            this.Type = ReadInt();
            this.IsTargetable = ReadInt();
            this.MeshIndex = ReadInt();
            this.IsClosed = ReadInt();
            this.IsEnemy = ReadInt();
            this.CurHP = ReadInt();
            this.MaxHP = ReadInt();
            this.ShowHP = ReadInt();
            this.DamageGrade = ReadInt();
        }
    }
}