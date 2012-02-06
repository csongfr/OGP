using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Windows.Threading;

namespace Cinch
{
    /// <summary>
    /// This class provides an ObservableCollection which supports the 
    /// Dispatcher thread marshalling for added items. 
    /// 
    /// This class does not take support any thread sycnhronization of
    /// adding items using multiple threads, that level of thread synchronization
    /// is left to the user. This class simply marshalls the CollectionChanged
    /// call to the correct Dispatcher thread
    /// </summary>
    /// <typeparam name="T">Type this collection holds</typeparam>
    public class DispatcherNotifiedObservableCollection<T> : ObservableCollection<T>
    {
        #region Ctors

        public DispatcherNotifiedObservableCollection()
            : base()
        {
        }

        public DispatcherNotifiedObservableCollection(List<T> list)
            : base(list)
        {
        }

        public DispatcherNotifiedObservableCollection(IEnumerable<T> collection)
            : base(collection)
        {
        }
        #endregion

        #region Overrides

        /// <summary>
        /// Occurs when an item is added, removed, changed, moved, 
        /// or the entire list is refreshed.
        /// </summary>
        public override event NotifyCollectionChangedEventHandler CollectionChanged;

        /// <summary>
        /// Raises the <see cref="E:System.Collections.ObjectModel.
        /// ObservableCollection`1.CollectionChanged"/> 
        /// event with the provided arguments.
        /// </summary>
        /// <param name="e">Arguments of the event being raised.</param>
        protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            // Be nice - use BlockReentrancy like MSDN said
            using (BlockReentrancy())
            {
                System.Collections.Specialized.NotifyCollectionChangedEventHandler eventHandler = CollectionChanged;
                if (eventHandler == null)
                    return;

                Delegate[] delegates = eventHandler.GetInvocationList();
                // Walk thru invocation list
                foreach (System.Collections.Specialized.NotifyCollectionChangedEventHandler handler in delegates)
                {
                    DispatcherObject dispatcherObject = handler.Target as DispatcherObject;
                    // If the subscriber is a DispatcherObject and different thread
                    if (dispatcherObject != null && dispatcherObject.CheckAccess() == false)
                    {
                        // Invoke handler in the target dispatcher's thread
                        dispatcherObject.Dispatcher.Invoke(DispatcherPriority.DataBind, handler, this, e);
                    }
                    else // Execute handler as is
                    {
                        handler(this, e);
                    }
                }
            }
        }
        #endregion
    }
}
