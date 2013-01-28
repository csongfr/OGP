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

       public string DownloadDirectory
       {
           get
           {
               return ConfigHelper.RetrieveConfig("OGP.ClientWpf", "plugins", "downloadDirectory");
           }
       }

       public string LocalDirectory
       {
           get
           {
               return ConfigHelper.RetrieveConfig("OGP.ClientWpf", "plugins", "localDirectory");
           }
       }

       public string TmpDirectory
       {
           get
           {
               return ConfigHelper.RetrieveConfig("OGP.ClientWpf", "plugins", "tmpDirectory");
           }
       }
   }
}
