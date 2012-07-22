using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ulHelper.Packets
{
    /*
    29=ChangeWaitType:d(CharID)d(MoveType)d(X)d(Y)d(Z)
    */
    /// <summary>
    /// ID = 29
    /// </summary>
    public class ChangeWaitType : ServerPacket
    {
        public int ObjectID { get; set; }
        public int MoveType { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }

        public ChangeWaitType(ServerPacket pck)
            : base(pck)
        {
            this.ObjectID = ReadInt();
            this.MoveType = ReadInt();
            this.X = ReadInt();
            this.Y = ReadInt();
            this.Z = ReadInt();
        }
    }
}