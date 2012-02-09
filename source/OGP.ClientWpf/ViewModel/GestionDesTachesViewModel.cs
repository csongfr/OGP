using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using OGP.ClientWpf.Comands;

namespace OGP.ClientWpf.ViewModel
{
    /// <summary>
    /// onglet qui gère les tâches
    /// </summary>
    public class GestionDesTachesViewModel : ViewModelBase
    {
        #region Membres privés
        /// <summary>
        /// Ajoute une tâche
        /// </summary>
        private RelayCommand ajouterTache;

        #endregion

        #region Commandes

        #region Ajouter tache

        /// <summary>
        /// commande qui ajoute une nouvelle tâche
        /// </summary>
        public ICommand AjouterTache
        {
            get
            {
                if (ajouterTache == null)
                {
                   // ajouterTache = new RelayCommand();
                }
                return ajouterTache;
            }
        }
        #endregion

        #endregion

        #region Constructeur
        /// <summary>
        /// Default constructor
        /// </summary>
        /// 
        public GestionDesTachesViewModel()
        {
        }

        #endregion
    }
}
