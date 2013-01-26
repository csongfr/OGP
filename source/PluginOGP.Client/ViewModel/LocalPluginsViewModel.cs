using Cinch;
using OGP.Plugin.Interfaces;
using OGP.ServicePlugin;
using OGP.ServicePlugin.Modele;
using PluginOGP.Client.View;
using QuantumBitDesigns.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;


namespace PluginOGP.Client.ViewModel
{
    class LocalPluginsViewModel : AbstractPluginsViewModel
    {
        #region Constructor
        public LocalPluginsViewModel()
        {
            getListFromLocal();
        }
        #endregion

        public override void Refresh()
        {
            getListFromLocal();
        }

        /// <summary>
        /// retrive plugins list from local memory
        /// </summary>
        private void getListFromLocal()
        {
            var localPluginsInfo = ServiceProvider.Resolve<IPluginsInfo>();
            localPluginsInfo.RefreshMenu();
            IEnumerable<PluginModel> local = localPluginsInfo.GetPluginsInfo();

            this.availablePluginList.Clear();
            foreach (PluginModel plugin in local)
            {
                PluginContext newContext = new PluginContext(plugin);
                newContext.CanDownload = false;
                newContext.CanUninstall = true;
                lock (accesLock)
                {
                    this.availablePluginList.Add(newContext);
                }
            }

            showAvailablePlugins();
        }
    }
}
