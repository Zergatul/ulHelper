using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ulHelper.Packets
{
    /*
    0C=NpcInfo:
     * d(ObjID)d(NpcId:Get.NpcId)d(IsAttackable)d(X)d(Y)d(Z)d(Heading)
     * d(0)d(CastSpd)d(AtkSpd)d(RunSpd)d(WalkSpd)d(SwimRunSpd)d(SwimWalkSpd)
     * d(FlRunSpd)d(FlWalkSpd)d(FlyRunSpd)d(FlyWalkSpd)
     * f(MoveMult)f(ASpdMult)f(CollisionRadius)f(CollisionHeight)
     * d(RHand:Get.F0)d(Chest:Get.F0)d(LHand:Get.F0)
     * c(nameabove)c(isRunning)c(isInCombat)c(isALikeDead)c(isSummoned)
     * s(Name)s(Title)
     * d(TitleColor:Get.FCol)d(pvpFlag)d(Karma)d(AbnormalEffect)d(clanID)d(crestID)d(allyID)d(allyCrest)c(isFlying)c(Team)f(CollisionRadius)f(CollisionHeight)d(enchEffects)d(isFlying)d(0)d(form)c(isShowName)c(isShowName)d(SpecEffects)d(dispEffect)
    */
    /// <summary>
    /// ID = OC
    /// </summary>
    public class NpcInfo : ServerPacket
    {
        public int ObjectID { get; set; }
        public int NpcID { get; set; }
        public int Attackable { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public int CurHP { get; set; }
        public int MaxHP { get; set; }
        public int CurMP { get; set; }
        public int MaxMP { get; set; }

        public NpcInfo(ServerPacket pck)
            : base(pck)
        {
            this.ObjectID = ReadInt();
            this.NpcID = ReadInt() - 1000000;
            this.Attackable = ReadInt();
            this.X = ReadInt();
            this.Y = ReadInt();
            this.Z = ReadInt();
            this.Position += 12 * 4;
            this.Position += 4 * 8;
            this.Position += 3 * 4;
            this.Position += 5;
            this.Name = ReadString();
            this.Title = ReadString();
            this.Position = Data.Length - 13 - 4 * 4;
            this.CurHP = ReadInt();
            this.MaxHP = ReadInt();
            this.CurMP = ReadInt();
            this.MaxMP = ReadInt();
        }
    }
}