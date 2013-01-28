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
                            this.chargerPluginsDisponibles();
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
                            Console.WriteLine("********************************************jsdlfksjgl");
                            System.Windows.Application.Current.Shutdown();
                            Console.WriteLine("=====================================jsdlfksjgl");
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

        #region IPluginInfo members
        public void RefreshMenu()
        {
            //ListeMenu.Clear();

            int c = ListeMenu.Count;
            for (int i = 0; i < c; i++)
            {
                ListeMenu.RemoveAt(0);
            }
            chargerPluginsDisponibles();
        }

        public IEnumerable<PluginModel> GetPluginsInfo()
        {
            return this.ListeMenu.Select<IOgpMenu, PluginModel>(menu =>
            {
                var aih = new AssemblyInfoHelper(menu.GetType());
                PluginModel plugin = new PluginModel();
                plugin.Name = aih.Title;
                plugin.Description = aih.Description;
                plugin.Version = aih.AssemblyVersion;
                plugin.Location = aih.FilePath;

                return plugin;
            });
        }

        public string GetPluginsDirectory(DirectoryType dType)
        {
            switch (dType)
            {
                case DirectoryType.Local:
                    return AppConfig.Instance.LocalDirectory;
                    //break;

                case DirectoryType.Download:
                    return AppConfig.Instance.DownloadDirectory;
                    //break;

                case DirectoryType.Tmp:
                    return AppConfig.Instance.TmpDirectory;
                    //break;

                default:
                    return ".";
            }
        }

        #endregion


        #endregion

        #region Méthodes privées

        /// <summary>
        /// Récupère les plugins présents dans le répertoire défini dans le fichier de config du client
        /// </summary>
        private void chargerPluginsDisponibles()
        {
            try
            {
                string repertoireTmp = AppConfig.Instance.TmpDirectory;
                string repertoireLocal = AppConfig.Instance.LocalDirectory;

                var catalog = new AggregateCatalog();

                catalog.Catalogs.Add(new DirectoryCatalog(repertoireLocal));
                catalog.Catalogs.Add(new DirectoryCatalog(repertoireTmp));
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

        /// <summary>
        /// Nettoie le répertoire Tmp puis copie tous les plugins de répertoire Download vers Tmp
        /// </summary>
        private void updateTmp()
        {
            string downloadDirectory = AppConfig.Instance.DownloadDirectory;
            string tmpDirectory = AppConfig.Instance.TmpDirectory;
            if (!Directory.Exists(tmpDirectory))
            {
                Directory.CreateDirectory(tmpDirectory);
            }

            // clean up tmp directory
            Directory.GetFiles(tmpDirectory).ToList().ForEach(File.Delete);

            // copy dll files from Download to Tmp
            string[] files = Directory.GetFiles(downloadDirectory, "*.dll");
            foreach (string f in files)
            {
                string fileName = Path.GetFileName(f);
                string dst = Path.Combine(tmpDirectory, fileName);
                File.Copy(f, dst);
            }
        }

        #endregion

        #region Constructeur

        /// <summary>
        /// Default constructor
        /// </summary>
        public MainViewModel()
        {
            updateTmp();
            this.ListeDocuments = new ObservableList<DocumentContent>();
            ServiceProvider.Add(typeof(ICentralOnglets), this);
            ServiceProvider.Add(typeof(IPluginsInfo), this);
            ServiceProvider.Add(typeof(IMenuOperation), this);
            this.chargerPluginsDisponibles();
            
            // Initialisation des plugins
            foreach (IOgpMenu menu in ListeMenu)
            {
                menu.Initialize();
            }
        }

        #endregion
    }
}
