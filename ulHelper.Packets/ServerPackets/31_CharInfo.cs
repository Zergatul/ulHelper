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
        public int Karma { get; set; }
        public int CastSpeed { get; set; }
        public int AtkSpeed { get; set; }
        public int RunSpeed { get; set; }
        public int WalkSpeed { get; set; }
        public int SwimRunSpeed { get; set; }
        public int SwimWalkSpeed { get; set; }
        public int FlyRunSpeed { get; set; }
        public int FlyWalkSpeed { get; set; }
        public double MoveMult { get; set; }
        public double AtkSpeedMult { get; set; }
        public double CollisionRadius { get; set; }
        public double CollisionHeight { get; set; }
        public int HairStyle { get; set; }
        public int HairColor { get; set; }
        public int Face { get; set; }        
        public string VisibleTitle { get; set; }
        public int ClanID { get; set; }
        public int ClanCrestID { get; set; }
        public int AllyID { get; set; }
        public int AllyCrestID { get; set; }
        public byte IsStand { get; set; }
        public byte IsRun { get; set; }
        public byte InFight { get; set; }
        public byte IsAlikeDead { get; set; }
        public byte Invis { get; set; }
        public byte MountType { get; set; }
        public byte IsShop { get; set; }
        public List<short> Cubics { get; set; }
        public byte FindParty { get; set; }
        public int AbnormalEffect { get; set; }
        public byte IsFly { get; set; }
        public short ReccomendHave { get; set; }
        public int MountNpcID { get; set; }
        public int ClassID { get; set; }
        public byte IsMounted { get; set; }
        public byte Team { get; set; }
        public int ClanBigCrestID { get; set; }
        public byte IsNoble { get; set; }
        public byte IsHero { get; set; }
        public byte IsFishing { get; set; }
        public int FishX { get; set; }
        public int FishY { get; set; }
        public int FishZ { get; set; }
        public int NameColor { get; set; }
        public int Heading { get; set; }
        public int PledgeClass { get; set; }
        public int PledgeType { get; set; }
        public int TitleColor { get; set; }
        public int CursedWeaponEquipID { get; set; }
        public int ClanReputation { get; set; }
        public int TransformationID { get; set; }
        public int AgathionID { get; set; }
        public int CurCP { get; set; }
        public int CurHP { get; set; }
        public int MaxHP { get; set; }
        public int CurMP { get; set; }
        public int MaxMP { get; set; }

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
            this.Position += 54 * 4; // equip
            this.Karma = ReadInt();
            this.CastSpeed = ReadInt();
            this.AtkSpeed = ReadInt();
            this.Position += 4;
            this.RunSpeed = ReadInt();
            this.WalkSpeed = ReadInt();
            this.SwimRunSpeed = ReadInt();
            this.SwimWalkSpeed = ReadInt();
            this.FlyRunSpeed = ReadInt();
            this.FlyWalkSpeed = ReadInt();
            this.Position += 8;
            this.MoveMult = ReadDouble();
            this.AtkSpeedMult = ReadDouble();
            this.CollisionRadius = ReadDouble();
            this.CollisionHeight = ReadDouble();
            this.HairStyle = ReadInt();
            this.HairColor = ReadInt();
            this.Face = ReadInt();
            this.VisibleTitle = ReadString();
            this.ClanID = ReadInt();
            this.ClanCrestID = ReadInt();
            this.AllyID = ReadInt();
            this.AllyCrestID = ReadInt();
            this.IsStand = ReadByte();
            this.IsRun = ReadByte();
            this.InFight = ReadByte();
            this.IsAlikeDead = ReadByte();
            this.Invis = ReadByte();
            this.MountType = ReadByte();
            this.IsShop = ReadByte();
            int cubicsCount = ReadShort();
            this.Cubics = new List<short>(cubicsCount);
            for (int i = 0; i < cubicsCount; i++)
                this.Cubics.Add(ReadShort());
            this.FindParty = ReadByte();
            this.AbnormalEffect = ReadInt();
            this.IsFly = ReadByte();
            this.ReccomendHave = ReadShort();
            this.MountNpcID = ReadInt();
            this.ClassID = ReadInt();
            this.Position += 4;
            this.IsMounted = ReadByte();
            this.Team = ReadByte();
            this.ClanBigCrestID = ReadInt();
            this.IsNoble = ReadByte();
            this.IsHero = ReadByte();
            this.IsFishing = ReadByte();
            this.FishX = ReadInt();
            this.FishY = ReadInt();
            this.FishZ = ReadInt();
            this.NameColor = ReadInt();
            this.Heading = ReadInt();
            this.PledgeClass = ReadInt();
            this.PledgeType = ReadInt();
            this.TitleColor = ReadInt();
            this.CursedWeaponEquipID = ReadInt();
            this.ClanReputation = ReadInt();
            this.TransformationID = ReadInt();
            this.AgathionID = ReadInt();
            this.Position += 5 * 4;
            this.CurCP = ReadInt();
            this.CurHP = ReadInt();
            this.MaxHP = ReadInt();
            this.CurMP = ReadInt();
            this.MaxMP = ReadInt();
        }
    }
}