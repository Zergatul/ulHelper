using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ulHelper.Packets
{
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
        public double ExpPerc { get; set; }
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
        public byte CanCrystalize { get; set; }
        public int PK { get; set; }
        public int Pvp { get; set; }
        public List<short> Cubics { get; set; }
        public byte IsPartyRoom { get; set; }
        public byte FlyingMounted { get; set; }
        public int ClanPrivileges { get; set; }
        public short RecommendLeft { get; set; }
        public short RecommendHave { get; set; }
        public int MountNpcID { get; set; }
        public short InventoryLimit { get; set; }
        public int ClassID { get; set; }
        public int MaxCP { get; set; }
        public int CurCP { get; set; }
        public byte Enchant { get; set; }
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
        public int CursedWeaponLvl { get; set; }
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
        public int AllowHBMap { get; set; }
        public int Vitality { get; set; }
        public int Abnormal { get; set; }

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
            this.ExpPerc = ReadDouble();
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
            this.Position += 4; // atk speed copy
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
            this.CanCrystalize = ReadByte();
            this.PK = ReadInt();
            this.Pvp = ReadInt();
            int cubicsCount = ReadShort();
            this.Cubics = new List<short>(cubicsCount);
            for (int i = 0; i < cubicsCount; i++)
                this.Cubics.Add(ReadShort());
            this.IsPartyRoom = ReadByte();
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
            this.Enchant = ReadByte();
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
            this.CursedWeaponLvl = ReadInt();
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
            this.AllowHBMap = ReadInt();
            this.Vitality = ReadInt();
            this.Abnormal = ReadInt();
        }
    }
}