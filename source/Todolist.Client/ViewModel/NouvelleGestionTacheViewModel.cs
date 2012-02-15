using System.Windows.Input;
using Cinch;

namespace Plugin.Todolist.ViewModel
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
        /// Booléen permettant de savoir si le bouton "Enregistrer" est bien selectionné
        /// </summary>
        private bool actif;

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
            Actif = false;
        }

        #endregion

        #region propriétés de présentation
        /// <summary>
        /// Cinch : INPC helper.
        /// </summary>
        private static System.ComponentModel.PropertyChangedEventArgs sucessChangeArgs = Utils.Observable.ObservableHelper.CreateArgs<NouvelleGestionTacheViewModel>(x => x.Actif);

        /// <summary>
        /// Gets ou Sets pour savoir si le bouton "Enregistrer" est cliqué
        /// </summary>
        public bool Actif
        {
            get
            {
                return this.actif;
            }
            set
            {
                if (this.actif == value)
                {
                    return;
                }

                this.actif = value;

                NotifyPropertyChanged(sucessChangeArgs);
            }
        }

        /// <summary>
        /// Cinch : INPC Helper
        /// </summary>
        private static System.ComponentModel.PropertyChangedEventArgs nomDuProjetChangeArgs = Utils.Observable.ObservableHelper.CreateArgs<NouvelleGestionTacheViewModel>(x => x.NomDuProjet);

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
                              Actif = true;
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
