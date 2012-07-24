using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ulHelper.Packets
{
    /// <summary>
    /// ID = 6A
    /// </summary>
    public class PledgeCrest : ServerPacket
    {
        public int CrestID { get; set; }
        public byte[] CrestData { get; set; }

        public PledgeCrest(ServerPacket pck)
            : base(pck)
        {
            this.CrestID = ReadInt();
            this.CrestData = new byte[this.Data.Length - 4];
            Array.ConstrainedCopy(this.Data, 4, this.CrestData, 0, this.CrestData.Length);
        }
    }
}