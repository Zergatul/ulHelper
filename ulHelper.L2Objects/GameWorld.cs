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
        public L2Player User;
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
        public event EventHandler<L2AttackEventArgs> ObjectAttack;
        public event EventHandler<L2SkillUseEventArgs> ObjectSkillUse;

        L2CharacterEventArgs characterEventArgs = new L2CharacterEventArgs();
        L2NpcEventArgs npcEventArgs = new L2NpcEventArgs();
        L2LiveObjectEventArgs liveObjEventArgs = new L2LiveObjectEventArgs();
        L2AttackEventArgs attackEventArgs = new L2AttackEventArgs();
        L2SkillUseEventArgs skillUseEventArgs = new L2SkillUseEventArgs();

        Thread thread;
        volatile bool _needTerminate;
        List<L2LiveObject> movingObjects;

        public GameWorld()
        {
            User = new L2Player();
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
                        if (obj.MoveType == MoveType.MoveToLocation)
                        {
                            double time = (now - obj.StartMove).TotalSeconds;
                            double distance = time * obj.Speed;
                            if (distance > obj.MovingDistance)
                            {
                                needRemove.Add(obj);
                                obj.MoveType = MoveType.None;
                                obj.X = obj.MoveToX;
                                obj.Y = obj.MoveToY;
                            }
                            else
                            {
                                obj.X = obj.MoveFromX + obj.Cos * distance;
                                obj.Y = obj.MoveFromY + obj.Sin * distance;
                            }
                        }
                        else
                        {
                            obj.PrepareMoveToPawn();
                            double time = (now - obj.StartMove).TotalSeconds;
                            double distance = time * obj.Speed;
                            if (obj.MovingDistance - distance > obj.PawnDistance)
                            {
                                obj.X += obj.Cos * distance;
                                obj.Y += obj.Sin * distance;
                            }
                            else
                            {
                                distance = obj.MovingDistance - obj.PawnDistance;
                                if (distance > 1)
                                {
                                    obj.X += obj.Cos * distance;
                                    obj.Y += obj.Sin * distance;
                                }
                            }
                            obj.StartMove = now;
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
            if (pck is Attack)
            {
                ProcessAttack(pck as Attack);
                return;
            }
            if (pck is StopMove)
            {
                ProcessStopMove(pck as StopMove);
                return;
            }
            if (pck is MagicSkillUse)
            {
                ProcessMagicSkillUse(pck as MagicSkillUse);
                return;
            }
            if (pck is MoveToPawn)
            {
                ProcessMoveToPawn(pck as MoveToPawn);
                return;
            }
            if (pck is MyTargetSelected)
            {
                ProcessMyTargetSelected(pck as MyTargetSelected);
                return;
            }
        }

        private void ProcessMagicSkillUse(MagicSkillUse pck)
        {
            var obj = FindObject(pck.ObjectID);
            var target = FindObject(pck.TargetID);
            if (obj != null)
            {
                obj.X = pck.X;
                obj.Y = pck.Y;
                obj.Z = pck.Z;
            }
            if (target != null)
            {
                target.X = pck.TargetX;
                target.Y = pck.TargetY;
                target.Z = pck.TargetZ;
            }
            if (obj != null && target != null)
                OnObjectSkillUse(obj, target, pck.SkillID, pck.SkillLevel, pck.CastTimeMs, pck.ReuseTimeMs);
        }

        private void ProcessAttack(Attack pck)
        {
            var obj = FindObject(pck.AttackerObjectID);
            if (obj != null)
            {
                var targets = new List<AttackInfo>();
                var target = FindObject(pck.TargetObjectID);
                if (target != null)
                    targets.Add(new AttackInfo { Object = target, Damage = pck.Damage });
                foreach (var hit in pck.Hits)
                {
                    target = FindObject(hit.TargetID);
                    if (target != null)
                        targets.Add(new AttackInfo { Object = target, Damage = hit.Damage });
                }
                if (targets.Count > 0)
                    OnAttack(obj, targets);
            }
        }

        private void ProcessMoveToPawn(MoveToPawn pck)
        {
            var obj = FindObject(pck.CharID);
            var pawn = FindObject(pck.TargetID);
            if (obj != null && pawn != null)
            {
                lock (movingObjects)
                    if (!movingObjects.Contains(obj))
                        movingObjects.Add(obj);

                obj.MoveType = MoveType.MoveToPawn;
                obj.StartMove = DateTime.Now;
                obj.X = pck.X;
                obj.Y = pck.Y;
                obj.Z = pck.Z;
                pawn.X = pck.TargetX;
                pawn.Y = pck.TargetY;
                pawn.Z = pck.TargetZ;
                obj.Pawn = pawn;
                obj.PawnDistance = pck.Distance;
            }
        }

        private void ProcessStopMove(StopMove pck)
        {
            var obj = FindObject(pck.ObjectID);
            if (obj != null)
            {
                lock (movingObjects)
                    movingObjects.Remove(obj);
                obj.MoveType = MoveType.None;
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

                obj.MoveType = MoveType.MoveToLocation;
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
                    User.Target = ch;
            }
            if (ch != null)
            {
                OnPlayerTargetUpdate();
                return;
            }

            L2Npc npc;
            lock (Npcs)
            {
                npc = Npcs.FirstOrDefault(c => c.ObjectID == pck.ObjectID);
                if (npc != null)
                {
                    npc.Level = User.Level - pck.Color;
                    User.Target = npc;
                }
            }
            if (npc != null)
                OnPlayerTargetUpdate();
        }

        private void ProcessUserInfo(UserInfo pck)
        {
            User.Update(pck);
            OnPlayerUpdate();
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
                OnAddCharacter(ch);
            else
            {
                ch.Update(pck);
                if (User.Target == ch)
                    OnPlayerTargetUpdate();
                OnL2LiveObjectUpdate(ch);
            }
        }

        private void ProcessTargetUnselected(TargetUnselected pck)
        {
            if (pck.TargetID == User.ObjectID)
            {
                User.Target = null;
                OnPlayerTargetUpdate();
            }
        }

        private void ProcessTeletortToLocation(TeleportToLocation pck)
        {
            if (User.ObjectID == pck.TargetID)
            {
                lock (Characters)
                    Characters.Clear();
                lock (Npcs)
                    Npcs.Clear();
                OnDeleteAllObjects();
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
            if (obj == null && pck.ObjectID == User.ObjectID)
                obj = User;
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
                if (obj == User)
                    OnPlayerUpdate();
                else
                {
                    OnL2LiveObjectUpdate(obj);
                    if (User.Target == obj)
                        OnPlayerTargetUpdate();
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
                OnAddNpc(npc);
            else
            {
                npc.Update(pck);
                if (User.Target == npc)
                    OnPlayerTargetUpdate();
                OnL2LiveObjectUpdate(npc);
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
                OnDeleteCharacter(ch);
                if (User.Target == ch)
                {
                    User.Target = null;
                    OnPlayerTargetUpdate();
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
                OnDeleteNpc(npc);
                if (User.Target == npc)
                {
                    User.Target = null;
                    OnPlayerTargetUpdate();
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
                if (User.ObjectID == objectID)
                    obj = User;
            return obj;
        }

        void OnAddNpc(L2Npc npc)
        {
            var eh = AddNpc;
            if (eh != null)
            {
                npcEventArgs.Npc = npc;
                eh(this, npcEventArgs);
            }
        }

        void OnDeleteCharacter(L2Character ch)
        {
            lock (movingObjects)
                movingObjects.Remove(ch);
            var eh = DeleteCharacter;
            if (eh != null)
            {
                characterEventArgs.Character = ch;
                eh(this, characterEventArgs);
            }
        }

        void OnPlayerTargetUpdate()
        {
            var eh = PlayerTargetUpdate;
            if (eh != null)
                eh(this, EventArgs.Empty);
        }

        void OnDeleteNpc(L2Npc npc)
        {
            lock (movingObjects)
                movingObjects.Remove(npc);
            var eh = DeleteNpc;
            if (eh != null)
            {
                npcEventArgs.Npc = npc;
                eh(this, npcEventArgs);
            }
        }

        void OnL2LiveObjectUpdate(L2LiveObject obj)
        {
            var eh = L2LiveObjectUpdate;
            if (eh != null)
            {
                liveObjEventArgs.LiveObject = obj;
                eh(this, liveObjEventArgs);
            }
        }

        void OnPlayerUpdate()
        {
            var eh = PlayerUpdate;
            if (eh != null)
                eh(this, EventArgs.Empty);
        }

        void OnDeleteAllObjects()
        {
            lock (movingObjects)
                movingObjects.Clear();
            var eh = DeleteAllObjects;
            if (eh != null)
                eh(this, EventArgs.Empty);
        }

        void OnAddCharacter(L2Character ch)
        {
            var eh = AddCharacter;
            if (eh != null)
            {
                characterEventArgs.Character = ch;
                eh(this, characterEventArgs);
            }
        }

        void OnAttack(L2LiveObject attacker, List<AttackInfo> targets)
        {
            var eh = ObjectAttack;
            if (eh != null)
            {
                attackEventArgs.Attacker = attacker;
                attackEventArgs.Targets = targets;
                eh(this, attackEventArgs);
            }
        }

        void OnObjectSkillUse(L2LiveObject obj, L2LiveObject target, int skillID, int skillLevel, int castTimeMs, int reuseTimeMs)
        {
            var eh = ObjectSkillUse;
            if (eh != null)
            {
                skillUseEventArgs.Object = obj;
                skillUseEventArgs.Target = target;
                skillUseEventArgs.SkillID = skillID;
                skillUseEventArgs.SkillLevel = skillLevel;
                skillUseEventArgs.CastTimeMs = castTimeMs;
                skillUseEventArgs.ReuseTimeMs = reuseTimeMs;
                eh(this, skillUseEventArgs);
            }
        }

        public void ___OnAddCharacter()
        {
            characterEventArgs.Character = Characters.First();
            if (AddCharacter != null)
                AddCharacter(this, characterEventArgs);
        }

        #region Dispose pattern

        private bool _disposed;

        public void Dispose()
        {
            if (!_disposed)
            {
                /*_needTerminate = true;
                thread.Join();*/
                thread.Abort();
                _disposed = true;
            }
        }

        #endregion
    }
}