using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ulHelper.Packets
{
    /// <summary>
    /// ID = CC
    /// </summary>
    public class NicknameChanged : ServerPacket
    {
        public int ObjectID { get; set; }
        public string Title { get; set; }

        public NicknameChanged(ServerPacket pck)
            : base(pck)
        {
            this.ObjectID = ReadInt();
            this.Title = ReadString();
        }
    }
}