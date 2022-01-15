using System.Collections.ObjectModel;

namespace AC.Common
{
  /// <summary>
  /// ObservableCollection fournissant des traitements par lot
  /// </summary>
  /// <typeparam name="T">type d'objets gérés par la collection</typeparam>
  public class BulkObservableCollection<T> : ObservableCollection<T>
  {
    #region Constructors

    public BulkObservableCollection()
        : base()
    {

    }

    public BulkObservableCollection(IEnumerable<T> collection)
        : base(collection)
    {
    }

    public BulkObservableCollection(List<T> list)
        : base(list)
    {
    }

    #endregion

    #region Public methods

    public void AddRange(IEnumerable<T> items)
    {
      if (items == null)
        throw new ArgumentNullException("items");

      int index = base.Count;

      foreach (var item in items)
        base.Items.Add(item);

      OnCollectionChanged(new System.Collections.Specialized.NotifyCollectionChangedEventArgs(System.Collections.Specialized.NotifyCollectionChangedAction.Add, new List<T>(items), index));
    }

    public void RemoveRange(IEnumerable<T> items)
    {
      if (items == null)
        throw new ArgumentNullException("items");

      foreach (var item in items)
        base.Items.Remove(item);

      OnCollectionChanged(new System.Collections.Specialized.NotifyCollectionChangedEventArgs(System.Collections.Specialized.NotifyCollectionChangedAction.Remove, new List<T>(items), 0));
    }

    public void RemoveWhere(Func<T, bool> predicate)
    {
      if (predicate == null)
        throw new ArgumentNullException("predicate");

      RemoveRange(base.Items.Where(predicate));
    }

    public void ReplaceAll(IEnumerable<T> items)
    {
      if (items == null)
        throw new ArgumentNullException("items");

      var oldItems = new List<T>(base.Items);

      base.Items.Clear();

      foreach (var item in items)
        base.Items.Add(item);

      base.OnPropertyChanged(new System.ComponentModel.PropertyChangedEventArgs("Count"));
      base.OnPropertyChanged(new System.ComponentModel.PropertyChangedEventArgs("Item[]"));

      //warning Hotfix Tekigo
#if SILVERLIGHT
            OnCollectionChanged(new System.Collections.Specialized.NotifyCollectionChangedEventArgs(System.Collections.Specialized.NotifyCollectionChangedAction.Reset));
#else
      OnCollectionChanged(new System.Collections.Specialized.NotifyCollectionChangedEventArgs(System.Collections.Specialized.NotifyCollectionChangedAction.Replace, new List<T>(items), oldItems));
#endif
    }

    #endregion
  }
}
