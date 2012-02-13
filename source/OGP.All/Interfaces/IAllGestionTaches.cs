﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OGP.ValueObjects;

namespace OGP.All
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
        VOTodolist ChargerFichier(string nomFichier);

        /// <summary>
        /// Prototype de la méthode permettant d'ajouter une tâche
        /// </summary>
        /// <param name="nouvelleToDoList">projet dans lequel on ajoute une tâche</param>
        /// <param name="nouvelleTache"> la tâche à ajouter</param>
        void AjouterTache(VOTodolist nouvelleToDoList, VOTache nouvelleTache);

        /// <summary>
        /// Création d'une nouvelle gestion de projet
        /// </summary>
        /// <param name="nomProjet">Nom du projet</param>
        /// <param name="messageErreur">Message d'erreur.</param>
        /// <returns>VOToDoList</returns>
        VOTodolist NouvelleGestionTaches(string nomProjet, out string messageErreur);

        /// <summary>
        /// Désérialisation des fichiers du dossier grâce à leurs chemins
        /// </summary>
        /// <returns>Liste des fichiers désérialisés dans l'ordre du plus récent au plus vieux</returns>
        List<VOTodolist> ObtenirTousLesFichiers();
    }
}
