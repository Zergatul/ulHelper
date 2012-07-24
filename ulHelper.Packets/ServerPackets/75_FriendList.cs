using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ulHelper.Packets
{
    /// <summary>
    /// ID = 75
    /// </summary>
    public class FriendList : ServerPacket
    {
        public List<Friend> Friends { get; set; }

        public FriendList(ServerPacket pck)
            : base(pck)
        {
            int count = ReadInt();
            Friends = new List<Friend>(count);
            for (int i = 0; i < count; i++)
            {
                var friend = new Friend();
                friend.FriendID = ReadInt();
                friend.Name = ReadString();
                friend.Online = ReadInt();
                friend.ObjectID = ReadInt();
                friend.Level = ReadInt();
                friend.ClassID = ReadInt();
                short hz3 = ReadShort();
                Friends.Add(friend);
            }
        }

        public class Friend
        {
            public int FriendID { get; set; }
            public string Name { get; set; }
            public int Online { get; set; }
            public int ObjectID { get; set; }
            public int Level { get; set; }
            public int ClassID { get; set; }
        }
    }
}