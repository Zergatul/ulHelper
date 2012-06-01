using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ulHelper.Packets
{
    /// <summary>
    /// ID = 0x6B
    /// </summary>
    public class RequestSendFriendMsg : ClientPacket
    {
        protected override byte id
        {
            get { return 0x6B; }
        }

        public string Message { get; set; }
        public string Reciever { get; set; }

        protected override void Format()
        {
            AddValue(Message);
            AddValue(Reciever);
            base.Format();
        }
    }
}
