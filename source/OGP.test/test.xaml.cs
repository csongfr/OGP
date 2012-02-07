using System.ComponentModel.Composition;
using AvalonDock;

namespace OGP.test
{
    /// <summary>
    /// Logique d'interaction pour MonDocument.xaml
    /// </summary>
    [Export(typeof(DocumentContent))]
    [ExportMetadata("Title", "test")]
    public partial class test : DocumentContent
    {

        #region Constructeur

        /// <summary>
        /// Constructeur par défaud
        /// </summary>
        public test()
        {
            InitializeComponent();
        }

        #endregion
    }
}
