using System.Collections.ObjectModel;
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
        /// Commande qui ouvre la popup
        /// </summary>
        private SimpleCommand nouveauCommand;

        /// <summary>
        /// Commande pour enregistrer des tâches
        /// </summary>
        private SimpleCommand enregistrerTaches;

        /// <summary>
        /// Commande qui ouvre la fenêtre de sélection de fichiers
        /// </summary>
        private SimpleCommand commandeOuvrirFichier;

        /// <summary>
        /// Stocke le projet
        /// </summary>
        private VOProjet projet;

        /// <summary>
        /// Permet de selectionner une tâche pour la modifier
        /// </summary>
        private VOTache tacheSelectionnee;

        /// <summary>
        /// permet de gérer le ViewModel du menu
        /// </summary>
        private MenuViewModel menuViewModel;

        /// <summary>
        /// Permet de gérer le ViewModel des tâches
        /// </summary>
        private TacheViewModel tacheVM;

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

        #endregion

        #region commandes

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

        /// <summary>
        /// appelle la fonction qui enregistre les modifications sur le projet
        /// </summary>
        public SimpleCommand EnregistrerTaches
        {
            get
            {
                if (enregistrerTaches == null)
                {
                    enregistrerTaches = new SimpleCommand
                    {
                        ExecuteDelegate = delegate
                        {
                            EnregistrerModif();
                        },
                        CanExecuteDelegate = delegate
                        {
                            if (Projet == null)
                            {
                                // return this.projet == null;
                                return false;
                            }
                            return true;
                        }
                    };
                }
                return enregistrerTaches;
            }
        }

        /// <summary>
        /// Commande qui ouvre la popup
        /// </summary>
        public SimpleCommand CommandeOuvrirFichier
        {
            get
            {
                if (commandeOuvrirFichier == null)
                {
                    commandeOuvrirFichier = new SimpleCommand
                    {
                        ExecuteDelegate = delegate
                        {
                            OuvrirProjet();
                        }
                    };
                }
                return commandeOuvrirFichier;
            }
        }
        #endregion

        #region méthodes privées
        /// <summary>
        /// Permet d'ouvrir une nouvelle gestion de projet
        /// </summary>
        private void CreerTodolist()
        {
            var visualizerService = Resolve<IUIVisualizerService>();
            object popupCreation;

            visualizerService.ShowDialog(typeof(NouvelleGestionTache), new NouvelleGestionTacheViewModel(), out popupCreation);

            if (((NouvelleGestionTacheViewModel)popupCreation).Actif == true)
            {
                var exception = WcfHelper.Execute<IServiceGestionTaches>(client =>
                {
                    Projet = client.NouvelleToDoList(((NouvelleGestionTacheViewModel)popupCreation).NomDuProjet);
                });
            }
        }

        /// <summary>
        /// Fonction qui enregistre les modifications sur le projet
        /// </summary>
        private void EnregistrerModif()
        {
            string enregistrer = MessageBox.Show("Voulez-vous enregistrer les modifications", "Enregistrer", MessageBoxButton.YesNo).ToString();

            if (enregistrer == "Yes")
            {
                var erreur = WcfHelper.Execute<IServiceGestionTaches>(
                    client =>
                    {
                        string messageErreurEnregistrer = string.Empty;

                        Projet = client.EnregistrerToDoList(Projet);
                    });

                if (erreur != null)
                {
                    // TODO : gérer l'exception.
                }
            }
        }

        /// <summary>
        /// appelle la fonction qui enregistre les modifications sur le projet
        /// Ouvre la popup et charge le projet sélectionné
        /// </summary>
        private void OuvrirProjet()
        {
            var visualizerService = Resolve<IUIVisualizerService>();
            object popup;

            // Ouverture de la popup d'ouverture de projet
            visualizerService.ShowDialog(typeof(PopupOuvrirTodolistView), new PopupOuvrirTodolistViewModel(), out popup);

            // Cast pour manipuler l'objet PopupOuvrirTodolistViewModel
            PopupOuvrirTodolistViewModel popupCast = (PopupOuvrirTodolistViewModel)popup;

            // Gestion de l'exception dans le cas où le repertoire n'existe pas
            if (popupCast.ListeCouranteTodolist == null)
            {
                throw new PluginException("Pas de fichier");
            }

            if (popupCast.OuvertureActivee == true)
            {
                Projet = popupCast.ProjetAOuvrir;
            }
        }
        
        #endregion

        #region Constructeur

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
            this.TacheVM = new TacheViewModel();
        }

        #endregion
    }
}
