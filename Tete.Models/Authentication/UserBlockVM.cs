using System;

namespace Tete.Models.Authentication
{
  public class UserBlockVM
  {
    public Guid UserId { get; set; }
    public DateTime EndDate { get; set; }
    public string PublicComments { get; set; }
    public string PrivateComments { get; set; }

    public UserBlockVM()
    {
      this.UserId = Guid.Empty;
      this.PublicComments = "";
      this.PrivateComments = "";
    }

    public UserBlockVM(UserBlock block)
    {
      this.UserId = block.UserId;
      this.EndDate = block.EndDate;
      this.PublicComments = block.PublicComments;
      this.PrivateComments = "";
    }
  }
}
