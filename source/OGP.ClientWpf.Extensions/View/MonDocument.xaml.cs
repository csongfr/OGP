using System.ComponentModel.Composition;
using AvalonDock;

namespace OGP.ClientWpf.Extensions
{
    /// <summary>
    /// Logique d'interaction pour MonDocument.xaml
    /// </summary>
    [Export(typeof(DocumentContent))]
    [ExportMetadata("Title", "ToDoList")]
    public partial class MonDocument : DocumentContent
    {
        public MonDocument()
        {
            InitializeComponent();
            this.DataContext = new MonDocumentViewModel();
        }
    }
}
