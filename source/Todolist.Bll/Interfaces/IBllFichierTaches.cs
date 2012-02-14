using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Plugin.Todolist.ValueObjects;

namespace Plugin.Todolist.Bll
{
    /// <summary>
    /// interface de la Bll de gestion de fichiers
    /// </summary>
    public interface IBllFichierTaches
    {
        /// <summary>
        /// Prototype de la méthode permettant de créer un nouveau fichier de tâches
        /// </summary>
        /// <param name="nomProjet">Le nom du projet</param>
        /// <param name="cheminFichier">Chemin du fichier</param>
        /// <returns>VOToDoList</returns>
        VOProjet CreerFichierTachesXml(string nomProjet, string cheminFichier);

        /// <summary>
        /// Prototype de la méthode permettant de modifier et enregistrer un fichier de tâches
        /// </summary>
        /// <param name="nomProjet">Le nom du projet</param>
        /// <param name="ToDoList">ma todolist</param>
        /// <returns>VOToDoList</returns>
        VOProjet ModifierFichierTachesXml(string cheminFichier, VOProjet ToDoList);

        /// <summary>
        /// Désérialisation des fichiers Xml présents dans le répertoire
        /// </summary>
        /// <param name="listeFichiersExistants">liste des chemins</param>
        /// <returns>Liste des gestions de tâches désérialisées</returns>
        List<VOProjet> DeserialisationFichiers(List<string> listeFichiersExistants);


    }
}
