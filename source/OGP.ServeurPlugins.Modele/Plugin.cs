﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OGP.ServicePlugins.Modele
{
    public class Plugin
    {
        string id;
        string name;
        string version;
        string description;
        string dossier;


        public string Id
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
            }
        }
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }
        public string Version
        {
            get
            {
                return version;
            }
            set
            {
                version = value;
            }
        }
        public string Description
        {
            get
            {
                return description;
            }
            set
            {
                description = value;
            }
        }
        public string Dossier
        {
            get
            {
                return dossier;
            }
            set
            {
                dossier = value;
            }
        }


        public Plugin()
        {
        }

        public Plugin(string id, string name, string version, string description, string dossier)
        {
            this.id = id;
            this.name = name;
            this.version = version;
            this.description = description;
            this.dossier = dossier;
        }
    }
}
