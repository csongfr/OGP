using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using AvalonDock;
using Cinch;
using Fluent;

namespace MyPlugin
{
    public class EmptyPluginViewModel : ViewModelBase
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

        #region Onglets

        /// <summary>
        /// Nouvel onglets
        /// </summary>
        private void NouvelOnglet()
        {
        }

        #endregion
    }
}
