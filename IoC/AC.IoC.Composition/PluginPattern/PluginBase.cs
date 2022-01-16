namespace AC.Composition
{
  /// <summary>
  /// Base class for plugin
  /// </summary>
  public abstract class PluginBase
  {
    /// <summary>
    /// Unique identifier
    /// </summary>
    public virtual Guid Guid { get; protected set; }

    /// <summary>
    /// Constructor
    /// </summary>
    protected PluginBase()
    {
      var attr = GetType()
      .GetCustomAttributes(typeof(ExportPluginMetadataAttribute), true)
      .Cast<ExportPluginMetadataAttribute>()
      .SingleOrDefault();

      Guid = (attr == null) ? Guid.Empty : System.Guid.Parse(attr.Guid);
    }
  }
}
