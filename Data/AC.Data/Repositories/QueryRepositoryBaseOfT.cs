using System.Linq.Expressions;

namespace AC.Data
{
  /// <summary>
  /// Read only repository base class
  /// </summary>
  /// <typeparam name="TEntity"></typeparam>
  public abstract class QueryRepositoryBase<TEntity> : IQueryRepository<TEntity> where TEntity : class, IEntity
  {
    private readonly IUnitOfWork _unitOfWork;

    /// <summary>
    /// The associated parent unit of work 
    /// </summary>
    public IUnitOfWork UnitOfWork { get { return _unitOfWork; } }

    /// <summary>
    /// Constructor with unit of work parameter
    /// </summary>
    /// <param name="unitOfWork">The unit of work to set</param>
    public QueryRepositoryBase(IUnitOfWork unitOfWork)
    {
      _unitOfWork = unitOfWork;
    }

    /// <summary>
    /// Find an entity from its key fields like PKs
    /// </summary>
    /// <param name="keyValues">Values to test on the key fields of the expected entity</param>
    /// <returns>Found entity from its key fields like PKs</returns>   
    public abstract TEntity Find(params object[] keyValues);

    /// <summary>
    /// Give the number of all entities of this type
    /// </summary>
    /// <returns>The number of all entities of this type</returns>
    public virtual int Count()
    {
      return GetAll().Count();
    }

    /// <summary>
    /// Give the number of all entities of this type from search criteria
    /// </summary>
    /// <param name="predicate">predicate to use to filter</param>
    /// <returns>The number of all entities of this type from search criteria</returns>
    protected internal virtual int Count(Expression<Func<TEntity, bool>> predicate)
    {
      return GetByLambda(predicate).Count();
    }

    /// <summary>
    /// Get a set of entities from a lambda expression predicate
    /// </summary>
    /// <param name="predicate">The predicate to use for the filter</param>
    /// <returns>A set of entities from a lambda expression predicate</returns>
    protected internal abstract IQueryable<TEntity> GetByLambda(Expression<Func<TEntity, bool>> predicate);

    /// <summary>
    /// Get a set of ordered entities from a lambda expression predicate
    /// </summary>
    /// <param name="predicate">The predicate to use for the filter</param>
    /// <param name="orderBy">The expected order from a predicate</param>
    /// <returns>A set of ordered entities from a lambda expression predicate</returns>
    protected internal virtual IQueryable<TEntity> GetByLambdaOrdered<TKey>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TKey>> orderBy)
    {
      return GetByLambda(predicate).OrderBy(orderBy);
    }

