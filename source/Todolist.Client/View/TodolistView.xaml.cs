﻿using System.ComponentModel.Composition;
using AvalonDock;
using Fluent;
using Todolist.ViewModel;
using System.Windows;
<<<<<<< HEAD
using System.Windows.Input;
using System;
using System.Windows.Controls;
=======
>>>>>>> 74e42031f74ee47865c950491b37c89e2358e554

namespace Plugin.Todolist
{
    /// <summary>
    /// Logique d'interaction pour MonDocument.xaml
    /// </summary>
    [Export(typeof(DocumentContent))]
    public partial class Todolist : DocumentContent
    {
        /// <summary>
        /// Constructeur de la TODoList
        /// </summary>
        public Todolist()
        {
            InitializeComponent();
            this.DataContext = new TodolistViewModel();
        }

        /* This event fires when the mouse enters a control in which d&d is enabled. It gives you the opportunity to
        - inspect the DragEventArgs to see the DataObject being dragged onto your control is something you can accept.
        - perform any initialization you may need to handle the data.
        - set the Effect property to indicate whether the item will be copied or moved to the control. */
        private void OnDragEnter(object sender, DragEventArgs e)
        {
            e.Effects = DragDropEffects.All;
            TacheViewModel tvm = new TacheViewModel();
            Type type = tvm.GetType();
            // e.Data = e.Data.GetData(tvm);
        }

        /// <summary>
        /// Sauvegarde des infos pour le drag and drop
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnMouseDown(object sender, MouseEventArgs e)
        {
            // Determines l'item selection.
            // TacheViewModel tvm = ((TacheViewModel)sender);
           
        }
    }
}
