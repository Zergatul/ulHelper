using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ulHelper.Packets
{
    /// <summary>
    /// ID = 62
    /// </summary>
    public class SystemMessage : ServerPacket
    {
        public int MessageID { get; set; }
        public List<object> Values { get; set; }

        public SystemMessage(ServerPacket pck)
            : base(pck)
        {
            this.MessageID = ReadInt();
            int count = ReadInt();
            Values = new List<object>(count);
            for (int i = 0; i < count; i++)
            {
                int type = ReadInt();
                if (type == 1) // int
                    Values.Add(ReadInt());
                else
                    break;
            }
        }
    }
}