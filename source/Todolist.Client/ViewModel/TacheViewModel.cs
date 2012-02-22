using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using Cinch;
using Plugin.Todolist.ValueObjects;
using Todolist.Client.ViewModel;

namespace Todolist.ViewModel
{
    /// <summary>
    /// Classe pour gérer une tâche
    /// </summary>
    public class TacheViewModel : ViewModelBase
    {
        #region Membres privés

        /// <summary>
        /// Stocke le titre de la tâche
        /// </summary>
        private string titre;

        /// <summary>
        /// Stocke la priorité de la tâche
        /// </summary>
        private EnumPriorite prioriteDeLaTache;

        /// <summary>
        /// Stocke la liste des catégories
        /// </summary>
        private List<VOCategorie> listeDesCategories;

        /// <summary>
        /// Stocke l'estimation du temps
        /// </summary>
        private long estimation;

        /// <summary>
        /// Stocke le temps passé sur une tache
        /// </summary>
        private long tpsDepense;

        /// <summary>
        /// Stocke la date limite pour faire la tâche
        /// </summary>
        private DateTime dateLimite;

        /// <summary>
        /// Stocke les personnes du projet dans la combobox
        /// </summary>
        private ObservableCollection<PersonneViewModel> personneProjet;

        /// <summary>
        /// Stocke les personnes ajoutées  à une tâche
        /// </summary>
        private ObservableCollection<string> listePersonnesXml;

        #endregion

        #region Propriétés de présentation

        /// <summary>
        /// Cinch : INPC helper.
        /// </summary>
        private static System.ComponentModel.PropertyChangedEventArgs listePersonnesXmlChangeArgs = Utils.Observable.ObservableHelper.CreateArgs<TacheViewModel>(x => x.ListePersonnesXml);

        /// <summary>
        /// Gets et Sets de la liste des personnes d'une tâche
        /// </summary>
        public ObservableCollection<string> ListePersonnesXml
        {
            get
            {
                return this.listePersonnesXml;
            }
            set
            {
                if (this.listePersonnesXml == value)
                {
                    return;
                }

                this.listePersonnesXml = value;

                NotifyPropertyChanged(listePersonnesXmlChangeArgs);
            }
        }

        /// <summary>
        /// Cinch : INPC helper.
        /// </summary>
        private static System.ComponentModel.PropertyChangedEventArgs personneProjetChangeArgs = Utils.Observable.ObservableHelper.CreateArgs<TacheViewModel>(x => x.PersonneProjet);

        /// <summary>
        ///  Gets et Sets des personnes sur le projet
        /// </summary>
        public ObservableCollection<PersonneViewModel> PersonneProjet
        {
            get
            {
                return this.personneProjet;
            }
            set
            {
                if (this.personneProjet == value)
                {
                    return;
                }

                this.personneProjet = value;

                NotifyPropertyChanged(personneProjetChangeArgs);
            }
        }

        /// <summary>
        /// Cinch : INPC helper.
        /// </summary>
        private static System.ComponentModel.PropertyChangedEventArgs listeDesCategoriesChangeArgs = Utils.Observable.ObservableHelper.CreateArgs<TacheViewModel>(x => x.ListeDesCategories);

        /// <summary>
        /// Gets et Sets de la liste des catégories
        /// </summary>
        public List<VOCategorie> ListeDesCategories
        {
            get
            {
                return this.listeDesCategories;
            }
            set
            {
                if (this.listeDesCategories == value)
                {
                    return;
                }

                this.listeDesCategories = value;

                NotifyPropertyChanged(listeDesCategoriesChangeArgs);
            }
        }

        /// <summary>
        /// Cinch : INPC helper.
        /// </summary>
        private static System.ComponentModel.PropertyChangedEventArgs dateLimiteChangeArgs = Utils.Observable.ObservableHelper.CreateArgs<TacheViewModel>(x => x.DateLimite);

        /// <summary>
        /// Gets et Sets de la date limite
        /// </summary>
        public DateTime DateLimite
        {
            get
            {
                return this.dateLimite;
            }
            set
            {
                if (this.dateLimite == value)
                {
                    return;
                }

                this.dateLimite = value;

                NotifyPropertyChanged(dateLimiteChangeArgs);
            }
        }

