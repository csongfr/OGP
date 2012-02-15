using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using AvalonDock;
using Utils.Commands;
using Utils.ViewModel;

namespace OGP.ClientWpf.ViewModel
{
    /// <summary>
    /// Fenêtre principale
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        #region Membres privés

        /// <summary>
        /// Commande qui ferme l'application
        /// </summary>
        private RelayCommand fermerCommand;

        /// <summary>
        /// Stocke la commande chargeant les plugins disponibles
        /// </summary>
        private RelayCommand recupererCommand;

        /// <summary>
        /// Commande ajoute un plugin
        /// </summary>
        private RelayCommand chargerPlugin;

        /// <summary>
        /// Commande qui supprime un plugin
        /// </summary>
        private RelayCommand supprimePlugin;

        /// <summary>
        /// Liste des répertoire contenant les plugins
        /// </summary>
        // private List<DirectoryCatalog> repertoiresPlugins;

        /// <summary>
        /// Stocke la liste de tous les plugins
        /// </summary>
        private ObservableCollection<DocumentContent> listePlugins;

        /// <summary>
        /// Stock le plugin actif.
        /// </summary>
        private DocumentContent pluginActif;

        /// <summary>
        /// Stock la liste des plugins actifs.
        /// </summary>
        private ObservableCollection<DocumentContent> listeDocuments;

        #endregion

        #region Membres publiques

        /// <summary>
        /// Cinch : INPC Helper
        /// </summary>
        private static System.ComponentModel.PropertyChangedEventArgs listePluginsChangeArgs = Utils.Observable.ObservableHelper.CreateArgs<MainViewModel>(x => x.ListePlugins);

        /// <summary>
        /// Stocke la liste de tous les plugins
        /// </summary>
        [ImportMany]
        public ObservableCollection<DocumentContent> ListePlugins
        {
            get
            {
                return this.listePlugins;
            }
            set
            {
                this.listePlugins = value;
            }
        }

        /// <summary>
        /// Cinch : INPC Helper
        /// </summary>
        private static System.ComponentModel.PropertyChangedEventArgs listeDocumentsChangeArgs = Utils.Observable.ObservableHelper.CreateArgs<MainViewModel>(x => x.ListeDocuments);

        /// <summary>
        /// Get et set des plugins
        /// </summary>
        public ObservableCollection<DocumentContent> ListeDocuments
        {
            get
            {
                return listeDocuments;
            }
            set
            {
                this.listeDocuments = value;
                NotifyPropertyChanged(listeDocumentsChangeArgs);
            }
        }

        /// <summary>
        /// Cinch : INPC Helper
        /// </summary>
        private static System.ComponentModel.PropertyChangedEventArgs pluginActifChangeArgs = Utils.Observable.ObservableHelper.CreateArgs<MainViewModel>(x => x.PluginActif);

        /// <summary>
        /// Gets ou Sets du plugin actif.
        /// </summary>
        public DocumentContent PluginActif
        {
            get
            {
                return pluginActif;
            }
            set
            {
                this.pluginActif = value;

                NotifyPropertyChanged(pluginActifChangeArgs);
            }
        }

        #endregion

        #region Commandes

        /// <summary>
        /// Supprime un plugin
        /// </summary>
        public ICommand RecupererPlugins
        {
            get
            {
                if (recupererCommand == null)
                {
                    recupererCommand = new RelayCommand(x => this.ChargerPluginsDisponibles());
                }
                return recupererCommand;
            }
        }

        /// <summary>
        /// Supprime un plugin
        /// </summary>
        public ICommand SupprimePlugin
        {
            get
            {
                if (supprimePlugin == null)
                {
                    supprimePlugin = new RelayCommand(
                        delegate
                        {
                            SupprimerPlugin(PluginActif.Title);
                        },
                        delegate
                        {
                            return IsPluginCharge();
                        },
                        true);
                }
                return supprimePlugin;
            }
        }

        /// <summary>
        /// Ajoute un plugin
        /// </summary>
        public ICommand ChargerPlugin
        {
            get
            {
                if (chargerPlugin == null)
                {
                    chargerPlugin = new RelayCommand(
                        delegate
                        {
                            ChargerPluginActif(PluginActif.Title);
                        },
                        delegate
                        {
                            return pluginActif != null && !IsPluginCharge();
                        },
                        true);
                }
                return chargerPlugin;
            }
        }

        /// <summary>
        /// Exit from the application
        /// </summary>
        public ICommand FermerCommand
        {
            get
            {
                if (fermerCommand == null)
                {
                    fermerCommand = new RelayCommand(x => System.Windows.Application.Current.Shutdown());
                }
                return fermerCommand;
            }
        }

        #endregion

        #region Constructeur

        /// <summary>
        /// Default constructor
        /// </summary>
        public MainViewModel()
        {
            this.ListeDocuments = new ObservableCollection<DocumentContent>();

            ChargerPluginsDisponibles();
        }

        #endregion

        #region Méthodes privées

        /// <summary>
        /// Supprime le plugin de la fenêtre
        /// </summary>
        /// <param name="nomPlugin">Nom du plugin à Supprimer</param>
        private void SupprimerPlugin(string nomPlugin)
        {
            DocumentContent pluginCourant = ListeDocuments.SingleOrDefault(doc => doc.Title == nomPlugin);

            if (pluginCourant != null)
            {
                ListeDocuments.Remove(pluginCourant);
            }
            else
            {
                throw new ClientException("Pas de plugin");
            }   
        }

        /// <summary>
        /// appelle la fonction ChargerPlugins lors du click sur ajouter plugin
        /// </summary>
        /// <param name="nomPlugin">Nom du plugin à charger.</param>
        private void ChargerPluginActif(string nomPlugin)
        {
            // On ajoute le plugin aux documents
            foreach (var plugin in this.ListePlugins)
            {
                if (plugin.Title.Equals(nomPlugin))
                {
                    ListeDocuments.Add(plugin);
                }
            }
        }

        /// <summary>
        /// Récupère les plugins présents dans le répertoire défini dans le fichier de config du client
        /// </summary>
        private void ChargerPluginsDisponibles()
        {
            try
            {
                var section = ConfigurationManager.GetSection("OGP.ClientWpf") as NameValueCollection;
                string repertoire = section["repertoirePlugins"].ToString();

                var catalog = new AggregateCatalog();
                catalog.Catalogs.Add(new DirectoryCatalog(repertoire));
                CompositionContainer cataloguePlugins = new CompositionContainer(catalog);
                cataloguePlugins.ComposeParts(this);
            }
            catch (CompositionException compositionException)
            {
                Console.WriteLine(compositionException.ToString());
            }
            catch (DirectoryNotFoundException ex)
            {
                throw new ClientException("Le répertoire des plugins n'existe pas. Aucun plugin ne sera chargé", ex); 
            }
        }

        /// <summary>
        /// Permet de savoir si le plugin est déjà chargé.
        /// </summary>
        /// <returns>True or False.</returns>
        private bool IsPluginCharge()
        {
            if (pluginActif == null)
            {
                return false; 
            }
            else
            {
                return ListeDocuments.Any(doc => doc.Title == pluginActif.Title);
            }
        }

        #endregion
    }
}
