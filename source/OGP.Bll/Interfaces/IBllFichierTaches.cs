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
        /// <param name="nomFichier">Le nom du fichier</param>
        /// <param name="nomProjet">Le nom du projet</param>
        /// <returns>VOToDoList</returns>
        //VOToDoList CreerFichierTachesXml(string nomFichier, string nomProjet);

        VOToDoList CreerFichierTachesXml(string nomProjet);
    }
}
