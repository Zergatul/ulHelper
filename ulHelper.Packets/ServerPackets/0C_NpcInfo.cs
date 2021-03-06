﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ulHelper.Packets
{
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
        public int Heading { get; set; }
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
        public int RHand { get; set; }
        public int Chest { get; set; }
        public int LHand { get; set; }
        public byte IsNameAbove { get; set; }
        public byte IsRun { get; set; }
        public byte InCombat { get; set; }
        public byte IsAlikeDead { get; set; }
        public byte ShowSpawnAnim { get; set; }
        public string PetName { get; set; }
        public string OwnerName { get; set; }
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
            this.Heading = ReadInt();
            this.Position += 4;
            this.CastSpeed = ReadInt();
            this.AtkSpeed = ReadInt();
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
            this.RHand = ReadInt();
            this.Chest = ReadInt();
            this.LHand = ReadInt();
            this.IsNameAbove = ReadByte();
            this.IsRun = ReadByte();
            this.InCombat = ReadByte();
            this.IsAlikeDead = ReadByte();
            this.ShowSpawnAnim = ReadByte();
            this.Position += 4;
            this.PetName = ReadString();
            this.Position += 4;
            this.OwnerName = ReadString();
            this.Position += 4 * 9;
            this.Position += 16; // coll double data
            this.Position += 4 * 7;
            this.Position -= 4;
            this.CurHP = ReadInt();
            this.MaxHP = ReadInt();
            this.CurMP = ReadInt();
            this.MaxMP = ReadInt();
        }
    }
}