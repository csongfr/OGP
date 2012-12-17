using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OGP.ServicePlugin.Modele;
using OGP.ServicePlugin;
using System.IO;
using Utils.Wcf;


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
        public void TestMethode2()
        {
            var erreur = WcfHelper.Execute<IServicePlugin>(client =>
                {
                    client.GetPluginList();
                });
        }
    }
}
