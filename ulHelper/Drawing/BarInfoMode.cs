using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ulHelper.App.Drawing
{
    public enum BarInfoMode
    {
        Absolutely,
        Relative,
        Both
    }

    public static class BarInfoModeExtensions
    {
        public static BarInfoMode GetNext(this BarInfoMode bi)
        {
            if (bi < BarInfoMode.Both)
                return ++bi;
            return BarInfoMode.Absolutely;
        }
    }
}