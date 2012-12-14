using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using System.IO;

using OGP.ServicePlugins.Modele;

namespace OGP.ServicePlugins.DAL
{

    public class XML_DAL
    {
        private static object lockXML = new object();

        public static void StorePlugins(IList<Plugin> plugins, string xml_file)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<Plugin>));
            lock (lockXML)
            {
                using (StreamWriter xmlWriter = new StreamWriter(xml_file))
                {
                    serializer.Serialize(xmlWriter, plugins);
                }
            }
        }

        public static List<Plugin> LoadPlugins(string xml_file)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<Plugin>));
            FileStream xmlFile = new FileStream(xml_file, FileMode.Open);
            List<Plugin> plugins;
            lock (lockXML)
            {
                plugins = (List<Plugin>)serializer.Deserialize(xmlFile);
            }
            xmlFile.Close();
            return plugins;
        }

    }
}
