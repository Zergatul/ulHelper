using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ulHelper.Packets
{
    /// <summary>
    /// ID = 0x0F
    /// </summary>
    public class MoveBackwardToLocation : ClientPacket
    {
        protected override byte id
        {
            get { return 0x0F; }
        }

        public int TargetX { get; set; }
        public int TargetY { get; set; }
        public int TargetZ { get; set; }
        public int OriginX { get; set; }
        public int OriginY { get; set; }
        public int OriginZ { get; set; }
        public int MoveByMouse { get; set; }

        protected override void Format()
        {
            AddValue(TargetX);
            AddValue(TargetY);
            AddValue(TargetZ);
            AddValue(OriginX);
            AddValue(OriginY);
            AddValue(OriginZ);
            AddValue(MoveByMouse);
            base.Format();
        }
    }
}
