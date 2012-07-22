using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ulHelper.Packets
{
    /*
    BA=PartyMemberPosition:d(Count:Loop.01.0004)d(ObjectID)d(X)d(Y)d(Z)
    */
    /// <summary>
    /// ID = BA
    /// </summary>
    public class PartyMemberPosition : ServerPacket
    {
        public List<Member> Members { get; set; }

        public PartyMemberPosition(ServerPacket pck)
            : base(pck)
        {
            int count = ReadInt();
            Members = new List<Member>(count);
            for (int i = 0; i < count; i++)
            {
                var member = new Member();
                member.ObjectID = ReadInt();
                member.X = ReadInt();
                member.Y = ReadInt();
                member.Y = ReadInt();
                Members.Add(member);
            }
        }

        public class Member
        {
            public int ObjectID { get; set; }
            public int X { get; set; }
            public int Y { get; set; }
            public int Z { get; set; }
        }
    }
}