using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ulHelper.L2Objects.Events
{
    public class L2AttackEventArgs : EventArgs
    {
        public L2LiveObject Attacker { get; set; }
        public List<AttackInfo> Targets { get; set; }
    }

    public class AttackInfo
    {
        public L2LiveObject Object { get; set; }
        public int Damage { get; set; }
    }
}