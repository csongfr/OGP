using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OGP.ServicePlugins.Modele
{
    public class Plugin
    {
		private string id;
		private string name;
		private string version;
		private string description;
		private string dossier;

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
            this.Id = id;
            this.Name = name;
            this.Version = version;
            this.Description = description;
            this.Dossier = dossier;
        }
    }
}
