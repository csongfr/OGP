using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OGP.ValueObjects;
using System.Configuration;
using System.IO;
using System.Reflection;

namespace OGP.All
{
    /// <summary>
    /// Service permettant la gestion des taches
    /// </summary>
    public class AllGestionTaches : IAllGestionTaches
    {
        /// <summary>
        /// Méthode permettant de gérer le chargement d'un fichier
        /// </summary>
        /// <param name="nomFichier">Nom du Fichier.</param>
        /// <returns>Liste de taches.</returns>
        public VOToDoList ChargerFichier(string nomFichier)
        {
                      
            //FileStream fichierIn = new FileStream();
            return null;
        }

        /// <summary>
        /// Méthode permettant d'ajouter une tache
        /// </summary>
        /// <param name="nouvelleToDoList"></param>
        /// <param name="nouvelleTache"></param>
        public void AjouterTache(VOToDoList nouvelleToDoList, VOTache nouvelleTache)
        { 
            nouvelleToDoList.ListeDesTaches.Add(nouvelleTache);
        }
    }
}
