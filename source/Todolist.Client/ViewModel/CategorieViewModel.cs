using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cinch;

namespace Todolist.ViewModel
{
    public class CategorieViewModel : ViewModelBase
    {
        #region Membres privés

        private string nom;
        private bool check;
        private bool checkOuverture =false;

        #endregion

        #region Propriétés de présentation

        /// <summary>
        /// Cinch : INPC helper.
        /// </summary>
        private static System.ComponentModel.PropertyChangedEventArgs checkOuvertureChangeArgs = Utils.Observable.ObservableHelper.CreateArgs<CategorieViewModel>(x => x.CheckOuverture);

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
        private static System.ComponentModel.PropertyChangedEventArgs nomChangeArgs = Utils.Observable.ObservableHelper.CreateArgs<CategorieViewModel>(x => x.Nom);

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
        private static System.ComponentModel.PropertyChangedEventArgs checkChangeArgs = Utils.Observable.ObservableHelper.CreateArgs<CategorieViewModel>(x => x.Check);

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

        public event Action<CategorieViewModel> CheckBoxCocheChanged;

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

        public CategorieViewModel()
        {
        }

        #endregion

    }
}
