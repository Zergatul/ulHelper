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
        Font font = new Font("Tahoma", 11, FontStyle.Bold, GraphicsUnit.Pixel);

        public event EventHandler<AccountClickEventArgs> AccountClick;
        AccountClickEventArgs clickEA = new AccountClickEventArgs();

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
            pb.MouseUp += pb_MouseUp;

            scroll = new VScrollBar();
            scroll.Left = left + width - scroll.Width - 1;
            scroll.Top = top + 1;
            scroll.Height = height - 2;
            RefreshScroll();
            parent.Controls.Add(scroll);
            scroll.BringToFront();
        }

        void pb_MouseUp(object sender, MouseEventArgs e)
        {
            int index = (e.Y - 2) / 20;
            if (index >= 0 && index < Accounts.List.Count)
                PerformAccountClick(Accounts.List[index]);
        }

        void PerformAccountClick(AccountData acc)
        {
            if (AccountClick != null)
            {
                clickEA.Account = acc;
                AccountClick(this, clickEA);
            }
        }

        void pb_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawRectangle(Pens.Black, 0, 0, pb.Width - 1, pb.Height - 1);
            for (int i = 0; i < Accounts.List.Count; i++)
                e.Graphics.DrawString(Accounts.List[i].Name, font, Brushes.White, 2, 2 + i * 20);
        }

        void RefreshScroll()
        {
        }

        public void Update()
        {
            MainForm.Instance.InvokeIfNeeded(() => pb.Invalidate());
        }
    }
}