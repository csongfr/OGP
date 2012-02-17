using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using Cinch;
using Utils.UiDispatcher;

namespace OGP.ClientWpf
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {                 
        /// <summary>
        /// Lancement du boostrapper.
        /// </summary>
        /// <param name="e">e</param>
        protected override void OnStartup(StartupEventArgs e)
        {
            this.ShutdownMode = ShutdownMode.OnMainWindowClose;
                                                                                                                                                                        
            base.OnStartup(e);

            WaitCursor.UiDispatcher = Dispatcher;
            UiDispatcherHelper.SetUiDispatcher(Dispatcher);
        }
    }
}
