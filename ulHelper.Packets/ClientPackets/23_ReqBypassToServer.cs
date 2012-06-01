using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ulHelper.Packets
{
    /// <summary>
    /// ID = 0x23
    /// </summary>
    public class ReqBypassToServer : ClientPacket
    {
        protected override byte id
        {
            get { return 0x23; }
        }

        public string Command { get; set; }

        protected override void Format()
        {
            AddValue(Command);
            base.Format();
        }
    }
}
