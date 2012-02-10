using System.ComponentModel.Composition;
using AvalonDock;
using OGP.ClientWpf.ViewModel;

namespace OGP.ClientWpf.Extensions
{
    /// <summary>
    /// Logique d'interaction pour MonDocument.xaml
    /// </summary>
    [Export(typeof(DocumentContent))]
    [ExportMetadata("Title", "ToDoList")]
    public partial class ToDoList : DocumentContent
    {
        /// <summary>
        /// Constructeur de la TODoList
        /// </summary>
        public ToDoList()
        {
            InitializeComponent();
            this.DataContext = new ToDoListViewModel();
        }
    }
}
