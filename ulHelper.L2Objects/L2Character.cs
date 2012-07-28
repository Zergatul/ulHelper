using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ulHelper.Packets;

namespace ulHelper.L2Objects
{
    public class L2Character : L2LiveObject
    {
        public int Relation { get; set; }
        public int ClassID { get; set; }
        public string ClassName { get; set; }
        public int MaxCP { get; set; }
        public int CurCP { get; set; }
        public int Karma { get; set; }
        public bool Noble { get; set; }
        public bool Hero { get; set; }
        public bool LevelStrict { get; set; }

        public L2Character()
        {
        }

        public L2Character(CharInfo pck)
        {
            Update(pck);
        }

        public L2Character(PartySmallWindowUpdate pck)
        {
            Update(pck);
        }

        public void Update(CharInfo pck)
        {
            this.X = pck.X;
            this.Y = pck.Y;
            this.Z = pck.Z;
            this.ObjectID = pck.ObjectID;
            this.Name = pck.VisibleName;
            this.Title = pck.VisibleTitle;
            this.ClassID = pck.ClassID;
            //if (!this.LevelStrict)
                ;//this.Level = Info

            this.CurHP = pck.CurHP;
            this.MaxHP = pck.MaxHP;
            this.CurMP = pck.CurMP;
            this.MaxMP = pck.MaxMP;
            this.Speed = (int)Math.Round((pck.IsRun == 1 ? pck.RunSpeed : pck.WalkSpeed) * pck.MoveMult);
            this.CastSpeed = pck.CastSpeed;
            this.AtkSpeed = pck.AtkSpeed;
            this.Karma = pck.Karma;
            this.Noble = pck.IsNoble == 1;
            this.Hero = pck.IsHero == 1;
            this.CurCP = pck.CurCP;
        }

        public void Update(PartySmallWindowUpdate pck)
        {
            this.ObjectID = pck.ObjectID;
            this.Name = pck.Name;
            this.ClassID = pck.ClassID;
            this.Level = pck.Level;
            this.CurHP = pck.CurHP;
            this.MaxHP = pck.MaxHP;
            this.CurMP = pck.CurMP;
            this.MaxMP = pck.MaxMP;
            this.CurCP = pck.CurCP;
            this.MaxCP = pck.MaxCP;
            this.LevelStrict = true;
        }
    }
}