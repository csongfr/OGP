﻿using System.Collections.ObjectModel;
using Cinch;
using Plugin.Todolist.ValueObjects;
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
        private static System.ComponentModel.PropertyChangedEventArgs menuViewModelChangeArgs = Utils.Observable.ObservableHelper.CreateArgs<TodolistViewModel>(x => x.Menu);

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
        private static System.ComponentModel.PropertyChangedEventArgs tacheVMChangeArgs = Utils.Observable.ObservableHelper.CreateArgs<TodolistViewModel>(x => x.TacheVM);

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
        private static System.ComponentModel.PropertyChangedEventArgs listeTachesVmChangeArgs = Utils.Observable.ObservableHelper.CreateArgs<TodolistViewModel>(x => x.ListeTachesViewModel);

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
            ListeTachesViewModel.Add(new TacheViewModel(new VOTache()));
            taches.Add(new VOTache());

            foreach (var personne in Menu.ProjetOuvert.Personnes)
            {
                Menu.Personnes.Add(personne);
            }
            Menu.Personnes.Add(new VOPersonne());

            // Ajout des catégories au menu
            foreach (var categorie in Menu.ProjetOuvert.Categories)
            {
                Menu.CategoriesProjet.Add(categorie);
            }
            // Menu.CategoriesProjet.Add(new VOCategorie());
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
                tacheVO.Titre = tache.Titre;
                tacheVO.ListeCategoriesTache = tache.ListeCategoriesTache;
                tacheVO.PrioriteDeLaTache = tache.PrioriteDeLaTache;
                taches.Add(tacheVO);
            }

            Menu.ProjetOuvert.Personnes.Clear();
            foreach (var personne in Menu.Personnes)
	        {
		        VOPersonne personneVO = new VOPersonne();
                personneVO.Nom = personne.Nom;
                Menu.ProjetOuvert.Personnes.Add(personneVO);
	        }

            Menu.ProjetOuvert.Categories.Clear();
            foreach (var categorie in Menu.CategoriesProjet)
            {
                VOCategorie cat = new VOCategorie();
                cat.Nom = categorie.Nom;
                Menu.ProjetOuvert.Categories.Add(cat);
            }
        }

        private void AjouterPersonneProjet(ObservableCollection<VOPersonne> personne)
        {
            foreach (var personnes in personne)
            { 
                VOPersonne p = new VOPersonne();
                p.Nom = personnes.Nom;

                foreach (var per in listeTachesViewModel)
                {
                   per.PersonneProjet.Add(p);
               }
            }
        }

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
        }

        #endregion
    }
}
