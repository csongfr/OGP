﻿using System;
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
        

        static IList<PluginModel> plugins = initializePlugins();
        private static object lockPlugins = new object();


        public IList<PluginModel> GetPluginList()
        {
            return plugins;
        }


        public bool AddPlugin(PluginModel plug, MemoryStream memStream)
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
                    foreach (PluginModel p in plugins)
                    {
                        if (p.Name == plug.Name)
                            p.Actif = false;
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
                    File_DAL.putPlugin(memStream, Plugin_path + rep + plug.Dossier);

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

        public bool UpdatePlugin(PluginModel p, MemoryStream memStream)
        {
            throw new NotImplementedException();
        }

        public IList<PluginModel> CheckNewVersion(IList<PluginModel> plugs)
        {
            IList<PluginModel> res = new List<PluginModel>();
            lock (lockPlugins)
            {
                foreach (PluginModel p in plugs)
                {
                    PluginModel up = getNewVersion(p);
                    if (up != null)
                    {
                        res.Add(up);
                    }
                }
            }
            return res;
        }

        private PluginModel getNewVersion(PluginModel plug)
        {
            lock (lockPlugins)
            {
                return plugins.Where(p => p.Name == plug.Name && p.Actif)
                    .FirstOrDefault();
            }
        }

        public MemoryStream DownloadPlugin(string id)
        {
            PluginModel plug = plugins.Where(p => p.Id.Equals(id)).First();
            
            return File_DAL.getPlugin(Plugin_path+plug.Name+"/" + plug.Dossier);
        }

        static private IList<PluginModel> initializePlugins(){
            IList<PluginModel> res = new List<PluginModel>();

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
    }
}
