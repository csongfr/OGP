using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;
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
        /// Stocke si on passe dans le constructeur par défaut.
        /// </summary>
        private bool constructeur;

        /// <summary>
        /// Stocke l'identifiant de la tâche
        /// </summary>
        private int identifiant;

        /// <summary>
        /// Stocke le titre de la tâche
        /// </summary>
        private string titre;

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
        /// Récupére les catégories du Menu
        /// </summary>
        private ObservableCollection<CategorieViewModel> categoriesProjet;

        /// <summary>
        /// Liste des catégories affectées à la tache
        /// </summary>
        private ObservableCollection<string> listeCategoriesTache;

        /// <summary>
        /// Stocke les personnes ajoutées  à une tâche
        /// </summary>
        private ObservableCollection<string> listePersonnesXml;

        /// <summary>
        /// Stocke la priorité de la tâche
        /// </summary>
        private ObservableCollection<PrioriteViewModel> listePriorite;

        /// <summary>
        /// Stocke la Priorité selectionnée
        /// </summary>
        private PrioriteViewModel prioriteSelect;

        /// <summary>
        /// Commande pour supprimer une tâche
        /// </summary>
        private SimpleCommand supprimerTache;

        #endregion

        #region Propriétés de présentation

        /// <summary>
        /// Cinch : INPC helper.
        /// </summary>
        private static System.ComponentModel.PropertyChangedEventArgs constructeurChangeArgs = Utils.Observable.ObservableHelper.CreateArgs<TacheViewModel>(x => x.Constructeur);

        /// <summary>
        /// Gets et sets du constructeur
        /// </summary>
        public bool Constructeur
        {
            get
            {
                return this.constructeur;
            }
            set
            {
                if (this.constructeur == value)
                {
                    return;
                }

                this.constructeur = value;

                NotifyPropertyChanged(constructeurChangeArgs);
            }
        }

        /// <summary>
        /// Cinch : INPC helper.
        /// </summary>
        private static System.ComponentModel.PropertyChangedEventArgs identifiantChangeArgs = Utils.Observable.ObservableHelper.CreateArgs<TacheViewModel>(x => x.Identifiant);

        /// <summary>
        /// Gets et sets de l'identifiant de la tâche
        /// </summary>
        public int Identifiant
        {
            get
            {
                return this.identifiant;
            }
            set
            {
                if (this.identifiant == value)
                {
                    return;
                }

                this.identifiant = value;
                NotifyPropertyChanged(identifiantChangeArgs);
            }
        }

        /// <summary>
        /// Cinch : INPC helper.
        /// </summary>
        private static System.ComponentModel.PropertyChangedEventArgs prioriteSelectChangeArgs = Utils.Observable.ObservableHelper.CreateArgs<TacheViewModel>(x => x.PrioriteSelect);

        /// <summary>
        /// Gets et sets de la priorité sélectionnée.
        /// </summary>
        public PrioriteViewModel PrioriteSelect
        {
            get
            {
                return this.prioriteSelect;
            }
            set
            {
                if (this.prioriteSelect == value)
                {
                    return;
                }

                this.prioriteSelect = value;
                NotifyPropertyChanged(prioriteSelectChangeArgs);
            }
        }

        /// <summary>
        /// Cinch : INPC helper.
        /// </summary>
        private static System.ComponentModel.PropertyChangedEventArgs listePrioriteChangeArgs = Utils.Observable.ObservableHelper.CreateArgs<TacheViewModel>(x => x.ListePriorite);

        /// <summary>
        /// Gets et sets de la liste des priorités de la tâche
        /// </summary>
        public ObservableCollection<PrioriteViewModel> ListePriorite
        {
            get
            {
                return this.listePriorite;
            }
            set
            {
                if (this.listePriorite == value)
                {
                    return;
                }

                this.listePriorite = value;

                NotifyPropertyChanged(listePrioriteChangeArgs);
            }
        }
        
        /// <summary>
        /// Cinch : INPC helper.
        /// </summary>
        private static System.ComponentModel.PropertyChangedEventArgs listeCategoriesTacheChangeArgs = Utils.Observable.ObservableHelper.CreateArgs<TacheViewModel>(x => x.ListeCategoriesTache);

        /// <summary>
        /// Gets et sets de la liste des catégories de la tâche
        /// </summary>
        public ObservableCollection<string> ListeCategoriesTache
        {
            get
            {
                return this.listeCategoriesTache;
            }
            set
            {
                if (this.listeCategoriesTache == value)
                {
                    return;
                }
                this.listeCategoriesTache = value;
                NotifyPropertyChanged(listeCategoriesTacheChangeArgs);
            }
        }

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
                // this.listeCategoriesTache = value;

                // NotifyPropertyChanged(listeCategoriesTacheChangeArgs);
                this.listePersonnesXml = value;

                NotifyPropertyChanged(listePersonnesXmlChangeArgs);
            }
        }

        /// <summary>
        /// Cinch : INPC helper.
        /// </summary>
        private static System.ComponentModel.PropertyChangedEventArgs categoriesProjetChangeArgs = Utils.Observable.ObservableHelper.CreateArgs<TacheViewModel>(x => x.CategoriesProjet);

        /// <summary>
        /// Gets et sets de la liste des catégories du projet
        /// </summary>
        public ObservableCollection<CategorieViewModel> CategoriesProjet
        {
            get
            {
                return this.categoriesProjet;
            }
            set
            {
                if (this.categoriesProjet == value)
                {
                    return;
                }
                this.categoriesProjet = value;
                NotifyPropertyChanged(categoriesProjetChangeArgs);
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

        #endregion

        #region Commandes

        /// <summary>
        /// Commande qui ouvre la popup
        /// </summary>
        public SimpleCommand SupprimerTache
        {
            get
            {
                if (supprimerTache == null)
                {
                    supprimerTache = new SimpleCommand
                    {
                        ExecuteDelegate = delegate
                        {
                            SupprimeTache();
                        }
                    };
                }
                return supprimerTache;
            }
        }

        #endregion

        #region Evènements

        /// <summary>
        /// Evénement levé
        /// </summary>
        public event Action<int> SupprimerTacheChanged;

        /// <summary>
        /// Déclenche l'événement PersonneCheckChanged
        /// </summary>
        private void OnSupprimerTacheChanged()
        {
            var handler = SupprimerTacheChanged;

            if (handler != null)
            {
                handler(Identifiant);
            }
        }

        #endregion

        #region Methode privées

        /// <summary>
        /// Permet de supprimer un tâche
        /// </summary>
        private void SupprimeTache()
        {
            OnSupprimerTacheChanged();
        }

        /// <summary>
        /// Permet d'ajouter une catégorie
        /// </summary>
        /// <param name="categorieVM">la catégorie à ajouter</param>
        public void AjouterCategorie(CategorieViewModel categorieVM)
        {
            CategoriesProjet.Add(categorieVM);
            categorieVM.CheckBoxCocheChanged += AjoutCategorieViewModel;
        }

        /// <summary>
        /// Ajoute une catégorie cochée à la tache
        /// </summary>
        /// <param name="categorieVM">La catégorie cochée</param>
        private void AjoutCategorieViewModel(CategorieViewModel categorieVM)
        {
            if (categorieVM.Check)
            {
                ListeCategoriesTache.Add(categorieVM.Nom);
            }
            else
            {
                ListeCategoriesTache.Remove(categorieVM.Nom);
            }
        }
		
	    /// <summary>
        /// Fonction qui ajoute la liste de personne à la combobox
        /// </summary>
        /// <param name="personne">PersonneViewModel</param>
        public void PersonneAjout(PersonneViewModel personne)
        {
            if (personne.Nom != null)
            {
                PersonneProjet.Add(personne);
                personne.PersonneCheckChanged += PersonneAjouter;
            }
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

        /// <summary>
        /// Fonction pour ajouter les priorités
        /// </summary>
        /// <param name="constructeurDefault">bool pour savoir le construteur utiliser</param>
        /// <param name="tache">VOTache</param>
        public void AjouterPriorite(bool constructeurDefault, VOTache tache)
        {
            for (int i = 1; i <= 5; i++)
            {
                PrioriteViewModel priorite = new PrioriteViewModel();
                priorite.Texte = i.ToString();
                switch (i)
                {
                    case 1:
                        priorite.Couleur = Brushes.Green;
                        break;
                    case 2:
                        priorite.Couleur = Brushes.Blue;
                        break;
                    case 3:
                        priorite.Couleur = Brushes.Yellow;
                        break;
                    case 4:
                        priorite.Couleur = Brushes.Orange;
                        break;
                    case 5:
                        priorite.Couleur = Brushes.Red;
                        break;
                }
                listePriorite.Add(priorite);
                if (constructeurDefault == true)
                {
                    if (tache.PrioriteDeLaTache == priorite.Texte)
                    {
                        this.prioriteSelect = priorite;
                    }
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
            this.PersonneProjet = new ObservableCollection<PersonneViewModel>();
            listeCategoriesTache = new ObservableCollection<string>();
            this.listePriorite = new ObservableCollection<PrioriteViewModel>();
            this.prioriteSelect = new PrioriteViewModel();
            this.Constructeur = false;
            this.AjouterPriorite(this.Constructeur, null);
        }

        /// <summary>
        /// Constructeur qui prend une VOTache en paramètre
        /// </summary>
        /// <param name="tache">VOTache</param>
        public TacheViewModel(VOTache tache)
        {
            this.prioriteSelect = new PrioriteViewModel();
            this.Titre = tache.Titre;
            this.Identifiant = tache.Identifiant;
            this.DateLimite = tache.DateLimite;
            this.Estimation = tache.Estimation;
            this.PersonneProjet = new ObservableCollection<PersonneViewModel>();
            this.ListeCategoriesTache = tache.ListeCategoriesTache;
            this.CategoriesProjet = new ObservableCollection<CategorieViewModel>();
            this.ListePersonnesXml = new ObservableCollection<string>();
            this.listePriorite = new ObservableCollection<PrioriteViewModel>();
            this.Constructeur = true;
            this.AjouterPriorite(this.Constructeur, tache);
        }

        #endregion
    }
}
