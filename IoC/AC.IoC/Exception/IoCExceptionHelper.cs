using System.Collections;
using System.Text;

namespace AC.IoC
{
  /// <summary>
  /// Gère les exceptions à lever en permettant leur traçage
  /// </summary>
  public static class IoCExceptionHelper
  {
    #region Methode Pass

    /// <summary>
    /// Fait passer l'exception telle quelle
    /// </summary>
    /// <param name="exception">exception à passer</param>
    public static Exception Pass(Exception exception)
    {
      Trace(exception);
      return exception;
    }

    #endregion

    #region Methodes Create

    /// <summary>
    /// Crée une nouvelle exception
    /// </summary>
    /// <param name="message">message de l'exception</param>
    /// <typeparam name="TException">type de l'exception à créer</typeparam>
    public static Exception Create<TException>(string message)
        where TException : Exception
    {
      try
      {
        TException? exception = Activator.CreateInstance(typeof(TException), new object[] { message }) as TException;
        if (exception == null) throw new NullReferenceException($"Can't create an exception of type {typeof(TException).Name}");
        Trace(exception!);
        return exception!;
      }
      catch (TException ex)
      {
        return ex;
      }
      catch (Exception ex)
      {
        return Wrap<IoCException>(ex);
      }
    }

    /// <summary>
    /// Crée une nouvelle exception avec un message formaté
    /// </summary>
    /// <typeparam name="TException">type de l'exception à créer</typeparam>
    /// <param name="format">format du message</param>
    /// <param name="args">parametres du message</param>
    public static Exception Create<TException>(string format, params object[] args)
        where TException : Exception
    {
      try
      {
        TException? exception = Activator.CreateInstance(typeof(TException), new object[] { String.Format(format, args) }) as TException;
        if (exception == null) throw new NullReferenceException($"Can't create an exception of type {typeof(TException).Name}");
        Trace(exception!);
        return exception!;
      }
      catch (TException ex)
      {
        return ex;
      }
      catch (Exception ex)
      {
        return Wrap<IoCException>(ex);
      }
    }

    #endregion

    #region Methodes Replace

    /// <summary>
    /// Remplace une exception par une ExceptionBase
    /// </summary>
    /// <typeparam name="TException">type de l'exception</typeparam>
    /// <param name="exception">exception à remplacer</param>
    /// <remarks>Le message de la nouvelle exception sera le meme que celui de l'exception fournie</remarks>
    public static Exception Replace<TException>(Exception exception)
        where TException : Exception
    {
      try
      {
        if (exception == null) throw new ArgumentNullException(nameof(exception));

        TException? newException = Activator.CreateInstance(typeof(TException), new object[] { exception.Message }) as TException;
        if (newException == null) throw new NullReferenceException($"Can't create an exception of type {typeof(TException).Name}");
        Trace(newException!);
        return newException!;
      }
      catch (TException ex)
      {
        return ex;
      }
      catch (Exception ex)
      {
        return Wrap<IoCException>(ex);
      }
    }

    /// <summary>
    /// Remplace une exception par une ExceptionBase
    /// </summary>
    /// <typeparam name="TException">type de l'exception</typeparam>
    /// <param name="exception">exception à remplacer</param>
    /// <param name="message">message de l'exception</param>
    public static Exception Replace<TException>(Exception exception, string message)
        where TException : Exception
    {
      try
      {
        if (exception == null) throw new ArgumentNullException(nameof(exception));

        TException? newException = Activator.CreateInstance(typeof(TException), new object[] { message }) as TException;
        if (newException == null) throw new NullReferenceException($"Can't create an exception of type {typeof(TException).Name}");
        Trace(newException!);
        return newException!;
      }
      catch (TException ex)
      {
        return ex;
      }
      catch (Exception ex)
      {
        return Wrap<IoCException>(ex);
      }
    }

    /// <summary>
    /// Remplace une exception par une ExceptionBase
    /// </summary>
    /// <typeparam name="TException">type de l'exception</typeparam>
    /// <param name="exception">exception à remplacer</param>
    /// <param name="format">format du message</param>
    /// <param name="args">parametres du message</param>
    public static Exception Replace<TException>(Exception exception, string format, params object[] args)
        where TException : Exception
    {
      try
      {
        if (exception == null) throw new ArgumentNullException(nameof(exception));
        
        TException? newException = Activator.CreateInstance(typeof(TException), new object[] { String.Format(format, args) }) as TException;
        if (newException == null) throw new NullReferenceException($"Can't create an exception of type {typeof(TException).Name}");
        Trace(newException!);
        return newException!;
      }
      catch (TException ex)
      {
        return ex;
      }
      catch (Exception ex)
      {
        return Wrap<IoCException>(ex);
      }
    }

    #endregion

    #region Methodes Wrap

    /// <summary>
    /// Encapsule une exception dans une autre exception
    /// </summary>
    /// <typeparam name="TException">type de l'exception à créer</typeparam>
    /// <param name="innerException">exception à encapsuler</param>
    /// <remarks>Le message de la nouvelle exception sera le meme que celui de l'exception fournie</remarks>
    public static Exception Wrap<TException>(Exception innerException)
        where TException : Exception
    {
      try
      {
        if (innerException == null) throw new ArgumentNullException(nameof(innerException));
        
        TException? newException = Activator.CreateInstance(typeof(TException), new object[] { innerException.Message, innerException }) as TException;
        if (newException == null) throw new NullReferenceException($"Can't create an exception of type {typeof(TException).Name}");
        Trace(newException!);
        return newException!;
      }
      catch (TException ex)
      {
        return ex;
      }
      catch (Exception ex)
      {
        return Wrap<IoCException>(ex);
      }
    }

