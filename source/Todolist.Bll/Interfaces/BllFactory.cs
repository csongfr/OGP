using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Plugin.Todolist.Bll
{
    /// <summary>
    /// Classe BllFactory qui permet d'utiliser le Bll de gestion de fichiers
    /// </summary>
    public class BllFactory
    {
        /// <summary>
        /// Méthode qui instancie un objet BllFichierTache
        /// </summary>
        /// <returns>BllFichierTaches</returns>
        public static IBllFichierTaches GetBllGestionTaches()
        {
            return new BllFichierTaches();
        }
    }
}
