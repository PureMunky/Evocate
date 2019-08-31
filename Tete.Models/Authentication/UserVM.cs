namespace Tete.Models.Authentication
{
  public class UserVM
  {
    public string DisplayName { get; set; }

    public string Email { get; set; }

    public UserVM(User user)
    {
      this.DisplayName = user.DisplayName;
      this.Email = user.Email;
    }
  }
}