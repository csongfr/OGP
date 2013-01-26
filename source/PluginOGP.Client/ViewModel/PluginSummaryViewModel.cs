using Cinch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OGP.ServicePlugin.Modele;
using System.IO;
using Utils.Wcf;
using OGP.ServicePlugin;
using Utils;

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
            BackgroundTask.Start(() =>
            {
                // test à modifier
                Boolean b = true;
                var erreur = WcfHelper.Execute<IServicePlugin>(client =>
                {
                    PluginModel p = new PluginModel("id", "Test3", "7.1.3.5", "Ceci est un test", "/test", true);
                    IList<String> l = new List<String>();
                    l.Add("totovaalaplage");
                    File.WriteAllLines("testDownload", l);
                    MemoryStream initStream = new MemoryStream(File.ReadAllBytes("testDownload"));
                    client.AddPlugin(p, initStream);

                    MemoryStream stream = client.DownloadPlugin("Test3_7.1.3.5");
                    System.Diagnostics.Debug.Assert(initStream.Length == stream.Length, "Erreur dans le test1 addPlugin");
                    int count = 0;
                    while (count < stream.Length)
                    {
                        System.Diagnostics.Debug.Assert(initStream.ReadByte() == stream.ReadByte(), "Erreur dans le test1 addPlugin");
                        count++;
                    }
                });
            }, EnumBackgroundExceptionHandling.Throw);
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
