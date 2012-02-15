using System.ComponentModel.Composition;
using AvalonDock;
using Fluent;
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
            this.DataContext = new TodolistViewModel();
        }
    }
}
