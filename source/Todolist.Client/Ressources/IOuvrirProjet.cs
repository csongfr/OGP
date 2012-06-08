using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using Plugin.Todolist.ValueObjects;

namespace Todolist.Client.Ressources
{
    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public interface IOuvrirProjet
    {
        void InterfaceOuvrirProjet(ObservableCollection<VOTache> taches);
    }
}
