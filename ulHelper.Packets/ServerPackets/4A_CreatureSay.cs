using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ulHelper.Packets
{
    /// <summary>
    /// ID = 4A
    /// </summary>
    public class CreatureSay : ServerPacket
    {
        public int ObjectID { get; set; }
        public int TextType { get; set; }
        public string CharName { get; set; }
        public string Message { get; set; }

        public CreatureSay(ServerPacket pck)
            : base(pck)
        {
            this.ObjectID = ReadInt();
            this.TextType = ReadInt();
            this.CharName = ReadString();
            this.Position += 4;
            this.Message = ReadString();
        }
    }
}