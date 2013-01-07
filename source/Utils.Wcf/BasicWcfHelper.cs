using System;
using System.ServiceModel;

namespace Utils.Wcf
{
    /// <summary>
    /// Implémentation de base d'une connexion WCF.
    /// </summary>
    public class BasicWcfHelper : IWcfHelper
    {
        #region Méthodes publiques

        /// <summary>
        /// Cree et gere la vie d'une connexion WCF, même en cas d'exception.
        /// </summary>
        /// <typeparam name="T">Interface du service consomme.</typeparam>
        /// <param name="action">Code … executer.</param>
        /// <returns>Si une exception a eu lieu, on la catche et on la retourne.</returns>
        public Exception Execute<T>(Action<T> action)
        {
            return Execute<T>(action, ConfigurationWcf.Defaut);
        }

        /// <summary>
        /// Cree et gere la vie d'une connexion WCF, même en cas d'exception.
        /// </summary>
        /// <typeparam name="T">Interface du service consomme.</typeparam>
        /// <param name="action">Code … executer.</param>
        /// <param name="configurationWcf">Configuration de l'appel WCF.</param>
        /// <returns>Si une exception a eu lieu, on la catche et on la retourne.</returns>
        public Exception Execute<T>(Action<T> action, ConfigurationWcf configurationWcf)
        {
            
           if (configurationWcf == null)
            {
                throw new ArgumentNullException("La configuration WCF doit recevoir au minimum ConfigurationWcf.None");
            }

            // Les try ... catch garantissent la fermeture correcte du canal WCF, et assurent que l'appel distant ne bloquera pas l'application en retournant simplement l'exception.
            try
            {
                // Creation du proxy.
                var factory = CreateFactory<T>(configurationWcf);

                try
                {
                    BeforeAction<T>(factory);

                    if (action != null)
                    {
                        LaunchAction<T>(factory, action);
                    }
                }
                catch (Exception ex)
                {
                    // Quelque soit l'exception, il faut fermer le canal.
                    factory.Abort();

                    return ex;
                }
                finally
                {
                    factory = null;
                }
            }
            catch (Exception ex)
            {
                return ex;
            }

            return null;
        }

        #endregion

        #region Méthodes protégées

        /// <summary>
        /// Utilitaire de creation d'une fabrique qui cree des canaux
        /// </summary>
        /// <typeparam name="T">Interface du service consomme.</typeparam>
        /// <param name="configurationWcf">Configuration WCF spécifique à appliquer.</param>
        /// <returns>Fabrique de canaux</returns>
        protected virtual ChannelFactory<T> CreateFactory<T>(ConfigurationWcf configurationWcf)
        {
            return new ChannelFactory<T>(configurationWcf.NomEndpoint);
        }

        /// <summary>
        /// Appelé avant l'appel de l'action.
        /// </summary>
        /// <typeparam name="T">Type.</typeparam>
        /// <param name="factory">Factory de l'appel.</param>
        protected virtual void BeforeAction<T>(ChannelFactory<T> factory)
        {
        }

        /// <summary>
        /// Methode pour créer un canal de communication entre client et service
        /// </summary>
        /// <typeparam name="T">Interface du service consomme</typeparam>
        /// <param name="factory">fabrique accueillant les canaux</param>
        /// <param name="action">Code … executer.</param>
        protected virtual void LaunchAction<T>(ChannelFactory<T> factory, Action<T> action)
        {
            T client = factory.CreateChannel();

            // Execution du code.
            action(client);

            // On tente une fermeture propre du proxy.
            ((IClientChannel)client).Close();
            factory.Close();
        }

        #endregion
    }
}
