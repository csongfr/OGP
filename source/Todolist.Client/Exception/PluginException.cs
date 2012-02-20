using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Todolist.Client
{
    /// <summary>
    /// Classe qui gère les exceptions du client
    /// </summary>
    [System.Serializable]
    public class TodolistPluginException : System.ApplicationException
    {
        /// <summary>
        /// Constructeur par défaut
        /// </summary>
        public TodolistPluginException()
        {
        }

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="message"> message d'erreur</param>
        public TodolistPluginException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="message">message d'erreur</param>
        /// <param name="inner">gère un autre type d'exception</param>
        public TodolistPluginException(string message, System.Exception inner)
            : base(message, inner)
        {
        }

        /// <summary>
        /// Exception client
        /// </summary>
        /// <param name="info">SerializationInfo</param>
        /// <param name="context">StreamingContext</param>
        protected TodolistPluginException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
            : base(info, context)
        {
        }
    }
}