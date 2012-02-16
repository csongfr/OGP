﻿using System;
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
    /// Interaction logic for TousLesFichiersView.xaml
    /// </summary>
    public partial class PopupOuvrirTodolistView : Window
    {
        /// <summary>
        /// Constructeur
        /// </summary>
        public PopupOuvrirTodolistView()
        {
            InitializeComponent();
            //Vm = new PopupOuvrirTodolistViewModel();
            this.DataContext = new PopupOuvrirTodolistViewModel();
        }

        /// <summary>
        /// Action de fermeture
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">event</param>
        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            // TODO fhu : plus propre
            this.Close();
        }
    }
}
