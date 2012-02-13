using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Utils.Wcf
{
    /// <summary>
    /// Permet de spécifier des paramétrages spécifiques pour un appel Wcf donné.
    /// Toutes les informations sont optionnelles, un champ null laisse les valeurs par défaut.
    /// </summary>
    public class ConfigurationWcf
    {
        /// <summary>
        /// La configuration WCF vide : rien n'est modifié.
        /// </summary>
        private static readonly ConfigurationWcf defaut = new ConfigurationWcf();

        /// <summary>
        /// Gets la configuration par défaut pour le WCF.
        /// </summary>
        public static ConfigurationWcf Defaut
        {
            get
            {
                return defaut;
            }
        }

        /// <summary>
        /// Gets le nom du endpoint à utiliser.
        /// </summary>
        public string NomEndpoint { get; private set; }

        #region Constructeurs

        /// <summary>
        /// Constructeur.
        /// </summary>
        public ConfigurationWcf()
        {
            this.NomEndpoint = "*";
        }

        /// <summary>
        /// Constructeur de copie.
        /// </summary>
        /// <param name="nomEndpoint">Nom du endpoint.</param>
        public ConfigurationWcf(string nomEndpoint)
            : this()
        {
            this.NomEndpoint = nomEndpoint;
        }

        #endregion

        /// <summary>
        /// Modifie le endpoint.
        /// </summary>
        /// <param name="nomEndpoint">Valeur du endpoint.</param>
        /// <returns>La nouvelle version de configuration WCF.</returns>
        public ConfigurationWcf SetNomEndpoint(string nomEndpoint)
        {
            return new ConfigurationWcf(nomEndpoint);
        }
    }
}
