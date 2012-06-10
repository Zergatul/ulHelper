using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ulHelper.L2Objects.Events
{
    public class L2CharacterEventArgs : EventArgs
    {
        public L2Character Character { get; set; }
    }
}