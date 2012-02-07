using System;
using System.Collections.Generic;
using System.Linq;
using OGP.ClientWpf.Comands;
using System.Windows.Input;
using System.ComponentModel;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition;
using AvalonDock;
using System.IO;
using OGP.ClientWpf.View;
using System.Windows;
using Utils.Observable;
using System.Collections.ObjectModel;



namespace OGP.ClientWpf.ViewModel
{
    public class MainViewModel : ViewModelBase
    {                                 
        #region Membres privés

        /// <summary>
        /// Commande qui ferme l'application
        /// </summary>
        private RelayCommand exitCommand;

        /// <summary>
        /// Stock le catalogue des plugins
        /// </summary>
        private CompositionContainer _container;

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
                ChargerPlugins();
                NotifyPropertyChanged(pluginActifChangeArgs);

            }
        }

        #endregion

        #region Commands

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
                _container = new CompositionContainer(catalog);
                this._container.ComposeParts(this);
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


