using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using OGP.ClientWpf.Extensions;
using OGP.ClientWpf.Extensions.View;
using OGP.ClientWpf.Extensions.ViewModel;
using OGP.Todolist.CommandsToDoList;
using OGP.ValueObjects;
using Utils.Wcf;

namespace OGP.ClientWpf.Extensions
{
    /// <summary>
    /// Mon ToDoList
    /// </summary>
    public class ToDoListViewModel : ViewModelDocumentBase
    {
        #region Membres privés

        /// <summary>
        /// Commande qui ouvre la popup
        /// </summary>
        private RelayCommandToDoList ouvrirFichier;

        /// <summary>
        /// Permet de communiquer avec la view
        /// </summary>
        private NouvelleGestionTache fenetre;

        /// <summary>
        /// Permet de stocker le nom du projet
        /// </summary>
        private string nomProjet; 

        /// <summary>
        /// Permet de rendre Hidden la fenêtre par defaut
        /// </summary>
        private Visibility visible;

        #endregion

        /// <summary>
        /// Cinch : INPC helper.
        /// </summary>
        private static System.ComponentModel.PropertyChangedEventArgs nomProjetChangeArgs = Utils.Observable.ObservableHelper.CreateArgs<ToDoListViewModel>(x => x.NomProjet);

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
        private static System.ComponentModel.PropertyChangedEventArgs visibleChangeArgs = Utils.Observable.ObservableHelper.CreateArgs<ToDoListViewModel>(x => x.Visible);

        /// <summary>
        /// Gets ou Sets pour afficher le todolist.
        /// </summary>
        public Visibility Visible
        {
            get
            {
                return this.visible;
            }
            set
            {
                if (this.visible == value)
                {
                    return;
                }
                this.visible = value;

                NotifyPropertyChanged(visibleChangeArgs);
            }
        }

        #region Méthodes privées

        /// <summary>
        /// Permet d'ouvrir une nouvelle gestion de projet
        /// </summary>
        /// <param name="param">object</param>
        private void Ouvrir(object param)
        {
            fenetre = new NouvelleGestionTache();
            fenetre.ShowDialog();

            if (fenetre.Vm.NomDuProjet != null)
            {
                var exception = WcfHelper.Execute<OGP.ServiceWcf.IServiceGestionTaches>(
                               "ClientTest",
                               client =>
                               {
                                   // VOToDoList listetaches = client.ChagerListeTaches("");
                                   VOToDoList listetaches = client.NouvelleToDoList(fenetre.Vm.NomDuFichier, fenetre.Vm.NomDuProjet);
                               });

                if (exception != null)
                {
                    // TODO : gérer l'exception.
                }
                Visible = Visibility.Visible;
                NomProjet = fenetre.Vm.NomDuProjet;
            }
        }

        /// <summary>
        /// Commande qui ouvre la popup
        /// </summary>
        public ICommand OuvrirFichier
        {
            get
            {
                if (ouvrirFichier == null)
                {
                    ouvrirFichier = new RelayCommandToDoList(Ouvrir);
                }
                return ouvrirFichier;
            }
        }

        #endregion

        #region Constructeur

        /// <summary>
        /// Default constructor
        /// </summary>
        public ToDoListViewModel()
        {
            Visible = Visibility.Hidden;
        }

        #endregion
    }
}
