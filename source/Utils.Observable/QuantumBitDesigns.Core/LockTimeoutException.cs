using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Utils.Observable
{
    /// <summary>
    /// Exception
    /// </summary>
    public class LockTimeoutException : Exception
    {
        /// <summary>
        /// Constructeur
        /// </summary>
        public LockTimeoutException()
            : base("Timeout waiting for lock")
        {
        }
    }
}
