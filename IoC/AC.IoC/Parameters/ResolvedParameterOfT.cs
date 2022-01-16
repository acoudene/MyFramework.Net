namespace AC.IoC
{
  public class TDResolvedParameter<TParameter> : ResolvedParameter
  {
    public TDResolvedParameter()
      : base(typeof(TParameter))
    {

    }
    public TDResolvedParameter(string name)
      : base(typeof(TParameter), name)
    {

    }
  }
}
