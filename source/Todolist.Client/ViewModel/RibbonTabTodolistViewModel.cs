using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using AvalonDock;
using Cinch;
using OGP.Plugin.Interfaces;
using Plugin.Todolist;
using Plugin.Todolist.ValueObjects;
using Plugin.Todolist.View;
using Todolist.Client.Ressources;
using Todolist.Exception;
using Todolist.ViewModel;
using Utils.Wcf;
using Todolist.Commun;

namespace Todolist.Client.ViewModel
{
    class RibbonTabTodolistViewModel : ViewModelBase
    {

        /// <summary>
        /// Stocke le projet
        /// </summary>
        private VOProjet projetOuvert;

        /// <summary>
        /// Cinch : INPC helper.
        /// </summary>
        private static System.ComponentModel.PropertyChangedEventArgs projetOuvertChangeArgs = Utils.Mvvm.ObservableHelper.CreateArgs<MenuViewModel>(x => x.ProjetOuvert);

        /// <summary>
        /// Gets ou Sets pour afficher le nom du projet dans la todolist.
        /// </summary>
        public VOProjet ProjetOuvert
        {
            get
            {
                return this.projetOuvert;
            }
            set
            {
                if (this.projetOuvert == value)
                {
                    return;
                }

                this.projetOuvert = value;

                NotifyPropertyChanged(projetOuvertChangeArgs);
            }
        }

        /// <summary>
        /// Commande qui ouvre la popup
        /// </summary>
        private SimpleCommand nouveauCommand;

        /// <summary>
        /// Commande qui ouvre la popup
        /// </summary>
        public SimpleCommand CreerCommand
        {
            get
            {
                if (nouveauCommand == null)
                {
                    nouveauCommand = new SimpleCommand
                    {
                        ExecuteDelegate = delegate
                        {
                            CreerTodolist();
                        }
                    };
                }
                return nouveauCommand;
            }
        }

        private void CreerTodolist()
        {
            var methodeOuvrirNouvelOnglet = ServiceProvider.Resolve<ICentralOnglets>();

            var visualizerService = Resolve<IUIVisualizerService>();
            object popupCreation;

            var ouverturePopupCreation = visualizerService.ShowDialog(typeof(NouvelleGestionTache), new NouvelleGestionTacheViewModel(), out popupCreation);

            if (ouverturePopupCreation == true)
            {
                var todoListViewModel = new TodolistViewModel();
                Plugin.Todolist.Todolist Todol = new Plugin.Todolist.Todolist { DataContext = todoListViewModel };

                Todol.Title = ((NouvelleGestionTacheViewModel)popupCreation).NomDuProjet;
                methodeOuvrirNouvelOnglet.AjoutOnglet(Todol);

                var exception = WcfHelper.Execute<IServiceGestionTaches>(client =>
                {
                    ProjetOuvert = client.NouvelleToDoList(Todol.Title);
                    ProjetOuvert.ListeDesTaches = new ObservableCollection<VOTache>();
                    ProjetOuvert.Categories = new ObservableCollection<VOCategorie>();
                    todoListViewModel.AfficherTacheOuverture(ProjetOuvert.ListeDesTaches);
                });
                if (ProjetOuvert == null)
                {
                    throw new TodolistPluginException("Pas de fichier");
                }
            }
        }

        #region Constructeur

        public RibbonTabTodolistViewModel()
        {
        }

        #endregion
    }
}
