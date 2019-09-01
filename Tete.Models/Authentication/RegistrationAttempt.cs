namespace Tete.Models.Authentication
{
  public class RegistrationAttempt
  {
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string DisplayName { get; set; }
  }
}