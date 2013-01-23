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
        public readonly object accesLock = new object();

        protected Collection<PluginContext> availablePluginList;

        private ObservableCollection<PluginContext> _displayedPluginList;
        public ObservableCollection<PluginContext> DisplayedPluginList
        {
            get
            {
                if (_displayedPluginList == null)
                {
                    _displayedPluginList = new ObservableCollection<PluginContext>();
                }
                return _displayedPluginList;
            }
        }
        
        abstract public void Refresh();

        protected void showAvailablePlugins()
        {
            DisplayedPluginList.Clear();
            foreach (PluginContext plugin in availablePluginList)
            {
                DisplayedPluginList.Add(plugin);
            }
        }

        #region Command
        private SimpleCommand searchCommand;

        public SimpleCommand SearchCommand
        {
            get
            {
                if (searchCommand == null)
                {
                    searchCommand = new SimpleCommand
                    {
                        ExecuteDelegate = delegate(object param)
                        {
                            search(param.ToString());
                        }
                    };
                }
                return searchCommand;
            }
        }

        private void search(string txt)
        {
            if (txt == null || txt == "")
            {
                showAvailablePlugins();
                return;
            }
            var result = availablePluginList.Where(aPluginContext => aPluginContext.RawData.Name.Contains(txt));
            DisplayedPluginList.Clear();
            foreach (PluginContext pc in result)
            {
                DisplayedPluginList.Add(pc);
            }
        }
        #endregion

        protected AbstractPluginsViewModel()
        {
            availablePluginList = new Collection<PluginContext>();
        }
    }
}
