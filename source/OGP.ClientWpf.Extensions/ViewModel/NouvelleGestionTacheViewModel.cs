using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;
using OGP.ClientWpf.Extensions.ViewModel;
using OGP.ValueObjects;
using Utils.Commands;
using Utils.ViewModel;
using Utils.Wcf;

namespace OGP.ClientWpf.Extensions.ViewModel
{
    /// <summary>
    /// Classe qui gère la popup
    /// </summary>
    public class NouvelleGestionTacheViewModel : ViewModelBase
    {
        #region Membres privés

        /// <summary>
        /// Stocke le  nom du projet est vide
        /// </summary>
        private string nomDuProjet;

        /// <summary>
        /// bool pour savoir si enregistrer et bien selectionné
        /// </summary>
        private bool actif;

        /// <summary>
        /// Stocke le  nom du fichier est vide
        /// </summary>
        private string nomDuFichier;

        /// <summary>
        /// Commande pour enregistrer le nom du projet et du fichier
        /// </summary>
        private RelayCommand enregistrer;

        #endregion

        /// <summary>
        /// Cinch : INPC helper.
        /// </summary>
        private static System.ComponentModel.PropertyChangedEventArgs sucessChangeArgs = Utils.Observable.ObservableHelper.CreateArgs<NouvelleGestionTacheViewModel>(x => x.Actif);

        /// <summary>
        /// Gets ou Sets pour savoir si le bouton enregistrer est cliqué.
        /// </summary>
        public bool Actif
        {
            get
            {
                return this.actif;
            }
            set
            {
                if (this.actif == value)
                {
                    return;
                }

                this.actif = value;

                NotifyPropertyChanged(sucessChangeArgs);
            }
        }

        /// <summary>
        /// Cinch : INPC Helper
        /// </summary>
        private static System.ComponentModel.PropertyChangedEventArgs nomDuProjetChangeArgs = Utils.Observable.ObservableHelper.CreateArgs<NouvelleGestionTacheViewModel>(x => x.NomDuProjet);

        /// <summary>
        /// Gets ou Sets du plugin actif.
        /// </summary>
        public string NomDuProjet
        {
            get
            {
                return nomDuProjet;
            }
            set
            {
                if (nomDuProjet == value)
                {
                    return;
                }
                this.nomDuProjet = value;
                NotifyPropertyChanged(nomDuProjetChangeArgs);
            }
        }

        #region Commandes

        /// <summary>
        /// permet d'enregistrer le nom du projet et du fichier
        /// </summary>
        public ICommand Enregistrer
        {
            get
            {
                if (enregistrer == null)
                {
                    enregistrer = new RelayCommand(
                        delegate
                        {
                            Actif = true;
                        },
                        delegate
                        {
                            if ( (!string.IsNullOrEmpty(NomDuProjet) == false))
                            {
                                return false;
                            }
                            return true;
                        },
                        true);
                }
                return enregistrer;
            }
        }

        #endregion

        #region Constructeur

        /// <summary>
        /// Constructeur
        /// </summary>
        public NouvelleGestionTacheViewModel()
        {
            Actif = false;
        }

        #endregion
    }
}
