namespace AC.Data
{
  /// <summary>
  /// A readonly repository
  /// </summary>
  /// <typeparam name="TEntity"></typeparam>
  public interface IQueryRepository<TEntity> : IRepository where TEntity : class, IEntity
  {    
    /// <summary>
    /// Find an entity from its key fields like PKs
    /// </summary>
    /// <param name="keyValues">Values to test on the key fields of the expected entity</param>
    /// <returns></returns>
    TEntity Find(params object[] keyValues);

    /// <summary>
    /// Give the number of all entities of this type
    /// </summary>
    /// <returns>The number of all entities of this type</returns>
    int Count();
  }
}
