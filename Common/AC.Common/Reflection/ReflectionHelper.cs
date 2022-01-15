using System.Diagnostics;
using System.Linq.Expressions;
using System.Reflection;

namespace AC.Common.Reflection
{
  /// <summary>
  /// Fournit des méthodes permettant de récupérer des informations sur l'appelant d'une méthode
  /// </summary>
  public static class ReflectionHelper
  {
    /// <summary>
    /// Retourne le nom complet qualifié du type à l'origine de l'appel
    /// </summary>
    /// <param name="typesToIgnore">ensemble des types à ignorer</param>
    /// <returns>nom complet qualifié du type à l'origine de l'appel</returns>
    public static string? GetCallingFullTypeName(params Type[] typesToIgnore)
    {
      string? fullName = string.Empty;
      var callingMethod = GetCallingMethod(typesToIgnore);
      if (null != callingMethod && null != callingMethod.ReflectedType)
      {
        fullName = GetCallingMethod(typesToIgnore)?.ReflectedType?.FullName;
      }
      return fullName;
    }

    /// <summary>
    /// Retourne la méthode à l'origine de l'appel
    /// </summary>
    /// <param name="stackFrame">StackFrame correspondant à l'appel</param>
    /// <param name="typesToIgnore">ensemble des types à ignorer</param>
    /// <returns>méthode à l'origine de l'appel</returns>
    public static MethodBase? GetCallingMethod(params Type[] typesToIgnore)
    {
      StackFrame? stackFrame = null;

      List<string> ignoreNames = new List<string>(typesToIgnore.Length);

      foreach (Type type in typesToIgnore)
        ignoreNames.Add(type.Name);
      StackTrace stackTrace = new StackTrace(true);
      MethodBase? method = null;

      for (int i = 0; i < stackTrace.FrameCount; i++)
      {
        stackFrame = stackTrace.GetFrame(i);
        method = stackFrame?.GetMethod();
        if (null != method?.ReflectedType)
        {
          string typeName = method.ReflectedType.Name;
          if (String.Compare(typeName, "ReflectionHelper") != 0 && (ignoreNames.Count == 0 || !ignoreNames.Contains(typeName)))
            break;
        }
      }

      return method;
    }

    /// <summary>
    /// Gets the method represented by the lambda expression.
    /// </summary>
    /// <param name="method">An expression that invokes a method.</param>
    /// <exception cref="ArgumentNullException">The <paramref name="method"/> is null.</exception>
    /// <exception cref="ArgumentException">The <paramref name="method"/> is not a lambda expression or it does not represent a method invocation.</exception>
    /// <returns>The method info.</returns>
    public static MethodInfo GetMethod<TTarget>(Expression<Action<TTarget>> method)
    {
      return GetMethodInfo(method);
    }

    /// <summary>
    /// Gets the method represented by the lambda expression.
    /// </summary>
    /// <param name="method">An expression that invokes a method.</param>
    /// <typeparam name="T1">Type of the first argument.</typeparam>
    /// <exception cref="ArgumentNullException">The <paramref name="method"/> is null.</exception>
    /// <exception cref="ArgumentException">The <paramref name="method"/> is not a lambda expression or it does not represent a method invocation.</exception>
    /// <returns>The method info.</returns>
    public static MethodInfo GetMethod<TTarget, T1>(Expression<Action<TTarget, T1>> method)
    {
      return GetMethodInfo(method);
    }

    /// <summary>
    /// Gets the method represented by the lambda expression.
    /// </summary>
    /// <param name="method">An expression that invokes a method.</param>
    /// <typeparam name="T1">Type of the first argument.</typeparam>
    /// <typeparam name="T2">Type of the second argument.</typeparam>
    /// <exception cref="ArgumentNullException">The <paramref name="method"/> is null.</exception>
    /// <exception cref="ArgumentException">The <paramref name="method"/> is not a lambda expression or it does not represent a method invocation.</exception>
    /// <returns>The method info.</returns>
    public static MethodInfo GetMethod<TTarget, T1, T2>(Expression<Action<TTarget, T1, T2>> method)
    {
      return GetMethodInfo(method);
    }

