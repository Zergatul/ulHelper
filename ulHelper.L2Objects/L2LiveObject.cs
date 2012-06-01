using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ulHelper.L2Objects
{
    public class L2LiveObject : L2NonStaticObject
    {
        public bool Dead { get; set; }
        public bool Running { get; set; }
        public int CurHP { get; set; }
        public int MaxHP { get; set; }
        public int CurMP { get; set; }
        public int MaxMP { get; set; }
        public int AbnormalEffect { get; set; }
        public string Title { get; set; }
        public float Speed { get; set; }
        public int Level { get; set; }
        public L2LiveObject Target { get; set; }
        public bool Deleted { get; set; }
        public bool New { get; set; }

        public L2LiveObject()
        {
            this.New = true;
        }
    }
}