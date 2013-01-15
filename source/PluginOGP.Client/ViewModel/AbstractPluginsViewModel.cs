using Cinch;
using OGP.ServicePlugin.Modele;
using PluginOGP.Client.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PluginOGP.Client.ViewModel
{
    abstract class AbstractPluginsViewModel : ViewModelBase
    {
        private object accesLock = new object();
        
        private ObservableCollection<PluginContext> _pluginList;
        public ObservableCollection<PluginContext> PluginList
        {
            get
            {
                lock (accesLock)
                {
                    if (_pluginList == null)
                    {
                        _pluginList = new ObservableCollection<PluginContext>();
                    }
                }
                return _pluginList;
            }
        }
        
        abstract public void Refresh();
        
        protected AbstractPluginsViewModel()
        {
        }
    }
}
