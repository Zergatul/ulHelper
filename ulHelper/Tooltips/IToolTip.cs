using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace ulHelper.App.Tooltips
{
    public interface IToolTip
    {
        void Update();
        void Show();
        void Show(Point loc);
        void Hide();
        bool Visible { get; set; }
    }
}