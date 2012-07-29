using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ulHelper.Packets
{
    /// <summary>
    /// ID = A7
    /// </summary>
    public class TutorialShowQuestionMark : ServerPacket
    {
        public int Number { get; set; }

        public TutorialShowQuestionMark(ServerPacket pck)
            : base(pck)
        {
            this.Number = ReadInt();
        }
    }
}