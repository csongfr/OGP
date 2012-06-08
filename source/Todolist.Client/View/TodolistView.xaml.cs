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
    //[Export(typeof(DocumentContent))]
    public partial class Todolist : DocumentContent
    {
        /// <summary>
        /// Constructeur de la TODoList
        /// </summary>
        public Todolist()
        {
            InitializeComponent();
        }
    }
}
