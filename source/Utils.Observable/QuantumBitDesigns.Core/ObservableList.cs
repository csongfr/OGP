using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows.Threading;
using Cinch;
using Cinch.Services.Service_Interfaces;

namespace Utils.Observable
{
    /// <summary>
    /// Represents a list that allows cross thread collection and property binding.
    /// Use AcquireLock for multithreaded scenarios.
    /// 
    /// VRU : Récupéré sur http://blog.quantumbitdesigns.com/2008/07/22/wpf-cross-thread-collection-binding-part-1/
    /// </summary>
    /// <typeparam name="T">Type</typeparam>
    public class ObservableList<T> : IList<T>
    {
        #region Membres privés

        /// <summary>
        /// The dispatcher that is used to notify the UI thread of changes
        /// </summary>
        private Dispatcher dispatcher;

        /// <summary>
        /// The list used by a worker thread
        /// </summary>
        private List<T> list;

        /// <summary>
        /// The ObservableCollection that UI controls should bind to
        /// </summary>
        private ObservableCollection<T> observableCollection;

        /// <summary>
        /// Change callback.
        /// </summary>
        /// <param name="index">index</param>
        /// <param name="item">item</param>
        private delegate void InsertItemCallback(int index, T item);

        /// <summary>
        /// Change callback.
        /// </summary>
        /// <param name="index">index</param>
        private delegate void RemoveAtCallback(int index);

        /// <summary>
        /// Change callback.
        /// </summary>
        /// <param name="index">index</param>
        /// <param name="item">item</param>
        private delegate void SetItemCallback(int index, T item);

        /// <summary>
        /// Change callback.
        /// </summary>
        /// <param name="item">item</param>
        private delegate void AddCallback(T item);

        /// <summary>
        /// Change callback.
        /// </summary>
        private delegate void ClearCallback();

        /// <summary>
        /// Change callback.
        /// </summary>
        /// <param name="item">item</param>
        private delegate void RemoveCallback(T item);

        /// <summary>
        /// Property change callback.
        /// </summary>
        /// <param name="item">item</param>
        /// <param name="e">e</param>
        private delegate void PropertyChangedCallback(T item, PropertyChangedEventArgs e);

        #endregion

        #region Contructeurs

        /// <summary>
        /// Creates a new instance of the ObservableBackgroundList class
        /// </summary>
        public ObservableList()
        {
            this.dispatcher = ViewModelBase.ServiceProvider.Resolve<IUiDispatcher>().GetDispatcher();
            this.list = new List<T>();
            this.observableCollection = new ObservableCollection<T>();
        }

        #endregion

        #region Propriétés publiques

        /// <summary>
        /// Gets the ObservableCollection that UI controls should bind to
        /// </summary>
        public ObservableCollection<T> ObservableCollection
        {
            get
            {
                if (this.dispatcher.CheckAccess() == false)
                {
                    throw new InvalidOperationException("ObservableCollection only accessible from UI thread");
                }
                return this.observableCollection;
            }
        }

        #endregion

        #region ICollection<T> Members

        /// <summary>
        /// IsreadOnly.
        /// </summary>
        bool ICollection<T>.IsReadOnly
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// Searches for the specified object and returns the zero-based index of the
        /// first occurrence within the entire List.
        /// </summary>
        /// <param name="item">The object to locate in the List</param>
        /// <returns>The zero-based index of the first occurrence of item within the entire List
        /// if found; otherwise, –1.</returns>
        public int IndexOf(T item)
        {
            return this.list.IndexOf(item);
        }

        /// <summary>
        /// Inserts an element into the List at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index at which item should be inserted.</param>
        /// <param name="item">The object to insert. The value can be null for reference types.</param>
        public void Insert(int index, T item)
        {
            this.list.Insert(index, item);
            this.dispatcher.BeginInvoke(
                DispatcherPriority.Send,
                new InsertItemCallback(InsertItemFromDispatcherThread),
                index,
                new object[] { item });
        }

        /// <summary>
        /// Removes the element at the specified index of the List
        /// </summary>
        /// <param name="index">The zero-based index of the element to remove.</param>
        public void RemoveAt(int index)
        {
            this.list.RemoveAt(index);
            this.dispatcher.BeginInvoke(
                DispatcherPriority.Send,
                new RemoveAtCallback(RemoveAtFromDispatcherThread),
                index);
        }

        /// <summary>
        /// Gets or sets the element at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index of the element to get or set.</param>
        /// <returns>The element at the specified index.</returns>
        public T this[int index]
        {
            get
            {
                return this.list[index];
            }
            set
            {
                this.list[index] = value;
                this.dispatcher.BeginInvoke(
                    DispatcherPriority.Send,
                    new SetItemCallback(SetItemFromDispatcherThread),
                    index,
                    new object[] { value });
            }
        }

        #endregion

        #region ICollection<T> Members

        /// <summary>
        /// Adds an object to the end of the List.
        /// </summary>
        /// <param name="item">The object to be added to the end of the List</param>
        public void Add(T item)
        {
            this.list.Add(item);
            this.dispatcher.BeginInvoke(
                DispatcherPriority.Send,
                new AddCallback(AddFromDispatcherThread),
                item);
        }

        /// <summary>
        /// Removes all elements from the List
        /// </summary>
        public void Clear()
        {
            this.list.Clear();
            this.dispatcher.BeginInvoke(
                DispatcherPriority.Send,
                new ClearCallback(ClearFromDispatcherThread));
        }

        /// <summary>
        /// Determines whether an element is in the List
        /// </summary>
        /// <param name="item">The object to locate in the List</param>
        /// <returns>true if item is found in the List; otherwise, false</returns>
        public bool Contains(T item)
        {
            return this.list.Contains(item);
        }

