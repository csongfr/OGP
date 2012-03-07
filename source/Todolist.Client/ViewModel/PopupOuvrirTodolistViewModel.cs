using System.Collections.Generic;
using System.Linq;
using Cinch;
using Plugin.Todolist.Service;
using Plugin.Todolist.ValueObjects;
using Todolist.Exception;
using Utils.Wcf;

namespace Todolist.ViewModel
{
    /// <summary>
    /// Popup permettant de générer la liste des fichiers existants
    /// </summary>
    public class PopupOuvrirTodolistViewModel : ViewModelBase
    {
        #region Membres privés

        /// <summary>
        /// Pour gérer la liste de fichiers dans le répertoire courant
        /// </summary>
        private List<VOProjet> listeCouranteTodolist;

        /// <summary>
        /// Projet sélectionné par l'utilisateur
        /// </summary>
        private VOProjet projetAOuvrir;

        /// <summary>
        /// Commande pour ouvrir le fichier sélectionné
        /// </summary>
        private SimpleCommand ouvrirProjetSelectionneCommand;

        #endregion

        #region Constructeur

        /// <summary>
        /// Constructeur de la popup
        /// </summary>
        public PopupOuvrirTodolistViewModel()
        {
            // Appel au service pour charger les VOTodolist
            var exception = WcfHelper.Execute<IServiceGestionTaches>(client =>
                               {
                                   ListeCouranteTodolist = client.ChargementFichiers();
                                   if ((this.listeCouranteTodolist != null) && (this.listeCouranteTodolist.Count == 1))
                                   {
                                       ProjetAOuvrir = ListeCouranteTodolist.First();
                                   }
                               });

            if (ListeCouranteTodolist == null)
            {
                throw new TodolistPluginException("Pas de fichier");
            }
        }

        #endregion

        #region Propriétés de présentation
        /// <summary>
        /// Cinch : INPC helper.
        /// </summary>
        private static System.ComponentModel.PropertyChangedEventArgs listeCouranteChangeArgs = Utils.Mvvm.ObservableHelper.CreateArgs<PopupOuvrirTodolistViewModel>(x => x.ListeCouranteTodolist);

        /// <summary>
        /// Gets et sets de la liste des VO disponibles
        /// </summary>
        public List<VOProjet> ListeCouranteTodolist
        {
            get
            {
                return this.listeCouranteTodolist;
            }
            set
            {
                if (this.listeCouranteTodolist == value)
                {
                    return;
                }

                this.listeCouranteTodolist = value;

                NotifyPropertyChanged(listeCouranteChangeArgs);
            }
        }

        /// <summary>
        /// Cinch : INPC helper.
        /// </summary>
        private static System.ComponentModel.PropertyChangedEventArgs projetAOuvrirChangeArgs = Utils.Mvvm.ObservableHelper.CreateArgs<PopupOuvrirTodolistViewModel>(x => x.ProjetAOuvrir);

        /// <summary>
        /// Gets et sets du projet sélectionné
        /// </summary>
        public VOProjet ProjetAOuvrir
        {
            get
            {
                return this.projetAOuvrir;
            }
            set
            {
                if (this.projetAOuvrir == value)
                {
                    return;
                }

                this.projetAOuvrir = value;
                NotifyPropertyChanged(projetAOuvrirChangeArgs);
            }
        }

        #endregion

        #region commandes

        /// <summary>
        /// Commande de l'ouverture d'un projet
        /// </summary>
        public SimpleCommand OuvrirProjetSelectionneCommand
        {
            get
            {
                if (ouvrirProjetSelectionneCommand == null)
                {
                    ouvrirProjetSelectionneCommand = new SimpleCommand
                    {
                        ExecuteDelegate = delegate
                               {
                                   this.RaiseCloseRequest(true);
                               },
                        CanExecuteDelegate = delegate
                             {
                                 return projetAOuvrir != null;
                             }
                    };
                }
                return ouvrirProjetSelectionneCommand;
            }
        }

        #endregion
    }
}
