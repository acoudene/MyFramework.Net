using System.Data;

namespace AC.Data
{
  /// <summary>
  /// Factory to create unit of works
  /// </summary>
  public static class UnitOfWorkFactory
  {
    private static object _syncRoot = new object();
    private static IUnitOfWorkProvider _provider;

    /// <summary>
    /// Manage the way of creating unit of works
    /// </summary>
    public static IUnitOfWorkProvider Provider
    {
      get
      {
        lock (_syncRoot)
        {
          return _provider;
        }
      }
      set
      {
        lock (_syncRoot)
        {
          if (Frozen) throw new ReadOnlyException("The repository provider is frozen, you could unfreeze this object if needed and if it makes sense to your need.");
          _provider = value;
        }
      }
    }
    private static bool _frozen = true;

    /// <summary>
    /// Freeze the properties of this object
    /// </summary>
    public static bool Frozen
    {
      get
      {
        lock (_syncRoot)
        {
          return _frozen;
        }
      }
      set
      {
        lock (_syncRoot)
        {
          _frozen = value;
        }
      }
    }
  
    /// <summary>
    /// Static constructor to initialize default values
    /// </summary>
    static UnitOfWorkFactory()
    {
      // By default
      _provider = new MemoryUnitOfWorkProvider();
      Frozen = true;
    }

    /// <summary>
    /// Method to create a new unit of work
    /// </summary>
    /// <typeparam name="TUnitOfWork"></typeparam>
    /// <returns></returns>
    public static TUnitOfWork Create<TUnitOfWork>() where TUnitOfWork : IUnitOfWork
    {
      lock (_syncRoot)
      {
        return Provider.Create<TUnitOfWork>();
      }
    }

    /// <summary>
    /// Method to create a new unit of work with a specific pattern name
    /// </summary>
    /// <typeparam name="TUnitOfWork"></typeparam>
    /// <param name="unitOfWorkName">Name of the unit of work pattern</param>
    /// <returns></returns>
    public static TUnitOfWork Create<TUnitOfWork>(string unitOfWorkName) where TUnitOfWork : IUnitOfWork
    {
      lock (_syncRoot)
      {
        return Provider.Create<TUnitOfWork>(unitOfWorkName);
      }
    }    

    public static bool IsRegistered<TUnitOfWork>() where TUnitOfWork : IUnitOfWork
    {
      lock (_syncRoot)
      {
        return Provider.IsRegistered<TUnitOfWork>();
      }
    }

    public static bool IsRegistered<TUnitOfWork>(string unitOfWorkName) where TUnitOfWork : IUnitOfWork
    {
      lock (_syncRoot)
      {
        return Provider.IsRegistered<TUnitOfWork>(unitOfWorkName);
      }
    }
  }
}
