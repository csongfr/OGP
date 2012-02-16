using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using Cinch;
using Plugin.Todolist;
using Plugin.Todolist.Service;
using Plugin.Todolist.ValueObjects;
using Plugin.Todolist.View;
using Utils.Wcf;

namespace Todolist.ViewModel
{
    /// <summary>
    /// Classe qui gère le menu à gauche du plugin ToDoListS
    /// </summary>
    public class MenuViewModel : ViewModelBase
    {
        #region Membre privés

        /// <summary>
        /// Stocke les personnes du projet
        /// </summary>
        private VOPersonne personne;

        /// <summary>
        /// Commande qui ouvre la popup
        /// </summary>
        private SimpleCommand nouveauCommand;

        /// <summary>
        /// Permet de communiquer avec la view
        /// </summary>
        private NouvelleGestionTache fenetre;

        /// <summary>
        /// Stocke le projet
        /// </summary>
        private VOProjet projetOuvert;

        /// <summary>
        /// Commande pour enregistrer des tâches
        /// </summary>
        private SimpleCommand enregistrerTaches;

        /// <summary>
        /// Communication avec la vue de la fenêtre ouvrant tous les fichiers
        /// </summary>
        private PopupOuvrirTodolistView fenetreTousFichiers;

        /// <summary>
        /// Commande qui ouvre la fenêtre de sélection de fichiers
        /// </summary>
        private SimpleCommand commandeOuvrirFichier;

        #endregion

        #region Propriétés de présentation

        /// <summary>
        /// Cinch : INPC helper.
        /// </summary>
        private static System.ComponentModel.PropertyChangedEventArgs projetOuvertChangeArgs = Utils.Observable.ObservableHelper.CreateArgs<MenuViewModel>(x => x.ProjetOuvert);

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
        /// Cinch : INPC helper.
        /// </summary>
        private static System.ComponentModel.PropertyChangedEventArgs personneChangeArgs = Utils.Observable.ObservableHelper.CreateArgs<MenuViewModel>(x => x.Personne);

        /// <summary>
        /// Gets et Sets pour ajouter personne
        /// </summary>
        public VOPersonne Personne
        {
            get
            {
                return this.personne;
            }
            set
            {
                if (this.personne == value)
                {
                    return;
                }

                this.personne = value;

                NotifyPropertyChanged(personneChangeArgs);
            }
        }

        #endregion

        #region Commandes

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
                            return this.projetOuvert == null;
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

        #region Méthodes privées

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
                    ProjetOuvert = client.NouvelleToDoList(fenetre.Vm.NomDuProjet);
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

                        ProjetOuvert = client.EnregistrerToDoList(ProjetOuvert);
                    });

                if (erreur != null)
                {
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
                ProjetOuvert = fenetreTousFichiers.Vm.ProjetAOuvrir;
                // ListePersonnesProjet = new ObservableCollection<VOPersonne>(projet.Personnes);
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
                    ProjetOuvert = fenetreTousFichiers.Vm.ProjetAOuvrir;
                }
            }
        }

        /// <summary>
        /// Fonction qui met à jour la liste VOPersonne
        /// </summary>
        private void PersonneVOPersonneAjoutee()
        {
        }

        #endregion

        #region Constructeur

        /// <summary>
        /// Constructeur par défaut
        /// </summary>
        public MenuViewModel()
        {
            Personne = new VOPersonne();
        }

        #endregion
    }
}
