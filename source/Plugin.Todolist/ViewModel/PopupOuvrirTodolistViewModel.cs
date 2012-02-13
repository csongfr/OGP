using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using OGP.ValueObjects;
using Utils.Commands;
using Utils.ViewModel;
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
        private List<VOTodolist> listeCouranteTodolist;

        /// <summary>
        /// Projet sélectionné par l'utilisateur
        /// </summary>
        private VOTodolist projetSelectionne;

        /// <summary>
        /// Commande pour ouvrir le fichier sélectionné
        /// </summary>
        private RelayCommand ouvrirProjetSelectionneCommand;

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
            var exception = WcfHelper.Execute<OGP.ServiceWcf.IServiceGestionTaches>(
                               "Plugin.Todolist",
                               client =>
                               {
                                   ListeCouranteTodolist = client.ChargementFichiers();
                               });

            if (exception != null)
            {
                // TODO Gérer exception
            }
        }
        #endregion

        /// <summary>
        /// Cinch : INPC helper.
        /// </summary>
        private static System.ComponentModel.PropertyChangedEventArgs listeCouranteChangeArgs = Utils.Observable.ObservableHelper.CreateArgs<PopupOuvrirTodolistViewModel>(x => x.ListeCouranteTodolist);

        /// <summary>
        /// Gets et sets de la liste des VO disponibles
        /// </summary>
        public List<VOTodolist> ListeCouranteTodolist
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
        private static System.ComponentModel.PropertyChangedEventArgs projetSelectionneChangeArgs = Utils.Observable.ObservableHelper.CreateArgs<PopupOuvrirTodolistViewModel>(x => x.ProjetSelectionne);

        /// <summary>
        /// Gets et sets du projet sélectionné
        /// </summary>
        public VOTodolist ProjetSelectionne
        {
            get
            {
                return this.projetSelectionne;
            }
            set
            {
                if (this.projetSelectionne == value)
                {
                    return;
                }

                this.projetSelectionne = value;
                NotifyPropertyChanged(projetSelectionneChangeArgs);
            }
        }

        /// <summary>
        /// Cinch : INPC helper.
        /// </summary>
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

        #region commandes

        /// <summary>
        /// Commande de l'ouverture d'un projet
        /// </summary>
        public ICommand OuvrirProjetSelectionneCommand
        {
            get
            {
                if (ouvrirProjetSelectionneCommand == null)
                {
                    ouvrirProjetSelectionneCommand = new RelayCommand(
                            delegate
                            {
                                OuvertureActivee = true;
                            },
                            delegate
                            {
                                if (projetSelectionne == null)
                                {
                                    return false;
                                }
                                return true;
                            },
                            true);
                }
                return ouvrirProjetSelectionneCommand;
            }
        }

        #endregion
    }
}
