using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cinch;
using System.ComponentModel.Composition;
using System.Collections.ObjectModel;
using AvalonDock;
using OGP.Plugin.Interfaces;
using OGP.ClientWpf.ViewModel;
using OGP.ClientWpf.Interfaces;


namespace PluginTest
{


    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class RibbonTest1ViewModel : ViewModelBase
    {
        private string nom;
        /// <summary>
        /// Cinch : INPC helper.
        /// </summary>
        private static System.ComponentModel.PropertyChangedEventArgs nomChangeArgs = Utils.Mvvm.ObservableHelper.CreateArgs<RibbonTest1ViewModel>(x => x.Nom);

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
                            Ouvrir();
                        }
                    };
                }
                return commandeOuvrirFichier;
            }
        }

        public void Ouvrir()
        {
           var methodeOuvrirNouvelOnglet = ServiceProvider.Resolve<ICentralOnglets>();
            methodeOuvrirNouvelOnglet.NouvelOnglet(Nom);
        }

        public RibbonTest1ViewModel()
        {
            Nom = "Ribbon2";
        }
    }
}
