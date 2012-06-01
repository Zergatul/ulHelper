namespace ulHelper.App
{
    partial class ExceptionForm
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
            this.kryptonManager = new ComponentFactory.Krypton.Toolkit.KryptonManager(this.components);
            this.kryptonPanel = new ComponentFactory.Krypton.Toolkit.KryptonPanel();
            this.msgTB = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.msgLb = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.stLb = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.stTB = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel)).BeginInit();
            this.kryptonPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // kryptonManager
            // 
            this.kryptonManager.GlobalPaletteMode = ComponentFactory.Krypton.Toolkit.PaletteModeManager.SparkleOrange;
            // 
            // kryptonPanel
            // 
            this.kryptonPanel.Controls.Add(this.stLb);
            this.kryptonPanel.Controls.Add(this.stTB);
            this.kryptonPanel.Controls.Add(this.msgLb);
            this.kryptonPanel.Controls.Add(this.msgTB);
            this.kryptonPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel.Name = "kryptonPanel";
            this.kryptonPanel.Size = new System.Drawing.Size(470, 266);
            this.kryptonPanel.TabIndex = 0;
            // 
            // msgTB
            // 
            this.msgTB.Location = new System.Drawing.Point(67, 10);
            this.msgTB.Name = "msgTB";
            this.msgTB.Size = new System.Drawing.Size(403, 19);
            this.msgTB.TabIndex = 0;
            // 
            // msgLb
            // 
            this.msgLb.Location = new System.Drawing.Point(0, 13);
            this.msgLb.Name = "msgLb";
            this.msgLb.Size = new System.Drawing.Size(61, 19);
            this.msgLb.TabIndex = 1;
            this.msgLb.Values.Text = "Exception:";
            // 
            // stLb
            // 
            this.stLb.Location = new System.Drawing.Point(0, 38);
            this.stLb.Name = "stLb";
            this.stLb.Size = new System.Drawing.Size(66, 19);
            this.stLb.TabIndex = 3;
            this.stLb.Values.Text = "StackTrace:";
            // 
            // stTB
            // 
            this.stTB.Location = new System.Drawing.Point(67, 35);
            this.stTB.Multiline = true;
            this.stTB.Name = "stTB";
            this.stTB.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.stTB.Size = new System.Drawing.Size(403, 228);
            this.stTB.TabIndex = 2;
            this.stTB.WordWrap = false;
            // 
            // ExceptionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(470, 266);
            this.Controls.Add(this.kryptonPanel);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ExceptionForm";
            this.ShowIcon = false;
            this.Text = "Exception";
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel)).EndInit();
            this.kryptonPanel.ResumeLayout(false);
            this.kryptonPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private ComponentFactory.Krypton.Toolkit.KryptonManager kryptonManager;
        private ComponentFactory.Krypton.Toolkit.KryptonPanel kryptonPanel;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel stLb;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel msgLb;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox msgTB;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox stTB;
    }
}

