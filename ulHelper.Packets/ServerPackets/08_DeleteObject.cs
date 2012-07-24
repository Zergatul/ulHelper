using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ulHelper.Packets
{
    /// <summary>
    /// ID = 08
    /// </summary>
    public class DeleteObject : ServerPacket
    {
        public int ObjectID { get; set; }

        public DeleteObject(ServerPacket pck)
            : base(pck)
        {
            this.ObjectID = ReadInt();
        }
    }
}