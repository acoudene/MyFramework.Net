namespace AC.Data
{
  /// <summary>
  /// Interface for writting repositories
  /// </summary>
  /// <typeparam name="TEntity"></typeparam>
  public interface ICommandRepository<TEntity> : IRepository where TEntity : class, IEntity
  {
    /// <summary>
    /// Insert an entity into the unit of work
    /// </summary>
    /// <param name="entity">The entity to set to added</param>
    void Insert(TEntity entity);

    /// <summary>
    /// Update an entity into the unit of work
    /// </summary>
    /// <param name="entity">The entity to set as modified</param>
    void Update(TEntity entity);

    /// <summary>
    /// Delete an entity from the unit of work
    /// </summary>
    /// <param name="entity">The entity to delete</param>
    void Delete(TEntity entity);
  }
}

