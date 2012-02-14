using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using Plugin.Todolist.Service;
using Plugin.Todolist.ValueObjects;
using Plugin.Todolist.View;
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
        /// Stocke le VOTodolist
        /// </summary>
        private VOProjet todolist;

        #endregion

        #region Constructeur

        /// <summary>
        /// Default constructor
        /// </summary>
        public TodolistViewModel()
        {
        }

        #endregion

        #region Propriétés de présentation

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

        #region commandes
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
                    enregistrerTaches = new RelayCommand(
                            delegate
                            {
                                // OuvertureActivee = true;
                            },
                            delegate
                            {
                                if (this.todolist == null)
                                {
                                    return false;
                                }
                                return true;
                            },
                            true);
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
        #endregion

        #region méthodes privées
        /// <summary>
        /// Permet d'ouvrir une nouvelle gestion de projet
        /// </summary>
        /// <param name="param">object</param> 
        private void CreerTodolist(object param)
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
                               });

                if (exception != null)
                {
                    // TODO : gérer l'exception.
                }
            }
        }

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
                    "Todolist",
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
        /// Ouvre la popup et charge le projet sélectionné
        /// </summary>
        /// <param name="param">object</param>
        private void OuvrirProjet(object param)
        {
            this.fenetreTousFichiers = new PopupOuvrirTodolistView();

            // cas où le répertoire n'existe pas
            if ((fenetreTousFichiers.Vm.ListeCouranteTodolist == null) || (fenetreTousFichiers.Vm.ListeCouranteTodolist.Count == 0))
            {
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
                    Todolist = fenetreTousFichiers.Vm.ProjetAOuvrir;
                }
            }
        }

        #endregion
    }
}
