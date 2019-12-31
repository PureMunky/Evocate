using System;
using System.Collections.Generic;

namespace Tete.Models.Localization
{

  public class Dictionary
  {
    public Language Language { get; set; }

    public Dictionary<string, Element> Elements { get; set; }
  }
}