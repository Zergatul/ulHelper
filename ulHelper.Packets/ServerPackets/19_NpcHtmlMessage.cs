using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HtmlAgilityPack;

namespace ulHelper.Packets
{
    /*
     19=NpcHtmlMessage:
     * d(objID)s(HTML)d(itemID)
     */
    /// <summary>
    /// ID = 19
    /// </summary>
    public class NpcHtmlMessage : ServerPacket
    {
        public int ObjectID { get; set; }
        public string HTML { get; set; }
        public int ItemID { get; set; }

        public NpcHtmlMessage(ServerPacket pck)
            : base(pck)
        {
            this.ObjectID = ReadInt();
            this.HTML = ReadString();
            this.ItemID = ReadInt();
        }

        public void ParseHTML()
        {
            var doc = new HtmlDocument();
            doc.LoadHtml(HTML);
        }
    }
}