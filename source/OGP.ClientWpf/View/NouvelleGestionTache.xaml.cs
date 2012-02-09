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
using System.Windows.Shapes;
using OGP.ClientWpf.ViewModel;
using System.Windows.Controls.Primitives;

namespace OGP.ClientWpf.View
{
    /// <summary>
    /// Interaction logic for NouveauFichier.xaml
    /// </summary>
    public partial class NouvelleGestionTache : Window
    {
        public NouvelleGestionTache()
        {
            InitializeComponent();
            this.DataContext=new NouvelleGestionTacheViewModel();
        }
    }
}
