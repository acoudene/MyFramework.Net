namespace AC.Data
{
  /// <summary>
  /// Memory-based builder for repositories
  /// </summary>
  public class MemoryRepositoryBuilderProvider : IRepositoryBuilderProvider
  {
    private Dictionary<Type, IRepositoryBuilder> _items = new Dictionary<Type, IRepositoryBuilder>();

    public MemoryRepositoryBuilderProvider()
    {
    }
    
    public virtual void Configure()
    {
      ConfigureExt();
      IsConfigured = true;
    }

    protected virtual void ConfigureExt()
    {
      // To implement in inherited classes
    }

    public bool IsConfigured { get; protected set; }
    
    /// <summary>
    /// Get a repository from its interface
    /// </summary>
    /// <typeparam name="TRepository">The interface to find</typeparam>
    /// <returns>The mapped instance to the interface</returns>
    public virtual TRepository GetRepository<TRepository>(IUnitOfWork unitOfWork) where TRepository : class, IRepository
    {
      _items.TryGetValue(typeof(IRepositoryBuilder<TRepository>), out var storedBuilder);
      IRepositoryBuilder<TRepository>? builder = storedBuilder as IRepositoryBuilder<TRepository>;
      var instance = builder?.FromUnitOfWork(unitOfWork).Build();
      if (instance == null) throw new DataException($"Can't build repository instance for type {typeof(TRepository).Name}");
      return instance!;
    }

    /// <summary>
    /// Set a repository builder
    /// </summary>
    /// <typeparam name="TBuilder">The targeted builder</typeparam>
    /// <typeparam name="TRepository">The targeted repository interface</typeparam>
    /// <param name="builder">The repository builder to set</param>
    public virtual void SetBuilder<TBuilder, TRepository>(TBuilder builder)
      where TBuilder : class, IRepositoryBuilder<TRepository>
      where TRepository : class, IRepository
    {
      _items[typeof(IRepositoryBuilder<TRepository>)] = builder;
    }

    /// <summary>
    /// Get a repository builder
    /// </summary>
    /// <typeparam name="TRepository">The targeted repository interface</typeparam>
    /// <returns>A repository builder</returns>
    public virtual IRepositoryBuilder<TRepository> GetBuilder<TRepository>() where TRepository : class, IRepository
    {
      _items.TryGetValue(typeof(TRepository), out var storedBuilder);
      var instance = storedBuilder as IRepositoryBuilder<TRepository>;
      if (instance == null) throw new NullReferenceException($"Can't build an instance from this repository type {typeof(TRepository).Name}");
      return instance!;
    }

    /// <summary>
    /// Fix default builder to get functional behavior if not exist
    /// </summary>
    /// <typeparam name="TRepository">Repository associated to the default builder to fix</typeparam>
    public virtual IRepositoryBuilder<TRepository> FixDefaultBuilderIfNeeded<TRepository>() where TRepository : class, IRepository
    {
      var builder = GetBuilder<TRepository>();
      if (builder == null)
      {        
        builder = new MemoryRepositoryBuilder<TRepository>();
        SetBuilder<IRepositoryBuilder<TRepository>, TRepository>(builder);
      }
      return builder;
    }

		#region IDisposable Support
		private bool disposedValue = false; // To detect redundant calls

		protected virtual void Dispose(bool disposing)
		{
			if (!disposedValue)
			{
				if (disposing)
				{
					// dispose managed state (managed objects).
					_items.Clear();
				}

				// TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
				// TODO: set large fields to null.

				disposedValue = true;
			}
		}

		// TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
		// ~MemoryRepositoryBuilderProvider() {
		//   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
		//   Dispose(false);
		// }

		// This code added to correctly implement the disposable pattern.
		public void Dispose()
		{
			// Do not change this code. Put cleanup code in Dispose(bool disposing) above.
			Dispose(true);
			// uncomment the following line if the finalizer is overridden above.
			GC.SuppressFinalize(this);
		}
		#endregion
	}
}
