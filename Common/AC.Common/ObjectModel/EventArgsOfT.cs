namespace AC.Common
{
  /// <summary>
  /// EventArgs générique
  /// </summary>
  /// <typeparam name="T">type de donnée gérée</typeparam>
  public class EventArgs<T>
        : EventArgs
  {
    #region Attributs

    private T? _data = default(T);

    #endregion

    #region Constructeurs

    /// <summary>
    /// Constructeur
    /// </summary>
    public EventArgs()
    {
    }

    /// <summary>
    /// Constructeur
    /// </summary>
    /// <param name="data">donnée liée</param>
    public EventArgs(T data)
    {
      Data = data;
    }

    #endregion

    #region Propriétés

    /// <summary>
    /// Obtient ou définit la valeur associée
    /// </summary>
    public T? Data
    {
      get { return _data; }
      set { _data = value; }
    }

    #endregion
  }

  /// <summary>
  /// EventArgs générique
  /// </summary>
  /// <typeparam name="T1">type de donnée gérée</typeparam>
  /// <typeparam name="T2">type de donnée gérée</typeparam>
  public class EventArgs<T1, T2>
      : EventArgs
  {
    #region Attributs

    private T1? _data1 = default(T1);
    private T2? _data2 = default(T2);

    #endregion

    #region Constructeurs

    /// <summary>
    /// Constructeur
    /// </summary>
    public EventArgs()
    {
    }

    /// <summary>
    /// Constructeur
    /// </summary>
    /// <param name="data1">donnée liée</param>
    /// <param name="data2">donnée liée</param>
    public EventArgs(T1 data1, T2 data2)
    {
      Data1 = data1;
      Data2 = data2;
    }

    #endregion

    #region Propriétés

    /// <summary>
    /// Obtient ou définit la valeur associée
    /// </summary>
    public T1? Data1
    {
      get { return _data1; }
      set { _data1 = value; }
    }

    /// <summary>
    /// Obtient ou définit la valeur associée
    /// </summary>
    public T2? Data2
    {
      get { return _data2; }
      set { _data2 = value; }
    }

    #endregion
  }
}
