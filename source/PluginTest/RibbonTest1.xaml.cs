﻿using System;
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
using Fluent;
using OGP.Plugin.Interfaces;

namespace PluginTest
{
    /// <summary>
    /// Interaction logic for RibbonTest1.xaml
    /// </summary>
    [Export(typeof(IOgpMenu))]
    public partial class RibbonTest1 : RibbonTabItem, IOgpMenu
    {
        /// <summary>
        /// Constructeur
        /// </summary>
        public RibbonTest1()
        {
            InitializeComponent();
            DataContext = new RibbonTest1ViewModel();
        }

        public void Initialize()
        {
            ((RibbonTest1ViewModel)this.DataContext).Initialize();
        }
    }
}
