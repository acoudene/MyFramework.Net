using System.ComponentModel.Composition;

namespace AC.Composition
{
  /// <summary>
  /// Attribute to set above plugin
  /// </summary>
  [MetadataAttribute]
  [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
  public sealed class ExportPluginMetadataAttribute : ExportAttribute, IPluginMetadata
  {
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="guid"></param>
    /// <param name="contractType"></param>
    public ExportPluginMetadataAttribute(string guid, Type contractType)
      : base(contractType)
    {
      if (string.IsNullOrEmpty(guid))
        throw new ArgumentException("'guid' is required.", "guid");

      Guid = guid.ToUpper();
      Order = Int32.MaxValue;
    }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="guid"></param>
    /// <param name="contractType"></param>
    /// <param name="order"></param>
    public ExportPluginMetadataAttribute(string guid, Type contractType, int order)
      : base(contractType)
    {
      if (string.IsNullOrEmpty(guid))
        throw new ArgumentException("'guid' is required.", "guid");

      Guid = guid.ToUpper();
      Order = order;
    }

    /// <summary>
    /// Unique identifier
    /// </summary>
    public string Guid { get; private set; }

    /// <summary>
    /// Order
    /// </summary>
    public int Order { get; private set; }
  }
}
