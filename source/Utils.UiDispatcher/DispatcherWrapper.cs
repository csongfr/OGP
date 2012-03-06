using System;
using System.Windows.Threading;
using Cinch;

namespace Utils.UiDispatcher
{
    /// <summary>
    /// Encapsule un dispatcher et gère la transmission des exceptions au thread appelant.
    /// </summary>
    internal class DispatcherWrapper : IDispatcher
    {
        /// <summary>
        /// Stocke le dispatcher encapsulé.
        /// </summary>
        private Dispatcher dispatcher;

        /// <summary>
        /// Crée un wrapper qui encapsule un dispatcher.
        /// </summary>
        /// <param name="dispatcher">Dispatcher.</param>
        public DispatcherWrapper(Dispatcher dispatcher)
        {
            this.dispatcher = dispatcher;
        }

        /// <summary>
        /// Invoke l'action fournie sur le thread encapsulé, en transmettant l'éventuelle exception au thread appelant.
        /// </summary>
        /// <param name="action">Action à exécuter.</param>
        public void Invoke(Action action)
        {
            Exception exception = null;

            this.dispatcher.Invoke((Action)(() =>
            {
                try
                {
                    action();
                }
                catch (Exception ex)
                {
                    exception = ex;
                }
            }));

            if (exception != null)
            {
                 throw exception;
            }
        }

        /// <summary>
        /// Exécute l'action fournie, si nécessaire en Invoke, sur le thread encapsulé, en transmettant l'éventuelle exception au thread appelant.
        /// </summary>
        /// <param name="action">Action à exécuter.</param>
        public void InvokeIfRequired(Action action)
        {
            Exception exception = null;

            this.dispatcher.InvokeIfRequired(() =>
            {
                try
                {
                    action();
                }
                catch (Exception ex)
                {
                    exception = ex;
                }
            });

            if (exception != null)
            {
                throw exception;
            }
        }
    }
}
