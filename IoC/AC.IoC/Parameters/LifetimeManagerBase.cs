namespace AC.IoC
{
  public abstract class LifetimeManagerBase
  {
    public virtual Func<object>? GetValue { get; set; }
    public virtual Action<object>? SetValue { get; set; }
    public virtual Action? RemoveValue { get; set; }
  }
}
