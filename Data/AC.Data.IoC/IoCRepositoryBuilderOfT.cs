using AC.Common;
using AC.IoC;

namespace AC.Data
{
  public class IoCRepositoryBuilder<TRepository> : IRepositoryBuilder<TRepository> 
    where TRepository : class, IRepository
  {
    private readonly IIoCProvider? _ioCProvider = null;
    private IUnitOfWork? _unitOfWork = null;

    public IIoCProvider IoCProvider
    {
      get
      {
        if (_ioCProvider == null) throw new NullReferenceException("An IoC provider is necessary!");
        return _ioCProvider;
      }
    }

    public IUnitOfWork? UnitOfWork
    {
      get
      {
        return _unitOfWork;
      }
    }

    public IoCRepositoryBuilder()
    {
      _ioCProvider = IoCHelper.Provider;
    }

    public IoCRepositoryBuilder(IIoCProvider ioCProvider)
    {
      _ioCProvider = ioCProvider;
    }

    public IRepositoryBuilder<TRepository> FromUnitOfWork(IUnitOfWork unitOfWork)
    {
      _unitOfWork = unitOfWork;
      return this;
    }

    /// <summary>
    /// Fix a default repository if not exist
    /// </summary>
    /// <typeparam name="TToRepository">The default implementation if needed</typeparam>
    /// <returns>The fluent interface</returns>
    public IRepositoryBuilder<TRepository> FixDefaultRepositoryIfNeeded<TToRepository>()
      where TToRepository : TRepository
    {
      if (!IoCProvider.IsRegistered<TRepository>()) IoCProvider.RegisterType<TRepository, TToRepository>(new TransientLifetimeManager());
      return this;
    }

    public virtual TRepository Build()
    {
      if (_unitOfWork == null) throw new NullReferenceException(string.Format("A unit of work is necessary to build this repository type {0}!", typeof(TRepository).Name));
      var instance = _ioCProvider?.Resolve<TRepository>(new OrderedParametersOverride(new List<object>() { _unitOfWork }));
      if (instance == null) throw new NullReferenceException($"Can't build {typeof(TRepository).Name}");
      return instance;
    }

  }
}
