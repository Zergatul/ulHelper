using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ulHelper.App
{
    public static class Accounts
    {
        public static List<AccountData> List;

        static Accounts()
        {
            List = new List<AccountData>();
        }
    }
}