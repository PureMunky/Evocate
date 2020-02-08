using System;
using System.Collections.Generic;

namespace Tete.Models.Localization
{

  public class Language
  {
    public Guid LanguageId { get; set; }
    public string Name { get; set; }
    public bool Active { get; set; }

    public ICollection<Element> Elements { get; set; }

  }
}