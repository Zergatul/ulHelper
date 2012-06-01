using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ulHelper.Packets
{
    /*
    23=TargetSelected:
     * d(ObjectID)d(TargetID)d(X)d(Y)d(Z)d(0)
    */
    /// <summary>
    /// ID = 23
    /// </summary>
    public class TargetSelected : ServerPacket
    {
        public int ObjectID { get; set; }
        public int TargetID { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }

        public TargetSelected(ServerPacket pck)
            : base(pck)
        {
            this.ObjectID = ReadInt();
            this.TargetID = ReadInt();
            this.X = ReadInt();
            this.Y = ReadInt();
            this.Z = ReadInt();
        }
    }
}