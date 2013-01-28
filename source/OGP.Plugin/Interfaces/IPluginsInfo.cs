using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuantumBitDesigns.Core;
using OGP.ServicePlugin.Modele;

namespace OGP.Plugin.Interfaces
{
    public enum DirectoryType
    {
        Local,
        Download,
        Tmp
    }

    /// <summary>
    /// Interface qui permet de communiquer l'information de la liste de plugins entre plugin OGP et MainViewModel
    /// </summary>
    public interface IPluginsInfo
    {
        void RefreshMenu();
        IEnumerable<PluginModel> GetPluginsInfo();
        string GetPluginsDirectory(DirectoryType type);
    }
}
