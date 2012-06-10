using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ulHelper.L2Objects.Events
{
    public class L2NpcEventArgs : EventArgs
    {
        public L2Npc Npc { get; set; }
    }
}