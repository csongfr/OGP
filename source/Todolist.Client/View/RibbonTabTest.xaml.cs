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
using Fluent;
using OGP.Plugin.Interfaces;
using System.ComponentModel.Composition;
using Todolist.ViewModel;

namespace Todolist.Client.View
{
    /// <summary>
    /// Interaction logic for RibbonTabTest.xaml
    /// </summary>
    [Export(typeof(IOgpMenu))]
    public partial class RibbonTabTest : RibbonTabItem, IOgpMenu
    {
        public RibbonTabTest()
        {
            InitializeComponent();
            this.DataContext = new MenuViewModel();
        }
    }
}
