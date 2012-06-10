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
        public string PetName { get; set; }
        public string OwnerName { get; set; }
        public bool IsNameAbove { get; set; }

        public L2Npc()
        {
        }

        public L2Npc(NpcInfo pck)
        {
            Update(pck);
        }

        public void Update(NpcInfo pck)
        {
            this.X = pck.X;
            this.Y = pck.Y;
            this.Z = pck.Z;
            this.ObjectID = pck.ObjectID;
            this.NpcID = pck.NpcID;
            this.CurHP = pck.CurHP;
            this.MaxHP = pck.MaxHP;
            this.CurMP = pck.CurMP;
            this.MaxMP = pck.MaxMP;
            this.CastSpeed = pck.CastSpeed;
            this.AtkSpeed = pck.AtkSpeed;
            this.Speed = (int)Math.Round((pck.IsRun == 1 ? pck.RunSpeed : pck.WalkSpeed) * pck.MoveMult);
            this.IsNameAbove = pck.IsSummoned == 1;
            this.PetName = pck.PetName;
            this.OwnerName = pck.OwnerName;
        }
    }
}