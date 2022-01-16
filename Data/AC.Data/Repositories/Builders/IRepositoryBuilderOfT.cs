namespace AC.Data
{
  public interface IRepositoryBuilder<TRepository> : IRepositoryBuilder
    where TRepository : class, IRepository
  {
    IRepositoryBuilder<TRepository> FromUnitOfWork(IUnitOfWork unitOfWork);

    /// <summary>
    /// Fix a default repository if not exist
    /// </summary>
    /// <typeparam name="TToRepository">The default implementation if needed</typeparam>
    /// <returns>The fluent interface</returns>
    IRepositoryBuilder<TRepository> FixDefaultRepositoryIfNeeded<TToRepository>()
      where TToRepository : TRepository;

    TRepository Build();
  }
}
