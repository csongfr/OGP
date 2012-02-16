using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
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
                           if (this.projetOuvert == null)
                           {
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

        #region Méthodes privées

        private void OuvrirProjet()
        {
            var visualizerService = Resolve<IUIVisualizerService>();
           
            object popup;
            

            // Ouverture de la popup d'ouverture de projet
            var res = visualizerService.ShowDialog(typeof(PopupOuvrirTodolistView), new PopupOuvrirTodolistViewModel(), out popup);
            
            // Cast pour manipuler l'objet PopupOuvrirTodolistViewModel
            PopupOuvrirTodolistViewModel popupCast = (PopupOuvrirTodolistViewModel)popup;
            //res = popupCast.OuvertureActivee;

            // Gestion de l'exception dans le cas où le repertoire n'existe pas
            if (popupCast.ListeCouranteTodolist == null)
            {
                throw new PluginException("Pas de fichier");
            }

            //if (popupCast.OuvertureActivee == true)
            //if (popupCast.OuvertureActivee == true)
            if (res==true)
            {
                ProjetOuvert = popupCast.ProjetAOuvrir;
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
        /// Permet d'ouvrir une nouvelle gestion de projet
        /// </summary>
        private void CreerTodolist()
        {
            var visualizerService = Resolve<IUIVisualizerService>();
            object popupCreation;

            var ouverturePopupCreation = visualizerService.ShowDialog(typeof(NouvelleGestionTache), new NouvelleGestionTacheViewModel(), out popupCreation);

            if (ouverturePopupCreation == true)
            {
                var exception = WcfHelper.Execute<IServiceGestionTaches>(client =>
                {
                    ProjetOuvert = client.NouvelleToDoList(((NouvelleGestionTacheViewModel)popupCreation).NomDuProjet);
                });
                if (ProjetOuvert == null)
                {
                    throw new PluginException("Pas de fichier");
                }
            }
        }
        #endregion

        #region Constructeur

        /// <summary>
        /// Constructeur par défaut
        /// </summary>
        public MenuViewModel()
        {
        }

        #endregion
    }
}
