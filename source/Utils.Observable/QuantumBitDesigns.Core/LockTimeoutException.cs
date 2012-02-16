using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QuantumBitDesigns.Core
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
