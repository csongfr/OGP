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
using OGP.ClientWpf.ViewModel;

namespace OGP.ClientWpf.View
{
    /// <summary>
    /// Interaction logic for GestionDesTaches.xaml
    /// </summary>
    public partial class GestionDesTaches : RibbonTabItem
    {
        public GestionDesTaches()
        {
            InitializeComponent();
            DataContext =new GestionDesTachesViewModel();
        }
    }
}
