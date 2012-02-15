using System;
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
    public class VOTache
    {
        #region Membres publics

        /// <summary>
        /// Gets et sets de la liste de sous-tâches
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute("SousTaches", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public List<int> SousTaches { get; set; }

        /// <summary>
        /// Gets et sets du titre
        /// </summary>
        [System.Xml.Serialization.XmlAttributeAttribute]
        public string Titre { get; set; }

        /// <summary>
        /// Gets et sets de l'identifiant
        /// </summary>
        [System.Xml.Serialization.XmlAttributeAttribute]
        public int Identifiant { get; set; }

        /// <summary>
        /// Gets et sets des catégories
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute("CategorieDeLaTache", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public List<VOCategorie> ListeDesCategories { get; set; }

        /// <summary>
        /// Gets et sets de la priorité
        /// </summary>
        [System.Xml.Serialization.XmlAttributeAttribute]
        public EnumPriorite PrioriteDeLaTache { get; set; }

        /// <summary>
        /// Gets et sets du risque
        /// </summary>
        [System.Xml.Serialization.XmlAttributeAttribute]
        public string Risque { get; set; }

        /// <summary>
        /// Gets et sets du % effectué
        /// </summary>
        [System.Xml.Serialization.XmlAttributeAttribute]
        public int PourcentageEffectue { get; set; }

        /// <summary>
        /// Gets et sets du calcul du % effectué
        /// </summary>
        [System.Xml.Serialization.XmlAttributeAttribute]
        public float CalculDuPourcentageEffectue { get; set; }

        /// <summary>
        /// Gets et sets du temps estimé à la tâche
        /// </summary>
        [System.Xml.Serialization.XmlAttributeAttribute]
        public long Estimation { get; set; }

        /// <summary>
        /// Gets et sets du temps passé sur la tâche
        /// </summary>
        [System.Xml.Serialization.XmlAttributeAttribute]
        public long TpsDepense { get; set; }

        /// <summary>
        /// Gets et sets de la date limite de la tâche
        /// </summary>
        [System.Xml.Serialization.XmlAttributeAttribute]
        public DateTime DateLimite { get; set; }

        /// <summary>
        /// Gets et sets de la date du début
        /// </summary>
        [System.Xml.Serialization.XmlAttributeAttribute]
        public DateTime DateDeDebut { get; set; }

        /// <summary>
        /// Gets et sets de la date de création
        /// </summary>
        [System.Xml.Serialization.XmlAttributeAttribute]
        public DateTime DateDeCreation { get; set; }

        /// <summary>
        /// Gets et sets de la date de dernière modification
        /// </summary>
        [System.Xml.Serialization.XmlAttributeAttribute]
        public DateTime DateDeDernièreModif { get; set; }

        /// <summary>
        /// Gets et sets du temps réel
        /// </summary>
        [System.Xml.Serialization.XmlAttributeAttribute]
        public long TempsReel { get; set; }

        /// <summary>
        /// Gets et sets de la date à laquelle la date a été finie
        /// </summary>
        [System.Xml.Serialization.XmlAttributeAttribute]
        public DateTime DateFin { get; set; }

        /// <summary>
        /// Gets et sets des Personnes
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute("Personnes", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string ListePersonnesXml
        {
            get;
            // {
            // string strPersonnes = string.Empty;

            // foreach (VOPersonne personne in ListePersonnes)
            // {
            //    if (!string.IsNullOrEmpty(strPersonnes))
            //    {
            //        strPersonnes += ", ";
            //    }
            //    strPersonnes += personne.ToString();
            // }

            // return strPersonnes;
            // }
            set;
            // {
            // string[] tab=value.Split(new string[]{", "}, StringSplitOptions.RemoveEmptyEntries);

            // foreach (string p in tab)
            // {
            //    bool trouver = false;

            //    foreach (VOPersonne personne in ListePersonnes)
            //    {
            //        if (personne.AjouterPersonne==true)
            //        {
            //            trouver = true;
            //        }
            //    }
            //    if (!trouver)
            //    {
            //        ListePersonnes.Add(new VOPersonne(p));
            //    }
            // }
            // }
        }

        /// <summary>
        /// Constructeur par défaut
        /// </summary>
        public VOTache()
        {
           // ListePersonnes = new List<VOPersonne>();
        }
        /*private string listeCat;

        public string ListCat
        {
            get
            {
                int nb = ListeDesCategories.Count;
                int nb1 = 0;
                foreach (VOCategorie categories in ListeDesCategories)
                {
                    nb1++;
                    if (nb1 < nb)
                    {

                        listeCat = categories.Nom + ", " + listeCat;
                    }
                    else
                    {
                        listeCat = listeCat + categories.Nom;
                    }

                }
                return listeCat;
            }
        }*/
        #endregion
    }
}
