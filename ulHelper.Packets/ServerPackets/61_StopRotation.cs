using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ulHelper.Packets
{
    /*
    61=StopRotation:d(CharID)d(Degree)d(speed)c(0)
    */
    /// <summary>
    /// ID = 61
    /// </summary>
    public class StopRotation : ServerPacket
    {
        public int ObjectID { get; set; }
        public int Angle { get; set; }

        public StopRotation(ServerPacket pck)
            : base(pck)
        {
            this.ObjectID = ReadInt();
            this.Angle = ReadInt();
        }
    }
}