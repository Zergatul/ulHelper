using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ulHelper.Packets
{
    /// <summary>
    /// ID = 1F
    /// </summary>
    public class ActionFailed : ServerPacket
    {
        public ActionFailed(ServerPacket pck)
            : base(pck)
        {
            
        }
    }
}