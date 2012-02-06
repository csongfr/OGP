using System;
using System.Windows;


namespace Cinch
{
    /// <summary>
    /// This interface defines a interface that will allow 
    /// a ViewModel to save a file
    /// </summary>
    public interface ISaveFileService
    {
        /// <summary>
        /// FileName
        /// </summary>
        Boolean OverwritePrompt { get; set; }

        /// <summary>
        /// FileName
        /// </summary>
        String FileName { get; set; }

        /// <summary>
        /// Filter
        /// </summary>
        String Filter { get; set; }

        /// <summary>
        /// Filter
        /// </summary>
        String InitialDirectory { get; set; }

        /// <summary>
        /// This method should show a window that allows a file to be saved
        /// </summary>
        /// <returns>A bool from the ShowDialog call</returns>
        bool? ShowDialog();
    }
}
