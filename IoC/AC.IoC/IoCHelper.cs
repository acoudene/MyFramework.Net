
using System.Data;

namespace AC.IoC
{
  /// <summary>
  /// Façade pour l'inversion de contrôle
  /// </summary>
  /// <remarks>un conteneur statique est créé automatiquement</remarks>
  public static class IoCHelper
  {
    #region Attributs

    // Set Freeze design pattern
    private static IIoCProvider? _provider = null;
    private static object _syncRoot = new object();
    #endregion
    #region Properties
    public static IIoCProvider Provider
    {
      get
      {
        if (_provider==null) throw new NullReferenceException("An IoC provider is necessary!");
        return _provider;
      }
      set
      {
        if (Frozen) throw new ReadOnlyException("The IoC provider is frozen, you could unfreeze this object if needed and if it makes sense to your need.");
        _provider = value;
        Frozen = true;
      }
    }

    public static bool Frozen { get; set; }
    #endregion
    
    #region Constructeur

    /// <summary>
    /// Constructeur statique
    /// </summary>
    static IoCHelper()
    {
      Frozen = false;
    }

    public static void Configure()
    {
      lock (_syncRoot)
      {
        Provider.Configure();
      }
    }

    public static void Configure(IIoCProvider provider)
    {
      lock (_syncRoot)
      {
        provider.Configure();
        Provider = provider;
      }
    }

    /// <summary>
    /// Check if this provider is configured
    /// </summary>
    public static bool IsConfigured()
    {
      return _provider != null && _provider.IsConfigured;
    }

    public static TTDLifetimeManager CreateLifetimeManager<TTDLifetimeManager>()
      where TTDLifetimeManager : LifetimeManagerBase, new()
    {
      lock (_syncRoot)
      {
        return Provider.CreateLifetimeManager<TTDLifetimeManager>();
      }
    }

    #endregion

    #region Méthodes publiques

    //
    // Summary:
    //     Create a child container.
    //
    // Returns:
    //     The new child container.
    //
    // Remarks:
    //     A child container shares the parent's configuration, but can be configured with
    //     different settings or lifetime.
    public static IIoCProvider CreateChildProvider()
    {
      lock (_syncRoot)
      {
        return Provider.CreateChildProvider();
      }
    }

    //
    // Summary:
    //     Run an existing object through the container, and clean it up.
    //
    // Parameters:
    //   o:
    //     The object to tear down.
    public static void Teardown(object instance)
    {
      lock (_syncRoot)
      {
        Provider.Teardown(instance);
      }
    }

		//
		// Summary:
		//     Run an existing object through the container and perform injection on it.
		//
		// Parameters:
		//   container:
		//     Container to resolve through.
		//
		//   existing:
		//     Instance to build up.
		//
		//   resolverOverrides:
		//     Any overrides for the buildup.
		//
		// Type parameters:
		//   T:
		//     System.Type of object to perform injection on.
		//
		// Returns:
		//     The resulting object. By default, this will be existing, but container extensions
		//     may add things like automatic proxy creation which would cause this to return
		//     a different object (but still type compatible with T).
		//
		// Remarks:
		//     This method is useful when you don't control the construction of an instance
		//     (ASP.NET pages or objects created via XAML, for instance) but you still want
		//     properties and other injection performed.
		//     This overload uses the default registrations.    
		public static T BuildUp<T>(T existing)
		{
			lock (_syncRoot)
			{
				try
				{
					return Provider.BuildUp(existing);
				}
				catch (IoCException ex)
				{
					throw IoCExceptionHelper.Pass(ex);
				}
				catch (Exception ex)
				{
					throw IoCExceptionHelper.Wrap<IoCException>(ex, ex.GetExceptionDetails());
				}
			}
		}

    //
    // Summary:
    //     Check if a particular type/name pair has been registered with the container.
    //
    // Parameters:
    //   container:
    //     Container to inspect.
    //
    //   nameToCheck:
    //     Name to check registration for.
    //
    // Type parameters:
    //   T:
    //     Type to check registration for.
    //
    // Returns:
    //     True if this type/name pair has been registered, false if not.    
    public static bool IsRegistered<T>(string nameToCheck)
    {
      lock (_syncRoot)
      {
        return Provider.IsRegistered<T>(nameToCheck);
      }
    }

