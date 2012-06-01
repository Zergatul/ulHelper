using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ulHelper.Packets
{
    /// <summary>
    /// ID = 0x34
    /// </summary>
    public class RequestSocialAction : ClientPacket
    {
        protected override byte id
        {
            get { return 0x34; }
        }

        public int Action { get; set; }

        protected override void Format()
        {
            AddValue(Action);
            base.Format();
        }
    }
}
