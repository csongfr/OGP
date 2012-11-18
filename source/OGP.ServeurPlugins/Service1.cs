using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using OGP.ServeurPlugins.Modele;

namespace OGP.ServeurPlugins
{
    // REMARQUE : vous pouvez utiliser la commande Renommer du menu Refactoriser pour changer le nom de classe "Service1" à la fois dans le code et le fichier de configuration.
    public class ServeurPlugins : IServeurPlugins
    {

        public ICollection<Plugin> getPluginList()
        {
            throw new NotImplementedException();
        }

        public bool addPlugin(Plugin p)
        {
            throw new NotImplementedException();
        }

        public bool removePlugin(string id)
        {
            throw new NotImplementedException();
        }

        public bool updatePlugin(Plugin p)
        {
            throw new NotImplementedException();
        }

        public ICollection<Plugin> checkNewVersion(ICollection<Plugin> plugs)
        {
            throw new NotImplementedException();
        }

        public bool downloadPlugin(string id)
        {
            throw new NotImplementedException();
        }
    }
}
