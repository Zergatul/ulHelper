using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ulHelper.Packets
{
    /*
    27=SocialAction:d(PlayerID)d(Action:Get.F9)
    */
    /// <summary>
    /// ID = 27
    /// </summary>
    public class SocialAction : ServerPacket
    {
        public int ObjectID { get; set; }
        public int Action { get; set; }

        public SocialAction(ServerPacket pck)
            : base(pck)
        {
            this.ObjectID = ReadInt();
            this.Action = ReadInt();
        }
    }
}