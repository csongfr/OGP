using System;


namespace Cinch
{
    /// <summary>
    /// This interface defines a UI controller which can be used to display dialogs
    /// in either modal or modaless form from a ViewModel.
    /// </summary>
    public interface IUIVisualizerService
    {
        /// <summary>
        /// This method displays a modaless dialog associated with the given key.
        /// </summary>
        /// <param name="type">Type of the window.</param>
        /// <param name="state">Object state to associate with the dialog</param>
        /// <param name="setOwner">Set the owner of the window</param>
        /// <param name="completedProc">Callback used when UI closes (may be null)</param>
        /// <returns>True/False if UI is displayed</returns>
        bool Show(Type type, object state, bool setOwner,
            EventHandler<UICompletedEventArgs> completedProc);

        /// <summary>
        /// This method displays a modaless dialog associated with the given key.
        /// </summary>
        /// <param name="type">Type of the window.</param>
        /// <param name="state">Object state to associate with the dialog</param>
        /// <param name="setOwner">Set the owner of the window</param>
        /// <param name="completedProc">Callback used when UI closes (may be null)</param>
        /// <param name="objetRetour">Objet de retour (DataContext)</param>
        /// <returns>True/False if UI is displayed</returns>
        bool Show(Type type, object state, bool setOwner, EventHandler<UICompletedEventArgs> completedProc, out object objetRetour);

        /// <summary>
        /// This method displays a modal dialog associated with the given key.
        /// </summary>
        /// <param name="type">Type of the window.</param>
        /// <param name="state">Object state to associate with the dialog</param>
        /// <returns>True/False if UI is displayed.</returns>
        bool? ShowDialog(Type type, object state);

        /// <summary>
        /// This method displays a modal dialog associated with the given key.
        /// </summary>
        /// <param name="type">Type of the window.</param>
        /// <param name="state">Object state to associate with the dialog</param>
        /// <param name="objetRetour">Objet retourné.</param>
        /// <returns>True/False if UI is displayed.</returns>
        bool? ShowDialog(Type type, object state, out object objetRetour);
    }
}
