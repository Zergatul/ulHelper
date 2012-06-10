using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ulHelper.Packets;
using ulHelper.L2Objects.Events;
using System.Threading;

namespace ulHelper.L2Objects
{
    public class GameWorld : IDisposable
    {
        public L2Player Player;
        public List<L2Character> Characters;
        public List<L2Npc> Npcs;
        public List<ItemList.InventoryItem> Inventory;

        public event EventHandler<L2CharacterEventArgs> AddCharacter;
        public event EventHandler<L2NpcEventArgs> AddNpc;
        public event EventHandler<L2CharacterEventArgs> DeleteCharacter;
        public event EventHandler<L2NpcEventArgs> DeleteNpc;
        public event EventHandler DeleteAllObjects;
        public event EventHandler PlayerUpdate;
        public event EventHandler PlayerTargetUpdate;
        public event EventHandler<L2LiveObjectEventArgs> L2LiveObjectUpdate;

        L2CharacterEventArgs characterEventArgs = new L2CharacterEventArgs();
        L2NpcEventArgs npcEventArgs = new L2NpcEventArgs();
        L2LiveObjectEventArgs liveObjEventArgs = new L2LiveObjectEventArgs();

        Thread thread;
        bool _needTerminate;
        List<L2LiveObject> movingObjects;

        public GameWorld()
        {
            Player = new L2Player();
            Characters = new List<L2Character>();
            Npcs = new List<L2Npc>();
            _needTerminate = false;
            thread = new Thread(MoveObjects);
            thread.Start();
        }

        void MoveObjects()
        {
            movingObjects = new List<L2LiveObject>();
            var needRemove = new List<L2LiveObject>();
            while (!_needTerminate)
            {
                var now = DateTime.Now;
                needRemove.Clear();
                lock (movingObjects)
                {
                    foreach (var obj in movingObjects)
                    {
                        double time = (now - obj.StartMove).TotalSeconds;
                        double distance = time * obj.Speed;
                        if (distance > obj.MovingDistance)
                        {
                            needRemove.Add(obj);
                            obj.IsMoving = false;
                            obj.X = obj.MoveToX;
                            obj.Y = obj.MoveToY;
                        }
                        else
                        {
                            obj.X = obj.MoveFromX + (int)Math.Round(obj.Cos * distance);
                            obj.Y = obj.MoveFromY + (int)Math.Round(obj.Sin * distance);
                        }
                    }
                    foreach (var obj in needRemove)
                        movingObjects.Remove(obj);
                }
                Thread.Sleep(20);
            }
        }

        public void Update(ServerPacket pck)
        {
            if (pck is DeleteObject)
            {
                ProcessDeleteObject(pck as DeleteObject);
                return;
            }
            if (pck is NpcInfo)
            {
                ProcessNpcInfo(pck as NpcInfo);
                return;
            }
            if (pck is StatusUpdate)
            {
                ProcessStatusUpdate(pck as StatusUpdate);
                return;
            }
            if (pck is TeleportToLocation)
            {
                ProcessTeletortToLocation(pck as TeleportToLocation);
                return;
            }
            if (pck is TargetUnselected)
            {
                ProcessTargetUnselected(pck as TargetUnselected);
                return;
            }
            if (pck is MoveToLocation)
            {
                ProcessMoveToLocation(pck as MoveToLocation);
                return;
            }
            if (pck is CharInfo)
            {
                ProcessCharInfo(pck as CharInfo);
                return;
            }
            if (pck is UserInfo)
            {
                ProcessUserInfo(pck as UserInfo);
                return;
            }
            if (pck is StopMove)
            {
                ProcessStopMove(pck as StopMove);
                return;
            }
            if (pck is MyTargetSelected)
            {
                ProcessMyTargetSelected(pck as MyTargetSelected);
                return;
            }
        }

        private void ProcessStopMove(StopMove pck)
        {
            var obj = FindObject(pck.ObjectID);
            if (obj != null)
            {
                lock (movingObjects)
                    movingObjects.Remove(obj);
                obj.IsMoving = false;
                obj.X = pck.X;
                obj.Y = pck.Y;
                obj.Z = pck.Z;
            }
        }

