using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ulHelper.App.Drawing
{
    public class ObjectClickEventArgs : EventArgs
    {
        public L2Objects.L2Object Object { get; set; }

        public ObjectClickEventArgs(L2Objects.L2Object obj)
        {
            this.Object = obj;
        }
    }
}