using System.Collections.ObjectModel;
using Plugin.Todolist.Service;
using Plugin.Todolist.ValueObjects;
using Plugin.Todolist.View;
using Utils.Wcf;
using Cinch;
using Todolist.ViewModel;
using System.Windows;

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
        /// Permet de communiquer avec la view
        /// </summary>
        private NouvelleGestionTache fenetre;

        /// <summary>
        /// Communication avec la vue de la fenêtre ouvrant tous les fichiers
        /// </summary>
        private PopupOuvrirTodolistView fenetreTousFichiers;

        /// <summary>
        /// Stocke le projet
        /// </summary>
        private VOProjet projet;

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

        #region Propriétés de présentation /// <summary>
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
        private static System.ComponentModel.PropertyChangedEventArgs todolistChangeArgs = Utils.Observable.ObservableHelper.CreateArgs<TodolistViewModel>(x => x.Projet);

        /// <summary>
        /// Gets ou Sets pour afficher le nom du projet dans la todolist.
        /// </summary>
        public VOProjet Projet
        {
            get
            {
                return this.projet;
            }
            set
            {
                if (this.projet == value)
                {
                    return;
                }

                this.projet = value;

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
                            return this.projet == null;
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
            fenetre = new NouvelleGestionTache();
            fenetre.ShowDialog();

            if (fenetre.Vm.NomDuProjet != null && fenetre.Vm.Actif == true)
            {
                var exception = WcfHelper.Execute<IServiceGestionTaches>(client =>
                               {
                                   Projet = client.NouvelleToDoList(fenetre.Vm.NomDuProjet);
                               });

                if (exception != null)
                {
                    throw new PluginException("Erreur, il existe déjà un projet de ce nom.");
                }
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
            var res = visualizerService.ShowDialog(typeof(PopupOuvrirTodolistView), new PopupOuvrirTodolistViewModel(), out popup);

            if (res == true)
            {
                // TODO : A compléter une fois la gestionde la popup OK
            }

            this.fenetreTousFichiers = new PopupOuvrirTodolistView();

            // cas où le répertoire n'existe pas
            if ((fenetreTousFichiers.Vm.ListeCouranteTodolist == null) || (fenetreTousFichiers.Vm.ListeCouranteTodolist.Count == 0))
            {
                Projet = fenetreTousFichiers.Vm.ProjetAOuvrir;
                ListePersonnesProjet = new ObservableCollection<VOPersonne>(projet.Personnes);

                MessageBox.Show("Le répertoire ne contient pas de fichier");
            }
            else
            {
                string resultat = string.Empty;

                // Cas où il n'y a qu'un seul projet
                if (fenetreTousFichiers.Vm.ListeCouranteTodolist.Count == 1)
                {
                    // Est ce que l'utilisateur veut l'ouvrir?
                    resultat = MessageBox.Show("Un seul fichier présent dans le répertoire, il s'agit du projet " + fenetreTousFichiers.Vm.ProjetAOuvrir.NomDuProjet + "\nSouhaitez vous l'ouvrir?", "Un seul fichier", MessageBoxButton.YesNo).ToString();
                }
                else
                {
                    // Si il y a plusieurs fichiers dans la fenêtre
                    this.fenetreTousFichiers.ShowDialog();
                }

                if (resultat == "Yes" || fenetreTousFichiers.Vm.OuvertureActivee == true)
                {
                    Projet = fenetreTousFichiers.Vm.ProjetAOuvrir;
                }
            }
        }

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