    //
    // Summary:
    //     Check if a particular type/name pair has been registered with the container.
    //
    // Parameters:
    //   container:
    //     Container to inspect.
    //
    //   typeToCheck:
    //     Type to check registration for.
    //
    //   nameToCheck:
    //     Name to check registration for.
    //
    // Returns:
    //     True if this type/name pair has been registered, false if not.
    public static bool IsRegistered(Type typeToCheck, string nameToCheck)
    {
      lock (_syncRoot)
      {
        return Provider.IsRegistered(typeToCheck, nameToCheck);
      }
    }

    //
    // Summary:
    //     Check if a particular type has been registered with the container with the default
    //     name.
    //
    // Parameters:
    //   container:
    //     Container to inspect.
    //
    //   typeToCheck:
    //     Type to check registration for.
    //
    // Returns:
    //     True if this type has been registered, false if not.
    public static bool IsRegistered(Type typeToCheck)
    {
      lock (_syncRoot)
      {
        return Provider.IsRegistered(typeToCheck);
      }
    }

    //
    // Summary:
    //     Check if a particular type has been registered with the container with the default
    //     name.
    //
    // Parameters:
    //   container:
    //     Container to inspect.
    //
    // Type parameters:
    //   T:
    //     Type to check registration for.
    //
    // Returns:
    //     True if this type has been registered, false if not.    
    public static bool IsRegistered<T>()
    {
      lock (_syncRoot)
      {
        return Provider.IsRegistered<T>();
      }
    }

    //
    // Summary:
    //     Register an instance with the container.
    //
    // Parameters:
    //   container:
    //     Container to configure.
    //
    //   t:
    //     Type of instance to register (may be an implemented interface instead of the
    //     full type).
    //
    //   instance:
    //     Object to returned.
    //
    //   name:
    //     Name for registration.
    //
    // Returns:
    //     The IIoCProvider object that this method was called
    //
    // Remarks:
    //     Instance registration is much like setting a type as a singleton, except that
    //     instead of the container creating the instance the first time it is requested,
    //     the user creates the instance ahead of type and adds that instance to the container.
    //     This overload automatically has the container take ownership of the instance.    
    public static IIoCProvider RegisterInstance(Type t, string name, object instance)
    {
      lock (_syncRoot)
      {
        return Provider.RegisterInstance(t, name, instance);
      }
    }

    //
    // Summary:
    //     Register an instance with the container.
    //
    // Parameters:
    //   container:
    //     Container to configure.
    //
    //   t:
    //     Type of instance to register (may be an implemented interface instead of the
    //     full type).
    //
    //   instance:
    //     Object to returned.
    //
    //   lifetimeManager:
    //     TDLifetimeManagerBase object that controls how this instance
    //     will be managed by the container.
    //
    // Returns:
    //     The IIoCProvider object that this method was called
    //
    // Remarks:
    //     Instance registration is much like setting a type as a singleton, except that
    //     instead of the container creating the instance the first time it is requested,
    //     the user creates the instance ahead of type and adds that instance to the container.
    //     This overload does a default registration (name = null).
    public static IIoCProvider RegisterInstance(Type t, object instance, LifetimeManagerBase lifetimeManager)
    {
      lock (_syncRoot)
      {
        return Provider.RegisterInstance(t, instance, lifetimeManager);        
      }
    }

    //
    // Summary:
    //     Register an instance with the container.
    //
    // Parameters:
    //   container:
    //     Container to configure.
    //
    //   t:
    //     Type of instance to register (may be an implemented interface instead of the
    //     full type).
    //
    //   instance:
    //     Object to returned.
    //
    // Returns:
    //     The IIoCProvider object that this method was called
    //
    // Remarks:
    //     Instance registration is much like setting a type as a singleton, except that
    //     instead of the container creating the instance the first time it is requested,
    //     the user creates the instance ahead of type and adds that instance to the container.
    //     This overload does a default registration and has the container take over the
    //     lifetime of the instance.
    public static IIoCProvider RegisterInstance(Type t, object instance)
    {
      lock (_syncRoot)
      {
        return Provider.RegisterInstance(t, instance);        
      }
    }

    //
    // Summary:
    //     Register an instance with the container.
    //
    // Parameters:
    //   instance:
    //     Object to returned.
    //
    //   container:
    //     Container to configure.
    //
    //   name:
    //     Name for registration.
    //
    //   lifetimeManager:
    //     TDLifetimeManagerBase object that controls how this instance
    //     will be managed by the container.
    //
    // Type parameters:
    //   TInterface:
    //     Type of instance to register (may be an implemented interface instead of the
    //     full type).
    //
    // Returns:
    //     The IIoCProvider object that this method was called
    //
    // Remarks:
    //     Instance registration is much like setting a type as a singleton, except that
    //     instead of the container creating the instance the first time it is requested,
    //     the user creates the instance ahead of type and adds that instance to the container.
    public static IIoCProvider RegisterInstance<TInterface>(string name, TInterface instance, LifetimeManagerBase lifetimeManager)
    {
      lock (_syncRoot)
      {
        return Provider.RegisterInstance<TInterface>(name, instance, lifetimeManager);        
      }
    }

