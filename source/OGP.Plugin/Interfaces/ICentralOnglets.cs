using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AvalonDock;

namespace OGP.Plugin.Interfaces
{


    /// <summary>
    /// Interface des onglets
    /// </summary>
    public interface ICentralOnglets
    {
        /// <summary>
        /// Méthode qui permet d'ajouter un plugin
        /// </summary>
        /// <param name="Doc">Le document content à ajouter</param>
        void AjoutOnglet(DocumentContent Doc);
    }
}
