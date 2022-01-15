namespace AC.Common
{
  /// <summary>
  /// An interface for mapping an object to another object
  /// </summary>
  /// <typeparam name="TSource">The object source</typeparam>
  /// <typeparam name="TResult">The result object</typeparam>
  public interface IMapper<in TSource, out TResult>
    where TSource : class
    where TResult : class
  {
    /// <summary>
    /// Converts the TSource type object to TResult type object
    /// </summary>
    /// <param name="source">The source object</param>
    /// <returns>The transformed object</returns>
    TResult Convert(TSource source);
  }
}