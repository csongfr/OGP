using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Cinch;
using Plugin.Todolist.Service;
using Plugin.Todolist.ValueObjects;
using Utils.Wcf;

namespace Todolist.ViewModel
{
    /// <summary>
    /// Popup permettant de générer la liste des fichiers existants
    /// </summary>
    public class PopupOuvrirTodolistViewModel : ViewModelBase
    {
        #region membres privés
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

        /// <summary>
        /// permet de savoir si l'on a cliqué sur ouvrir
        /// </summary>
        private bool ouvertureActivee;

        #endregion

        #region constructeur

        /// <summary>
        /// Constructeur de la popup
        /// </summary>
        public PopupOuvrirTodolistViewModel()
        {
            OuvertureActivee = false;
            // Appel au service pour charger les VOTodolist
            var exception = WcfHelper.Execute<IServiceGestionTaches>(
                               "Todolist",
                               client =>
                               {
                                   ListeCouranteTodolist = client.ChargementFichiers();
                                   if ((this.listeCouranteTodolist != null) && (this.listeCouranteTodolist.Count == 1))
                                   {
                                       projetAOuvrir = ListeCouranteTodolist.First();
                                   }
                               });

            if (exception != null)
            {
                // TODO Gérer exception
            }
        }
        #endregion

        #region Propriétés de présentation
        /// <summary>
        /// Cinch : INPC helper.
        /// </summary>
        private static System.ComponentModel.PropertyChangedEventArgs listeCouranteChangeArgs = Utils.Observable.ObservableHelper.CreateArgs<PopupOuvrirTodolistViewModel>(x => x.ListeCouranteTodolist);

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
        private static System.ComponentModel.PropertyChangedEventArgs projetAOuvrirChangeArgs = Utils.Observable.ObservableHelper.CreateArgs<PopupOuvrirTodolistViewModel>(x => x.ProjetAOuvrir);

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

        /// <summary>
        /// Cinch : INPC helper.
        /// </summary>
        /// 
        private static System.ComponentModel.PropertyChangedEventArgs ouvertureActiveeChangeArgs = Utils.Observable.ObservableHelper.CreateArgs<PopupOuvrirTodolistViewModel>(x => x.OuvertureActivee);

        /// <summary>
        /// Gets et sets de l'ouverture du projet
        /// </summary>
        public bool OuvertureActivee
        {
            get
            {
                return this.ouvertureActivee;
            }
            set
            {
                if (this.ouvertureActivee == value)
                {
                    return;
                }

                this.ouvertureActivee = value;

                NotifyPropertyChanged(ouvertureActiveeChangeArgs);
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
                                   OuvertureActivee = true;
                               },
                        CanExecuteDelegate = delegate
                             {
                                 if (projetAOuvrir == null)
                                 {
                                     return false;
                                 }
                                 return true;
                             }
                    };
                }
                return ouvrirProjetSelectionneCommand;
            }
        }

        #endregion
    }
}
