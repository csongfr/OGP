using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Configuration;
using System.IO;
using System.Linq;
using AvalonDock;
using Cinch;
using OGP.Plugin.Interfaces;
using QuantumBitDesigns.Core;
using Fluent;
using System.Collections.Generic;
using Utils.AssemblyInfoResolver;
using OGP.ServicePlugin.Modele;
using OGP.ServicePlugin;
using Utils.Wcf;

namespace OGP.ClientWpf.ViewModel
{
    /// <summary>
    /// Fenêtre principale
    /// </summary>
    public class MainViewModel : ViewModelBase, ICentralOnglets, IPluginsInfo, IMenuOperation
    {
        #region Membres privés

        /// <summary>
        /// Commande qui ferme l'application
        /// </summary>
        private SimpleCommand fermerCommand;

        /// <summary>
        /// Stocke la commande chargeant les plugins disponibles
        /// </summary>
        private SimpleCommand recupererCommand;

        /// <summary>
        /// Stock la liste des plugins actifs.
        /// </summary>
        private ObservableList<DocumentContent> listeDocuments;

        /// <summary>
        /// Stocke la liste des menus chargés.
        /// </summary>
        private ObservableList<IOgpMenu> listeMenu;

        private RibbonTabItem monRibbon;

        #endregion

        #region Membres publics

        /// <summary>
        /// Cinch : INPC helper.
        /// </summary>
        private static System.ComponentModel.PropertyChangedEventArgs listeMenuChangeArgs = Utils.Mvvm.ObservableHelper.CreateArgs<MainViewModel>(x => x.ListeMenu);

        /// <summary>
        /// Gets ou sets la liste des menus d'OGP.
        /// </summary>
        [ImportMany(typeof(IOgpMenu))]
        public ObservableList<IOgpMenu> ListeMenu
        {
            get
            {
                return this.listeMenu;
            }
            set
            {
                if (this.listeMenu == value)
                {
                    return;
                }

                this.listeMenu = value;

                this.NotifyPropertyChanged(listeMenuChangeArgs);
            }
        }

        /// <summary>
        /// Cinch : INPC Helper
        /// </summary>
        private static System.ComponentModel.PropertyChangedEventArgs listeDocumentsChangeArgs = Utils.Mvvm.ObservableHelper.CreateArgs<MainViewModel>(x => x.ListeDocuments);

        /// <summary>
        /// Get et set des plugins
        /// </summary>
        public ObservableList<DocumentContent> ListeDocuments
        {
            get
            {
                return this.listeDocuments;
            }
            set
            {
                this.listeDocuments = value;
                this.NotifyPropertyChanged(listeDocumentsChangeArgs);
            }
        }

        /// <summary>
        /// Cinch : INPC helper.
        /// </summary>
        private static System.ComponentModel.PropertyChangedEventArgs monRibbonChangeArgs = Utils.Mvvm.ObservableHelper.CreateArgs<MainViewModel>(x => x.MonRibbon);

        public RibbonTabItem MonRibbon
        {
            get
            {
                return this.monRibbon;
            }
            set
            {
                if (this.monRibbon == value)
                {
                    return;
                }

                this.monRibbon = value;

                NotifyPropertyChanged(monRibbonChangeArgs);
            }
        }

        #endregion

        #region Commandes

        /// <summary>
        /// Supprime un plugin
        /// </summary>
        public SimpleCommand RecupererPlugins
        {
            get
            {
                if (this.recupererCommand == null)
                {
                    this.recupererCommand = new SimpleCommand
                    {
                        ExecuteDelegate = delegate
                        {
                            this.ChargerPluginsDisponibles();
                        }
                    };
                }
                return this.recupererCommand;
            }
        }

        /// <summary>
        /// Exit from the application
        /// </summary>
        public SimpleCommand FermerCommand
        {
            get
            {
                if (this.fermerCommand == null)
                {
                    this.fermerCommand = new SimpleCommand
                    {
                        ExecuteDelegate = delegate
                        {
                            System.Windows.Application.Current.Shutdown();
                        }
                    };
                }
                return this.fermerCommand;
            }
        }

        #endregion

        #region Méthodes publiques

        /// <summary>
        /// Ajoute la plugin à la liste
        /// </summary>
        /// <param name="doc">Le document à ajouter</param>
        public void AjoutOnglet(DocumentContent doc)
        {
            this.ListeDocuments.Add(doc);
        }

        public void RefreshMenu()
        {
            int c = ListeMenu.Count;
            for (int i = 0; i < c; i++)
            {
                ListeMenu.RemoveAt(0);
            }
            ChargerPluginsDisponibles();
            Console.WriteLine("================================================================" + ListeMenu.Count);
        }

        public IEnumerable<PluginModel> GetPluginsInfo()
        {
            return this.ListeMenu.Select<IOgpMenu, PluginModel>(menu =>
            {
                var aih = new AssemblyInfoHelper(menu.GetType());
                PluginModel plugin = new PluginModel();
                plugin.Name = aih.Title;
                plugin.Description = aih.Description;
                return plugin;
            });
        }

        #endregion

        #region Méthodes privées

        /// <summary>
        /// Récupère les plugins présents dans le répertoire défini dans le fichier de config du client
        /// </summary>
        private void ChargerPluginsDisponibles()
        {
            try
            {
                string repertoireSynchro = AppConfig.Instance.RepertoirePluginsSynchro;
                string repertoireLocal = AppConfig.Instance.RepertoirePluginsLocal;

                var catalog = new AggregateCatalog();

                catalog.Catalogs.Add(new DirectoryCatalog(repertoireLocal));
                catalog.Catalogs.Add(new DirectoryCatalog(repertoireSynchro));
                CompositionContainer cataloguePlugins = new CompositionContainer(catalog);
                cataloguePlugins.ComposeParts(this);
            }
            catch (CompositionException compositionException)
            {
                Console.WriteLine(compositionException.ToString());
            }
            catch (DirectoryNotFoundException ex)
            {
                throw new OgpClientCoreException("Le répertoire des plugins n'existe pas. Aucun plugin ne sera chargé", ex);
            }
        }

        #endregion

        #region Constructeur

        /// <summary>
        /// Default constructor
        /// </summary>
        public MainViewModel()
        {
            this.ListeDocuments = new ObservableList<DocumentContent>();
            ServiceProvider.Add(typeof(ICentralOnglets), this);
            ServiceProvider.Add(typeof(IPluginsInfo), this);
            ServiceProvider.Add(typeof(IMenuOperation), this);
            this.ChargerPluginsDisponibles();
        }

        #endregion
    }
}
