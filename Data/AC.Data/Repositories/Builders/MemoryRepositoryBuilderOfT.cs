namespace AC.Data
{
  public class MemoryRepositoryBuilder<TRepository> : IRepositoryBuilder<TRepository> 
    where TRepository : class, IRepository
  {
    private IUnitOfWork? _unitOfWork = default(IUnitOfWork);
    private Type? _repositoryType = default(Type);
    public IUnitOfWork? UnitOfWork
    {
      get
      {
        return _unitOfWork;
      }
    }

    public MemoryRepositoryBuilder()
    {
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
      _repositoryType = typeof(TToRepository); 
      return this;
    }

    public virtual TRepository Build()
    {
      if (_unitOfWork == null) throw new DataException(string.Format("A unit of work is necessary to build this repository type {0}!", typeof(TRepository).Name));
      if (_repositoryType == null) throw new DataException(string.Format("A repository type is necessary to build this repository type {0}!", typeof(TRepository).Name));
      var instance = Activator.CreateInstance(_repositoryType!, _unitOfWork) as TRepository;
      if (instance == null) throw new DataException(string.Format("Can't build a repository instance for this type {0}!", typeof(TRepository).Name));
      return instance!;
    }

  }
}
