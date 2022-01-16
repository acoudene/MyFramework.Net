using AC.Common;
using AC.IoC;

namespace AC.Data
{
  public class IoCUnitOfWorkProvider : IUnitOfWorkProvider
  {
    private readonly IIoCProvider? _ioCProvider = null;
    public IIoCProvider IoCProvider
    {
      get
      {
        if(_ioCProvider == null) throw new NullReferenceException("An IoC provider is necessary!");
        return _ioCProvider;
      }
    }

    public IoCUnitOfWorkProvider()
    {
      _ioCProvider = IoCHelper.Provider;
    }

    public IoCUnitOfWorkProvider(IIoCProvider ioCProvider)
    {
      _ioCProvider = ioCProvider;
    }

    public TUnitOfWork Create<TUnitOfWork>() where TUnitOfWork : IUnitOfWork
    {
      return Create<TUnitOfWork>(null);
    }

    public TUnitOfWork Create<TUnitOfWork>(string? unitOfWorkName) where TUnitOfWork : IUnitOfWork
    {
      var uow = default(TUnitOfWork);
      
      if (string.IsNullOrEmpty(unitOfWorkName))
      {
        Assert.IsTrue(IoCProvider.IsRegistered<TUnitOfWork>(), string.Format("This unit of work type {0} was not registered!", typeof(TUnitOfWork).FullName));        
        uow = IoCProvider.Resolve<TUnitOfWork>();
      }
      else
      {
        Assert.IsTrue(IoCProvider.IsRegistered<TUnitOfWork>(unitOfWorkName), string.Format("This unit of work type {0} with name {1} was not registered!", typeof(TUnitOfWork).FullName, unitOfWorkName));
        uow = IoCProvider.Resolve<TUnitOfWork>(unitOfWorkName);
      }

      Assert.IsTrue(!EqualityComparer<TUnitOfWork>.Default.Equals(uow,default(TUnitOfWork)), "An unit of work is necessary!");
      Assert.IsTrue(!uow.IsDisposed, "The targeted unit of work has been disposed!");
      return uow;
    }        

    public bool IsRegistered<TUnitOfWork>() where TUnitOfWork : IUnitOfWork
    {
      return IsRegistered<TUnitOfWork>(null);
    }

    public bool IsRegistered<TUnitOfWork>(string? unitOfWorkName) where TUnitOfWork : IUnitOfWork
    {
      var isRegistered = false;
      if (string.IsNullOrEmpty(unitOfWorkName))
      {
        isRegistered = IoCProvider.IsRegistered<TUnitOfWork>();
      }
      else
      {
        isRegistered = IoCProvider.IsRegistered<TUnitOfWork>(unitOfWorkName);
      }
      return isRegistered;
    }
  }
}
