using System;
using System.Collections.Generic;
using System.Windows;
using Cinch.Services.Service_Interfaces.Lmj;
using Microsoft.Win32;


namespace Cinch
{
    /// <summary>
    /// This class implements the IOpenFileService for WPF purposes.
    /// </summary>
    public class WPFOpenFileService : IOpenFileService
    {
        #region Data

        /// <summary>
        /// Embedded OpenFileDialog to pass back correctly selected
        /// values to ViewModel
        /// </summary>
        private OpenFileDialog ofd = new OpenFileDialog();

        #endregion

        #region IOpenFileService Members
        /// <summary>
        /// This method should show a window that allows a file to be selected
        /// </summary>
        /// <returns>A bool from the ShowDialog call</returns>
        public bool? ShowDialog()
        {
            IUiDispatcher uiDispatcher = ViewModelBase.ServiceProvider.Resolve<IUiDispatcher>();

            bool? retour = null;

            uiDispatcher.InvokeIfRequired(() =>
                {
                    //Set embedded OpenFileDialog.Filter
                    if (!String.IsNullOrEmpty(this.Filter))
                        ofd.Filter = this.Filter;

                    //Set embedded OpenFileDialog.InitialDirectory
                    if (!String.IsNullOrEmpty(this.InitialDirectory))
                        ofd.InitialDirectory = this.InitialDirectory;

                    //return results
                    retour = ofd.ShowDialog(Application.Current.MainWindow);
                });

            return retour;
        }

        /// <summary>
        /// FileName : Simply use embedded OpenFileDialog.FileName
        /// But DO NOT allow a Set as it will ONLY come from user
        /// picking a file
        /// </summary>
        public string FileName
        {
            get { return ofd.FileName; }
            set
            {
                //Do nothing
            }
        }

        /// <summary>
        /// Filter : Simply use embedded OpenFileDialog.Filter
        /// </summary>
        public string Filter
        {
            get { return ofd.Filter; }
            set { ofd.Filter = value; }
        }

        /// <summary>
        /// Filter : Simply use embedded OpenFileDialog.InitialDirectory
        /// </summary>
        public string InitialDirectory
        {
            get { return ofd.InitialDirectory; }
            set { ofd.InitialDirectory = value; }
        }

        #endregion
    }
}
