using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using ulHelper.L2Objects;
using ulHelper.App.Drawing;
using System.Threading;

namespace ulHelper.App.Tooltips
{
    public class CharacterToolTip : ToolTip
    {
        public L2Character Character;

        public CharacterToolTip() :
            base(200, 155)
        {
            this._form.PictureBox.Paint += pb_Paint;
        }

        void pb_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            e.Graphics.Clear(ToolTipForm.BgColor);
            e.Graphics.DrawPath(ToolTipForm.BorderPen, this._regionPath);
            e.Graphics.ResetTransform();
            if (Character != null)
            {
                int y = 3;
                e.Graphics.DrawString("Name: " + Character.Name, GUI.Font, Brushes.Black, 3, y);
                y += 12;
                e.Graphics.DrawString("Title: " + Character.Title, GUI.Font, Brushes.Black, 3, y);
                y += 12;
                e.Graphics.DrawString("HP: " + Character.CurHP + "/" + Character.MaxHP, GUI.Font, Brushes.Black, 3, y);
                y += 12;
                e.Graphics.DrawString("MP: " + Character.CurMP + "/" + Character.MaxMP, GUI.Font, Brushes.Black, 3, y);
                y += 12;
                e.Graphics.DrawString("CurCP: " + Character.CurCP, GUI.Font, Brushes.Black, 3, y);
                y += 12;
                e.Graphics.DrawString("Speed: " + Character.Speed, GUI.Font, Brushes.Black, 3, y);
                y += 12;
                e.Graphics.DrawString("AtkSpeed: " + Character.AtkSpeed, GUI.Font, Brushes.Black, 3, y);
                y += 12;
                e.Graphics.DrawString("CastSpeed: " + Character.CastSpeed, GUI.Font, Brushes.Black, 3, y);
                y += 12;
                e.Graphics.DrawString("Karma: " + Character.Karma, GUI.Font, Brushes.Black, 3, y);
                y += 12;
                e.Graphics.DrawString("Class: " + Character.ClassName, GUI.Font, Brushes.Black, 3, y);
                y += 12;
                e.Graphics.DrawString("Noble: " + (Character.Noble ? "Yes" : "No"), GUI.Font, Brushes.Black, 3, y);
                y += 12;
                e.Graphics.DrawString("Hero: " + (Character.Hero ? "Yes" : "No"), GUI.Font, Brushes.Black, 3, y);
                y += 12;                
            }
        }
    }
}