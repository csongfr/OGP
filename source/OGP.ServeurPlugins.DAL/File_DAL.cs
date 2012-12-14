using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace OGP.ServicePlugins.DAL
{
    public class File_DAL
    {
        public static MemoryStream getPlugin(string pluginPath)
        {
            MemoryStream memStream = new MemoryStream();

            using(FileStream fs = System.IO.File.OpenRead(pluginPath))
            {
                int cpt=0;
                while (cpt < fs.Length)
                {
                    memStream.WriteByte(Convert.ToByte(fs.ReadByte()));
                    cpt++;
                }

                fs.Close();
                return memStream;  
            }

        }

        public static void putPlugin(MemoryStream memStream, string pluginPath)
        {
            using (FileStream filePlugin = System.IO.File.Create(pluginPath))
            {
                int cpt = 0;

                while (cpt < memStream.Length)
                {
                    filePlugin.WriteByte(Convert.ToByte(memStream.ReadByte()));
                    cpt++;
                }
                filePlugin.Close();
            }
        }
    }
}
