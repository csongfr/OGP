using OGP.Plugin.Exception;
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
            this.PluginList.Clear();
            getListfromServer();
        }

        private void getListfromServer()
        {
            IList<PluginModel> remote = null;

            var background = new BackgroundWorker();
            background.DoWork += (DoWorkEventHandler)((sender, e) =>
                {
                    Exception erreur = WcfHelper.Execute<IServicePlugin>(client =>
                        {
                            remote = client.GetPluginList();
                        });

                    if (erreur != null)
                    {
                        throw new OgpPluginException("", erreur);
                    }
                });

            background.RunWorkerCompleted += (RunWorkerCompletedEventHandler)((sender, e) =>
                {
                    if (remote != null)
                    {
                        foreach (PluginModel plugin in remote)
                        {
                            PluginContext newContext = new PluginContext(plugin);
                            newContext.CanDownload = true;
                            newContext.CanUnistall = false;
                            this.PluginList.Add(newContext);
                        }
                        //Thread.Sleep(2000);
                        RibbonWindowViewModel.ServerDockInstance.DoSomethingWhenBackgroundEnd();
                    }
                });

            RibbonWindowViewModel.ServerDockInstance.DoSomethingWhenBackgroundBegin();
            background.RunWorkerAsync();
        }
    }
}
