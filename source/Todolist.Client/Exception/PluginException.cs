[System.Serializable]

/// <summary>
/// Classe qui gère les exceptions du client
/// </summary>
public class PluginException : System.ApplicationException
{
    /// <summary>
    /// Constructeur par défaut
    /// </summary>
    public PluginException()
    {
    }

    /// <summary>
    /// Constructeur
    /// </summary>
    /// <param name="message"> message d'erreur</param>
    public PluginException(string message)
        : base(message)
    {
    }

    /// <summary>
    /// Constructeur
    /// </summary>
    /// <param name="message">message d'erreur</param>
    /// <param name="inner">gère un autre type d'exception</param>
    public PluginException(string message, System.Exception inner)
        : base(message, inner)
    {
    }

    /// <summary>
    /// Exception client
    /// </summary>
    /// <param name="info">SerializationInfo</param>
    /// <param name="context">StreamingContext</param>
    protected PluginException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
        : base(info, context)
    {
    }
}