    //
    // Summary:
    //     Register an instance with the container.
    //
    // Parameters:
    //   container:
    //     Container to configure.
    //
    //   instance:
    //     Object to returned.
    //
    //   lifetimeManager:
    //     TDLifetimeManagerBase object that controls how this instance
    //     will be managed by the container.
    //
    // Type parameters:
    //   TInterface:
    //     Type of instance to register (may be an implemented interface instead of the
    //     full type).
    //
    // Returns:
    //     The IIoCProvider object that this method was called
    //
    // Remarks:
    //     Instance registration is much like setting a type as a singleton, except that
    //     instead of the container creating the instance the first time it is requested,
    //     the user creates the instance ahead of type and adds that instance to the container.
    //     This overload does a default registration (name = null).
    public static IIoCProvider RegisterInstance<TInterface>(TInterface instance, LifetimeManagerBase lifetimeManager)
    {
      lock (_syncRoot)
      {
        return Provider.RegisterInstance<TInterface>(instance, lifetimeManager);        
      }
    }

    //
    // Summary:
    //     Register an instance with the container.
    //
    // Parameters:
    //   container:
    //     Container to configure.
    //
    //   instance:
    //     Object to returned.
    //
    // Type parameters:
    //   TInterface:
    //     Type of instance to register (may be an implemented interface instead of the
    //     full type).
    //
    // Returns:
    //     The IIoCProvider object that this method was called
    //
    // Remarks:
    //     Instance registration is much like setting a type as a singleton, except that
    //     instead of the container creating the instance the first time it is requested,
    //     the user creates the instance ahead of type and adds that instance to the container.
    //     This overload does a default registration and has the container take over the
    //     lifetime of the instance.
    public static IIoCProvider RegisterInstance<TInterface>(TInterface instance)
    {
      lock (_syncRoot)
      {
        return Provider.RegisterInstance<TInterface>(instance);        
      }
    }

    //
    // Summary:
    //     Register an instance with the container.
    //
    // Parameters:
    //   instance:
    //     Object to returned.
    //
    //   container:
    //     Container to configure.
    //
    //   name:
    //     Name for registration.
    //
    // Type parameters:
    //   TInterface:
    //     Type of instance to register (may be an implemented interface instead of the
    //     full type).
    //
    // Returns:
    //     The IIoCProvider object that this method was called
    //
    // Remarks:
    //     Instance registration is much like setting a type as a singleton, except that
    //     instead of the container creating the instance the first time it is requested,
    //     the user creates the instance ahead of type and adds that instance to the container.
    //     This overload automatically has the container take ownership of the instance.
    public static IIoCProvider RegisterInstance<TInterface>(string name, TInterface instance)
    {
      lock (_syncRoot)
      {
        return Provider.RegisterInstance<TInterface>(name, instance);        
      }
    }
    //
    // Summary:
    //     Register a TDLifetimeManagerBase for the given type and name
    //     with the container. No type mapping is performed for this type.
    //
    // Parameters:
    //   container:
    //     Container to configure.
    //
    //   t:
    //     The System.Type to configure in the container.
    //
    //   name:
    //     Name to use for registration, null if a default registration.
    //
    //   injectionMembers:
    //     Injection configuration objects.
    //
    // Returns:
    //     The IIoCProvider object that this method was called
    public static IIoCProvider RegisterType(Type t, string name, params InjectionMemberBase[] injectionMembers)
    {
      lock (_syncRoot)
      {
        return Provider.RegisterType(t, name, injectionMembers);        
      }
    }

    //
    // Summary:
    //     Register a type mapping with the container.
    //
    // Parameters:
    //   container:
    //     Container to configure.
    //
    //   injectionMembers:
    //     Injection configuration objects.
    //
    // Type parameters:
    //   TFrom:
    //     System.Type that will be requested.
    //
    //   TTo:
    //     System.Type that will actually be returned.
    //
    // Returns:
    //     The IIoCProvider object that this method was called
    //
    // Remarks:
    //     This method is used to tell the container that when asked for type TFrom, actually
    //     return an instance of type TTo. This is very useful for getting instances of
    //     interfaces.
    //     This overload registers a default mapping and transient lifetime.

