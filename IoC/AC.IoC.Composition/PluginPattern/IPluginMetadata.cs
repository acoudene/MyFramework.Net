using System.ComponentModel;

namespace AC.Composition
{
  /// <summary>
  /// Plugin metadata
  /// </summary>
  public interface IPluginMetadata
  {
    /// <summary>
    /// Unique identifier
    /// </summary>
    string Guid { get; }

    /// <summary>
    /// Order 
    /// </summary>
    [DefaultValue(Int32.MaxValue)]
    int Order { get; }
  }
}
