using System;
using System.Collections.Generic;
using System.Linq;
using OGP.ClientWpf.Comands;
using System.Windows.Input;
using System.ComponentModel;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition;
using AvalonDock;
using System.IO;
using System.Windows;




namespace OGP.ClientWpf.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {                              
        #region Events

        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        // Raise PropertyChanged event
        void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion

        #region Membres privés       

        private RelayCommand exitCommand;

        private CompositionContainer _container;

        [ImportMany]
        private IEnumerable<Lazy<DocumentContent, IDocumentData>> Plugins
        {
            get;
            set;
        }

        #endregion

        #region Membres public

        public List<DocumentContent> Documents
        {
            get;
            set;
        }

        #endregion
                             
        #region Commands

        #region Exit

        /// <summary>
        /// Exit from the application
        /// </summary>
        public ICommand ExitCommand
        {
            get
            {
                if (exitCommand == null)
                {
                    exitCommand = new RelayCommand(x => System.Windows.Application.Current.Shutdown());
                }
                return exitCommand;
            }
        }

        #endregion

        #endregion

        #region Constructeur

        /// <summary>
        /// Default constructor
        /// </summary>
        public MainViewModel()
        {
            this.Documents = new List<DocumentContent>();
            ChargerPlugins();
        }

        #endregion

        #region Méthodes privées

        private void ChargerPlugins()
        {
            try
            {
                var catalog = new AggregateCatalog();
                catalog.Catalogs.Add(new DirectoryCatalog(@"..\..\Ressources\Plugins"));
                _container = new CompositionContainer(catalog);
                this._container.ComposeParts(this);

                foreach (var plugin in this.Plugins)
                {
                    if (plugin.Value != null)
                    {
                        this.Documents.Add(plugin.Value);
                    }
                }
            }
            catch (CompositionException compositionException)
            {
                Console.WriteLine(compositionException.ToString());
            }
            catch (DirectoryNotFoundException)
            {
                MessageBox.Show("Le répertoire des plugins n'existe pas. Aucun plugin ne sera chargé", "Attention", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
            }
        }

        #endregion
    }
}
