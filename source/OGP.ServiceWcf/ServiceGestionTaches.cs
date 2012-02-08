﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using OGP.ValueObjects;
using OGP.All;

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
    }
}
