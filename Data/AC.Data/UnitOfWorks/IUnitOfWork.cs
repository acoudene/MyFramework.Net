namespace AC.Data
{
  /// <summary>
  /// Unit of work interface
  /// </summary>
  public interface IUnitOfWork : IDisposable
  {
    /// <summary>
    /// Clean an entity into this unit of work
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <param name="entity">The entity to clean</param>
    void MarkClean<TEntity>(TEntity entity) where TEntity : class, IEntity;

    /// <summary>
    /// Set an entity to be a new one into this unit of work
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <param name="entity">The entity to set to new one</param>
    void MarkNew<TEntity>(TEntity entity) where TEntity : class, IEntity;

    /// <summary>
    /// Set an entity to be a modified one into this unit of work
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <param name="entity">The entity to set to a modified one</param>
    void MarkDirty<TEntity>(TEntity entity) where TEntity : class, IEntity;

    /// <summary>
    /// Set an entity to be a deleted one into this unit of work
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <param name="entity">The entity to set to a deleted one</param>
    void MarkDeleted<TEntity>(TEntity entity) where TEntity : class, IEntity;

    /// <summary>
    /// Get the state of an entity
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <param name="entity">The entity to get its state</param>
    /// <returns>The state of the given entity</returns>
    EntityState GetEntityState<TEntity>(TEntity entity) where TEntity : class, IEntity;

    /// <summary>
    /// Commit the modification to this unit of work
    /// </summary>
    void Commit();

    /// <summary>
    /// Rollback or undo modifications into this unit of work
    /// </summary>
    void Rollback();

    /// <summary>
    /// Check if this instance has already been disposed (for particular use cases).
    /// </summary>
    bool IsDisposed { get; }
  }
}
