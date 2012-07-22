using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO;

namespace ulHelper.App.Modules
{
    class PacketsSendModule
    {
        AccountData acc;
        Mutex mutex;
        EventWaitHandle eventWH;
        Thread thread;
        EventWaitHandle addPacketEvent;

        public PacketsSendModule(AccountData acc)
        {
            this.acc = acc;
            this.addPacketEvent = new EventWaitHandle(false, EventResetMode.AutoReset);
            this.thread = new Thread((ThreadStart)ThreadFunc);
            this.thread.Start();
        }

        public void Terminate()
        {
            if (this.thread.ThreadState != ThreadState.Stopped)
            {
                addPacketEvent.Set();
                //thread.Join();
                thread.Abort();
            }
        }

        public void NewPacketInQueue()
        {
            addPacketEvent.Set();
        }

        unsafe void ThreadFunc()
        {
            try
            {
                int size = 65536;
                var hFile = WinAPI.OpenFileMapping(
                    WinAPI.FileMapAccess.FileMapAllAccess,
                    false,
                    "ulHelper_fmSend_" + acc.Name);
                var lpBaseAddress = WinAPI.MapViewOfFile(
                    hFile,
                    WinAPI.FileMapAccess.FileMapAllAccess,
                    0, 0, 0);

                bool createdNew;
                mutex = new Mutex(false, "ulHelper_mutexSend_" + acc.Name, out createdNew);
                eventWH = new EventWaitHandle(true, EventResetMode.AutoReset, "ulHelper_eventSend_" + acc.Name);

                var stream = new UnmanagedMemoryStream((byte*)lpBaseAddress.ToPointer(), size, size, FileAccess.ReadWrite);
                var br = new BinaryReader(stream);
                var bw = new BinaryWriter(stream);

                byte[] buf = new byte[size];

                while (true)
                {
                    addPacketEvent.WaitOne();
                    if (acc.NeedTerminate)
                        break;
                    mutex.WaitOne();
                    try
                    {
                        stream.Position = 0;
                        int pckCount = br.ReadInt32();
                        int index = br.ReadInt32();
                        stream.Position = index;

                        int bufferCount;
                        lock (acc.SendBuffer)
                        {
                            bufferCount = acc.SendBuffer.Count;
                            for (int i = 0; i < acc.SendBuffer.Count; i++)
                                acc.SendBuffer[i].WriteTo(bw);
                            acc.SendBuffer.Clear();
                        }

                        index = (int)stream.Position;
                        stream.Position = 0;
                        bw.Write((int)pckCount + bufferCount);
                        bw.Write((int)index);
                    }
                    finally
                    {
                        mutex.ReleaseMutex();
                    }
                    eventWH.Set();
                }

                WinAPI.UnmapViewOfFile(lpBaseAddress);
                WinAPI.CloseHandle(hFile);
            }
            catch (ThreadAbortException)
            {
                throw;
            }
            catch (Exception ex)
            {
                var f = new ExceptionForm(ex);
                f.ShowDialog();
            }
        }
    }
}