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
using AvalonDock;

namespace PluginTest
{
    /// <summary>
    /// Interaction logic for UserControl2.xaml
    /// </summary>
    [Export(typeof(DocumentContent))]
    public partial class UserControl2 : DocumentContent
    {
        public UserControl2()
        {
            InitializeComponent();

        }
    }
}