    public static IIoCProvider RegisterType<TFrom, TTo>(params InjectionMemberBase[] injectionMembers) where TTo : TFrom
    {
      lock (_syncRoot)
      {
        return Provider.RegisterType<TFrom, TTo>(injectionMembers);        
      }
    }
    //
    // Summary:
    //     Register a type mapping with the container, where the created instances will
    //     use the given TDLifetimeManagerBase.
    //
    // Parameters:
    //   container:
    //     Container to configure.
    //
    //   lifetimeManager:
    //     The TDLifetimeManagerBase that controls the lifetime of the
    //     returned instance.
    //
    //   injectionMembers:
    //     Injection configuration objects.
    //
    // Type parameters:
    //   TFrom:
    //     System.Type that will be requested.
    //
    //   TTo:
    //     System.Type that will actually be returned.
    //
    // Returns:
    //     The IIoCProvider object that this method was called
    public static IIoCProvider RegisterType<TFrom, TTo>(LifetimeManagerBase lifetimeManager, params InjectionMemberBase[] injectionMembers) where TTo : TFrom
    {
      lock (_syncRoot)
      {
        return Provider.RegisterType<TFrom, TTo>(lifetimeManager, injectionMembers);        
      }
    }

    //
    // Summary:
    //     Register a type mapping with the container.
    //
    // Parameters:
    //   container:
    //     Container to configure.
    //
    //   name:
    //     Name of this mapping.
    //
    //   injectionMembers:
    //     Injection configuration objects.
    //
    // Type parameters:
    //   TFrom:
    //     System.Type that will be requested.
    //
    //   TTo:
    //     System.Type that will actually be returned.
    //
    // Returns:
    //     The IIoCProvider object that this method was called
    //
    // Remarks:
    //     This method is used to tell the container that when asked for type TFrom, actually
    //     return an instance of type TTo. This is very useful for getting instances of
    //     interfaces.
    public static IIoCProvider RegisterType<TFrom, TTo>(string name, params InjectionMemberBase[] injectionMembers) where TTo : TFrom
    {
      lock (_syncRoot)
      {
        return Provider.RegisterType<TFrom, TTo>(name, injectionMembers);        
      }
    }

    //
    // Summary:
    //     Register a type mapping with the container, where the created instances will
    //     use the given TDLifetimeManagerBase.
    //
    // Parameters:
    //   container:
    //     Container to configure.
    //
    //   name:
    //     Name to use for registration, null if a default registration.
    //
    //   lifetimeManager:
    //     The TDLifetimeManagerBase that controls the lifetime of the
    //     returned instance.
    //
    //   injectionMembers:
    //     Injection configuration objects.
    //
    // Type parameters:
    //   TFrom:
    //     System.Type that will be requested.
    //
    //   TTo:
    //     System.Type that will actually be returned.
    //
    // Returns:
    //     The IIoCProvider object that this method was called
    public static IIoCProvider RegisterType<TFrom, TTo>(string name, LifetimeManagerBase lifetimeManager, params InjectionMemberBase[] injectionMembers) where TTo : TFrom
    {
      lock (_syncRoot)
      {
        return Provider.RegisterType<TFrom, TTo>(name, lifetimeManager, injectionMembers);        
      }
    }

    //
    // Summary:
    //     Register a TDLifetimeManagerBase for the given type with
    //     the container. No type mapping is performed for this type.
    //
    // Parameters:
    //   container:
    //     Container to configure.
    //
    //   lifetimeManager:
    //     The TDLifetimeManagerBase that controls the lifetime of the
    //     returned instance.
    //
    //   injectionMembers:
    //     Injection configuration objects.
    //
    // Type parameters:
    //   T:
    //     The type to apply the lifetimeManager to.
    //
    // Returns:
    //     The IIoCProvider object that this method was called
    public static IIoCProvider RegisterType<T>(LifetimeManagerBase lifetimeManager, params InjectionMemberBase[] injectionMembers)
    {
      lock (_syncRoot)
      {
        return Provider.RegisterType<T>(lifetimeManager, injectionMembers);        
      }
    }

