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

       public string DownloadDossier
       {
           get
           {
               return ConfigHelper.RetrieveConfig("OGP.ClientWpf", "plugins", "downloadDossier");
           }
       }

       public string LocalDossier
       {
           get
           {
               return ConfigHelper.RetrieveConfig("OGP.ClientWpf", "plugins", "localDossier");
           }
       }

       public string TmpDossier
       {
           get
           {
               return ConfigHelper.RetrieveConfig("OGP.ClientWpf", "plugins", "tmpDossier");
           }
       }
   }
}
