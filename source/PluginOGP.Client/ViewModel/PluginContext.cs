using Cinch;
using OGP.Plugin.Exception;
using OGP.Plugin.Interfaces;
using OGP.ServicePlugin;
using OGP.ServicePlugin.Modele;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Utils.Wcf;

namespace PluginOGP.Client.ViewModel
{
    class PluginContext : ViewModelBase
    {
        public PluginModel RawData { get; private set; }

        private bool canDownload;
        private bool canUninstall;
        private Visibility progressBarStatus;
        private double progress;

        private object streamWriteLock = new object();

        #region ObservableProperties
        private static System.ComponentModel.PropertyChangedEventArgs canDownloadChangeArgs = Utils.Mvvm.ObservableHelper.CreateArgs<PluginContext>(x => x.CanDownload);
        public bool CanDownload
        {
            get
            {
                return this.canDownload;
            }
            set
            {
                if (this.canDownload == value)
                {
                    return;
                }

                this.canDownload = value;

                NotifyPropertyChanged(canDownloadChangeArgs);
            }
        }

        private static System.ComponentModel.PropertyChangedEventArgs canUninstallChangeArgs = Utils.Mvvm.ObservableHelper.CreateArgs<PluginContext>(x => x.CanUninstall);
        public bool CanUninstall
        {
            get
            {
                return this.canUninstall;
            }
            set
            {
                if (this.canUninstall == value)
                {
                    return;
                }

                this.canUninstall = value;

                NotifyPropertyChanged(canUninstallChangeArgs);
            }
        }

        private static System.ComponentModel.PropertyChangedEventArgs progressBarStatusChangeArgs = Utils.Mvvm.ObservableHelper.CreateArgs<PluginContext>(x => x.ProgressBarStatus);
        public Visibility ProgressBarStatus
        {
            get
            {
                return progressBarStatus;
            }
            set
            {
                if (this.progressBarStatus == value)
                {
                    return;
                }

                this.progressBarStatus = value;
                NotifyPropertyChanged(progressBarStatusChangeArgs);
            }
        }

        private static System.ComponentModel.PropertyChangedEventArgs progressChangeArgs = Utils.Mvvm.ObservableHelper.CreateArgs<PluginContext>(x => x.Progress);
        public double Progress
        {
            get
            {
                return progress;
            }
            set
            {
                if (progress == value)
                {
                    return;
                }
                else
                {
                    progress = value;
                    if (progress > 100.0)
                    {
                        progress = 100.0;
                    }
                    NotifyPropertyChanged(progressChangeArgs);
                }
            }
        }
        #endregion

        #region Private commands

        /// <summary>
        /// Commande permet de telecharger un plugin
        /// </summary>
        private SimpleCommand downloadCommand;

        /// <summary>
        /// Commande permet de desinstaller un plugin
        /// </summary>
        private SimpleCommand uninstallCommand;

        #endregion

        #region Command properties

        /// <summary>
        /// Commande permet de telecharger un plugin
        /// </summary>
        public SimpleCommand DownloadCommand
        {
            get
            {
                if (downloadCommand == null)
                {
                    downloadCommand = new SimpleCommand
                    {
                        ExecuteDelegate = delegate
                        {
                            download();
                        }
                    };
                }
                return downloadCommand;
            }
        }

        /// <summary>
        /// Commande permet de desinstaller un plugin
        /// </summary>
        public SimpleCommand UninstallCommand
        {
            get
            {
                if (uninstallCommand == null)
                {
                    uninstallCommand = new SimpleCommand
                    {
                        ExecuteDelegate = delegate
                        {
                            uninstall();
                        }
                    };
                }
                return uninstallCommand;
            }
        }

        #endregion


        #region Operations

        private void download()
        {
            MemoryStream memo = null;

            var background = new BackgroundWorker();
            background.DoWork += (DoWorkEventHandler)((sender, e) =>
            {
                Exception error = WcfHelper.Execute<IServicePlugin>(client =>
                {
                    memo = client.DownloadPlugin(RawData.Id);
                });

                if (error != null)
                {
                    throw new OgpPluginException("Erreur de telechargement", error);
                }
            });

            background.RunWorkerCompleted += (RunWorkerCompletedEventHandler)((sender, e) =>
            {
                if (memo != null)
                {
                    var localPluginsInfo = ServiceProvider.Resolve<IPluginsInfo>();
                    string dossier = localPluginsInfo.GetPluginsDossier(DossierType.Download);
                    string filePath = dossier + 
                        Path.DirectorySeparatorChar + RawData.Name + "_" + RawData.Version + ".dll";
                    lock (streamWriteLock)
                    {
                        using (FileStream filePlugin = System.IO.File.Create(filePath))
                        {
                            memo.WriteTo(filePlugin);
                        }
                    }
                    this.Progress = 100.0;
                }
            });

            background.RunWorkerAsync();
            this.ProgressBarStatus = Visibility.Visible;
            this.CanDownload = false;
        }

        private void uninstall()
        {
        }

        #endregion

        #region Constructeur
        public PluginContext(PluginModel plugin)
        {
            RawData = plugin;
            ProgressBarStatus = Visibility.Hidden;
        }
        #endregion
    }
}
