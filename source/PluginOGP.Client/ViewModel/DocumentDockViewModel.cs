using Cinch;
using OGP.Plugin.Interfaces;
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
    class DocumentDockViewModel : ViewModelBase
    {
        public IPluginsInfo PluginsInformations { get; private set; }

        public DocumentDockViewModel()
        {
            PluginsInformations = ServiceProvider.Resolve<IPluginsInfo>();
        }
    }
}
