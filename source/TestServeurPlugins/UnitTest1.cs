using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Utils.Wcf;
using System.IO;
using System.Collections.Generic;
using OGP.ServicePlugin;
using OGP.ServicePlugin.Modele;
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

    }
}
