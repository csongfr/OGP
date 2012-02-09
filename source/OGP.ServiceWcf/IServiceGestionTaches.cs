using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using OGP.ValueObjects;

namespace OGP.ServiceWcf
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.

    /// <summary>
    /// Interface pour le service de gestion des taches
    /// </summary>
    [ServiceContract]
    public interface IServiceGestionTaches
    {
        /// <summary>
        /// Méthode qui charge la liste des tâches
        /// </summary>
        /// <param name="nomFichier">Nom du fichier à charger.</param>
        /// <returns>Liste des taches.</returns>
        [OperationContract]
        VOToDoList ChagerListeTaches(string nomFichier);

        /// <summary>
        /// Création d'un nouveau projet
        /// </summary>
        /// <param name="nomFichier">le nom du fichier</param>
        /// <param name="nomProjet">Le nom du projet</param>
        /// <returns>VOToDoList</returns>
        [OperationContract]
        VOToDoList NouvelleToDoList(string nomFichier, string nomProjet);
    }
}
