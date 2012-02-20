using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using Cinch;
using Utils.UiDispatcher;
using System.Windows.Input;
using Utils.Log;
using OGP.Plugin.Exception;

namespace OGP.ClientWpf
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// Indique que l'application est en train de se fermer.
        /// </summary>
        private bool applicationEnArret = false;
   
        /// <summary>
        /// Lancement du boostrapper.
        /// </summary>
        /// <param name="e">e</param>
        protected override void OnStartup(StartupEventArgs e)
        {
            this.ShutdownMode = ShutdownMode.OnMainWindowClose;
                                                                                                                                                                        
            base.OnStartup(e);

            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
            this.DispatcherUnhandledException += new System.Windows.Threading.DispatcherUnhandledExceptionEventHandler(Application_DispatcherUnhandledException);

            WaitCursor.UiDispatcher = Dispatcher;
            UiDispatcherHelper.SetUiDispatcher(Dispatcher);
        }

        /// <summary>
        /// Capte les exceptions non catchées dans le domaine. C'est le dernier moyen de détecter une exception, mais c'est déjà trop tard pour la gérer.
        /// Notamment parce que l'exception peut avoir eu lieu dans un autre thread.
        /// Explique à l'utilisateur la fermeture immédiate de l'application suite à une erreur.
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
#if DEBUG
            System.Diagnostics.Debugger.Break();
#endif
            Environment.ExitCode = 2;
            // Garantit qu'on a plus le curseur d'attente.
            Mouse.OverrideCursor = null;

            var exception = e.ExceptionObject as Exception;
            if (exception != null)
            {
                LogHelper.Log(new BasicLogEntry { Exception = exception, Message = "Une erreur fatale est survenue. L'application va être arrêtée. Plus d'informations sont disponibles dans le fichier de traces." });
            }
            else
            {
                LogHelper.Log(new BasicLogEntry { Message = "Une erreur NON MANAGEE fatale est survenue. L'application va être arrêtée. Plus d'informations sont disponibles dans le fichier de traces." });
            }

            MessageBox.Show(
                "Une erreur fatale est survenue. L'application va être arrêtée. Plus d'informations sont disponibles dans le fichier de traces.",
                "Fermeture en cours ...",
                MessageBoxButton.OK,
                MessageBoxImage.Error);
        }

        /// <summary>
        /// Capte et gère les exceptions non gérées au niveau du thread STA (thread principal de l'application).
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void Application_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            // Garantit qu'on a plus le curseur d'attente.
            Mouse.OverrideCursor = null;

            if (e.Exception is OgpClientCoreException )
            {
                LogHelper.Log(new BasicLogEntry { Message = "Erreur survenue", Exception = e.Exception});

                // Evite la fermeture de l'application.
                e.Handled = true;

               MessageBox.Show(
                e.Exception.Message,
                "Une erreur gérée est survenue.",
                MessageBoxButton.OK,
                MessageBoxImage.Warning);
            }
            else if (e.Exception is OgpPluginException)
            {
                throw new NotImplementedException();
            }
            else
            {
#if DEBUG
                System.Diagnostics.Debugger.Break();
#endif

                // Si on est déjà passé par ce code, c'est que ce code a levé une exception.
                // On embraye sur la fermeture directe de l'application pour éviter tout artefact d'affichage.
                if (applicationEnArret == false)
                {
                    // On mémorise qu'on a tenté d'exécuter ce code.
                    applicationEnArret = true;

                    LogHelper.Log(new BasicLogEntry { Message = "Erreur survenue", Exception = e.Exception });

                    // Dans le cas des exceptions imprévues, on préfère arrêter l'application par sécurité.
                    MessageBox.Show(
                        "Une erreur inattendue est survenue. L'application va être arrêtée. Plus d'informations sont disponibles dans le fichier de traces.",
                        "Une erreur est survenue",
                        MessageBoxButton.OK,
                        MessageBoxImage.Error);
                }

                e.Handled = true;

                Environment.Exit(1);
            }
        }
    }
}
