using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using Utils;

namespace Utils
{
    /// <summary>
    /// Démarre un background worker.
    /// </summary>
    public static class BackgroundTask
    {
        /// <summary>
        /// Lance une tache dans un background worker.
        /// </summary>
        /// <param name="taskFunc">Tache à exécuter.</param>
        /// <param name="exceptionHandling">Décrit la gestion d'exception à appliquer.</param>
        public static void Start(Action taskFunc, EnumBackgroundExceptionHandling exceptionHandling)
        {
            Action<Exception> action = (Action<Exception>)(
                exception =>
                {
                    switch (exceptionHandling)
                    {
                        case EnumBackgroundExceptionHandling.SilentCatch:
                            // Silent catch, rien à faire !
                            break;
                        case EnumBackgroundExceptionHandling.Throw:
                        default:
                            if (exception != null)
                            {
                                exception.Rethrow();
                            }
                            break;
                    }
                });

            Start(taskFunc, action);
        }

        /// <summary>
        /// Lance une tache dans un background worker.
        /// </summary>
        /// <param name="taskFunc">Tache à exécuter.</param>
        /// <param name="completionAction">Action à réaliser après l'exécution. Recoit l'exception si une exception a eu lieu.</param>
        public static void Start(Action taskFunc, Action<Exception> completionAction)
        {
            if (taskFunc == null)
            {
                throw new ArgumentNullException("taskFunc");
            }

            var backgroundWorker = new BackgroundWorker();

            backgroundWorker.DoWork += new DoWorkEventHandler(
                delegate
                {
                    taskFunc();
                });

            backgroundWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(
                delegate(object sender, RunWorkerCompletedEventArgs e)
                {
                    if (completionAction != null)
                    {
                        completionAction(e.Error);
                    }
                });

            backgroundWorker.RunWorkerAsync();
        }
    }
}
