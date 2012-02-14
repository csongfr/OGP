using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Plugin.Todolist.ValueObjects;

namespace Plugin.Todolist.Service
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
        VOProjet ChagerListeTaches(string nomFichier);

        /// <summary>
        /// Méthode qui enregistre la liste des tâches
        /// </summary>
        /// <param name="maToDoList">Nom de la ToDoList à enregistrer</param>
        /// <param name="messageErreurnregistrer">retourne un message d'erreur si elle ne peut être enregistrer</param>
        /// <returns>VOToDoList</returns>
        [OperationContract]
        VOProjet EnregistrerToDoList(VOProjet maToDoList,out string messageErreurEnregistrer);

        /// <summary>
        /// Création d'un nouveau projet
        /// </summary>
        /// <param name="nomProjet">Le nom du projet</param>
        /// <param name="messageErreur">Message d'erreur.</param>
        /// <returns>VOToDoList</returns>
        [OperationContract]
        VOProjet NouvelleToDoList(string nomProjet, out string messageErreur);

        /// <summary>
        /// Chargement des fichiers
        /// </summary>
        /// <returns>liste des fichiers chargés et désérialisés</returns>
        [OperationContract]
        List<VOProjet> ChargementFichiers();

    }
}
