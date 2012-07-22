using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO;

namespace ulHelper.App.Modules
{
    class AccountLiveModule
    {
        AccountData acc;
        EventWaitHandle eventWH;
        Thread thread;

        public AccountLiveModule(AccountData acc)
        {
            this.acc = acc;
            this.thread = new Thread((ThreadStart)ThreadFunc);
            this.thread.Start();
        }

        public void Terminate()
        {
            if (this.thread.ThreadState != ThreadState.Stopped)
            {
                eventWH.Set();
                thread.Join();
            }
        }

        unsafe void ThreadFunc()
        {
            try
            {
                eventWH = new EventWaitHandle(false, EventResetMode.AutoReset, "ulHelper_liveEvent_" + acc.Name);
                while (!acc.NeedTerminate)
                    if (!eventWH.WaitOne(250))
                    {
                        if (acc.NeedTerminate)
                            break;
                        lock (MainForm.Instance.Accounts)
                        {
                            MainForm.Instance.Accounts.Remove(this.acc);
                            var bw = new BinaryWriter(MainForm.Instance.AccManager.Stream);
                            MainForm.Instance.AccManager.Mutex.WaitOne();
                            try
                            {
                                bw.BaseStream.Position = 0;
                                bw.Write(MainForm.Instance.Accounts.Count);
                                for (int i = 0; i < MainForm.Instance.Accounts.Count; i++)
                                {
                                    bw.BaseStream.Position = 8 + i * 128;
                                    var buf = Encoding.ASCII.GetBytes(acc.Name);
                                    bw.Write(buf, 0, buf.Length);
                                    bw.Write((byte)0);
                                }
                            }
                            finally
                            {
                                MainForm.Instance.AccManager.Mutex.ReleaseMutex();
                            }
                            acc.Form.NeedTerminate = true;
                            acc.Form.Invoke((ThreadStart)acc.Form.Close);
                            MainForm.Instance.Invoke((ThreadStart)MainForm.Instance.RefreshAccounts);
                            acc.Dispose();
                            break;
                        }
                    }
            }
            catch (Exception ex)
            {
                var f = new ExceptionForm(ex);
                f.ShowDialog();
            }
        }
    }
}