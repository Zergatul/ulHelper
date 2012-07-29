using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ulHelper.Packets
{
    /// <summary>
    /// ID = 9E
    /// </summary>
    public class PlaySound : ServerPacket
    {
        public int Type { get; set; }
        public string SoundFile { get; set; }
        public int HasCenterObject { get; set; }
        public int ObjectID { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }

        public PlaySound(ServerPacket pck)
            : base(pck)
        {
            this.Type = ReadInt();
            this.SoundFile = ReadString();
            this.HasCenterObject = ReadInt();
            this.ObjectID = ReadInt();
            this.X = ReadInt();
            this.Y = ReadInt();
            this.Z = ReadInt();
        }
    }
}