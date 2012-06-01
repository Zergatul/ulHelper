using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ulHelper.Packets
{
    /*
    31=CharInfo:
     * d(X)d(Y)d(Z)d(Heading)
     * d(ObjectID)
     * s(Name)
     * d(Race)d(Sex)
     * d(ClassID:Get.ClassID)
     * d(Under:Get.F0)d(Head:Get.F0)d(RHand:Get.F0)
     * d(LHand:Get.F0)d(Gloves:Get.F0)d(Chest:Get.F0)
     * d(Legs:Get.F0)d(Feet:Get.F0)d(Back:Get.F0)
     * d(LRHand:Get.F0)d(Hair:Get.F0)d(Hair2:Get.F0)
     * d(RBrace:Get.F0)d(LBrace:Get.F0)d(DEC1:Get.F0)
     * d(DEC2:Get.F0)d(DEC3:Get.F0)d(DEC4:Get.F0)
     * d(DEC5:Get.F0)d(DEC6:Get.F0)d(Belt:Get.F0)
     * d(AUnder:Get.F1)d(AHead:Get.F1)d(ARHand:Get.F1)
     * d(ALHand:Get.F1)d(AGloves:Get.F1)d(AChest:Get.F1)
     * d(ALegs:Get.F1)d(AFeet:Get.F1)d(ABack:Get.F1)
     * d(ALRHand:Get.F1)d(AHair:Get.F1)d(AHair2:Get.F1)
     * d(ARBrace:Get.F1)d(ALBrace:Get.F1)d(ADEC1:Get.F1)
     * d(ADEC2:Get.F1)d(ADEC3:Get.F1)d(ADEC4:Get.F1)
     * d(ADEC5:Get.F1)d(ADEC6:Get.F1)d(ABelt:Get.F1)
     * d(0)d(1)
     * d(pvpFlag)d(karma)
     * d(CastSpd)d(AtkSpd)
     * d(0)
     * d(runSpd)d(walkSpd)d(swimRSpd)d(swimWSpd)d(flRunSpd)
     * d(flWalkSpd)d(flyRunSpd)d(flyWalkSpd)
     * f(SpdMult)f(ASpdMult)
     * f(collisRadius)f(collisHeight)
     * d(HairStyle)d(HairColor)d(Face)
     * s(Title)
     * d(clanID)d(clanCrestID)d(allyID)d(allyCrestID)
     * c(isStand)c(isRun)c(isInFight)c(isAlikeDead)c(Invis)c(mountType)c(isShop)
     * h(cubics:Loop.01.0001)h(cubID)
     * c(findparty)d(abnEffects)c(isFlying)
     * h(RecomHave)d(MountNpcID:Get.NpcId)
     * d(classID:Get.ClassID)d(curCP)c(isMounted)c(Team)
     * d(clanBigCrestId)c(isNoble)c(isHero)c(isFishing)d(fishX)d(fishY)d(fishZ)
     * d(NameColor:Get.FCol)d(heading)
     * d(PledgeClass)d(PledgeType)d(TitleColor:Get.FCol)
     * d(CursedItem:Get.F0)d(ClanRep)d(TransformID)d(AgathionID)d(Fame)
     * d(specEffects)d(0)d(0)d(0)
    */
    /// <summary>
    /// ID = 31
    /// </summary>
    public class CharInfo : ServerPacket
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }
        public int ObjectID { get; set; }
        public string VisibleName { get; set; }
        public int Race { get; set; }
        public int Sex { get; set; }
        public int SkinID { get; set; }
        public int ClassID { get; set; }
        public int PvpFlag { get; set; }
        public string VisibleTitle { get; set; }
        public int ClanID { get; set; }
        public int ClanCrestID { get; set; }

        public CharInfo(ServerPacket pck) 
            : base(pck)
        {
            this.X = ReadInt();
            this.Y = ReadInt();
            this.Z = ReadInt();
            this.Position += 4;
            this.ObjectID = ReadInt();
            this.VisibleName = ReadString();
            this.Race = ReadInt();
            this.Sex = ReadInt();
            this.SkinID = ReadInt();
            this.Position += 44 * 4;
            this.PvpFlag = ReadInt();
            this.Position += 13 * 4;
            this.Position += 4 * 8;
            this.Position += 3 * 4;
            this.Position += 8;
            this.VisibleTitle = ReadString();
            this.ClanID = ReadInt();
            this.ClanCrestID = ReadInt();
            this.Position += 2 * 4;
            this.Position += 7;
            this.Position += 14;
            this.Position += 15;
            this.Position += 27;
            this.Position -= 18;
            this.ClassID = ReadByte();
        }
    }
}