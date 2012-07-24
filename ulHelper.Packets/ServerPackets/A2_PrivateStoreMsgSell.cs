using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ulHelper.Packets
{
    /// <summary>
    /// ID = A2
    /// </summary>
    public class PrivateStoreMsgSell : ServerPacket
    {
        public int ObjectID { get; set; }
        public string StoreMsg { get; set; }

        public PrivateStoreMsgSell(ServerPacket pck)
            : base(pck)
        {
            this.ObjectID = ReadInt();
            this.StoreMsg = ReadString();
        }
    }
}