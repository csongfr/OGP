using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using Cinch;
using Plugin.Todolist.Service;
using Plugin.Todolist.ValueObjects;
using Plugin.Todolist.View;
using Todolist.Client.ViewModel;
using Todolist.ViewModel;
using Utils.Observable;
using Utils.Wcf;

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
            if (taches == null)
            {
                ListeTachesViewModel.Add(new TacheViewModel(new VOTache()));
                taches.Add(new VOTache());
                Menu.Personnes.Add(new VOPersonne());
            }
            else
            {
                foreach (var ta in taches)
                {
                    ListeTachesViewModel.Add(new TacheViewModel(ta));
                }
                foreach (var personne in Menu.ProjetOuvert.Personnes)
                {
                    Menu.Personnes.Add(personne);
                }
            }
        }

        /// <summary>
        /// fonction qui permet d'enregistrer les tâches
        /// </summary>
        /// <param name="taches">ObservableCollection de VOTache </param>
        private void EnregistrerTache(ObservableCollection<VOTache> taches)
        {
            taches.Clear();
            foreach (var tache in listeTachesViewModel)
            {
                VOTache tacheVO = new VOTache();
                tacheVO.Titre = tache.Titre;
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
        }

        #endregion
    }
}
