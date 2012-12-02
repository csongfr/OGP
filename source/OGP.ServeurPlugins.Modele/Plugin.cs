using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OGP.ServicePlugins.Modele
{
    public class Plugin
    {
		private string Id {get; private set;}
        private string Name { get; private set; }
        private string Version { get; private set; }
        private string Description { get; private set; }
        private string Dossier { get; private set; }

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
