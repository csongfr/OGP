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
using Todolist.ViewModel;

namespace Plugin.Todolist.View
{
    /// <summary>
    /// Interaction logic for NouvelleGestionTache.xaml
    /// </summary>
    public partial class NouvelleGestionTache : Window
    {        
        /// <summary>
        /// Constructeur de popup
        /// </summary>
        public NouvelleGestionTache()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.DataContext = new NouvelleGestionTacheViewModel();
        }
    }
}
