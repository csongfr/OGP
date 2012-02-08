using System;
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
        VOToDoList ChargerFichier(string nomFichier);

        /// <summary>
        /// Prototype de la méthode permettant d'ajouter une tâche
        /// </summary>
        /// <param name="nouvelleToDoList"></param>
        /// <param name="nouvelleTache"></param>
        void AjouterTache(VOToDoList nouvelleToDoList, VOTache nouvelleTache);
    }
}
