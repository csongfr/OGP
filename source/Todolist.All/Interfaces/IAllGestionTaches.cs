using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Plugin.Todolist.ValueObjects;

namespace Plugin.Todolist.All
{
    /// <summary>
    /// Interface de l'All de gestion de taches
    /// </summary>
    public interface IAllGestionTaches
    {
        /// <summary>
        /// Méthode permettant de gérer le chargement d'un fichier
        /// </summary>
        /// <param name="nomFichier">Nom du Fichier.</param>
        /// <returns>Liste de taches.</returns>
        VOProjet ChargerFichier(string nomFichier);

        /// <summary>
        /// Prototype de la méthode permettant d'ajouter une tâche
        /// </summary>
        /// <param name="nouvelleToDoList">projet dans lequel on ajoute une tâche</param>
        /// <param name="nouvelleTache"> la tâche à ajouter</param>
        void AjouterTache(VOProjet nouvelleToDoList, VOTache nouvelleTache);

        /// <summary>
        /// Création d'une nouvelle gestion de projet
        /// </summary>
        /// <param name="nomProjet">Nom du projet</param>
        /// <param name="messageErreur">Message d'erreur.</param>
        /// <returns>VOToDoList</returns>
        VOProjet NouvelleGestionTaches(string nomProjet, out string messageErreur);

        /// <summary>
        /// Enregistre les nouvelles modifications
        /// </summary>
        /// <param name="maToDoList">Nom de ma ToDoList</param>
        /// <param name="messageErreurEnregistrer">Message d'erreur</param>
        /// <returns>VOToDoList</returns>
        VOProjet EnregistrerNouvelleToDoList(VOProjet maToDoList, out string messageErreurEnregistrer);

        /// <summary>
        /// Désérialisation des fichiers du dossier grâce à leurs chemins
        /// </summary>
        /// <returns>Liste des fichiers désérialisés dans l'ordre du plus récent au plus vieux</returns>
        List<VOProjet> ObtenirTousLesFichiers();

    }
}
