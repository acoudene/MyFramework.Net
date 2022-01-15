namespace AC.Common
{
  /// <summary>
  /// Retry strategy: retry during a specific delay with an interval between them
  /// </summary>
  public class TillDelayLimitRetryStrategy : IRetryStrategy
  {
    private readonly TimeSpan _delayLimit = default(TimeSpan);
    private readonly TimeSpan _intervalBetweenRetries = default(TimeSpan);

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="delayLimit"></param>
    /// <param name="intervalBetweenRetries"></param>
    public TillDelayLimitRetryStrategy(TimeSpan delayLimit, TimeSpan intervalBetweenRetries)
    {
      _delayLimit = delayLimit;
      _intervalBetweenRetries = intervalBetweenRetries;
    }

    /// <summary>
    /// Delay limit of time
    /// </summary>
    public TimeSpan DelayLimit { get { return _delayLimit; } }

    /// <summary>
    /// Interval between retries
    /// </summary>
    public TimeSpan IntervalBetweenRetries { get { return _intervalBetweenRetries; } }

    /// <summary>
    /// Retry action
    /// </summary>
    /// <param name="logic"></param>
    public void Retry(Action logic)
    {
      DateTime startedDate = DateTime.Now;
      DateTime currentNow = startedDate;
      TimeSpan elapsedTime = currentNow - startedDate;
      
      while (elapsedTime < _delayLimit)
      {
        try
        {
          elapsedTime = currentNow - startedDate;
          currentNow = DateTime.Now;
          logic();
          break;
        }
        catch (Exception ex)
        {
          // log the exception 
          Console.WriteLine(ex.Message);

          if (elapsedTime >= _delayLimit)
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
