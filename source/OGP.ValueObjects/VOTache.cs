using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace OGP.ValueObjects
{    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]

    /// <summary>
    /// Classe correspondant à une tâche
    /// </summary>
   public class VOTache
   {
       #region Membres publics

       /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("SousTaches", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]

       /// <summary>
       /// Gets et sets de la liste de sous-tâches
       /// </summary>
        public List<int> SousTaches { get; set; }

       /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]

        /// <summary>
        /// Gets et sets du titre
        /// </summary>
        public string Titre { get; set; }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]

        /// <summary>
        /// Gets et sets de l'identifiant
        /// </summary>
        public int Identifiant { get; set; }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Personne", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]

        /// <summary>
        /// Gets et sets des Personnes
        /// </summary>
        public List<VOPersonne> ListeDesPersonnes { get; set; }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("CategorieDeLaTache", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]

        /// <summary>
        /// Gets et sets des catégories
        /// </summary>
        public List<VOCategorie> ListeDesCategories { get; set; }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]

        /// <summary>
        /// Gets et sets de la priorité
        /// </summary>
        public EnumPriorite PrioriteDeLaTache { get; set; }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]

        /// <summary>
        /// Gets et sets du risque
        /// </summary>
        public string Risque { get; set; }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]

        /// <summary>
        /// Gets et sets du % effectué
        /// </summary>
        public int PourcentageEffectue { get; set; }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]

        /// <summary>
        /// Gets et sets du calcul du % effectué
        /// </summary>
        public float CalculDuPourcentageEffectue { get; set; }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]

        /// <summary>
        /// Gets et sets du temps estimé à la tâche
        /// </summary>
        public TimeSpan Estimation { get; set; }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]

        /// <summary>
        /// Gets et sets du temps passé sur la tâche
        /// </summary>
        public TimeSpan TpsDepense { get; set; }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]

        /// <summary>
        /// Gets et sets de la date limite de la tâche
        /// </summary>
        public DateTime DateLimite { get; set; }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]

        /// <summary>
        /// Gets et sets de la date du début
        /// </summary>
        public DateTime DateDeDebut { get; set; }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]

        /// <summary>
        /// Gets et sets de la date de création
        /// </summary>
        public DateTime DateDeCreation { get; set; }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]

        /// <summary>
        /// Gets et sets de la date de dernière modification
        /// </summary>
        public DateTime DateDeDernièreModif { get; set; }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]

        /// <summary>
        /// Gets et sets du temps réel
        /// </summary>
        public TimeSpan TempsReel { get; set; }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]

        /// <summary>
        /// Gets et sets de la date à laquelle la date a été finie
        /// </summary>
        public DateTime DateFin { get; set; }

        #endregion
    }
}
