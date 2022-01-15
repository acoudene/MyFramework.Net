using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace AC.Common
{
  public class PropertyChangedManager : IDisposable
  {
    #region Attributs

    private List<Subscription> _subscriptions = new List<Subscription>();
    private WeakReference _source;
    private bool _isDisposed = false;

    #endregion

    #region Constructeur

    public PropertyChangedManager(INotifyPropertyChanged source)
    {
      _source = new WeakReference(source);
    }

    ~PropertyChangedManager()
    {
      Dispose(false);
    }

    #endregion

    #region Méthodes publiques

    public IDisposable SubscribeToPropertyChanged<T>(string propertyName, Action<T> callback)
        where T : INotifyPropertyChanged
    {
      return AddSubscription(new Subscription
      {
        IsStatic = (callback.Target == null),
        PropertyName = propertyName,
        SubscriberReference = new WeakReference(callback.Target),
        MethodCallback = callback.Method
      });
    }

    public void UnsubscribeToPropertyChanged<T>(string propertyName, Action<T> callback)
        where T : INotifyPropertyChanged
    {
      _subscriptions.RemoveWhere(s => s.PropertyName == propertyName && s.MethodCallback == callback.Method);
    }

    public IDisposable AddHandler(PropertyChangedEventHandler handler)
    {
      return AddSubscription(new Subscription
      {
        IsStatic = (handler.Target == null),
        SubscriberReference = new WeakReference(handler.Target),
        MethodCallback = handler.Method
      });
    }

    public void RemoveHandler(PropertyChangedEventHandler handler)
    {
      CleanupSubscribers();

      _subscriptions.RemoveAll(s => s.SubscriberReference?.Target == handler.Target && s.MethodCallback == handler.Method);
    }

    public void NotifyPropertyChanged(string propertyName)
    {
      CleanupSubscribers();

      foreach (var subscription in _subscriptions.Where(s => s.PropertyName == propertyName))
        if (_source?.Target != null)
          subscription?.MethodCallback?.Invoke(subscription?.SubscriberReference?.Target, new object[] { _source?.Target! });

      foreach (var subscription in _subscriptions.Where(s => s.PropertyName == null))
        if (_source?.Target != null)
          subscription?.MethodCallback?.Invoke(subscription?.SubscriberReference?.Target, new object[] { _source?.Target!, new PropertyChangedEventArgs(propertyName) });
    }

    #endregion

    #region Méthodes privées

    private IDisposable AddSubscription(Subscription subscription)
    {
      CleanupSubscribers();

      _subscriptions.Add(subscription);

      return new SubscriptionReference(_subscriptions, subscription);
    }

    private void CleanupSubscribers()
    {
      _subscriptions.RemoveAll(s => !s.IsStatic && s.SubscriberReference!= null && !s.SubscriberReference.IsAlive);
    }

    #endregion

    #region IDisposable Members

    public void Dispose()
    {
      Dispose(true);
      GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
      if (!_isDisposed)
      {
        _isDisposed = true;

        if (disposing)
        {
          if (_subscriptions != null)
            _subscriptions.Clear();
        }
      }
    }

    #endregion

    #region Classes internes

    private sealed class SubscriptionReference : IDisposable
    {
      private List<Subscription> subscriptions;
      private Subscription entry;

      public SubscriptionReference(List<Subscription> subscriptions, Subscription entry)
      {
        this.subscriptions = subscriptions;
        this.entry = entry;
      }

      public void Dispose()
      {
        this.subscriptions.Remove(this.entry);
      }
    }

    private class Subscription
    {
      public bool IsStatic { get; set; }
      public string? PropertyName { get; set; }
      public WeakReference? SubscriberReference { get; set; }
      public MethodInfo? MethodCallback { get; set; }
    }

    #endregion
  }
}
