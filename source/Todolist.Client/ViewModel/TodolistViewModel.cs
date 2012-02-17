using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using Cinch;
using Plugin.Todolist.Service;
using Plugin.Todolist.ValueObjects;
using Plugin.Todolist.View;
using Todolist.ViewModel;
using Utils.Observable;
using Utils.Wcf; 

namespace Plugin.Todolist
{
    /// <summary>
    /// Mon ToDoList
    /// </summary>
    public class TodolistViewModel : ViewModelBase
    {
        #region Membres privés

        /// <summary>
        /// permet de gérer le ViewModel du menu
        /// </summary>
        private MenuViewModel menuViewModel;

        /// <summary>
        /// Permet de gérer le ViewModel des tâches
        /// </summary>
        private TacheViewModel tacheVM;

        /// <summary>
        /// Liste des tâches
        /// </summary>
        private ObservableList<TacheViewModel> listeTachesViewModel;

        #endregion

        #region Propriétés de présentation

        /// <summary>
        /// Cinch : INPC helper.
        /// </summary>
        private static System.ComponentModel.PropertyChangedEventArgs menuViewModelChangeArgs = Utils.Observable.ObservableHelper.CreateArgs<TodolistViewModel>(x => x.Menu);

        /// <summary>
        /// Gets et Sets du MenuViewModel
        /// </summary>
        public MenuViewModel Menu
        {
            get
            {
                return this.menuViewModel;
            }
            set
            {
                if (this.menuViewModel == value)
                {
                    return;
                }
                this.menuViewModel = value;
                NotifyPropertyChanged(menuViewModelChangeArgs);
            }
        }

        /// <summary>
        /// Cinch : INPC helper.
        /// </summary>
        private static System.ComponentModel.PropertyChangedEventArgs tacheVMChangeArgs = Utils.Observable.ObservableHelper.CreateArgs<TodolistViewModel>(x => x.TacheVM);

        /// <summary>
        /// Gets et Sets de la tache ViewModel
        /// </summary>
        public TacheViewModel TacheVM
        {
            get
            {
                return this.tacheVM;
            }
            set
            {
                if (this.tacheVM == value)
                {
                    return;
                }
                
                this.tacheVM = value;
                NotifyPropertyChanged(tacheVMChangeArgs);
            }
        }

        /// <summary>
        /// Cinch : INPC helper.
        /// </summary>
        private static System.ComponentModel.PropertyChangedEventArgs listeTachesVmChangeArgs = Utils.Observable.ObservableHelper.CreateArgs<TodolistViewModel>(x => x.ListeTachesViewModel);

        /// <summary>
        /// Gets ou Sets la liste des taches.
        /// </summary>
        public ObservableList<TacheViewModel> ListeTachesViewModel
        {
            get
            {
                return this.listeTachesViewModel;
            }
            set
            {
                if (this.listeTachesViewModel == value)
                {
                    return;
                }

                this.listeTachesViewModel = value;                
                NotifyPropertyChanged(listeTachesVmChangeArgs);
            }
        }

        #endregion

        #region Methode privées

        /// <summary>
        /// Fonction qui permet d'ajouter une tâche
        /// </summary>
        /// <param name="taches">List VOTache</param>
        private void AfficherTacheOuverture(List<VOTache> taches)
        {
            ListeTachesViewModel = new ObservableList<TacheViewModel>();
            foreach (var ta in taches)
            {
                ListeTachesViewModel.Add(new TacheViewModel(ta));
            }
            ListeTachesViewModel.Add(new TacheViewModel(new VOTache()));
        }

        #endregion

        #region Constructeur

        /// <summary>
        /// Default constructor
        /// </summary>
        public TodolistViewModel()
        {
            this.Menu = new MenuViewModel();
            this.TacheVM = new TacheViewModel();
            ListeTachesViewModel = new ObservableList<TacheViewModel>();
            // ListeTachesViewModel.Add(new TacheViewModel(new VOTache()));

            this.Menu.ProjetOuvertChanged += AfficherTacheOuverture;
        }

        #endregion
    }
}
