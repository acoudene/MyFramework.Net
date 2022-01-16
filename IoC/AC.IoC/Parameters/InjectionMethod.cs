namespace AC.IoC
{
  public class InjectionMethod : InjectionMemberBase
  {
    private readonly string _methodName;
    private readonly object[] _methodParameters;

    public string MethodName { get { return _methodName; } }
    public object[] MethodParameters { get { return _methodParameters; } }

    //
    // Summary:
    //     Create a new Microsoft.Practices.Unity.InjectionMethod instance which will configure
    //     the container to call the given methods with the given parameters.
    //
    // Parameters:
    //   methodName:
    //     Name of the method to call.
    //
    //   methodParameters:
    //     Parameter values for the method.
    public InjectionMethod(string methodName, params object[] methodParameters)
    {
      _methodName = methodName;
      _methodParameters = methodParameters;
    }
  }
}
