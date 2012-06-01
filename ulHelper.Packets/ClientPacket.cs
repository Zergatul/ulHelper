using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ulHelper.Packets
{
    public abstract class ClientPacket
    {
        protected string format;
        private List<object> data;
        protected abstract byte id { get; }
        protected virtual short id2 { get { return -1; } }

        public ClientPacket()
        {
            
        }

        protected void AddValue(object obj)
        {
            if (obj is byte)
            {
                data.Add(obj);
                return;
            }
            if (obj is short)
            {
                data.Add(obj);
                return;
            }
            if (obj is int)
            {
                data.Add(obj);
                return;
            }
            if (obj is long)
            {
                data.Add(obj);
                return;
            }
            if (obj is string)
            {
                data.Add(obj);
                return;
            }
            throw new InvalidDataException("Неправильный тип данных.");
        }

        protected virtual void Format()
        {
            foreach (var obj in data)
            {
                if (obj is byte)
                {
                    format += "c";
                    continue;
                }
                if (obj is short)
                {
                    format += "h";
                    continue;
                }
                if (obj is int)
                {
                    format += "d";
                    continue;
                }
                if (obj is long)
                {
                    format += "Q";
                    continue;
                }
                if (obj is string)
                {
                    format += "S";
                    continue;
                }
            }
        }

        public void WriteTo(BinaryWriter bw)
        {
            format = "";
            data = new List<object>();
            AddValue(id);
            if (id2 != -1)
                AddValue(id2);
            Format();
            byte[] buf = Encoding.Unicode.GetBytes(format);
            bw.Write(format.Length);
            bw.Write(buf);
            bw.Write((short)0);
            foreach (var obj in data)
            {
                if (obj is byte)
                    bw.Write((byte)obj);
                if (obj is short)
                    bw.Write((short)obj);
                if (obj is int)
                    bw.Write((int)obj);
                if (obj is long)
                    bw.Write((long)obj);
                if (obj is string)
                {
                    buf = Encoding.Unicode.GetBytes((string)obj);
                    bw.Write(((string)obj).Length);
                    bw.Write(buf);
                    bw.Write((short)0);
                }
            }
        }
    }
}