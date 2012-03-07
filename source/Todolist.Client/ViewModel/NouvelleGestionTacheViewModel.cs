using Cinch;

namespace Todolist.ViewModel
{
    /// <summary>
    /// Classe qui gère la popup
    /// </summary>
    public class NouvelleGestionTacheViewModel : ViewModelBase
    {
        #region Membres privés

        /// <summary>
        /// Stocke le nom du projet est vide
        /// </summary>
        private string nomDuProjet;

        /// <summary>
        /// Commande pour enregistrer le nom du projet et du fichier
        /// </summary>
        private SimpleCommand enregistrer;

        #endregion

        #region Constructeur

        /// <summary>
        /// Constructeur
        /// </summary>
        public NouvelleGestionTacheViewModel()
        {
        }

        #endregion

        #region propriétés de présentation

        /// <summary>
        /// Cinch : INPC Helper
        /// </summary>
        private static System.ComponentModel.PropertyChangedEventArgs nomDuProjetChangeArgs = Utils.Mvvm.ObservableHelper.CreateArgs<NouvelleGestionTacheViewModel>(x => x.NomDuProjet);

        /// <summary>
        /// Gets ou Sets du plugin actif
        /// </summary>
        public string NomDuProjet
        {
            get
            {
                return nomDuProjet;
            }
            set
            {
                if (nomDuProjet == value)
                {
                    return;
                }
                this.nomDuProjet = value;

                NotifyPropertyChanged(nomDuProjetChangeArgs);
            }
        }

        #endregion

        #region Commandes

        /// <summary>
        /// Permet d'enregistrer le nom du projet et du fichier
        /// </summary>
        public SimpleCommand Enregistrer
        {
            get
            {
                if (enregistrer == null)
                {
                    enregistrer = new SimpleCommand
                     {
                         ExecuteDelegate = delegate
                          {
                              this.RaiseCloseRequest(true);
                          },
                         CanExecuteDelegate = delegate
                          {
                              return !string.IsNullOrEmpty(NomDuProjet);
                          }
                     };
                }
                return enregistrer;
            }
        }

        #endregion
    }
}
