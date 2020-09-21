using System;
using System.ComponentModel.DataAnnotations;

namespace Tete.Models.Authentication
{
  public class UserBlock
  {
    public Guid UserBlockId { get; set; }

    [Required]
    public Guid UserId { get; set; }
    public User User { get; set; }

    [Required]
    public DateTime Created { get; set; }
    public DateTime EndDate { get; set; }

    public Guid CreatedBy { get; set; }

    public string PublicComments { get; set; }
    public string PrivateComments { get; set; }

    public UserBlock()
    {
      this.UserBlockId = Guid.NewGuid();
      this.UserId = Guid.Empty;
      this.Created = DateTime.UtcNow;
      this.EndDate = DateTime.UtcNow;
      this.CreatedBy = Guid.Empty;
      this.PublicComments = "";
      this.PrivateComments = "";
    }

    public UserBlock(Guid UserId, DateTime EndDate, Guid CreatedBy, string PublicComments, string PrivateComments)
    {
      this.UserBlockId = Guid.NewGuid();
      this.UserId = UserId;
      this.Created = DateTime.UtcNow;
      this.EndDate = EndDate;
      this.CreatedBy = CreatedBy;
      this.PublicComments = PublicComments;
      this.PrivateComments = PrivateComments;
    }
  }
}