using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace QuantumBitDesigns.Core
{
    /// <summary>
    /// Notifies the clients that a property has changed
    /// 
    /// VRU : Récupéré sur http://blog.quantumbitdesigns.com/2008/07/22/wpf-cross-thread-collection-binding-part-1/
    /// </summary>
    public interface ICollectionItemNotifyPropertyChanged : INotifyPropertyChanged
    {
        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        event PropertyChangedEventHandler CollectionItemPropertyChanged;

        /// <summary>
        /// Raises the PropertyChanged event.
        /// </summary>
        /// <param name="e">e</param>
        void NotifyPropertyChanged(PropertyChangedEventArgs e);
    }
}
