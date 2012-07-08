using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ulHelper.L2Objects
{
    public class L2LiveObject : L2NonStaticObject
    {
        public bool Dead { get; set; }
        public bool Running { get; set; }
        public int CurHP { get; set; }
        public int MaxHP { get; set; }
        public int CurMP { get; set; }
        public int MaxMP { get; set; }
        public int AbnormalEffect { get; set; }
        public string Title { get; set; }
        public int Speed { get; set; }
        public int CastSpeed { get; set; }
        public int AtkSpeed { get; set; }
        public int Level { get; set; }
        public L2LiveObject Target { get; set; }

        internal MoveType MoveType { get; set; }
        internal int MoveFromX { get; set; }
        internal int MoveFromY { get; set; }
        internal int MoveToX { get; set; }
        internal int MoveToY { get; set; }
        internal L2LiveObject Pawn { get; set; }
        internal double Cos { get; set; }
        internal double Sin { get; set; }
        internal double MovingDistance { get; set; }
        internal int PawnDistance { get; set; }
        internal DateTime StartMove { get; set; }

        public L2LiveObject()
        {
            
        }

        internal void CalcMoveData()
        {
            MovingDistance = Math.Sqrt(
                (MoveFromX - MoveToX) * (MoveFromX - MoveToX) + (MoveFromY - MoveToY) * (MoveFromY - MoveToY));
            this.Cos = (MoveToX - MoveFromX) / MovingDistance;
            this.Sin = (MoveToY - MoveFromY) / MovingDistance;
            this.StartMove = DateTime.Now;
        }

        internal void PrepareMoveToPawn()
        {
            MovingDistance = Math.Sqrt((X - Pawn.X) * (X - Pawn.X) + (Y - Pawn.Y) * (Y - Pawn.Y));
            this.Cos = (Pawn.X - X) / MovingDistance;
            this.Sin = (Pawn.Y - Y) / MovingDistance;
        }
    }
}