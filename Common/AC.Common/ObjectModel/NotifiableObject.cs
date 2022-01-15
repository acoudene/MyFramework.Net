using System.ComponentModel;
using System.Runtime.Serialization;

namespace AC.Common
{
  /// <summary>
  /// Classe de base implémentant INotifyPropertyChanged
  /// </summary>
  [DataContract]
  public abstract class NotifiableObject : INotifyPropertyChanged, IDisposable
  {
    #region Attributs

    private Lazy<PropertyChangedManager> _propertyChangedManager;
    private bool _isDisposed = false;

    #endregion

    #region Propriétés

    /// <summary>
    /// Obtient le <see cref="PropertyChangedManager"/>
    /// </summary>
    protected PropertyChangedManager PropertyChangedManager
    {
      get { return _propertyChangedManager.Value; }
    }

    #endregion

    #region Constructeur

    /// <summary>
    /// Constructeur
    /// </summary>
    public NotifiableObject()
    {
      _propertyChangedManager = new Lazy<PropertyChangedManager>(
          new Func<PropertyChangedManager>(() => new PropertyChangedManager(this)));
    }

    ~NotifiableObject()
    {
      Dispose(false);
    }

    #endregion

    #region Gestion de la déserialisation

    [OnDeserializing]
    private void OnDeserializing(StreamingContext context)
    {
      OnDeserializing();
    }

    protected virtual void OnDeserializing()
    {
      _propertyChangedManager = new Lazy<PropertyChangedManager>(new Func<PropertyChangedManager>(() => new PropertyChangedManager(this)));
    }

    #endregion

    #region INotifyPropertyChanged Members

    /// <summary>
    /// Evénement déclenché lors du changement de valeur d'une propriété
    /// </summary        
    public event PropertyChangedEventHandler? PropertyChanged
    {
      add { if (value!=null) PropertyChangedManager.AddHandler(value); }
      remove { if (value != null) PropertyChangedManager.RemoveHandler(value); }
    }

    /// <summary>
    /// Déclenche l'événement <see cref="PropertyChanged"/>
    /// </summary>
    /// <param name="propertyName"></param>
    protected virtual void OnPropertyChanged(string propertyName)
    {
      // Vérifie que le nom de la propriété existe
      CheckPropertyName(propertyName);
      // Déclenche les callbacks
      PropertyChangedManager.NotifyPropertyChanged(propertyName);
    }

    /// <summary>
    /// Vérifie que le nom de la propriété existe bien
    /// </summary>
    /// <param name="propertyName">nom de la propriété</param>
    /// <remarks>n'existe qu'en debug</remarks>
    [System.Diagnostics.Conditional("DEBUG")]
    [System.Diagnostics.DebuggerStepThrough()]
    private void CheckPropertyName(string propertyName)
    {
      if (TypeDescriptor.GetProperties(this)[propertyName] == null)
        throw new ArgumentException("Unknown property name ({0})".FormatString(propertyName));
    }
    #endregion

    #region Méthodes publiques

    /// <summary>
    /// Créé un abonnement faible sur le changement de valeur d'une propriété
    /// </summary>
    /// <typeparam name="T">type d'objet sur lequel on veut s'abonner qui sera passé au callback</typeparam>
    /// <param name="propertyName">nom de la propriété à surveiller</param>
    /// <param name="callback">callback à appeler lors du déclenchement de l'événement PropertyChanged</param>
    /// <returns>un objet à disposer lorsqu'on souhaite se désabonner</returns>
    public IDisposable SubscribeToPropertyChanged<T>(string propertyName, Action<T> callback)
        where T : INotifyPropertyChanged
    {
      return PropertyChangedManager.SubscribeToPropertyChanged<T>(propertyName, callback);
    }

    /// <summary>
    /// Créé un abonnement faible sur le changement de valeur d'une propriété
    /// </summary>
    /// <param name="propertyName">nom de la propriété à surveiller</param>
    /// <param name="callback">callback à appeler lors du déclenchement de l'événement PropertyChanged</param>
    /// <returns>un objet à disposer lorsqu'on souhaite se désabonner</returns>
    public IDisposable SubscribeToPropertyChanged(string propertyName, Action callback)
    {
      return PropertyChangedManager.SubscribeToPropertyChanged<NotifiableObject>(propertyName, n => callback());
    }

    /// <summary>
    /// Supprime un abonnement faible sur le changement de valeur d'une propriété
    /// </summary>
    /// <typeparam name="T">type d'objet sur lequel on veut se désabonner</typeparam>
    /// <param name="propertyName">nom de la propriété à ne plus surveiller</param>
    /// <param name="callback">callback appelé lors du déclenchement de l'événement PropertyChanged</param>
    public void UnsubscribeToPropertyChanged<T>(string propertyName, Action<T> callback)
        where T : INotifyPropertyChanged
    {
      PropertyChangedManager.UnsubscribeToPropertyChanged<T>(propertyName, callback);
    }

    /// <summary>
    /// Supprime un abonnement faible sur le changement de valeur d'une propriété
    /// </summary>
    /// <param name="propertyName">nom de la propriété à ne plus surveiller</param>
    /// <param name="callback">callback appelé lors du déclenchement de l'événement PropertyChanged</param>
    public void UnsubscribeToPropertyChanged(string propertyName, Action callback)
    {
      PropertyChangedManager.UnsubscribeToPropertyChanged<NotifiableObject>(propertyName, n => callback());
    }

    #endregion

    #region IDisposable Members

    /// <summary>
    /// Nettoie les resources
    /// </summary>
    public void Dispose()
    {
      Dispose(true);
      GC.SuppressFinalize(this);
    }

    protected void Dispose(bool disposing)
    {
      if (!_isDisposed)
      {
        _isDisposed = true;

        if (disposing)
        {
          PropertyChangedManager.Dispose();
          OnDispose();
        }
      }
    }

    protected virtual void OnDispose()
    {
    }

    #endregion
  }
}
