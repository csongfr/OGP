using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Windows;
using Cinch;
using Plugin.Todolist.Service;
using Plugin.Todolist.ValueObjects;
using Plugin.Todolist.View;
using Todolist.Client.ViewModel;
using Todolist.Exception;
using Utils.Wcf;
using AvalonDock;
using Todolist.Client.Ressources;

namespace Todolist.ViewModel
{
    /// <summary>
    /// Classe qui gère le menu à gauche du plugin ToDoListS
    /// </summary>
    public class MenuViewModel : ViewModelBase
    {
        #region Membre privés

        /// <summary>
        /// Commande qui ouvre la popup
        /// </summary>
        private SimpleCommand nouveauCommand;

        /// <summary>
        /// Permet de communiquer avec la view
        /// </summary>
        private NouvelleGestionTache fenetre;

        /// <summary>
        /// Stocke le projet
        /// </summary>
        private VOProjet projetOuvert;

        /// <summary>
        /// Commande pour enregistrer des tâches
        /// </summary>
        private SimpleCommand enregistrerTaches;

        /// <summary>
        /// Communication avec la vue de la fenêtre ouvrant tous les fichiers
        /// </summary>
        private PopupOuvrirTodolistView fenetreTousFichiers;

        /// <summary>
        /// Commande qui ouvre la fenêtre de sélection de fichiers
        /// </summary>
        private SimpleCommand commandeOuvrirFichier;

        /// <summary>
        /// Stocke les personnes du projet 
        /// </summary>
        private ObservableCollection<VOPersonne> personnes;

        /// <summary>
        /// Catégories dans le menu (volet de gauche)
        /// </summary>
        // private ObservableCollection<VOCategorie> categoriesProjet;

        /// <summary>
        /// Categories du menu
        /// </summary>
        private ObservableCollection<CategoriesMenuViewModel> listeCategoriesMenuVM;

        /// <summary>
        /// Bool pour savoir si la personne est ajouté à la tâche
        /// </summary>
        private bool personneAjout;

        private ObservableCollection<DocumentContent> listeDocumentContent;

        #endregion

        #region Propriétés de présentation

        /// <summary>
        /// Cinch : INPC helper.
        /// </summary>
        private static System.ComponentModel.PropertyChangedEventArgs listeDocumentContentChangeArgs = Utils.Mvvm.ObservableHelper.CreateArgs<MenuViewModel>(x => x.ListeDocumentContent);

        public ObservableCollection<DocumentContent> ListeDocumentContent
        {
            get
            {
                return this.listeDocumentContent;
            }
            set
            {
                if (this.listeDocumentContent == value)
                {
                    return;
                }

                this.listeDocumentContent = value;

                NotifyPropertyChanged(listeDocumentContentChangeArgs);
            }
        }

        /// <summary>
        /// Cinch : INPC helper.
        /// </summary>
        private static System.ComponentModel.PropertyChangedEventArgs listeCategoriesMenuVMChangeArgs = Utils.Mvvm.ObservableHelper.CreateArgs<MenuViewModel>(x => x.ListeCategoriesMenuVM);

        /// <summary>
        /// Gets et sets des catégories du menu
        /// </summary>
        public ObservableCollection<CategoriesMenuViewModel> ListeCategoriesMenuVM
        {
            get
            {
                return this.listeCategoriesMenuVM;
            }
            set
            {
                if (this.listeCategoriesMenuVM == value)
                {
                    return;
                }

                this.listeCategoriesMenuVM = value;

                NotifyPropertyChanged(listeCategoriesMenuVMChangeArgs);
            }
        }

        /// <summary>
        /// Cinch : INPC helper.
        /// </summary>
        // private static System.ComponentModel.PropertyChangedEventArgs categoriesProjetChangeArgs = Utils.Mvvm.ObservableHelper.CreateArgs<MenuViewModel>(x => x.CategoriesProjet);

        /// <summary>
        /// Gets et sets 
        /// </summary>
        /*public ObservableCollection<VOCategorie> CategoriesProjet
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
                // OnCategorieChanged();
                NotifyPropertyChanged(categoriesProjetChangeArgs);
            }
        }*/

        /// <summary>
        /// Cinch : INPC helper.
        /// </summary>
        private static System.ComponentModel.PropertyChangedEventArgs personneAjoutChangeArgs = Utils.Mvvm.ObservableHelper.CreateArgs<MenuViewModel>(x => x.PersonneAjout);

        /// <summary>
        /// Gets et Sets Ajout personne tâche
        /// </summary>
        public bool PersonneAjout
        {
            get
            {
                return this.personneAjout;
            }
            set
            {
                if (this.personneAjout == value)
                {
                    return;
                }

                this.personneAjout = value;

                NotifyPropertyChanged(personneAjoutChangeArgs);
            }
        }

