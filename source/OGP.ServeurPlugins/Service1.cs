using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Xml.Serialization;
using System.IO;
using OGP.ServeurPlugins.Modele;

namespace OGP.ServeurPlugins
{
    // REMARQUE : vous pouvez utiliser la commande Renommer du menu Refactoriser pour changer le nom de classe "Service1" à la fois dans le code et le fichier de configuration.
    public class ServeurPlugins : IServeurPlugins
    {
        static String XML_path = "ressources/";
        static String XML_fileName = "plugins.xml";
        ICollection<Plugin> plugins = XML_getPlugins();

        public ICollection<Plugin> getPluginList()
        {
            ICollection<Plugin> res = new List<Plugin>();

            foreach (Plugin p in plugins)
            {
                res.Add(new Plugin(p.Id, p.Name, p.Version, p.Description, ""));
            }

            return res;
        }

        public bool addPlugin(Plugin p)
        {
            throw new NotImplementedException();
        }

        public bool removePlugin(string id)
        {
            throw new NotImplementedException();
        }

        public bool updatePlugin(Plugin p)
        {
            throw new NotImplementedException();
        }

        public ICollection<Plugin> checkNewVersion(ICollection<Plugin> plugs)
        {
            throw new NotImplementedException();
        }

        public bool downloadPlugin(string id)
        {
            throw new NotImplementedException();
        }

        static private ICollection<Plugin> XML_getPlugins(){
            ICollection<Plugin> res = new List<Plugin>();

            XmlSerializer serializer = new XmlSerializer(typeof(Plugin));
            FileStream xmlFile = new FileStream(XML_path + XML_fileName, FileMode.Open);

            res = (ICollection<Plugin>) serializer.Deserialize(xmlFile);

            return res;
        }


    }
}
