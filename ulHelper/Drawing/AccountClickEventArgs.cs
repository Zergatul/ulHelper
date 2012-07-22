using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ulHelper.App.Drawing
{
    public class AccountClickEventArgs : EventArgs
    {
        public AccountData Account { get; set; }
    }
}