using System;
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
using Fluent;
using OGP.ClientWpf.ViewModel;
using System.Collections.ObjectModel;
using System.Collections;
using OGP.Plugin.Interfaces;
using QuantumBitDesigns.Core;

namespace OGP.ClientWpf.View
{
    /// <summary>
    /// Interaction logic for MainView.xaml
    /// </summary>
    public partial class MainView : RibbonWindow
    {
        private static MainView instance;

        /// <summary>
        /// Constructeur de l'application
        /// </summary>
        public MainView()
        {
            InitializeComponent();

            instance = this;
        }

        private void RibbonWindow_Loaded(object sender, RoutedEventArgs e)
        {
            DataContext = new MainViewModel();

            Binding b = new Binding("ListeMenu.ObservableCollection");
            SetBinding(MainView.RibbonTabsProperty, b);
        }

        public ObservableCollection<IOgpMenu> RibbonTabs
        {
            get { return (ObservableCollection<IOgpMenu>)GetValue(RibbonTabsProperty); }
            set { SetValue(RibbonTabsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for RibbonTabs.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RibbonTabsProperty =
            DependencyProperty.Register("RibbonTabs", typeof(ObservableCollection<IOgpMenu>), typeof(MainView), new UIPropertyMetadata(TibbonTabsChanged));


        public static void TibbonTabsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ObservableList<IOgpMenu> liste = e.NewValue as ObservableList<IOgpMenu>;
            ObservableList<IOgpMenu> oldListe = e.OldValue as ObservableList<IOgpMenu>;

            if (oldListe != null)
            {
                oldListe.ObservableCollection.CollectionChanged -= ObservableCollection_CollectionChanged;
            }

            // On a changé de liste, on change donc tous les menus.
            instance.ribbonPrincipal.Tabs.Clear();

            if (liste != null)
            {
                // On locke la liste, afin d'être sûr qu'elle ne bouge pas pendant l'initialisation.
                lock (liste.AcquireLock())
                {
                    // On initialise avec les menus déjà présent dans la liste.
                    foreach (RibbonTabItem item in liste.ObservableCollection)
                    {
                        instance.ribbonPrincipal.Tabs.Add(item);
                    }

                    liste.ObservableCollection.CollectionChanged += ObservableCollection_CollectionChanged;
                }
            }
        }

        static void ObservableCollection_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.OldItems != null)
            {
                foreach (RibbonTabItem item in e.OldItems)
                {
                    instance.ribbonPrincipal.Tabs.Remove(item);
                }
            }

            if (e.NewItems != null)
            {
                foreach (RibbonTabItem item in e.NewItems)
                {
                    instance.ribbonPrincipal.Tabs.Add(item);
                }
            }
        }
    }
}
