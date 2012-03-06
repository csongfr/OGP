using System;

namespace Utils.UiDispatcher
{
    /// <summary>
    /// Méthodes disponibles classiquement sur un Dispatcher.
    /// </summary>
    public interface IDispatcher
    {
        /// <summary>
        /// Invoque la méthode sur le Dispatcher.
        /// </summary>
        /// <param name="action">Action à réaliser.</param>
        void Invoke(Action action);

        /// <summary>
        /// Invoque la méthode sur le Dispatcher si on y est pas déjà dessus.
        /// </summary>
        /// <param name="action">Action à réaliser.</param>
        void InvokeIfRequired(Action action);
    }
}
