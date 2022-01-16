using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;
using System.Reflection;

namespace AC.Composition
{
  /// <summary>
  /// Plugin Loader
  /// </summary>
  /// <typeparam name="TPlugin"></typeparam>
  /// <typeparam name="TPluginMetadata"></typeparam>
  public class PluginLoader<TPlugin, TPluginMetadata>
  {
    /// <summary>
    /// Builder to compose container
    /// </summary>
    public ContainerBuilder<CompositionContainer> ContainerBuilder { get; }

    /// <summary>
    /// Plugins set
    /// </summary>
    [ImportMany]
    public IEnumerable<Lazy<TPlugin, TPluginMetadata>>? Plugins
    {
      get;
      set;
    }    

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="exportedValues"></param>
    /// <param name="composableCatalogs"></param>
    public PluginLoader(List<ComposablePartCatalog> composableCatalogs)
    {
      ContainerBuilder = new ContainerBuilder<CompositionContainer>()
        .ApplyCatalogs(composableCatalogs.ToArray());
    }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="path"></param>
    /// <param name="searchPattern"></param>
    /// <param name="exportedValues"></param>
    public PluginLoader(string? path = null, string? searchPattern = null, params ComposablePartCatalog[] composableCatalogs)
    {
      // Example:
      //var catalog = new AggregateCatalog(
      //  // The current Assembly
      //   new AssemblyCatalog(System.Reflection.Assembly.GetExecutingAssembly()),
      //       new DirectoryCatalog(".", "TD.Framework.Presentation.*.dll"),// The Framework
      //       new DirectoryCatalog(".", "TD.Toolkit.Presentation.*.dll")); // The Toolkit      

      string? pluginPath = path;      

      if (string.IsNullOrEmpty(pluginPath))
      {
        // Current path
        string codeBase = Assembly.GetExecutingAssembly().Location;
        UriBuilder uri = new UriBuilder(codeBase);
        string executingAssemblyPath = Uri.UnescapeDataString(uri.Path);
        pluginPath = Path.GetDirectoryName(executingAssemblyPath);
      }

      if (pluginPath == null) throw new NullReferenceException($"{nameof(pluginPath)} is not found!");
      
      DirectoryCatalog? directoryCatalog = null;
      if (!string.IsNullOrEmpty(searchPattern))
      {
        directoryCatalog = new DirectoryCatalog(pluginPath, searchPattern);
      }
      else
      {
        directoryCatalog = new DirectoryCatalog(pluginPath);
      }

      //An aggregate catalog that combines multiple catalogs 
      ContainerBuilder = new ContainerBuilder<CompositionContainer>(composableCatalogs)        
        .AddCatalog(directoryCatalog);      
    }    

    /// <summary>
    /// Call this method to build and load plugins
    /// </summary>
    public void Load()
    {
      ContainerBuilder
        .Build()
        .ComposeParts(this);
    }

    /// <summary>
    /// Load plugins 
    /// </summary>
    /// <param name="compositionContainer"></param>
    public void Load(CompositionContainer compositionContainer)
    {
      if (compositionContainer == null) throw new NullReferenceException("A composition container is expected!");
      compositionContainer.ComposeParts(this);
    }

  } 

}
