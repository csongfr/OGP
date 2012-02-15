using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using Plugin.Todolist.Service;
using Plugin.Todolist.ValueObjects;
using Plugin.Todolist.View;
using Todolist.ViewModel;
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

        /// <summary>
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

        /// <summary>
        /// Permet de selectionner une tâche pour la modifier
        /// </summary>
        private VOTache tacheSelectionnee;

        /// <summary>
        /// Stocke la liste des personnes affectées sur le projet.
        /// </summary>
        private ObservableCollection<VOPersonne> listePersonnesProjet;

        /// <summary>
        /// Stocke la personne active.
        /// </summary>
        private VOPersonne personneActive;

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

        /// <summary>
        /// Cinch : INPC helper.
        /// </summary>
        private static System.ComponentModel.PropertyChangedEventArgs tacheSelectionneeChangeArgs = Utils.Observable.ObservableHelper.CreateArgs<TodolistViewModel>(x => x.TacheSelectionnee);

        /// <summary>
        /// Gets et Sets de la tâche sélectionnée
        /// </summary>
        public VOTache TacheSelectionnee
        {
            get
            {
                return this.tacheSelectionnee;
            }
            set
            {
                if (this.tacheSelectionnee == value)
                {
                    return;
                }

                this.tacheSelectionnee = value;

                NotifyPropertyChanged(tacheSelectionneeChangeArgs);
            }
        }

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

        /// <summary>
        /// Cinch : INPC helper.
        /// </summary>
        private static System.ComponentModel.PropertyChangedEventArgs listePersonnesProjetChangeArgs = Utils.Observable.ObservableHelper.CreateArgs<TodolistViewModel>(x => x.ListePersonnesProjet);

        /// <summary>
        /// Gets ou Sets la liste des personnes affectées au projet.
        /// </summary>
        public ObservableCollection<VOPersonne> ListePersonnesProjet
        {
            get
            {
                return this.listePersonnesProjet;
            }
            set
            {
                if (this.listePersonnesProjet == value)
                {
                    return;
                }

                this.listePersonnesProjet = value;

                NotifyPropertyChanged(listePersonnesProjetChangeArgs);
            }
        }

        /// <summary>
        /// Cinch : INPC helper.
        /// </summary>
        private static System.ComponentModel.PropertyChangedEventArgs personneActiveChangeArgs = Utils.Observable.ObservableHelper.CreateArgs<TodolistViewModel>(x => x.PersonneActive);

        /// <summary>
        /// Gets ou Sets la personne active.
        /// </summary>
        public VOPersonne PersonneActive
        {
            get
            {
                return this.personneActive;
            }
            set
            {
                if (this.personneActive == value)
                {
                    return;
                }

                this.personneActive = value;
                
                // TODO : Méthode permettant de mettre à jour la liste coté TacheSelectionnée
                if (this.personneActive.AjouterPersonne)
                {
                    tacheSelectionnee.ListePersonnesXml = this.personneActive.ToString();
                }

                NotifyPropertyChanged(personneActiveChangeArgs);
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
                var erreur = WcfHelper.Execute<IServiceGestionTaches>(client =>
                    {
                        string messageErreurEnregistrer = string.Empty;

                        Todolist = client.EnregistrerToDoList(Todolist);
                    });
                if (erreur != null)
                {
                    throw new PluginException("Projet vide");
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
                var exception = WcfHelper.Execute<IServiceGestionTaches>(client =>
                               {
                                   Todolist = client.NouvelleToDoList(fenetre.Vm.NomDuProjet);
                                   NomProjet = Todolist.NomDuProjet;
                               });

                if (exception != null)
                {
                    throw new PluginException("Erreur, il existe déjà un projet de ce nom.");
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

        /// <summary>
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
                ListePersonnesProjet = new ObservableCollection<VOPersonne>(todolist.Personnes);
            }
        }

        #endregion

        #region Constructeur

        /// <summary>
        /// Default constructor
        /// </summary>
        public TodolistViewModel()
        {
            // FIXME : temporaire voir comment on peut initialiser plus proprement
            ListePersonnesProjet = new ObservableCollection<VOPersonne>();
            ListePersonnesProjet.Add(new VOPersonne("NLA"));
            this.Menu = new MenuViewModel();
        }

        #endregion
    }
}
