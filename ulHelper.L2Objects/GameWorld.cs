using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ulHelper.Packets;

namespace ulHelper.L2Objects
{
    public class GameWorld
    {
        public L2Player Player;
        public List<L2Character> Characters;
        public List<L2Npc> Npcs;
        public List<ItemList.InventoryItem> Inventory;

        public event EventHandler AddCharacter;
        public event EventHandler AddNpc;
        public event EventHandler DeleteCharacter;
        public event EventHandler DeleteNpc;
        public event EventHandler PlayerStatusUpdate;
        public event EventHandler PlayerTargetUpdate;

        public GameWorld()
        {
            Player = new L2Player();
            Characters = new List<L2Character>();
            Npcs = new List<L2Npc>();
        }

        public void Update(ServerPacket packet)
        {
            #region DeleteObject
            if (packet is DeleteObject)
            {
                var pck = packet as DeleteObject;
                lock (Characters)
                {
                    var ch = Characters.FirstOrDefault(c => c.ObjectID == pck.ObjectID);
                    if (ch != null)
                    {
                        ch.Deleted = true;
                        Characters.Remove(ch);
                        if (DeleteCharacter != null)
                            DeleteCharacter(this, EventArgs.Empty);
                        return;
                    }
                }
                lock (Npcs)
                {
                    var npc = Npcs.FirstOrDefault(c => c.ObjectID == pck.ObjectID);
                    if (npc != null)
                    {
                        npc.Deleted = true;
                        Npcs.Remove(npc);
                        if (DeleteNpc != null)
                            DeleteNpc(this, EventArgs.Empty);
                    }
                }
                return;
            }
            #endregion
            #region NpcInfo
            if (packet is NpcInfo)
            {
                var pck = packet as NpcInfo;
                lock (Npcs)
                {
                    var npc = Npcs.FirstOrDefault(c => c.ObjectID == pck.ObjectID);
                    if (npc == null)
                    {
                        Npcs.Add(new L2Npc(pck));
                        if (AddNpc != null)
                            AddNpc(this, EventArgs.Empty);
                    }
                    else
                        npc.Update(pck);
                }
                return;
            }
            #endregion
            #region StatusUpdate
            if (packet is StatusUpdate)
            {
                var pck = packet as StatusUpdate;
                L2LiveObject obj = null;
                lock (Characters)
                    obj = Characters.FirstOrDefault(c => c.ObjectID == pck.ObjectID);
                if (obj == null)
                    lock (Npcs)
                        obj = Npcs.FirstOrDefault(c => c.ObjectID == pck.ObjectID);
                lock (Player)
                    if (obj == null && pck.ObjectID == Player.ObjectID)
                        obj = Player;
                if (obj != null)
                {
                    foreach (var attr in pck.Attributes)
                    {
                        if (attr.Attribute == StatusUpdate.Attribute.CurHP)
                            obj.CurHP = attr.Value;
                        if (attr.Attribute == StatusUpdate.Attribute.MaxHP)
                            obj.MaxHP = attr.Value;
                        if (attr.Attribute == StatusUpdate.Attribute.Level)
                            obj.Level = attr.Value;
                    }
                }
                if (obj == Player)
                    if (PlayerStatusUpdate != null)
                        PlayerStatusUpdate(this, EventArgs.Empty);
                return;
            }
            #endregion
            #region ItemList
            if (packet is ItemList)
                Inventory = (packet as ItemList).Inventory;
            #endregion
            #region TeleportToLocation
            if (packet is TeleportToLocation)
            {
                var pck = packet as TeleportToLocation;
                lock (Player)
                    if (Player.ObjectID == pck.TargetID)
                        lock (Characters)
                            Characters.Clear();
                return;
            }
            #endregion
            #region TargetUnselected
            if (packet is TargetUnselected)
            {
                var pck = packet as TargetUnselected;
                lock (Player)
                    if (pck.TargetID == Player.ObjectID)
                        Player.Target = null;
                return;
            }
            #endregion
            #region CharInfo
            if (packet is CharInfo)
            {
                var pck = packet as CharInfo;
                lock (Characters)
                {
                    var ch = Characters.FirstOrDefault(c => c.ObjectID == pck.ObjectID);
                    if (ch == null)
                        Characters.Add(new L2Character(pck));
                    else
                        ch.Update(pck);
                }
                return;
            }
            #endregion
            #region UserInfo
            if (packet is UserInfo)
            {
                var pck = packet as UserInfo;
                lock (Player)
                    Player.Update(pck);
                if (PlayerStatusUpdate != null)
                    PlayerStatusUpdate(this, EventArgs.Empty);
                return;
            }
            #endregion
            #region MyTargetSelected
            if (packet is MyTargetSelected)
            {
                var pck = packet as MyTargetSelected;
                lock (Characters)
                {
                    var ch = Characters.FirstOrDefault(c => c.ObjectID == pck.ObjectID);
                    if (ch != null)
                        lock (Player)
                        {
                            Player.Target = ch;
                            return;
                        }
                }
                lock (Npcs)
                {
                    var npc = Npcs.FirstOrDefault(c => c.ObjectID == pck.ObjectID);
                    if (npc != null)
                        lock (Player)
                        {
                            if (Player.Level != 0)
                                npc.Level = Player.Level - pck.Color;
                            Player.Target = npc;
                        }
                }
                return;
            }
            #endregion
        }
    }
}