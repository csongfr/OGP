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
        /// <param name="cheminFichier">Chemin du fichier</param>
        /// <returns>VOToDoList</returns>
        public VOProjet CreerFichierTachesXml(string nomProjet, string cheminFichier)
        {
            VOProjet nouvelleTodolist = new VOProjet(nomProjet, cheminFichier);
            
            System.IO.FileStream fichier = System.IO.File.Create(cheminFichier);

            // Création d'un objet permettant la sérialisation d'un objet de type VOToDoList
            XmlSerializer serialiser = new XmlSerializer(typeof(VOProjet));

            // Sérialisation du fichier
            serialiser.Serialize(fichier, nouvelleTodolist);
            fichier.Close();
            return nouvelleTodolist;
        }

        /// <summary>
        /// Permet d'ouvrir et de sérialiser les modifications
        /// </summary>
        /// <param name="nomProjet">Nom du projet</param>
        /// <param name="ToDoList">Nom de ma ToDoList</param>
        /// <returns>VOToDoList</returns>
        public VOProjet ModifierFichierTachesXml(string cheminFichier, VOProjet ToDoList)
        {
            System.IO.FileStream fichier = System.IO.File.Open(cheminFichier, FileMode.Open);
            XmlSerializer serialiser = new XmlSerializer(typeof(VOProjet));
             serialiser.Serialize(fichier, ToDoList);
            fichier.Close();
            return ToDoList;
        }

        /// <summary>
        /// Désérialisation des fichiers Xml présents dans le répertoire
        /// </summary>
        /// <param name="listeFichiersExistants">Liste des chemins du répertoire</param>
        /// <returns>Liste des gestions de tâches désérialisées</returns>
        public List<VOProjet> DeserialisationFichiers(List<string> listeFichiersExistants)
        {
            // Une gestion de tâches
            VOProjet uneGestionTache;
            List<VOProjet> listeVOToDoListDeserialisees = new List<VOProjet>();

            // Création d'un objet permettant la sérialisation d'un objet de type VOToDoList
            XmlSerializer serialisateur = new XmlSerializer(typeof(VOProjet));

            // Pour chaque chemin
            foreach (string nomChemin in listeFichiersExistants)
            {
                // On désérialise le fichier se trouvant au chemin correspondant...
                FileStream file = new FileStream(nomChemin, FileMode.Open);
                uneGestionTache = (VOProjet)serialisateur.Deserialize(file);
                // ... Puis on l'ajoute à la liste
                listeVOToDoListDeserialisees.Add(uneGestionTache);
            }

            return listeVOToDoListDeserialisees;
        }
        #endregion
    }
}
