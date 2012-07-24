using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ulHelper.Packets
{
    /// <summary>
    /// ID = FD
    /// </summary>
    public class AgitDecoInfo : ServerPacket
    {
        public int ClanHallID { get; set; }
        public byte HPRecovery { get; set; }
        public byte MPRecovery { get; set; }
        public byte ExpRecovery { get; set; }
        public byte Teleport { get; set; }
        public byte Curtains { get; set; }
        public byte Support { get; set; }
        public byte Platform { get; set; }
        public byte ItemCreate { get; set; }

        public AgitDecoInfo(ServerPacket pck)
            : base(pck)
        {
            this.ClanHallID = ReadInt();
            this.HPRecovery = ReadByte();
            this.MPRecovery = ReadByte();
            this.Position++;
            this.ExpRecovery = ReadByte();
            this.Teleport = ReadByte();
            this.Position++;
            this.Curtains = ReadByte();
            this.Position++;
            this.Support = ReadByte();
            this.Position++;
            this.Platform = ReadByte();
            this.ItemCreate = ReadByte();
        }
    }
}