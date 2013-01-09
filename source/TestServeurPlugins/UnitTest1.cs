using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Utils.Wcf;
using System.IO;
using System.Collections.Generic;
using OGP.ServicePlugin;
using OGP.ServicePlugin.Modele;
using OGP.ServicePlugin.DAL;
using System.Windows.Forms;

namespace TestServeurPlugins
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            ServeurPlugins sp = new ServeurPlugins();

            PluginModel p = new PluginModel("PluginTest3", "Test3", "7.1.3.5", "Ceci est un test", "/test", true);
            
            sp.AddPlugin(p, new MemoryStream());
        }

        [TestMethod]
        public void TestMethod2()
        {
            Boolean b = true;
            var erreur = WcfHelper.Execute<IServicePlugin>(client =>
            {
                b = false;
                IList<PluginModel> l = client.GetPluginList();
                Assert.AreEqual(l.Count, 2);
            });

            if (!b)
            {
                Assert.AreEqual(new ArrayTypeMismatchException(), erreur.Message);
            }
            if (erreur != null)
            {
                Console.WriteLine("Non null");
                //Assert.AreEqual(new ArrayTypeMismatchException(), erreur.Message); 
            }
        }
        [TestMethod]
        public void TestDownloadPlugin() {
            ServeurPlugins sp = new ServeurPlugins();

            PluginModel p = new PluginModel("PluginTest3", "Test3", "7.1.3.5", "Ceci est un test", "/test", true);
            IList<String> l = new List<String>();
            l.Add("totovaalaplage");
            File.WriteAllLines("testDownload", l);
            MemoryStream initStream = File_DAL.getPlugin("testDownload");
            sp.AddPlugin(p, initStream);

            Boolean b = true;
            var erreur = WcfHelper.Execute<IServicePlugin>(client =>
            {
                b = false;
                MemoryStream stream = client.DownloadPlugin("PluginTest3_7.1.3.5");
                Assert.AreEqual(initStream.Length, stream.Length);
                int count = 0;
                while (count < stream.Length) {
                    Assert.AreEqual(initStream.ReadByte(), stream.ReadByte());
                    count++;
                }
            });
           
        }

    }
}
