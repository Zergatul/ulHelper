using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ulHelper.Packets
{
    /*
    B9=MyTargetSelected:
     * d(ObjectID)h(Color)d(0)
    */
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