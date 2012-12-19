using Cinch;
using OGP.Plugin.Interfaces;
using OGP.ServicePlugin;
using PluginOGP.Client.View;
using QuantumBitDesigns.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;


namespace PluginOGP.Client.ViewModel
{
    class LocalPluginsViewModel : ViewModelBase
    {
        public IPluginsInfo LocalPluginsInformations { get; private set; }

        public LocalPluginsViewModel()
        {
            LocalPluginsInformations = ServiceProvider.Resolve<IPluginsInfo>();
        }
    }
}
