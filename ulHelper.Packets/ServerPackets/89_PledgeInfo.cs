using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ulHelper.Packets
{
    /// <summary>
    /// ID = 89
    /// </summary>
    public class PledgeInfo : ServerPacket
    {
        public int ClanID { get; set; }
        public string ClanName { get; set; }
        public string AllyName { get; set; }

        public PledgeInfo(ServerPacket pck)
            : base(pck)
        {
            this.ClanID = ReadInt();
            this.ClanName = ReadString();
            this.AllyName = ReadString();
        }
    }
}