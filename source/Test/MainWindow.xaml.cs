using System.Collections.Generic;
using System.Windows;
using System.Windows.Documents;
using OGP.ValueObjects;
using Utils.Wcf;

namespace Test
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Constructeur par défaut
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Bouton de test
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Event</param>
        private void BoutonTest(object sender, RoutedEventArgs e)
        {
            var exception = WcfHelper.Execute<OGP.ServiceWcf.IServiceGestionTaches>(
               "ClientTest",
               client =>
               {
                   List<VOTodolist> list = client.ChargementFichiers();
                   foreach (VOTodolist liste in list)
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
