using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Plugin.Todolist.All
{
    /// <summary>
    /// Classe permettant d'utiliser l'All de gestion de tâche
    /// </summary>
    public class AllFactory
    {
        /// <summary>
        /// Méthode permettant de générer l'objet AllGestionTaches
        /// </summary>
        /// <returns>IAllGestionTaches : objet permettant la gestion des taches</returns>
        public static IAllGestionTaches GetAllGestionTaches()
        {
            return new AllGestionTaches();
        }
    }
}
