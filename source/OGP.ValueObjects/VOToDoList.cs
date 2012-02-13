using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace OGP.ValueObjects
{
    /// <summary>
    /// Classe correspondant à la ToDoList
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
    [System.SerializableAttribute]
    [System.Diagnostics.DebuggerStepThroughAttribute]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    public class VOTodolist
    {
        /// <summary>
        /// Gets et sets des tâches de la ToDoList
        /// </summary>
        public List<VOTache> ListeDesTaches { get; set; }

        /// <summary>
        /// Gets et sets des personnes affectées au projet
        /// </summary>
        public List<VOPersonne> Personnes { get; set; }

        /// <summary>
        /// Gets et sets des différentes catégories du projet
        /// </summary>
        public List<VOCategorie> Categories { get; set; }

        /// <summary>
        /// Gets et sets du nom du projet
        /// </summary>
        public string NomDuProjet { get; set; }

        /// <summary>
        /// Gets et sets de la date de la derniere modification
        /// </summary>
        public DateTime DateDerniereModif { get; set; }

        /// <summary>
        /// Constructeur par défaut
        /// </summary>
        public VOTodolist()
        { 
        }

        /// <summary>
        /// Constructeur d'une ToDoList à partir du nom du projet
        /// </summary>
        /// <param name="nomProjet">Nom du projet</param>
        public VOTodolist(string nomProjet)
        {
            this.NomDuProjet = nomProjet;
            this.DateDerniereModif = DateTime.Now;

            // Initialisation de la liste des taches
            this.ListeDesTaches = new List<VOTache>();
        }
    }
}
