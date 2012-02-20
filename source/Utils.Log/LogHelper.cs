using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Utils.Log
{
    /// <summary>
    /// Gère l'écriture des logs.
    /// </summary>
    public static class LogHelper
    {
        /// <summary>
        /// Loggue un message dans le système de log.
        /// </summary>
        /// <param name="logEntry">Entrée de log à écrire.</param>
        public static void Log(ILogEntry logEntry)
        {
            // TODO : obtenir l'instance de log auprès de Unity.
            // TODO : instance.Write(logEntry.GetStringMessage());
        }
    }
}
