using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Threading;

namespace Cinch.Services.Service_Interfaces
{
    /// <summary>
    /// Méthodes de manipulation du thread STA.
    /// </summary>
    public interface IUiDispatcher
    {
        /// <summary>
        /// Invoque la méthode sur le Dispatcher si on y est pas déjà dessus.
        /// </summary>
        /// <param name="action">Action à réaliser.</param>
        void InvokeIfRequired(Action action);

        /// <summary>
        /// Provoque le recalcul des commandes actives.
        /// </summary>
        void RefreshCommand();

        [EditorBrowsable(EditorBrowsableState.Never)]
        Dispatcher GetDispatcher();
    }
}
