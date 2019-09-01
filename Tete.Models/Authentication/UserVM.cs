namespace Tete.Models.Authentication
{
  public class UserVM
  {
    public string DisplayName { get; set; }

    public string Email { get; set; }

    public string UserName { get; set; }

    public UserVM(User user)
    {
      if (user != null)
      {
        this.DisplayName = user.DisplayName;
        this.Email = user.Email;
        this.UserName = user.UserName;
      }
    }
  }
}