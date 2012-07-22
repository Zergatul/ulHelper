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
     *  d(OIDUnder)d(OIDRear)d(OIDLear)d(OIDNeck)d(OIDFinger)d(OIDLfinger)d(OIDHead)d(OIDRhand)d(OIDLhand)d(OIDGloves)d(OIDChest)d(OIDLegs)d(OIDFeet)d(OIDBack)d(OIDLrhand)d(OIDHair)d(OIDHair2)d(OIDRbracelet)d(OIDLbracelet)d(OIDDeco)d(OIDDeco2)d(OIDDeco3)d(OIDDeco4)d(OIDDeco5)d(OIDDeco6)d(OIDBelt)
     *  d(IDUnder:Get.Func01)d(IDRear:Get.Func01)d(IDLear:Get.Func01)d(IDNeck:Get.Func01)d(IDFinger:Get.Func01)d(IDLfinger:Get.Func01)d(IDHead:Get.Func01)d(IDRhand:Get.Func01)d(IDLhand:Get.Func01)d(IDGloves:Get.Func01)d(IDChest:Get.Func01)d(IDLegs:Get.Func01)d(IDFeet:Get.Func01)d(IDBack:Get.Func01)d(IDLrhand:Get.Func01)d(IDHair:Get.Func01)d(IDHair2:Get.Func01)d(IDRbracelet:Get.Func01)d(IDLbracelet:Get.Func01)d(IDDeco:Get.Func01)d(IDDeco2:Get.Func01)d(IDDeco3:Get.Func01)d(IDDeco4:Get.Func01)d(IDDeco5:Get.Func01)d(IDDeco6:Get.Func01)d(IDBelt:Get.Func01)
     *  d(AugIDUnder)d(AugIDRear)d(AugIDLear)d(AugIDNeck)d(AugIDFinger)d(AugIDLfinger)d(AugIDHead)d(AugIDRhand)d(AugIDLhand)d(AugIDGloves)d(AugIDChest)d(AugIDLegs)d(AugIDFeet)d(AugIDBack)d(AugIDLrhand)d(AugIDHair)d(AugIDHair2)d(AugIDRbracelet)d(AugIDLbracelet)d(AugIDDeco)d(AugIDDeco2)d(AugIDDeco3)d(AugIDDeco4)d(AugIDDeco5)d(AugIDDeco6)d(AugIDBelt)
     *  d(TalismanSlots)d(01)
     *  d(Patk)d(PatkSpd)d(Pdef)d(EvasionRate)d(Accuracy)d(CriticalHit)
     *  d(Matk)d(MatkSpd)d(PatkSpd)d(Mdef)
     *  d(PvPFlag)d(Karma)
     *  d(RunSpd)d(WalkSpd)d(SwimRunSpd)d(SwimWalkSpd)d(0)d(0)d(FlyRunSpd)d(FlyWalkSpd)
     *  f(MoveMul)f(AtkSpeedMul)f(ColRadius)f(ColHeight)d(HairStyle)d(HairColor)d(Face)
     *  d(isGM:1,0)s(Title)d(ClanID)d(ClanCrestID)d(AllyID)d(AllyCrestID)d(Relation)
     *  c(MountType)c(PrivateStoreType)c(DwarvenCraft:1,0)d(PkKills)d(PvPKills)
     *  h(CubicsSize:Loop.01.0001)h(CubicID)c(0)d(AbnormalEffect)c(FlayingMounted:2,0)
     *  d(ClanPrivileges)h(RecomLeft)h(RecomHave)d(MountNpcID)h(InventoryLimit)
     *  d(ClassID:Get.ClassID)d(0)
     *  d(MaxCP)d(CurrentCP)c(isMounted)c(Team:1-blue,2-red)d(ClanCrestLargeID)c(isNoble)
     *  c(isHero)c(isFishing)d(FishingX)d(FishingY)d(FishingZ)d(NameColor)c(isRunning)
     *  d(PledgeClass)d(PledgeType)d(TitleColor)d(CursedWeaponEquipID)d(TranformationID)
     *  h(AtkElementAttr)h(AttackElementVal)h(DefAttrFire)h(DefAttrWater)h(DefAttrWind)
     *  h(DefAttrEarth)h(DefAttrHoly)h(DefAttrDark)d(AgathionId)d(Fame)d(Unknown)
     *  d(VitalityPoints)d(0)d(0)d(0)d(0)
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
        public int SkinID { get; set; }
        public int Level { get; set; }
        public long Exp { get; set; }
        public int Str { get; set; }
        public int Dex { get; set; }
        public int Con { get; set; }
        public int Int { get; set; }
        public int Wit { get; set; }
        public int Men { get; set; }
        public int MaxHP { get; set; }
        public int CurHP { get; set; }
        public int MaxMP { get; set; }
        public int CurMP { get; set; }
        public int SP { get; set; }
        public int CurLoad { get; set; }
        public int MaxLoad { get; set; }
        public int PAtk { get; set; }
        public int AtkSpeed { get; set; }
        public int PDef { get; set; }
        public int PEvasion { get; set; }
        public int PAccuracy { get; set; }
        public int PCritical { get; set; }
        public int MAtk { get; set; }
        public int CastSpeed { get; set; }
        public int MDef { get; set; }
        public int MEvasion { get; set; }
        public int MAccuracy { get; set; }
        public int MCritical { get; set; }
        public int PvpFlag { get; set; }
        public int Karma { get; set; }
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
        public int Hair { get; set; }
        public int HairColor { get; set; }
        public int Face { get; set; }
        public int IsGM { get; set; }
        public string Title { get; set; }
        public int ClanID { get; set; }
        public int CrestID { get; set; }
        public int AllyID { get; set; }
        public int AllyCrestID { get; set; }
        public int Relation { get; set; }
        public byte MountType { get; set; }
        public byte PrivateStore { get; set; }
        public byte DwarvenCraft { get; set; }
        public int PK { get; set; }
        public int Pvp { get; set; }
        public List<short> Cubics { get; set; }
        public int AbnormalEffect { get; set; }
        public byte FlyingMounted { get; set; }
        public int ClanPrivileges { get; set; }
        public short RecommendLeft { get; set; }
        public short RecommendHave { get; set; }
        public int MountNpcID { get; set; }
        public short InventoryLimit { get; set; }
        public int ClassID { get; set; }
        public int MaxCP { get; set; }
        public int CurCP { get; set; }
        public byte IsMounted { get; set; }
        public byte Team { get; set; }
        public int ClanCrestLargeID { get; set; }
        public byte IsNoble { get; set; }
        public byte IsHero { get; set; }
        public byte IsFishing { get; set; }
        public int FishX { get; set; }
        public int FishY { get; set; }
        public int FishZ { get; set; }
        public int NameColor { get; set; }
        public byte IsRun { get; set; }
        public int PledgeClass { get; set; }
        public int PledgeType { get; set; }
        public int TitleColor { get; set; }
        public int CursedWeaponEquipID { get; set; }
        public int TransformationID { get; set; }
        public short AtkAttr { get; set; }
        public short AtkAttrValue { get; set; }
        public short DefFire { get; set; }
        public short DefWater { get; set; }
        public short DefWind { get; set; }
        public short DefEarth { get; set; }
        public short DefHoly { get; set; }
        public short DefDark { get; set; }
        public int AgathionID { get; set; }
        public int Fame { get; set; }
        public int Vitality { get; set; }

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
            this.SkinID = ReadInt();
            this.Level = ReadInt();
            this.Exp = ReadLong();
            this.Position += 2 * 4;
            this.Str = ReadInt();
            this.Dex = ReadInt();
            this.Con = ReadInt();
            this.Int = ReadInt();
            this.Wit = ReadInt();
            this.Men = ReadInt();
            this.MaxHP = ReadInt();
            this.CurHP = ReadInt();
            this.MaxMP = ReadInt();
            this.CurMP = ReadInt();
            this.SP = ReadInt();
            this.CurLoad = ReadInt();
            this.MaxLoad = ReadInt();
            this.Position += 90 * 4; // equip
            this.PAtk = ReadInt();
            this.AtkSpeed = ReadInt();
            this.PDef = ReadInt();
            this.PEvasion = ReadInt();
            this.PAccuracy = ReadInt();
            this.PCritical = ReadInt();
            this.MAtk = ReadInt();
            this.CastSpeed = ReadInt();
            this.Position += 4; // ??? atk speed copy
            this.MDef = ReadInt();
            this.MEvasion = ReadInt();
            this.MAccuracy = ReadInt();
            this.MCritical = ReadInt();
            this.PvpFlag = ReadInt();
            this.Karma = ReadInt();
            this.RunSpeed = ReadInt();
            this.WalkSpeed = ReadInt();
            this.SwimRunSpeed = ReadInt();
            this.SwimWalkSpeed = ReadInt();
            this.Position += 8;
            this.FlyRunSpeed = ReadInt();
            this.FlyWalkSpeed = ReadInt();
            this.MoveMult = ReadDouble();
            this.AtkSpeedMult = ReadDouble();
            this.CollisionRadius = ReadDouble();
            this.CollisionHeight = ReadDouble();
            this.Hair = ReadInt();
            this.HairColor = ReadInt();
            this.Face = ReadInt();
            this.IsGM = ReadInt();
            this.Title = ReadString();
            this.ClanID = ReadInt();
            this.CrestID = ReadInt();
            this.AllyID = ReadInt();
            this.AllyCrestID = ReadInt();
            this.Relation = ReadInt();
            this.MountType = ReadByte();
            this.PrivateStore = ReadByte();
            this.DwarvenCraft = ReadByte();
            this.PK = ReadInt();
            this.Pvp = ReadInt();
            int cubicsCount = ReadShort();
            this.Cubics = new List<short>(cubicsCount);
            for (int i = 0; i < cubicsCount; i++)
                this.Cubics.Add(ReadShort());
            this.Position++;
            this.FlyingMounted = ReadByte();
            this.ClanPrivileges = ReadInt();
            this.RecommendLeft = ReadShort();
            this.RecommendHave = ReadShort();
            this.MountNpcID = ReadInt();
            this.InventoryLimit = ReadShort();
            this.ClassID = ReadInt();
            this.Position += 4;
            this.MaxCP = ReadInt();
            this.CurCP = ReadInt();
            this.IsMounted = ReadByte();
            this.Team = ReadByte();
            this.ClanCrestLargeID = ReadInt();
            this.IsNoble = ReadByte();
            this.IsHero = ReadByte();
            this.IsFishing = ReadByte();
            this.FishX = ReadInt();
            this.FishY = ReadInt();
            this.FishZ = ReadInt();
            this.NameColor = ReadInt();
            this.IsRun = ReadByte();
            this.PledgeClass = ReadInt();
            this.PledgeType = ReadInt();
            this.TitleColor = ReadInt();
            this.CursedWeaponEquipID = ReadInt();
            this.TransformationID = ReadInt();
            this.AtkAttr = ReadShort();
            this.AtkAttrValue = ReadShort();
            this.DefFire = ReadShort();
            this.DefWater = ReadShort();
            this.DefWind = ReadShort();
            this.DefEarth = ReadShort();
            this.DefHoly = ReadShort();
            this.DefDark = ReadShort();
            this.AgathionID = ReadInt();
            this.Fame = ReadInt();
            this.Position += 4;
            this.Vitality = ReadInt();
        }
    }
}