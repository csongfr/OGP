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
using OGP.Plugin.Interfaces;
using Fluent;

namespace PluginTest2
{
    /// <summary>
    /// Interaction logic for RibbonTest2.xaml
    /// </summary>
    [Export(typeof(IOgpMenu))]
    public partial class RibbonTest2 :  RibbonTabItem, IOgpMenu
    {
        public RibbonTest2()
        {
            InitializeComponent();
            DataContext = new RibbonTest2ViewModel();
        }
    }
}
