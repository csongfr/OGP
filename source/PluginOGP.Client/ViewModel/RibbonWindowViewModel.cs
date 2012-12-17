using System;
using Cinch;
using OGP.ClientWpf;
using OGP.Plugin.Interfaces;
using PluginOGP.Client.View;
using System.IO;

namespace PluginOGP.Client.ViewModel
{
      
    class RibbonWindowViewModel : ViewModelBase
    {
        #region constant names
        private const string LOCAL_DOCK_TITLE = "Installed plugins";
        private const string SERVER_DOCK_TITLE = "Available plugins in server";
        private const string PLUGIN_EXTENSION = ".dll";
        private const string PLUGIN_TYPE_DESCRIPTION = "Dynamic Link Library  (.dll)|*.dll";
        #endregion

        #region private components

        private DocumentDock localDockInstance;
        private DocumentDock serverDockInstance;

        /// <summary>
        /// Commande qui affiche les plugins locaux
        /// </summary>
        private SimpleCommand openLocalCommand;
        /// <summary>
        /// Commande qui affiche les plugins disponibles sur serveur
        /// </summary>
        private SimpleCommand openServerCommand;
        /// <summary>
        /// Commande qui permet de s'identifier
        /// </summary>
        private SimpleCommand loginCommand;
        /// <summary>
        /// Commande qui permet d'importer un plugin
        /// </summary>
        private SimpleCommand importPluginCommand;

        #endregion

        #region properties

        /// <summary>
        /// Commande qui ouvre la liste de plugins installes
        /// </summary>
        public SimpleCommand OpenLocalCommand
        {
            get
            {
                if (openLocalCommand == null)
                {
                    openLocalCommand = new SimpleCommand
                    {
                        ExecuteDelegate = delegate
                        {
                            openLocal();
                        }
                    };
                }
                return openLocalCommand;
            }
        }

        /// <summary>
        /// Commande qui affiche les plugins disponible a distance
        /// </summary>
        public SimpleCommand OpenServerCommand
        {
            get
            {
                if (openServerCommand == null)
                {
                    openServerCommand = new SimpleCommand
                    {
                        ExecuteDelegate = delegate
                        {
                            openServer();
                        }
                    };
                }
                return openServerCommand;
            }
        }

        /// <summary>
        /// Commande qui permet de s'identifier
        /// </summary>
        public SimpleCommand LoginCommand
        {
            get
            {
                if (loginCommand == null)
                {
                    loginCommand = new SimpleCommand
                    {
                        ExecuteDelegate = delegate
                        {
                            signIn();
                        }
                    };
                }
                return loginCommand;
            }
        }

        /// <summary>
        /// Commande qui permet d'importer un plugin
        /// </summary>
        public SimpleCommand ImportPluginCommand
        {
            get
            {
                if (importPluginCommand == null)
                {
                    importPluginCommand = new SimpleCommand
                    {
                        ExecuteDelegate = delegate
                        {
                            import();
                        }
                    };
                }
                return importPluginCommand;
            }
        }

        #endregion
        

        #region Actions

        private void openLocal()
        {
            if (localDockInstance == null)
            {
                var addDoc = ServiceProvider.Resolve<ICentralOnglets>();
                var doc = new LocalDocumentDock(LOCAL_DOCK_TITLE);
                addDoc.AjoutOnglet(doc);
                localDockInstance = doc;
            }
            localDockInstance.Activate();
        }

        private void openServer()
        {
            if (serverDockInstance == null)
            {
                var addDoc = ServiceProvider.Resolve<ICentralOnglets>();
                var doc = new ServerDocumentDock(SERVER_DOCK_TITLE);
                addDoc.AjoutOnglet(doc);
                serverDockInstance = doc;
            }
            serverDockInstance.Activate();
        }

        private void signIn()
        {
        }

        private void import()
        {
            // Configure open file dialog box
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.FileName = "plugin" + PLUGIN_EXTENSION; // Default file name 
            dlg.DefaultExt = PLUGIN_EXTENSION; // Default file extension 
            dlg.Filter = PLUGIN_TYPE_DESCRIPTION; // Filter files by extension 

            // Show open file dialog box
            Nullable<bool> result = dlg.ShowDialog();

            // Process open file dialog box results
            if (result == true)
            {
                // Open document
                string filepath = dlg.FileName;
                string filename = Path.GetFileName(filepath);
                string dstFilepath = AppConfig.Instance.RepertoirePluginsSynchro + Path.DirectorySeparatorChar + filename;
                File.Copy(filepath, dstFilepath, true);
                new WPFMessageBoxService().ShowInformation("Plugin imported.");
            }
        }

        #endregion

        #region Constructeur
        public RibbonWindowViewModel()
        {
        }
        #endregion

    }

}
