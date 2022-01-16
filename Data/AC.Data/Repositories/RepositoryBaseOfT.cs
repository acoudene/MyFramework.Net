namespace AC.Data
{
  /// <summary>
  /// Repository base class writable and readable
  /// </summary>
  /// <typeparam name="TEntity"></typeparam>
  public abstract class RepositoryBase<TEntity> : QueryRepositoryBase<TEntity>, ICommandRepository<TEntity> 
    where TEntity : class, IEntity
  {
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="unitOfWork"></param>
    public RepositoryBase(IUnitOfWork unitOfWork)
      : base(unitOfWork)
    {

    }

    /// <summary>
    /// Insert an entity to an unit of work
    /// </summary>
    /// <param name="entity"></param>
    public virtual void Insert(TEntity entity)
    {
      UnitOfWork.MarkNew(entity);
    }

    /// <summary>
    /// Update an entity inside unit of work
    /// </summary>
    /// <param name="entity"></param>
    public virtual void Update(TEntity entity)
    {
      UnitOfWork.MarkDirty(entity);
    }

    /// <summary>
    /// Delete an entity inside unit of work
    /// </summary>
    /// <param name="entity"></param>
    public virtual void Delete(TEntity entity)
    {
      UnitOfWork.MarkDeleted(entity);
    }
  }
}
