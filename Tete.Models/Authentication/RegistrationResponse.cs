using System.Collections.Generic;

namespace Tete.Models.Authentication
{
  public class RegistrationResponse
  {
    public bool Successful { get; set; }
    public List<string> Messages { get; set; }

    public LoginAttempt Attempt { get; set; }

    public RegistrationResponse()
    {
      this.Successful = true;
      this.Messages = new List<string>();
    }

    public void Combine(RegistrationResponse response)
    {
      this.Messages.AddRange(response.Messages);
      this.Successful = (this.Successful && response.Successful);
    }
  }
}