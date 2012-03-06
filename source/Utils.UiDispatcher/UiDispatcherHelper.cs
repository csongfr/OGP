using System;
using System.Windows.Threading;
using Cinch;
using Cinch.Services.Service_Interfaces;

namespace Utils.UiDispatcher
{
    /// <summary>
    /// Helper permettant d'accéder au thread d'affichage.
    /// </summary>
    public static class UiDispatcherHelper
    {
        /// <summary>
        /// VRU : Renseigner pour l'ajout de la gestion de l'appel de WaitCursor depuis l'extérieur du thread STA.
        /// </summary>
        private static Dispatcher uiDispatcher;

        /// <summary>
        /// Permet d'exécuter des instructions sur le thread d'affichage tout assurant la continuité de la gestion d'exception.
        /// </summary>
        public static IDispatcher UiDispatcher { get; set; }

        /// <summary>
        /// Permet d'indiquer quel est le thread d'affichage (thread STA).
        /// </summary>
        /// <param name="dispatcher">Thread d'affichage.</param>
        public static void SetUiDispatcher(Dispatcher dispatcher)
        {
            uiDispatcher = dispatcher;
            UiDispatcher = new DispatcherWrapper(dispatcher);
        }

        /// <summary>
        /// Provoque la réinitialisation des états des commandes (force la réexécution les CanExecute).
        /// </summary>
        public static void RefreshCommandes()
        {
            UiDispatcher.InvokeIfRequired((Action)System.Windows.Input.CommandManager.InvalidateRequerySuggested);
        }

        /// <summary>
        /// Constructeur statique.
        /// </summary>
        static UiDispatcherHelper()
        {
            ViewModelBase.ServiceProvider.Add(typeof(IUiDispatcher), new UiDispatcherNested());
        }

        /// <summary>
        /// Wrapper ajouté pour publier ces fonctionnalités aux classes qui ne peuvent pas voir la classe statique.
        /// C'est trop tard pour changer, mais il aurait fallu utiliser plus d'injection de dépendance dès le départ pour éviter ça.
        /// </summary>
        private class UiDispatcherNested : IUiDispatcher
        {
            /// <summary>
            /// Invoque la méthode sur le Dispatcher si on y est pas déjà dessus.
            /// </summary>
            /// <param name="action">Action à réaliser.</param>
            public void InvokeIfRequired(Action action)
            {
                UiDispatcher.InvokeIfRequired(action);
            }

            /// <summary>
            /// Provoque le recalcul des commandes actives.
            /// </summary>
            public void RefreshCommand()
            {
                UiDispatcherHelper.RefreshCommandes();
            }

            /// <summary>
            /// Retourne le dispatcher de l'affichage.
            /// </summary>
            /// <returns>Dispatcher.</returns>
            Dispatcher IUiDispatcher.GetDispatcher()
            {
                return uiDispatcher;
            }
        }
    }
}
