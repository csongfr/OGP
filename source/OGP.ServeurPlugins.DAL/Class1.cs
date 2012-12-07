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
        private static object lockFile = new object();

        public static void StorePlugins(IList<Plugin> plugins, string xml_file)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<Plugin>));
            using (StreamWriter xmlWriter = new StreamWriter(xml_file))
            {
                lock (lockFile)
                {
                    serializer.Serialize(xmlWriter, plugins);
                }
                xmlWriter.Close();
            }
        }

        public static List<Plugin> LoadPlugins(string xml_file)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<Plugin>));
            FileStream xmlFile = new FileStream(xml_file, FileMode.Open);
            return (List<Plugin>)serializer.Deserialize(xmlFile);
        }

    }
}
