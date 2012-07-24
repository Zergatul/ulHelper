using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ulHelper.Packets
{
    /// <summary>
    /// ID = E1
    /// </summary>
    public class RecipeShopMsg : ServerPacket
    {
        public int ObjectID { get; set; }
        public string StoreMsg { get; set; }

        public RecipeShopMsg(ServerPacket pck)
            : base(pck)
        {
            this.ObjectID = ReadInt();
            this.StoreMsg = ReadString();
        }
    }
}