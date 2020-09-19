using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Tete.Api.Contexts;
using Tete.Api.Services.Localization;
using Tete.Models.Authentication;
using Tete.Models.Users;

namespace Tete.Api.Services.Users
{
  public class UserService : ServiceBase
  {

    public UserService(MainContext mainContext, UserVM actor)
    {
      FillData(mainContext, actor);
    }

    public UserService(MainContext mainContext, User actor)
    {
      FillData(mainContext, new UserVM());
      this.Actor = GetUser(actor);
    }

    public UserVM GetUser(Guid UserId)
    {
      return GetUser(this.mainContext.Users.Where(u => u.Id == UserId).FirstOrDefault());
    }

    public UserVM GetUser(User user)
    {
      // var languages = UserLanguageService.GetUserLanguages(user.Id);
      var profiles = this.mainContext.UserProfiles.AsNoTracking().Where(p => p.UserId == user.Id).FirstOrDefault();
      var roles = this.mainContext.AccessRoles.AsNoTracking().Where(r => r.UserId == user.Id).ToList();
      return new UserVM(
        user,
        new List<Models.Localization.UserLanguage>(),
        profiles,
        roles,
        new UserBlockVM(CurrentBlock(user.Id))
      );
    }

    public void SaveUser(UserVM user)
    {
      User dbUser = this.mainContext.Users.Where(u => u.Id == user.UserId).FirstOrDefault();

      if (dbUser != null && (this.Actor.UserId == dbUser.Id || this.Actor.Roles.Contains("Admin")))
      {
        dbUser.DisplayName = user.DisplayName;
        dbUser.Email = user.Email;
        this.mainContext.Users.Update(dbUser);
        this.mainContext.SaveChanges();
      }
    }

    public IEnumerable<UserVM> Search(string searchText)
    {
      var search = searchText.ToLower();
      IEnumerable<UserVM> users = null;

      if (this.Actor.Roles.Contains("Admin"))
      {
        users = this.mainContext.Users.AsNoTracking().Where(u => u.DisplayName.ToLower().Contains(search) || u.Email.ToLower().Contains(search) || u.UserName.ToLower().Contains(search)).Select(u => new UserVM(u));
      }

      return users;
    }

    #region Role Management
    public bool GrantGuestRole(Guid UserId)
    {
      return GrantAccessRole(UserId, "Guest");
    }

    public bool GrantRole(Guid UserId, String RoleName)
    {
      bool created = false;

      if (this.Actor.Roles.Contains("Admin"))
      {
        created = GrantAccessRole(UserId, RoleName);
      }

      return created;
    }
    private bool GrantAccessRole(Guid UserId, String RoleName)
    {
      bool created = false;

      var testRole = this.mainContext.AccessRoles.Where(r => r.UserId == UserId && r.Name == RoleName).FirstOrDefault();

      if (testRole == null)
      {
        LogService.Write("Grant Role", String.Format("User:{0};Role:{1}", UserId, RoleName));
        created = true;
        var role = new AccessRole(UserId, RoleName);
        role.CreatedBy = this.Actor.UserId;
        this.mainContext.AccessRoles.Add(role);
        this.mainContext.SaveChanges();
      }

      return created;
    }

    public bool RemoveGuestRole(Guid UserId)
    {
      return RemoveAccessRole(UserId, "Guest");
    }

    public bool RemoveRole(Guid UserId, string RoleName)
    {
      bool removed = false;

      if (this.Actor.Roles.Contains("Admin"))
      {
        removed = RemoveAccessRole(UserId, RoleName);
      }

      return removed;
    }

    private bool RemoveAccessRole(Guid UserId, string RoleName)
    {
      bool removed = false;

      var dbRole = this.mainContext.AccessRoles.Where(r => r.UserId == UserId && r.Name == RoleName).FirstOrDefault();
      if (dbRole != null)
      {
        LogService.Write("Removed Role", String.Format("User:{0};Role:{1}", UserId, RoleName));
        removed = true;

        this.mainContext.AccessRoles.Remove(dbRole);
        this.mainContext.SaveChanges();
      }
      return removed;
    }

    #endregion

    #region Block
    public bool Block(UserBlockVM block)
    {
      var result = false;

      if (this.Actor.Roles.Contains("Admin"))
      {
        if (!CurrentlyBlocked(block.UserId))
        {
          var newBlock = new UserBlock(block.UserId, block.EndDate, this.Actor.UserId, block.PublicComments, block.PrivateComments);

          this.mainContext.UserBlocks.Add(newBlock);
          this.mainContext.SaveChanges();
          result = true;
        }
      }

      return result;
    }

    private UserBlock CurrentBlock(Guid UserId)
    {
      return this.mainContext.UserBlocks.AsNoTracking().Where(b => b.UserId == UserId && b.EndDate >= DateTime.UtcNow).OrderByDescending(b => b.EndDate).FirstOrDefault();

    }
    private bool CurrentlyBlocked(Guid UserId)
    {
      return (CurrentBlock(UserId) != null);
    }
    #endregion

    private void FillData(MainContext mainContext, UserVM actor)
    {
      this.mainContext = mainContext;
      this.Actor = actor;
    }
  }
}