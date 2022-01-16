namespace AC.IoC
{
  /// <summary>
  /// IoC provider markup interface
  /// </summary>
  public interface IIoCProvider : IDisposable
  {
    /// <summary>
    /// Check if this provider is configured
    /// </summary>
    bool IsConfigured { get; }

    /// <summary>
    /// Configure this provider
    /// </summary>
    void Configure();

    /// <summary>
    /// Create a life time to store instance
    /// </summary>
    /// <typeparam name="TTDLifetimeManager"></typeparam>
    /// <returns></returns>
    TTDLifetimeManager CreateLifetimeManager<TTDLifetimeManager>() where TTDLifetimeManager : LifetimeManagerBase, new();

    ///
    /// Summary:
    ///     Create a child provider.
    ///
    /// Returns:
    ///     The new child provider.
    ///
    /// Remarks:
    ///     A child provider shares the parent's configuration, but can be configured with
    ///     different settings or lifetime.
    IIoCProvider CreateChildProvider();

    ///
    /// Summary:
    ///     Run an existing object through the container, and clean it up.
    ///
    /// Parameters:
    ///   o:
    ///     The object to tear down.
    void Teardown(object instance);

    ///
    /// Summary:
    ///     Run an existing object through the container and perform injection on it.
    ///
    /// Parameters:
    ///   container:
    ///     Container to resolve through.
    ///
    ///   existing:
    ///     Instance to build up.
    ///
    ///   resolverOverrides:
    ///     Any overrides for the buildup.
    ///
    /// Type parameters:
    ///   T:
    ///     System.Type of object to perform injection on.
    ///
    /// Returns:
    ///     The resulting object. By default, this will be existing, but container extensions
    ///     may add things like automatic proxy creation which would cause this to return
    ///     a different object (but still type compatible with T).
    ///
    /// Remarks:
    ///     This method is useful when you don't control the construction of an instance
    ///     (ASP.NET pages or objects created via XAML, for instance) but you still want
    ///     properties and other injection performed.
    ///     This overload uses the default registrations.    
    T BuildUp<T>(T existing);

    ///
    /// Summary:
    ///     Run an existing object through the container and perform injection on it.
    ///
    /// Parameters:
    ///   t:
    ///     System.Type of object to perform injection on.
    ///
    ///   existing:
    ///     Instance to build up.
    ///
    ///   name:
    ///     name to use when looking up the TypeMappings and other configurations.
    ///
    ///   resolverOverrides:
    ///     Any overrides for the resolve calls.
    ///
    /// Returns:
    ///     The resulting object. By default, this will be existing, but container extensions
    ///     may add things like automatic proxy creation which would cause this to return
    ///     a different object (but still type compatible with t).
    ///
    /// Remarks:
    ///     This method is useful when you don't control the construction of an instance
    ///     (ASP.NET pages or objects created via XAML, for instance) but you still want
    ///     properties and other injection performed.
    object BuildUp(Type t, object existing, string name, params ResolverOverrideBase[] resolverOverrides);

    ///
    /// Summary:
    ///     Check if a particular type/name pair has been registered with the container.
    ///
    /// Parameters:
    ///   container:
    ///     Container to inspect.
    ///
    ///   nameToCheck:
    ///     Name to check registration for.
    ///
    /// Type parameters:
    ///   T:
    ///     Type to check registration for.
    ///
    /// Returns:
    ///     True if this type/name pair has been registered, false if not.    
    bool IsRegistered<T>(string nameToCheck);

    ///
    /// Summary:
    ///     Check if a particular type/name pair has been registered with the container.
    ///
    /// Parameters:
    ///   container:
    ///     Container to inspect.
    ///
    ///   typeToCheck:
    ///     Type to check registration for.
    ///
    ///   nameToCheck:
    ///     Name to check registration for.
    ///
    /// Returns:
    ///     True if this type/name pair has been registered, false if not.
    bool IsRegistered(Type typeToCheck, string nameToCheck);

    ///
    /// Summary:
    ///     Check if a particular type has been registered with the container with the default
    ///     name.
    ///
    /// Parameters:
    ///   container:
    ///     Container to inspect.
    ///
    ///   typeToCheck:
    ///     Type to check registration for.
    ///
    /// Returns:
    ///     True if this type has been registered, false if not.
    bool IsRegistered(Type typeToCheck);

    ///
    /// Summary:
    ///     Check if a particular type has been registered with the container with the default
    ///     name.
    ///
    /// Parameters:
    ///   container:
    ///     Container to inspect.
    ///
    /// Type parameters:
    ///   T:
    ///     Type to check registration for.
    ///
    /// Returns:
    ///     True if this type has been registered, false if not.    
    bool IsRegistered<T>();

    ///
    /// Summary:
    ///     Register an instance with the container.
    ///
    /// Parameters:
    ///   container:
    ///     Container to configure.
    ///
    ///   t:
    ///     Type of instance to register (may be an implemented interface instead of the
    ///     full type).
    ///
    ///   instance:
    ///     Object to returned.
    ///
    ///   name:
    ///     Name for registration.
    ///
    /// Returns:
    ///     The IIoCProvider object that this method was called
    ///
    /// Remarks:
    ///     Instance registration is much like setting a type as a singleton, except that
    ///     instead of the container creating the instance the first time it is requested,
    ///     the user creates the instance ahead of type and adds that instance to the container.
    ///     This overload automatically has the container take ownership of the instance.
    IIoCProvider RegisterInstance(Type t, string name, object instance);

    ///
    /// Summary:
    ///     Register an instance with the container.
    ///
    /// Parameters:
    ///   container:
    ///     Container to configure.
    ///
    ///   t:
    ///     Type of instance to register (may be an implemented interface instead of the
    ///     full type).
    ///
    ///   instance:
    ///     Object to returned.
    ///
    ///   lifetimeManager:
    ///     TDLifetimeManagerBase object that controls how this instance
    ///     will be managed by the container.
    ///
    /// Returns:
    ///     The IIoCProvider object that this method was called
    ///
    /// Remarks:
    ///     Instance registration is much like setting a type as a singleton, except that
    ///     instead of the container creating the instance the first time it is requested,
    ///     the user creates the instance ahead of type and adds that instance to the container.
    ///     This overload does a default registration (name = null).
    IIoCProvider RegisterInstance(Type t, object instance, LifetimeManagerBase lifetimeManager);

    ///
    /// Summary:
    ///     Register an instance with the container.
    ///
    /// Parameters:
    ///   container:
    ///     Container to configure.
    ///
    ///   t:
    ///     Type of instance to register (may be an implemented interface instead of the
    ///     full type).
    ///
    ///   instance:
    ///     Object to returned.
    ///
    /// Returns:
    ///     The IIoCProvider object that this method was called
    ///
    /// Remarks:
    ///     Instance registration is much like setting a type as a singleton, except that
    ///     instead of the container creating the instance the first time it is requested,
    ///     the user creates the instance ahead of type and adds that instance to the container.
    ///     This overload does a default registration and has the container take over the
    ///     lifetime of the instance.
    IIoCProvider RegisterInstance(Type t, object instance);

    ///
    /// Summary:
    ///     Register an instance with the container.
    ///
    /// Parameters:
    ///   instance:
    ///     Object to returned.
    ///
    ///   container:
    ///     Container to configure.
    ///
    ///   name:
    ///     Name for registration.
    ///
    ///   lifetimeManager:
    ///     TDLifetimeManagerBase object that controls how this instance
    ///     will be managed by the container.
    ///
    /// Type parameters:
    ///   TInterface:
    ///     Type of instance to register (may be an implemented interface instead of the
    ///     full type).
    ///
    /// Returns:
    ///     The IIoCProvider object that this method was called
    ///
    /// Remarks:
    ///     Instance registration is much like setting a type as a singleton, except that
    ///     instead of the container creating the instance the first time it is requested,
    ///     the user creates the instance ahead of type and adds that instance to the container.
    IIoCProvider RegisterInstance<TInterface>(string name, TInterface instance, LifetimeManagerBase lifetimeManager);

    ///
    /// Summary:
    ///     Register an instance with the container.
    ///
    /// Parameters:
    ///   container:
    ///     Container to configure.
    ///
    ///   instance:
    ///     Object to returned.
    ///
    ///   lifetimeManager:
    ///     TDLifetimeManagerBase object that controls how this instance
    ///     will be managed by the container.
    ///
    /// Type parameters:
    ///   TInterface:
    ///     Type of instance to register (may be an implemented interface instead of the
    ///     full type).
    ///
    /// Returns:
    ///     The IIoCProvider object that this method was called
    ///
    /// Remarks:
    ///     Instance registration is much like setting a type as a singleton, except that
    ///     instead of the container creating the instance the first time it is requested,
    ///     the user creates the instance ahead of type and adds that instance to the container.
    ///     This overload does a default registration (name = null).
    IIoCProvider RegisterInstance<TInterface>(TInterface instance, LifetimeManagerBase lifetimeManager);

    ///
    /// Summary:
    ///     Register an instance with the container.
    ///
    /// Parameters:
    ///   container:
    ///     Container to configure.
    ///
    ///   instance:
    ///     Object to returned.
    ///
    /// Type parameters:
    ///   TInterface:
    ///     Type of instance to register (may be an implemented interface instead of the
    ///     full type).
    ///
    /// Returns:
    ///     The IIoCProvider object that this method was called
    ///
    /// Remarks:
    ///     Instance registration is much like setting a type as a singleton, except that
    ///     instead of the container creating the instance the first time it is requested,
    ///     the user creates the instance ahead of type and adds that instance to the container.
    ///     This overload does a default registration and has the container take over the
    ///     lifetime of the instance.
    IIoCProvider RegisterInstance<TInterface>(TInterface instance);

    ///
    /// Summary:
    ///     Register an instance with the container.
    ///
    /// Parameters:
    ///   instance:
    ///     Object to returned.
    ///
    ///   container:
    ///     Container to configure.
    ///
    ///   name:
    ///     Name for registration.
    ///
    /// Type parameters:
    ///   TInterface:
    ///     Type of instance to register (may be an implemented interface instead of the
    ///     full type).
    ///
    /// Returns:
    ///     The IIoCProvider object that this method was called
    ///
    /// Remarks:
    ///     Instance registration is much like setting a type as a singleton, except that
    ///     instead of the container creating the instance the first time it is requested,
    ///     the user creates the instance ahead of type and adds that instance to the container.
    ///     This overload automatically has the container take ownership of the instance.
    IIoCProvider RegisterInstance<TInterface>(string name, TInterface instance);

    ///
    /// Summary:
    ///     Register an instance with the container.
    ///
    /// Parameters:
    ///   t:
    ///     Type of instance to register (may be an implemented interface instead of the
    ///     full type).
    ///
    ///   instance:
    ///     Object to returned.
    ///
    ///   name:
    ///     Name for registration.
    ///
    ///   lifetime:
    ///     Microsoft.Practices.Unity.LifetimeManager object that controls how this instance
    ///     will be managed by the container.
    ///
    /// Returns:
    ///     The Microsoft.Practices.Unity.UnityContainer object that this method was called
    ///     on (this in C#, Me in Visual Basic).
    ///
    /// Remarks:
    ///     Instance registration is much like setting a type as a singleton, except that
    ///     instead of the container creating the instance the first time it is requested,
    ///     the user creates the instance ahead of type and adds that instance to the container.
    IIoCProvider RegisterInstance(Type t, string name, object instance, LifetimeManagerBase lifetime);

    /// <summary>
    /// Enregistre une instance avec une weak référence
    /// </summary>
    /// <typeparam name="T">type de l'instance</typeparam>
    /// <param name="instance">instance à enregistrer</param>
    IIoCProvider RegisterInstanceWeak<T>(T instance);

    /// <summary>
    /// Enregistre une instance avec son nom en weak référence
    /// </summary>
    /// <typeparam name="T">type de l'instance</typeparam>
    /// <param name="name">nom de l'instance</param>
    /// <param name="instance">instance à enregistrer</param>
    IIoCProvider RegisterInstanceWeak<T>(string name, T instance);

    ///
    /// Summary:
    ///     Register a TDLifetimeManagerBase for the given type and name
    ///     with the container. No type mapping is performed for this type.
    ///
    /// Parameters:
    ///   container:
    ///     Container to configure.
    ///
    ///   t:
    ///     The System.Type to configure in the container.
    ///
    ///   name:
    ///     Name to use for registration, null if a default registration.
    ///
    ///   injectionMembers:
    ///     Injection configuration objects.
    ///
    /// Returns:
    ///     The IIoCProvider object that this method was called
    IIoCProvider RegisterType(Type t, string name, params InjectionMemberBase[] injectionMembers);

    ///
    /// Summary:
    ///     Register a type mapping with the container.
    ///
    /// Parameters:
    ///   container:
    ///     Container to configure.
    ///
    ///   injectionMembers:
    ///     Injection configuration objects.
    ///
    /// Type parameters:
    ///   TFrom:
    ///     System.Type that will be requested.
    ///
    ///   TTo:
    ///     System.Type that will actually be returned.
    ///
    /// Returns:
    ///     The IIoCProvider object that this method was called
    ///
    /// Remarks:
    ///     This method is used to tell the container that when asked for type TFrom, actually
    ///     return an instance of type TTo. This is very useful for getting instances of
    ///     interfaces.
    ///     This overload registers a default mapping and transient lifetime.
    IIoCProvider RegisterType<TFrom, TTo>(params InjectionMemberBase[] injectionMembers) where TTo : TFrom;

    ///
    /// Summary:
    ///     Register a type mapping with the container, where the created instances will
    ///     use the given TDLifetimeManagerBase.
    ///
    /// Parameters:
    ///   container:
    ///     Container to configure.
    ///
    ///   lifetimeManager:
    ///     The TDLifetimeManagerBase that controls the lifetime of the
    ///     returned instance.
    ///
    ///   injectionMembers:
    ///     Injection configuration objects.
    ///
    /// Type parameters:
    ///   TFrom:
    ///     System.Type that will be requested.
    ///
    ///   TTo:
    ///     System.Type that will actually be returned.
    ///
    /// Returns:
    ///     The IIoCProvider object that this method was called
    IIoCProvider RegisterType<TFrom, TTo>(LifetimeManagerBase lifetimeManager, params InjectionMemberBase[] injectionMembers) where TTo : TFrom;

    ///
    /// Summary:
    ///     Register a type mapping with the container.
    ///
    /// Parameters:
    ///   container:
    ///     Container to configure.
    ///
    ///   name:
    ///     Name of this mapping.
    ///
    ///   injectionMembers:
    ///     Injection configuration objects.
    ///
    /// Type parameters:
    ///   TFrom:
    ///     System.Type that will be requested.
    ///
    ///   TTo:
    ///     System.Type that will actually be returned.
    ///
    /// Returns:
    ///     The IIoCProvider object that this method was called
    ///
    /// Remarks:
    ///     This method is used to tell the container that when asked for type TFrom, actually
    ///     return an instance of type TTo. This is very useful for getting instances of
    ///     interfaces.
    IIoCProvider RegisterType<TFrom, TTo>(string name, params InjectionMemberBase[] injectionMembers) where TTo : TFrom;

    ///
    /// Summary:
    ///     Register a type mapping with the container, where the created instances will
    ///     use the given TDLifetimeManagerBase.
    ///
    /// Parameters:
    ///   container:
    ///     Container to configure.
    ///
    ///   name:
    ///     Name to use for registration, null if a default registration.
    ///
    ///   lifetimeManager:
    ///     The TDLifetimeManagerBase that controls the lifetime of the
    ///     returned instance.
    ///
    ///   injectionMembers:
    ///     Injection configuration objects.
    ///
    /// Type parameters:
    ///   TFrom:
    ///     System.Type that will be requested.
    ///
    ///   TTo:
    ///     System.Type that will actually be returned.
    ///
    /// Returns:
    ///     The IIoCProvider object that this method was called
    IIoCProvider RegisterType<TFrom, TTo>(string name, LifetimeManagerBase lifetimeManager, params InjectionMemberBase[] injectionMembers) where TTo : TFrom;

    ///
    /// Summary:
    ///     Register a TDLifetimeManagerBase for the given type with
    ///     the container. No type mapping is performed for this type.
    ///
    /// Parameters:
    ///   container:
    ///     Container to configure.
    ///
    ///   lifetimeManager:
    ///     The TDLifetimeManagerBase that controls the lifetime of the
    ///     returned instance.
    ///
    ///   injectionMembers:
    ///     Injection configuration objects.
    ///
    /// Type parameters:
    ///   T:
    ///     The type to apply the lifetimeManager to.
    ///
    /// Returns:
    ///     The IIoCProvider object that this method was called
    IIoCProvider RegisterType<T>(LifetimeManagerBase lifetimeManager, params InjectionMemberBase[] injectionMembers);

    ///
    /// Summary:
    ///     Register a TDLifetimeManagerBase for the given type with
    ///     the container. No type mapping is performed for this type.
    ///
    /// Parameters:
    ///   container:
    ///     Container to configure.
    ///
    ///   name:
    ///     Name that will be used to request the type.
    ///
    ///   injectionMembers:
    ///     Injection configuration objects.
    ///
    /// Type parameters:
    ///   T:
    ///     The type to configure injection on.
    ///
    /// Returns:
    ///     The IIoCProvider object that this method was called
    IIoCProvider RegisterType<T>(string name, params InjectionMemberBase[] injectionMembers);

    ///
    /// Summary:
    ///     Register a TDLifetimeManagerBase for the given type and name
    ///     with the container. No type mapping is performed for this type.
    ///
    /// Parameters:
    ///   container:
    ///     Container to configure.
    ///
    ///   name:
    ///     Name that will be used to request the type.
    ///
    ///   lifetimeManager:
    ///     The TDLifetimeManagerBase that controls the lifetime of the
    ///     returned instance.
    ///
    ///   injectionMembers:
    ///     Injection configuration objects.
    ///
    /// Type parameters:
    ///   T:
    ///     The type to apply the lifetimeManager to.
    ///
    /// Returns:
    ///     The IIoCProvider object that this method was called
    IIoCProvider RegisterType<T>(string name, LifetimeManagerBase lifetimeManager, params InjectionMemberBase[] injectionMembers);

    ///
    /// Summary:
    ///     Register a TDLifetimeManagerBase for the given type and name
    ///     with the container. No type mapping is performed for this type.
    ///
    /// Parameters:
    ///   container:
    ///     Container to configure.
    ///
    ///   t:
    ///     The System.Type to apply the lifetimeManager to.
    ///
    ///   name:
    ///     Name to use for registration, null if a default registration.
    ///
    ///   lifetimeManager:
    ///     The TDLifetimeManagerBase that controls the lifetime of the
    ///     returned instance.
    ///
    ///   injectionMembers:
    ///     Injection configuration objects.
    ///
    /// Returns:
    ///     The IIoCProvider object that this method was called
    IIoCProvider RegisterType(Type t, string name, LifetimeManagerBase lifetimeManager, params InjectionMemberBase[] injectionMembers);

    ///
    /// Summary:
    ///     Register a type with specific members to be injected.
    ///
    /// Parameters:
    ///   container:
    ///     Container to configure.
    ///
    ///   injectionMembers:
    ///     Injection configuration objects.
    ///
    /// Type parameters:
    ///   T:
    ///     Type this registration is for.
    ///
    /// Returns:
    ///     The IIoCProvider object that this method was called
    IIoCProvider RegisterType<T>(params InjectionMemberBase[] injectionMembers);

    ///
    /// Summary:
    ///     Register a type with specific members to be injected.
    ///
    /// Parameters:
    ///   container:
    ///     Container to configure.
    ///
    ///   t:
    ///     Type this registration is for.
    ///
    ///   injectionMembers:
    ///     Injection configuration objects.
    ///
    /// Returns:
    ///     The IIoCProvider object that this method was called
    IIoCProvider RegisterType(Type t, params InjectionMemberBase[] injectionMembers);

    ///
    /// Summary:
    ///     Register a type mapping with the container.
    ///
    /// Parameters:
    ///   container:
    ///     Container to configure.
    ///
    ///   from:
    ///     System.Type that will be requested.
    ///
    ///   to:
    ///     System.Type that will actually be returned.
    ///
    ///   injectionMembers:
    ///     Injection configuration objects.
    ///
    /// Returns:
    ///     The IIoCProvider object that this method was called
    ///
    /// Remarks:
    ///     This method is used to tell the container that when asked for type from, actually
    ///     return an instance of type to. This is very useful for getting instances of interfaces.
    ///     This overload registers a default mapping.
    IIoCProvider RegisterType(Type from, Type to, params InjectionMemberBase[] injectionMembers);

    ///
    /// Summary:
    ///     Register a type mapping with the container.
    ///
    /// Parameters:
    ///   container:
    ///     Container to configure.
    ///
    ///   from:
    ///     System.Type that will be requested.
    ///
    ///   to:
    ///     System.Type that will actually be returned.
    ///
    ///   name:
    ///     Name to use for registration, null if a default registration.
    ///
    ///   TDInjectionMemberBases:
    ///     Injection configuration objects.
    ///
    /// Returns:
    ///     The IIoCProvider object that this method was called
    ///
    /// Remarks:
    ///     This method is used to tell the container that when asked for type from, actually
    ///     return an instance of type to. This is very useful for getting instances of interfaces.
    IIoCProvider RegisterType(Type from, Type to, string name, params InjectionMemberBase[] injectionMembers);

    ///
    /// Summary:
    ///     Register a type mapping with the container, where the created instances will
    ///     use the given TDLifetimeManagerBase.
    ///
    /// Parameters:
    ///   container:
    ///     Container to configure.
    ///
    ///   from:
    ///     System.Type that will be requested.
    ///
    ///   to:
    ///     System.Type that will actually be returned.
    ///
    ///   lifetimeManager:
    ///     The TDLifetimeManagerBase that controls the lifetime of the
    ///     returned instance.
    ///
    ///   TDInjectionMemberBases:
    ///     Injection configuration objects.
    ///
    /// Returns:
    ///     The IIoCProvider object that this method was called
    IIoCProvider RegisterType(Type from, Type to, LifetimeManagerBase lifetimeManager, params InjectionMemberBase[] injectionMembers);

    ///
    /// Summary:
    ///     Register a TDLifetimeManagerBase for the given type and name
    ///     with the container. No type mapping is performed for this type.
    ///
    /// Parameters:
    ///   container:
    ///     Container to configure.
    ///
    ///   t:
    ///     The System.Type to apply the lifetimeManager to.
    ///
    ///   lifetimeManager:
    ///     The TDLifetimeManagerBase that controls the lifetime of the
    ///     returned instance.
    ///
    ///   TDInjectionMemberBases:
    ///     Injection configuration objects.
    ///
    /// Returns:
    ///     The IIoCProvider object that this method was called
    IIoCProvider RegisterType(Type t, LifetimeManagerBase lifetimeManager, params InjectionMemberBase[] injectionMembers);

    ///
    /// Summary:
    ///     Register a type mapping with the container, where the created instances will
    ///     use the given Microsoft.Practices.Unity.LifetimeManager.
    ///
    /// Parameters:
    ///   from:
    ///     System.Type that will be requested.
    ///
    ///   to:
    ///     System.Type that will actually be returned.
    ///
    ///   name:
    ///     Name to use for registration, null if a default registration.
    ///
    ///   lifetimeManager:
    ///     The Microsoft.Practices.Unity.LifetimeManager that controls the lifetime of the
    ///     returned instance.
    ///
    ///   injectionMembers:
    ///     Injection configuration objects.
    ///
    /// Returns:
    ///     The Microsoft.Practices.Unity.UnityContainer object that this method was called
    ///     on (this in C#, Me in Visual Basic).
    IIoCProvider RegisterType(Type from, Type to, string name, LifetimeManagerBase lifetimeManager, params InjectionMemberBase[] injectionMembers);
  
    /// <summary>
    /// Register a type
    /// </summary>    
    /// <typeparam name="TFrom">Source type</typeparam>
    /// <typeparam name="TTo">Target type</typeparam>
    /// <param name="name"></param>
    /// <param name="asSingleton">Indicate if it should be stored as a singleton for this provider</param>
    IIoCProvider RegisterType<TFrom, TTo>(string name, bool asSingleton)
            where TTo : TFrom;

    /// <summary>
    /// Register a type with a list of contructor injections
    /// </summary>    
    /// <typeparam name="TFrom">Source type</typeparam>
    /// <typeparam name="TTo">Target type</typeparam>
    /// <param name="asSingleton">Indicate if it should be stored as a singleton for this provider</param>
    /// <param name="injectionConstructors">Instances of injection values</param>
    IIoCProvider RegisterType<TFrom, TTo>(bool asSingleton, params object[] injectionConstructors)
            where TTo : TFrom;

    ///
    /// Summary:
    ///     Resolve an instance of the requested type with the given name from the container.
    ///
    /// Parameters:
    ///   container:
    ///     Container to resolve from.
    ///
    ///   name:
    ///     Name of the object to retrieve.
    ///
    ///   overrides:
    ///     Any overrides for the resolve call.
    ///
    /// Type parameters:
    ///   T:
    ///     System.Type of object to get from the container.
    ///
    /// Returns:
    ///     The retrieved object.
    T Resolve<T>(string name, params ResolverOverrideBase[] overrides);

    ///
    /// Summary:
    ///     Resolve an instance of the default requested type from the container.
    ///
    /// Parameters:
    ///   container:
    ///     Container to resolve from.
    ///
    ///   overrides:
    ///     Any overrides for the resolve call.
    ///
    /// Type parameters:
    ///   T:
    ///     System.Type of object to get from the container.
    ///
    /// Returns:
    ///     The retrieved object.
    T Resolve<T>(params ResolverOverrideBase[] overrides);

    ///
    /// Summary:
    ///     Resolve an instance of the default requested type from the container.
    ///
    /// Parameters:
    ///   container:
    ///     Container to resolve from.
    ///
    ///   t:
    ///     System.Type of object to get from the container.
    ///
    ///   overrides:
    ///     Any overrides for the resolve call.
    ///
    /// Returns:
    ///     The retrieved object.
    object Resolve(Type t, params ResolverOverrideBase[] overrides);


    ///
    /// Summary:
    ///     Resolve an instance of the requested type with the given name from the container.
    ///
    /// Parameters:
    ///   t:
    ///     System.Type of object to get from the container.
    ///
    ///   name:
    ///     Name of the object to retrieve.
    ///
    ///   resolverOverrides:
    ///     Any overrides for the resolve call.
    ///
    /// Returns:
    ///     The retrieved object.
    object Resolve(Type t, string name, params ResolverOverrideBase[] resolverOverrides);

    ///
    /// Summary:
    ///     Return instances of all registered types requested.
    ///
    /// Parameters:
    ///   container:
    ///     Container to resolve from.
    ///
    ///   resolverOverrides:
    ///     Any overrides for the resolve calls.
    ///
    /// Type parameters:
    ///   T:
    ///     The type requested.
    ///
    /// Returns:
    ///     Set of objects of type T.
    ///
    /// Remarks:
    ///     This method is useful if you've registered multiple types with the same System.Type
    ///     but different names.
    ///     Be aware that this method does NOT return an instance for the default (unnamed)
    ///     registration.
    IEnumerable<T> ResolveAll<T>(params ResolverOverrideBase[] resolverOverrides);

    ///
    /// Summary:
    ///     Return instances of all registered types requested.
    ///
    /// Parameters:
    ///   t:
    ///     The type requested.
    ///
    ///   resolverOverrides:
    ///     Any overrides for the resolve calls.
    ///
    /// Returns:
    ///     Set of objects of type t.
    ///
    /// Remarks:
    ///     This method is useful if you've registered multiple types with the same System.Type
    ///     but different names.
    ///     Be aware that this method does NOT return an instance for the default (unnamed)
    ///     registration.
    IEnumerable<object> ResolveAll(Type t, params ResolverOverrideBase[] resolverOverrides);

    /// <summary>
    /// Unregister a type
    /// </summary>
    /// <param name="t"></param>
    /// <returns></returns>
    IIoCProvider UnregisterType(Type t);

    /// <summary>
    /// Unregister a type with a specific name
    /// </summary>
    /// <param name="t"></param>
    /// <param name="name"></param>
    /// <returns></returns>
    IIoCProvider UnregisterType(Type t, string name);

    /// <summary>
    /// Unregister a type
    /// </summary>
    /// <returns></returns>
    IIoCProvider UnregisterType<T>();

    /// <summary>
    /// Unregister a type with a specific name
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    IIoCProvider UnregisterType<T>(string name);
  }
}