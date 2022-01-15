namespace AC.Common
{
  /// <summary>
  /// Defines the disposer interface
  /// </summary>
  public interface IDisposer : IDisposable
  {
    /// <summary>
    /// Registers a delegate to call when the disposer is disposed
    /// </summary>
    /// <param name="removeAction">delegate to call</param>
    /// <returns>the disposer instance</returns>
    IDisposer Register(Action removeAction);

    /// <summary>
    /// Registers a IDisposable instance
    /// </summary>
    /// <param name="disposable">instance to register</param>
    /// <returns>the disposer instance</returns>
    IDisposer Register(IDisposable disposable);
  }
}
