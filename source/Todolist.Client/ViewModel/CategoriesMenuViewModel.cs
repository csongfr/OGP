using System;
using Cinch;
using Plugin.Todolist.ValueObjects;

namespace Todolist.Client.ViewModel
{
    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class CategoriesMenuViewModel : ViewModelBase
    {
        #region Membres privés

        /// <summary>
        /// Catégorie
        /// </summary>
        private VOCategorie categorie;

        /// <summary>
        /// Nom de la catégorie
        /// </summary>
        private string nomCategorie;

        #endregion

        #region Propriétés de présentation

        /// <summary>
        /// Cinch : INPC helper.
        /// </summary>
        private static System.ComponentModel.PropertyChangedEventArgs categorieChangeArgs = Utils.Mvvm.ObservableHelper.CreateArgs<CategoriesMenuViewModel>(x => x.Categorie);

        /// <summary>
        /// Gets et sets du VOCategorie
        /// </summary>
        public VOCategorie Categorie
        {
            get
            {
                return this.categorie;
            }
            set
            {
                if (this.categorie == value)
                {
                    return;
                }

                this.categorie = value;

                NotifyPropertyChanged(categorieChangeArgs);
            }
        }

        /// <summary>
        /// Cinch : INPC helper.
        /// </summary>
        private static System.ComponentModel.PropertyChangedEventArgs nomCategorieChangeArgs = Utils.Mvvm.ObservableHelper.CreateArgs<CategoriesMenuViewModel>(x => x.NomCategorie);

        /// <summary>
        /// Gets et sets du nom de la catégorie
        /// </summary>
        public string NomCategorie
        {
            get
            {
                return this.nomCategorie;
            }
            set
            {
                if (this.nomCategorie == value)
                {
                    return;
                }

                if (this.nomCategorie == null)
                {
                    OnNouvelleCategorie(value);
                }
                else
                {
                    OnModifCategorie(nomCategorie, value);
                }

                this.nomCategorie = value;

                // OnCategorieMenuChanged();
                NotifyPropertyChanged(nomCategorieChangeArgs);   
            }
        }

        #endregion

        #region Events

        /// <summary>
        /// Evénement levé
        /// </summary>
        public event Action<string, string> ModifCategorie;

        /// <summary>
        /// Déclenche l'événement ModifCategorie
        /// </summary>
        private void OnModifCategorie(string ancienneValeur, string nouvelleValeur)
        {
            var handler = ModifCategorie;
            if (handler != null)
            {
                handler(ancienneValeur, nouvelleValeur);
            }
        }

        /// <summary>
        /// Evénement levé
        /// </summary>
        public event Action<string> NouvelleCategorie;

        /// <summary>
        /// Déclenche l'événement NouvelleCategorie
        /// </summary>
        private void OnNouvelleCategorie(string valeur)
        {
            var handler = NouvelleCategorie;
            if (handler != null)
            {
                handler(valeur);
            }
        }

        #endregion

        #region Constructeur

        /// <summary>
        /// Constructeur par défaut
        /// </summary>
        public CategoriesMenuViewModel()
        {
        }

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="categorie">VOCategorie</param>
        public CategoriesMenuViewModel(VOCategorie categorie)
        {
            Categorie = categorie;
            NomCategorie = categorie.Nom;
        }

        #endregion
    }
}