    //
    // Summary:
    //     Register a TDLifetimeManagerBase for the given type with
    //     the container. No type mapping is performed for this type.
    //
    // Parameters:
    //   container:
    //     Container to configure.
    //
    //   name:
    //     Name that will be used to request the type.
    //
    //   injectionMembers:
    //     Injection configuration objects.
    //
    // Type parameters:
    //   T:
    //     The type to configure injection on.
    //
    // Returns:
    //     The IIoCProvider object that this method was called
    public static IIoCProvider RegisterType<T>(string name, params InjectionMemberBase[] injectionMembers)
    {
      lock (_syncRoot)
      {
        return Provider.RegisterType<T>(name, injectionMembers);        
      }
    }

    //
    // Summary:
    //     Register a TDLifetimeManagerBase for the given type and name
    //     with the container. No type mapping is performed for this type.
    //
    // Parameters:
    //   container:
    //     Container to configure.
    //
    //   name:
    //     Name that will be used to request the type.
    //
    //   lifetimeManager:
    //     The TDLifetimeManagerBase that controls the lifetime of the
    //     returned instance.
    //
    //   injectionMembers:
    //     Injection configuration objects.
    //
    // Type parameters:
    //   T:
    //     The type to apply the lifetimeManager to.
    //
    // Returns:
    //     The IIoCProvider object that this method was called
    public static IIoCProvider RegisterType<T>(string name, LifetimeManagerBase lifetimeManager, params InjectionMemberBase[] injectionMembers)
    {
      lock (_syncRoot)
      {
        return Provider.RegisterType<T>(name, lifetimeManager, injectionMembers);        
      }
    }

    //
    // Summary:
    //     Register a TDLifetimeManagerBase for the given type and name
    //     with the container. No type mapping is performed for this type.
    //
    // Parameters:
    //   container:
    //     Container to configure.
    //
    //   t:
    //     The System.Type to apply the lifetimeManager to.
    //
    //   name:
    //     Name to use for registration, null if a default registration.
    //
    //   lifetimeManager:
    //     The TDLifetimeManagerBase that controls the lifetime of the
    //     returned instance.
    //
    //   injectionMembers:
    //     Injection configuration objects.
    //
    // Returns:
    //     The IIoCProvider object that this method was called
    public static IIoCProvider RegisterType(Type t, string name, LifetimeManagerBase lifetimeManager, params InjectionMemberBase[] injectionMembers)
    {
      lock (_syncRoot)
      {
        return Provider.RegisterType(t, name, lifetimeManager, injectionMembers);        
      }
    }

    //
    // Summary:
    //     Register a type with specific members to be injected.
    //
    // Parameters:
    //   container:
    //     Container to configure.
    //
    //   injectionMembers:
    //     Injection configuration objects.
    //
    // Type parameters:
    //   T:
    //     Type this registration is for.
    //
    // Returns:
    //     The IIoCProvider object that this method was called
    public static IIoCProvider RegisterType<T>(params InjectionMemberBase[] injectionMembers)
    {
      lock (_syncRoot)
      {
        return Provider.RegisterType<T>(injectionMembers);        
      }
    }

    //
    // Summary:
    //     Register a type with specific members to be injected.
    //
    // Parameters:
    //   container:
    //     Container to configure.
    //
    //   t:
    //     Type this registration is for.
    //
    //   injectionMembers:
    //     Injection configuration objects.
    //
    // Returns:
    //     The IIoCProvider object that this method was called
    public static IIoCProvider RegisterType(Type t, params InjectionMemberBase[] injectionMembers)
    {
      lock (_syncRoot)
      {
        return Provider.RegisterType(t, injectionMembers);        
      }
    }

    //
    // Summary:
    //     Register a type mapping with the container.
    //
    // Parameters:
    //   container:
    //     Container to configure.
    //
    //   from:
    //     System.Type that will be requested.
    //
    //   to:
    //     System.Type that will actually be returned.
    //
    //   injectionMembers:
    //     Injection configuration objects.
    //
    // Returns:
    //     The IIoCProvider object that this method was called
    //
    // Remarks:
    //     This method is used to tell the container that when asked for type from, actually
    //     return an instance of type to. This is very useful for getting instances of interfaces.
    //     This overload registers a default mapping.
    public static IIoCProvider RegisterType(Type from, Type to, params InjectionMemberBase[] injectionMembers)
    {
      lock (_syncRoot)
      {
        return Provider.RegisterType(from, to, injectionMembers);        
      }
    }