    /// <summary>
    /// Gets the method represented by the lambda expression.
    /// </summary>
    /// <param name="method">An expression that invokes a method.</param>
    /// <typeparam name="T1">Type of the first argument.</typeparam>
    /// <typeparam name="T2">Type of the second argument.</typeparam>
    /// <typeparam name="T3">Type of the third argument.</typeparam>
    /// <exception cref="ArgumentNullException">The <paramref name="method"/> is null.</exception>
    /// <exception cref="ArgumentException">The <paramref name="method"/> is not a lambda expression or it does not represent a method invocation.</exception>
    /// <returns>The method info.</returns>
    public static MethodInfo GetMethod<TTarget, T1, T2, T3>(Expression<Action<TTarget, T1, T2, T3>> method)
    {
      return GetMethodInfo(method);
    }

    /// <summary>
    /// Gets the property represented by the lambda expression.
    /// </summary>
    /// <param name="property">An expression that accesses a property.</param>
    /// <exception cref="ArgumentNullException">The <paramref name="method"/> is null.</exception>
    /// <exception cref="ArgumentException">The <paramref name="method"/> is not a lambda expression or it does not represent a property access.</exception>
    /// <returns>The property info.</returns>
    public static PropertyInfo GetProperty<TTarget>(Expression<Func<TTarget, object>> property)
    {
      PropertyInfo? info = GetMemberInfo(property) as PropertyInfo;
      if (info == null)
      {
        throw new ArgumentException("Member is not a property");
      }

      return info;
    }

    /// <summary>
    /// Gets the field represented by the lambda expression.
    /// </summary>
    /// <param name="field">An expression that accesses a field.</param>
    /// <exception cref="ArgumentNullException">The <paramref name="method"/> is null.</exception>
    /// <exception cref="ArgumentException">The <paramref name="method"/> is not a lambda expression or it does not represent a field access.</exception>
    /// <returns>The field info.</returns>
    public static FieldInfo GetField<TTarget>(Expression<Func<TTarget, object>> field)
    {
      FieldInfo? info = GetMemberInfo(field) as FieldInfo;
      if (info == null)
      {
        throw new ArgumentException("Member is not a field");
      }

      return info;
    }

    private static MethodInfo GetMethodInfo(Expression method)
    {
      if (method == null)
      {
        throw new ArgumentNullException("method");
      }

      LambdaExpression? lambda = method as LambdaExpression;
      if (lambda == null)
      {
        throw new ArgumentException("Not a lambda expression", "method");
      }

      if (lambda.Body.NodeType != ExpressionType.Call)
      {
        throw new ArgumentException("Not a method call", "method");
      }

      return ((MethodCallExpression)lambda.Body).Method;
    }

    private static MemberInfo GetMemberInfo(Expression member)
    {
      if (member == null)
      {
        throw new ArgumentNullException("member");
      }

      LambdaExpression? lambda = member as LambdaExpression;
      if (lambda == null)
      {
        throw new ArgumentException("Not a lambda expression", "member");
      }

      MemberExpression? memberExpr = null;

      // The Func<TTarget, object> we use returns an object, so first statement can be either 
      // a cast (if the field/property does not return an object) or the direct member access.
      if (lambda.Body.NodeType == ExpressionType.Convert)
      {
        // The cast is an unary expression, where the operand is the 
        // actual member access expression.
        memberExpr = ((UnaryExpression)lambda.Body).Operand as MemberExpression;
      }
      else if (lambda.Body.NodeType == ExpressionType.MemberAccess)
      {
        memberExpr = lambda.Body as MemberExpression;
      }

      if (memberExpr == null)
      {
        throw new ArgumentException("Not a member access", "member");
      }

      return memberExpr.Member;
    }
  }
}
