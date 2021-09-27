using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace Goniometer.Settings
{
    public static class FileFolderProvider
    {
        static FileFolderProvider()
        {
            FileFolderProvider.DefaultDataFolder = ConfigurationManager.AppSettings["default.reportFolder"];
        }

        public static string DefaultDataFolder { get; set; }
    }
}
