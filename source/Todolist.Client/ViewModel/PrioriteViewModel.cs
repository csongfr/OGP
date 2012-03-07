using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using Cinch;

namespace Todolist.Client.ViewModel
{
    /// <summary>
    /// Classe qui gère la priorité d'une tache
    /// </summary>
    public class PrioriteViewModel : ViewModelBase
    {
      #region Membres privés

        /// <summary>
        /// Stocke la couleur de la tâche
        /// </summary>
        private Brush couleur;

        /// <summary>
        /// Stocke le nom de la priorité de la tâche
        /// </summary>
        private string texte;

        #endregion

        #region Propriétés de présentation

        /// <summary>
        /// Cinch : INPC helper.
        /// </summary>
        private static System.ComponentModel.PropertyChangedEventArgs texteChangeArgs = Utils.Mvvm.ObservableHelper.CreateArgs<PrioriteViewModel>(x => x.Texte);

        /// <summary>
        /// Gets et Sets d'une priorité d'une tâche
        /// </summary>
        public string Texte
        {
            get
            {
                return this.texte;
            }
            set
            {
                if (this.texte == value)
                {
                    return;
                }

                this.texte = value;

                NotifyPropertyChanged(texteChangeArgs);
            }
        }

        /// <summary>
        /// Cinch : INPC helper.
        /// </summary>
        private static System.ComponentModel.PropertyChangedEventArgs couleurChangeArgs = Utils.Mvvm.ObservableHelper.CreateArgs<PrioriteViewModel>(x => x.Couleur);

        /// <summary>
        /// Gets et Sets de la couleur d'une tâche
        /// </summary>
        public Brush Couleur
        {
            get
            {
                return this.couleur;
            }
            set
            {
                if (this.couleur == value)
                {
                    return;
                }

                this.couleur = value;

                NotifyPropertyChanged(couleurChangeArgs);
            }
        }

        #endregion
  
        #region Constructeur

        /// <summary>
        /// Constructeur par défaut
        /// </summary>
        public PrioriteViewModel()
        { 
        }

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="text">string</param>
        /// <param name="color">Brush</param>
        public PrioriteViewModel(string text, Brush color)
        {
            Texte = text;
            Couleur = color;
        }

        #endregion
    }
}
