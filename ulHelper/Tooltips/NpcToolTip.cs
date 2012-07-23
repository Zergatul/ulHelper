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
    public class NpcToolTip : ToolTip
    {
        public L2Npc Npc;

        public NpcToolTip() : 
            base(200, 135)
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
            if (Npc != null)
            {
                int y = 3;
                e.Graphics.DrawString("Name: " + Npc.Name, GUI.Font, Brushes.Black, 3, y);
                y += 12;
                e.Graphics.DrawString("ID: " + Npc.NpcID, GUI.Font, Brushes.Black, 3, y);
                y += 12;
                e.Graphics.DrawString("Level: " + Npc.Level, GUI.Font, Brushes.Black, 3, y);
                y += 12;
                e.Graphics.DrawString("Unique name: " + Npc.PetName, GUI.Font, Brushes.Black, 3, y);
                y += 12;
                e.Graphics.DrawString("Owner name: " + Npc.OwnerName, GUI.Font, Brushes.Black, 3, y);
                y += 12;
                e.Graphics.DrawString("HP: " + Npc.CurHP + "/" + Npc.MaxHP, GUI.Font, Brushes.Black, 3, y);
                y += 12;
                e.Graphics.DrawString("MP: " + Npc.CurMP + "/" + Npc.MaxMP, GUI.Font, Brushes.Black, 3, y);
                y += 12;
                e.Graphics.DrawString("Speed: " + Npc.Speed, GUI.Font, Brushes.Black, 3, y);
                y += 12;
                e.Graphics.DrawString("AtkSpeed: " + Npc.AtkSpeed, GUI.Font, Brushes.Black, 3, y);
                y += 12;
                e.Graphics.DrawString("CastSpeed: " + Npc.CastSpeed, GUI.Font, Brushes.Black, 3, y);
                y += 12;
            }
        }
    }
}