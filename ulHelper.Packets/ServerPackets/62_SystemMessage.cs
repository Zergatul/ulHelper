using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ulHelper.Packets
{
    /*
    62=SystemMessage:
     * d(_messageId:Get.MsgID)d(size:Loop.1.1)d(type:switch.0.13)_(TYPE_TEXT:case.0.1)s(String)_(TYPE_NUMBER:case.1.1)d(Number)_(TYPE_NPC_NAME:case.2.1)d(Number:Get.NpcID)_(TYPE_ITEM_NAME:case.3.1)d(ItemID:Get.Func01)_(TYPE_SKILL_NAME:case.4.2)d(skill_id:Get.SkillID)d(skill_level)_(TYPE_UNKNOWN_5:case.5.1)d(Number)_(TYPE_LONG:case.6.1)q(Long)_(TYPE_ZONE_NAME:case.7.3)d(coord.x)d(coord.y)d(coord.z)_(TYPE_UNKNOWN_8:case.8.3)d(ItemID:Get.Func01)h(Number)h(Number)_(TYPE_UNKNOWN_9:case.9.1)d(Number)_(TYPE_UNKNOWN_10:case.10.1)d(Number)_(TYPE_UNKNOWN_11:case.11.1)d(Number)_(TYPE_UNKNOWN_12:case.12.1)s(String)
    */
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