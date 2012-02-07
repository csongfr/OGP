using System;
using System.ComponentModel;
using System.ServiceModel;

namespace Utils.Wcf
{
    /// <summary>
    /// Fournit une implémentation propre pour l'utilisation d'un service WCF.
    /// </summary>
    /// <remarks>
    /// Sert essentiellement de passe-plat et de factory pour les instances de WcfHelper.
    /// </remarks>
    public static class WcfHelper
    {
        /// <summary>
        /// Stocke l'instance par défaut de WcfHelper.
        /// </summary>
        private static readonly IWcfHelper defaultInstance = new BasicWcfHelper();

        /// <summary>
        /// Stocke l'instance principale de WcfHelper. 
        /// </summary>
        private static IWcfHelper instance;

        /// <summary>
        /// Constructeur statique.
        /// </summary>
        static WcfHelper()
        {
            instance = defaultInstance;
        }

        /// <summary>
        /// Retourne l'instance par défaut de WcfHelper.
        /// </summary>
        /// <returns>Instance de IWcfHelper.</returns>
        public static IWcfHelper GetDefaultInstance()
        {
            return defaultInstance;
        }

        /// <summary>
        /// Permet de fournir une implémentation différente de gestionnaire d'appel WCF.
        /// </summary>
        /// <param name="helper">Implémentation d'helper wcf.</param>
        public static void SetHelper(IWcfHelper helper)
        {
            if (helper == null)
            {
                throw new ArgumentNullException("helper");
            }

            instance = helper;
        }

        /// <summary>
        /// Gets l'instance courante du helper.
        /// </summary>
        /// <returns>Instance.</returns>
        public static IWcfHelper GetHelper()
        {
            return instance;
        }

        /// <summary>
        /// Cree et gere la vie d'une connexion WCF, mˆme en cas d'exception.
        /// </summary>
        /// <typeparam name="T">Interface du service consomme.</typeparam>
        /// <param name="application">Nom de l'application/service … contacter. Un groupe de section doit exister … ce nom dans le fichier de config.</param>
        /// <param name="action">Code … executer.</param>
        /// <returns>Si une exception a eu lieu, on la catche et on la retourne.</returns>
        public static Exception Execute<T>(string application, Action<T> action)
        {
            return instance.Execute<T>(application, action);
        }

        /// <summary>
        /// Cree et gere la vie d'une connexion WCF, mˆme en cas d'exception.
        /// </summary>
        /// <typeparam name="T">Interface du service consomme.</typeparam>
        /// <param name="application">Nom de l'application/service … contacter. Un groupe de section doit exister … ce nom dans le fichier de config.</param>
        /// <param name="action">Code … executer.</param>
        /// <param name="configurationWcf">Configuration de l'appel WCF.</param>
        /// <returns>Si une exception a eu lieu, on la catche et on la retourne.</returns>
        public static Exception Execute<T>(string application, Action<T> action, ConfigurationWcf configurationWcf)
        {
            return instance.Execute<T>(application, action, configurationWcf);
        }

        /// <summary>
        /// Exécute un appel WCF au sein d'un BackgroundWorker.
        /// </summary>
        /// <typeparam name="T">Interface du service consomme.</typeparam>
        /// <param name="application">Nom de l'application/service à contacter. Un groupe de section doit exister à ce nom dans le fichier de config.</param>
        /// <param name="action">Code à exécuter dans le BackgroundWorker.</param>
        /// <param name="actionCompletedHandler">Code à exécuter une fois le BackgroundWorker terminé. Prend en argument l'exception qui est éventuellement survenue.</param>
        public static void Execute<T>(string application, Action<T> action, Action<Exception> actionCompletedHandler)
        {
            var exception = instance.Execute<T>(application, action);
            actionCompletedHandler(exception);
        }

        /// <summary>
        /// Exécute un appel WCF au sein d'un BackgroundWorker.
        /// </summary>
        /// <typeparam name="T">Interface du service consomme.</typeparam>
        /// <param name="application">Nom de l'application/service à contacter. Un groupe de section doit exister à ce nom dans le fichier de config.</param>
        /// <param name="action">Code à exécuter dans le BackgroundWorker.</param>
        /// <param name="actionCompletedHandler">Code à exécuter une fois le BackgroundWorker terminé. Prend en argument l'exception qui est éventuellement survenue.</param>
        /// <param name="configurationWcf">Configuration de l'appel WCF.</param>
        public static void Execute<T>(string application, Action<T> action, Action<Exception> actionCompletedHandler, ConfigurationWcf configurationWcf)
        {
            var exception = instance.Execute<T>(application, action, configurationWcf);
            actionCompletedHandler(exception);
        }
    }
}

