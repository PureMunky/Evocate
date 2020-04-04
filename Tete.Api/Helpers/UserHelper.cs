using System;
using Tete.Models.Authentication;

namespace Tete.Api.Helpers
{
    public static class UserHelper
    {
        private static UserVM CurrentUserOverride;

        public static UserVM CurrentUser() {
            var user = new UserVM();

            // var token = Microsoft.AspNetCore.Http.HttpContext.Current.Request.Cookies["Tete.SessionToken"];

            if (CurrentUserOverride != null) {
                user = CurrentUserOverride;
            }

            return user;
        }

        public static void setCurrentUser(UserVM user) {
            CurrentUserOverride = user;
        }
    }
}