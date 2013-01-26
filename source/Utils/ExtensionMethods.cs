using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Utils
{
    /// <summary>
    /// Classe permettant de stocker des méthodes d'extension utiles.
    /// </summary>
    public static class ExtensionMethods
    {
        /// <summary>
        /// Rethrow une exception en rétablissant la StackTrace complète.
        /// Cf : http://stackoverflow.com/questions/4555599/how-to-rethrow-the-inner-exception-of-a-targetinvocationexception-without-losing/4557183#4557183
        /// </summary>
        /// <param name="ex">Exception</param>
        [DebuggerStepThrough]
        public static void Rethrow(this Exception ex)
        {
            if (ex == null)
            {
                return;
            }

            typeof(Exception).GetMethod("PrepForRemoting", BindingFlags.NonPublic | BindingFlags.Instance).Invoke(ex, new object[0]);

            throw ex;
        }
    }
}
