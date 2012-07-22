using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ulHelper.Packets
{
    /*
    79=ValidateLocation:d(CharID)d(X)d(Y)d(Z)d(Heading)
    */
    /// <summary>
    /// ID = 79
    /// </summary>
    public class ValidateLocation : ServerPacket
    {
        public int ObjectID { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }
        public int Heading { get; set; }

        public ValidateLocation(ServerPacket pck)
            : base(pck)
        {
            this.ObjectID = ReadInt();
            this.X = ReadInt();
            this.Y = ReadInt();
            this.Z = ReadInt();
            this.Heading = ReadInt();
        }
    }
}