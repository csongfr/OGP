﻿using PluginOGP.Client.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PluginOGP.Client.View
{
    /// <summary>
    /// Interaction logic for dockControler.xaml
    /// </summary>
    public partial class DockControler : UserControl
    {
        protected DockControler()
        {
            InitializeComponent();
        }
    }

    public class LocalDockControler : DockControler
    {
        public LocalDockControler()
            : base()
        {
            this.DataContext = new LocalPluginsViewModel();
        }
    }

    public class ServerDockControler : DockControler
    {
        public ServerDockControler()
            : base()
        {
            this.DataContext = new RemotePluginsViewModel();
        }
    }
}
