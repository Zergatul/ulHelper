using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ulHelper.Packets
{
    /// <summary>
    /// ID = 0x1F
    /// </summary>
    public class Action : ClientPacket
    {
        protected override byte id
        {
            get { return 0x1F; }
        }

        public int ObjectID { get; set; }
        public int OrigX { get; set; }
        public int OrigY { get; set; }
        public int OrigZ { get; set; }
        public byte ActionID { get; set; }

        protected override void Format()
        {
            AddValue(ObjectID);
            AddValue(OrigX);
            AddValue(OrigY);
            AddValue(OrigZ);
            AddValue(ActionID);
            base.Format();
        }
    }
}
