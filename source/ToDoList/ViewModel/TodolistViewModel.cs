﻿using System.Windows;
using System.Windows.Input;
using Plugin.Todolist.View;
using Plugin.Todolist.Service;
using Plugin.Todolist.ValueObjects;
using Utils.Commands;
using Utils.ViewModel;
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
        /// Commande qui ouvre la popup
        /// </summary>
        private RelayCommand nouveauCommand;

        /// <summary>
        /// Commande pour enregistrer des tâches
        /// </summary>
        private RelayCommand enregistrerTaches;

        /// Commande qui ouvre la fenêtre de sélection de fichiers
        /// </summary>
        private RelayCommand commandeOuvrirFichier;

        /// <summary>
        /// Permet de communiquer avec la view
        /// </summary>
        private NouvelleGestionTache fenetre;

        /// <summary>
        /// Communication avec la vue de la fenêtre ouvrant tous les fichiers
        /// </summary>
        private PopupOuvrirTodolistView fenetreTousFichiers;

        /// <summary>
        /// Permet de stocker le nom du projet
        /// </summary>
        private string nomProjet;

        /// <summary>
        /// Stocke le VOTodolist
        /// </summary>
        private VOProjet todolist;

        #endregion

        #region Propriétés de présentation

        /// <summary>
        /// Cinch : INPC helper.
        /// </summary>
        private static System.ComponentModel.PropertyChangedEventArgs nomProjetChangeArgs = Utils.Observable.ObservableHelper.CreateArgs<TodolistViewModel>(x => x.NomProjet);

        /// <summary>
        /// Gets ou Sets pour afficher le nom du projet dans la todolist.
        /// </summary>
        public string NomProjet
        {
            get
            {
                return this.nomProjet;
            }
            set
            {
                if (this.nomProjet == value)
                {
                    return;
                }

                this.nomProjet = value;

                NotifyPropertyChanged(nomProjetChangeArgs);
            }
        }

        /// <summary>
        /// Cinch : INPC helper.
        /// </summary>
        private static System.ComponentModel.PropertyChangedEventArgs todolistChangeArgs = Utils.Observable.ObservableHelper.CreateArgs<TodolistViewModel>(x => x.Todolist);

        /// <summary>
        /// Gets ou Sets pour afficher le nom du projet dans la todolist.
        /// </summary>
        public VOProjet Todolist
        {
            get
            {
                return this.todolist;
            }
            set
            {
                if (this.todolist == value)
                {
                    return;
                }

                this.todolist = value;

                NotifyPropertyChanged(todolistChangeArgs);
            }
        }

        #endregion

        #region Méthodes privées

        /// <summary>
        /// Fonction qui enregistre les modifications sur le projet
        /// </summary>
        /// <param name="param">object</param>
        public void EnregistrerModif(object param)
        {
            string enregistrer = MessageBox.Show("Voulez Vous enregistrer les modifications", "Enregistrer", MessageBoxButton.YesNo).ToString();
            if (enregistrer == "Yes")
            {
                var erreur = WcfHelper.Execute<IServiceGestionTaches>(
                    "Plugin.ToDoList",
                    client =>
                    {
                        string messageErreurEnregistrer = string.Empty;

                        Todolist = client.EnregistrerToDoList(Todolist, out messageErreurEnregistrer);
                    });
                if (erreur != null)
                {
                    // TODO : gérer l'exception.
                }
            }
        }

        /// <summary>
        /// Permet d'ouvrir une nouvelle gestion de projet
        /// </summary>
        /// <param name="param">object</param>
        public void CreerTodolist(object param)
        {
            fenetre = new NouvelleGestionTache();
            fenetre.ShowDialog();

            if (fenetre.Vm.NomDuProjet != null && fenetre.Vm.Actif == true)
            {
                var exception = WcfHelper.Execute<IServiceGestionTaches>(
                               "Todolist",
                               client =>
                               {
                                   string messageErreur = string.Empty;

                                   Todolist = client.NouvelleToDoList(fenetre.Vm.NomDuProjet, out messageErreur);

                                   if (!string.IsNullOrEmpty(messageErreur))
                                   {
                                       MessageBox.Show(messageErreur);
                                   }
                                   else
                                   {
                                       NomProjet = Todolist.NomDuProjet;
                                   }
                               });

                if (exception != null)
                {
                    // TODO : gérer l'exception.
                }
            }
        }

        /// <summary>
        /// Commande qui ouvre la popup
        /// </summary>
        public ICommand CreerCommand
        {
            get
            {
                if (nouveauCommand == null)
                {
                    nouveauCommand = new RelayCommand(CreerTodolist);
                }
                return nouveauCommand;
            }
        }

        /// <summary>

        /// appelle la fonction qui enregistre les modifications sur le projet
        /// </summary>
        public ICommand EnregistrerTaches
        {
            get
            {
                if (enregistrerTaches == null)
                {
                    enregistrerTaches = new RelayCommand(EnregistrerModif);
                }
                return enregistrerTaches;

            }
        }

        /// Commande qui ouvre la popup
        /// </summary>
        public ICommand CommandeOuvrirFichier
        {
            get
            {
                if (commandeOuvrirFichier == null)
                {
                    commandeOuvrirFichier = new RelayCommand(OuvrirProjet);
                }
                return commandeOuvrirFichier;
            }
        }

        /// <summary>
        /// Ouvre la popup et charge le projet sélectionné
        /// </summary>
        /// <param name="param">object</param>
        private void OuvrirProjet(object param)
        {
            this.fenetreTousFichiers = new PopupOuvrirTodolistView();
            this.fenetreTousFichiers.ShowDialog();

            if (fenetreTousFichiers.Vm.OuvertureActivee == true)
            {
                Todolist = fenetreTousFichiers.Vm.ProjetSelectionne;

            }
        }

        #endregion

        #region Constructeur

        /// <summary>
        /// Default constructor
        /// </summary>
        public TodolistViewModel()
        {
        }

        #endregion
    }
}
