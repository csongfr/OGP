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
    public class VOCategorie
    {
        /// <summary>
        /// Gets et sets de la catégorie
        /// </summary>
        public string Nom { get; set; }

        public override string ToString()
        {
            return Nom;
        }
        /// <summary>
        /// Constructeur par défaut
        /// </summary>
        public VOCategorie()
        {
        }
    }
}
