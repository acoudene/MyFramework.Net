namespace AC.Common
{
  /// <summary>
  /// Retry strategy
  /// </summary>
  public interface IRetryStrategy
  {
    /// <summary>
    /// Retry action
    /// </summary>
    /// <param name="logic"></param>
    void Retry(Action logic);
  }
}