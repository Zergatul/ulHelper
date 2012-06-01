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
        public int MaxCP { get; set; }
        public int CurCP { get; set; }

        public L2Character()
        {
        }

        public L2Character(CharInfo pck)
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
        }
    }
}