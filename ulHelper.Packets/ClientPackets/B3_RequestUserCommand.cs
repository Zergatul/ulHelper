using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ulHelper.Packets
{
    /// <summary>
    /// ID = B3
    /// </summary>
    public class RequestUserCommand : ClientPacket
    {
        protected override byte id
        {
            get { return 0xB3; }
        }

        public int Command { get; set; }

        protected override void Format()
        {
            AddValue(Command);
            base.Format();
        }
    }
}