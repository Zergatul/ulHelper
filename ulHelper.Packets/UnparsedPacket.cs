using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ulHelper.Packets
{
    public class UnparsedPacket : ServerPacket
    {
        public byte _ID
        {
            get { return ID; }
        }
        public short _ID2
        {
            get { return ID2; }
        }
        public byte[] _Data
        {
            get { return Data; }
        }

        public UnparsedPacket(ServerPacket pck)
            : base(pck)
        {
        }
    }
}