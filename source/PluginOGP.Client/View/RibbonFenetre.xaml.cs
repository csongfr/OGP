using Fluent;
using OGP.Plugin.Interfaces;
using PluginOGP.Client.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel.Composition;

namespace PluginOGP.Client.View
{
    /// <summary>
    /// Interaction logic for FenetrePrincipale.xaml
    /// </summary>
    [Export(typeof(IOgpMenu))]
    public partial class RibbonFenetre : RibbonTabItem, IOgpMenu
    {
        public RibbonFenetre()
        {
            InitializeComponent();
            this.DataContext = new RibbonFenetre();
        }
    }
}
