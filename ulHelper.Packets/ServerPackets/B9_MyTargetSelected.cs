using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ulHelper.Packets
{
    /// <summary>
    /// ID = B9
    /// </summary>
    public class MyTargetSelected : ServerPacket
    {
        public int ObjectID { get; set; }
        public short Color { get; set; }

        public MyTargetSelected(ServerPacket pck)
            : base(pck)
        {
            this.ObjectID = ReadInt();
            this.Color = ReadShort();
        }
    }
}