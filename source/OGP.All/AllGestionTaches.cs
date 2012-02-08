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
        /// <param name="nomFichier">Nom du fichier</param>
        /// <param name="nomProjet">Nom du projet</param>
        /// <returns>VOToDoList</returns>
        public VOToDoList NouvelleGestionTaches(string nomFichier, string nomProjet)
        {
            VOToDoList nouvelleVOToDoList = new VOToDoList(nomProjet);

            var section = ConfigurationManager.GetSection("gestionTaches") as NameValueCollection;
            string repertoire = section["repertoireStockage"].ToString();

            if (!Directory.Exists(repertoire))
            {
                Directory.CreateDirectory(repertoire);
            }

            // Extension du fichier sous format 'xml'
            nomFichier = Path.Combine(repertoire, nomFichier + Constants.ExtensionFichierXml);

            BllFactory.GetBllGestionTaches().CreerFichierTachesXml(nomFichier, nomProjet);
            return nouvelleVOToDoList;
        }
    }
}
