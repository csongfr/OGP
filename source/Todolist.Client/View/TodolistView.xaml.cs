using System;
using System.ComponentModel.Composition;
using System.Windows;
using System.Windows.Input;
using AvalonDock;
using Todolist.ViewModel;

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
        }

        private void DocumentContent_Loaded(object sender, RoutedEventArgs e)
        {
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
        /// <param name="sender">object</param>
        /// <param name="e">MouseEventArgs</param>
        private void OnMouseDown(object sender, MouseEventArgs e)
        {
            // Determines l'item selection.
            // TacheViewModel tvm = ((TacheViewModel)sender);
        }
    }
}
