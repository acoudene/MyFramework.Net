namespace AC.Data
{
  public interface IUnitOfWorkProvider
  {
    /// <summary>
    /// Method to create a new unit of work
    /// </summary>
    /// <typeparam name="TUnitOfWork"></typeparam>
    /// <returns></returns>  
    TUnitOfWork Create<TUnitOfWork>() where TUnitOfWork : IUnitOfWork;

    /// <summary>
    /// Method to create a new unit of work with a specific pattern name
    /// </summary>
    /// <typeparam name="TUnitOfWork"></typeparam>
    /// <param name="unitOfWorkName">Name of the unit of work pattern</param>
    /// <returns></returns>
    TUnitOfWork Create<TUnitOfWork>(string unitOfWorkName) where TUnitOfWork : IUnitOfWork;    

    bool IsRegistered<TUnitOfWork>() where TUnitOfWork : IUnitOfWork;   
    bool IsRegistered<TUnitOfWork>(string unitOfWorkName) where TUnitOfWork : IUnitOfWork;
  }
}
