namespace AC.IoC
{
  public abstract class TypedInjectionValueBase : InjectionParameterValueBase
  {
    private readonly Type _parameterType;
    public override string ParameterTypeName { get { return _parameterType.Name; } }
    public virtual Type ParameterType { get { return _parameterType; } }

    protected TypedInjectionValueBase(Type parameterType)
    {
      _parameterType = parameterType;
    }
  }
}
