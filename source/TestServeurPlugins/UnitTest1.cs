using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OGP.ServicePlugins.Modele;
using OGP.ServicePlugins;

namespace TestServeurPlugins
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            ServeurPlugins sp = new ServeurPlugins();

            Plugin p = new Plugin("PluginTest2", "Test", "7.1.3.5", "Ceci est un test", "/test");
            
            sp.addPlugin(p);
        }
    }
}
