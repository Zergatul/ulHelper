using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ulHelper.Packets
{
    /// <summary>
    /// ID = F9
    /// </summary>
    public class EtcStatusUpdate : ServerPacket
    {
        public int Charges { get; set; }
        public int WeightPenalty { get; set; }
        public int ChatBanned { get; set; }
        public int DangerArea { get; set; }
        public int ExpertiseWeapPenalty { get; set; }
        public int ExpertArmorPenalty { get; set; }
        public int CharmOfCourage { get; set; }
        public int DeathPenaltyLevel { get; set; }
        public int Souls { get; set; }

        public EtcStatusUpdate(ServerPacket pck)
            : base(pck)
        {
            this.Charges = ReadInt();
            this.WeightPenalty = ReadInt();
            this.ChatBanned = ReadInt();
            this.DangerArea = ReadInt();
            this.ExpertiseWeapPenalty = ReadInt();
            this.ExpertArmorPenalty = ReadInt();
            this.CharmOfCourage = ReadInt();
            this.DeathPenaltyLevel = ReadInt();
            this.Souls = ReadInt();
        }
    }
}