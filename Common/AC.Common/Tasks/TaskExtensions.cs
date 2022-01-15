namespace AC.Common
{
  /// <summary>
  /// Extensions methods for Tasks
  /// </summary>
  public static class TaskExtensions
  {
#pragma warning disable RECS0165 // Asynchronous methods should return a Task instead of void
    public static async void FireAndForgetSafeAsync(this Task task, IErrorHandler? handler = null)
#pragma warning restore RECS0165 // Asynchronous methods should return a Task instead of void
    {
      try
      {
        await task;
      }
      catch (Exception ex)
      {
        if (handler != null)
          handler.HandleError(ex);
        else
          throw;
      }
    }
  }
}
