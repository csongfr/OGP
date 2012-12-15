using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OGP.ServicePlugins.Modele;
using OGP.ServicePlugins;
using System.IO;


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

    }
}
