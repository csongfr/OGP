using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OGP.ServicePlugins.Modele
{
    public class Plugin
    {
		public string Id {get; private set;}
        public string Name { get; private set; }
        public string Version { get; private set; }
        public string Description { get; private set; }
        public string Dossier { get; private set; }

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
