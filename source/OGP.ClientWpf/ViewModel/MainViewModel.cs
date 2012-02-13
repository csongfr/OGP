using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;
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
        /// Stock le catalogue des plugins
        /// </summary>
        private CompositionContainer cataloguePlugins;

        /// <summary>
        /// Stock le plugin actif.
        /// </summary>
        private ComposablePartDefinition pluginActif;

        /// <summary>
        /// Stock la liste des plugins actifs.
        /// </summary>
        private ObservableCollection<DocumentContent> listeDocuments;

        #endregion

        #region Membres publiques

        /// <summary>
        /// Stock la listes de tous les plugins
        /// </summary>
        [ImportMany]
        public IEnumerable<Lazy<DocumentContent, IPluginData>> ListePlugins
        {
            get;
            set;
        }

        /// <summary>
        /// Cinch : INPC Helper
        /// </summary>
        private static System.ComponentModel.PropertyChangedEventArgs cataloguePluginsChangeArgs = Utils.Observable.ObservableHelper.CreateArgs<MainViewModel>(x => x.CataloguePlugins);

        /// <summary>
        /// Get et set des plgins
        /// </summary>
        public CompositionContainer CataloguePlugins
        {
            get
            {
                return cataloguePlugins;
            }
            set
            {
                this.cataloguePlugins = value;
                NotifyPropertyChanged(cataloguePluginsChangeArgs);
            }
        }

        /// <summary>
        /// Cinch : INPC Helper
        /// </summary>
        private static System.ComponentModel.PropertyChangedEventArgs listeDocumentsChangeArgs = Utils.Observable.ObservableHelper.CreateArgs<MainViewModel>(x => x.ListeDocuments);

        /// <summary>
        /// Get et set des plgins
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
        public ComposablePartDefinition PluginActif
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

        #region RecupererPlugins

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

        #endregion

        #region SupprimerPlugin

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
                            SupprimerPlugin(PluginActif.ToString());
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

        #endregion

        #region ChargerPlugin

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
                            ChargerPluginActif(PluginActif.ToString());
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

        #endregion

        #region Fermer

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
                if (plugin.Value != null)
                {
                    if (plugin.Metadata.Title.Equals(nomPlugin))
                    {
                        ListeDocuments.Add(plugin.Value);
                    }
                }
            }
        }

        /// <summary>
        /// Récupère les plugins présent dans le répertoire ..\..\Ressources\Plugins
        /// </summary>
        private void ChargerPluginsDisponibles()
        {
            try
            {
                var catalog = new AggregateCatalog();
                catalog.Catalogs.Add(new DirectoryCatalog(@"..\..\Ressources\Plugins"));
                CataloguePlugins = new CompositionContainer(catalog);
                CataloguePlugins.ComposeParts(this);
            }
            catch (CompositionException compositionException)
            {
                Console.WriteLine(compositionException.ToString());
            }
            catch (DirectoryNotFoundException)
            {
                MessageBox.Show("Le répertoire des plugins n'existe pas. Aucun plugin ne sera chargé", "Attention", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
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
                return ListeDocuments.Any(doc => doc.Title == pluginActif.ToString());
            }
        }

        #endregion
    }
}
