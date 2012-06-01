using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ulHelper.Packets
{
    public class D09D : ClientPacket
    {
        protected override byte id
        {
            get { return 0xD0; }
        }

        protected override short id2
        {
            get { return 0x9D; }
        }

        public int ObjectID { get; set; }
        public string Name { get; set; }
        public long Price { get; set; }
        public long Quantity { get; set; }
        public int Period { get; set; }

        protected override void Format()
        {
            AddValue(ObjectID);
            AddValue(Name);
            AddValue(Price);
            AddValue(Quantity);
            AddValue(Period);
            AddValue((int)0);
            AddValue((int)0);
            base.Format();
        }
    }
}
