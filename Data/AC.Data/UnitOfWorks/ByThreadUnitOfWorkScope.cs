namespace AC.Data
{
  public class ByThreadUnitOfWorkScope : IDisposable
  {
    [ThreadStatic]
    private static ByThreadUnitOfWorkScope? _instance = default(ByThreadUnitOfWorkScope);
    private static ByThreadUnitOfWorkScope Instance
    {
      get
      {
        if (_instance == null) throw new NullReferenceException("Can't be called before constructor");
        return _instance;
      }
    }
    private readonly IUnitOfWork _unitOfWork;
    private bool _isDisposed = false;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="unitOfWork"></param>
    public ByThreadUnitOfWorkScope(IUnitOfWork unitOfWork)
    {
      if (_instance != null && !_instance._isDisposed)
      {
        throw new DataException("UnitOfWork instances cannot be nested", new InvalidOperationException("UnitOfWork instances cannot be nested"));
      }

      Thread.BeginThreadAffinity();
      _instance = this;
      _unitOfWork = unitOfWork;
    }

    #region IDisposable Members

    /// <summary>
    /// 
    /// </summary>
    ~ByThreadUnitOfWorkScope()
    {
      Dispose(false);
    }

    /// <summary>
    /// 
    /// </summary>
    public void Dispose()
    {
      Dispose(true);
      GC.SuppressFinalize(this);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="disposing"></param>
    protected virtual void Dispose(bool disposing)
    {
      if (!_isDisposed)
      {
        if (disposing)
        {
          // free other managed objects that implement
          // IDisposable only          
          _isDisposed = true;
          Thread.EndThreadAffinity();
        }
      }
    }

    #endregion    

  }
}
