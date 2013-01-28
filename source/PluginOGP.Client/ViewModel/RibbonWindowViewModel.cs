using Cinch;
using OGP.Plugin.Exception;
using OGP.Plugin.Interfaces;
using OGP.ServicePlugin;
using OGP.ServicePlugin.Modele;
using PluginOGP.Client.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows;
using Utils.AssemblyInfoResolver;
using Utils.Wcf;

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

        

        public static DocumentDock LocalDockInstance { get; private set; }
        public static DocumentDock ServerDockInstance { get; private set; }

        #region private components
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

        /// <summary>
        /// Upload un plugin au serveur
        /// </summary>
        private SimpleCommand uploadCommand;

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

        /// <summary>
        /// Upload un plugin au serveur
        /// </summary>
        public SimpleCommand UploadCommand
        {
            get
            {
                if (uploadCommand == null)
                {
                    uploadCommand = new SimpleCommand
                    {
                        ExecuteDelegate = delegate
                        {
                            upload();
                        }
                    };
                }
                return uploadCommand;
            }
        }

        #endregion
        

        #region Actions

        private void openLocal()
        {
            if (LocalDockInstance == null)
            {
                var addDoc = ServiceProvider.Resolve<ICentralOnglets>();
                var doc = new LocalDocumentDock(LOCAL_DOCK_TITLE);
                addDoc.AjoutOnglet(doc);
                LocalDockInstance = doc;
            }
            else
            {
                var localModel = (LocalPluginsViewModel)LocalDockInstance.ItemControler.DataContext;
                localModel.Refresh();
            }
            LocalDockInstance.Activate();
        }

        private void openServer()
        {
            if (ServerDockInstance == null)
            {
                var addDoc = ServiceProvider.Resolve<ICentralOnglets>();
                var doc = new ServerDocumentDock(SERVER_DOCK_TITLE);
                addDoc.AjoutOnglet(doc);
                ServerDockInstance = doc;
                // séparer construction et longue operation
                var remoteModel = (RemotePluginsViewModel)doc.ItemControler.DataContext;
                remoteModel.RetrieveList();
            }
            else
            {
                var remoteModel = (RemotePluginsViewModel)ServerDockInstance.ItemControler.DataContext;
                remoteModel.Refresh();
            }
            ServerDockInstance.Activate();
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
            bool? result = dlg.ShowDialog();

            // Process open file dialog box results
            if (result == true)
            {
                // Open document
                string filepath = dlg.FileName;
                string filename = Path.GetFileName(filepath);
                string dossier = ServiceProvider.Resolve<IPluginsInfo>().GetPluginsDirectory(DirectoryType.Local);
                string dstFilepath = dossier + Path.DirectorySeparatorChar + filename;
                File.Copy(filepath, dstFilepath, true);
                //try
                //{
                //    File.Copy(filepath, dstFilepath, true);
                //}
                //catch (Exception ex)
                //{
                //     gestion
                //}
                new WPFMessageBoxService().ShowInformation("Plugin imported.");
            }
        }

        private void upload()
        {
            // Configure open file dialog box
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.Title = "Choose the plugin to upload";
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
                string dstFilepath  = Path.GetFullPath(filepath);

                // retrieve assembly info
                AssemblyInfoHelper asm = new AssemblyInfoHelper(dstFilepath); 
                PluginModel pm = new PluginModel();
                pm.Name = asm.Title;
                pm.Version = asm.AssemblyVersion;
                pm.Description = asm.Description;
                pm.Actif = true;
                // retrieve dll file
                MemoryStream ms = new MemoryStream(File.ReadAllBytes(dstFilepath));

                // run in background
                var background = new BackgroundWorker();
                background.DoWork += (DoWorkEventHandler)((sender, e) =>
                {
                    Exception error = WcfHelper.Execute<IServicePlugin>(client =>
                    {
                        result = client.AddPlugin(pm, ms);
                    });

                    if (error != null)
                    {
                        throw new OgpPluginException("Erreur de upload", error);
                    }
                });

                background.RunWorkerCompleted += (RunWorkerCompletedEventHandler)((sender, e) =>
                {
                    new WPFMessageBoxService().ShowInformation("Plugin uploaded.");
                });
                background.RunWorkerAsync();
            }
        }

        private void backgroundUpdate()
        {
            // recuperer liste de plugins locaux
            var localPluginsInfo = ServiceProvider.Resolve<IPluginsInfo>();
            IEnumerable<PluginModel> localAndDownload = localPluginsInfo.GetPluginsInfo();
            IEnumerable<PluginModel> download = localAndDownload.Where(p =>
                {
                    string tmpDirectory = ServiceProvider.Resolve<IPluginsInfo>().GetPluginsDirectory(DirectoryType.Tmp);
                    string tmpDirectoryStandard = new DirectoryInfo(tmpDirectory).FullName.TrimEnd('\\').ToUpper();

                    string pDirectory = Path.GetDirectoryName(p.Location);
                    string pDirectoryStandard = new DirectoryInfo(pDirectory).FullName.TrimEnd('\\').ToUpper();

                    return tmpDirectoryStandard == pDirectoryStandard;
                }
            );

            IList<PluginModel> toUpload = null;
            // run in background
            var background = new BackgroundWorker();
            background.DoWork += (DoWorkEventHandler)((sender, e) =>
            {
                // recuperer liste de plugins à mettre à jour
                Exception error = WcfHelper.Execute<IServicePlugin>(client =>
                {
                    toUpload = client.CheckNewVersion(new List<PluginModel>(download));
                });

                if (error != null)
                {
                    throw new OgpPluginException("Erreur de maj", error);
                }
            });

            background.RunWorkerCompleted += (RunWorkerCompletedEventHandler)((sender, e) =>
            {
                if (toUpload != null)
                {
                    foreach (PluginModel p in toUpload)
                    {
                        MemoryStream memo = null;
                        // télécharger plugin
                        Exception error = WcfHelper.Execute<IServicePlugin>(client =>
                        {
                            memo = client.DownloadPlugin(p.Id);
                        });

                        if (error != null)
                        {
                            throw new OgpPluginException("Erreur de telechargement", error);
                        }
                        // écrit le plugin dans répertoire 
                        string downloadDirectory = localPluginsInfo.GetPluginsDirectory(DirectoryType.Download);
                        string fileName =p.Name + ".dll";
                        string dstWriteTo = Path.Combine(downloadDirectory, fileName);
                        using (FileStream filePlugin = System.IO.File.Create(dstWriteTo))
                        {
                            memo.WriteTo(filePlugin);
                        }
                    }

                    MessageBox.Show("Update complete, they are available after next reboot.");
                    // log?
                    Console.WriteLine("============Mise à jour terminé!============");
                }
            });
            background.RunWorkerAsync();
        }


        #endregion

        #region Constructeur
        public RibbonWindowViewModel()
        {
        }
        #endregion


        internal void Initialize()
        {
            backgroundUpdate();
        }
    }

}
