using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace ulHelper.App.Tooltips
{
    public partial class ToolTipForm : Form
    {
        public static Color BgColor = Color.FromArgb(0x7A, 0x85, 0xA5);
        public static Pen BorderPen = new Pen(Color.Black, 2);

        Thread thread;
        bool needRedraw;
        volatile bool needTerminate;

        public ToolTipForm()
        {
            InitializeComponent();
            needTerminate = false;
            thread = new Thread(PictureBoxUpdateFunc);
            thread.Start();
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                // turn on WS_EX_TOOLWINDOW style bit
                cp.ExStyle |= 0x80;
                return cp;
            }
        }

        void PictureBoxUpdateFunc()
        {
            int delay = 50;
            while (!needTerminate)
            {
                needRedraw = false;
                if (this.IsHandleCreated)
                    PictureBox.Invoke((ThreadStart)(() => { PictureBox.Invalidate(); }));
                Thread.Sleep(delay);
                while (!needRedraw)
                    Thread.Sleep(1);
            }
        }

        public void UpdateImage()
        {
            needRedraw = true;
        }

        void ShowNoActivate()
        {
            WinAPI.ShowWindow(this.Handle, 4);
            WinAPI.SetWindowPos(this.Handle, -1, 
                this.Left, this.Top, this.Width, this.Height,
                0x0010);
            UpdateImage();
        }

        public void ShowToolTip()
        {
            if (!this.Visible)
                this.ShowNoActivate();
        }

        public void ShowToolTip(Point loc)
        {
            this.Location = loc;
            ShowToolTip();
        }

        public void HideToolTip()
        {
            if (this.Visible)
                Hide();
        }

        public void DisposeResources()
        {
            /*needRedraw = true;
            needTerminate = true;
            thread.Join();*/
            thread.Abort();
        }
    }
}
