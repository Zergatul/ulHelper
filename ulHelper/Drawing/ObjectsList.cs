using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using ulHelper.L2Objects;

namespace ulHelper.App.Drawing
{
    public class ObjectsList
    {
        static Color borderColor = Color.FromArgb(0x3C, 0x41, 0x51);
        static Pen borderPen = new Pen(borderColor);
        static int itemHeight = 13;
        static Bitmap unknownClass;

        int x, y, width, height;
        VScrollBar scroll;
        GameWorld world;
        MiniHPBar bar;

        public static void Load()
        {
            var buf = new Bitmap(@"Images\UnknownClass.png");
            unknownClass = new Bitmap(11, 11);
            using (var g = Graphics.FromImage(unknownClass))
                g.DrawImage(buf, new RectangleF(0, 0, 11, 11));
            buf.Dispose();
        }

        public event EventHandler ScrollChanged;

        public ObjectsList(PictureBox pb, GameWorld world, int x, int y, int width, int height)
        {
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
            this.world = world;

            scroll = new VScrollBar();
            scroll.Height = height - 1;
            scroll.Left = pb.Left + x + width - scroll.Width;
            scroll.Top = pb.Top + y + 1;
            scroll.LargeChange = height / itemHeight;
            pb.FindForm().Controls.Add(scroll);
            scroll.BringToFront();
            scroll.ValueChanged += ScrollValueChanged;

            bar = new MiniHPBar(72);
        }

        void ScrollValueChanged(object sender, EventArgs e)
        {
            if (ScrollChanged != null)
                ScrollChanged(this, EventArgs.Empty);
        }

        public void Draw(Graphics g)
        {
            g.DrawRectangle(borderPen, x, y, width, height);
            lock (world.Characters)
            {
                if (scroll.Maximum != world.Characters.Count - 1)
                    scroll.Maximum = world.Characters.Count - 1;
                scroll.Enabled = scroll.LargeChange < scroll.Maximum;
                for (int i = scroll.Value; i < Math.Min(scroll.Value + scroll.LargeChange, world.Characters.Count - 1); i++)
                    DrawObject(g, world.Characters[i], 4, 4 + itemHeight * (i - scroll.Value), 206);
            }
        }

        void DrawObject(Graphics g, L2Character ch, int x, int y, int width)
        {
            if (GameInfo.Classes.ContainsKey(ch.ClassID))
                g.DrawImage(GameInfo.Classes[ch.ClassID].Icon, x, y);
            else
                g.DrawImage(unknownClass,  x, y);
            bar.Draw(g, x + 12, y + 1, ch.CurHP, ch.MaxHP);
            g.DrawString(ch.Name, GUI.Font, GUI.NeutralBrush, x + 84, y);
        }
    }
}