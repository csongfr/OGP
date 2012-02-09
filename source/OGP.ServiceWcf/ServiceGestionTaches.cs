﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using OGP.All;
using OGP.ValueObjects;

namespace OGP.ServiceWcf
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.

    /// <summary>
    /// Classe permettant la gestion des tâches
    /// </summary>
    public class ServiceGestionTaches : IServiceGestionTaches
    {
        /// <summary>
        /// Méthode qui charge la liste des tâches
        /// </summary>
        /// <param name="nomFichier">Nom du fichier à charger.</param>
        /// <returns>Liste des taches.</returns>
        public VOToDoList ChagerListeTaches(string nomFichier)
        {
            IAllGestionTaches allGestionTaches = AllFactory.GetAllGestionTaches();

            return allGestionTaches.ChargerFichier(nomFichier);
        }

        /// <summary>
        /// Création d'un nouveau projet
        /// </summary>
        /// <param name="nomFichier">Le nom du fichier</param>
        /// <param name="nomProjet">Le nom du projet</param>
        /// <returns>VOToDoList</returns>
        public VOToDoList NouvelleToDoList(string nomFichier, string nomProjet)
        {
            IAllGestionTaches allGestionTaches = AllFactory.GetAllGestionTaches();

            return allGestionTaches.NouvelleGestionTaches(nomFichier, nomProjet);
        }
    }
}