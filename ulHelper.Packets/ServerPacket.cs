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
            if (ID == 0x18)
                return new StatusUpdate(this);
            if (ID == 0x19)
                return new NpcHtmlMessage(this);
            if (ID == 0x21)
                return new InventoryUpdate(this);
            if (ID == 0x22)
                return new TeleportToLocation(this);
            if (ID == 0x23)
                return new TargetSelected(this);
            if (ID == 0x24)
                return new TargetUnselected(this);
            if (ID == 0x31)
                return new CharInfo(this);
            if (ID == 0x32)
                return new UserInfo(this);
            if (ID == 0x62)
                return new SystemMessage(this);
            if (ID == 0xB9)
                return new MyTargetSelected(this);
            if (ID == 0xCE)
                return new RelationChanged(this);
            if (ID == 0xD0)
                return new MultiSellList(this);
            if (ID == 0xFE && ID2 == 0xF6)
                return new FEF6(this);
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

        protected string ReadString()
        {
            int start = Position;
            while (!(Data[Position] == 0 && Data[Position + 1] == 0))
                Position += 2;
            Position += 2;
            return Encoding.Unicode.GetString(Data, start, Position - start - 2);
        }
    }
}