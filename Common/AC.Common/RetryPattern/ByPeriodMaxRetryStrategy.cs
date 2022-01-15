namespace AC.Common
{
  /// <summary>
  /// Retry strategy: retry during a specific delay with a max number of retries and an interval between them
  /// </summary>
  public class ByPeriodMaxRetryStrategy : IRetryStrategy
  {
    private readonly TimeSpan _periodLimit = default(TimeSpan);
    private readonly int _maxRetriesByPeriod = default(int);
    private readonly TimeSpan _intervalBetweenRetries = default(TimeSpan);

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="periodLimit"></param>
    /// <param name="maxRetriesByPeriod"></param>
    /// <param name="intervalBetweenRetries"></param>
    public ByPeriodMaxRetryStrategy(TimeSpan periodLimit, int maxRetriesByPeriod, TimeSpan intervalBetweenRetries)
    {
      _periodLimit = periodLimit;
      _maxRetriesByPeriod = maxRetriesByPeriod;
      _intervalBetweenRetries = intervalBetweenRetries;
    }

    /// <summary>
    /// Period limit of time
    /// </summary>
    public TimeSpan PeriodLimit { get { return _periodLimit; } }

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

      DateTime startedDate = DateTime.Now;
      DateTime currentNow = startedDate;
      TimeSpan elapsedTime = currentNow - startedDate;

      while (retries < _maxRetriesByPeriod)
      {
        try
        {
          retries++;
          elapsedTime = currentNow - startedDate;
          currentNow = DateTime.Now;
          logic();
          break;
        }
        catch (Exception ex)
        {
          // log the exception 
          Console.WriteLine(ex.Message);

          if (elapsedTime >= _periodLimit)
          {
            Console.WriteLine($"New period begins, {retries} retries have been done during this period.");
            startedDate = DateTime.Now;
            retries = 0;
          }
          else
          {
            if (retries >= _maxRetriesByPeriod)
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
}
