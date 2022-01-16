namespace AC.IoC
{
  public class OrderedParametersOverride : ResolverOverrideBase
  {
    private readonly IEnumerable<object> _parameterValues;
    public IEnumerable<object> ParameterValues { get { return _parameterValues; } }
    public OrderedParametersOverride(IEnumerable<object> parameterValues)
    {
      _parameterValues = parameterValues;
    }
  }
}
