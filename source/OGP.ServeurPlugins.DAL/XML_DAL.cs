using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using System.IO;

using OGP.ServicePlugin.Modele;

namespace OGP.ServicePlugin.DAL
{

    public class XML_DAL
    {
        private static object lockXML = new object();

        public static void StorePlugins(IList<PluginModel> plugins, string xml_file)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<PluginModel>));
            lock (lockXML)
            {
                using (StreamWriter xmlWriter = new StreamWriter(xml_file))
                {
                    serializer.Serialize(xmlWriter, plugins);
                }
            }
        }

        public static List<PluginModel> LoadPlugins(string xml_file)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<PluginModel>));
            FileStream xmlFile = new FileStream(xml_file, FileMode.Open);
            List<PluginModel> plugins;
            lock (lockXML)
            {
                plugins = (List<PluginModel>)serializer.Deserialize(xmlFile);
            }
            xmlFile.Close();
            return plugins;
        }

    }
}
