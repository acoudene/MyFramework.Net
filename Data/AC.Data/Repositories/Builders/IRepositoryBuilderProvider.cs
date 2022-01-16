namespace AC.Data
{
  /// <summary>
  /// Provider used to store repositories
  /// </summary>
  public interface IRepositoryBuilderProvider : IDisposable
  {
    /// <summary>
    /// Used to configure the unit of work
    /// </summary>
    void Configure();

    /// <summary>
    /// Check if this unit of work has beend configured
    /// </summary>
    bool IsConfigured { get; }

    /// <summary>
    /// Get a repository from its interface
    /// </summary>
    /// <typeparam name="TRepository">The interface to find</typeparam>
    /// <returns>The mapped instance to the interface</returns>
    TRepository GetRepository<TRepository>(IUnitOfWork unitOfWork) where TRepository : class, IRepository;

    /// <summary>
    /// Set a repository builder
    /// </summary>
    /// <typeparam name="TBuilder">The targeted builder</typeparam>
    /// <typeparam name="TRepository">The targeted repository interface</typeparam>
    /// <param name="builder">The repository builder to set</param>
    void SetBuilder<TBuilder, TRepository>(TBuilder builder)
      where TBuilder : class, IRepositoryBuilder<TRepository>
      where TRepository : class, IRepository;

    /// <summary>
    /// Get a repository builder
    /// </summary>
    /// <typeparam name="TRepository">The targeted repository interface</typeparam>
    /// <returns>A repository builder</returns>
    IRepositoryBuilder<TRepository> GetBuilder<TRepository>() where TRepository : class, IRepository;

    /// <summary>
    /// Fix default builder to get functional behavior if not exist
    /// </summary>
    /// <typeparam name="TRepository">Repository associated to the default builder to fix</typeparam>
    /// <returns>The default builder fixed</returns>
    IRepositoryBuilder<TRepository> FixDefaultBuilderIfNeeded<TRepository>() where TRepository : class, IRepository;
    
  }
}