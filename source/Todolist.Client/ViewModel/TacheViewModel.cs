using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cinch;
using Plugin.Todolist.ValueObjects;

namespace Todolist.ViewModel
{
    public class TacheViewModel : ViewModelBase
    {
        private VOTache uneTache;

        /// <summary>
        /// Cinch : INPC helper.
        /// </summary>
        private static System.ComponentModel.PropertyChangedEventArgs uneTacheChangeArgs = Utils.Observable.ObservableHelper.CreateArgs<TacheViewModel>(x => x.UneTache);

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

        #region Constructeur

        /// <summary>
        /// Constructeur par défaut
        /// </summary>
        public TacheViewModel()
        {
            this.UneTache=new VOTache();
        }

        #endregion
    }
}
