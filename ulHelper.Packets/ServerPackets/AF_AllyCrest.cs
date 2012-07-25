using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ulHelper.Packets
{
    /// <summary>
    /// ID = AF
    /// </summary>
    public class AllyCrest : ServerPacket
    {
        public int CrestID { get; set; }
        public byte[] CrestData { get; set; }

        public AllyCrest(ServerPacket pck)
            : base(pck)
        {
            this.CrestID = ReadInt();
            int length = ReadInt();
            this.CrestData = new byte[length];
            Array.ConstrainedCopy(this.Data, 8, this.CrestData, 0, length);
        }
    }
}