        private void ProcessMoveToLocation(MoveToLocation pck)
        {
            var obj = FindObject(pck.ObjectID);
            if (obj != null)
            {
                lock (movingObjects)
                    if (!movingObjects.Contains(obj))
                        movingObjects.Add(obj);

                obj.IsMoving = true;
                obj.MoveFromX = pck.CurX;
                obj.MoveFromY = pck.CurY;
                obj.MoveToX = pck.DestX;
                obj.MoveToY = pck.DestY;
                obj.CalcMoveData();

                obj.X = pck.CurX;
                obj.Y = pck.CurY;
                obj.Z = pck.CurZ;
            }
        }

        private void ProcessMyTargetSelected(MyTargetSelected pck)
        {
            L2Character ch;
            lock (Characters)
            {
                ch = Characters.FirstOrDefault(c => c.ObjectID == pck.ObjectID);
                if (ch != null)
                    Player.Target = ch;
            }
            if (ch != null)
            {
                PerformPlayerTargetUpdate();
                return;
            }

            L2Npc npc;
            lock (Npcs)
            {
                npc = Npcs.FirstOrDefault(c => c.ObjectID == pck.ObjectID);
                if (npc != null)
                {
                    npc.Level = Player.Level - pck.Color;
                    Player.Target = npc;
                }
            }
            if (npc != null)
                PerformPlayerTargetUpdate();
        }

        private void ProcessUserInfo(UserInfo pck)
        {
            Player.Update(pck);
            PerformPlayerUpdate();
        }

        private void ProcessCharInfo(CharInfo pck)
        {
            L2Character ch;
            bool added = false;
            lock (Characters)
            {
                ch = Characters.FirstOrDefault(c => c.ObjectID == pck.ObjectID);
                if (ch == null)
                {
                    ch = new L2Character(pck);
                    Characters.Add(ch);
                    added = true;
                }
            }
            if (added)
                PerformAddCharacter(ch);
            else
            {
                ch.Update(pck);
                if (Player.Target == ch)
                    PerformPlayerTargetUpdate();
                PerformL2LiveObjectUpdate(ch);
            }
        }

        private void ProcessTargetUnselected(TargetUnselected pck)
        {
            if (pck.TargetID == Player.ObjectID)
            {
                Player.Target = null;
                PerformPlayerTargetUpdate();
            }
        }

        private void ProcessTeletortToLocation(TeleportToLocation pck)
        {
            if (Player.ObjectID == pck.TargetID)
            {
                lock (Characters)
                    Characters.Clear();
                lock (Npcs)
                    Npcs.Clear();
                PerformDeleteAllObjects();
            }
        }

        private void ProcessStatusUpdate(StatusUpdate pck)
        {
            L2LiveObject obj = null;
            lock (Characters)
                obj = Characters.FirstOrDefault(c => c.ObjectID == pck.ObjectID);
            if (obj == null)
                lock (Npcs)
                    obj = Npcs.FirstOrDefault(c => c.ObjectID == pck.ObjectID);
            if (obj == null && pck.ObjectID == Player.ObjectID)
                obj = Player;
            if (obj != null)
            {
                foreach (var attr in pck.Attributes)
                    switch (attr.Attribute)
                    {
                        case StatusUpdate.Attribute.CurHP:
                            obj.CurHP = attr.Value;
                            break;
                        case StatusUpdate.Attribute.MaxHP:
                            obj.MaxHP = attr.Value;
                            break;
                        case StatusUpdate.Attribute.Level:
                            obj.Level = attr.Value;
                            break;
                        case StatusUpdate.Attribute.CurMP:
                            obj.CurMP = attr.Value;
                            break;
                        case StatusUpdate.Attribute.MaxMP:
                            obj.MaxMP = attr.Value;
                            break;
                        case StatusUpdate.Attribute.CurCP:
                            (obj as L2Character).CurCP = attr.Value;
                            break;
                        default:
                            break;
                    }
                if (obj == Player)
                    PerformPlayerUpdate();
                else
                {
                    PerformL2LiveObjectUpdate(obj);
                    if (Player.Target == obj)
                        PerformPlayerTargetUpdate();
                }
            }
        }

