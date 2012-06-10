using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ulHelper.L2Objects
{
    public class L2NonStaticObject : L2Object
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }

        public int IntX { get { return (int)(X + 0.5d); } }
        public int IntY { get { return (int)(Y + 0.5d); } }
        public int IntZ { get { return (int)(Z + 0.5d); } }
    }
}