using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ulHelper.Packets
{
    /// <summary>
    /// ID = 0x17
    /// </summary>
    public class RequestDropItem : ClientPacket
    {
        protected override byte id
        {
            get { return 0x17; }
        }

        public int ObjectID { get; set; }
        public long Count { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }

        protected override void Format()
        {
            AddValue(ObjectID);
            AddValue(Count);
            AddValue(X);
            AddValue(Y);
            AddValue(Z);
            base.Format();
        }
    }
}
