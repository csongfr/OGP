using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Input;
using System.Windows.Threading;

/// <summary>
/// Allows a hour glass cursor to be shown.
/// This is more than likely specific to WPF
/// </summary>
namespace Cinch
{
    /// <summary>
    /// This class implements a disposable WaitCursor to 
    /// show an hourglass while some long-running event occurs.
    /// </summary>
    /// <example>
    /// <![CDATA[
    /// 
    /// using (new WaitCursor())
    /// {
    ///    .. Do work here ..
    /// }
    /// 
    /// ]]>
    /// </example>
    public class WaitCursor : IDisposable
    {
        #region Data

        /// <summary>
        /// Objet de verrouillage du compteur.
        /// </summary>
        private static readonly object lockObject = new object();

        /// <summary>
        /// Compte le nombre de WaitCursor créé.
        /// </summary>
        private static volatile int compteur = 0;

        #endregion

        #region Properties

        /// <summary>
        /// VRU : Renseigner pour l'ajout de la gestion de l'appel de WaitCursor depuis l'extérieur du thread STA.
        /// </summary>
        public static Dispatcher UiDispatcher { get; set; }

        #endregion

        #region Ctor

        /// <summary>
        /// Constructor : crée un WaitCursor et affiche le curseur d'attente.
        /// </summary>
        public WaitCursor()
            : this(Cursors.Wait)
        {
        }

        /// <summary>
        /// Constructor : crée un WaitCursor et affiche le curseur d'attente.
        /// </summary>
        /// <param name="typeDeCurseur">Type de curseur à afficher</param>
        public WaitCursor(Cursor typeDeCurseur)
        {
            lock (lockObject)
            {
                if (compteur == 0)
                {
                    DisplayWaitingCursor(typeDeCurseur);
                }
                compteur++;
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Affiche le curseur normal s'il n'y a plus de WaitCursor actif.
        /// </summary>
        public void Dispose()
        {
            lock (lockObject)
            {
                compteur--;
                if (compteur == 0)
                {
                    HideWaitingCursor();
                }
            }
        }

        /// <summary>
        /// Retourne true pour indiquer que le curseur d'attente est actif. false sinon.
        /// </summary>
        /// <returns>true pour en attente.</returns>
        public static bool IsWaiting()
        {
            lock (lockObject)
            {
                return compteur > 0;
            }
        }

        #endregion

        #region Privates methods

        /// <summary>
        /// Affiche le curseur d'attente, en gérant les pb de threads.
        /// </summary>
        /// <param name="typeDeCurseur">Type de curseur à afficher</param>
        private void DisplayWaitingCursor(Cursor typeDeCurseur)
        {
            if (UiDispatcher != null)
            {
                UiDispatcher.BeginInvoke(DispatcherPriority.Normal, (Action)delegate
             {
                 Mouse.OverrideCursor = typeDeCurseur;
             });
            }
        }

        /// <summary>
        /// Cache le curseur d'attente, en gérant les pb de threads.
        /// </summary>
        private void HideWaitingCursor()
        {
            if (UiDispatcher != null)
            {
                UiDispatcher.BeginInvoke(DispatcherPriority.Normal, (Action)delegate
                {
                    Mouse.OverrideCursor = null;
                });
            }
        }

        #endregion
    }
}
