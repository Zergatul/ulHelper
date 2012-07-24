using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ulHelper.Packets
{
    /// <summary>
    /// ID = 7A
    /// </summary>
    public class StartRotation : ServerPacket
    {
        public int ObjectID { get; set; }
        public int Angle { get; set; }
        public int Side { get; set; }

        public StartRotation(ServerPacket pck)
            : base(pck)
        {
            this.ObjectID = ReadInt();
            this.Angle = ReadInt();
            this.Side = ReadInt();
        }
    }
}