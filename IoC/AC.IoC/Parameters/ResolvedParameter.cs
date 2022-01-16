namespace AC.IoC
{
  public class ResolvedParameter : TypedInjectionValueBase
  {
    private readonly string? _name = null;
    public string? Name { get { return _name; } }
    public ResolvedParameter(Type parameterType)
      : base(parameterType)
    {

    }
    public ResolvedParameter(Type parameterType, string name)
      : base(parameterType)
    {
      _name = name;
    }
  }
}
