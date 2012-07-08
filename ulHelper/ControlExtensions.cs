using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ulHelper.App
{
    public static class ControlExtensions
    {
        public static void InvokeIfNeeded(this Form form, Action act)
        {
            if (form.InvokeRequired)
                form.Invoke(act);
            else
                act();
        }
    }
}