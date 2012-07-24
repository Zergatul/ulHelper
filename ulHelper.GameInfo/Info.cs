using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Drawing;

namespace ulHelper.GameInfo
{
    public static class Info
    {
        public static Dictionary<int, L2Class> Classes;
        public static SortedDictionary<int, string> Npcs;
        public static SortedDictionary<int, string> Skills;
        public static SortedDictionary<int, long> LevelsExp;
        public static SortedDictionary<int, string> Items;

        public static void LoadClasses()
        {
            var images = new List<Bitmap>();
            var bmp = new Bitmap(@"Images\Classes.png");
            for (int i = 0; i < bmp.Width / 11; i++)
            {
                var icon = new Bitmap(11, 11);
                var g = Graphics.FromImage(icon);
                g.DrawImage(bmp, new Rectangle(0, 0, 11, 11), new Rectangle(i * 11, 0, 11, 11), GraphicsUnit.Pixel);
                g.Dispose();
                images.Add(icon);
            }

            Classes = new Dictionary<int, L2Class>();
            var xDoc = XDocument.Load(@"Data\Classes.xml");
            foreach (var classNode in xDoc.Element("classes").Elements("class"))
            {
                
                Classes.Add(
                    (int)classNode.Attribute("id"),
                    new L2Class
                    {
                        Name = (string)classNode.Attribute("name"),
                        Icon = images[(int)classNode.Attribute("icon")]
                    });
            }
        }

        public static void LoadNpcs()
        {
            Npcs = new SortedDictionary<int, string>();
            var xDoc = XDocument.Load(@"Data\Npcs.xml");
            foreach (var npcNode in xDoc.Element("npcs").Elements("npc"))
                Npcs.Add((int)npcNode.Attribute("id"), (string)npcNode.Attribute("name"));
        }

        public static void LoadSkills()
        {
            Skills = new SortedDictionary<int, string>();
            var xDoc = XDocument.Load(@"Data\Skills.xml");
            foreach (var skillNode in xDoc.Element("skills").Elements("skill"))
                Skills.Add((int)skillNode.Attribute("id"), (string)skillNode.Attribute("name"));
        }

        public static void LoadItems()
        {
            Items = new SortedDictionary<int, string>();
            var xDoc = XDocument.Load(@"Data\Items.xml");
            foreach (var itemNode in xDoc.Element("items").Elements("item"))
                Items.Add((int)itemNode.Attribute("id"), (string)itemNode.Attribute("name"));
        }

        public static void LoadLevels()
        {
            LevelsExp = new SortedDictionary<int, long>();
            var xDoc = XDocument.Load(@"Data\Levels.xml");
            int index = 1;
            foreach (var lvlNode in xDoc.Element("levels").Elements("level"))
                LevelsExp.Add(index++, (long)lvlNode.Attribute("exp"));
        }

        public class L2Class
        {
            public string Name;
            public Bitmap Icon;
        }

        public enum Attribute
        {
            None = -2,
            Fire = 0,
            Water = 1,
            Wind = 2,
            Earth = 3,
            Holy = 4,
            Dark = 5
        }
    }
}