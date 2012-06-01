using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ulHelper.Packets
{
    /*
    32=UserInfo:
        d(X)d(Y)d(Z)d(isInAirShip)
     *  d(ObjectID)s(Name)d(Race)d(Sex)d(ClassID:Get.ClassID)
     *  d(Level)q(Exp)d(Str)d(Dex)d(Con)d(Int)d(Wit)d(Men)
     *  d(MaxHP)d(CurrentHP)d(MaxMP)d(CurrentMP)d(Sp)
     *  d(CurrentLoad)d(MaxLoad)d(WeaponEquipment 20=no,40=yes)
     *  d(OIDUnder)d(OIDRear)d(OIDLear)d(OIDNeck)d(OIDFinger)d(OIDLfinger)d(OIDHead)d(OIDRhand)d(OIDLhand)d(OIDGloves)d(OIDChest)d(OIDLegs)d(OIDFeet)d(OIDBack)d(OIDLrhand)d(OIDHair)d(OIDHair2)d(OIDRbracelet)d(OIDLbracelet)d(OIDDeco)d(OIDDeco2)d(OIDDeco3)d(OIDDeco4)d(OIDDeco5)d(OIDDeco6)d(OIDBelt)d(IDUnder:Get.Func01)d(IDRear:Get.Func01)d(IDLear:Get.Func01)d(IDNeck:Get.Func01)d(IDFinger:Get.Func01)d(IDLfinger:Get.Func01)d(IDHead:Get.Func01)d(IDRhand:Get.Func01)d(IDLhand:Get.Func01)d(IDGloves:Get.Func01)d(IDChest:Get.Func01)d(IDLegs:Get.Func01)d(IDFeet:Get.Func01)d(IDBack:Get.Func01)d(IDLrhand:Get.Func01)d(IDHair:Get.Func01)d(IDHair2:Get.Func01)d(IDRbracelet:Get.Func01)d(IDLbracelet:Get.Func01)d(IDDeco:Get.Func01)d(IDDeco2:Get.Func01)d(IDDeco3:Get.Func01)d(IDDeco4:Get.Func01)d(IDDeco5:Get.Func01)d(IDDeco6:Get.Func01)d(IDBelt:Get.Func01)d(AugIDUnder)d(AugIDRear)d(AugIDLear)d(AugIDNeck)d(AugIDFinger)d(AugIDLfinger)d(AugIDHead)d(AugIDRhand)d(AugIDLhand)d(AugIDGloves)d(AugIDChest)d(AugIDLegs)d(AugIDFeet)d(AugIDBack)d(AugIDLrhand)d(AugIDHair)d(AugIDHair2)d(AugIDRbracelet)d(AugIDLbracelet)d(AugIDDeco)d(AugIDDeco2)d(AugIDDeco3)d(AugIDDeco4)d(AugIDDeco5)d(AugIDDeco6)d(AugIDBelt)d(TalismanSlots)d(01)d(Patk)d(PatkSpd)d(Pdef)d(EvasionRate)d(Accuracy)d(CriticalHit)d(Matk)d(MatkSpd)d(PatkSpd)d(Mdef)d(PvPFlag)d(Karma)d(RunSpd)d(WalkSpd)d(SwimRunSpd)d(SwimWalkSpd)d(0)d(0)d(FlyRunSpd)d(FlyWalkSpd)f(MoveMul)f(AtkSpeedMul)f(ColRadius)f(ColHeight)d(HairStyle)d(HairColor)d(Face)d(isGM:1,0)s(Title)d(ClanID)d(ClanCrestID)d(AllyID)d(AllyCrestID)d(Relation)c(MountType)c(PrivateStoreType)c(DwarvenCraft:1,0)d(PkKills)d(PvPKills)h(CubicsSize:Loop.01.0001)h(CubicID)c(0)d(AbnormalEffect)c(FlayingMounted:2,0)d(ClanPrivileges)h(RecomLeft)h(RecomHave)d(MountNpcID)h(InventoryLimit)d(ClassID:Get.ClassID)d(0)
     *  d(MaxCP)d(CurrentCP)c(isMounted)c(Team:1-blue,2-red)d(ClanCrestLargeID)c(isNoble)c(isHero)c(isFishing)d(FishingX)d(FishingY)d(FishingZ)d(NameColor)c(isRunning)d(PledgeClass)d(PledgeType)d(TitleColor)d(CursedWeaponEquipID)d(TranformationID)h(AtkElementAttr)h(AttackElementVal)h(DefAttrFire)h(DefAttrWater)h(DefAttrWind)h(DefAttrEarth)h(DefAttrHoly)h(DefAttrDark)d(AgathionId)d(Fame)d(Unknown)d(VitalityPoints)d(0)d(0)d(0)d(0)
    */
    /// <summary>
    /// ID = 32
    /// </summary>
    public class UserInfo : ServerPacket
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }
        public int ObjectID { get; set; }
        public string Name { get; set; }
        public int Race { get; set; }
        public int Sex { get; set; }
        public int ClassID { get; set; }
        public int Level { get; set; }
        public long Exp { get; set; }
        public int MaxHP { get; set; }
        public int CurHP { get; set; }
        public int MaxMP { get; set; }
        public int CurMP { get; set; }
        public int MaxCP { get; set; }
        public int CurCP { get; set; }
        public string Title { get; set; }

        public UserInfo(ServerPacket pck) 
            : base(pck)
        {
            this.X = ReadInt();
            this.Y = ReadInt();
            this.Z = ReadInt();
            this.Position += 4;
            this.ObjectID = ReadInt();
            this.Name = ReadString();
            this.Race = ReadInt();
            this.Sex = ReadInt();
            this.ClassID = ReadInt();
            this.Level = ReadInt();
            this.Exp = ReadLong();
            this.Position += 8 * 4;
            this.MaxHP = ReadInt();
            this.CurHP = ReadInt();
            this.MaxMP = ReadInt();
            this.CurMP = ReadInt();
            this.Position += 100 * 4;
            this.Position += 4 * 8;
            this.Position += 14 * 4;
            this.Title = ReadString();
            this.Position += 5 * 4;
            this.Position += 3;
            this.Position += 2 * 4;
            this.Position += 2 * 2;
            this.Position++;
            this.Position += 4;
            this.Position++;
            this.Position += 4;
            this.Position += 2 * 2;
            this.Position += 4;
            this.Position += 2 * 4;
            this.Position += 24;
            this.CurCP = ReadInt();
            this.MaxCP = ReadInt();
        }
    }
}