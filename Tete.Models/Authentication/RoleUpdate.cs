using System;

namespace Tete.Models.Authentication
{
  public class RoleUpdate
  {
    public Guid UserId { get; set; }

    public string Name { get; set; }
  }
}