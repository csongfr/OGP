// -----------------------------------------------------------------------
// <copyright file="IOuvrirProjet.cs" company="SopraGroup">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Todolist.Client.Ressources
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Collections.ObjectModel;
    using Plugin.Todolist.ValueObjects;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public interface IOuvrirProjet
    {
        void InterfaceOuvrirProjet(ObservableCollection<VOTache> taches);
    }
}