    //
    // Summary:
    //     Register a type mapping with the container.
    //
    // Parameters:
    //   container:
    //     Container to configure.
    //
    //   from:
    //     System.Type that will be requested.
    //
    //   to:
    //     System.Type that will actually be returned.
    //
    //   name:
    //     Name to use for registration, null if a default registration.
    //
    //   TDInjectionMemberBases:
    //     Injection configuration objects.
    //
    // Returns:
    //     The IIoCProvider object that this method was called
    //
    // Remarks:
    //     This method is used to tell the container that when asked for type from, actually
    //     return an instance of type to. This is very useful for getting instances of interfaces.

    public static IIoCProvider RegisterType(Type from, Type to, string name, params InjectionMemberBase[] injectionMembers)
    {
      lock (_syncRoot)
      {
        return Provider.RegisterType(from, to, name, injectionMembers);        
      }
    }

    //
    // Summary:
    //     Register a type mapping with the container, where the created instances will
    //     use the given TDLifetimeManagerBase.
    //
    // Parameters:
    //   container:
    //     Container to configure.
    //
    //   from:
    //     System.Type that will be requested.
    //
    //   to:
    //     System.Type that will actually be returned.
    //
    //   lifetimeManager:
    //     The TDLifetimeManagerBase that controls the lifetime of the
    //     returned instance.
    //
    //   TDInjectionMemberBases:
    //     Injection configuration objects.
    //
    // Returns:
    //     The IIoCProvider object that this method was called
    public static IIoCProvider RegisterType(Type from, Type to, LifetimeManagerBase lifetimeManager, params InjectionMemberBase[] injectionMembers)
    {
      lock (_syncRoot)
      {
        return Provider.RegisterType(from, to, lifetimeManager, injectionMembers);        
      }
    }

    //
    // Summary:
    //     Register a TDLifetimeManagerBase for the given type and name
    //     with the container. No type mapping is performed for this type.
    //
    // Parameters:
    //   container:
    //     Container to configure.
    //
    //   t:
    //     The System.Type to apply the lifetimeManager to.
    //
    //   lifetimeManager:
    //     The TDLifetimeManagerBase that controls the lifetime of the
    //     returned instance.
    //
    //   TDInjectionMemberBases:
    //     Injection configuration objects.
    //
    // Returns:
    //     The IIoCProvider object that this method was called
    public static IIoCProvider RegisterType(Type t, LifetimeManagerBase lifetimeManager, params InjectionMemberBase[] injectionMembers)
    {
      lock (_syncRoot)
      {
        return Provider.RegisterType(t, lifetimeManager, injectionMembers);       
      }
    }

    //
    // Summary:
    //     Resolve an instance of the requested type with the given name from the container.
    //
    // Parameters:
    //   container:
    //     Container to resolve from.
    //
    //   name:
    //     Name of the object to retrieve.
    //
    //   overrides:
    //     Any overrides for the resolve call.
    //
    // Type parameters:
    //   T:
    //     System.Type of object to get from the container.
    //
    // Returns:
    //     The retrieved object.
    public static T Resolve<T>(string name, params ResolverOverrideBase[] overrides)
    {
      lock (_syncRoot)
      {
				try
				{
					return Provider.Resolve<T>(name, overrides);
				}
				catch(IoCException ex)
				{
					throw IoCExceptionHelper.Pass(ex);
				}
				catch(Exception ex)
				{
					throw IoCExceptionHelper.Wrap<IoCException>(ex, ex.GetExceptionDetails());
				}
      }
    }

		//
		// Summary:
		//     Resolve an instance of the default requested type from the container.
		//
		// Parameters:
		//   container:
		//     Container to resolve from.
		//
		//   overrides:
		//     Any overrides for the resolve call.
		//
		// Type parameters:
		//   T:
		//     System.Type of object to get from the container.
		//
		// Returns:
		//     The retrieved object.
		public static T Resolve<T>(params ResolverOverrideBase[] overrides)
		{
			lock (_syncRoot)
			{
				try
				{
					return Provider.Resolve<T>(overrides);
				}
				catch (IoCException ex)
				{
					throw IoCExceptionHelper.Pass(ex);
				}
				catch (Exception ex)
				{
					throw IoCExceptionHelper.Wrap<IoCException>(ex, ex.GetExceptionDetails());
				}
			}
		}

