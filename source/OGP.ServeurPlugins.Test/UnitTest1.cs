using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Utils.Wcf;
using System.IO;
using System.Collections.Generic;
using OGP.ServicePlugin;
using OGP.ServicePlugin.Modele;
using OGP.ServicePlugin.DAL;
using System.Windows.Forms;

namespace OGP.ServeurPlugins.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            ServeurPlugin sp = new ServeurPlugin();

            PluginModel p = new PluginModel("PluginTest3", "Test3", "7.1.3.5", "Ceci est un test", "/test", true);
            IList<String> l = new List<String>();
            l.Add("contenudufichier");
            File.WriteAllLines("Test3", l);
            sp.AddPlugin(p, File_DAL.getPlugin("Test3"));
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
        public void TestDownloadPlugin()
        {
            ServeurPlugin sp = new ServeurPlugin();

            PluginModel p = new PluginModel("id", "Test3", "7.1.3.5", "Ceci est un test", "/test", true);
            IList<String> l = new List<String>();
            l.Add("totovaalaplage");
            File.WriteAllLines("testDownload", l);
            MemoryStream initStream = File_DAL.getPlugin("testDownload");
            sp.AddPlugin(p, initStream);

            MemoryStream stream = sp.DownloadPlugin("Test3_7.1.3.5");
            Assert.AreEqual(initStream.Length, stream.Length);
            int count = 0;
            while (count < stream.Length)
            {
                Assert.AreEqual(initStream.ReadByte(), stream.ReadByte());
                count++;
            }

        }

    }
}
