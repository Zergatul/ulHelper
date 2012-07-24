using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ulHelper.Packets
{
    /// <summary>
    /// ID = BF
    /// </summary>
    public class PrivateStoreMsgBuy : ServerPacket
    {
        public int ObjectID { get; set; }
        public string StoreMsg { get; set; }

        public PrivateStoreMsgBuy(ServerPacket pck)
            : base(pck)
        {
            this.ObjectID = ReadInt();
            this.StoreMsg = ReadString();
        }
    }
}