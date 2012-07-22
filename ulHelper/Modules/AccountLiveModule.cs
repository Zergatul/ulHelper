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

        public event EventHandler RemoveAccount;

        public AccountLiveModule(AccountData acc)
        {
            this.acc = acc;
            this.thread = new Thread((ThreadStart)ThreadFunc);
            this.thread.Start();
        }

        void PerformRemoveAccount()
        {
            if (RemoveAccount != null)
                RemoveAccount(this, EventArgs.Empty);
        }

        public void Terminate()
        {
            if (this.thread.ThreadState != ThreadState.Stopped)
            {
                eventWH.Set();
                //thread.Join();
                thread.Abort();
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
                        lock (Accounts.List)
                        {
                            Accounts.List.Remove(this.acc);
                            var bw = new BinaryWriter(AccountManagerModule.Stream);
                            AccountManagerModule.Mutex.WaitOne();
                            try
                            {
                                bw.BaseStream.Position = 0;
                                bw.Write(Accounts.List.Count);
                                for (int i = 0; i < Accounts.List.Count; i++)
                                {
                                    bw.BaseStream.Position = 8 + i * 128;
                                    var buf = Encoding.ASCII.GetBytes(acc.Name);
                                    bw.Write(buf, 0, buf.Length);
                                    bw.Write((byte)0);
                                }
                            }
                            finally
                            {
                                AccountManagerModule.Mutex.ReleaseMutex();
                            }
                            acc.Form.NeedTerminate = true;
                            acc.Form.InvokeIfNeeded(acc.Form.Close);
                            PerformRemoveAccount();
                            acc.Dispose();
                            break;
                        }
                    }
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