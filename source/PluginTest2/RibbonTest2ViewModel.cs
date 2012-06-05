using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cinch;
using Fluent;
using System.Windows;
using AvalonDock;

namespace PluginTest2
{
    
    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class RibbonTest2ViewModel : ViewModelBase
    {
        private string nom;
        /// <summary>
        /// Cinch : INPC helper.
        /// </summary>
        private static System.ComponentModel.PropertyChangedEventArgs nomChangeArgs = Utils.Mvvm.ObservableHelper.CreateArgs<RibbonTest2ViewModel>(x => x.Nom);

        public string Nom
        {
            get
            {
                return this.nom;
            }
            set
            {
                if (this.nom == value)
                {
                    return;
                }

                this.nom = value;

                NotifyPropertyChanged(nomChangeArgs);
            }
        }

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

        private void NouvelOnglet()
        {

        }

         public RibbonTest2ViewModel()
         {
             Nom = "Ribbon";
         }
    }
}
