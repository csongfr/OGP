using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;
using OGP.ClientWpf.Comands;
using OGP.ClientWpf.View;

namespace OGP.ClientWpf.ViewModel
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
        /// Stocke le  nom du fichier est vide
        /// </summary>
        private string nomDuFichier;

        /// <summary>
        /// Commande pour enregistrer le nom du projet et du fichier
        /// </summary>
        private RelayCommand enregistrer;

        #endregion

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

        /// <summary>
        /// Cinch : INPC Helper
        /// </summary>
        private static System.ComponentModel.PropertyChangedEventArgs nomDuFichierChangeArgs = Utils.Observable.ObservableHelper.CreateArgs<NouvelleGestionTacheViewModel>(x => x.NomDuFichier);

        /// <summary>
        /// Gets ou Sets NomDuFichier.
        /// </summary>
        public string NomDuFichier
        {
            get
            {
                return nomDuFichier;
            }
            set
            {
                if (nomDuFichier == value)
                {
                    return;
                }
                this.nomDuFichier = value;
                NotifyPropertyChanged(nomDuFichierChangeArgs);
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
                            MessageBox.Show("Nom du Projet :" + " " + NomDuProjet + "\n" + "Nom du Fichier :" + " " + NomDuFichier);
                        },
                        delegate
                        {
                            if (!string.IsNullOrEmpty(NomDuProjet) == false || !string.IsNullOrEmpty(NomDuFichier) == false)
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
        }
        #endregion
    }
}
