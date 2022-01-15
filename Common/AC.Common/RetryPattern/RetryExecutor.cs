namespace AC.Common
{
  /// <summary>
  /// Execute some code, retry it if needed before waiting a delay
  /// </summary>
  public class RetryExecutor
  {
    /// <summary>
    /// Retry during a specific delay with a max number of retries and an interval between them
    /// </summary>
    /// <param name="logic"></param>
    /// <param name="periodLimit"></param>
    /// <param name="maxRetriesByPeriod"></param>
    /// <param name="intervalBetweenRetries"></param>
    public static void Retry(Action logic, TimeSpan periodLimit, int maxRetriesByPeriod, TimeSpan intervalBetweenRetries)
    {
      var retryStrategy = new ByPeriodMaxRetryStrategy(periodLimit, maxRetriesByPeriod, intervalBetweenRetries);
      RetryExecutor.Retry(logic, retryStrategy);
    }

    /// <summary>
    /// Retry during a specific delay with an interval between them
    /// </summary>
    /// <param name="logic"></param>
    /// <param name="durationLimit"></param>
    /// <param name="intervalBetweenRetries"></param>
    public static void Retry(Action logic, TimeSpan durationLimit, TimeSpan intervalBetweenRetries)
    {
      var retryStrategy = new TillDelayLimitRetryStrategy(durationLimit, intervalBetweenRetries);
      RetryExecutor.Retry(logic, retryStrategy);
    }

    /// <summary>
    /// Retry from a number of max retries and interval between them
    /// </summary>
    /// <param name="logic"></param>
    /// <param name="maxRetries"></param>
    /// <param name="intervalBetweenRetries"></param>
    public static void Retry(Action logic, int maxRetries, TimeSpan intervalBetweenRetries)
    {
      var retryStrategy = new TillMaxRetryStrategy(maxRetries, intervalBetweenRetries);
      RetryExecutor.Retry(logic, retryStrategy);
    }

    /// <summary>
    /// Retry action from an external strategy
    /// </summary>
    /// <param name="logic"></param>
    /// <param name="retryStrategy"></param>
    public static void Retry(Action logic, IRetryStrategy retryStrategy)
    {
      var retryExecutor = new RetryExecutor(retryStrategy);
      retryExecutor.Retry(logic);
    }

    private readonly IRetryStrategy _retryStrategy;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="retryStrategy"></param>
    public RetryExecutor(IRetryStrategy retryStrategy)
    {
      _retryStrategy = retryStrategy;
    }

    /// <summary>
    /// Retry action
    /// </summary>
    /// <param name="logic"></param>
    public void Retry(Action logic)
    {
      Assert.NotNull(_retryStrategy, $"A {nameof(IRetryStrategy)} is expected!");
      _retryStrategy.Retry(logic);
    }
  }
}