    //
    // Summary:
    //     Resolve an instance of the default requested type from the container.
    //
    // Parameters:
    //   container:
    //     Container to resolve from.
    //
    //   t:
    //     System.Type of object to get from the container.
    //
    //   overrides:
    //     Any overrides for the resolve call.
    //
    // Returns:
    //     The retrieved object.
    public static object Resolve(Type t, params ResolverOverrideBase[] overrides)
    {
			lock (_syncRoot)
			{
				try
				{
					return Provider.Resolve(t, overrides);
				}
				catch (IoCException ex)
				{
					throw IoCExceptionHelper.Pass(ex);
				}
				catch (Exception ex)
				{
					throw IoCExceptionHelper.Wrap<IoCException>(ex, ex.GetExceptionDetails());
				}
			}
    }

		//
		// Summary:
		//     Return instances of all registered types requested.
		//
		// Parameters:
		//   container:
		//     Container to resolve from.
		//
		//   resolverOverrides:
		//     Any overrides for the resolve calls.
		//
		// Type parameters:
		//   T:
		//     The type requested.
		//
		// Returns:
		//     Set of objects of type T.
		//
		// Remarks:
		//     This method is useful if you've registered multiple types with the same System.Type
		//     but different names.
		//     Be aware that this method does NOT return an instance for the default (unnamed)
		//     registration.
		public static IEnumerable<T> ResolveAll<T>(params ResolverOverrideBase[] resolverOverrides)
		{
			lock (_syncRoot)
			{
				try
				{
					return Provider.ResolveAll<T>(resolverOverrides);
				}
				catch (IoCException ex)
				{
					throw IoCExceptionHelper.Pass(ex);
				}
				catch (Exception ex)
				{
					throw IoCExceptionHelper.Wrap<IoCException>(ex, ex.GetExceptionDetails());
				}
			}
		}

    //
    // Summary:
    //     Register an instance with the container.
    //
    // Parameters:
    //   t:
    //     Type of instance to register (may be an implemented interface instead of the
    //     full type).
    //
    //   instance:
    //     Object to returned.
    //
    //   name:
    //     Name for registration.
    //
    //   lifetime:
    //     Microsoft.Practices.Unity.LifetimeManager object that controls how this instance
    //     will be managed by the container.
    //
    // Returns:
    //     The Microsoft.Practices.Unity.UnityContainer object that this method was called
    //     on (this in C#, Me in Visual Basic).
    //
    // Remarks:
    //     Instance registration is much like setting a type as a singleton, except that
    //     instead of the container creating the instance the first time it is requested,
    //     the user creates the instance ahead of type and adds that instance to the container.
    public static IIoCProvider RegisterInstance(Type t, string name, object instance, LifetimeManagerBase lifetime)
    {
      lock (_syncRoot)
      {
        return Provider.RegisterInstance(t, name, instance, lifetime);
      }
    }

    //
    // Summary:
    //     Register a type mapping with the container, where the created instances will
    //     use the given Microsoft.Practices.Unity.LifetimeManager.
    //
    // Parameters:
    //   from:
    //     System.Type that will be requested.
    //
    //   to:
    //     System.Type that will actually be returned.
    //
    //   name:
    //     Name to use for registration, null if a default registration.
    //
    //   lifetimeManager:
    //     The Microsoft.Practices.Unity.LifetimeManager that controls the lifetime of the
    //     returned instance.
    //
    //   injectionMembers:
    //     Injection configuration objects.
    //
    // Returns:
    //     The Microsoft.Practices.Unity.UnityContainer object that this method was called
    //     on (this in C#, Me in Visual Basic).
    public static IIoCProvider RegisterType(Type from, Type to, string name, LifetimeManagerBase lifetimeManager, params InjectionMemberBase[] injectionMembers)
    {
      lock (_syncRoot)
      {
        return Provider.RegisterType(from, to, name, lifetimeManager, injectionMembers);
      }
    }

    //
    // Summary:
    //     Resolve an instance of the requested type with the given name from the container.
    //
    // Parameters:
    //   t:
    //     System.Type of object to get from the container.
    //
    //   name:
    //     Name of the object to retrieve.
    //
    //   resolverOverrides:
    //     Any overrides for the resolve call.
    //
    // Returns:
    //     The retrieved object.
    public static object Resolve(Type t, string name, params ResolverOverrideBase[] resolverOverrides)
    {
      lock (_syncRoot)
      {
				try
				{
					return Provider.Resolve(t, name, resolverOverrides);
				}
				catch (IoCException ex)
				{
					throw IoCExceptionHelper.Pass(ex);
				}
				catch (Exception ex)
				{
					throw IoCExceptionHelper.Wrap<IoCException>(ex, ex.GetExceptionDetails());
				}
			}
    }

