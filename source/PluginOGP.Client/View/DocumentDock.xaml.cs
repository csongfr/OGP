using AvalonDock;
using PluginOGP.Client.ViewModel;
using System;
using System.Collections.Generic;
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
            this.DataContext = new DocumentDockViewModel();
        }
    }

    public class LocalDocumentDock : DocumentDock
    {
        public LocalDocumentDock(string title)
            : base(title)
        {
            // test
            for (int i = 0; i < 10; i++)
            {
                PluginSummary ps = new PluginSummary();
                ps.DownloadButton.IsEnabled = false;
                ps.UninstallButton.IsEnabled = true;
                this.pluginPanel.Children.Add(ps);
            }
        }
    }

    public class ServerDocumentDock : DocumentDock
    {
        public ServerDocumentDock(string title)
            : base(title)
        {
            // test
            for (int i = 0; i < 10; i++)
            {
                PluginSummary ps = new PluginSummary();
                ps.DownloadButton.IsEnabled = true;
                ps.UninstallButton.IsEnabled = false;
                this.pluginPanel.Children.Add(ps);
            }
        }
    }
}
