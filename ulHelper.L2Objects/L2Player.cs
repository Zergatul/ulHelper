using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ulHelper.Packets;

namespace ulHelper.L2Objects
{
    public class L2Player : L2Character
    {
        public long Exp { get; set; }

        public L2Player()
        {
            Name = "[unknown]";
            CurCP = CurHP = CurMP = MaxCP = MaxHP = MaxMP = 1;
            Level = 1;
        }

        public void Update(UserInfo pck)
        {
            this.X = pck.X;
            this.Y = pck.Y;
            this.Z = pck.Z;
            this.ObjectID = pck.ObjectID;
            this.Name = pck.Name;
            this.Exp = pck.Exp;
            this.CurHP = pck.CurHP;
            this.MaxHP = pck.MaxHP;
            this.CurMP = pck.CurMP;
            this.MaxMP = pck.MaxMP;
            this.CurCP = pck.CurCP;
            this.MaxCP = pck.MaxCP;
            this.Title = pck.Title;
            this.Level = pck.Level;
        }
    }
}