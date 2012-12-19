using AvalonDock;
using Cinch;
using OGP.Plugin.Exception;
using OGP.Plugin.Interfaces;
using OGP.ServicePlugin;
using OGP.ServicePlugin.Modele;
using PluginOGP.Client.ViewModel;
using QuantumBitDesigns.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Utils.Wcf;

namespace PluginOGP.Client.View
{
    /// <summary>
    /// Interaction logic for FenetrePrincipale.xaml
    /// </summary>
    public partial class DocumentDock : DocumentContent
    {
        public DocumentDock(string title)
        {
            InitializeComponent();
            this.Title = title;
        }
    }

    public class LocalDocumentDock : DocumentDock
    {
        public LocalDocumentDock(string title)
            : base(title)
        {
            this.DataContext = new LocalPluginsViewModel();
            IPluginsInfo pi = ((LocalPluginsViewModel)this.DataContext).LocalPluginsInformations;
            foreach (PluginModel plugin in pi.GetPluginsInfo())
            {
                PluginSummary ps = new PluginSummary(plugin.Name, plugin.Description);
                ps.DownloadButton.IsEnabled = false;
                this.pluginPanel.Children.Add(ps);
            }
        }
    }

    public class ServerDocumentDock : DocumentDock
    {
        public ServerDocumentDock(string title)
            : base(title)
        {
            this.DataContext = new ServerPluginsViewModel();

            IList<PluginModel> pluginList = ((ServerPluginsViewModel)(this.DataContext)).Plugins;
            foreach (PluginModel plugin in pluginList)
            {
                PluginSummary ps = new PluginSummary(plugin.Name, plugin.Description);
                ps.UploadButton.IsEnabled = false;
                ps.UninstallButton.IsEnabled = false;
                this.pluginPanel.Children.Add(ps);
            }
        }
    }

}
