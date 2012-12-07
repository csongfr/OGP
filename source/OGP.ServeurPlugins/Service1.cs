using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.IO;

using OGP.ServicePlugins.Modele;
using OGP.ServicePlugins.DAL;

namespace OGP.ServicePlugins
{

    public class ServeurPlugins : IServicePlugin
    {
        static private String Plugin_path = "plugins/";
        static private String XML_path = "ressources/";
        static private String XML_fileName = "plugins.xml";
        

        static IList<Plugin> plugins = initializePlugins();
        private static object lockPlugins = new object();


        public IList<Plugin> GetPluginList()
        {
            return plugins;
        }


        public bool AddPlugin(Plugin plug, MemoryStream memStream)
        {
            bool b;
            //Création du nom de dossier du plugin
            String rep = plug.Name + "/";
            plug.Id = plug.Name + "_" + plug.Version;
            plug.Dossier = plug.Name+"_"+plug.Version;
            
            lock (lockPlugins)
            {
                if (plug.Actif)
                {
                    foreach (Plugin p in plugins)
                    {
                        if (p.Name == plug.Name)
                        {
                            p.Actif = false;
                        }
                    }
                }
                plugins.Add(plug);

                if (!System.IO.Directory.Exists(Plugin_path))
                {
                    System.IO.Directory.CreateDirectory(Plugin_path);
                }

                if (!System.IO.Directory.Exists(Plugin_path + rep))
                {
                    System.IO.Directory.CreateDirectory(Plugin_path + rep);
                }

                if (!System.IO.Directory.Exists(Plugin_path + rep + plug.Dossier))
                {
                    FileStream fs = System.IO.File.Create(Plugin_path + rep + plug.Dossier);

                    putPlugin(memStream, fs);

                    fs.Close();

                    XML_DAL.StorePlugins(plugins, XML_path + XML_fileName);
                    b = true;
                }
                else b=false;
            }
            return b;
        }

        public bool RemovePlugin(string id)
        {
            throw new NotImplementedException();
        }

        public bool UpdatePlugin(Plugin p)
        {
            throw new NotImplementedException();
        }

        public IList<Plugin> CheckNewVersion(IList<Plugin> plugs)
        {
            IList<Plugin> res = new List<Plugin>();
            lock (plugins)
            {
                foreach (Plugin p in plugs)
                {
                    Plugin up = getNewVersion(p);
                    if (up != null)
                    {
                        res.Add(up);
                    }
                }
            }
            return res;
        }

        private Plugin getNewVersion(Plugin plug)
        {
            lock (plugins)
            {
                return plugins.Where(p => p.Name == plug.Name && p.Actif)
                    .FirstOrDefault();
            }
        }

        public bool DownloadPlugin(string id)
        {
            throw new NotImplementedException();
        }

        static private IList<Plugin> initializePlugins(){
            IList<Plugin> res = new List<Plugin>();

            try
            {
                res = XML_DAL.LoadPlugins(XML_path + XML_fileName);
            } catch
            {
                if (!System.IO.Directory.Exists(XML_path))
                {
                    System.IO.Directory.CreateDirectory(XML_path);
                }

                XML_DAL.StorePlugins(res, XML_path + XML_fileName);
            }

            return res;
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
