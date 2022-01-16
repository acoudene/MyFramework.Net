namespace AC.Data
{
  public class MemoryUnitOfWorkProvider : IUnitOfWorkProvider
  {
    public TUnitOfWork Create<TUnitOfWork>() where TUnitOfWork : IUnitOfWork
    {
      throw new NotImplementedException();
    }

    public TUnitOfWork Create<TUnitOfWork>(string unitOfWorkName) where TUnitOfWork : IUnitOfWork
    {
      throw new NotImplementedException();
    }

    public bool IsRegistered<TUnitOfWork>() where TUnitOfWork : IUnitOfWork
    {
      throw new NotImplementedException();
    }

    public bool IsRegistered<TUnitOfWork>(string unitOfWorkName) where TUnitOfWork : IUnitOfWork
    {
      throw new NotImplementedException();
    }
  }
}