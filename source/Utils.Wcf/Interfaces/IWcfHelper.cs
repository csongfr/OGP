using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Utils.Wcf
{
    /// <summary>
    /// Interface d'appel WCF.
    /// </summary>
    public interface IWcfHelper
    {
        /// <summary>
        /// Cree et gere la vie d'une connexion WCF, même en cas d'exception.
        /// </summary>
        /// <typeparam name="T">Interface du service consomme.</typeparam>
        /// <param name="application">Nom de l'application/service … contacter. Un groupe de section doit exister … ce nom dans le fichier de config.</param>
        /// <param name="action">Code … executer.</param>
        /// <returns>Si une exception a eu lieu, on la catche et on la retourne.</returns>
        Exception Execute<T>(string application, Action<T> action);

        /// <summary>
        /// Cree et gere la vie d'une connexion WCF, même en cas d'exception.
        /// </summary>
        /// <typeparam name="T">Interface du service consomme.</typeparam>
        /// <param name="application">Nom de l'application/service … contacter. Un groupe de section doit exister … ce nom dans le fichier de config.</param>
        /// <param name="action">Code … executer.</param>
        /// <param name="configurationWcf">Configuration de l'appel WCF.</param>
        /// <returns>Si une exception a eu lieu, on la catche et on la retourne.</returns>
        Exception Execute<T>(string application, Action<T> action, ConfigurationWcf configurationWcf);
    }
}