    /// <summary>
    /// Get a set of ordered entities from a lambda expression predicate with pagination
    /// </summary>
    /// <param name="predicate">The predicate to use for the filter</param>
    /// <param name="orderBy">The expected order from a predicate</param>
    /// <param name="selectedPage">The index of the expected page</param>
    /// <param name="pageSize">The size of the page</param>
    /// <returns>A set of ordered entities from a lambda expression predicate with pagination</returns>
    protected internal virtual IQueryable<TEntity> GetByLambdaOrdered<TKey>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TKey>> orderBy, int selectedPage, int pageSize)
    {
      int itemsToSkip = GetItemsToSkip(selectedPage, pageSize);
      return GetByLambda(predicate).OrderBy(orderBy).Skip(itemsToSkip).Take(pageSize);
    }

    /// <summary>
    /// Get a set of entities from a lambda expression predicate
    /// </summary>
    /// <typeparam name="TProperty"></typeparam>
    /// <param name="predicate">The predicate to use for the filter</param>    
    /// <param name="joinPath">The join entities path to use</param>
    /// <returns>A set of entities from a lambda expression predicate</returns>
    protected internal abstract IQueryable<TEntity> GetByLambda<TProperty>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TProperty>> joinPath);

    /// <summary>
    /// Get a set of ordered entities from a lambda expression predicate
    /// </summary>
    /// <typeparam name="TProperty"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    /// <param name="predicate">The predicate to use for the filter</param>    
    /// <param name="joinPath">The join entities path to use</param>
    /// <param name="orderBy">The expected order from a predicate</param>
    /// <returns>A set of ordered entities from a lambda expression predicate</returns>
    protected internal virtual IQueryable<TEntity> GetByLambdaOrdered<TProperty, TKey>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TProperty>> joinPath, Expression<Func<TEntity, TKey>> orderBy)
    {
      return GetByLambda(predicate, joinPath).OrderBy(orderBy);
    }

    /// <summary>
    /// Get a set of ordered entities from a lambda expression predicate
    /// </summary>
    /// <typeparam name="TProperty"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    /// <param name="predicate">The predicate to use for the filter</param>    
    /// <param name="joinPath">The join entities path to use</param>
    /// <param name="orderBy">The expected order from a predicate</param>
    /// <param name="selectedPage">The index of the expected page</param>
    /// <param name="pageSize">The size of the page</param>
    /// <returns>A set of ordered entities from a lambda expression predicate</returns>
    protected internal virtual IQueryable<TEntity> GetByLambdaOrdered<TProperty, TKey>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TProperty>> joinPath, Expression<Func<TEntity, TKey>> orderBy, int selectedPage, int pageSize)
    {
      int itemsToSkip = GetItemsToSkip(selectedPage, pageSize);
      return GetByLambda(predicate, joinPath).OrderBy(orderBy).Skip(itemsToSkip).Take(pageSize);
    }

    /// <summary>
    /// Get all entities of this expected type
    /// </summary>
    /// <returns>All entities of this expected type</returns>
    protected internal abstract IQueryable<TEntity> GetAll();

    /// <summary>
    /// Get all ordered entities of this expected type
    /// </summary>
    /// <param name="orderBy">The expected order from a predicate</param>
    /// <returns>All entities of this expected type</returns>
    protected internal virtual IQueryable<TEntity> GetAllOrdered<TKey>(Expression<Func<TEntity, TKey>> orderBy)
    {
      return GetAll().OrderBy(orderBy);
    }

    /// <summary>
    /// Get all ordered entities of this expected type
    /// </summary>
    /// <param name="orderBy">The expected order from a predicate</param>
    /// <param name="selectedPage">The index of the expected page</param>
    /// <param name="pageSize">The size of the page</param>
    /// <returns>All ordered entities of this expected type</returns>
    protected internal virtual IQueryable<TEntity> GetAllOrdered<TKey>(Expression<Func<TEntity, TKey>> orderBy, int selectedPage, int pageSize)
    {
      int itemsToSkip = GetItemsToSkip(selectedPage, pageSize);
      return GetAll().OrderBy(orderBy).Skip(itemsToSkip).Take(pageSize);
    }

    /// <summary>
    /// Get all entities of this expected type with hydrated related entities
    /// </summary>
    /// <typeparam name="TProperty"></typeparam>
    /// <param name="joinPath">The join entities path to us</param>
    /// <returns>All entities of this expected type with hydrated related entities</returns>
    protected internal abstract IQueryable<TEntity> GetAll<TProperty>(Expression<Func<TEntity, TProperty>> joinPath);

    /// <summary>
    /// Get all ordered entities of this expected type with hydrated related entities
    /// </summary>
    /// <typeparam name="TProperty"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    /// <param name="joinPath">The join entities path to us</param>
    /// <returns>All entities of this expected type with hydrated related entities</returns>
    /// <param name="orderBy">The expected order from a predicate</param>
    /// <returns>All ordered entities of this expected type with hydrated related entities</returns>
    protected internal virtual IQueryable<TEntity> GetAllOrdered<TProperty, TKey>(Expression<Func<TEntity, TProperty>> joinPath, Expression<Func<TEntity, TKey>> orderBy)
    {
      return GetAll(joinPath).OrderBy(orderBy);
    }

    /// <summary>
    /// Get all ordered entities of this expected type with hydrated related entities
    /// </summary>
    /// <typeparam name="TProperty"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    /// <param name="joinPath">The join entities path to us</param>
    /// <returns>All entities of this expected type with hydrated related entities</returns>
    /// <param name="orderBy">The expected order from a predicate</param>
    /// <param name="selectedPage">The index of the expected page</param>
    /// <param name="pageSize">The size of the page</param>
    /// <returns>All ordered entities of this expected type with hydrated related entities</returns>
    protected internal virtual IQueryable<TEntity> GetAllOrdered<TProperty, TKey>(Expression<Func<TEntity, TProperty>> joinPath, Expression<Func<TEntity, TKey>> orderBy, int selectedPage, int pageSize)
    {
      int itemsToSkip = GetItemsToSkip(selectedPage, pageSize);
      return GetAll(joinPath).OrderBy(orderBy).Skip(itemsToSkip).Take(pageSize);
    }

    /// <summary>
    /// Tools function to calculate pagination and items to skip from an index
    /// </summary>
    /// <param name="selectedPage">The given index</param>
    /// <param name="pageSize">The page size by index</param>
    /// <returns>The positions to skip</returns>
    protected int GetItemsToSkip(int selectedPage, int pageSize)
    {
      if (!(selectedPage >= 1)) throw new ArgumentException("Starting page for paging must start by 1", nameof(selectedPage));
      if (!(pageSize > 0)) throw new ArgumentException("Page size must be greater than 0", nameof(pageSize));
      return (selectedPage - 1) * pageSize;
    }
  }
}
