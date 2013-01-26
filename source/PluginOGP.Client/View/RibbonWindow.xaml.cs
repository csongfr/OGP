using Fluent;
using OGP.Plugin.Interfaces;
using PluginOGP.Client.ViewModel;
using System.ComponentModel.Composition;

namespace PluginOGP.Client.View
{
    /// <summary>
    /// Interaction logic for FenetrePrincipale.xaml
    /// </summary>
    [Export(typeof(IOgpMenu))]
    public partial class RibbonWindow : RibbonTabItem, IOgpMenu
    {
        public RibbonWindow()
        {
            InitializeComponent();
            this.DataContext = new RibbonWindowViewModel();
        }
    }
}
