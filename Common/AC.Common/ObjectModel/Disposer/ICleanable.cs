namespace AC.Common
{
  /// <summary>
  /// D�finit le comportement d'une classe pouvant �tre nettoy�e.
  /// </summary>
  public interface ICleanable
  {
    /// <summary>
    /// Nettoie l'instance.
    /// </summary>
    void Cleanup();
  }
}
