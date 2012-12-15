using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Utils.Config;

namespace OGP.ClientWpf
{
   public sealed class AppConfig
   {
       #region Singleton

       private static readonly AppConfig instance = new AppConfig();

       public static AppConfig Instance
       {
           get
           {
               return instance;
           }
       }

       /// <summary>
       /// Singleton.
       /// </summary>
       private AppConfig()
       {

       }

       #endregion

       public string RepertoirePluginsSynchro
       {
           get
           {
               return ConfigHelper.RetrieveConfig("OGP.ClientWpf", "plugins", "repertoirePluginsSynchro");
           }
       }

       public string RepertoirePluginsLocal
       {
           get
           {
               return ConfigHelper.RetrieveConfig("OGP.ClientWpf", "plugins", "repertoirePluginsLocal");
           }
       }
   }
}
