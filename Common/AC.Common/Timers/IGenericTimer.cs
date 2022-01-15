using System.Timers;

namespace AC.Common
{
  /// <summary>
  /// Interface that wraps <see cref="Timer"/>
  /// </summary>
  public interface IGenericTimer
  {
    /// <summary>
    ///  Starts raising the <see cref="Timer.Elapsed"/> event by setting <see cref="Timer.Enabled"/> to <see langword="true"/>
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException">
    /// The <see cref="Timer"/> is created with an interval equal to or greater than <see cref="Int32.MaxValue"/> + 1, or set to an interval less than zero.
    /// </exception>
    void Start();

    /// <summary>
    /// Occurs when the interval elapses.
    /// </summary>
    event ElapsedEventHandler Elapsed;

    /// <summary>
    /// Stops raising the <see cref="Timer.Elapsed"/> event by setting <see cref="Timer.Enabled"/> to <see langword="false"/>
    /// </summary>
    void Stop();
  }
}