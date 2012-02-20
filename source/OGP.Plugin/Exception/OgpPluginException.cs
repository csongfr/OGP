using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OGP.Plugin.Exception
{
    /// <summary>
    /// Exception à dériver pour les plugins OGP.
    /// </summary>
    [Serializable]
    public class OgpPluginException : System.ApplicationException
    {
        public OgpPluginException() { }
        public OgpPluginException(string message) : base(message) { }
        public OgpPluginException(string message, System.Exception inner) : base(message, inner) { }
        protected OgpPluginException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}
