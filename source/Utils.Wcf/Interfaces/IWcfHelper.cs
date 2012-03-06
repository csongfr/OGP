using System;

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
        /// <param name="action">Code … executer.</param>
        /// <returns>Si une exception a eu lieu, on la catche et on la retourne.</returns>
        Exception Execute<T>(Action<T> action);

        /// <summary>
        /// Cree et gere la vie d'une connexion WCF, même en cas d'exception.
        /// </summary>
        /// <typeparam name="T">Interface du service consomme.</typeparam>
        /// <param name="action">Code … executer.</param>
        /// <param name="configurationWcf">Configuration de l'appel WCF.</param>
        /// <returns>Si une exception a eu lieu, on la catche et on la retourne.</returns>
        Exception Execute<T>(Action<T> action, ConfigurationWcf configurationWcf);
    }
}
