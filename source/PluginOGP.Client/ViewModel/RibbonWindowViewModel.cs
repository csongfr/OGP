using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cinch;
using OGP.ClientWpf.View;
using OGP.Plugin.Interfaces;
using PluginOGP.Client.View;

namespace PluginOGP.Client.ViewModel
{
      
    class RibbonWindowViewModel : ViewModelBase
    {
        #region constant names
        private const string LOCAL_DOCK_TITLE = "Installed plugins";
        private const string SERVER_DOCK_TITLE = "Available plugins in server";
        #endregion

        #region private components

        private DocumentDock localDockInstance = null;
        private DocumentDock serverDockInstance = null;

        /// <summary>
        /// Commande qui affiche les plugins locaux
        /// </summary>
        private SimpleCommand openLocalCommand = null;
        /// <summary>
        /// Commande qui affiche les plugins disponibles sur serveur
        /// </summary>
        private SimpleCommand openServerCommand = null;
        /// <summary>
        /// Commande qui permet de s'identifier
        /// </summary>
        private SimpleCommand loginCommand = null;

        #endregion

        #region properties

        /// <summary>
        /// Commande qui ouvre la liste de plugins installes
        /// </summary>
        public SimpleCommand OpenLocalCommand
        {
            get
            {
                if (openLocalCommand == null)
                {
                    openLocalCommand = new SimpleCommand
                    {
                        ExecuteDelegate = delegate
                        {
                            openLocal();
                        }
                    };
                }
                return openLocalCommand;
            }
        }

        /// <summary>
        /// Commande qui affiche les plugins disponible a distance
        /// </summary>
        public SimpleCommand OpenServerCommand
        {
            get
            {
                if (openServerCommand == null)
                {
                    openServerCommand = new SimpleCommand
                    {
                        ExecuteDelegate = delegate
                        {
                            openServer();
                        }
                    };
                }
                return openServerCommand;
            }
        }

        #endregion



        #region Actions

        private void openLocal()
        {
            if (localDockInstance == null)
            {
                var addDoc = ServiceProvider.Resolve<ICentralOnglets>();
                var doc = new DocumentDock(LOCAL_DOCK_TITLE);
                addDoc.AjoutOnglet(doc);
                localDockInstance = doc;
            }
            localDockInstance.Activate();
        }

        private void openServer()
        {
            if (serverDockInstance == null)
            {
                var addDoc = ServiceProvider.Resolve<ICentralOnglets>();
                var doc = new DocumentDock(SERVER_DOCK_TITLE);
                addDoc.AjoutOnglet(doc);
                serverDockInstance = doc;
            }
            serverDockInstance.Activate();
        }

        #endregion

        #region Constructeur
        public RibbonWindowViewModel()
        {
        }
        #endregion

    }

}
