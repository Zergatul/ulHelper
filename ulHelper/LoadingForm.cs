using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ComponentFactory.Krypton.Toolkit;
using System.Threading;
using ulHelper.L2Objects;
using ulHelper.App.Drawing;

namespace ulHelper.App
{
    public partial class LoadingForm : ComponentFactory.Krypton.Toolkit.KryptonForm
    {
        Thread Thread;
        int progress;
        string operText;

        public LoadingForm()
        {
            InitializeComponent();
        }

        private void LoadingForm_Shown(object sender, EventArgs e)
        {
            operLb.Text = "";
            Thread = new Thread((ThreadStart)AppLoad);
            Thread.Start();
        }

        void AppLoad()
        {
            operText = "�������� ����� . . .";
            this.Invoke((SimpleCall)TextChange);
            WorldMap.Load();
            progress = 30;

            operText = "�������� �������� . . .";
            this.Invoke((SimpleCall)TextChange);
            this.Invoke((SimpleCall)ProgressChange);
            CPBar.Load();
            HPBar.Load();
            MPBar.Load();
            ExpBar.Load();
            LevelBar.Load();
            MiniHPBar.Load();
            ObjectsList.Load();
            progress = 50;

            operText = "�������� ������� ������� . . .";
            this.Invoke((SimpleCall)TextChange);
            this.Invoke((SimpleCall)ProgressChange);
            GameInfo.LoadClasses();
            GameInfo.LoadLevels();
            progress = 55;

            operText = "�������� NPC . . .";
            this.Invoke((SimpleCall)TextChange);
            this.Invoke((SimpleCall)ProgressChange);
            GameInfo.LoadNpcs();
            progress = 60;

            operText = "�������� �������� . . .";
            this.Invoke((SimpleCall)TextChange);
            this.Invoke((SimpleCall)ProgressChange);
            AppPlugins.Load();
            progress = 100;

            operText = "���������";
            this.Invoke((SimpleCall)TextChange);
            this.Invoke((SimpleCall)ProgressChange);

            this.Invoke((SimpleCall)Close);
        }

        void ProgressChange()
        {
            loadPB.Value = progress;
        }

        void TextChange()
        {
            operLb.Text = operText;
        }
    }
}