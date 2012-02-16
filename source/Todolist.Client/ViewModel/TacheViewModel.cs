using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cinch;
using Plugin.Todolist.ValueObjects;

namespace Todolist.ViewModel
{
    /// <summary>
    /// Classe pour gérer une tâche
    /// </summary>
    public class TacheViewModel : ViewModelBase
    {
        #region Membres privés

        /// <summary>
        /// Stocke uen tâche
        /// </summary>
        private VOTache uneTache;

        #endregion

        #region Propriétés de présentation

        /// <summary>
        /// Cinch : INPC helper.
        /// </summary>
        private static System.ComponentModel.PropertyChangedEventArgs uneTacheChangeArgs = Utils.Observable.ObservableHelper.CreateArgs<TacheViewModel>(x => x.UneTache);

        /// <summary>
        /// Gets et Sets dune tâche
        /// </summary>
        public VOTache UneTache
        {
            get
            {
                return this.uneTache;
            }
            set
            {
                if (this.uneTache == value)
                {
                    return;
                }

                this.uneTache = value;

                NotifyPropertyChanged(uneTacheChangeArgs);
            }
        }

        #endregion

        #region Constructeur

        /// <summary>
        /// Constructeur par défaut
        /// </summary>
        public TacheViewModel()
        {
            this.UneTache = new VOTache();
        }

        #endregion
    }
}
