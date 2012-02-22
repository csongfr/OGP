using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Cinch;
using Plugin.Todolist.ValueObjects;

namespace Todolist.Client.ViewModel
{
    /// <summary>
    /// Classe pour une personne
    /// </summary>
    public class PersonneViewModel : ViewModelBase
    {
        #region Membres privés

        /// <summary>
        /// Stocke le nom de la personne
        /// </summary>
        private string nom;

        /// <summary>
        /// Stock si la personne est affecté à une tâche
        /// </summary>
        private bool affecte;

        #endregion

        #region Propriétés de présentation

        /// <summary>
        /// Cinch : INPC helper.
        /// </summary>
        private static System.ComponentModel.PropertyChangedEventArgs nomChangeArgs = Utils.Observable.ObservableHelper.CreateArgs<PersonneViewModel>(x => x.Nom);

        /// <summary>
        /// Gets et Sets du nom de la personne
        /// </summary>
        public string Nom
        {
            get
            {
                return this.nom;
            }
            set
            {
                if (this.nom == value)
                {
                    return;
                }

                this.nom = value;

                NotifyPropertyChanged(nomChangeArgs);
            }
        }

        /// <summary>
        /// Cinch : INPC helper.
        /// </summary>
        private static System.ComponentModel.PropertyChangedEventArgs affecteChangeArgs = Utils.Observable.ObservableHelper.CreateArgs<PersonneViewModel>(x => x.Affecte);

        /// <summary>
        /// Gets et Sets d'un personne affecté à une tâche
        /// </summary>
        public bool Affecte
        {
            get
            {
                return this.affecte;
            }
            set
            {
                if (this.affecte == value)
                {
                    return;
                }

                this.affecte = value;
                 
                NotifyPropertyChanged(affecteChangeArgs);
                OnPersonneCheckChanged();
            }
        }

        #endregion

        #region Evènements

        /// <summary>
        /// Evénement levé
        /// </summary>
        public event Action<bool> PersonneCheckChanged;

        /// <summary>
        /// Déclenche l'événement PersonneCheckChanged
        /// </summary>
        private void OnPersonneCheckChanged()
        {
            var handler = PersonneCheckChanged;

            if (handler != null)
            {
                handler(Affecte);
            }
        }

        #endregion

        #region Constructeur

        /// <summary>
        /// Constructeur par défaut
        /// </summary>
        public PersonneViewModel()
        {
        }

        /// <summary>
        ///  Constructeur qui prend une VOPersonne en paramètre
        /// </summary>
        /// <param name="personne">VOPersonne</param>
        public PersonneViewModel(VOPersonne personne)
        {
            this.Nom = personne.Nom;
            this.Affecte = personne.Affecte;
        }

        #endregion
    }
}
