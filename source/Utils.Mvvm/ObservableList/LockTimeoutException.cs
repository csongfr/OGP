using System;
using Utils;

namespace QuantumBitDesigns.Core
{
    /// <summary>
    /// Exception
    /// </summary>
    public class LockTimeoutException : UtilsApplicationException
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
