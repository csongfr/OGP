using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml.Serialization;
using Plugin.Todolist.Bll;
using Plugin.Todolist.ValueObjects;

namespace Plugin.Todolist.All
{
    /// <summary>
    /// Service permettant la gestion des taches
    /// </summary>
    public class AllGestionTaches : IAllGestionTaches
    {
        #region Méthodes publiques

        /// <summary>
        /// Méthode permettant de gérer le chargement d'un fichier
        /// </summary>
        /// <param name="nomFichier">Nom du Fichier.</param>
        /// <returns>Liste de taches.</returns>
        public VOProjet ChargerFichier(string nomFichier)
        {
            // FileStream fichierIn = new FileStream();
            return null;
        }

        /// <summary>
        /// Méthode permettant d'ajouter une tache
        /// </summary>
        /// <param name="nouvelleToDoList">Projet auquel on ajoute une tâche</param>
        /// <param name="nouvelleTache">tâche à ajouter</param>
        public void AjouterTache(VOProjet nouvelleToDoList, VOTache nouvelleTache)
        {
            nouvelleToDoList.ListeDesTaches.Add(nouvelleTache);
        }

        /// <summary>
        /// Enregistrement de ma ToDolist
        /// </summary>
        /// <param name="ToDoList"> ma ToDoList</param>
        /// <param name="messageErreurEnregistrer">Message d'erreur</param>
        /// <returns>VOToDoList</returns>
        public VOProjet EnregistrerNouvelleToDoList(VOProjet ToDoList, out string messageErreurEnregistrer)
        {
            // Initialisation du message d'erreur
            messageErreurEnregistrer = string.Empty;

            string nomProjet = ToDoList.NomDuProjet;

            // Ouverture du dossier
            var section = ConfigurationManager.GetSection("gestionTaches") as NameValueCollection;
            string repertoire = section["repertoireStockage"].ToString();

            // Obtention du chemin complet
            string cheminComplet = Path.Combine(repertoire, nomProjet + Constants.ExtensionFichierXml);

            return BllFactory.GetBllGestionTaches().ModifierFichierTachesXml(cheminComplet, ToDoList); 
        }

        /// <summary>
        /// Création d'une nouvelle gestion de projet
        /// </summary>
        /// <param name="nomProjet">Nom du projet</param>
        /// <param name="messageErreur">Message d'erreur.</param>
        /// <returns>VOToDoList</returns>
        public VOProjet NouvelleGestionTaches(string nomProjet, out string messageErreur)
        {
            // Initialisation du message d'erreur
            messageErreur = string.Empty;

            // Ouverture du dossier
            var section = ConfigurationManager.GetSection("gestionTaches") as NameValueCollection;
            string repertoire = section["repertoireStockage"].ToString();

            // Obtention du chemin complet
            string cheminComplet = Path.Combine(repertoire, nomProjet + Constants.ExtensionFichierXml);

            // Création du dossier si il n'existe pas
            if (!Directory.Exists(repertoire))
            {
                Directory.CreateDirectory(repertoire);
            }
            // Dossier existant?
            bool verif = DossierExistant(cheminComplet);
            if (verif == true)
            {
                messageErreur = "Erreur, il existe déjà un projet de ce nom.";
                return null;
            }
            else
            {
                return BllFactory.GetBllGestionTaches().CreerFichierTachesXml(nomProjet, cheminComplet);
            }
        }

        /// <summary>
        /// Désérialisation des fichiers du dossier grâce à leurs chemins
        /// </summary>
        /// <returns>Liste des fichiers désérialisés dans l'ordre du plus récent au plus vieux</returns>
        public List<VOProjet> ObtenirTousLesFichiers()
        {
            // Ouverture du dossier
            var section = ConfigurationManager.GetSection("gestionTaches") as NameValueCollection;
            string repertoire = section["repertoireStockage"].ToString();
            
            // Création du dossier si il n'existe pas
            if (!Directory.Exists(repertoire))
            {
                return null;
            }
            else
            {
                // Création d'une liste contenant les chemins complets de chaque fichiers
                string[] tableauFichiersExistants = Directory.GetFiles(repertoire);
                List<string> listeFichiersExistants = tableauFichiersExistants.ToList();

                // Retourne les fichiers désérialisés et classés
                return BllFactory.GetBllGestionTaches().DeserialisationFichiers(listeFichiersExistants).OrderBy(tdl => tdl.DateDerniereModif).ToList();
            }
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
