using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Plugin.Todolist.ValueObjects
{
    /// <summary>
    /// Classe correspondant à la ToDoList
    /// </summary>
    public class VOProjet
    {
        /// <summary>
        /// Gets et sets des tâches de la ToDoList
        /// </summary>
        public ObservableCollection<VOTache> ListeDesTaches { get; set; }

        /// <summary>
        /// Gets et sets des personnes affectées au projet
        /// </summary>
        public ObservableCollection<VOPersonne> Personnes { get; set; }

        /// <summary>
        /// Gets et sets des différentes catégories du projet
        /// </summary>
        public ObservableCollection<VOCategorie> Categories { get; set; }

        /// <summary>
        /// Gets et sets du nom du projet
        /// </summary>
        public string NomDuProjet { get; set; }

        /// <summary>
        /// Chemin du projet
        /// </summary>
        [XmlIgnore]
        public string CheminDuProjet { get; set; }

        /// <summary>
        /// Gets et sets de la date de la derniere modification
        /// </summary>
        public DateTime DateDerniereModif { get; set; }

        /// <summary>
        /// Constructeur par défaut
        /// </summary>
        public VOProjet()
        { 
        }

        /// <summary>
        /// Constructeur d'une ToDoList à partir du nom du projet
        /// </summary>
        /// <param name="nomProjet">Nom du projet</param>
        /// <param name="nomFichier">Nom du fichier</param>
        public VOProjet(string nomProjet, string nomFichier)
        {
            this.NomDuProjet = nomProjet;
            this.CheminDuProjet = nomFichier;
            this.DateDerniereModif = DateTime.Now;

            // Initialisation de la liste des personnes
            this.Personnes = new ObservableCollection<VOPersonne>();
        }
    }
}
