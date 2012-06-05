using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OGP.ClientWpf.ViewModel;

namespace OGP.ClientWpf.Interfaces
{
    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public interface ICentralOnglets
    {
        /// <summary>
        /// Met au premier plan l'onglet fourni. Si celui-ci n'est pas dans la liste des onglets, il est ajouté.
        /// </summary>
        /// <param name="workspace">Onglet à afficher.</param>
         void NouvelOnglet(string Nom);
    }
}