        /// <summary>
        /// Cinch : INPC helper.
        /// </summary>
        private static System.ComponentModel.PropertyChangedEventArgs personnesChangeArgs = Utils.Mvvm.ObservableHelper.CreateArgs<MenuViewModel>(x => x.Personnes);

        /// <summary>
        /// Gets et Sets pour ajouter une personne au projet
        /// </summary>
        public ObservableCollection<VOPersonne> Personnes
        {
            get
            {
                return this.personnes;
            }
            set
            {
                if (this.personnes == value)
                {
                    return;
                }

                this.personnes = value;

                NotifyPropertyChanged(personnesChangeArgs);
            }
        }

        /// <summary>
        /// Cinch : INPC helper.
        /// </summary>
        private static System.ComponentModel.PropertyChangedEventArgs projetOuvertChangeArgs = Utils.Mvvm.ObservableHelper.CreateArgs<MenuViewModel>(x => x.ProjetOuvert);

        /// <summary>
        /// Gets ou Sets pour afficher le nom du projet dans la todolist.
        /// </summary>
        public VOProjet ProjetOuvert
        {
            get
            {
                return this.projetOuvert;
            }
            set
            {
                if (this.projetOuvert == value)
                {
                    return;
                }

                this.projetOuvert = value;

                NotifyPropertyChanged(projetOuvertChangeArgs);
            }
        }

        #endregion

        #region Evènements

        /// <summary>
        /// Evénement levé
        /// </summary>
        public event Action<ObservableCollection<VOTache>> ProjetOuvertChanged;
        
        /// <summary>
        /// Déclenche l'événement ProjetOuvertChanged
        /// </summary>
        private void OnProjetOuvertChanged()
        {
            var handler = ProjetOuvertChanged;

            if (handler != null)
            {
                handler(ProjetOuvert.ListeDesTaches);
            }
        }

        /// <summary>
        /// Evénement levé
        /// </summary>
        public event Action<VOProjet> PersonneChanged;

        /// <summary>
        /// Déclenche l'événement PersonneChanged
        /// </summary>
        private void OnPersonneChanged()
        {
            var handler = PersonneChanged;
           
            if (handler != null)
            {
                handler(projetOuvert);
            }
        }

        /// <summary>
        /// Evénement levé
        /// </summary>
        public event Action<ObservableCollection<PersonneViewModel>> PersonneAjouterChanged;

        /// <summary>
        /// Déclenche l'événement PersonneChanged
        /// </summary>
       /* private void OnPersonneAjouterChanged()
        {
            var handler = PersonneAjouterChanged;

            if (handler != null)
            {
                handler(listePersonneVM);
            }
        }*/

        /// <summary>
        /// Evènement sur les catégories à l'ouverture
        /// </summary>
        public event Action<ObservableCollection<VOCategorie>> CategorieChanged;

        /// <summary>
        /// Déclenche l'événement CategorieChanged
        /// </summary>
        private void OnCategorieChanged()
        {
            var handler = CategorieChanged;

            if (handler != null)
            {
                handler(projetOuvert.Categories);
            }
        }

        /// <summary>
        /// Evénement levé
        /// </summary>
        public event Action<ObservableCollection<VOTache>> ProjetEnregistrerChanged;

        /// <summary>
        /// Déclenche l'événement ProjetEnregistrerChanged
        /// </summary>
        private void OnProjetEnregistrerChanged()
        {
            var handler = ProjetEnregistrerChanged;

            if (handler != null)
            {
                projetOuvert.ListeDesTaches = new ObservableCollection<VOTache>();
                handler(projetOuvert.ListeDesTaches);
            }
        }

        #endregion

        #region Commandes

        /// <summary>
        /// Commande qui ouvre la popup
        /// </summary>
        public SimpleCommand CreerCommand
        {
            get
            {
                if (nouveauCommand == null)
                {
                    nouveauCommand = new SimpleCommand
                    {
                        ExecuteDelegate = delegate
                        {
                            CreerTodolist();
                        }
                    };
                }
                return nouveauCommand;
            }
        }

        /// <summary>
        /// appelle la fonction qui enregistre les modifications sur le projet
        /// </summary>
        public SimpleCommand EnregistrerTaches
        {
            get
            {
                if (enregistrerTaches == null)
                {
                    enregistrerTaches = new SimpleCommand
                    {
                        ExecuteDelegate = delegate
                        {
                            EnregistrerModif();
                        },
                        CanExecuteDelegate = delegate
                        {
                            if (this.projetOuvert == null)
                            {
                                return false;
                            }
                            return true;
                        }
                    };
                }
                return enregistrerTaches;
            }
        }

