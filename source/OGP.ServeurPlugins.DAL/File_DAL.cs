using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace OGP.ServicePlugin.DAL
{
    public class File_DAL
    {
        public static MemoryStream getPlugin(string pluginPath)
        {
            return new MemoryStream(File.ReadAllBytes(pluginPath));
        }

        public static void putPlugin(MemoryStream memStream, string pluginPath)
        {
            using (FileStream filePlugin = System.IO.File.Create(pluginPath))
            {
                memStream.WriteTo(filePlugin);
            }
        }
    }
}
