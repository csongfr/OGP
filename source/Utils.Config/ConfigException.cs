using System;

namespace Utils.Config
{
    /// <summary>
    /// Exception levé lorsqu'un soucis survient avec la lecture du fichier de config.
    /// </summary>
    public class ConfigException : UtilsApplicationException
    {
        /// <summary>
        /// Initializes a new instance of the System.SystemException class with a specified error message.
        /// </summary>
        /// <param name="sectionGroupName">Nom du groupe de section.</param>
        /// <param name="sectionName">Nom de la section</param>
        public ConfigException(string sectionGroupName, string sectionName)
            : this(sectionGroupName, sectionName, (Exception)null)
        {
          
        }

         /// <summary>
        /// Initializes a new instance of the System.SystemException class with a specified error message.
        /// </summary>
        /// <param name="sectionGroupName">Nom du groupe de section.</param>
        /// <param name="sectionName">Nom de la section</param>
        /// <param name="innerException">Inner Exception</param>
        public ConfigException(string sectionGroupName, string sectionName, Exception innerException)
            : base(String.Format("Section introuvable dans le fichier de configuration : '{0}'/'{1}'.", sectionGroupName, sectionName), innerException)
        {
            Data.Add("sectionName", sectionName);
            Data.Add("sectionGroupName", sectionGroupName);
        }

        /// <summary>
        /// Initializes a new instance of the System.SystemException class with a specified error message.
        /// </summary>
        /// <param name="sectionGroupName">Nom du groupe de section.</param>
        /// <param name="sectionName">Nom de la section</param>
        /// <param name="keyName">Nom de l'entrée.</param>
        public ConfigException(string sectionGroupName, string sectionName, string keyName)
            : this(sectionGroupName, sectionName, keyName, null)
        {
        }

           /// <summary>
        /// Initializes a new instance of the System.SystemException class with a specified error message.
        /// </summary>
        /// <param name="sectionGroupName">Nom du groupe de section.</param>
        /// <param name="sectionName">Nom de la section</param>
        /// <param name="keyName">Nom de l'entrée.</param>
        /// <param name="innerException">Inner Exception</param>
        public ConfigException(string sectionGroupName, string sectionName, string keyName, Exception innerException)
            : base(String.Format("Clé '{0}' introuvable dans la section '{1}'/'{2}' du fichier de configuration.", keyName, sectionGroupName, sectionName), innerException)
        {
            Data.Add("keyName", keyName);
            Data.Add("sectionName", sectionName);
            Data.Add("sectionGroupName", sectionGroupName);
        }
    }
}
