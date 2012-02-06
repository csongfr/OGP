using System;
using System.Collections.Generic;


namespace Cinch
{
    /// <summary>
    /// This class implements the IUIVisualizerService for Unit testing purposes.
    /// </summary>
    /// <example>
    /// <![CDATA[
    /// 
    ///    TestUIVisualizerService testUIVisualizerService =
    ///      (TestUIVisualizerService)
    ///        ViewModelBase.ServiceProvider.Resolve<IUIVisualizerService>();
    ///        
    ///    //Queue up the response we expect for our given TestUIVisualizerService
    ///    //for a given ICommand/Method call within the test ViewModel
    ///    testUIVisualizerService.ShowDialogResultResponders.Enqueue
    ///     (() =>
    ///        {
    ///            return true;
    ///        }
    ///     );
    /// ]]>
    /// </example>
    public class TestUIVisualizerService : IUIVisualizerService
    {
        #region Data

        /// <summary>
        /// Queue of callback delegates for the Show methods expected
        /// for the item under test
        /// </summary>
        public Queue<Func<bool>> ShowResultResponders { get; set; }

        /// <summary>
        /// Queue of callback delegates for the ShowDialog methods expected
        /// for the item under test
        /// </summary>
        public Queue<Func<bool>> ShowDialogResultResponders { get; set; }

        #endregion

        #region Ctor
        /// <summary>
        /// Ctor
        /// </summary>
        public TestUIVisualizerService()
        {
            ShowResultResponders = new Queue<Func<bool>>();
            ShowDialogResultResponders = new Queue<Func<bool>>();
        }
        #endregion

        #region IUIVisualizerService Members

        /// <summary>
        /// Returns the next Dequeue Show response expected. See the tests for 
        /// the Func callback expected values
        /// </summary>
        /// <param name="type">Type of the window.</param>
        /// <param name="state">Object state to associate with the dialog</param>
        /// <param name="setOwner">Set the owner of the window</param>
        /// <param name="completedProc">Callback used when UI closes (may be null)</param>
        /// <returns>True/False if UI is displayed</returns>
        public bool Show(Type type, object state, bool setOwner, EventHandler<UICompletedEventArgs> completedProc)
        {
            if (ShowResultResponders.Count == 0)
                throw new ApplicationException(
                    "TestUIVisualizerService Show method expects a Func<bool> callback \r\n" +
                    "delegate to be enqueued for each Show call");
            else
            {
                Func<bool> responder = ShowResultResponders.Dequeue();
                return responder();
            }
        }

        /// <summary>
        /// Returns the next Dequeue ShowDialog response expected. See the tests for 
        /// the Func callback expected values
        /// </summary>
        /// <param name="type">Type of the window.</param>
        /// <param name="state">Object state to associate with the dialog</param>
        /// <returns>True/False if UI is displayed.</returns>
        public bool? ShowDialog(Type type, object state)
        {
            if (ShowDialogResultResponders.Count == 0)
                throw new ApplicationException(
                    "TestUIVisualizerService ShowDialog method expects a Func<bool?> callback \r\n" +
                    "delegate to be enqueued for each Show call");
            else
            {
                Func<bool> responder = ShowDialogResultResponders.Dequeue();
                return responder() as bool?;
            }
        }

        #endregion

        #region IUIVisualizerService Members


        public bool? ShowDialog(Type type, object state, out object objetRetour)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IUIVisualizerService Members


        public bool Show(Type type, object state, bool setOwner, EventHandler<UICompletedEventArgs> completedProc, out object objetRetour)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
