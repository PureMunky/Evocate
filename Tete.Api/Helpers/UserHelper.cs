using System;
using Tete.Models.Authentication;
using Tete.Api.Contexts;
using Microsoft.AspNetCore.Http;
using Tete.Api.Services.Authentication;

namespace Tete.Api.Helpers
{
    public static class UserHelper
    {
        private static UserVM CurrentUserOverride;

        public static UserVM CurrentUser(HttpContext current, MainContext mainContext) {
            var token = current.Request.Cookies["Tete.SessionToken"];
            var user = new LoginService(mainContext).GetUserVMFromToken(token);

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