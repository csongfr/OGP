using Cinch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PluginOGP.Client.ViewModel
{
    class PluginSummaryViewModel : ViewModelBase
    {
        #region private components

        /// <summary>
        /// Commande permet de telecharger un plugin
        /// </summary>
        private SimpleCommand downloadCommand;
        /// <summary>
        /// Commande permet de upload un plugin
        /// </summary>
        private SimpleCommand uploadCommand;
        /// <summary>
        /// Commande permet de desinstaller un plugin
        /// </summary>
        private SimpleCommand uninstallCommand;

        #endregion

        #region properties

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
        /// Commande permet de upload un plugin
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
        

        #region Actions

        private void download()
        {
        }

        private void upload()
        {
        }

        private void uninstall()
        {
        }

        #endregion

        #region Constructeur
        public PluginSummaryViewModel()
        {
        }
        #endregion
    }
}
