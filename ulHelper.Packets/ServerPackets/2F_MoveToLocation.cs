using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ulHelper.Packets
{
    /*
    2F=MoveToLocation:
     * d(ObjectID)d(DestX)d(DestY)d(DestZ)d(CurX)d(CurY)d(CurZ)
    */
    /// <summary>
    /// ID = 2F
    /// </summary>
    public class MoveToLocation : ServerPacket
    {
        public int ObjectID { get; set; }
        public int DestX { get; set; }
        public int DestY { get; set; }
        public int DestZ { get; set; }
        public int CurX { get; set; }
        public int CurY { get; set; }
        public int CurZ { get; set; }

        public MoveToLocation(ServerPacket pck)
            : base(pck)
        {
            this.ObjectID = ReadInt();
            this.DestX = ReadInt();
            this.DestY = ReadInt();
            this.DestZ = ReadInt();
            this.CurX = ReadInt();
            this.CurY = ReadInt();
            this.CurZ = ReadInt();
        }
    }
}