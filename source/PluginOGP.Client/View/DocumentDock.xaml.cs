using AvalonDock;
using OGP.Plugin.Interfaces;
using OGP.ServicePlugin.Modele;
using PluginOGP.Client.ViewModel;
using System.Collections.Generic;
using System.Windows.Controls;

namespace PluginOGP.Client.View
{
    /// <summary>
    /// Interaction logic for FenetrePrincipale.xaml
    /// </summary>
    abstract public partial class DocumentDock : DocumentContent
    {
        protected DockControler itemControler;
        public DockControler ItemControler
        {
            get
            {
                return itemControler;
            }
        }

        protected DocumentDock(string title)
        {
            InitializeComponent();
            this.Title = title;
        }

        abstract public void DoSomethingWhenBackgroundBegin();

        abstract public void DoSomethingWhenBackgroundEnd();

    }

    public class LocalDocumentDock : DocumentDock
    {
        public LocalDocumentDock(string title)
            : base(title)
        {
            itemControler = new LocalDockControler();
            this.documentContainer.Children.Add(itemControler);
        }

        public override void DoSomethingWhenBackgroundBegin()
        {
            // nothing
        }

        public override void DoSomethingWhenBackgroundEnd()
        {
            // nothing
        }
    }

    public class ServerDocumentDock : DocumentDock
    {
        public ServerDocumentDock(string title)
            : base(title)
        {
            itemControler = new ServerDockControler();
        }

        public override void DoSomethingWhenBackgroundBegin()
        {
            if (this.documentContainer.Children.Count > 0)
            {
                this.documentContainer.Children.RemoveAt(0);
            }
            this.documentContainer.VerticalAlignment = System.Windows.VerticalAlignment.Center;
            this.documentContainer.Children.Add(new LoadingAnimation());
        }

        public override void DoSomethingWhenBackgroundEnd()
        {
            if (this.documentContainer.Children.Count > 0)
            {
                this.documentContainer.Children.RemoveAt(0);
            }
            this.documentContainer.VerticalAlignment = System.Windows.VerticalAlignment.Top;
            this.documentContainer.Children.Add(itemControler);
        }
    }

}
