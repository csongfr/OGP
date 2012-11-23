using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OGP.ServeurPlugins.Modele
{
    public class Plugin
    {
        string id;
        string name;
        string version;
        string description;
        string dossier;


        public string Id {get;}
        public string Name { get; }
        public string Version { get; }
        public string Description { get; }
        public string Dossier { get; }


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
