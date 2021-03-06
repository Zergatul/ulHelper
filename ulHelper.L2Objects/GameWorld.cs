﻿using System;
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
        public L2User User;
        public List<L2Character> Characters;
        public List<L2Npc> Npcs;
        public List<ItemList.InventoryItem> Inventory;
        public List<L2DropItem> DropItems;

        public event EventHandler<L2CharacterEventArgs> AddCharacter;
        public event EventHandler<L2DropEventArgs> AddDrop;
        public event EventHandler<L2NpcEventArgs> AddNpc;
        public event EventHandler<L2CharacterEventArgs> DeleteCharacter;
        public event EventHandler<L2DropEventArgs> DeleteDrop;
        public event EventHandler<L2NpcEventArgs> DeleteNpc;
        public event EventHandler DeleteAllObjects;
        public event EventHandler UserUpdate;
        public event EventHandler UserTargetUpdate;
        public event EventHandler<L2LiveObjectEventArgs> L2LiveObjectUpdate;
        public event EventHandler<L2AttackEventArgs> ObjectAttack;
        public event EventHandler<L2SkillUseEventArgs> ObjectSkillUse;

        L2CharacterEventArgs characterEventArgs = new L2CharacterEventArgs();
        L2DropEventArgs dropEventArgs = new L2DropEventArgs();
        L2NpcEventArgs npcEventArgs = new L2NpcEventArgs();
        L2LiveObjectEventArgs liveObjEventArgs = new L2LiveObjectEventArgs();
        L2AttackEventArgs attackEventArgs = new L2AttackEventArgs();
        L2SkillUseEventArgs skillUseEventArgs = new L2SkillUseEventArgs();

        Thread thread;
        volatile bool _needTerminate;
        List<L2LiveObject> movingObjects;

        public GameWorld()
        {
            User = new L2User();
            Characters = new List<L2Character>();
            Npcs = new List<L2Npc>();
            DropItems = new List<L2DropItem>();
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
            if (pck is SpawnItem)
            {
                ProcessSpawnItem(pck as SpawnItem);
                return;
            }
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
            if (pck is DropItem)
            {
                ProcessDropItem(pck as DropItem);
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
            if (pck is PartySmallWindowUpdate)
            {
                ProcessPartySmallWindowUpdate(pck as PartySmallWindowUpdate);
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

        private void ProcessDropItem(DropItem pck)
        {
            AddDropToList(new L2DropItem(pck));
        }

        private void ProcessSpawnItem(SpawnItem pck)
        {
            AddDropToList(new L2DropItem(pck));
        }

        private void ProcessPartySmallWindowUpdate(PartySmallWindowUpdate pck)
        {
            var ch = FindCharacter(pck.ObjectID);
            if (ch != null)
            {
                ch.Update(pck);
                OnL2LiveObjectUpdate(ch);
            }
            else
                AddCharacterToList(new L2Character(pck));
        }

        private void ProcessMagicSkillUse(MagicSkillUse pck)
        {
            var obj = FindLiveObject(pck.ObjectID);
            var target = FindLiveObject(pck.TargetID);
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
            var obj = FindLiveObject(pck.AttackerObjectID);
            if (obj != null)
            {
                var targets = new List<AttackInfo>();
                var target = FindLiveObject(pck.TargetObjectID);
                if (target != null)
                    targets.Add(new AttackInfo { Object = target, Damage = pck.Damage });
                foreach (var hit in pck.Hits)
                {
                    target = FindLiveObject(hit.TargetID);
                    if (target != null)
                        targets.Add(new AttackInfo { Object = target, Damage = hit.Damage });
                }
                if (targets.Count > 0)
                    OnAttack(obj, targets);
            }
        }

        private void ProcessMoveToPawn(MoveToPawn pck)
        {
            var obj = FindLiveObject(pck.CharID);
            var pawn = FindLiveObject(pck.TargetID);
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
            var obj = FindLiveObject(pck.ObjectID);
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
            var obj = FindLiveObject(pck.ObjectID);
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
            var obj = FindLiveObject(pck.ObjectID);
            if (obj != null)
            {
                User.Target = obj;
                if (obj is L2Npc)
                    (obj as L2Npc).Level = User.Level - pck.Color;
                OnUserTargetUpdate();
            }
        }

        private void ProcessUserInfo(UserInfo pck)
        {
            User.Update(pck);
            OnUserUpdate();
        }

        private void ProcessCharInfo(CharInfo pck)
        {
            L2Character ch = FindCharacter(pck.ObjectID);
            if (ch != null)
            {
                ch.Update(pck);
                OnL2LiveObjectUpdate(ch);
                if (User.Target == ch)
                    OnUserTargetUpdate();
            }
            else
                AddCharacterToList(new L2Character(pck));
        }

        private void ProcessTargetUnselected(TargetUnselected pck)
        {
            if (pck.TargetID == User.ObjectID)
            {
                User.Target = null;
                OnUserTargetUpdate();
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
            L2LiveObject obj = FindLiveObject(pck.ObjectID);
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
                    OnUserUpdate();
                else
                {
                    OnL2LiveObjectUpdate(obj);
                    if (User.Target == obj)
                        OnUserTargetUpdate();
                }
            }
        }

        private void ProcessNpcInfo(NpcInfo pck)
        {
            L2Npc npc = FindNpc(pck.ObjectID);
            if (npc != null)
            {
                npc.Update(pck);
                OnL2LiveObjectUpdate(npc);
                if (User.Target == npc)
                    OnUserTargetUpdate();
            }
            else
                AddNpcToList(new L2Npc(pck));
        }

        private void ProcessDeleteObject(DeleteObject pck)
        {
            var ch = FindCharacter(pck.ObjectID);
            if (ch != null)
            {
                DeleteCharacterFromList(ch);
                return;
            }

            var npc = FindNpc(pck.ObjectID);
            if (npc != null)
            {
                DeleteNpcFromList(npc);
                return;
            }

            var drop = FindDrop(pck.ObjectID);
            if (drop != null)
            {
                DeleteDropFromList(drop);
                return;
            }
        }

        L2LiveObject FindLiveObject(int objectID)
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

        L2Object FindObject(int objectID)
        {
            L2Object obj;
            lock (Characters)
                obj = Characters.FirstOrDefault(c => c.ObjectID == objectID);
            if (obj == null)
                lock (Npcs)
                    obj = Npcs.FirstOrDefault(n => n.ObjectID == objectID);
            if (obj == null)
                lock (DropItems)
                    obj = DropItems.FirstOrDefault(d => d.ObjectID == objectID);
            if (obj == null)
                if (User.ObjectID == objectID)
                    obj = User;
            return obj;
        }

        L2Character FindCharacter(int objectID)
        {
            L2Character ch;
            lock (Characters)
                ch = Characters.FirstOrDefault(c => c.ObjectID == objectID);
            return ch;
        }

        L2Npc FindNpc(int objectID)
        {
            L2Npc npc;
            lock (Npcs)
                npc = Npcs.FirstOrDefault(n => n.ObjectID == objectID);
            return npc;
        }

        L2DropItem FindDrop(int objectID)
        {
            L2DropItem drop;
            lock (DropItems)
                drop = DropItems.FirstOrDefault(d => d.ObjectID == objectID);
            return drop;
        }

        void AddCharacterToList(L2Character ch)
        {
            lock (Characters)
                Characters.Add(ch);
            OnAddCharacter(ch);
        }

        void AddNpcToList(L2Npc npc)
        {
            lock (Npcs)
                Npcs.Add(npc);
            OnAddNpc(npc);
        }

        void AddDropToList(L2DropItem drop)
        {
            lock (DropItems)
                DropItems.Add(drop);
            OnAddDrop(drop);
        }

        void DeleteCharacterFromList(L2Character ch)
        {
            lock (Characters)
                Characters.Remove(ch);
            OnDeleteCharacter(ch);
            if (User.Target == ch)
            {
                User.Target = null;
                OnUserTargetUpdate();
            }
        }

        void DeleteNpcFromList(L2Npc npc)
        {
            lock (Npcs)
                Npcs.Remove(npc);
            OnDeleteNpc(npc);
            if (User.Target == npc)
            {
                User.Target = null;
                OnUserTargetUpdate();
            }
        }

        void DeleteDropFromList(L2DropItem drop)
        {
            lock (DropItems)
                DropItems.Remove(drop);
            OnDeleteDrop(drop);
        }

        void OnAddCharacter(L2Character ch)
        {
            if (AddCharacter != null)
            {
                characterEventArgs.Character = ch;
                AddCharacter(this, characterEventArgs);
            }
        }

        void OnAddNpc(L2Npc npc)
        {
            if (AddNpc != null)
            {
                npcEventArgs.Npc = npc;
                AddNpc(this, npcEventArgs);
            }
        }

        void OnAddDrop(L2DropItem drop)
        {
            if (AddDrop != null)
            {
                dropEventArgs.Drop = drop;
                AddDrop(this, dropEventArgs);
            }
        }

        void OnDeleteCharacter(L2Character ch)
        {
            lock (movingObjects)
                movingObjects.Remove(ch);
            if (DeleteCharacter != null)
            {
                characterEventArgs.Character = ch;
                DeleteCharacter(this, characterEventArgs);
            }
        }

        void OnUserTargetUpdate()
        {
            if (UserTargetUpdate != null)
                UserTargetUpdate(this, EventArgs.Empty);
        }

        void OnDeleteNpc(L2Npc npc)
        {
            lock (movingObjects)
                movingObjects.Remove(npc);
            if (DeleteNpc != null)
            {
                npcEventArgs.Npc = npc;
                DeleteNpc(this, npcEventArgs);
            }
        }

        void OnDeleteDrop(L2DropItem drop)
        {
            if (DeleteDrop != null)
            {
                dropEventArgs.Drop = drop;
                DeleteDrop(this, dropEventArgs);
            }
        }

        void OnL2LiveObjectUpdate(L2LiveObject obj)
        {
            if (L2LiveObjectUpdate != null)
            {
                liveObjEventArgs.LiveObject = obj;
                L2LiveObjectUpdate(this, liveObjEventArgs);
            }
        }

        void OnUserUpdate()
        {
            if (UserUpdate != null)
                UserUpdate(this, EventArgs.Empty);
        }

        void OnDeleteAllObjects()
        {
            lock (movingObjects)
                movingObjects.Clear();
            if (DeleteAllObjects != null)
                DeleteAllObjects(this, EventArgs.Empty);
        }        

        void OnAttack(L2LiveObject attacker, List<AttackInfo> targets)
        {
            if (ObjectAttack != null)
            {
                attackEventArgs.Attacker = attacker;
                attackEventArgs.Targets = targets;
                ObjectAttack(this, attackEventArgs);
            }
        }

        void OnObjectSkillUse(L2LiveObject obj, L2LiveObject target, int skillID, int skillLevel, int castTimeMs, int reuseTimeMs)
        {
            if (ObjectSkillUse != null)
            {
                skillUseEventArgs.Object = obj;
                skillUseEventArgs.Target = target;
                skillUseEventArgs.SkillID = skillID;
                skillUseEventArgs.SkillLevel = skillLevel;
                skillUseEventArgs.CastTimeMs = castTimeMs;
                skillUseEventArgs.ReuseTimeMs = reuseTimeMs;
                ObjectSkillUse(this, skillUseEventArgs);
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