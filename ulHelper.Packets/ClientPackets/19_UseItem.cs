using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ulHelper.Packets
{
    /// <summary>
    /// ID = 0x19
    /// </summary>
    public class UseItem : ClientPacket
    {
        protected override byte id
        {
            get { return 0x19; }
        }

        public int ObjectID { get; set; }
        public int CtrlPressed { get; set; }

        protected override void Format()
        {
            AddValue(ObjectID);
            AddValue(CtrlPressed);
            base.Format();
        }
    }
}
