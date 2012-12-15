using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.IO;
using OGP.ServicePlugins.Modele;

namespace OGP.ServicePlugins
{
    [ServiceContract]
    public interface IServicePlugin
    {
        [OperationContract]
        IList<PluginModel> GetPluginList();

        // TODO : ajouter paramètre pour transfert fichier
        [OperationContract]
        bool AddPlugin(PluginModel p, MemoryStream memStream);

        [OperationContract]
        bool RemovePlugin(string id);

        [OperationContract]
        bool UpdatePlugin(PluginModel p, MemoryStream memStream);

        [OperationContract]
        IList<PluginModel> CheckNewVersion(IList<PluginModel> plugs);

        // TODO : ajouter paramètre pour transfert fichier
        [OperationContract]
        MemoryStream DownloadPlugin(string id);
    }
    /*
    // REMARQUE : vous pouvez utiliser la commande Renommer du menu Refactoriser pour changer le nom d'interface "IService1" à la fois dans le code et le fichier de configuration.
    [ServiceContract]
    public interface IService1
    {
        [OperationContract]
        string GetData(int value);

        [OperationContract]
        CompositeType GetDataUsingDataContract(CompositeType composite);

        // TODO: ajoutez vos opérations de service ici
    }

    // Utilisez un contrat de données (comme illustré dans l'exemple ci-dessous) pour ajouter des types composites aux opérations de service.
    // Vous pouvez ajouter des fichiers XSD au projet. Une fois le projet généré, vous pouvez utiliser directement les types de données qui y sont définis, avec l'espace de noms "OGP.ServeurPlugins.ContractType".
    [DataContract]
    public class CompositeType
    {
        bool boolValue = true;
        string stringValue = "Hello ";

        [DataMember]
        public bool BoolValue
        {
            get { return boolValue; }
            set { boolValue = value; }
        }

        [DataMember]
        public string StringValue
        {
            get { return stringValue; }
            set { stringValue = value; }
        }
    }*/
}
