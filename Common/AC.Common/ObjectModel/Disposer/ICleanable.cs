namespace AC.Common
{
  /// <summary>
  /// Définit le comportement d'une classe pouvant être nettoyée.
  /// </summary>
  public interface ICleanable
  {
    /// <summary>
    /// Nettoie l'instance.
    /// </summary>
    void Cleanup();
  }
}
