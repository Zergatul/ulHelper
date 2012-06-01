using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ulHelper.Packets
{
    /// <summary>
    /// ID = 0x3A
    /// </summary>
    public class Appearing : ClientPacket
    {
        protected override byte id
        {
            get { return 0x3A; }
        }
    }
}
