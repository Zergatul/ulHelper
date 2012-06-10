using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ulHelper.L2Objects.Events
{
    public class L2LiveObjectEventArgs : EventArgs
    {
        public L2LiveObject LiveObject { get; set; }
    }
}