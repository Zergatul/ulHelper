using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ulHelper.Packets
{
    /// <summary>
    /// ID = 6B
    /// </summary>
    public class SetupGauge : ServerPacket
    {
        public int ObjectID { get; set; }
        public int Time1 { get; set; }
        public int Time2 { get; set; }

        public SetupGauge(ServerPacket pck)
            : base(pck)
        {
            this.ObjectID = ReadInt();
            this.Position += 4;
            this.Time1 = ReadInt();
            this.Time2 = ReadInt();
        }
    }
}