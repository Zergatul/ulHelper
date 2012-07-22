using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace ulHelper.App.Drawing
{
    class AccountList
    {
        int left, top, width, height;
        PictureBox pb;
        VScrollBar scroll;

        public AccountList(Control parent, int left, int top, int width, int height)
        {
            this.left = left;
            this.top = top;
            this.width = width;
            this.height = height;

            pb = new PictureBox();
            pb.Left = left;
            pb.Top = top;
            pb.Width = width;
            pb.Height = height;
            parent.Controls.Add(pb);
            pb.Paint += pb_Paint;

            scroll = new VScrollBar();
            scroll.Left = left + width - scroll.Width - 1;
            scroll.Top = top + 1;
            scroll.Height = height - 2;
            RefreshScroll();
            parent.Controls.Add(scroll);
            scroll.BringToFront();
        }

        void pb_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawRectangle(Pens.Black, 0, 0, pb.Width - 1, pb.Height - 1);
        }

        void RefreshScroll()
        {
        }
    }
}