using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ulHelper.L2Objects.Events
{
    public class L2SkillUseEventArgs : EventArgs
    {
        public L2LiveObject Object { get; set; }
        public L2LiveObject Target { get; set; }
        public int SkillID { get; set; }
        public int SkillLevel { get; set; }
        public int CastTimeMs { get; set; }
        public int ReuseTimeMs { get; set; }
    }
}