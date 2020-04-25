using System;
using System.ComponentModel.DataAnnotations;

namespace Tete.Models.Localization
{
  public class Element
  {
    public Guid ElementId { get; set; }
    public string Key { get; set; }
    public string Text { get; set; }

    public Guid LanguageId { get; set; }
  }
}