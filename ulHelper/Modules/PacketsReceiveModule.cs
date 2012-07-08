using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO;
using ulHelper.Packets;

namespace ulHelper.App.Modules
{
    class PacketsReceiveModule
    {
        AccountData acc;
        Mutex mutex;
        EventWaitHandle eventWH;
        Thread thread;

        public PacketsReceiveModule(AccountData acc)
        {
            this.acc = acc;
            this.thread = new Thread((ThreadStart)ThreadFunc);
            this.thread.Start();
        }

        public void Terminate()
        {
            thread.Abort();
            /*if (this.thread.ThreadState != ThreadState.Stopped)
            {
                eventWH.Set();
                thread.Join();
            }*/
        }

        unsafe void ThreadFunc()
        {
            try
            {
                int size = 65536;
                var hFile = WinAPI.OpenFileMapping(
                    WinAPI.FileMapAccess.FileMapAllAccess,
                    false,
                    "ulHelper_fm_" + acc.Name);
                var lpBaseAddress = WinAPI.MapViewOfFile(
                    hFile,
                    WinAPI.FileMapAccess.FileMapAllAccess,
                    0, 0, 0);

                bool createdNew;
                mutex = new Mutex(false, "ulHelper_mutex_" + acc.Name, out createdNew);
                eventWH = new EventWaitHandle(true, EventResetMode.AutoReset, "ulHelper_event_" + acc.Name);

                var stream = new UnmanagedMemoryStream((byte*)lpBaseAddress.ToPointer(), size, size, FileAccess.ReadWrite);
                var br = new BinaryReader(stream);
                var bw = new BinaryWriter(stream);

                byte[] buf = new byte[size];
                var pckBuffer = new List<ServerPacket>();

                eventWH.Set();
                while (true)
                {
                    pckBuffer.Clear();

                    eventWH.WaitOne();
                    if (acc.NeedTerminate)
                        break;
                    mutex.WaitOne();
                    try
                    {
                        stream.Position = 0;
                        int pckCount = br.ReadInt32();
                        stream.Position = 8;
                        for (int i = 0; i < pckCount; i++)
                        {
                            byte id = br.ReadByte();
                            short id2 = br.ReadInt16();
                            byte[] data = new byte[br.ReadInt32()];
                            br.Read(data, 0, data.Length);
                            pckBuffer.Add(new ServerPacket(id, id2, data));
                        }
                        stream.Position = 0;
                        bw.Write((int)0);
                        bw.Write((int)8);
                    }
                    finally
                    {
                        mutex.ReleaseMutex();
                    }

                    foreach (var pck in pckBuffer)
                        acc.PacketReceive(pck);
                }

                WinAPI.UnmapViewOfFile(lpBaseAddress);
                WinAPI.CloseHandle(hFile);
            }
            catch (Exception ex)
            {
                var f = new ExceptionForm(ex);
                f.ShowDialog();
            }
        }
    }
}