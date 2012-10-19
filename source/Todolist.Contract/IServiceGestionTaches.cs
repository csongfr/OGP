using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Plugin.Todolist.ValueObjects;

namespace Todolist.Commun
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
        /// <param name="toDoList">Nom de la ToDoList à enregistrer</param>
        /// <returns>VOToDoList</returns>
        [OperationContract]
        VOProjet EnregistrerToDoList(VOProjet toDoList);

        /// <summary>
        /// Création d'un nouveau projet
        /// </summary>
        /// <param name="nomProjet">Le nom du projet</param>
        /// <returns>VOToDoList</returns>
        [OperationContract]
        VOProjet NouvelleToDoList(string nomProjet);

        /// <summary>
        /// Chargement des fichiers
        /// </summary>
        /// <returns>liste des fichiers chargés et désérialisés</returns>
        [OperationContract]
        List<VOProjet> ChargementFichiers();
    }
}
