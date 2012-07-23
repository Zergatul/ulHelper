using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ulHelper.App
{
    public static class Settings
    {
        public static ObjectsListSettings ObjectsList { get; private set; }

        static Settings()
        {
            ObjectsList = new ObjectsListSettings();
        }
    }
}