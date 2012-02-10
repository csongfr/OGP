using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Cinch;

namespace Utils.ViewModel
{
    /// <summary>
    /// Classe qui gère les INotifyPropertyChanged
    /// </summary>
    public class ViewModelBase : INotifyPropertyChanged
    {
        /// <summary>
        /// Service resolver for view models.  Allows derived types to add/remove
        /// services from mapping.
        /// </summary>
        public static readonly ServiceProvider ServiceProvider = new ServiceProvider();

        /// <summary>
        /// This resolves a service type and returns the implementation.
        /// </summary>
        /// <typeparam name="T">Type to resolve</typeparam>
        /// <returns>Implementation</returns>
        protected T Resolve<T>()
        {
            return ServiceProvider.Resolve<T>();
        }

        #region Events

        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>  
        /// Notify using pre-made PropertyChangedEventArgs
        /// </summary>
        /// <param name="propertyName">Propertiy notified.</param>
        public void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        /// <summary>
        /// Notify using pre-made PropertyChangedEventArgs
        /// </summary>
        /// <param name="args">Propertiy notified.</param>
        protected void NotifyPropertyChanged(PropertyChangedEventArgs args)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, args);
            }
        }
        #endregion
    }
}
