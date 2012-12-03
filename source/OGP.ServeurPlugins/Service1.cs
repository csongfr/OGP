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
        static private String Plugin_path = "plugins/";
        static private String XML_path = "ressources/";
        static private String XML_fileName = "plugins.xml";
        

        static IList<Plugin> plugins = XML_getPlugins();



        public IList<Plugin> getPluginList()
        {
            return plugins;
        }

        public bool addPlugin(Plugin p, MemoryStream memStream)
        {
            bool b;
            //Création du nom de dossier du plugin
            String rep = p.Name + "/";
            p.Dossier = p.Name+"_"+p.Version;
            
            lock (plugins)
            {
                plugins.Add(p);

                if (!System.IO.Directory.Exists(Plugin_path))
                    System.IO.Directory.CreateDirectory(Plugin_path);

                if (!System.IO.Directory.Exists(Plugin_path + rep))
                    System.IO.Directory.CreateDirectory(Plugin_path + rep);

                if (!System.IO.Directory.Exists(Plugin_path + rep + p.Dossier))
                {
                    FileStream fs = System.IO.File.Create(Plugin_path + rep + p.Dossier);

                    putPlugin(memStream, fs);

                    fs.Close();

                    XML_storePlugins();
                    b = true;
                }
                else b=false;
            }
            return b;
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
            lock (plugins)
            {
                foreach (Plugin p in plugs)
                {
                    Plugin up = getNewVersion(p);
                    if (up != null)
                        res.Add(up);
                }
            }
            return res;
        }

        //Une version par plugin sur le serveur
        private Plugin getNewVersion(Plugin plug)
        {
            Plugin new_plug=null;
            lock (plugins)
            {
                foreach (Plugin p in plugins)
                {
                    if (p.Name == plug.Name)
                        if (versionSup(p.Version, plug.Version)){ //Passage à p.supVersion ?
                            new_plug = p;
                            break;
                        }
                }
            }
            return new_plug;
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

            XmlSerializer serializer = new XmlSerializer(typeof(List<Plugin>));
            FileStream xmlFile=null;
            
            try
            {
                xmlFile = new FileStream(XML_path + XML_fileName, FileMode.Open);
                lock (XML_path)
                {
                    res = (IList<Plugin>)serializer.Deserialize(xmlFile);
                }
                xmlFile.Close();
            } catch (Exception e)
            {
                StreamWriter xmlWriter = new StreamWriter(XML_path + XML_fileName);
                lock (XML_path)
                {
                    if (!System.IO.Directory.Exists(XML_path))
                        System.IO.Directory.CreateDirectory(XML_path);

                    serializer.Serialize(xmlWriter, res);
                }
                xmlWriter.Close();
            }
            finally{
                if(xmlFile!=null)
                    xmlFile.Close();
            }
            
           return res;
        }

        private void XML_storePlugins()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<Plugin>));
            StreamWriter xmlFile = new StreamWriter(XML_path + XML_fileName);
            lock (XML_path)
            {
                if (!System.IO.Directory.Exists(XML_path))
                    System.IO.Directory.CreateDirectory(XML_path);

                serializer.Serialize(xmlFile, plugins);
            }
            xmlFile.Close();

        }

        private void putPlugin(MemoryStream memStream, FileStream filePlugin)
        {
            int cpt = 0;

            while (cpt < memStream.Length)
            {
                filePlugin.WriteByte(Convert.ToByte(memStream.ReadByte()));
                cpt++;
            }            
        }
    }
}
