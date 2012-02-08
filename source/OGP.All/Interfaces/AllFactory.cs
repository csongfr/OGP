using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OGP.All
{
    /// <summary>
    /// Classe permettant d'utiliser l'All de gestion de tâche
    /// </summary>
    public class AllFactory
    {
        /// <summary>
        /// Méthode permettant de générer l'objet AllGestionTaches
        /// </summary>
        /// <returns></returns>
        public static IAllGestionTaches GetAllGestionTaches()
        {
            return new AllGestionTaches();
        }
    }
}
