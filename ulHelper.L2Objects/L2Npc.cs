using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ulHelper.Packets;

namespace ulHelper.L2Objects
{
    public class L2Npc : L2LiveObject
    {
        public int NpcID { get; set; }

        public L2Npc(NpcInfo pck)
        {
            Update(pck);
        }

        public void Update(NpcInfo pck)
        {
            this.X = pck.X;
            this.Y = pck.Y;
            this.Z = pck.Z;
            this.Name = pck.Name;
            this.Title = pck.Title;
            this.ObjectID = pck.ObjectID;
            this.NpcID = pck.NpcID;
            this.CurHP = pck.CurHP;
            this.MaxHP = pck.MaxHP;
            this.CurMP = pck.CurMP;
            this.MaxMP = pck.MaxMP;
        }
    }
}