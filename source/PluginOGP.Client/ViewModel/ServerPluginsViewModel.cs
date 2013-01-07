using OGP.ServicePlugin;
using OGP.ServicePlugin.Modele;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils.Wcf;

namespace PluginOGP.Client.ViewModel
{
    class ServerPluginsViewModel
    {
        public IList<PluginModel> Plugins { get; private set; }

        public ServerPluginsViewModel()
        {
            //var background = new BackgroundWorker();
            //background.DoWork += (DoWorkEventHandler)((sender, e) =>
            //    {
            //        Exception erreur = WcfHelper.Execute<IServicePlugin>(client =>
            //            {
            //                pluginList = client.GetPluginList();
            //            });

            //        if (erreur != null)
            //        {
            //            throw new OgpPluginException("", erreur);
            //        }
            //    });

            //background.RunWorkerCompleted += (RunWorkerCompletedEventHandler)((sender, e) =>
            //    {
            //        if (pluginList != null)
            //        {
            //            foreach (PluginModel plugin in pluginList)
            //            {
            //                PluginSummary ps = new PluginSummary(plugin.Name, plugin.Description);
            //                ps.DownloadButton.IsEnabled = true;
            //                ps.UninstallButton.IsEnabled = false;
            //                this.pluginPanel.Children.Add(ps);
            //            }
            //        }
            //    });

            //background.RunWorkerAsync();

            var erreur = WcfHelper.Execute<IServicePlugin>(client =>
            {
                Plugins = client.GetPluginList();
            });
            //Plugins.Add(new PluginModel());
            //int s = Plugins.Count;
        }
    }
}
