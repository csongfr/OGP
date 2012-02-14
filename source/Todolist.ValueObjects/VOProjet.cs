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
    public class VOProjet
    {
        /// <summary>
        /// Gets et sets des tâches de la ToDoList
        /// </summary>
        public List<VOTache> ListeDesTaches { get; set; }

        /// <summary>
        /// Gets et sets des personnes affectées au projet
        /// </summary>
        public List<VOPersonne> Personnes/*Projet*/ { get; set; }

        /// <summary>
        /// Gets et sets des différentes catégories du projet
        /// </summary>
        public List<VOCategorie> Categories { get; set; }

        /// <summary>
        /// Gets et sets du nom du projet
        /// </summary>
        public string NomDuProjet { get; set; }

        /// <summary>
        /// Chemin du projet
        /// </summary>
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
        public VOProjet(string nomProjet, string nomFichier)
        {
           this.NomDuProjet = nomProjet;
           this.CheminDuProjet = nomFichier;
            this.DateDerniereModif = DateTime.Now;

            // Initialisation de la liste des taches
            this.ListeDesTaches = new List<VOTache>();
          
            /*  this.Personnes = new List<VOPersonne>();
             this.Categories = new List<VOCategorie>();
             VOTache ta = new VOTache();
             ta.ListeDesPersonnes=new List<Guid>();
             ta.ListeDesCategories = new List<VOCategorie>();
             ta.Titre="trop facile";
             ta.PrioriteDeLaTache = EnumPriorite.Normal;
             ta.PourcentageEffectue = 50;
             ta.Estimation = 102;
             ta.TpsDepense = 150;
             ta.DateLimite = DateTime.Now;
             VOPersonne test = new VOPersonne();
             test.Nom = "Florian";

             test.Identifiant =Guid.NewGuid();
             VOPersonne test1 = new VOPersonne();
             test1.Nom = "matthieu";

             test1.Identifiant = Guid.NewGuid();
             Personnes.Add(test);
             Personnes.Add(test1);
             ta.ListeDesPersonnes.Add(test.Identifiant);
             ta.ListeDesPersonnes.Add(test1.Identifiant);
             VOCategorie cat = new VOCategorie();
             cat.Identifiant = Guid.NewGuid();
             cat.Nom = "informatique";
             cat.Description = "";
             Categories.Add(cat);
             ta.ListeDesCategories.Add(cat);
             ListeDesTaches.Add(ta);*/



        }
    }
}
