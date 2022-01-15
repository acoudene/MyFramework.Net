using System.ComponentModel;

namespace AC.Common
{
  public class NotifiableBulkObservableCollection<T> : BulkObservableCollection<T>, INotifyPropertyChanged
        where T : class, INotifyPropertyChanged
  {
    protected override void OnCollectionChanged(System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
    {
      switch (e.Action)
      {
        case System.Collections.Specialized.NotifyCollectionChangedAction.Add:
          if (e.NewItems != null) Subscribe(e.NewItems);
          break;
        case System.Collections.Specialized.NotifyCollectionChangedAction.Remove:
          if (e.OldItems != null) UnSubscribe(e.OldItems);
          break;
        case System.Collections.Specialized.NotifyCollectionChangedAction.Replace:
        case System.Collections.Specialized.NotifyCollectionChangedAction.Reset:
          if (e.OldItems != null) UnSubscribe(e.OldItems);
          if (e.NewItems != null) Subscribe(e.NewItems);
          break;
        default:
          break;
      }

      base.OnCollectionChanged(e);
    }

    private void Subscribe(System.Collections.IList iList)
    {
      if (iList != null)
      {
        foreach (INotifyPropertyChanged item in iList)
          item.PropertyChanged += new PropertyChangedEventHandler(item_PropertyChanged!);
      }
    }

    private void UnSubscribe(System.Collections.IList iList)
    {
      if (iList != null)
      {
        foreach (INotifyPropertyChanged item in iList)
          item.PropertyChanged -= new PropertyChangedEventHandler(item_PropertyChanged!);
      }
    }

    private void item_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
      OnPropertyChanged(sender, (e.PropertyName == null) ? string.Empty: e.PropertyName);
    }

    public new event PropertyChangedEventHandler? PropertyChanged;

    protected void OnPropertyChanged(object item, string propertyName)
    {
      if (PropertyChanged != null)
        PropertyChanged(item, new PropertyChangedEventArgs(propertyName));
    }
  }
}