        private void ProcessNpcInfo(NpcInfo pck)
        {
            L2Npc npc;
            bool added = false;
            lock (Npcs)
            {
                npc = Npcs.FirstOrDefault(c => c.ObjectID == pck.ObjectID);
                if (npc == null)
                {
                    npc = new L2Npc(pck);
                    Npcs.Add(npc);
                    added = true;
                }
            }
            if (added)
                PerformAddNpc(npc);
            else
            {
                npc.Update(pck);
                if (Player.Target == npc)
                    PerformPlayerTargetUpdate();
                PerformL2LiveObjectUpdate(npc);
            }
        }

        private void ProcessDeleteObject(DeleteObject pck)
        {
            L2Character ch;
            lock (Characters)
            {
                ch = Characters.FirstOrDefault(c => c.ObjectID == pck.ObjectID);
                if (ch != null)
                    Characters.Remove(ch);
            }
            if (ch != null)
            {
                PerformDeleteCharacter(ch);
                if (Player.Target == ch)
                {
                    Player.Target = null;
                    PerformPlayerTargetUpdate();
                }
                return;
            }
            L2Npc npc;
            lock (Npcs)
            {
                npc = Npcs.FirstOrDefault(c => c.ObjectID == pck.ObjectID);
                if (npc != null)
                    Npcs.Remove(npc);
            }
            if (npc != null)
            {
                PerformDeleteNpc(npc);
                if (Player.Target == npc)
                {
                    Player.Target = null;
                    PerformPlayerTargetUpdate();
                }
            }
        }

        L2LiveObject FindObject(int objectID)
        {
            L2LiveObject obj;
            lock (Characters)
                obj = Characters.FirstOrDefault(c => c.ObjectID == objectID);
            if (obj == null)
                lock (Npcs)
                    obj = Npcs.FirstOrDefault(c => c.ObjectID == objectID);
            if (obj == null)
                if (Player.ObjectID == objectID)
                    obj = Player;
            return obj;
        }

        void PerformAddNpc(L2Npc npc)
        {
            if (AddNpc != null)
            {
                npcEventArgs.Npc = npc;
                AddNpc(this, npcEventArgs);
            }
        }

        void PerformDeleteCharacter(L2Character ch)
        {
            lock (movingObjects)
                movingObjects.Remove(ch);
            if (DeleteCharacter != null)
            {
                characterEventArgs.Character = ch;
                DeleteCharacter(this, characterEventArgs);
            }
        }

        void PerformPlayerTargetUpdate()
        {
            if (PlayerTargetUpdate != null)
                PlayerTargetUpdate(this, EventArgs.Empty);
        }

        void PerformDeleteNpc(L2Npc npc)
        {
            lock (movingObjects)
                movingObjects.Remove(npc);
            if (DeleteNpc != null)
            {
                npcEventArgs.Npc = npc;
                DeleteNpc(this, npcEventArgs);
            }
        }

        void PerformL2LiveObjectUpdate(L2LiveObject obj)
        {
            if (L2LiveObjectUpdate != null)
            {
                liveObjEventArgs.LiveObject = obj;
                L2LiveObjectUpdate(this, liveObjEventArgs);
            }
        }

        void PerformPlayerUpdate()
        {
            if (PlayerUpdate != null)
                PlayerUpdate(this, EventArgs.Empty);
        }

        void PerformDeleteAllObjects()
        {
            lock (movingObjects)
                movingObjects.Clear();
            if (DeleteAllObjects != null)
                DeleteAllObjects(this, EventArgs.Empty);
        }

        void PerformAddCharacter(L2Character ch)
        {
            if (AddCharacter != null)
            {
                characterEventArgs.Character = ch;
                AddCharacter(this, characterEventArgs);
            }
        }

        public void ___PerformAddCharacter()
        {
            characterEventArgs.Character = Characters.First();
            if (AddCharacter != null)
                AddCharacter(this, characterEventArgs);
        }

        #region Dispose pattern

        private bool _disposed;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _needTerminate = true;
                    thread.Join();
                }
                _disposed = true;
            }
        }

        #endregion
    }
}