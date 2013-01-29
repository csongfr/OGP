using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace OGP.ServicePlugin.Modele
{
    [DataContract]
    public class PluginModel
    {
        [DataMember]
		public string Id {get; set;}
        
        [DataMember]
        public string Name { get; set; }
        
        [DataMember]
        public string Version { get; set; }

        [DataMember]
        public string Description { get; set; }
        
        [IgnoreDataMemberAttribute]
        public string Location { get; set; }

        [DataMember]
        public bool Actif {get; set;}

        public override bool Equals(object obj)
        {
            if (obj is PluginModel)
            {
                PluginModel other = (PluginModel)obj;
                return (this.Name == other.Name);
            }
            else return false;
        }


        public PluginModel()
        {
        }

        public PluginModel(string id, string name, string version, string description, string dossier, bool actif)
        {
            this.Id = id;
            this.Name = name;
            this.Version = version;
            this.Description = description;
            this.Location = dossier;
            this.Actif = actif;
        }
    }
}
