using System;

namespace Utils
{
    [Serializable]
    public class UtilsApplicationException : ApplicationException
    {
        public UtilsApplicationException() { }
        public UtilsApplicationException(string message) : base(message) { }
        public UtilsApplicationException(string message, Exception inner) : base(message, inner) { }
        protected UtilsApplicationException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}
