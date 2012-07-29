using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ulHelper.Packets
{
    /// <summary>
    /// ID = 86
    /// </summary>
    public class QuestList : ServerPacket
    {
        public List<Quest> Quests { get; set; }

        public QuestList(ServerPacket pck)
            : base(pck)
        {
            short count = ReadShort();
            this.Quests = new List<Quest>(count);
            for (int i = 0; i < count; i++)
            {
                var quest = new Quest();
                quest.QuestID = ReadInt();
                quest.Status = ReadInt();
                this.Quests.Add(quest);
            }
            // 128 bytes after
        }

        public class Quest
        {
            public int QuestID { get; set; }
            public int Status { get; set; }
        }
    }
}