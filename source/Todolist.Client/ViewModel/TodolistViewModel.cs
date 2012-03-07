using System.Collections.ObjectModel;
using System.Windows;
using Cinch;
using Plugin.Todolist.ValueObjects;
using Todolist.Client.ViewModel;
using Todolist.ViewModel;

namespace Plugin.Todolist
{
    /// <summary>
    /// Mon ToDoList
    /// </summary>
    public class TodolistViewModel : ViewModelBase
    {
        #region Membres privés

        /// <summary>
        /// Stocke l'identifiant de la tâche
        /// </summary>
        private int id;

        /// <summary>
        /// permet de gérer le ViewModel du menu
        /// </summary>
        private MenuViewModel menuViewModel;

        /// <summary>
        /// Permet de gérer le ViewModel des tâches
        /// </summary>
        private TacheViewModel tacheVM;

        /// <summary>
        /// Liste des tâches
        /// </summary>
        private ObservableCollection<TacheViewModel> listeTachesViewModel;

        #endregion

        #region Propriétés de présentation

        /// <summary>
        /// Cinch : INPC helper.
        /// </summary>
        private static System.ComponentModel.PropertyChangedEventArgs menuViewModelChangeArgs = Utils.Mvvm.ObservableHelper.CreateArgs<TodolistViewModel>(x => x.Menu);

        /// <summary>
        /// Gets et Sets du MenuViewModel
        /// </summary>
        public MenuViewModel Menu
        {
            get
            {
                return this.menuViewModel;
            }
            set
            {
                if (this.menuViewModel == value)
                {
                    return;
                }
                this.menuViewModel = value;
                NotifyPropertyChanged(menuViewModelChangeArgs);
            }
        }

        /// <summary>
        /// Cinch : INPC helper.
        /// </summary>
        private static System.ComponentModel.PropertyChangedEventArgs tacheVMChangeArgs = Utils.Mvvm.ObservableHelper.CreateArgs<TodolistViewModel>(x => x.TacheVM);

        /// <summary>
        /// Gets et Sets de la tache ViewModel
        /// </summary>
        public TacheViewModel TacheVM
        {
            get
            {
                return this.tacheVM;
            }
            set
            {
                if (this.tacheVM == value)
                {
                    return;
                }

                this.tacheVM = value;
                NotifyPropertyChanged(tacheVMChangeArgs);
            }
        }

        /// <summary>
        /// Cinch : INPC helper.
        /// </summary>
        private static System.ComponentModel.PropertyChangedEventArgs listeTachesVmChangeArgs = Utils.Mvvm.ObservableHelper.CreateArgs<TodolistViewModel>(x => x.ListeTachesViewModel);

        /// <summary>
        /// Gets ou Sets la liste des taches.
        /// </summary>
        public ObservableCollection<TacheViewModel> ListeTachesViewModel
        {
            get
            {
                return this.listeTachesViewModel;
            }
            set
            {
                if (this.listeTachesViewModel == value)
                {
                    return;
                }

                this.listeTachesViewModel = value;
                NotifyPropertyChanged(listeTachesVmChangeArgs);
            }
        }

        #endregion

        #region Methode privées

        /// <summary>
        /// Fonction qui permet d'ajouter une tâche
        /// </summary>
        /// <param name="taches">List VOTache</param>
        private void AfficherTacheOuverture(ObservableCollection<VOTache> taches)
        {
            ListeTachesViewModel = new ObservableCollection<TacheViewModel>();
                      
            foreach (var ta in taches)
            {
                ListeTachesViewModel.Add(new TacheViewModel(ta));
            }
            foreach (var tacheView in listeTachesViewModel)
            {
                tacheView.SupprimerTacheChanged += TacheVM_SupprimerTache;
            }
            this.ListeTachesViewModel.CollectionChanged += new System.Collections.Specialized.NotifyCollectionChangedEventHandler(ListeTachesViewModel_CollectionChanged);
            
            foreach (var personne in Menu.ProjetOuvert.Personnes)
            {
                Menu.Personnes.Add(personne);
            }
      
            // Ajout des catégories au menu
            foreach (var categorie in Menu.ProjetOuvert.Categories)
            {
                Menu.CategoriesProjet.Add(categorie);
            }
        }

