namespace AC.IoC
{
  public class InjectionProperty : InjectionMemberBase
  {
    private readonly string _propertyName;
    private readonly object? _propertyValue = null;

    public string PropertyName { get { return _propertyName; } }
    public object? PropertyValue { get { return _propertyValue; } }

    //
    // Summary:
    //     Configure the container to inject the given property name, resolving the value
    //     via the container.
    //
    // Parameters:
    //   propertyName:
    //     Name of the property to inject.
    public InjectionProperty(string propertyName)
    {
      _propertyName = propertyName;
    }

    //
    // Summary:
    //     Configure the container to inject the given property name, using the value supplied.
    //     This value is converted to an Microsoft.Practices.Unity.InjectionParameterValue
    //     object using the rules defined by the Microsoft.Practices.Unity.InjectionParameterValue.ToParameters(System.Object[])
    //     method.
    //
    // Parameters:
    //   propertyName:
    //     Name of property to inject.
    //
    //   propertyValue:
    //     Value for property.
    public InjectionProperty(string propertyName, object propertyValue)
    {
      _propertyName = propertyName;
      _propertyValue = propertyValue;
    }
  }
}
