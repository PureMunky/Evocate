using System;
using System.Web;

namespace Tete.Models.Authentication
{

  /// <summary>
  /// The simplest user object.
  /// </summary>
  public class User
  {

    /// <summary>
    /// The visual representation of the user.
    /// </summary>
    /// <value></value>
    public string DisplayName {get; set;}

    /// <summary>
    /// The unique user id assinged to each user.
    /// </summary>
    /// <value></value>
    public Guid Id {get; set;}

    /// <summary>
    /// The user's email address.
    /// </summary>
    /// <value></value>
    public EmailAddress Email {get; set;}
    
  }
}