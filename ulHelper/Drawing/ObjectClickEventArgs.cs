using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ulHelper.App.Drawing
{
    public class ObjectClickEventArgs : EventArgs
    {
        public L2Objects.L2LiveObject Object { get; set; }

        public ObjectClickEventArgs(L2Objects.L2LiveObject obj)
        {
            this.Object = obj;
        }
    }
}