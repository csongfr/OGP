using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cinch;
using Plugin.Todolist.ValueObjects;

namespace Todolist.ViewModel
{
    /// <summary>
    /// Classe CategorieViewModel pour le binding sur les combobox de la todolist
    /// </summary>
    public class CategorieViewModel : ViewModelBase
    {
        #region Membres privés

        /// <summary>
        /// Nom de la catégorieViewModel
        /// </summary>
        private string nom;

        /// <summary>
        /// Cochage de la checkBox de la catégorieViewModel
        /// </summary>
        private bool check;

        /// <summary>
        /// Booléen vérifiant si on ouvre un nouveau fichier ou pas
        /// </summary>
        private bool checkOuverture = false;

        #endregion

        #region Propriétés de présentation

        /// <summary>
        /// Cinch : INPC helper.
        /// </summary>
        private static System.ComponentModel.PropertyChangedEventArgs checkOuvertureChangeArgs = Utils.Mvvm.ObservableHelper.CreateArgs<CategorieViewModel>(x => x.CheckOuverture);

        /// <summary>
        /// Gets et sets de bool checkOuverture
        /// </summary>
        public bool CheckOuverture
        {
            get
            {
                return this.checkOuverture;
            }
            set
            {
                if (this.checkOuverture == value)
                {
                    return;
                }

                this.checkOuverture = value;
                
                NotifyPropertyChanged(checkOuvertureChangeArgs);
            }
        }

        /// <summary>
        /// Cinch : INPC helper.
        /// </summary>
        private static System.ComponentModel.PropertyChangedEventArgs nomChangeArgs = Utils.Mvvm.ObservableHelper.CreateArgs<CategorieViewModel>(x => x.Nom);

        /// <summary>
        /// Gets et sets du nom de la catégorie
        /// </summary>
        public string Nom
        {
            get
            {
                return this.nom;
            }
            set
            {
                if (this.nom == value)
                {
                    return;
                }

                this.nom = value;
                NotifyPropertyChanged(nomChangeArgs);
                OncheckBoxCocheChanged();
            }
        }

        /// <summary>
        /// Cinch : INPC helper.
        /// </summary>
        private static System.ComponentModel.PropertyChangedEventArgs checkChangeArgs = Utils.Mvvm.ObservableHelper.CreateArgs<CategorieViewModel>(x => x.Check);

        /// <summary>
        /// Gets et sets de bool check
        /// </summary>
        public bool Check
        {
            get
            {
                return this.check;
            }
            set
            {
                if (this.check == value)
                {
                    return;
                }

                this.check = value;
                NotifyPropertyChanged(checkChangeArgs);
                if (CheckOuverture == false)
                {
                    OncheckBoxCocheChanged();
                }
            }
        }

        #endregion

        #region Evènements

        /// <summary>
        /// Evénement sur le cochage des checbox des catégories
        /// </summary>
        public event Action<CategorieViewModel> CheckBoxCocheChanged;

        /// <summary>
        /// Déclenche l'évènement CheckBoxCocheChanged
        /// </summary>
        private void OncheckBoxCocheChanged()
        {
            var handler = CheckBoxCocheChanged;
            if (handler != null)
            {
                handler(this);
            }
        }

        #endregion

        #region Constructeur

        /// <summary>
        /// Constructeur par défaut
        /// </summary>
        public CategorieViewModel()
        {
        }

        public CategorieViewModel(VOCategorie categorie)
        {
            Nom = categorie.Nom;
        }
        #endregion
    }
}
