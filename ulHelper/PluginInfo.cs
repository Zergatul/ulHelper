using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ulHelper.Plugins;

namespace ulHelper.App
{
    public class PluginInfo
    {
        public Type Type { get; set; }
        public string Name { get; set; }
        public string Version { get; set; }
        public string Info { get; set; }

        public PluginInfo(Type type)
        {
            this.Type = type;

            var plugin = CreateInstance(false);
            this.Name = plugin.Name;
            this.Info = plugin.Info;
            this.Version = plugin.Version;
        }

        public BasePlugin CreateInstance(bool inThread)
        {
            if (!inThread)
                return (BasePlugin)Activator.CreateInstance(Type);
            else
            {
                return (BasePlugin)Activator.CreateInstance(Type);
            }
        }

        public override string ToString()
        {
            return Name + " [" + Version + "]";
        }
    }
}