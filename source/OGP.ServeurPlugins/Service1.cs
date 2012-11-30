using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Xml.Serialization;
using System.IO;
using OGP.ServicePlugins.Modele;

namespace OGP.ServicePlugins
{
    // REMARQUE : vous pouvez utiliser la commande Renommer du menu Refactoriser pour changer le nom de classe "Service1" à la fois dans le code et le fichier de configuration.

    public class ServeurPlugins : IServicePlugin
    {
        static private String XML_path = "ressources/";
        static private String XML_fileName = "plugins.xml";
        
        IList<Plugin> plugins = XML_getPlugins();

        public IList<Plugin> getPluginList()
        {
            IList<Plugin> res = new List<Plugin>();

            foreach (Plugin p in plugins)
            {
                res.Add(new Plugin(p.Id, p.Name, p.Version, p.Description, ""));
            }

            return res;
        }

        public bool addPlugin(Plugin p)
        {
            plugins.Add(p);
            XML_storePlugins();
            
            return true;
        }

        public bool removePlugin(string id)
        {
            throw new NotImplementedException();
        }

        public bool updatePlugin(Plugin p)
        {
            throw new NotImplementedException();
        }

        public IList<Plugin> checkNewVersion(IList<Plugin> plugs)
        {
            IList<Plugin> res = new List<Plugin>();
            foreach (Plugin p in plugs)
            {
                Plugin up = getNewVersion(p);
                if(up!=null)
                    res.Add(up);
            }
            return res;
        }

        private Plugin getNewVersion(Plugin plug)
        {
            foreach (Plugin p in plugins)
            {
                if (p.Name == plug.Name)
                    if (versionSup(p.Version, plug.Version)) //Passage à p.supVersion ?
                        return p;
            }
            return null;
        }

        private bool versionSup(string p1, string p2)
        {
            throw new NotImplementedException();
        }

        public bool downloadPlugin(string id)
        {
            throw new NotImplementedException();
        }

        static private IList<Plugin> XML_getPlugins(){
            IList<Plugin> res = new List<Plugin>();

            XmlSerializer serializer = new XmlSerializer(typeof(Plugin));
            FileStream xmlFile = new FileStream(XML_path + XML_fileName, FileMode.Open);

            res = (IList<Plugin>) serializer.Deserialize(xmlFile);
            xmlFile.Close();
                
            return res;
        }

        private void XML_storePlugins()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Plugin));
            StreamWriter xmlFile = new StreamWriter(XML_path + XML_fileName);
            serializer.Serialize(xmlFile,plugins);
            xmlFile.Close();

        }



    }
}