        /// <summary>
        /// Copies the entire List to a compatible one-dimensional
        /// array, starting at the specified index of the target array.
        /// </summary>
        /// <param name="array">The one-dimensional System.Array that is the destination of the elements
        /// copied from System.Collections.Generic.List[T]. The System.Array must have
        /// zero-based indexing.</param>
        /// <param name="arrayIndex">The zero-based index in array at which copying begins.</param>
        public void CopyTo(T[] array, int arrayIndex)
        {
            this.list.CopyTo(array, arrayIndex);
        }

        /// <summary>
        /// Gets the number of elements actually contained in the List.
        /// </summary>
        public int Count
        {
            get
            {
                return this.list.Count;
            }
        }

        /// <summary>
        /// Removes the first occurrence of a specific object from the List.
        /// </summary>
        /// <param name="item">The object to remove from the List.</param>
        /// <returns>true if item is successfully removed; otherwise, false. This method also returns false if item was not found in the List</returns>
        public bool Remove(T item)
        {
            bool result = this.list.Remove(item);

            // only remove the item from the UI collection if it is removed from the worker collection
            if (result == true)
            {
                this.dispatcher.BeginInvoke(
                    DispatcherPriority.Send,
                    new RemoveCallback(RemoveFromDispatcherThread),
                    item);
            }
            return result;
        }

        #endregion

        #region IEnumerable<T> Members

        /// <summary>
        /// Returns an enumerator that iterates through the List
        /// </summary>
        /// <returns>A System.Collections.Generic.List[T].Enumerator for the System.Collections.Generic.List[T].</returns>
        public IEnumerator<T> GetEnumerator()
        {
            return this.list.GetEnumerator();
        }

        #endregion

        #region IEnumerable Members

        /// <summary>
        /// Returns an enumerator that iterates through the List
        /// </summary>
        /// <returns>Am Enumerator for the List.</returns>
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.list.GetEnumerator();
        }

        #endregion

        #region Méthodes privées

        /// <summary>
        /// Attaches listener for the CollectionItemNotifyPropertyChanged event
        /// </summary>
        /// <param name="source">The event source</param>
        private void StartListening(T source)
        {
            ICollectionItemNotifyPropertyChanged item = source as ICollectionItemNotifyPropertyChanged;
            if (item != null)
            {
                item.CollectionItemPropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(ItemCollectionItemPropertyChanged);
            }
        }

        /// <summary>
        /// Removes listener for the CollectionItemNotifyPropertyChanged event
        /// </summary>
        /// <param name="source">The event source</param>
        private void StopListening(T source)
        {
            ICollectionItemNotifyPropertyChanged item = source as ICollectionItemNotifyPropertyChanged;
            if (item != null)
            {
                item.CollectionItemPropertyChanged -= new System.ComponentModel.PropertyChangedEventHandler(ItemCollectionItemPropertyChanged);
            }
        }

        /// <summary>
        /// Handles the CollectionItemNotifyPropertyChanged event by posting to the UI thread to
        /// raise the PropertyChanged event
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void ItemCollectionItemPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            this.dispatcher.BeginInvoke(
                DispatcherPriority.Send,
                new PropertyChangedCallback(PropertyChangedFromDispatcherThread),
                sender,
                new object[] { e });
        }

        /// <summary>
        /// Raises the PropertyChanged event.
        /// </summary>
        /// <param name="source">source</param>
        /// <param name="e">e</param>
        private void PropertyChangedFromDispatcherThread(T source, PropertyChangedEventArgs e)
        {
            ICollectionItemNotifyPropertyChanged item = source as ICollectionItemNotifyPropertyChanged;
            item.NotifyPropertyChanged(e);
        }

        /// <summary>
        /// Change callback.
        /// </summary>
        /// <param name="index">index</param>
        /// <param name="item">item</param>
        private void InsertItemFromDispatcherThread(int index, T item)
        {
            this.observableCollection.Insert(index, item);
            StartListening(item);
        }

        /// <summary>
        /// Change callback.
        /// </summary>
        /// <param name="index">index</param>
        private void RemoveAtFromDispatcherThread(int index)
        {
            StopListening(this.observableCollection[index]);
            this.observableCollection.RemoveAt(index);
        }

        /// <summary>
        /// Change callback.
        /// </summary>
        /// <param name="index">index</param>
        /// <param name="item">item</param>
        private void SetItemFromDispatcherThread(int index, T item)
        {
            StopListening(this.observableCollection[index]);
            this.observableCollection[index] = item;
            StartListening(item);
        }

        /// <summary>
        /// Change callback.
        /// </summary>
        /// <param name="item">item</param>
        private void AddFromDispatcherThread(T item)
        {
            this.observableCollection.Add(item);
            StartListening(item);
        }

        /// <summary>
        /// Change callback.
        /// </summary>
        private void ClearFromDispatcherThread()
        {
            foreach (T item in this.observableCollection)
            {
                StopListening(item);
            }
            this.observableCollection.Clear();
        }

        /// <summary>
        /// Change callback.
        /// </summary>
        /// <param name="item">item</param>
        private void RemoveFromDispatcherThread(T item)
        {
            StopListening(item);
            this.observableCollection.Remove(item);
        }

        #endregion

        /// <summary>
        /// Acquires an object that locks on the collection. The lock is released when the object is disposed
        /// </summary>
        /// <returns>A disposable object that unlocks the collection when disposed</returns>
        public TimedLock AcquireLock()
        {
            return TimedLock.Lock(((ICollection)this.list).SyncRoot);
        }
    }
}
