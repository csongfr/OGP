using System;
using System.Windows;
using System.Windows.Input;
using Cinch;

namespace Utils.UiDispatcher
{
    /// <summary>
    /// This class implements the IUIVisualizerService for WPF purposes.
    /// This implementation HAD TO be in the Main interface project, as
    /// it needs to know about Popup windows that are not known about in 
    /// the ViewModel or Cinch projects.
    /// </summary>
    /// <remarks>
    ///		Historique des évolutions et anomalies.
    ///		<list type="table">
    ///			<listheader>
    ///				<term>Numéro d'évolution ou d'anomalie - Date</term>
    ///				<description>Description</description>
    ///			</listheader>
    ///		</list>
    /// </remarks>
    public class WPFUIVisualizerService : Cinch.IUIVisualizerService
    {
        #region Ctor

        /// <summary>
        /// Constructor
        /// </summary>
        public WPFUIVisualizerService()
        {
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// This method displays a modal dialog associated with the given key.
        /// </summary>
        /// <param name="type">Type of the window.</param>
        /// <param name="state">Object state to associate with the dialog</param>
        /// <returns>True/False if UI is displayed.</returns>
        public bool? ShowDialog(Type type, object state)
        {
            object objetRetour;
            return ShowDialog(type, state, out objetRetour);
        }

        /// <summary>
        /// This method displays a modal dialog associated with the given key and return an object.
        /// </summary>
        /// <param name="type">Type of the window.</param>
        /// <param name="state">Object state to associate with the dialog</param>
        /// <param name="objetRetour">Out object</param>
        /// <returns>True/False if UI is displayed.</returns>
        public bool? ShowDialog(Type type, object state, out object objetRetour)
        {
            bool? retour = null;
            object objetRetourTemp = null;

            Action method = () =>
                {
                    Window win = CreateWindow(type, state, true, null, true);

                    if (win != null)
                    {
                        Cursor curseur = Mouse.OverrideCursor;
                        Mouse.OverrideCursor = null;

                        objetRetourTemp = win.DataContext;
                        retour = win.ShowDialog();

                        Mouse.OverrideCursor = curseur;
                    }
                    else
                    {
                        objetRetourTemp = null;
                        retour = false;
                    }
                };

            UiDispatcherHelper.UiDispatcher.InvokeIfRequired(method);

            objetRetour = objetRetourTemp;
            return retour;
        }

        /// <summary>
        /// This method displays a modaless dialog associated with the given key.
        /// </summary>
        /// <param name="type">Type of the window.</param>
        /// <param name="state">Object state to associate with the dialog</param>
        /// <param name="setOwner">Set the owner of the window</param>
        /// <param name="completedProc">Callback used when UI closes (may be null)</param>
        /// <returns>True/False if UI is displayed</returns>
        public bool Show(Type type, object state, bool setOwner, EventHandler<UICompletedEventArgs> completedProc)
        {
            object objetRetour = null;
            return Show(type, state, setOwner, completedProc, out objetRetour);
        }

        /// <summary>
        /// This method displays a modaless dialog associated with the given key.
        /// </summary>
        /// <param name="type">Type of the window.</param>
        /// <param name="state">Object state to associate with the dialog</param>
        /// <param name="setOwner">Set the owner of the window</param>
        /// <param name="completedProc">Callback used when UI closes (may be null)</param>
        /// <param name="objetRetour">Objet de retour (DataContext)</param>
        /// <returns>True/False if UI is displayed</returns>
        public bool Show(Type type, object state, bool setOwner, EventHandler<UICompletedEventArgs> completedProc, out object objetRetour)
        {
            bool retour = false;
            object objetRetourTemp = null;

            UiDispatcherHelper.UiDispatcher.InvokeIfRequired((Action)delegate
                {
                    Window win = CreateWindow(type, state, setOwner, completedProc, false);
                    if (win != null)
                    {
                        objetRetourTemp = win.DataContext;
                        win.Show();
                        retour = true;
                    }
                    objetRetourTemp = win.DataContext;
                    retour = false;
                });

            objetRetour = objetRetourTemp;
            return retour;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// This creates the WPF window from a key.
        /// </summary>
        /// <param name="type">Type</param>
        /// <param name="dataContext">DataContext (state) object</param>
        /// <param name="setOwner">True/False to set ownership to MainWindow</param>
        /// <param name="completedProc">Callback</param>
        /// <param name="isModal">True if this is a ShowDialog request</param>
        /// <returns>Success code</returns>
        private Window CreateWindow(Type type, object dataContext, bool setOwner, EventHandler<UICompletedEventArgs> completedProc, bool isModal)
        {
            var win = (Window)Activator.CreateInstance(type);
            win.DataContext = dataContext;
            if (setOwner)
            {
                if (Application.Current.MainWindow != win)
                {
                    win.Owner = Application.Current.MainWindow;
                }
                else
                {
                    // Si l'erreur a lieu avant la création de la fenêtre principale, c'est la MessageBox qui va passer pour la fenêtre principale.
                    // Hors une fenêtre ne peut pas avoir soi-même comme propriétaire.
                    win.Owner = null;
                }
            }

            if (dataContext != null)
            {
                var bvm = dataContext as ViewModelBase;
                if (bvm != null)
                {
                    if (isModal)
                    {
                        bvm.CloseRequest += ((EventHandler<CloseRequestEventArgs>)((s, e) =>
                        {
                            UiDispatcherHelper.UiDispatcher.InvokeIfRequired(() =>
                                {
                                    try
                                    {
                                        win.DialogResult = e.Result;
                                    }
                                    catch (InvalidOperationException)
                                    {
                                        win.Close();
                                    }
                                });
                        })).MakeWeak(eh => bvm.CloseRequest -= eh);
                    }
                    else
                    {
                        bvm.CloseRequest += ((EventHandler<CloseRequestEventArgs>)((s, e) => UiDispatcherHelper.UiDispatcher.InvokeIfRequired(() => win.Close())))
                            .MakeWeak(eh => bvm.CloseRequest -= eh);
                    }
                    bvm.ActivateRequest += ((EventHandler<EventArgs>)((s, e) => UiDispatcherHelper.UiDispatcher.InvokeIfRequired(() => win.Activate())))
                        .MakeWeak(eh => bvm.ActivateRequest -= eh);
                }
            }

            win.Closed += (s, e) =>
                {
                    if (completedProc != null)
                    {
                        UICompletedEventArgs eventArgs = new UICompletedEventArgs
                            {
                                State = dataContext,
                                Result = isModal ? win.DialogResult : null
                            };

                        completedProc(this, eventArgs);
                    }
                };

            return win;
        }

        #endregion
    }
}
