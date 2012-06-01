namespace ulHelper.App
{
    partial class AccountForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AccountForm));
            this.kryptonManager = new ComponentFactory.Krypton.Toolkit.KryptonManager(this.components);
            this.kryptonPanel = new ComponentFactory.Krypton.Toolkit.KryptonPanel();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.kryptonButton1 = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.showNpcsCB = new ComponentFactory.Krypton.Toolkit.KryptonCheckBox();
            this.showPlayersCB = new ComponentFactory.Krypton.Toolkit.KryptonCheckBox();
            this.targetPB = new System.Windows.Forms.PictureBox();
            this.objectsLB = new ComponentFactory.Krypton.Toolkit.KryptonListBox();
            this.settingsBtn = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.mapPB = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel)).BeginInit();
            this.kryptonPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.targetPB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mapPB)).BeginInit();
            this.SuspendLayout();
            // 
            // kryptonManager
            // 
            this.kryptonManager.GlobalPaletteMode = ComponentFactory.Krypton.Toolkit.PaletteModeManager.SparkleOrange;
            // 
            // kryptonPanel
            // 
            this.kryptonPanel.Controls.Add(this.textBox1);
            this.kryptonPanel.Controls.Add(this.kryptonButton1);
            this.kryptonPanel.Controls.Add(this.showNpcsCB);
            this.kryptonPanel.Controls.Add(this.showPlayersCB);
            this.kryptonPanel.Controls.Add(this.targetPB);
            this.kryptonPanel.Controls.Add(this.objectsLB);
            this.kryptonPanel.Controls.Add(this.settingsBtn);
            this.kryptonPanel.Controls.Add(this.mapPB);
            this.kryptonPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel.Name = "kryptonPanel";
            this.kryptonPanel.Size = new System.Drawing.Size(230, 524);
            this.kryptonPanel.TabIndex = 0;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(3, 295);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(196, 21);
            this.textBox1.TabIndex = 11;
            // 
            // kryptonButton1
            // 
            this.kryptonButton1.Location = new System.Drawing.Point(67, 321);
            this.kryptonButton1.Name = "kryptonButton1";
            this.kryptonButton1.Size = new System.Drawing.Size(37, 25);
            this.kryptonButton1.TabIndex = 10;
            this.kryptonButton1.Values.Text = "tt";
            this.kryptonButton1.Click += new System.EventHandler(this.kryptonButton1_Click);
            // 
            // showNpcsCB
            // 
            this.showNpcsCB.LabelStyle = ComponentFactory.Krypton.Toolkit.LabelStyle.NormalControl;
            this.showNpcsCB.Location = new System.Drawing.Point(-1, 331);
            this.showNpcsCB.Name = "showNpcsCB";
            this.showNpcsCB.Size = new System.Drawing.Size(45, 19);
            this.showNpcsCB.TabIndex = 9;
            this.showNpcsCB.Text = "NPC";
            this.showNpcsCB.Values.Text = "NPC";
            this.showNpcsCB.CheckedChanged += new System.EventHandler(this.showNpcsCB_CheckedChanged);
            // 
            // showPlayersCB
            // 
            this.showPlayersCB.Checked = true;
            this.showPlayersCB.CheckState = System.Windows.Forms.CheckState.Checked;
            this.showPlayersCB.LabelStyle = ComponentFactory.Krypton.Toolkit.LabelStyle.NormalControl;
            this.showPlayersCB.Location = new System.Drawing.Point(-1, 316);
            this.showPlayersCB.Name = "showPlayersCB";
            this.showPlayersCB.Size = new System.Drawing.Size(62, 19);
            this.showPlayersCB.TabIndex = 8;
            this.showPlayersCB.Text = "Игроки";
            this.showPlayersCB.Values.Text = "Игроки";
            this.showPlayersCB.CheckedChanged += new System.EventHandler(this.showPlayersCB_CheckedChanged);
            // 
            // targetPB
            // 
            this.targetPB.Location = new System.Drawing.Point(0, 58);
            this.targetPB.Name = "targetPB";
            this.targetPB.Size = new System.Drawing.Size(199, 56);
            this.targetPB.TabIndex = 7;
            this.targetPB.TabStop = false;
            this.targetPB.Paint += new System.Windows.Forms.PaintEventHandler(this.targetPB_Paint);
            // 
            // objectsLB
            // 
            this.objectsLB.Location = new System.Drawing.Point(0, 352);
            this.objectsLB.Name = "objectsLB";
            this.objectsLB.Size = new System.Drawing.Size(202, 171);
            this.objectsLB.TabIndex = 5;
            this.objectsLB.SelectedIndexChanged += new System.EventHandler(this.objectsLB_SelectedIndexChanged);
            // 
            // settingsBtn
            // 
            this.settingsBtn.Location = new System.Drawing.Point(132, 322);
            this.settingsBtn.Name = "settingsBtn";
            this.settingsBtn.Size = new System.Drawing.Size(68, 24);
            this.settingsBtn.TabIndex = 4;
            this.settingsBtn.Values.Text = "Настройки";
            this.settingsBtn.Click += new System.EventHandler(this.settingsBtn_Click);
            // 
            // mapPB
            // 
            this.mapPB.Location = new System.Drawing.Point(0, 116);
            this.mapPB.Name = "mapPB";
            this.mapPB.Size = new System.Drawing.Size(200, 200);
            this.mapPB.TabIndex = 0;
            this.mapPB.TabStop = false;
            this.mapPB.Paint += new System.Windows.Forms.PaintEventHandler(this.mapPB_Paint);
            // 
            // AccountForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(230, 524);
            this.Controls.Add(this.kryptonPanel);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AccountForm";
            this.Text = "AccountForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AccountForm_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.AccountForm_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel)).EndInit();
            this.kryptonPanel.ResumeLayout(false);
            this.kryptonPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.targetPB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mapPB)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private ComponentFactory.Krypton.Toolkit.KryptonManager kryptonManager;
        private ComponentFactory.Krypton.Toolkit.KryptonPanel kryptonPanel;
        private System.Windows.Forms.PictureBox mapPB;
        private ComponentFactory.Krypton.Toolkit.KryptonButton settingsBtn;
        private ComponentFactory.Krypton.Toolkit.KryptonListBox objectsLB;
        private System.Windows.Forms.PictureBox targetPB;
        private ComponentFactory.Krypton.Toolkit.KryptonCheckBox showNpcsCB;
        private ComponentFactory.Krypton.Toolkit.KryptonCheckBox showPlayersCB;
        private ComponentFactory.Krypton.Toolkit.KryptonButton kryptonButton1;
        private System.Windows.Forms.TextBox textBox1;
    }
}

