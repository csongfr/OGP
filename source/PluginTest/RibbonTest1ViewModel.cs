using System;
using Cinch;
using OGP.Plugin.Interfaces;

namespace PluginTest
{
    /// <summary>
    /// Plugin de test
    /// </summary>
    public class RibbonTest1ViewModel : ViewModelBase
    {
        #region Commandes

        /// <summary>
        /// Stocke la commande qui fait le café.
        /// </summary>
        private SimpleCommand commandeOuvrirFichier;

        /// <summary>
        /// Gets ou sets la commande qui fait le café.
        /// </summary>
        public SimpleCommand CommandeOuvrirFichier
        {
            get
            {
                if (commandeOuvrirFichier == null)
                {
                    commandeOuvrirFichier = new SimpleCommand
                    {
                        ExecuteDelegate = delegate
                        {
                            Ouvrir();
                        }
                    };
                }
                return commandeOuvrirFichier;
            }
        }

        #endregion

        #region Méthodes privées

        /// <summary>
        /// Méthode qui permet d'ouvrir le plugin
        /// </summary>
        private void Ouvrir()
        {
           var methodeOuvrirNouvelOnglet = ServiceProvider.Resolve<ICentralOnglets>();
            UserControl2 docs = new UserControl2();
            docs.Title = DateTime.Now.ToString();
            methodeOuvrirNouvelOnglet.AddPlugin(docs);
        }

        #endregion

        #region Constructeur

        /// <summary>
        /// Constructeur par défaut
        /// </summary>
        public RibbonTest1ViewModel()
        {
        }

        #endregion
    }
}
