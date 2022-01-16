namespace AC.Data
{
  public interface IUnitOfWorkRepository : IUnitOfWork
  {
    /// <summary>
    /// Get a dedicated repository from this unit of work
    /// </summary>
    /// <typeparam name="TRepository"></typeparam>
    /// <returns>The expected repository</returns>
    TRepository GetRepository<TRepository>() where TRepository : class, IRepository;
  }
}
