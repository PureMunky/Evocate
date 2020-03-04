using System;
using System.ComponentModel.DataAnnotations;

namespace Tete.Models.Localization
{

    public class UserLanguageVM
    {
        public Guid UserLanguageId { get; set; }

        [Required]
        public Guid UserId { get; set; }

        [Required]
        public int Priority { get; set; }

        public Guid LanguageId { get; set; }

        public Language Language { get; set; }

        public Boolean Read { get; set; }

        public Boolean Speak { get; set; }

        public UserLanguageVM(UserLanguage userLanguage)
        {
            this.UserLanguageId = userLanguage.UserLanguageId;
            this.UserId = userLanguage.UserId;
            this.Priority = userLanguage.Priority;
            this.LanguageId = userLanguage.LanguageId;
            this.Language = userLanguage.Language;
            this.Read = userLanguage.Read;
            this.Speak = userLanguage.Speak;
        }
    }
}