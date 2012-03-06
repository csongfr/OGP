using System;
﻿using System.Collections.Specialized;
using System.Configuration;

namespace Utils.Config
{
    /// <summary>
    /// Class to handle configuration files
    /// </summary>
    public static class ConfigHelper
    {
        /// <summary>
        /// Récupère une collection d'entrées dans le fichier de configuration courant.
        /// </summary>
        /// <param name="sectionGroupName">Nom du groupe de section.</param>
        /// <param name="sectionName">Nom de la section</param>
        /// <returns>Valeur de l'entrée.</returns>
        public static NameValueCollection RetrieveConfig(string sectionGroupName, string sectionName)
        {
            try
            {
                string fullSectionName = string.IsNullOrEmpty(sectionGroupName) ? sectionName : sectionGroupName + "/" + sectionName;
                NameValueCollection nameValueCollectionChaine = (NameValueCollection)ConfigurationManager.GetSection(fullSectionName);
                return nameValueCollectionChaine;
            }
            catch (Exception ex)
            {
                throw new ConfigException(sectionGroupName, sectionName, ex);
            }
        }

        /// <summary>
        /// Récupère une entrée dans le fichier de configuration courant.
        /// </summary>
        /// <param name="sectionGroupName">Nom du groupe de section.</param>
        /// <param name="sectionName">Nom de la section</param>
        /// <param name="keyName">Nom de l'entrée.</param>
        /// <returns>Valeur de l'entrée.</returns>
        public static string RetrieveConfig(string sectionGroupName, string sectionName, string keyName)
        {
            string retour;
            NameValueCollection nameValueCollection = RetrieveConfig(sectionGroupName, sectionName);

            try
            {
                retour = nameValueCollection[keyName];
            }
            catch (Exception ex)
            {
                throw new ConfigException(sectionGroupName, sectionName, keyName, ex);
            }

            if (retour == null)
            {
                throw new ConfigException(sectionGroupName, sectionName, keyName);
            }

            return retour;
        }

        /// <summary>
        /// Convertit une section de .config lue localement en une NameValueCollection classique.
        /// </summary>
        /// <param name="config"></param>
        /// <param name="sectionName"></param>
        /// <returns></returns>
        public static NameValueCollection GetNameValueCollectionSection(Configuration config, string sectionName)
        {
            System.Xml.XmlDocument xDoc = new System.Xml.XmlDocument();
            NameValueCollection nRet = new NameValueCollection();

            string sXml = config.GetSection(sectionName).SectionInformation.GetRawXml();
            xDoc.LoadXml(sXml);

            System.Xml.XmlNode xList = xDoc.ChildNodes[0];
            foreach (System.Xml.XmlNode xNodo in xList)
            {
                nRet.Add(xNodo.Attributes[0].Value, xNodo.Attributes[1].Value);
            }

            return nRet;
        }
    }
}
