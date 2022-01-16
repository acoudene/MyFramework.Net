using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;

namespace AC.Composition
{
  /// <summary>
  /// Builder for composition container
  /// </summary>
  /// <typeparam name="TContainer"></typeparam>
  public class ContainerBuilder<TContainer> where TContainer : CompositionContainer
  {
    private AggregateCatalog? _aggregateCatalog = null;

    /// <summary>
    /// Constructor
    /// </summary>
    public ContainerBuilder(params ComposablePartCatalog[] catalogs)
    {
      ApplyCatalogs(catalogs);
    }

    /// <summary>
    /// Add a single catalog
    /// </summary>
    /// <param name="catalogToAdd"></param>
    /// <returns></returns>
    public ContainerBuilder<TContainer> AddCatalog(ComposablePartCatalog catalogToAdd)
    {
      if (_aggregateCatalog == null) throw new NullReferenceException($"No aggregate catalog was generated before!");
      _aggregateCatalog.Catalogs.Add(catalogToAdd);
      return this;
    }

    /// <summary>
    /// Replace all catalogs
    /// </summary>
    /// <param name="catalogs"></param>
    /// <returns></returns>
    public ContainerBuilder<TContainer> ApplyCatalogs(params ComposablePartCatalog[] catalogs)
    {
      _aggregateCatalog = new AggregateCatalog(catalogs);
      return this;
    }

    /// <summary>
    /// Build container
    /// </summary>
    /// <returns></returns>
    public virtual TContainer Build()
    {
      var container = Activator.CreateInstance(typeof(TContainer), _aggregateCatalog) as TContainer;
      if (container == null) throw new NullReferenceException($"Can't build a container instance for type {typeof(TContainer).Name}");
      return container;
    }
  }
}
