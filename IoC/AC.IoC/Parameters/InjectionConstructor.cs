namespace AC.IoC
{
  public class InjectionConstructor : InjectionMemberBase
  {
    private readonly object[] _parameterValues;
    public object[] ParameterValues { get { return _parameterValues; } }
    public InjectionConstructor(params object[] parameterValues)
    {
      _parameterValues = parameterValues;
    }
  }
}
