using System;
using System.ComponentModel.DataAnnotations;

namespace Tete.Models.Authentication
{
  public class AccessRole
  {
    public Guid AccessRoleId { get; set; }

    [Required]
    public Guid UserId { get; set; }

    public string Name { get; set; }

    public DateTime Created { get; set; }

    [Required]
    public Guid CreatedBy { get; set; }

    public AccessRole(Guid UserId, string Name)
    {
      this.AccessRoleId = Guid.NewGuid();
      this.UserId = UserId;
      this.Name = Name;
      this.Created = DateTime.UtcNow;
    }
  }
}