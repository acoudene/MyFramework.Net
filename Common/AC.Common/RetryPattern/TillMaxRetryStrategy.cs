namespace AC.Common
{
  /// <summary>
  /// Retry strategy: retry from a number of max retries and interval between them
  /// </summary>
  public class TillMaxRetryStrategy : IRetryStrategy
  {
    private readonly int _maxRetries = default(int);
    private readonly TimeSpan _intervalBetweenRetries = default(TimeSpan);

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="maxRetries"></param>
    /// <param name="intervalBetweenRetries"></param>
    public TillMaxRetryStrategy(int maxRetries, TimeSpan intervalBetweenRetries)
    {
      _maxRetries = maxRetries;
      _intervalBetweenRetries = intervalBetweenRetries;
    }

    /// <summary>
    /// Max retries
    /// </summary>
    public int MaxRetries { get { return _maxRetries; } }

    /// <summary>
    /// Interval to wait for between retries
    /// </summary>
    public TimeSpan IntervalBetweenRetries { get { return _intervalBetweenRetries; } }

    /// <summary>
    /// Retry action
    /// </summary>
    /// <param name="logic"></param>
    public void Retry(Action logic)
    {
      int retries = 0;

      while (retries < _maxRetries)
      {
        try
        {
          retries++;
          logic();
          break;
        }
        catch (Exception ex)
        {
          // log the exception 
          Console.WriteLine(ex.Message);

          if (retries >= _maxRetries)
          {
            throw;
          }
          else
          {
            Console.WriteLine($"Waiting {_intervalBetweenRetries} before retry...");
            Task.Delay(_intervalBetweenRetries).Wait();
          }
        }
      }
    }
  }
}
