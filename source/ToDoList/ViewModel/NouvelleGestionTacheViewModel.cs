﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;
using Utils.Commands;
using Utils.ViewModel;
using Utils.Wcf;

namespace Plugin.Todolist.ViewModel
{
    /// <summary>
    /// Classe qui gère la popup
    /// </summary>
    public class NouvelleGestionTacheViewModel : ViewModelBase
    {
        #region Membres privés

        /// <summary>
        /// Stocke le nom du projet est vide
        /// </summary>
        private string nomDuProjet;

        /// <summary>
        /// Booléen permettant de savoir si le bouton "Enregistrer" est bien selectionné
        /// </summary>
        private bool actif;

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
        /// Gets ou Sets pour savoir si le bouton "Enregistrer" est cliqué
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
        /// Gets ou Sets du plugin actif
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
        /// Permet d'enregistrer le nom du projet et du fichier
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
                            return !string.IsNullOrEmpty(NomDuProjet);
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
