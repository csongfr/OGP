using OGP.Plugin.Exception;
using OGP.Plugin.Interfaces;
using OGP.ServicePlugin;
using OGP.ServicePlugin.Modele;
using PluginOGP.Client.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Utils.Wcf;

namespace PluginOGP.Client.ViewModel
{
    class RemotePluginsViewModel : AbstractPluginsViewModel
    {
        #region Constructor
        public RemotePluginsViewModel()
        {
        }
        #endregion

        public void RetrieveList()
        {
            getListfromServer();
        }

        public override void Refresh()
        {
            getListfromServer();
        }

        private void getListfromServer()
        {
            IList<PluginModel> remote = null;

            var localPluginsInfo = ServiceProvider.Resolve<IPluginsInfo>();
            IEnumerable<PluginModel> local = localPluginsInfo.GetPluginsInfo();

            var background = new BackgroundWorker();
            background.DoWork += (DoWorkEventHandler)((sender, e) =>
                {
                    Exception error = WcfHelper.Execute<IServicePlugin>(client =>
                        {
                            remote = client.GetPluginList();
                        });

                    if (error != null)
                    {
                        throw new OgpPluginException("Erreur de recuperation de liste de plugins", error);
                    }
                });

            background.RunWorkerCompleted += (RunWorkerCompletedEventHandler)((sender, e) =>
                {
                    if (remote != null)
                    {
                        this.availablePluginList.Clear();
                        foreach (PluginModel plugin in remote)
                        {
                            PluginContext newContext = new PluginContext(plugin);
                            if (local.Contains<PluginModel>(plugin))
                            {
                                newContext.CanDownload = false;
                                newContext.ProgressBarStatus = System.Windows.Visibility.Visible;
                                newContext.Progress = 100.0;
                            }
                            else
                            {
                                newContext.CanDownload = true;
                            }
                            newContext.CanUninstall = false;
                            this.availablePluginList.Add(newContext);
                        }
                        showAvailablePlugins();
                        //Thread.Sleep(2000);
                        RibbonWindowViewModel.ServerDockInstance.DoSomethingWhenBackgroundEnd();
                    }
                });

            RibbonWindowViewModel.ServerDockInstance.DoSomethingWhenBackgroundBegin();
            background.RunWorkerAsync();
        }
    }
}
