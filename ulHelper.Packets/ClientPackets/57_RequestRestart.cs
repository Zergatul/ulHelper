using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ulHelper.Packets
{
    /// <summary>
    /// ID = 0x57
    /// </summary>
    public class RequestRestart : ClientPacket
    {
        protected override byte id
        {
            get { return 0x57; }
        }
    }
}
