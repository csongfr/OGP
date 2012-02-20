using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OGP.ClientWpf
{
    /// <summary>
    /// Classe qui gère les exceptions du client
    /// </summary>
    [System.Serializable]
    public class OgpClientCoreException : System.ApplicationException
    {
        /// <summary>
        /// Constructeur par défaut
        /// </summary>
        public OgpClientCoreException()
        {
        }

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="message"> message d'erreur</param>
        public OgpClientCoreException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="message">message d'erreur</param>
        /// <param name="inner">gère un autre type d'exception</param>
        public OgpClientCoreException(string message, System.Exception inner)
            : base(message, inner)
        {
        }

        /// <summary>
        /// Exception client
        /// </summary>
        /// <param name="info">SerializationInfo</param>
        /// <param name="context">StreamingContext</param>
        protected OgpClientCoreException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
            : base(info, context)
        {
        }
    }
}