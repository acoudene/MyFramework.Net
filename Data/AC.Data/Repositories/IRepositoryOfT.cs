namespace AC.Data
{
  /// <summary>
  /// Repository interface by generics
  /// </summary>
  /// <typeparam name="TEntity"></typeparam>
  public interface IRepository<TEntity> : 
    IQueryRepository<TEntity>, 
    ICommandRepository<TEntity> 
    where TEntity : class, IEntity
  {    
    
  }
}
                                                          
