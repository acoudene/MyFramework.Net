namespace AC.Common
{
  /// <summary>
  /// Helper to manage enum
  /// </summary>
  public static class EnumHelper
  {
    /// <summary>
    /// Convert an object to an enum
    /// </summary>
    /// <typeparam name="TExpectedEnum"></typeparam>
    /// <param name="value"></param>
    /// <returns></returns>
    public static TExpectedEnum Convert<TExpectedEnum>(int value)
    {
      if (!typeof(TExpectedEnum).IsEnum) throw new ArgumentException("Expected type must be an enum to use this function!");

      return Enum.IsDefined(typeof(TExpectedEnum), value)
        ? (TExpectedEnum)Enum.ToObject(typeof(TExpectedEnum), value)
        : throw new ArgumentOutOfRangeException(value.ToString(), string.Format("Value: {0} is not defined in this enum: {1}", value, typeof(TExpectedEnum).FullName));
    }
  }
}
