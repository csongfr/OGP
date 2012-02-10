using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using OGP.ValueObjects;

namespace OGP.Bll
{
    /// <summary>
    /// Classe permettant de gérer la gestion de fichiers
    /// </summary>
    public class BllFichierTaches : IBllFichierTaches
    {
        /// <summary>
        /// Création d'une nouvelle gestion de projet
        /// </summary>
        /// <param name="nomProjet">Nom du projet</param>
        /// <returns>VOToDoList</returns>
        public VOToDoList CreerFichierTachesXml(string nomProjet)
        {
            VOToDoList nouvelleVOToDoList = new VOToDoList(nomProjet);
            System.IO.FileStream fichier = System.IO.File.Create(nomProjet);

            // Création d'un objet permettant la sérialisation d'un objet de type VOToDoList
            XmlSerializer serialiser = new XmlSerializer(typeof(VOToDoList));

            // Sérialisation du fichier
            serialiser.Serialize(fichier, nouvelleVOToDoList);
            fichier.Close();
            return nouvelleVOToDoList;
        }
    }
}
