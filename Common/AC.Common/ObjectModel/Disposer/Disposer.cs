namespace AC.Common
{
  // #warning Faire un exemple

  public class Disposer : IDisposable, IDisposer
  {
    private bool _isDisposed;
    private IList<WeakReference> _items;
    private IList<Action> _actions;

    public Disposer()
    {
      _items = new List<WeakReference>();
      _actions = new List<Action>();
    }

    public IDisposer Register(IDisposable disposable)
    {
      _items.Add(new WeakReference(disposable));
      return this;
    }

    public IDisposer Register(Action removeAction)
    {
      _actions.Add(removeAction);
      return this;
    }

    #region IDisposable Members

    public void Dispose()
    {
      if (!_isDisposed)
      {
        _isDisposed = true;

        while (_actions.Count > 0)
        {
          var action = _actions[0];
          action();
          _actions.RemoveAt(0);
        }

        while (_items.Count > 0)
        {
          WeakReference wr = _items[0];
          if (wr.IsAlive)
          {
            (wr.Target as IDisposable)?.Dispose();
            wr.Target = null;
          }

          _items.RemoveAt(0);
        }
      }
    }

    #endregion
  }
}
