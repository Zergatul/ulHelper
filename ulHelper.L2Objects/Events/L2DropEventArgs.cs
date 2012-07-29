using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ulHelper.L2Objects.Events
{
    public class L2DropEventArgs : EventArgs
    {
        public L2DropItem Drop { get; set; }
    }
}