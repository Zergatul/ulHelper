using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ulHelper.Packets
{
    /// <summary>
    /// ID = 74
    /// </summary>
    public class SendBypassBuildCmd : ClientPacket
    {
        protected override byte id
        {
            get { return 0x74; }
        }

        public string Command { get; set; }

        protected override void Format()
        {
            AddValue(Command);
            base.Format();
        }
    }
}