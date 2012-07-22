using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace ulHelper.App.Modules
{
    class AppLiveModule
    {
        AccountData acc;
        EventWaitHandle eventWH;
        Thread thread;

        public AppLiveModule(AccountData acc)
        {
            this.acc = acc;
            this.thread = new Thread((ThreadStart)ThreadFunc);
            this.thread.Start();
        }

        public void Terminate()
        {
            if (this.thread.ThreadState != ThreadState.Stopped)
                //thread.Join();
                thread.Abort();
        }

        unsafe void ThreadFunc()
        {
            eventWH = new EventWaitHandle(false, EventResetMode.AutoReset, "ulHelper_appLiveEvent_" + acc.Name);
            while (!acc.NeedTerminate)
            {
                Thread.Sleep(100);
                eventWH.Set();
            }
        }
    }
}