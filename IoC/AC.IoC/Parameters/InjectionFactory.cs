namespace AC.IoC
{
  /// <summary>
  /// A class that lets you specify a factory method the container will use to create the object.
  /// </summary>
  /// <remarks>This is a significantly easier way to do the same thing the old static factory extension was used for.</remarks>
  public class InjectionFactory : InjectionMemberBase
  {
    private readonly Func<IIoCProvider, object>? _genericFunc = null;
		private readonly Func<IIoCProvider, Type, string, object>? _byTypeAndIdFunc = null;
		public Func<IIoCProvider, object>? GenericFunc { get { return _genericFunc; } }
		public Func<IIoCProvider, Type, string, object>? ByTypeAndIdFunc { get { return _byTypeAndIdFunc; } }

		/// <summary>
		/// Create a new instance of TDInjectionFactory with the given factory function.
		/// </summary>
		/// <param name="factoryFunc">Factory function.</param>
		public InjectionFactory(Func<IIoCProvider, object> factoryFunc)
    {
			_genericFunc = factoryFunc;
    }

		/// <summary>
		/// Create a new instance of TDInjectionFactory with the given factory function.
		/// </summary>
		/// <param name="factoryFunc">Factory function.</param>
		public InjectionFactory(Func<IIoCProvider, Type, string, object> factoryFunc)
		{
			_byTypeAndIdFunc = factoryFunc;
		}
	}
}
