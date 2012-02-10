using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
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
using AvalonDock;
using Fluent;
using OGP.ClientWpf.Extensions.ViewModel;

namespace OGP.ClientWpf.Extensions.View
{
    /// <summary>
    /// Interaction logic for GestionsTaches.xaml
    /// </summary>
    public partial class GestionsTaches : RibbonTabItem
    {
        /// <summary>
        /// Classe qui gère l'onglet Gestions Des Tâches
        /// </summary>
        /// 
        public GestionsTaches()
        {
            InitializeComponent();
            this.DataContext = new GestionsTachesViewModel();
        }
    }
}
