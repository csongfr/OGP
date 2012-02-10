using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using OGP.ClientWpf.Extensions.View;
using OGP.ClientWpf.View;
using Utils.ViewModel;
using Utils.Commands;

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

        /// <summary>
        /// Commande qui ouvre un gestionnaire de fichier
        /// </summary>
        private RelayCommand ouvrirFichier;

        #endregion

        #region Commandes

        /// <summary>
        /// Ouvre un fichier
        /// </summary>
        #region OuvrirFichier

        public ICommand OuvrirFichier
        {
            get
            {
                if (ouvrirFichier == null)
                {
                    ouvrirFichier = new RelayCommand(Ouvrir);
                }
                return ouvrirFichier;
            }
        }

        #endregion

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

        #region Méthodes privées

        /// <summary>
        /// Ouvrir la popup
        /// </summary>
        /// <param name="param">object</param>
        private void Ouvrir(object param)
        {
            new NouvelleGestionTache().ShowDialog();
        }

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
