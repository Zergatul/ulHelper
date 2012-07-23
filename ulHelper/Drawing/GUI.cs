using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace ulHelper.App.Drawing
{
    public static class GUI
    {
        public static Font Font = new Font("Arial", 8.25f, FontStyle.Bold, GraphicsUnit.Point);
        public static Color BgColor = Color.FromArgb(0x63, 0x6C, 0x87);
        public static Color NpcColor = Color.FromArgb(0xFF, 0xF6, 0x00);
        public static Color WarColor = Color.FromArgb(0xB0, 0x00, 0x00);
        public static Color AllyColor = Color.FromArgb(0x00, 0xCA, 0x21);
        public static Color NeutralColor = Color.FromArgb(0xE8, 0xE8, 0xE8);
        public static Color TargetColor = Color.Red;
        public static Color PlayerColor = Color.Black;
        public static Brush NpcBrush = new SolidBrush(NpcColor);
        public static Brush WarBrush = new SolidBrush(WarColor);
        public static Brush AllyBrush = new SolidBrush(AllyColor);
        public static Brush NeutralBrush = new SolidBrush(NeutralColor);

        public static void RoundedRectangle(Graphics g, int x, int y, int width, int height)
        {
            g.DrawLine(Pens.Black, 1, 0, width - 1, 0);
            g.DrawLine(Pens.Black, 1, height, width - 1, height);
            g.DrawLine(Pens.Black, 0, 1, 0, height - 1);
            g.DrawLine(Pens.Black, width, 1, width, height - 1);

            g.DrawLine(Pens.Black, 2, 0, 0, 2);
            g.DrawLine(Pens.Black, 2, height, 0, height - 2);
            g.DrawLine(Pens.Black, width - 2, 0, width, 2);
            g.DrawLine(Pens.Black, width - 2, height, width, height - 2);
        }

        public static void CroppedRectangle(Graphics g, int x, int y, int width, int height, int dx, int dy)
        {
            g.DrawLine(Pens.Black, 1, 0, width - 1, 0);
            g.DrawLine(Pens.Black, 1, height, width - 1  - dx, height);
            g.DrawLine(Pens.Black, 0, 1, 0, height - 1);
            g.DrawLine(Pens.Black, width, 1, width, height - 1 - dy);
            g.DrawLine(Pens.Black, width - dx + 1, height - dy, width - 1, height - dy);
            g.DrawLine(Pens.Black, width - dx, height - dy + 1, width - dx, height - 1);

            g.DrawLine(Pens.Black, 2, 0, 0, 2);
            g.DrawLine(Pens.Black, 2, height, 0, height - 2);
            g.DrawLine(Pens.Black, width - 2, 0, width, 2);
            g.DrawLine(Pens.Black, width - dx + 2, height - dy, width - dx, height - dy + 2);
            g.DrawLine(Pens.Black, width, height - dy - 2, width - 2, height - dy);
            g.DrawLine(Pens.Black, width - dx - 2, height, width - dx, height - 2);
        }
    }
}