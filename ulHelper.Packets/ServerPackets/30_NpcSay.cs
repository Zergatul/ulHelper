using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ulHelper.Packets
{
    /// <summary>
    /// ID = 30
    /// </summary>
    public class NpcSay : ServerPacket
    {
        public int ObjectID { get; set; }
        public int TextType { get; set; }
        public int NpcID { get; set; }
        public int MsgType { get; set; }

        public NpcSay(ServerPacket pck)
            : base(pck)
        {
            this.ObjectID = ReadInt();
            this.TextType = ReadInt();
            this.NpcID = ReadInt();
            this.MsgType = ReadInt();
        }
    }
}