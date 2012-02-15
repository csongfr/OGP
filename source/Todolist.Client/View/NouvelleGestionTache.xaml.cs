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
using Plugin.Todolist.ViewModel;

namespace Plugin.Todolist.View
{
    /// <summary>
    /// Interaction logic for NouvelleGestionTache.xaml
    /// </summary>
    public partial class NouvelleGestionTache : Window
    {
        /// <summary>
        /// Gets et sets pour accéder à la NouvelleGestionTacheViewModel 
        /// </summary>
        public NouvelleGestionTacheViewModel Vm
        {
            get;
            set;
        }
        
        /// <summary>
        /// Constructeur de popup
        /// </summary>
        public NouvelleGestionTache()
        {
            InitializeComponent();
        }

        /// <summary>
        /// bof
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">RoutedEventArgs</param>
        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Fonction qui est chargée à l'ouverture de la fenêtre
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">RoutedEventArgs</param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Vm = new NouvelleGestionTacheViewModel();
            DataContext = Vm;
        }
    }
}
