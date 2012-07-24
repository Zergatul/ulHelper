using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ulHelper.Packets
{
    public class ServerPacket
    {
        protected byte ID;
        protected short ID2;
        protected byte[] Data;

        protected int Position;

        public ServerPacket(byte id, short id2, byte[] data)
        {
            this.ID = id;
            this.ID2 = id2;
            this.Data = data;
        }

        protected ServerPacket(ServerPacket pck)
        {
            this.ID = pck.ID;
            this.ID2 = pck.ID2;
            this.Data = pck.Data;
        }

        public ServerPacket Parse()
        {
            if (ID == 0x08)
                return new DeleteObject(this);
            if (ID == 0x0C)
                return new NpcInfo(this);
            if (ID == 0x11)
                return new ItemList(this);
            if (ID == 0x16)
                return new DropItem(this);
            if (ID == 0x17)
                return new GetItem(this);
            if (ID == 0x18)
                return new StatusUpdate(this);
            if (ID == 0x19)
                return new NpcHtmlMessage(this);
            if (ID == 0x1F)
                return new ActionFailed(this);
            if (ID == 0x21)
                return new InventoryUpdate(this);
            if (ID == 0x22)
                return new TeleportToLocation(this);
            if (ID == 0x23)
                return new TargetSelected(this);
            if (ID == 0x24)
                return new TargetUnselected(this);
            if (ID == 0x25)
                return new AutoAttackStart(this);
            if (ID == 0x26)
                return new AutoAttackStop(this);
            if (ID == 0x27)
                return new SocialAction(this);
            if (ID == 0x29)
                return new ChangeWaitType(this);
            if (ID == 0x2F)
                return new MoveToLocation(this);
            if (ID == 0x30)
                return new NpcSay(this);
            if (ID == 0x31)
                return new CharInfo(this);
            if (ID == 0x32)
                return new UserInfo(this);
            if (ID == 0x33)
                return new Attack(this);
            if (ID == 0x47)
                return new StopMove(this);
            if (ID == 0x48)
                return new MagicSkillUse(this);
            if (ID == 0x4A)
                return new CreatureSay(this);
            if (ID == 0x59)
                return new _59(this);
            if (ID == 0x52)
                return new PartySmallWindowUpdate(this);
            if (ID == 0x54)
                return new MagicSkillLaunched(this);
            if (ID == 0x5F)
                return new SkillList(this);
            if (ID == 0x61)
                return new StopRotation(this);
            if (ID == 0x62)
                return new SystemMessage(this);
            if (ID == 0x6A)
                return new PledgeCrest(this);
            if (ID == 0x6B)
                return new SetupGauge(this);
            if (ID == 0x72)
                return new MoveToPawn(this);
            if (ID == 0x75)
                return new FriendList(this);
            if (ID == 0x79)
                return new ValidateLocation(this);
            if (ID == 0x7A)
                return new StartRotation(this);
            if (ID == 0x85)
                return new BuffList(this);
            if (ID == 0x89)
                return new PledgeInfo(this);
            if (ID == 0x9F)
                return new StaticObject(this);
            if (ID == 0xA1)
                return new PrivateStoreListSell(this);
            if (ID == 0xA2)
                return new PrivateStoreMsgSell(this);
            if (ID == 0xB9)
                return new MyTargetSelected(this);
            if (ID == 0xBA)
                return new PartyMemberPosition(this);
            if (ID == 0xBE)
                return new PrivateStoreListBuy(this);
            if (ID == 0xBF)
                return new PrivateStoreMsgBuy(this);
            if (ID == 0xC7)
                return new SkillCoolTime(this);
            if (ID == 0xCC)
                return new NicknameChanged(this);
            if (ID == 0xCE)
                return new RelationChanged(this);
            if (ID == 0xD0)
                return new MultiSellList(this);
            if (ID == 0xD4)
                return new FlyToLocation(this);
            if (ID == 0xE1)
                return new RecipeShopMsg(this);
            if (ID == 0xF9)
                return new EtcStatusUpdate(this);
            if (ID == 0xFE)
            {
                if (ID2 == 0xE6)
                    return new FEE6(this);
                if (ID2 == 0xF6)
                    return new ResponseCommissionList(this);
            }
            // fucking packets, fuck you
            if (ID == 0xD9)
                return this;
            if (ID == 0xFE)
            {
                if (ID2 == 0xC7)
                    return this; 
            }
            // end
            return this;
        }

        protected byte ReadByte()
        {
            byte result = Data[Position];
            Position++;
            return result;
        }

        protected short ReadShort()
        {
            short result = BitConverter.ToInt16(Data, Position);
            Position += 2;
            return result;
        }

        protected int ReadInt()
        {
            int result = BitConverter.ToInt32(Data, Position);
            Position += 4;
            return result;
        }

        protected long ReadLong()
        {
            long result = BitConverter.ToInt32(Data, Position);
            Position += 8;
            return result;
        }

        protected double ReadDouble()
        {
            double result = BitConverter.ToDouble(Data, Position);
            Position += 8;
            return result;
        }

        protected string ReadString()
        {
            int start = Position;
            while (!(Data[Position] == 0 && Data[Position + 1] == 0))
                Position += 2;
            Position += 2;
            return Encoding.Unicode.GetString(Data, start, Position - start - 2);
        }

        protected void ReadItemInfo(ItemInfo item)
        {
            item.ObjectID = ReadInt();
            item.ItemID = ReadInt();
            item.EquipSlot = ReadInt();
            item.Count = ReadLong();
            item.Type2 = ReadShort();
            item.CustomType1 = ReadShort();
            item.IsEquipped = ReadShort();
            item.BodyPart = ReadInt();
            item.Enchant = ReadShort();
            item.CustomType2 = ReadShort();
            item.AugmentID = ReadInt();
            item.ShadowTime = ReadInt();
            item.TempTime = ReadInt();
            this.Position += 2;
            item.AttackAttr = ReadShort();
            item.AttackAttrValue = ReadShort();
            item.DefFire = ReadShort();
            item.DefWater = ReadShort();
            item.DefWind = ReadShort();
            item.DefEarth = ReadShort();
            item.DefHoly = ReadShort();
            item.DefDark = ReadShort();
            item.EnchantOptions1 = ReadShort();
            item.EnchantOptions2 = ReadShort();
            item.EnchantOptions3 = ReadShort();
            this.Position += 4;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(ID.ToString("X2"));
            if (ID2 != -1)
                sb.Append(" [" + ID2.ToString("X2") + "]");
            for (int i = 0; i < Data.Length; i++)
            {
                sb.Append(' ');
                sb.Append(Data[i].ToString("X2"));
            }
            return sb.ToString();
        }
    }
}