using Cinch;
using OGP.ServicePlugin.Modele;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PluginOGP.Client.ViewModel
{
    class PluginContext : ViewModelBase
    {
        public PluginModel RawData { get; private set; }
        public bool CanDownload { get; set; }
        public bool CanUnistall { get; set; }

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
        }

        private void uninstall()
        {
        }

        #endregion

        #region Constructeur
        public PluginContext(PluginModel plugin)
        {
            RawData = plugin;
        }
        #endregion
    }
}
