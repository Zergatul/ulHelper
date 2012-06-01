using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ComponentFactory.Krypton.Toolkit;

namespace ulHelper.App
{
    public partial class ExceptionForm : ComponentFactory.Krypton.Toolkit.KryptonForm
    {
        public ExceptionForm(Exception ex)
        {
            InitializeComponent();
            msgTB.Text = ex.Message;
            stTB.Text = ex.StackTrace;
        }
    }
}