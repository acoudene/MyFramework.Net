//$9=======================================================================
//$9            TECHNIDATA COPYRIGHT - NOT TO BE REPRODUCED                
//$9          WITHOUT WRITTEN CONSENT OF TECHNIDATA SAS (FRANCE)           
//$9=======================================================================
//$2-----------------------------------------------------------------------
//$2 Module : TDBusinessException.cs                      Project: POCNA
//$2 Purpose: Business exception managed for TD Framework
//$2 Author : Anthony COUDENE                            Date: Oct 16,2012
//$2-----------------------------------------------------------------------
//$2-----------------------------------------------------------------------
//$2 OM     : TFS10.15067    TECHNIDATA                       Project: POCNA
//$2 Purpose: Creation
//$2 Author : Anthony COUDENE                            Date: Oct 16,2012
//$2-----------------------------------------------------------------------
//$2-----------------------------------------------------------------------
//$2 OM     : 24192    TECHNIDATA                            Project: FGSP
//$2 Purpose: transfer exception built-in collection
//$2 Author : WP                                        Date : 2011/12/15
//$2-----------------------------------------------------------------------
//$2-----------------------------------------------------------------------
//$2 OM     : TFS10.15067    TECHNIDATA                       Project: POCNA
//$2 Purpose: Silverlight
//$2 Author : Anthony COUDENE                            Date: Oct 16,2012
//$2-----------------------------------------------------------------------

using System;
using System.Collections;
namespace AC.IoC
{
  /// <summary>
  /// Classe d'exception pour la couche business
  /// </summary>
  [Serializable]
  public class IoCException : Exception
  {
    public IoCException(string message)
        : base(message)
    {
    }

    public IoCException(string format, params object[] args)
        : base(String.Format(format, args))
    {
    }

    public IoCException(string message, Exception innerException)
        : base(message, innerException)
    {
      // transfer exception base data to high level exception
      this.Data.Clear();
      foreach (DictionaryEntry de in innerException.Data)
      {
        this.Data.Add(de.Key, de.Value);
      }
    }

    public IoCException(Exception innerException, string format, params object[] args)
        : base(String.Format(format, args), innerException)
    {
    }

    public IoCException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
        : base(info, context)
    {
    }
  }
}
