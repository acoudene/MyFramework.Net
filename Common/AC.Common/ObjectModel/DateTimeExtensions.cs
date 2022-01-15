namespace AC.Common
{
  /// <summary>
  /// Defines extension methods to the DateTime type
  /// </summary>
  public static class DateTimeExtensions
  {
    /// <summary>
    /// get the current time and set the milliseconds to ZERO
    /// </summary>
    public static DateTime ClearMilliseconds(this DateTime value)
    {
      DateTime dt = new DateTime(value.Year, value.Month, value.Day, value.Hour, value.Minute, value.Second);
      return dt;
    }
  }
}
