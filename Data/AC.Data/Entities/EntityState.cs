namespace AC.Data
{
  /// <summary>
  /// Status of an entity outside database
  /// </summary>
  public enum EntityState
  {
    /// <summary>
    /// Detached
    /// </summary>
    Detached,
    /// <summary>
    /// Unchanged
    /// </summary>
    Unchanged,
    /// <summary>
    /// Added
    /// </summary>
    Added,
    /// <summary>
    /// Updated
    /// </summary>
    Modified,
    /// <summary>
    /// Deleted
    /// </summary>
    Deleted
  }
}