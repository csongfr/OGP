using System;
using System.Collections.Generic;
using System.IO;
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
        #region méthodes publiques
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

        /// <summary>
        /// Désérialisation des fichiers Xml présents dans le répertoire
        /// </summary>
        /// <param name="listeFichiersExistants">Liste des chemins du répertoire</param>
        /// <returns>Liste des gestions de tâches désérialisées</returns>
        public List<VOToDoList> DeserialisationFichiers(List<string> listeFichiersExistants)
        {
            // Une gestion de tâches
            VOToDoList uneGestionTache;
            List<VOToDoList> listeVOToDoListDeserialisees = new List<VOToDoList>();

            // Création d'un objet permettant la sérialisation d'un objet de type VOToDoList
            XmlSerializer serialisateur = new XmlSerializer(typeof(VOToDoList));

            // Pour chaque chemin
            foreach (string nomChemin in listeFichiersExistants)
            {
                // On désérialise le fichier se trouvant au chemin correspondant...
                FileStream file = new FileStream(nomChemin, FileMode.Open);
                uneGestionTache = (VOToDoList)serialisateur.Deserialize(file);
                // ... Puis on l'ajoute à la liste
                listeVOToDoListDeserialisees.Add(uneGestionTache);
            }
            return listeVOToDoListDeserialisees;
        }
        #endregion
    }
}
