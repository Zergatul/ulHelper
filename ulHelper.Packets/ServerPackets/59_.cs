using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ulHelper.Packets
{
    /// <summary>
    /// ID = 59
    /// </summary>
    public class _59 : ServerPacket
    {
        public string Str { get; set; }
        public int IntVal { get; set; }

        public _59(ServerPacket pck)
            : base(pck)
        {
            this.Position += 4;
            this.Str = ReadString();
            this.IntVal = ReadInt();
        }
    }
}