using System;

namespace Tete.Models.Authentication
{

  public class SessionVM
  {
    public string Token { get; set; }

    public DateTime Created { get; set; }

    public SessionVM()
    {

    }
    public SessionVM(Session session)
    {
      this.Token = session.Token;
      this.Created = session.Created;
    }
  }
}