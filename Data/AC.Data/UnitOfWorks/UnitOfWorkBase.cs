namespace AC.Data
{
  /// <summary>
  /// Base class for unit of work
  /// </summary>
  public abstract class UnitOfWorkBase : IUnitOfWork
  {
    /// <summary>
    /// Commit the modification to this unit of work
    /// </summary>
    public abstract void Commit();

    /// <summary>
    /// Get the state of an entity
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <param name="entity">The entity to get its state</param>
    /// <returns>The state of the given entity</returns>
    public abstract EntityState GetEntityState<TEntity>(TEntity entity) where TEntity : class, IEntity;

    /// <summary>
    /// Clean an entity into this unit of work
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <param name="entity">The entity to clean</param>
    public abstract void MarkClean<TEntity>(TEntity entity) where TEntity : class, IEntity;

    /// <summary>
    /// Set an entity to be a deleted one into this unit of work
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <param name="entity">The entity to set to a deleted one</param>
    public abstract void MarkDeleted<TEntity>(TEntity entity) where TEntity : class, IEntity;

    /// <summary>
    /// Set an entity to be a modified one into this unit of work
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <param name="entity">The entity to set to a modified one</param>
    public abstract void MarkDirty<TEntity>(TEntity entity) where TEntity : class, IEntity;

    /// <summary>
    /// Set an entity to be a new one into this unit of work
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <param name="entity">The entity to set to new one</param>
    public abstract void MarkNew<TEntity>(TEntity entity) where TEntity : class, IEntity;

    /// <summary>
    /// Rollback or undo modifications into this unit of work
    /// </summary>
    public abstract void Rollback();

    #region IDisposable Support
    private bool _isDisposed = false; // To detect redundant calls

    /// <summary>
    /// Check if this instance has already been disposed (for particular use cases).
    /// </summary>
    public virtual bool IsDisposed => _isDisposed;

    /// <summary>
    /// Dispose pattern
    /// </summary>
    /// <param name="disposing"></param>
    protected virtual void Dispose(bool disposing)
    {
      if (!_isDisposed)
      {
        if (disposing)
        {
          // dispose managed state (managed objects).					
        }

        // free unmanaged resources (unmanaged objects) and override a finalizer below.
        // set large fields to null.

        _isDisposed = true;
      }
    }

    // override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
    // ~UnitOfWorkBase() {
    //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
    //   Dispose(false);
    // }

    /// <summary>
    /// This code added to correctly implement the disposable pattern. 
    /// </summary>
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