        /// <summary>
        /// Cinch : INPC helper.
        /// </summary>
        private static System.ComponentModel.PropertyChangedEventArgs tpsDepenseChangeArgs = Utils.Observable.ObservableHelper.CreateArgs<TacheViewModel>(x => x.TpsDepense);

        /// <summary>
        /// Gets et sets du temps passé sur la tâche 
        /// </summary>
        public long TpsDepense
        {
            get
            {
                return this.tpsDepense;
            }
            set
            {
                if (this.tpsDepense == value)
                {
                    return;
                }

                this.tpsDepense = value;

                NotifyPropertyChanged(tpsDepenseChangeArgs);
            }
        }

        /// <summary>
        /// Cinch : INPC helper.
        /// </summary>
        private static System.ComponentModel.PropertyChangedEventArgs estimationChangeArgs = Utils.Observable.ObservableHelper.CreateArgs<TacheViewModel>(x => x.Estimation);

        /// <summary>
        /// Gets et sets du temps estimé à la tâche
        /// </summary>
        public long Estimation
        {
            get
            {
                return this.estimation;
            }
            set
            {
                if (this.estimation == value)
                {
                    return;
                }

                this.estimation = value;

                NotifyPropertyChanged(estimationChangeArgs);
            }
        }

        /// <summary>
        /// Cinch : INPC helper.
        /// </summary>
        private static System.ComponentModel.PropertyChangedEventArgs titreChangeArgs = Utils.Observable.ObservableHelper.CreateArgs<TacheViewModel>(x => x.Titre);

        /// <summary>
        /// Gets et Sets du titre 
        /// </summary>
        public string Titre
        {
            get
            {
                return this.titre;
            }
            set
            {
                if (this.titre == value)
                {
                    return;
                }

                this.titre = value;
                NotifyPropertyChanged(titreChangeArgs);
            }
        }

        /// <summary>
        /// Cinch : INPC helper.
        /// </summary>
        private static System.ComponentModel.PropertyChangedEventArgs prioriteDeLaTacheChangeArgs = Utils.Observable.ObservableHelper.CreateArgs<TacheViewModel>(x => x.PrioriteDeLaTache);

        /// <summary>
        /// Gets et Sets de la priorité de la tâche
        /// </summary>
        public EnumPriorite PrioriteDeLaTache
        {
            get
            {
                return this.prioriteDeLaTache;
            }
            set
            {
                if (this.prioriteDeLaTache == value)
                {
                    return;
                }

                this.prioriteDeLaTache = value;

                NotifyPropertyChanged(prioriteDeLaTacheChangeArgs);
            }
        }

        #endregion

        #region Methode privées

        /// <summary>
        /// Fonction qui ajoute la liste de personne à la combobox
        /// </summary>
        /// <param name="personne">PersonneViewModel</param>
        public void PersonneAjout(PersonneViewModel personne)
        {
            PersonneProjet.Add(personne);
            personne.PersonneCheckChanged += PersonneAjouter;
        }

        /// <summary>
        /// Evènement déclenché lorsqu que l'on coche ou décoche une personne
        /// </summary>
        /// <param name="affect">bool</param>
        public void PersonneAjouter(bool affect)
        {
            this.ListePersonnesXml = new ObservableCollection<string>();
            this.ListePersonnesXml.Clear();
            foreach (var personne in PersonneProjet)
            {
                if (personne.Affecte)
                {
                    ListePersonnesXml.Add(personne.Nom);
                }
            }
            
        }

        #endregion

        #region Constructeur

        /// <summary>
        /// Constructeur par défaut
        /// </summary>
        public TacheViewModel()
        {
        }

        /// <summary>
        /// Constructeur qui prend une VOTache en paramètre
        /// </summary>
        /// <param name="tache">VOTache</param>
        public TacheViewModel(VOTache tache)
        {
            this.Titre = tache.Titre;
            this.DateLimite = tache.DateLimite;
            this.Estimation = tache.Estimation;
            this.ListeDesCategories = tache.ListeDesCategories;
            this.PrioriteDeLaTache = tache.PrioriteDeLaTache;
            this.PersonneProjet = new ObservableCollection<PersonneViewModel>();
            this.ListePersonnesXml = new ObservableCollection<string>();
        }

        #endregion
    }
}
