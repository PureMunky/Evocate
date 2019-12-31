using System;
using System.ComponentModel.DataAnnotations;

namespace Tete.Models.Localization
{

  public class UserLanguage
  {
    public Guid UserLanguageId { get; set; }

    [Required]
    public Guid UserId { get; set; }

    [Required]
    public int Priority { get; set; }

    public Boolean Read { get; set; }

    public Boolean Speak { get; set; }

    public UserLanguage()
    {
      this.UserLanguageId = Guid.NewGuid();
      this.Read = true;
      this.Speak = true;
    }
  }
}