    /// <summary>
    /// Encapsule une exception dans une autre exception
    /// </summary>
    /// <typeparam name="TException">type de l'exception à créer</typeparam>
    /// <param name="innerException">exception à encapsuler</param>
    /// <param name="message">message de l'exception à créer</param>
    public static Exception Wrap<TException>(Exception innerException, string message)
        where TException : Exception
    {
      try
      {
        if (innerException==null) throw new ArgumentNullException(nameof(innerException));
 
        TException? newException = Activator.CreateInstance(typeof(TException), new object[] { message, innerException }) as TException;
        if (newException == null) throw new NullReferenceException($"Can't create an exception of type {typeof(TException).Name}");
        Trace(newException!);
        return newException!;
      }
      catch (TException ex)
      {
        return ex;
      }
      catch (Exception ex)
      {
        return Wrap<IoCException>(ex);
      }
    }

    /// <summary>
    /// Encapsule une exception dans une autre exception
    /// </summary>
    /// <typeparam name="TException">type de l'exception à créer</typeparam>
    /// <param name="innerException">exception à encapsuler</param>
    /// <param name="format">format du message</param>
    /// <param name="args">parametres du message</param>
    public static Exception Wrap<TException>(Exception innerException, string format, params object[] args)
        where TException : Exception
    {
      try
      {
        if (innerException==null) throw new ArgumentNullException(nameof(innerException));

        TException? newException = Activator.CreateInstance(typeof(TException), new object[] { String.Format(format, args), innerException }) as TException;
        if (newException == null) throw new NullReferenceException($"Can't create an exception of type {typeof(TException).Name}");
        Trace(newException!);
        return newException!;
      }
      catch (TException ex)
      {
        return ex;
      }
      catch (Exception ex)
      {
        return Wrap<IoCException>(ex);
      }
    }

    #endregion

    #region Exception stack through the inner exception if there are any.
    /// <summary>
    /// Exception stack through the inner exception if there are any.
    /// </summary>
    /// <param name="exception"></param>
    /// <param name="stack"></param>
    /// <returns></returns>
    public static StringBuilder BuildExceptionDetail(Exception exception, StringBuilder stack)
    {
      stack.AppendLine("Message: " + exception.Message);

      if (!string.IsNullOrEmpty(exception.Source)) stack.AppendLine("Source: " + exception.Source);
      if (exception.TargetSite != null && !string.IsNullOrEmpty(exception.TargetSite.ToString()))
      {
        stack.AppendLine("TargetSite: " + exception.TargetSite);
      }

      if (!string.IsNullOrEmpty(exception.StackTrace))
      {
        stack.AppendLine("StackTrace: " + exception.StackTrace);
      }

      // loop recursivly through the inner exception if there are any.
      if (exception.InnerException != null)
      {
        stack.AppendLine("InnerException: ");
        BuildExceptionDetail(exception.InnerException, stack);
      }

      return stack;
    }
    /// <summary>
    /// Build exception detail including data dictionary if any
    /// </summary>
    /// <param name="exception"></param>
    /// <returns></returns>
    public static StringBuilder BuildExceptionDetail(Exception exception)
    {
      StringBuilder errorMsg = new StringBuilder();
      errorMsg.AppendLine("Message: " + exception.Message);
      errorMsg.AppendLine("Source: " + exception.Source);

      foreach (DictionaryEntry de in exception.Data)
      {
        errorMsg.AppendLine(de.Key + ": " + de.Value);
      }

      // loop recursivly through the inner exception if there are any.
      if (exception.InnerException != null)
      {
        errorMsg.AppendLine("InnerException: ");
        BuildExceptionDetail(exception.InnerException, errorMsg);
      }

      return errorMsg;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="exception"></param>
    /// <returns></returns>
    public static string GetExceptionDetails(this Exception exception)
    {
      return IoCExceptionHelper.BuildExceptionDetail(exception).ToString();
    }


    #endregion

    #region Methodes privées

    /// <summary>
    /// Trace l'exception fournie
    /// </summary>
    /// <param name="exception">exception à tracer</param>
    private static void Trace(Exception exception)
    {
      try
      {
        if (exception==null) throw new ArgumentNullException(nameof(exception), "The exception to trace is null!");

        Console.WriteLine(exception.GetExceptionDetails());
      }
      catch (Exception ex)
      {
        Console.WriteLine(string.Format("Exception while tracing error with details: {0}", ex.GetFullExceptionMessage()));
        throw;
      }
    }

    /// <summary>
    /// Get the complete exception message
    /// </summary>
    /// <param name="ex"></param>
    /// <returns></returns>
    public static string GetFullExceptionMessage(this Exception ex)
    {
      string message;
      StringBuilder builder = new StringBuilder();
      Exception? currentEx = ex;
      while (currentEx != null)
      {
        builder.Append(currentEx.Message);
        builder.Append(". ");
        currentEx = currentEx.InnerException;
      }
      message = builder.ToString();
      return message;
    }

    #endregion
  }
}
