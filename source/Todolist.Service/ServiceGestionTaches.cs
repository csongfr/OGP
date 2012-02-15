using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Plugin.Todolist.All;
using Plugin.Todolist.ValueObjects;

namespace Plugin.Todolist.Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.

    /// <summary>
    /// Classe permettant la gestion des tâches
    /// </summary>
    public class ServiceGestionTaches : IServiceGestionTaches
    {
        #region Méthodes publiques

        /// <summary>
        /// Méthode qui charge la liste des tâches
        /// </summary>
        /// <param name="nomFichier">Nom du fichier à charger.</param>
        /// <returns>Liste des taches.</returns>
        public VOProjet ChagerListeTaches(string nomFichier)
        {
            IAllGestionTaches allGestionTaches = AllFactory.GetAllGestionTaches();

            return allGestionTaches.ChargerFichier(nomFichier);
        }

        /// <summary>
        /// Méthode qui enregistre les modifications du projet
        /// </summary>
        /// <param name="toDoList">Nom de ma ToDoList</param>
        /// <returns>VOToDoList</returns>
        public VOProjet EnregistrerToDoList(VOProjet toDoList)
        {
            IAllGestionTaches allGestionTaches = AllFactory.GetAllGestionTaches();

            return allGestionTaches.EnregistrerNouvelleToDoList(toDoList);
        }

        /// <summary>
        /// Création d'un nouveau projet
        /// </summary>
        /// <param name="nomProjet">Le nom du projet</param>
        /// <returns>VOToDoList</returns>
        public VOProjet NouvelleToDoList(string nomProjet)
        {
            IAllGestionTaches allGestionTaches = AllFactory.GetAllGestionTaches();

            return allGestionTaches.NouvelleGestionTaches(nomProjet);
        }

        /// <summary>
        /// Chargement des fichiers
        /// </summary>
        /// <returns>liste des fichiers chargés et désérialisés</returns>
        public List<VOProjet> ChargementFichiers()
        {
            IAllGestionTaches allGestionTaches = AllFactory.GetAllGestionTaches();
            return allGestionTaches.ObtenirTousLesFichiers();
        }

        #endregion
    }
}
