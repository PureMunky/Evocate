using System;
using System.Collections.Generic;

namespace Tete.Models.Localization
{

    public class LanguageVM
    {
        public Guid LanguageId { get; set; }
        public string Name { get; set; }
        public Dictionary<string, string> Elements { get; set; }

        public LanguageVM(Language language)
        {
            this.LanguageId = language.LanguageId;
            this.Name = language.Name;
            this.Elements = new Dictionary<string, string>();
            foreach (Element e in language.Elements)
            {
                this.Elements.Add(e.Key, e.Text);
            }
        }
    }
}