using System.Timers;

namespace AC.Common
{
  /// <summary>
  /// Default implementation of <see cref="IGenericTimer"/>
  /// </summary>
  public class SimpleTimer : IGenericTimer, IDisposable
  {
    private const double OneSecondInMs = 1000;

    /// <summary>
    /// Default interval is 15 seconds.
    /// </summary>
    public const double DefaultIntervalInMs = 15 * OneSecondInMs;

    /// <summary>
    /// The internal timer
    /// </summary>
    private readonly System.Timers.Timer _timer;

    /// <summary>
    /// Given Interval
    /// </summary>
    public double IntervalInMs { get; }

    /// <summary>
    /// Instantiates <see cref="SimpleTimer"/> with the given <paramref name="intervalInMs"/>.
    /// </summary>
    /// <remarks>
    /// When <paramref name="intervalInMs"/> is less than or equal to zero, the interval will be set to <see cref="DefaultIntervalInMs"/>
    /// </remarks>
    /// <param name="intervalInMs"></param>
    public SimpleTimer(double intervalInMs = 0)
    {
      if (intervalInMs <= 0)
        intervalInMs = DefaultIntervalInMs;
      IntervalInMs = intervalInMs;
      _timer = new System.Timers.Timer
      {
        Interval = IntervalInMs
      };
    }

    private readonly List<ElapsedEventHandler> _elapsedEventHandlers = new List<ElapsedEventHandler>();

    /// <summary>
    /// Occurs when the interval elapses.
    /// </summary>
    public event ElapsedEventHandler Elapsed
    {
      add
      {
        _timer.Elapsed += value;
        _elapsedEventHandlers.Add(value);
      }
      remove
      {
        _timer.Elapsed -= value;
        _elapsedEventHandlers.Remove(value);
      }
    }

    /// <summary>
    ///  Starts raising the <see cref="Timer.Elapsed"/> event by setting <see cref="Timer.Enabled"/> to <see langword="true"/>
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException">
    /// The <see cref="Timer"/> is created with an interval equal to or greater than <see cref="Int32.MaxValue"/> + 1, or set to an interval less than zero.
    /// </exception>
    public void Start() => _timer.Start();

    /// <summary>
    /// Stops raising the <see cref="Timer.Elapsed"/> event by setting <see cref="Timer.Enabled"/> to <see langword="false"/>
    /// </summary>
    public void Stop() => _timer.Stop();

    #region IDisposable Support
    private bool _disposedValue = false; // To detect redundant calls

    /// <summary>
    /// Dispose process
    /// </summary>
    /// <param name="disposing"></param>
    protected virtual void Dispose(bool disposing)
    {
      if (!_disposedValue)
      {
        if (disposing)
        {
          // dispose managed state (managed objects).
          Stop();
          _elapsedEventHandlers.ForEach(e => _timer.Elapsed -= e);
          _timer.Dispose();
        }

        _disposedValue = true;
      }
    }

    /// <summary>
    /// This code added to correctly implement the disposable pattern.
    /// </summary>
    public void Dispose()
    {
      // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
      Dispose(true);
    }
    #endregion
  }
}