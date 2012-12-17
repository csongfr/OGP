using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Utils.Wcf;
using OGP.ServicePlugins.Modele;
using OGP.ServicePlugins;
using System.IO;
using System.Collections.Generic;


namespace TestServeurPlugins
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            ServeurPlugins sp = new ServeurPlugins();

            PluginModel p = new PluginModel("PluginTest2", "Test", "7.1.3.5", "Ceci est un test", "/test", true);
            
            sp.AddPlugin(p, new MemoryStream());
        }

        [TestMethod]
        public void TestMethod2()
        {
            var erreur = WcfHelper.Execute<IServicePlugin>(client =>
            {
                IList<PluginModel> l = client.GetPluginList();
                Console.WriteLine("Toto\n");
                Console.WriteLine(l);
            });
        }

    }
}
