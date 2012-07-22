using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ulHelper.Packets
{
    /*
    52=PartySmallWindowUpdate:d(MemberObjId)s(MemberName)
        d(CurrentCP)d(MaxCP)d(CurrentHP)d(MaxHP)d(CurrentMP)d(MaxMP)
        d(Level)d(ClassID:Get.ClassID)
    */
    /// <summary>
    /// ID = 52
    /// </summary>
    public class PartySmallWindowUpdate : ServerPacket
    {
        public int ObjectID { get; set; }
        public string Name { get; set; }
        public int CurCP { get; set; }
        public int MaxCP { get; set; }
        public int CurHP { get; set; }
        public int MaxHP { get; set; }
        public int CurMP { get; set; }
        public int MaxMP { get; set; }
        public int Level { get; set; }
        public int ClassID { get; set; }

        public PartySmallWindowUpdate(ServerPacket pck)
            : base(pck)
        {
            this.ObjectID = ReadInt();
            this.Name = ReadString();
            this.CurCP = ReadInt();
            this.MaxCP = ReadInt();
            this.CurHP = ReadInt();
            this.MaxHP = ReadInt();
            this.CurMP = ReadInt();
            this.MaxMP = ReadInt();
            this.Level = ReadInt();
            this.ClassID = ReadInt();
        }
    }
}