using AC.Common;
using AC.IoC;

namespace AC.Data
{
  /// <summary>
  /// Builder by IoC for repositories
  /// </summary>
  public class IoCRepositoryBuilderProvider : IRepositoryBuilderProvider
  {
		private readonly bool _providerOwnsIoCContainer = true;
    private readonly IIoCProvider? _ioCProvider = null;
    public IIoCProvider IoCProvider
    {
      get
      {
        if (_ioCProvider == null) throw new NullReferenceException("An IoC provider is necessary!");
        return _ioCProvider;
      }
    }

    public IoCRepositoryBuilderProvider(bool providerOwnsIoCContainer = true)
    {
      _ioCProvider = IoCHelper.Provider.CreateChildProvider();
			_providerOwnsIoCContainer = providerOwnsIoCContainer;
    }

    public IoCRepositoryBuilderProvider(IIoCProvider ioCProvider, bool providerOwnsIoCContainer = true)
    {
      _ioCProvider = ioCProvider;
			_providerOwnsIoCContainer = providerOwnsIoCContainer;
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
      var builder = GetBuilder<TRepository>();
      Assert.NotNull(builder, string.Format("A repository builder must be registered for this repository type: {0}!", typeof(TRepository).Name));

      return builder.FromUnitOfWork(unitOfWork).Build();
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
      Assert.NotNull(builder, string.Format("A repository builder must be registered for this repository type: {0}!", typeof(TRepository).Name));
      IoCProvider.RegisterInstance<IRepositoryBuilder<TRepository>>(builder);
    }

		/// <summary>
		/// Get a repository builder
		/// </summary>
		/// <typeparam name="TRepository">The targeted repository interface</typeparam>
		/// <returns>A repository builder</returns>
		public virtual IRepositoryBuilder<TRepository> GetBuilder<TRepository>() where TRepository : class, IRepository
		{
			var instance = (IoCProvider.IsRegistered<IRepositoryBuilder<TRepository>>()) ? IoCProvider.Resolve<IRepositoryBuilder<TRepository>>() : null;
      if (instance == null) throw new NullReferenceException($"Can't build builder with type {typeof(IRepositoryBuilder<TRepository>).Name}");
      return instance;
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
        IoCProvider.RegisterType<IRepositoryBuilder<TRepository>, IoCRepositoryBuilder<TRepository>>(new InjectionConstructor());
        builder = GetBuilder<TRepository>();
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
					if (_providerOwnsIoCContainer)
					{
						_ioCProvider?.Dispose();
					}
				}

				// TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
				// TODO: set large fields to null.

				disposedValue = true;
			}
		}

		// TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
		// ~IoCRepositoryBuilderProvider() {
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
