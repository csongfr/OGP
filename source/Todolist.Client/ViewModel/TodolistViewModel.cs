﻿using System.Collections.ObjectModel;
using System.Windows;
using Cinch;
using Plugin.Todolist.Service;
using Plugin.Todolist.ValueObjects;
using Plugin.Todolist.View;
using Todolist.ViewModel;
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

        #endregion

        #region commandes
        
        #endregion

        #region méthodes privées
        #endregion

        #region Constructeur

        /// <summary>
        /// Default constructor
        /// </summary>
        public TodolistViewModel()
        {
            // FIXME : temporaire voir comment on peut initialiser plus proprement
            // ListePersonnesProjet = new ObservableCollection<VOPersonne>();
            // ListePersonnesProjet.Add(new VOPersonne("NLA"));
            this.Menu = new MenuViewModel();
        }

        #endregion
    }
}
