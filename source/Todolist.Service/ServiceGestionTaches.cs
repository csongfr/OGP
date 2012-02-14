﻿using System;
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
        /// <param name="maToDoList">Nom de ma ToDoList</param>
        /// <param name="messageErreurEnregistrer">Message d'erreur</param>
        /// <returns>VOToDoList</returns>
        public VOProjet EnregistrerToDoList(VOProjet maToDoList, out string messageErreurEnregistrer)
        {
            IAllGestionTaches allGestionTaches = AllFactory.GetAllGestionTaches();

            return allGestionTaches.EnregistrerNouvelleToDoList(maToDoList,out messageErreurEnregistrer);
        }

        /// <summary>
        /// Création d'un nouveau projet
        /// </summary>
        /// <param name="nomProjet">Le nom du projet</param>
        /// <param name="messageErreur">Message d'erreur.</param>
        /// <returns>VOToDoList</returns>
        public VOProjet NouvelleToDoList(string nomProjet, out string messageErreur)
        {
            IAllGestionTaches allGestionTaches = AllFactory.GetAllGestionTaches();

            return allGestionTaches.NouvelleGestionTaches(nomProjet, out messageErreur);
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