        /// <summary>
        /// Commande qui ouvre la popup
        /// </summary>
        public SimpleCommand CommandeOuvrirFichier
        {
            get
            {
                if (commandeOuvrirFichier == null)
                {
                    commandeOuvrirFichier = new SimpleCommand
                    {
                        ExecuteDelegate = delegate
                        {
                            OuvrirProjet();
                        }
                    };
                }
                return commandeOuvrirFichier;
            }
        }

        #endregion

        #region Méthodes privées

        /// <summary>
        /// Fonction qui permet d'ouvri une popup
        /// </summary>
        private void OuvrirProjet()
        {
            var visualizerService = Resolve<IUIVisualizerService>();
           
            object popup;
            
            // Ouverture de la popup d'ouverture de projet
            var res = visualizerService.ShowDialog(typeof(PopupOuvrirTodolistView), new PopupOuvrirTodolistViewModel(), out popup);
            
            // Cast pour manipuler l'objet PopupOuvrirTodolistViewModel
            PopupOuvrirTodolistViewModel popupCast = (PopupOuvrirTodolistViewModel)popup;
            // res = popupCast.OuvertureActivee;

            // Gestion de l'exception dans le cas où le repertoire n'existe pas
            if (popupCast.ListeCouranteTodolist == null)
            {
                throw new TodolistPluginException("Pas de fichier");
            }

            if (res == true)
            {
                ListeCategoriesMenuVM = new ObservableCollection<CategoriesMenuViewModel>();
                this.Personnes = new ObservableCollection<VOPersonne>();
                this.Personnes.CollectionChanged += new NotifyCollectionChangedEventHandler(Personnes_CollectionChanged);               
                
                ProjetOuvert = popupCast.ProjetAOuvrir;
                var methodeOuvrirNouveauProjet = ServiceProvider.Resolve<IOuvrirProjet>();
                methodeOuvrirNouveauProjet.InterfaceOuvrirProjet(ProjetOuvert.ListeDesTaches);
                OnProjetOuvertChanged();
                OnPersonneChanged();
                OnCategorieChanged();
            }
        }

        /// <summary>
        /// Cette fonction est déclenchée à chaque ajout d'une personne au projet
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">NotifyCollectionChangedEventArgs</param>
        public void Personnes_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            // MessageBox.Show("toto");
            // OnPersonneAjouterChanged();
        }

        /// <summary>
        /// Fonction qui enregistre les modifications sur le projet
        /// </summary>
        private void EnregistrerModif()
        {
            string enregistrer = MessageBox.Show("Voulez-vous enregistrer les modifications", "Enregistrer", MessageBoxButton.YesNo).ToString();

            if (enregistrer == "Yes")
            {
                var erreur = WcfHelper.Execute<IServiceGestionTaches>(
                    client =>
                    {
                        OnProjetEnregistrerChanged();
                        ProjetOuvert = client.EnregistrerToDoList(ProjetOuvert);
                    });

                if (erreur != null)
                {
                }
            }
        }

        /// <summary>
        /// Permet d'ouvrir une nouvelle gestion de projet
        /// </summary>
        private void CreerTodolist()
        {
            var visualizerService = Resolve<IUIVisualizerService>();
            object popupCreation;

            var ouverturePopupCreation = visualizerService.ShowDialog(typeof(NouvelleGestionTache), new NouvelleGestionTacheViewModel(), out popupCreation);

            if (ouverturePopupCreation == true)
            {
                var exception = WcfHelper.Execute<IServiceGestionTaches>(client =>
                {
                    ListeCategoriesMenuVM = new ObservableCollection<CategoriesMenuViewModel>();
                    this.Personnes = new ObservableCollection<VOPersonne>();
                    this.Personnes.CollectionChanged += new NotifyCollectionChangedEventHandler(Personnes_CollectionChanged);
                    // this.CategoriesProjet = new ObservableCollection<VOCategorie>();
                    // this.CategoriesProjet.CollectionChanged += new NotifyCollectionChangedEventHandler(Categories_CollectionChanged);
                    ProjetOuvert = client.NouvelleToDoList(((NouvelleGestionTacheViewModel)popupCreation).NomDuProjet);
                    projetOuvert.ListeDesTaches = new ObservableCollection<VOTache>();
                    projetOuvert.Categories = new ObservableCollection<VOCategorie>();
                    OnProjetOuvertChanged();

                    var methodeOuvrirNouveauProjet = ServiceProvider.Resolve<IOuvrirProjet>();
                    methodeOuvrirNouveauProjet.InterfaceOuvrirProjet(ProjetOuvert.ListeDesTaches);
                });
                if (ProjetOuvert == null)
                {
                    throw new TodolistPluginException("Pas de fichier");
                }

                // TODO gérer exception
            }
        }
        #endregion

        #region Constructeur

        /// <summary>
        /// Constructeur par défaut
        /// </summary>
        public MenuViewModel()
        {
        }

        #endregion
    }
}
