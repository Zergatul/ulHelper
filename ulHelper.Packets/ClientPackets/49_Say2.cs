using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ulHelper.Packets
{
    /// <summary>
    /// ID = 49
    /// </summary>
    public class Say2 : ClientPacket
    {
        protected override byte id
        {
            get { return 0x49; }
        }

        public string Text { get; set; }
        public SayType Type { get; set; }
        public string Target { get; set; }

        protected override void Format()
        {
            AddValue(Text);
            AddValue((int)Type);
            if (Type == SayType.Private)
                AddValue(Target);
            base.Format();
        }

        public enum SayType
        {
            WhiteChat = 0,
            Shout = 1,
            Private = 2,
            Unknown3 = 3,
            Unknown4 = 4
        }
    }
}