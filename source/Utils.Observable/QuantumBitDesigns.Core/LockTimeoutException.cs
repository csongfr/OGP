using System;

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
