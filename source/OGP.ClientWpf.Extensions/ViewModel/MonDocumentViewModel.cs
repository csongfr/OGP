using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using System.ComponentModel;
using System.Windows;
using OGP.Todolist.CommandsToDoList;
namespace OGP.ClientWpf.Extensions
{
    public class MonDocumentViewModel:INotifyPropertyChanged
    {

        
        
            public event PropertyChangedEventHandler PropertyChanged;

            protected virtual void OnPropertyChanged(string propertyName)
            {

                if (this.PropertyChanged != null)
                {
                    this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
                }
            }
        

        private RelayCommandToDoList _AjouterLigneCommande;
        private RelayCommandToDoList _FermerCommande;
        
        #region fonction

        /// <summary>
        ///  //La fonction a executer pour ajouter une ligne
        /// </summary>
       
        public void AjouterLigne(object param)
        {
   
        }

        //Le test a faire avant la fonction a executer
        public bool CanAjouterLigne(object param)
        {
            return true;
        }

        /// <summary>
        /// La fonction a executer pour fermer l'application todolist
        /// </summary>
        /// 

        public void Fermer(object param)
        {
            string reponse = (MessageBox.Show("Voulez vous Quitter ?", "Fermer ToDoList", MessageBoxButton.YesNo)).ToString();
            if (reponse == "Yes")
            {
                System.Windows.Application.Current.Shutdown();
            }
        }

        #endregion


        #region AjouterLigne 
        public ICommand AjouterLigneCommande
        {
            get
            {
               
                 if (this._AjouterLigneCommande == null)
                { 
                    //On lui appropri un DelegateCommande qui prend en parametre la fonction a executer et le tes a effeectuer avant d'executer la fonction
                    _AjouterLigneCommande = new RelayCommandToDoList(AjouterLigne);
                }
                return _AjouterLigneCommande;
            }
            
        }
        #endregion

        #region Fermer 
        public ICommand FermerToDoList
        {
            get
            {

                if (this._FermerCommande == null)
                {
                    //On lui appropri un DelegateCommande qui prend en parametre la fonction a executer et le tes a effeectuer avant d'executer la fonction
                    _FermerCommande = new RelayCommandToDoList(Fermer);
                }
                return _FermerCommande;
            }

        }
        #endregion

        #region Constructeur

        /// <summary>
        /// Default constructor
        /// </summary>
        public MonDocumentViewModel()
        {
        }
        #endregion
    }
}
