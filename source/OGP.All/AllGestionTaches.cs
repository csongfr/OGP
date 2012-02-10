using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml.Serialization;
using OGP.Bll;
using OGP.ValueObjects;

namespace OGP.All
{
    /// <summary>
    /// Service permettant la gestion des taches
    /// </summary>
    public class AllGestionTaches : IAllGestionTaches
    {
        #region méthodes publiques
        /// <summary>
        /// Méthode permettant de gérer le chargement d'un fichier
        /// </summary>
        /// <param name="nomFichier">Nom du Fichier.</param>
        /// <returns>Liste de taches.</returns>
        public VOToDoList ChargerFichier(string nomFichier)
        {
            // FileStream fichierIn = new FileStream();
            return null;
        }

        /// <summary>
        /// Méthode permettant d'ajouter une tache
        /// </summary>
        /// <param name="nouvelleToDoList">Projet auquel on ajoute une tâche</param>
        /// <param name="nouvelleTache">tâche à ajouter</param>
        public void AjouterTache(VOToDoList nouvelleToDoList, VOTache nouvelleTache)
        {
            nouvelleToDoList.ListeDesTaches.Add(nouvelleTache);
        }

        /// <summary>
        /// Création d'une nouvelle gestion de projet
        /// </summary>
        /// <param name="nomProjet">Nom du projet</param>
        /// <returns>VOToDoList</returns>
        public VOToDoList NouvelleGestionTaches(string nomProjet)
        {
            // Ouverture du dossier
            var section = ConfigurationManager.GetSection("gestionTaches") as NameValueCollection;
            string repertoire = section["repertoireStockage"].ToString();

            nomProjet = nomProjet + Constants.ExtensionFichierXml;
            // Création du dossier si il n'existe pas
            if (!Directory.Exists(nomProjet))
            {
                Directory.CreateDirectory(repertoire);
            }

            // Obtention du chemin complet
            string cheminComplet = Path.Combine(repertoire, nomProjet);

            // Dossier existant?
            bool verif = DossierExistant(cheminComplet);
            if (verif == true)
            {
                return null;
            }
            else
            {
                return BllFactory.GetBllGestionTaches().CreerFichierTachesXml(cheminComplet);
            }
        }

        /// <summary>
        /// Désérialisation des fichiers du dossier grâce à leurs chemins
        /// </summary>
        /// <returns>Liste des fichiers désérialisés dans l'ordre du plus récent au plus vieux</returns>
        public List<VOToDoList> ObtenirTousLesFichiers()
        {
            // Ouverture du dossier
            var section = ConfigurationManager.GetSection("gestionTaches") as NameValueCollection;
            string repertoire = section["repertoireStockage"].ToString();

            // Création d'une liste contenant les chemins complets de chaque fichiers
            string[] tableauFichiersExistants = Directory.GetFiles(repertoire);
            List<string> listeFichiersExistants = tableauFichiersExistants.ToList();

            // Retourne les fichiers désérialisés et classés
            return BllFactory.GetBllGestionTaches().DeserialisationFichiers(listeFichiersExistants).OrderBy(tdl => tdl.DateDerniereModif).ToList();
        }
        #endregion

        #region Méthodes privées
        /// <summary>
        /// Vérifie l'existance d'un projet identique
        /// </summary>
        /// <param name="nomChemin">Nom du projet à ajouter</param>
        /// <returns>Vrai si le fichier existe, faux sinon</returns>
        private bool DossierExistant(string nomChemin)
        {
            if (File.Exists(nomChemin))
            {
                return true;
            }

            return false;
        }
        #endregion 
    }
}
