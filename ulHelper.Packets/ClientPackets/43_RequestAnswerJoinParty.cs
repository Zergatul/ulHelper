using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ulHelper.Packets
{
    /// <summary>
    /// ID = 0x43
    /// </summary>
    public class RequestAnswerJoinParty : ClientPacket
    {
        protected override byte id
        {
            get { return 0x43; }
        }

        public int Response { get; set; }

        protected override void Format()
        {
            AddValue(Response);
            base.Format();
        }
    }
}