		//
		// Summary:
		//     Return instances of all registered types requested.
		//
		// Parameters:
		//   t:
		//     The type requested.
		//
		//   resolverOverrides:
		//     Any overrides for the resolve calls.
		//
		// Returns:
		//     Set of objects of type t.
		//
		// Remarks:
		//     This method is useful if you've registered multiple types with the same System.Type
		//     but different names.
		//     Be aware that this method does NOT return an instance for the default (unnamed)
		//     registration.
		public static IEnumerable<object> ResolveAll(Type t, params ResolverOverrideBase[] resolverOverrides)
		{
			lock (_syncRoot)
			{
				try
				{
					return Provider.ResolveAll(t, resolverOverrides);
				}
				catch (IoCException ex)
				{
					throw IoCExceptionHelper.Pass(ex);
				}
				catch (Exception ex)
				{
					throw IoCExceptionHelper.Wrap<IoCException>(ex, ex.GetExceptionDetails());
				}
			}
		}


    //
    // Summary:
    //     Run an existing object through the container and perform injection on it.
    //
    // Parameters:
    //   t:
    //     System.Type of object to perform injection on.
    //
    //   existing:
    //     Instance to build up.
    //
    //   name:
    //     name to use when looking up the TypeMappings and other configurations.
    //
    //   resolverOverrides:
    //     Any overrides for the resolve calls.
    //
    // Returns:
    //     The resulting object. By default, this will be existing, but container extensions
    //     may add things like automatic proxy creation which would cause this to return
    //     a different object (but still type compatible with t).
    //
    // Remarks:
    //     This method is useful when you don't control the construction of an instance
    //     (ASP.NET pages or objects created via XAML, for instance) but you still want
    //     properties and other injection performed.
    public static object BuildUp(Type t, object existing, string name, params ResolverOverrideBase[] resolverOverrides)
    {
      lock (_syncRoot)
      {
				try
				{
					return Provider.BuildUp(t, existing, name, resolverOverrides);
				}
				catch (IoCException ex)
				{
					throw IoCExceptionHelper.Pass(ex);
				}
				catch (Exception ex)
				{
					throw IoCExceptionHelper.Wrap<IoCException>(ex, ex.GetExceptionDetails());
				}
			}
    }


    #region specific

    /// <summary>
    /// Enregistre un type
    /// </summary>
    /// <typeparam name="TFrom">type source</typeparam>
    /// <typeparam name="TTo">type cible</typeparam>
    /// <param name="asSingleton">indique si lors de sa résolution, le type doit être conservé comme un singleton</param>
    public static IIoCProvider RegisterType<TFrom, TTo>(string name, bool asSingleton)
            where TTo : TFrom
    {
      lock (_syncRoot)
      {
        if (asSingleton)
        {
          return Provider.RegisterType<TFrom, TTo>(name, new ContainerControlledLifetimeManager());
        }
        else
        {
          return Provider.RegisterType<TFrom, TTo>(name);
        }
      }
    }

    public static IIoCProvider RegisterType<TFrom, TTo>(bool asSingleton, params object[] injectionConstructors)
            where TTo : TFrom
    {
      lock (_syncRoot)
      {
        if (asSingleton)
        {
          return Provider.RegisterType<TFrom, TTo>(new ContainerControlledLifetimeManager()
            , injectionConstructors.Select(i => new InjectionConstructor(injectionConstructors)).ToArray());
        }
        else
        {
          return Provider.RegisterType<TFrom, TTo>(injectionConstructors.Select(i => new InjectionConstructor(injectionConstructors)).ToArray());
        }
      }
    }


    /// <summary>
    /// Enregistre une instance avec une weak référence
    /// </summary>
    /// <typeparam name="T">type de l'instance</typeparam>
    /// <param name="instance">instance à enregistrer</param>
    public static IIoCProvider RegisterInstanceWeak<T>(T instance)
    {
      lock (_syncRoot)
      {
        return Provider.RegisterInstance<T>(instance, new TDExternallyControlledLifetimeManager());
      }
    }


    /// <summary>
    /// Enregistre une instance avec son nom en weak référence
    /// </summary>
    /// <typeparam name="T">type de l'instance</typeparam>
    /// <param name="name">nom de l'instance</param>
    /// <param name="instance">instance à enregistrer</param>
    public static IIoCProvider RegisterInstanceWeak<T>(string name, T instance)
    {
      lock (_syncRoot)
      {
        return Provider.RegisterInstance<T>(name, instance, new TDExternallyControlledLifetimeManager());
      }
    }
    #endregion
    #endregion
  }
}
