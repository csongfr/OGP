﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Plugin.Todolist.ValueObjects
{
    /// <summary>
    /// Classe correspondant à une tâche
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
    [System.SerializableAttribute]
    [System.Diagnostics.DebuggerStepThroughAttribute]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public class VOPersonne
    {
        /// <summary>
        /// Bool pour savoir si la personne doit être ajoutée au projet
        /// </summary>
        private bool affecte;

        /// <summary>
        /// Gets et sets du nom
        /// </summary>
        [System.Xml.Serialization.XmlAttributeAttribute]
        public string Nom { get; set; }

        /// <summary>
        /// Surcharge de la méthode
        /// </summary>
        /// <returns>Nom de la personne</returns>
        public override string ToString()
        {
            return Nom;
        }

        /// <summary>
        /// Gets et sets pour savoir sile nom de la personne est coché
        /// </summary>
        [XmlIgnore]
        public bool Affecte 
        {
            get
            {
                return affecte;
            }
            set
            {
                affecte = value;
            }
        }

        /// <summary>
        /// Constructeur par défaut
        /// </summary>
        public VOPersonne()
        {
        }

        /// <summary>
        /// Constructeur à partir d'un nom de personne 
        /// </summary>
        /// <param name="nom">Nom de la personne</param>
        public VOPersonne(string nom)
        {
            Nom = nom;
        }
    }
}
