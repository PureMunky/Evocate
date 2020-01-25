using System;
using System.ComponentModel.DataAnnotations;

namespace Tete.Models.Localization
{

  public class Language
  {
    public Guid LanguageId { get; set; }
    public string Name { get; set; }
    public bool Active { get; set; }
  }
}