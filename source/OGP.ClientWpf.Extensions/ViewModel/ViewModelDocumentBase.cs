using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace OGP.ClientWpf.Extensions.ViewModel
{
    /// <summary>
    /// Classe qui gère les INotifyPropertyChanged
    /// </summary>
    public class ViewModelDocumentBase : INotifyPropertyChanged
    {
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

