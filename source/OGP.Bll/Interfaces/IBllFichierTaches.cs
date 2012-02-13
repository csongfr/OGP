using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OGP.ValueObjects;

namespace OGP.Bll
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
        /// <returns>VOToDoList</returns>
        VOTodolist CreerFichierTachesXml(string nomProjet);

        /// <summary>
        /// Désérialisation des fichiers Xml présents dans le répertoire
        /// </summary>
        /// <param name="listeFichiersExistants">liste des chemins</param>
        /// <returns>Liste des gestions de tâches désérialisées</returns>
        List<VOTodolist> DeserialisationFichiers(List<string> listeFichiersExistants);
    }
}
