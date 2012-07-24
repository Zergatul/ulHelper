using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ulHelper.Packets
{
    /// <summary>
    /// ID = CE
    /// </summary>
    public class RelationChanged : ServerPacket
    {
        public int ObjectID { get; set; }
        public int Relation { get; set; }

        public RelationChanged(ServerPacket pck)
            : base(pck)
        {
            this.ObjectID = ReadInt();
            this.Relation = ReadInt();
        }
    }
}