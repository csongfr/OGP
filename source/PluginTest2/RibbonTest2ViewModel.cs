using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using AvalonDock;
using Cinch;
using Fluent;

namespace PluginTest2
{
    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class RibbonTest2ViewModel : ViewModelBase
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
                           NouvelOnglet();
                        }
                    };
                }
                return commandeOuvrirFichier;
            }
        }

        #endregion

        #region Constructeur

        /// <summary>
        /// Constructeur
        /// </summary>
        private void NouvelOnglet()
        {
        }

        #endregion

        internal void Initialize()
        {
            // nothing
        }
    }
}
