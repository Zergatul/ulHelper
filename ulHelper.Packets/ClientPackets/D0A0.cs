using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ulHelper.Packets
{
    /// <summary>
    /// ID = D0A0
    /// </summary>
    public class D0A0 : ClientPacket
    {
        protected override byte id
        {
            get { return 0xD0; }
        }

        protected override short id2
        {
            get { return 0xA0; }
        }

        public int AuctionID { get; set; }
        public int RangFilter { get; set; }
        public int ItemTypeFilter { get; set; }
        public string Search { get; set; }

        public D0A0()
        {
            ItemTypeFilter = -1;
            RangFilter = -1;
            Search = "";
        }

        protected override void Format()
        {
            AddValue((int)1);
            AddValue(AuctionID);
            AddValue(ItemTypeFilter);
            AddValue(RangFilter);
            AddValue(Search);
            base.Format();
        }
    }
}
