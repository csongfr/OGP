using System;

namespace Utils.Log
{
   public class BasicLogEntry : ILogEntry
    {
        public string Message { get; set; }

        public Exception Exception { get; set; }

        public string GetStringMessage()
        {
            return Message;
        }

        public bool IsExceptionMessage
        {
            get
            {                       
                return Exception != null;
            }
        }
    }
}
