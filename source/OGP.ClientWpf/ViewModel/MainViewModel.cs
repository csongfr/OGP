using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using AvalonDock;
using Fluent;
using Microsoft.Win32;
using OGP.ClientWpf.Comands;
using OGP.ClientWpf.View;
using Utils.Observable;

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
        private RelayCommand exitCommand;

        /// <summary>
        /// Commande ajoute un plugin
        /// </summary>
        private RelayCommand ajouterPlugin;

        /// <summary>
        /// Commande qui supprime un plugin
        /// </summary>
        private RelayCommand supprimePlugin;

        /// <summary>
        /// Stock le catalogue des plugins
        /// </summary>
        private CompositionContainer container;

        /// <summary>
        /// Stock le plugin actif.
        /// </summary>
        private string pluginActif;

        /// <summary>
        /// Stock la liste des plugins actifs.
        /// </summary>
        private ObservableCollection<DocumentContent> listeDocuments;

        /// <summary>
        /// Stock la listes de tous les plugins
        /// </summary>
        [ImportMany]
        private IEnumerable<Lazy<DocumentContent, IDocumentData>> Plugin
        {
            get;
            set;
        }

        #endregion

        #region listes plugins

        /// <summary>
        /// Stock le titre des plugins
        /// </summary>
        public ObservableCollection<string> ListesPlugins
        {
            get;
            set;
        }
        #endregion

        #region Membres public

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
        public string PluginActif
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
                    supprimePlugin = new RelayCommand(SupprimerPlugin);
                }
                return supprimePlugin;
            }
        }

        #endregion

        #region AjouterPlugin

        /// <summary>
        /// Ajoute un plugin
        /// </summary>
        public ICommand AjouterPlugin
        {
            get
            {
                if (ajouterPlugin == null)
                {
                    ajouterPlugin = new RelayCommand(ChargerPlugin);
                }
                return ajouterPlugin;
            }
        }

        #endregion

        #region Exit

        /// <summary>
        /// Exit from the application
        /// </summary>
        public ICommand ExitCommand
        {
            get
            {
                if (exitCommand == null)
                {
                    exitCommand = new RelayCommand(x => System.Windows.Application.Current.Shutdown());
                }
                return exitCommand;
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
            this.ListesPlugins = new ObservableCollection<string>();
            ChargerPlugins();
            Listeplugins();
        }

        #endregion

        #region Méthodes privées

        /// <summary>
        /// Supprime le plugin de la fenêtre
        /// </summary>
        /// <param name="param">object</param>
        private void SupprimerPlugin(object param)
        {
            ListeDocuments.Clear();         
        }

        /// <summary>
        /// appelle la fonction ChargerPlugins lors du click sur ajouter plugin
        /// </summary>
        /// <param name="param">object</param>
        private void ChargerPlugin(object param)
        {
            ChargerPlugins();
        }

        /// <summary>
        /// Stock les titres des plugins dans la liste 
        /// </summary>
        private void Listeplugins()
        {
            foreach (var plugin in this.Plugin)
            {
                if (plugin.Value != null)
                {
                    this.ListesPlugins.Add(plugin.Metadata.Title);
                }
            }
        }

        /// <summary>
        /// récupère les plugins présent dans le répertoire ..\..\Ressources\Plugins
        /// </summary>
        private void ChargerPlugins()
        {
            try
            {
                var catalog = new AggregateCatalog();
                catalog.Catalogs.Add(new DirectoryCatalog(@"..\..\Ressources\Plugins"));
                container = new CompositionContainer(catalog);
                this.container.ComposeParts(this);
                foreach (var plugin in this.Plugin)
                {
                    if (plugin.Value != null)
                    {
                        if (plugin.Metadata.Title.Equals(pluginActif))
                        {
                          ListeDocuments.Add(plugin.Value);
                        }
                    }
                }
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

        #endregion
    }
}
