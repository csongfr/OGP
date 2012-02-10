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
using OGP.ValueObjects;
using Utils.Wcf;

namespace Test
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
             var exception = WcfHelper.Execute<OGP.ServiceWcf.IServiceGestionTaches>(
                "ClientTest",
                client =>
                {
                    List<VOToDoList> list = client.ChargementFichiers();
                    foreach (VOToDoList liste in list)
                    {
                        MessageBox.Show("Le nom est : " + liste.NomDuProjet + " Heure : " + liste.DateDerniereModif.Hour);
                    }
                });

            if (exception != null)
            {
                // TODO : gérer l'exception.
            }

        }
    }
}
