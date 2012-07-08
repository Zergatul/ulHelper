using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO;

namespace ulHelper.App.Modules
{
    public static class AccountManagerModule
    {
        public static Mutex Mutex;
        static EventWaitHandle eventWH;
        static Thread thread;
        public static UnmanagedMemoryStream Stream;

        public static event EventHandler NewAccount;

        static AccountManagerModule()
        {
            thread = new Thread((ThreadStart)ThreadFunc);
            thread.Start();
        }

        public static void Terminate()
        {
            if (thread.ThreadState != ThreadState.Stopped)
            {
                eventWH.Set();
                //thread.Join();
                thread.Abort();
            }
        }

        static void PerformNewAccount()
        {
            if (NewAccount != null)
                NewAccount(null, EventArgs.Empty);
        }

        unsafe private static void ThreadFunc()
        {
            try
            {
                int size = 65536;
                bool createdMain = false;
                var hFile = WinAPI.OpenFileMapping(
                    WinAPI.FileMapAccess.FileMapAllAccess,
                    false,
                    "ulHelper_fm");
                if (hFile == IntPtr.Zero)
                {
                    hFile = WinAPI.CreateFileMapping(
                        new IntPtr(-1),
                        IntPtr.Zero,
                        WinAPI.FileMapProtection.PageReadWrite,
                        (uint)0, (uint)size,
                        "ulHelper_fm");
                    createdMain = true;
                }
                var lpBaseAddress = WinAPI.MapViewOfFile(
                    hFile,
                    WinAPI.FileMapAccess.FileMapAllAccess,
                    0, 0, 0);

                Mutex = new Mutex(false, "ulHelper_mutex");
                Stream = new UnmanagedMemoryStream((byte*)lpBaseAddress.ToPointer(), size, size, FileAccess.ReadWrite);

                eventWH = new EventWaitHandle(false, EventResetMode.AutoReset, "ulHelper_event");

                int accCount;
                byte[] buf = new byte[size];

                if (createdMain)
                {
                    Mutex.WaitOne();
                    try
                    {
                        buf[0] = buf[1] = buf[2] = buf[3] = 0;
                        Stream.Position = 0;
                        Stream.Write(buf, 0, 4);
                    }
                    finally
                    {
                        Mutex.ReleaseMutex();
                    }
                }

                eventWH.Set();
                while (true)
                {
                    eventWH.WaitOne();
                    if (MainForm.NeedTerminate)
                        break;
                    Mutex.WaitOne();
                    try
                    {
                        Stream.Position = 0;
                        accCount = Stream.ReadByte();
                        lock (Accounts.List)
                            if (Accounts.List.Count != accCount)
                            {
                                foreach (var acc in Accounts.List)
                                    acc.Active = false;
                                for (int i = 0; i < accCount; i++)
                                {
                                    string accountName = "";
                                    int index = 0;
                                    Stream.Position = 8 + i * 128;
                                    Stream.Read(buf, 0, 128);
                                    while (buf[index] != 0)
                                        accountName += (char)buf[index++];
                                    if (!Accounts.List.Any(acc => acc.Name == accountName))
                                    {
                                        var accData = new AccountData(accountName);
                                        Accounts.List.Add(accData);
                                    }
                                    Accounts.List.First(acc => acc.Name == accountName).Active = true;
                                }
                                PerformNewAccount();
                            }
                    }
                    finally
                    {
                        Mutex.ReleaseMutex();
                    }
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