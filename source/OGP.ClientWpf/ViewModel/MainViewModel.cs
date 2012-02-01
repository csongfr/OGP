using System;
using System.Collections.Generic;
using System.Linq;
using OGP.ClientWpf.Comands;
using System.Windows.Input;
using System.ComponentModel;




namespace OGP.ClientWpf.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {                              
        #region Events

        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        // Raise PropertyChanged event
        void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion

        #region Membres privés       

        private RelayCommand exitCommand;

        #endregion
                             
        #region Commands

        #region Exit

        /// <summary>
        /// Exit from the application
        /// </summary>
        public ICommand ExitCommand
        {
            get
            {
                if (exitCommand == null)
                {
                    exitCommand = new RelayCommand(x => System.Windows.Application.Current.Shutdown());
                }
                return exitCommand;
            }
        }

        #endregion

        #endregion

        #region Constructeur

        /// <summary>
        /// Default constructor
        /// </summary>
        public MainViewModel() {
        
        }

        #endregion
    }
}
