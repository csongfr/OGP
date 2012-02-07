using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Windows.Input;
using System.Windows.Threading;
using System.Diagnostics;
using System.Windows;

namespace OGP.Todolist.CommandsToDoList
{
    class RelayCommandToDoList : ICommand
    {
        //cette classe implementant l'interface ICommand va nous permette de binder des ICommand sur des bouton afin de déclencher des fonction contenu dans le ViewModel

        Func<object, bool> canExecute;
        Action<object> executeAction;


        public RelayCommandToDoList(Action<object> executeAction)
        {
            this.executeAction = executeAction;
        }

        //Methode qui sera appeler Sur un ICommand du ViewModel afin de pouvoir lancer une methode lors du click sur un bouton de la vue, il faut préciser un test et une fonction a executer
        public RelayCommandToDoList(Action<object> executeAction, Func<object, bool> canExecute)
        {
            this.executeAction = executeAction;
            this.canExecute = canExecute;
        }

        /// <summary>
        /// Gets whether the command can be executed
        /// </summary>
        /// <param name="parameter">Parameter</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        public bool CanExecute(object parameter)
        {
            return canExecute == null ? true : canExecute(parameter);
        }


        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            executeAction(parameter);
        }

    }
}
