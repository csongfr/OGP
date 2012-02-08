using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace OGP.ValueObjects
{
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]

    /// <summary>
    /// Classe correspondant à la ToDoList
    /// </summary>
    public class VOToDoList
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
        public VOToDoList()
        { 
        }

        public VOToDoList(string nomProjet)
        {
            this.NomDuProjet = nomProjet;
            this.DateDerniereModif = DateTime.Now;
        }
    }
}
