using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ulHelper.Packets
{
    /// <summary>
    /// ID = 0x00
    /// </summary>
    public class Logout : ClientPacket
    {
        protected override byte id
        {
            get { return 0x00; }
        }
    }
}
