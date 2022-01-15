namespace AC.Common
{
  /// <summary>
  /// An abstract class that serves as a template in implementing an IMapper
  /// </summary>
  /// <typeparam name="TSource">The Source Type</typeparam>
  /// <typeparam name="TResult">The Result Type</typeparam>
  public abstract class BaseMapper<TSource, TResult> : IMapper<TSource, TResult>
    where TSource : class
    where TResult : class
  {
    /// <summary>
    /// Converts a type of <typeparamref name="TSource"/> to a type of <typeparamref name="TResult"/>
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    public TResult Convert(TSource source)
    {
      var result = CreateInstance(source);
      Map(source, result);
      return result;
    }

    /// <summary>
    /// Override this if the <typeparamref name="TResult"/> object is created via a static class or in a special way
    /// </summary>
    /// <returns>TResult type object</returns>
    protected virtual TResult CreateInstance(TSource source)
    {
      return Activator.CreateInstance<TResult>();
    }

    /// <summary>
    /// An abstract method that is responsible for mapping the source object to the corresponding result object
    /// </summary>
    /// <param name="source"></param>
    /// <param name="result"></param>
    protected abstract void Map(TSource source, TResult result);
  }
}