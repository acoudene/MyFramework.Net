namespace AC.Common
{
  /// <summary>
  /// Handler specific for error to override
  /// </summary>
  public interface IErrorHandler
  {
    void HandleError(Exception ex);
  }
}
