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
using Utils.Wcf;
using OGP.ValueObjects;

namespace ClientTests
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, RoutedEventArgs e)
        {
            var exception = WcfHelper.Execute<OGP.ServiceWcf.IServiceGestionTaches>(
                "ClientTest",
                client =>
                {
                    //VOToDoList listetaches = client.ChagerListeTaches("");
                    VOToDoList listetaches = client.NouvelleToDoList("TestNewToDoList", "Test");
                });

            if (exception != null)
            {
                //TODO : gérer l'exception.
            }
        }
    }
}
