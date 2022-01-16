namespace AC.Data
{
  /// <summary>
  /// Classe d'exception pour la couche d'accès aux données
  /// </summary>
  [Serializable]
  public class DataException : Exception
  {
    public DataException(string message)
        : base(message)
    {
    }

    public DataException(string format, params object[] args)
        : base(String.Format(format, args))
    {
    }

    public DataException(string message, Exception innerException)
        : base(message, innerException)
    {
    }

    public DataException(Exception innerException, string format, params object[] args)
        : base(String.Format(format, args), innerException)
    {
    }

    public DataException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
            : base(info, context)
    {
    }

  }
}