        /// <summary>
        /// Cette fonction est décenchée à l'ajout d'une tâche.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">NotifyCollectionChangedEventArgs</param>
        public void ListeTachesViewModel_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            int nombreTaches = ListeTachesViewModel.Count;
            int i = 0;

            if (nombreTaches == 1)
            {
                id++;
            }
            foreach (var tacheid in ListeTachesViewModel)
            {
                i++;
                if (i == nombreTaches - 1)
                {
                    id = tacheid.Identifiant + 1;
                }
            }

            int nombreTaches2 = id++;
            foreach (var tache in ListeTachesViewModel)
            {
                if (tache.Identifiant == 0)
                {
                    tache.Identifiant = nombreTaches2;
                }
                tache.SupprimerTacheChanged += TacheVM_SupprimerTache;
            }
        }

        /// <summary>
        /// fonction qui permet d'enregistrer les tâches
        /// </summary>
        /// <param name="taches">Liste des taches du projet ouvert</param>
        private void EnregistrerTache(ObservableCollection<VOTache> taches)
        {
            taches.Clear();
            // On affecte à chaque tache du projetCourant les taches du ViewModel
            foreach (var tache in listeTachesViewModel)
            {
                VOTache tacheVO = new VOTache();
                tacheVO.Identifiant = tache.Identifiant;
                tacheVO.PrioriteDeLaTache = tache.PrioriteSelect.Texte;
                tacheVO.Titre = tache.Titre;
                tacheVO.ListeCategoriesTache = tache.ListeCategoriesTache;
                taches.Add(tacheVO);
            }
            Menu.ProjetOuvert.Personnes.Clear();
            foreach (var personne in Menu.Personnes)
            {
                if (personne.Nom != null)
                {
                    VOPersonne personneVO = new VOPersonne();
                    personneVO.Nom = personne.Nom;
                    Menu.ProjetOuvert.Personnes.Add(personneVO);
                }
	        }

            Menu.ProjetOuvert.Categories.Clear();
            foreach (var categorie in Menu.CategoriesProjet)
            {
                VOCategorie cat = new VOCategorie();
                cat.Nom = categorie.Nom;
                Menu.ProjetOuvert.Categories.Add(cat);
            }

            foreach (var personnes in taches)
            {
                personnes.ListePersonnesXml = new ObservableCollection<string>();
                foreach (var personneTache in listeTachesViewModel)
                {
                    if (personnes.Titre == personneTache.Titre)
                    {
                        if (personneTache.ListePersonnesXml == null || personneTache.ListePersonnesXml.Count == 0)
                        {
                            personneTache.ListePersonnesXml = new ObservableCollection<string>();
                        }
                        else
                        {
                            personnes.ListePersonnesXml = personneTache.ListePersonnesXml;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Foncion qui permet d'ajouter des personnes à une tâche
        /// </summary>
        /// <param name="projet"> Liste de VOProjet</param>
        private void AjouterPersonneProjet(VOProjet projet)
        {
            foreach (var per in listeTachesViewModel)
            {
                per.PersonneProjet.Clear();
                ObservableCollection<string> test = new ObservableCollection<string>();
                foreach (var liste in projet.ListeDesTaches)
                {
                    if (per.Titre == liste.Titre)
                    {
                        test = liste.ListePersonnesXml;
                        if (test.Count != 0)
                        {
                            per.ListePersonnesXml = liste.ListePersonnesXml;
                            foreach (var personnes in projet.Personnes)
                            {
                                bool affecte = false;
                            
                                foreach (var t in test)
                                {
                                    if (personnes.Nom == t)
                                    {
                                        affecte = true;
                                    }
                                }
                                if (affecte)
                                {
                                    PersonneViewModel p2 = new PersonneViewModel();
                                    p2.Nom = personnes.Nom;
                                    p2.Affecte = true;
                                    per.PersonneAjout(p2);
                                }
                                else 
                                {
                                    PersonneViewModel p2 = new PersonneViewModel();
                                    p2.Nom = personnes.Nom;
                                    p2.Affecte = false;
                                    per.PersonneAjout(p2); 
                                }
                            }
                        }
                        else
                        {
                            foreach (var personnes in projet.Personnes)
                            {
                                PersonneViewModel p2 = new PersonneViewModel();
                                p2.Nom = personnes.Nom;
                                p2.Affecte = false;
                                per.PersonneAjout(p2);
                            }
                        }
                    }
                }
            }
        }

        /*private void AjouterPersonne(VOProjet projet)
        {
            foreach (var taches in listeTachesViewModel)
            {
                //ObservableCollection<PersonneViewModel> personneProjetTemp = new ObservableCollection<PersonneViewModel>();
                if (taches.PersonneProjet.Count != 0)
                {
                    //foreach (var pers in taches.PersonneProjet)
                    //{
                        foreach (var personne in Menu.Personnes)
                        {
                            if (personne.Nom != null )
                            {
                                PersonneViewModel p2 = new PersonneViewModel();
                                p2.Nom = personne.Nom;
                                p2.Affecte = false;
                                taches.PersonneProjet.Add(p2);
                            }
                        }
                    //}

                }
                else
                {
                    foreach (var personne in Menu.Personnes)
                    {
                        if (personne.Nom != null)
                        {
                            PersonneViewModel p2 = new PersonneViewModel();
                            p2.Nom = personne.Nom;
                            p2.Affecte = false;
                            taches.PersonneProjet.Add(p2);
                        }
                    }
                }
            }
        }*/

        /// <summary>
        /// Permet d'ajouter les catégories du Menu à chaque tâche
        /// </summary>
        /// <param name="categories">Collection de catégories</param>
        private void AjoutCategorieProjet(ObservableCollection<VOCategorie> categories)
        {
            // Ajout des catégories du projet à chaque tacheViewModel
            foreach (var tache in ListeTachesViewModel)
            {
                foreach (var categorie in /*Menu.CategoriesProjet*/categories)
                {
                    CategorieViewModel catVM = new CategorieViewModel();
                    catVM.Nom = categorie.Nom;
                    tache.AjouterCategorie(catVM);
                }

                if (tache.ListeCategoriesTache != null)
                {
                    foreach (var categorieTache in tache.ListeCategoriesTache)
                    {
                        foreach (var categorieProjet in tache.CategoriesProjet)
                        {
                            if (categorieProjet.Nom.Equals(categorieTache))
                            {
                                categorieProjet.CheckOuverture = true;
                                categorieProjet.Check = true;
                                categorieProjet.CheckOuverture = false;
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Fonction qui supprime une tâche de la liste.
        /// </summary>
        /// <param name="identifiant">int</param>
        private void TacheVM_SupprimerTache(int identifiant)
        {
            TacheViewModel tachesViewModelTemp = new TacheViewModel();

            foreach (var taches in ListeTachesViewModel)
            {
                if (identifiant == taches.Identifiant)
                {
                    tachesViewModelTemp = taches;
                }
            }
            ListeTachesViewModel.Remove(tachesViewModelTemp);
        }

        #endregion

        #region Constructeur

        /// <summary>
        /// Default constructor
        /// </summary>
        public TodolistViewModel()
        {
            this.Menu = new MenuViewModel();
            this.TacheVM = new TacheViewModel();
            this.Menu.ProjetOuvertChanged += AfficherTacheOuverture;
            this.Menu.ProjetEnregistrerChanged += EnregistrerTache;
            this.Menu.PersonneChanged += AjouterPersonneProjet;
            this.Menu.CategorieChanged += AjoutCategorieProjet;
            // this.Menu.PersonneAjouterChanged += AjouterPersonne;
        }
        #endregion
    }
}
