using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO;
using System.Reflection;
using ulHelper.Plugins;
using System.Windows.Forms;

namespace ulHelper.App
{
    public static class AppPlugins
    {
        public static List<PluginInfo> Plugins { get; set; }

        static AppPlugins()
        {
            Plugins = new List<PluginInfo>();
        }

        public static void Load()
        {
            foreach (var file in Directory.EnumerateFiles(@"Plugins").Where(s => s.EndsWith(".dll")))
            {
                var asm = Assembly.LoadFile(Application.StartupPath + @"\" + file);
                foreach (var type in asm.GetTypes())
                {
                    if (type.BaseType.FullName == typeof(ParsedPacketsPlugin).FullName)
                        Plugins.Add(new PluginInfo(type));
                    if (type.BaseType.FullName == typeof(UnparsedPacketsPlugin).FullName)
                        Plugins.Add(new PluginInfo(type));
                    if (type.BaseType.FullName == typeof(L2ObjectsPlugin).FullName)
                        Plugins.Add(new PluginInfo(type));
                }
            }
        }
    }
}